using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXin.Lib.Core.Helper;
using WeiXin.Lib.Core.Helper.WXPay;
using WeiXin.Lib.Core.Models.UnifiedMessage;
using WeiXin.Lib.Core.PayModel;
using Wxpay;

namespace _06ThirdplatForm.eggsoft.cn.v3pay_weixin
{
    public partial class Default : System.Web.UI.Page
    {
        //protected string payout_trade_no = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            Pay();
        }

        protected void Pay()
        {

            try
            {
                PayModel payModel = null;
                string payout_trade_no = "";

                string strOrderINT = Request.QueryString["OrderINT"];//订单记录的ID

                string strOrderNum = "";
                string myAllMoney = "";
                string strOrderName = "";
                string strParent = "";

                //Eggsoft.Common.debug_Log.Call_WriteLog("OrderINT=" + strOrderINT);
                if (strOrderINT != null)
                {
                    strOrderINT = strOrderINT.ToString();
                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.Model.tab_Order myModel = my_BLL_tab_Order.GetModel(Convert.ToInt32(strOrderINT));

                    strOrderNum = myModel.OrderNum;
                    strOrderName = myModel.OrderName;
                    myAllMoney = myModel.TotalMoney.ToString();
                }
                else
                {
                    strOrderNum = Request.QueryString["OrderNum"].ToString();
                    myAllMoney = Request.QueryString["myAllMoney"].ToString();
                    strOrderName = Request.QueryString["OrderName"].ToString();
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

                payout_trade_no = strOrderNum;
                string description = strOrderName; //商品描述
                string totalFee = intMoney.ToString();//订单金额（单位：分）
                string createIp = Eggsoft.Common.CommUtil.GetClientIP();
                string notifyUrl = Eggsoft_Public_CL.Pub.GetAppConfiug_WeiXin_Developmebt_URL() + "/v3pay_weixin/warning-" + strOrderNum + ".aspx"; //通知url
                string openId = Model_tab_User.OpenID;
                //prepayid 只有第一次支付时生成，如果需要再次支付，必须拿之前生成的prepayid。
                //也就是说一个orderNo 只能对应一个prepayid
                string prepayid = "wx201502131652087ba48071450108438523"; //string.Empty;

                #region 之前生成过 prepayid，此处可略过

                WxPayModel wxPayModel = new WxPayModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                //创建Model
                UnifiedWxPayModel model = UnifiedWxPayModel.CreateUnifiedModel(wxPayModel.AppId, wxPayModel.PartnerID, wxPayModel.PartnerKey);

                //预支付
                UnifiedPrePayMessage result = WeiXinHelper.UnifiedPrePay(model.CreatePrePayPackage(description, strOrderNum, totalFee, createIp, notifyUrl, openId));

                if (result == null
                        || !result.ReturnSuccess
                        || !result.ResultSuccess
                        || string.IsNullOrEmpty(result.Prepay_Id))
                {
                    throw new Exception("获取PrepayId 失败");

                    //Eggsoft.Common.JsUtil.ShowMsg("暂时不能支付", -2);
                }

                //预支付订单wx201502121946219122ebdc300363851322
                prepayid = result.Prepay_Id;

                #endregion

                //JSAPI 支付参数的Model

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
                Eggsoft.Common.debug_Log.Call_WriteLog(payModel.ToString());



                payModel_AppId.Value = payModel.AppId;
                payModel_Timestamp.Value = payModel.Timestamp;
                payModel_Noncestr.Value = payModel.Noncestr;
                payModel_Package.Value = payModel.Package;
                payModel_PaySign.Value = payModel.PaySign;
                Inputpayout_trade_no.Value = payout_trade_no;

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione,"请求支付旧版");
            }
            finally
            {
            }


        }
    }
}