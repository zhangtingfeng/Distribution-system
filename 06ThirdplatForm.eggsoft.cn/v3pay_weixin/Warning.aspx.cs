using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Eggsoft.Common;
using WeiXin.Lib.Core.Helper;
using WeiXin.Lib.Core.Helper.WXPay;
using WeiXin.Lib.Core.Models.UnifiedMessage;

namespace _06ThirdplatForm.eggsoft.cn.v3pay_weixin
{
    public partial class Warning : System.Web.UI.Page
    {
        /// <summary>
        ///微信异步 支付通知的用的   微信异步 支付通知URL地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Stream inputstream = Request.InputStream;
                byte[] b = new byte[inputstream.Length];
                inputstream.Read(b, 0, (int)inputstream.Length);
                string inputstr = UTF8Encoding.UTF8.GetString(b);
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(inputstr);
                Eggsoft.Common.debug_Log.Call_WriteLog("inputstr=" + inputstr + "   OrderNum=" + Request.QueryString["OrderNum"].ToString(), "支付通知");

                Dictionary<string, string> dic = new Dictionary<string, string>();
                string sign = string.Empty;
                foreach (XmlNode node in doc.FirstChild.ChildNodes)
                {
                    if (node.Name.ToLower() != "sign")
                        dic.Add(node.Name, node.InnerText);
                    else
                        sign = node.InnerText;
                }

                string strParent = "";
                string strOrderNum = Request.QueryString["OrderNum"].ToString();
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

                WxPayModel wxPayModel = new WxPayModel(Convert.ToInt32(Model_tab_User.ShopClientID));
                //创建Model
                UnifiedWxPayModel model = UnifiedWxPayModel.CreateUnifiedModel(wxPayModel.AppId, wxPayModel.PartnerID, wxPayModel.PartnerKey);

                NotifyMessage myNotifyMessage = WeiXin.Lib.Core.Helper.HttpClientHelper.XmlDeserialize<NotifyMessage>(inputstr);

                if ((String.IsNullOrEmpty(myNotifyMessage.Out_Trade_No) == false) && (String.IsNullOrEmpty(myNotifyMessage.Transaction_Id) == false))
                {
                    #region 注销待付款未读消息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and Type='Info_cart_good' and Readed=0", userID, Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(userID.toString()));
                    #endregion 注销待付款未读消息

                  

                    Eggsoft.Common.debug_Log.Call_WriteLog(myNotifyMessage.Out_Trade_No, "支付通知");
                    Eggsoft_Public_CL.GoodP.IGetMoney(myNotifyMessage.Out_Trade_No, "Tenpay", myNotifyMessage.Transaction_Id);

                    ReturnMessage returnMsg = new ReturnMessage() { Return_Code = "SUCCESS", Return_Msg = "OK" };
                    Response.Write(returnMsg.ToXmlString());
                    Response.End();
                }

            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "支付通知", "线程异常");
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee, "支付通知");
            }
            finally { }






        }


        /// <summary>
        /// 获取Post Xml数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetXmlString(HttpRequestBase request)
        {
            using (System.IO.Stream stream = request.InputStream)
            {
                Byte[] postBytes = new Byte[stream.Length];
                stream.Read(postBytes, 0, (Int32)stream.Length);
                return System.Text.Encoding.UTF8.GetString(postBytes);
            }
        }
    }
}