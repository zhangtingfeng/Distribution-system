using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfService1.User
{
    /// <summary>
    /// WS_UserInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WS_UserInfo : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public string getshowMyInfoNum()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            try
            {
                //Eggsoft.Common.debug_Log.Call_WriteLog(context.toJsonString(), "获取消息显示", "试图找出报错原因");


                String strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                String strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                String strInfomarker = context.QueryString["Infomarker"];
                String strTSign = (context.QueryString["TSign"]);

                #region 检查签名
                string strSafeCode = "";
                EggsoftWX.Model.tab_User Modeltab_User = new EggsoftWX.BLL.tab_User().GetModel(strUserID.toInt32());
                if (Modeltab_User != null) strSafeCode = Modeltab_User.SafeCode;
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strUserID + strShopClientID + Eggsoft.Common.DESCrypt.hex_md5_2(strSafeCode) + strInfomarker);
                if (strTSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strUserID + strShopClientID  strSafeCode" + strUserID + " " + strShopClientID + " " + strSafeCode, "获取消息显示签名失败", "pub_Int_Session_CurUserID=" + strUserID);
                    return "";
                }
                #endregion 检查签名
                int intUserID = 0;
                int intShopClientID = 0;

                int.TryParse(strUserID, out intUserID);
                int.TryParse(strShopClientID, out intShopClientID);
                if ((Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString()) != intShopClientID))
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("获取消息显示严重错误");
                }
                Int32 Int32Count = 0;
                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                if ((intShopClientID > 0) && (intUserID > 0) && (String.IsNullOrEmpty(strInfomarker) == false))
                {
                    Int32Count = bll_b011_InfoAlertMessage.ExistsCount("ShopClient_ID=@ShopClient_ID and UserID=@UserID and Type=@Type and Readed=0", intShopClientID, intUserID, strInfomarker);
                }

                switch (strInfomarker)
                {
                    case "Info_ZongFenXiaoShorRu":///代理总收入

                        //Int32Count = 1;
                        break;
                    case "Info_ZhangHuYuE"://账户余额
                        //Int32Count = 2;
                        break;
                    case "Info_GouWuHongBao"://购物券
                        break;
                    case "Info_TotalWealth":///财富积分
                        break;
                    case "Info_cart_good3":///我的订单  查看全部已购宝贝
                        break;
                    case "Info_cart_good":///待付款
                        break;
                    case "Info_cart_good2"://待收货
                        break;
                    case "Info_cart"://购物车
                        break;
                    case "Info_myYunYingOrder"://我的运营中心订单
                        break;
                    case "Info_myYunYingMoney"://我的运营中心余额
                        break;
                    case "Info_MySonmember"://购物车  我的直推 与间推
                        break;
                    case "Info_myYunYingAllMember"://运营中心所有会员
                        break;

                }




                str = "{\"ErrorCode\":" + 0 + ",\"CountShow\":" + Int32Count + "}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "获取消息显示", "线程异常");
            }
            catch (Exception Exceptione)
            {

                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "获取消息显示", "程序报错所有的请求");
                Eggsoft.Common.debug_Log.Call_WriteLog(context.QueryString.toJsonString(), "获取消息显示", "程序报错所有的请求");
            }
            finally
            {

            }
            return str;
        }



        [WebMethod]
        public string getshowMyInfoNum_ByShopClient()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";
            string strShopClientID = "";
            string strInfomarker = "";


            try
            {
                strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["ShopClientID"]);
                strInfomarker = context.QueryString["Infomarker"];
                String strTSign = (context.QueryString["TSign"]);


                #region 检查商户的签名
                string strSafeCode = "";
                var varNetUserSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2(strShopClientID + "Aakfnkasjfdaskjfhas");
                string strNetSign = Eggsoft.Common.DESCrypt.hex_md5_8(strShopClientID + varNetUserSafeCode + strInfomarker);
                if (strTSign != strNetSign)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strShopClientID  strSafeCode" + " " + strShopClientID + " " + strSafeCode, "后台商户显示签名失败", "strShopClientID=" + strShopClientID);
                    return "";
                }
                #endregion 检查商户的签名
                Int32 Int32Count = 10;


                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();

                Int32Count = bll_b011_InfoAlertMessage.ExistsCount("ShopClient_ID=@ShopClient_ID  and Type=@Type and isnull(Done,0)=0", strShopClientID, strInfomarker);




                str = "{\"ErrorCode\":" + 0 + ",\"CountShow\":" + Int32Count + "}";
                if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + str + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "获取商户消息显示", "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("strShopClientID=" + strShopClientID + "  strInfomarker=" + strInfomarker, "获取商户消息显示", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "获取商户消息显示");
            }
            finally
            {

            }
            return str;
        }


    }
}
