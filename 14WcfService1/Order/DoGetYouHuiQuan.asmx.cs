using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.Order
{
    /// <summary>
    /// DoGetYouHuiQuan 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DoGetYouHuiQuan : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string doGetOneYouHuiQuan()
        {
            string str = "";

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;


                string strpub_Int_Session_CurUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strpub_Int_Session_CurUserID"]);
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);
                int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpub_Int_Session_CurUserID);


                string strpub_Int_ShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strpub_Int_ShopClientID"]);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_Int_ShopClientID, out pub_Int_ShopClientID);

                ////直接领用线上的
                string strVouchersSchemeID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["VouchersSchemeID"]);
                int pub_Int_VouchersSchemeID = 0;
                int.TryParse(strVouchersSchemeID, out pub_Int_VouchersSchemeID);

                ///用户手工录入的号码  线下发放的
                string strVouchersNum = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["VouchersNum"]);

                if (pub_Int_VouchersSchemeID > 0)
                {
                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme BLLtab_ShopClient_Shopping_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                    EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme ModelVouchersScheme = BLLtab_ShopClient_Shopping_VouchersScheme.GetModel(pub_Int_VouchersSchemeID);
                    if (ModelVouchersScheme == null)
                    {
                        str = "{\"ErrorCode\":-4}";////表示购物券不存在 或者已过期  
                    }
                    else if (ModelVouchersScheme.ValidateEndTime < DateTime.Now)
                    {
                        str = "{\"ErrorCode\":-3}";////表示购物券不存在 或者已过期  
                    }
                    else
                    {
                        if (ModelVouchersScheme.LimitHowMany != 0)
                        {///0表示不限制
                            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLLtab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                            int intHaveGotCount = BLLtab_ShopClient_Shopping_VouchersScheme_Detail.ExistsCount("Scheme_ID=" + pub_Int_VouchersSchemeID + " and ShopClientID=" + pub_Int_ShopClientID + " and UserID=" + pub_Int_Session_CurUserID);
                            if (intHaveGotCount >= ModelVouchersScheme.LimitHowMany)
                            {
                                str = "{\"ErrorCode\":3,\"LimitOnePerson\":" + ModelVouchersScheme.LimitHowMany + "}";////每人限制领用  
                            }
                            else
                            {
                                Int32 ThisDetailID = Eggsoft_Public_CL.GoodP_YouHuiQuan.GetOneYouHuiQuan(pub_Int_Session_CurUserID, pub_Int_ShopClientID, pub_Int_VouchersSchemeID, ModelVouchersScheme.Money.toDecimal(), Convert.ToDateTime(ModelVouchersScheme.ValidateStartTime), Convert.ToDateTime(ModelVouchersScheme.ValidateEndTime));

                                str = "{\"ErrorCode\":1,\"ThisYouHuiQuanDetailID\":" + ThisDetailID + "}";////成功领用 
                            }
                        }
                        else
                        {
                            #region 检查发放总量
                            int intAllCount = ModelVouchersScheme.AllCount.toInt32();
                            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLLtab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                            int intPutCount = BLLtab_ShopClient_Shopping_VouchersScheme_Detail.ExistsCount("Scheme_ID=" + pub_Int_VouchersSchemeID + " and ShopClientID=" + pub_Int_ShopClientID);
                            if (intPutCount >= intAllCount)
                            {
                                str = "{\"ErrorCode\":-5}";////优惠券已被领用完毕
                            }
                            else
                            {
                                Int32 ThisDetailID = Eggsoft_Public_CL.GoodP_YouHuiQuan.GetOneYouHuiQuan(pub_Int_Session_CurUserID, pub_Int_ShopClientID, pub_Int_VouchersSchemeID, ModelVouchersScheme.Money.toDecimal(), Convert.ToDateTime(ModelVouchersScheme.ValidateStartTime), Convert.ToDateTime(ModelVouchersScheme.ValidateEndTime));
                                str = "{\"ErrorCode\":1,\"ThisYouHuiQuanDetailID\":" + ThisDetailID + "}";////成功领用  
                            }
                            #endregion 检查发放总量

                        }
                    }
                }
                else if (string.IsNullOrEmpty(strVouchersNum) == false)
                {
                    #region 领取  输入购物券号码
                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLLtab_ShopClient_Shopping_VouchersScheme_Detail = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                    EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Modeltab_ShopClient_Shopping_VouchersScheme_Detail = BLLtab_ShopClient_Shopping_VouchersScheme_Detail.GetModel("VouchersNum=@VouchersNum and ShopClientID=@ShopClientID", strVouchersNum, pub_Int_ShopClientID);
                    if (Modeltab_ShopClient_Shopping_VouchersScheme_Detail == null)
                    {
                        str = "{\"ErrorCode\":-4}";
                    }
                    else if (Modeltab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime < DateTime.Now)
                    {
                        str = "{\"ErrorCode\":-3}";
                    }
                    else
                    {
                        if (Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID > 0)
                        {////可能是别人转发给我的

                            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();

                            if (Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID != pub_Int_Session_CurUserID && (Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID_Old == 0 || Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID_Old == null))////只能转发一次
                            {
                                Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID_Old = Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID;
                                Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID = pub_Int_Session_CurUserID;
                                Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UpdateTime = DateTime.Now;
                                BLLtab_ShopClient_Shopping_VouchersScheme_Detail.Update(Modeltab_ShopClient_Shopping_VouchersScheme_Detail);

                                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID.toInt32());

                                str = "{\"ErrorCode\":4,\"ThisYouHuiQuanDetailID\":" + Modeltab_ShopClient_Shopping_VouchersScheme_Detail.ID + ",\"GotFromFriend\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(Model_tab_User.NickName) + "\"}";////成功领用朋友的 }

                            }
                            else
                            {
                                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID.toInt32());

                                str = "{\"ErrorCode\":2,\"ErrorCodeGetNickName\":\"" + Microsoft.JScript.GlobalObject.encodeURIComponent(Model_tab_User.NickName) + "\"}";////表示购物券已被领用
                            }
                        }
                        else if (Modeltab_ShopClient_Shopping_VouchersScheme_Detail.Scheme_ID > 0)
                        {
                            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme BLLtab_ShopClient_Shopping_VouchersScheme = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme();
                            EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme ModelVouchersScheme = BLLtab_ShopClient_Shopping_VouchersScheme.GetModel(Modeltab_ShopClient_Shopping_VouchersScheme_Detail.Scheme_ID.toInt32());
                            Boolean BooleanIFCanGet = false;

                            int intHaveGotCount = BLLtab_ShopClient_Shopping_VouchersScheme_Detail.ExistsCount("Scheme_ID=" + Modeltab_ShopClient_Shopping_VouchersScheme_Detail.Scheme_ID + " and ShopClientID=" + pub_Int_ShopClientID + " and UserID=" + pub_Int_Session_CurUserID);
                            if (intHaveGotCount > 0)
                            {
                                if (ModelVouchersScheme.LimitHowMany != 0)
                                {
                                    if (intHaveGotCount >= ModelVouchersScheme.LimitHowMany)
                                    {
                                        str = "{\"ErrorCode\":3,\"LimitOnePerson\":" + ModelVouchersScheme.LimitHowMany + "}";////每人限制领用  
                                    }
                                    else
                                    {
                                        BooleanIFCanGet = true;
                                    };
                                }
                                else///0表示不限制领用张数
                                {
                                    BooleanIFCanGet = true;
                                }
                            }
                            else
                            {
                                BooleanIFCanGet = true;
                            }


                            #region 可以领取的动作
                            if (BooleanIFCanGet)
                            {
                                if (ModelVouchersScheme.ValidateTypeRelativeCheck.toBoolean())
                                {
                                    DateTime tMinValidateDate = DateTime.Now.AddDays(ModelVouchersScheme.ValidateDateTypeRelative.toInt32());
                                    if (tMinValidateDate < Modeltab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime)
                                    {
                                        Modeltab_ShopClient_Shopping_VouchersScheme_Detail.ValidateEndTime = tMinValidateDate;////领用后多少天过期
                                    }
                                }
                                Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UserID = pub_Int_Session_CurUserID;
                                Modeltab_ShopClient_Shopping_VouchersScheme_Detail.UpdateTime = DateTime.Now;
                                BLLtab_ShopClient_Shopping_VouchersScheme_Detail.Update(Modeltab_ShopClient_Shopping_VouchersScheme_Detail);

                                str = "{\"ErrorCode\":1,\"ThisYouHuiQuanDetailID\":" + Modeltab_ShopClient_Shopping_VouchersScheme_Detail.ID + "}";////成功领用 }

                            }
                            #endregion 可以领取的动作

                        }
                    }
                    #endregion 领取  输入购物券号码
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            Eggsoft.Common.debug_Log.Call_WriteLog(str, "优惠券", "JS返回");
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }
    }
}
