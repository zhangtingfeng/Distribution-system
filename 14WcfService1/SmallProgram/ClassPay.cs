using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Lib.Core.Helper;
using WeiXin.Lib.Core.Helper.WXPay;
using WeiXin.Lib.Core.Models.UnifiedMessage;
using WeiXin.Lib.Core.PayModel;

namespace _14WcfService1.SmallProgram
{
    public class ClassPay
    {
        private static object object_GetWeiXinPay = new object();


        public static string spayModelGetPay(HttpRequest context)
        {
            string strReturn = "";
            lock (object_GetWeiXinPay)
            {
                //var response = HttpContext.Current.Response;
                // var context = HttpContext.Current.Request;

                String strOrderINT = context.QueryString["OrderINT"];//订单记录的ID
                String strShopClientID = context.QueryString["ShopClientID"];
                string WXSPtypeApp = context.QueryString["typeApp"];

                string strOrderNum = "";
                string myAllMoney = "";
                string strOrderName = "";
                string strParent = "";

                Eggsoft.Common.debug_Log.Call_WriteLog("OrderINT=" + strOrderINT);
                if (strOrderINT != null)
                {
                    //strOrderINT = strOrderINT.ToString();
                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.Model.tab_Order myModel = my_BLL_tab_Order.GetModel(Convert.ToInt32(strOrderINT));

                    strOrderNum = myModel.OrderNum;
                    strOrderName = myModel.OrderName;
                    myAllMoney = myModel.TotalMoney.ToString();
                }
                else
                {
                    strOrderNum = context.QueryString["OrderNum"].ToString();
                    myAllMoney = context.QueryString["myAllMoney"].ToString();
                    strOrderName = context.QueryString["OrderName"].ToString();
                }
                //Eggsoft.Common.debug_Log.Call_WriteLog("是否能支付的根本原因0 OrderNum=" + strOrderNum + "  myAllMoney=" + myAllMoney + "  OrderName=" + strOrderName);


                EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order Model_tab_Order = BLL_tab_Order.GetModel("OrderNum='" + strOrderNum + "'");

                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                if (BLL_tab_Orderdetails.Exists("OrderID=" + Model_tab_Order.ID))
                {
                    EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel("OrderID=" + Model_tab_Order.ID);
                    int intParentID = Convert.ToInt32(Model_tab_Orderdetails.ParentID);
                    if (intParentID > 0)
                    {
                        strParent = intParentID.ToString();
                    }
                }

                int userID = Convert.ToInt32(Model_tab_Order.UserID);
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(userID);


                Decimal myAllDecimal = Decimal.Parse(myAllMoney);
                decimal decimalAllMoney = decimal.Multiply(myAllDecimal, 100);
                int intMoney = Decimal.ToInt32(decimalAllMoney);

                string payout_trade_no = strOrderNum;
                string description = strOrderName; //商品描述
                string totalFee = intMoney.ToString();//订单金额（单位：分）
                string createIp = Eggsoft.Common.CommUtil.GetClientIP();
                string notifyUrl = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/v3pay_weixin/warning-" + strOrderNum + ".aspx"; //通知url
                string openId = Model_tab_User.OpenID; if (WXSPtypeApp == "WXSP") openId = Model_tab_User.SmallProgramOpenID;
                //prepayid 只有第一次支付时生成，如果需要再次支付，必须拿之前生成的prepayid。
                //也就是说一个orderNo 只能对应一个prepayid
                string prepayid = string.Empty;


                #region 之前生成过 prepayid，此处可略过

                WxPayModel wxPayModel = new WxPayModel(Convert.ToInt32(strShopClientID), WXSPtypeApp);

                //创建Model
                UnifiedWxPayModel model = UnifiedWxPayModel.CreateUnifiedModel(wxPayModel.AppId, wxPayModel.PartnerID, wxPayModel.PartnerKey);
                Eggsoft.Common.debug_Log.Call_WriteLog(Newtonsoft.Json.JsonConvert.SerializeObject(model), "预支付返回", "创建Model");
                Eggsoft.Common.debug_Log.Call_WriteLog(notifyUrl, "预支付返回", "resultnotifyUrl");
                //预支付
                UnifiedPrePayMessage result = WeiXinHelper.UnifiedPrePay(model.CreatePrePayPackage(description, strOrderNum, totalFee, createIp, notifyUrl, openId));
                Eggsoft.Common.debug_Log.Call_WriteLog(Newtonsoft.Json.JsonConvert.SerializeObject(result), "预支付返回", "result");

                if (result == null
                        || !result.ReturnSuccess
                        || !result.ResultSuccess
                        || string.IsNullOrEmpty(result.Prepay_Id))
                {
                    throw new Exception("获取PrepayId 失败  可能使 支付密码修改了 或者权限问题 联系登陆微信支付后台查看");

                    //Eggsoft.Common.JsUtil.ShowMsg("暂时不能支付", -2);
                }

                //预支付订单wx201502121946219122ebdc300363851322
                prepayid = result.Prepay_Id;

                #endregion


                //JSAPI 支付参数的Model
                PayModel payModel = null;
                payModel = new PayModel()
                {
                    AppId = model.AppId,
                    Package = string.Format("prepay_id={0}", prepayid),
                    Timestamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString(),
                    Noncestr = WeiXin.Lib.Core.Helper.WXPay.CommonUtil.CreateNoncestr(),
                };

                Dictionary<string, string> nativeObj = new Dictionary<string, string>();
                nativeObj.Add("appId", payModel.AppId);
                nativeObj.Add("package", payModel.Package);
                nativeObj.Add("timeStamp", payModel.Timestamp);
                nativeObj.Add("nonceStr", payModel.Noncestr);
                nativeObj.Add("signType", payModel.SignType);
                payModel.PaySign = model.GetCftPackage(nativeObj); //生成JSAPI 支付签名


                int intErrorCode = 1;

                try
                {
                    intErrorCode = 0;
                    strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\",\"OrderNum\":\"" + strOrderNum + "\",\"appId\":\"" + payModel.AppId + "\",\"nonceStr\":\"" + payModel.Noncestr + "\",\"timestamp\":\"" + payModel.Timestamp + "\",\"package\":\"" + payModel.Package + "\",\"paySign\":\"" + payModel.PaySign + "\"}";

                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                    intErrorCode = -1;
                    strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\"}";
                }
                finally
                {

                }
            }

            Eggsoft.Common.debug_Log.Call_WriteLog(strReturn, "预支付返回", "spayModelGetPay");
            return strReturn;
        }
    }
}