using Eggsoft_Public_CL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient
{
    public partial class IwantRetunMoney : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private EggsoftWX.BLL.tab_ReturnMoney bll_tab_ReturnMoney = new EggsoftWX.BLL.tab_ReturnMoney();
        private EggsoftWX.Model.tab_ReturnMoney Model_tab_ReturnMoney = new EggsoftWX.Model.tab_ReturnMoney();

        private EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();
        private EggsoftWX.Model.tab_Order Model_tab_Order = new EggsoftWX.Model.tab_Order();
        private EggsoftWX.BLL.tab_Orderdetails bll_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();

        // private int pSTR_webuy8_ClientAdmin_Users_ID = 0;
        //private string STR_tab_ShopClient_ModelUpLoadPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                iniInfo();
            }
        }


        protected void iniInfo()
        {
            string strOrderID = Request.QueryString["OrderID"];

            if (String.IsNullOrEmpty(strOrderID) == false)
            {

                if (bll_tab_Order.Exists(Int32.Parse(strOrderID)))
                {
                    Model_tab_Order = bll_tab_Order.GetModel(Int32.Parse(strOrderID));

                    Label_OrderNum.Text = Model_tab_Order.OrderNum;
                    TextBox_Ask_Money.Text = Eggsoft_Public_CL.Pub.getPubMoney(Eggsoft_Public_CL.GoodP.GetThisOrderMoneyNotIncludeYunFei(Int32.Parse(strOrderID))); ;
                    TextBox_TextBox_Ask_Money_Hide.Text = TextBox_Ask_Money.Text;
                    Label_GoodInfo.Text = Model_tab_Order.OrderName;
                    Label_PayInfo.Text = Eggsoft_Public_CL.Pub.gePayChineseName(Model_tab_Order.PayWay) + " " + Model_tab_Order.PaywayOrderNum + " " + Model_tab_Order.TotalMoney + " " + Model_tab_Order.PayDateTime;
                    Label_UserInfo.Text = "微店号：" + Eggsoft_Public_CL.Pub.GetMyShopUserID(Model_tab_Order.UserID.ToString()) + " " + "昵称：" + Eggsoft_Public_CL.Pub.GetNickName(Model_tab_Order.UserID.ToString()) + Model_tab_Order.CreateDateTime;
                }


                if (bll_tab_ReturnMoney.Exists("OrderID=" + strOrderID))
                {
                    Model_tab_ReturnMoney = bll_tab_ReturnMoney.GetModel("OrderID=" + strOrderID);
                    string strLabel_OrderNum_Status = "";
                    if (Model_tab_ReturnMoney.FinanceCheck)
                    {
                        strLabel_OrderNum_Status += "已被财务退款处理！";

                    }
                    else if ((Model_tab_ReturnMoney.FinanceCheck == false) && (Model_tab_ReturnMoney.AdminCheck == true))
                    {
                        strLabel_OrderNum_Status += "已被管理处理，正在等待财务处理！";

                    }
                    else if (Model_tab_ReturnMoney.AdminCheck == false)
                    {
                        strLabel_OrderNum_Status += "正在等待管理处理,请勿重复提交！";
                    }
                    Label_OrderNum_Status.Text = strLabel_OrderNum_Status;
                    TextBox_ReturnMoney.Text = Model_tab_ReturnMoney.ReturnMoneyContent + "\r\n" + "提交时间：" + Model_tab_ReturnMoney.UpdateTime;

                }
            }

        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                string strID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                int pSTR_webuy8_ClientAdmin_Users_ID = Convert.ToInt32(strID);

                string strOrderID = Request.QueryString["OrderID"];

                Decimal Ask_Money = Decimal.Parse(TextBox_Ask_Money.Text);
                Decimal Ask_Money_Hide = Decimal.Parse(TextBox_TextBox_Ask_Money_Hide.Text);
                if (Ask_Money > Ask_Money_Hide)
                {
                    Eggsoft.Common.JsUtil.ShowMsg("退款金额错误！", -1);
                    return;
                }
                string strEmailTo = "";
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pSTR_webuy8_ClientAdmin_Users_ID);
                if (String.IsNullOrEmpty(my_Model_tab_ShopClient.XML) == false)
                {
                    XML__Class_Shop_Client myXML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(my_Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);
                    if (myXML__Class_Shop_Client.CheckEmail == true)
                    {
                        strEmailTo = myXML__Class_Shop_Client.Email;
                    }
                }

                if (bll_tab_ReturnMoney.Exists("OrderID=" + strOrderID) == false)
                {


                    Model_tab_ReturnMoney.OrderID = Int32.Parse(strOrderID);
                    Model_tab_ReturnMoney.RefundMoney = Decimal.Parse(TextBox_Ask_Money.Text);
                    Model_tab_ReturnMoney.ShopClientID = pSTR_webuy8_ClientAdmin_Users_ID;
                    Model_tab_ReturnMoney.ReturnMoneyContent = TextBox_ReturnMoney.Text;
                    Model_tab_ReturnMoney.ReturnMoneyTitle = "商户退款信息";
                    bll_tab_ReturnMoney.Add(Model_tab_ReturnMoney);

                    string strGetShopClientNameFromShopClientID = Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(pSTR_webuy8_ClientAdmin_Users_ID);
                    string strInfo = strGetShopClientNameFromShopClientID + " 订单号" + Eggsoft_Public_CL.GoodP.GetOrderNum_From_OrderID(Int32.Parse(strOrderID));
                    strInfo += TextBox_ReturnMoney.Text;

                    string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];
                    strInfo += "<a href=\"" + strClientAdminURL + "/ClientAdmin/13Order_Cancel/IS_FinanceAdmin_check_ReturnMoney.aspx\">单击这里进行处理</a>";


                    Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName + "商户退款信息", strEmailTo, "退款信息", strInfo);
                    Eggsoft.Common.JsUtil.ShowMsg("保存成功！\\n请通过第三方支付平台或者银行转账后到财务管理中确认取消订单！\\n 请不要重复提交信息！");
                }
                else
                {
                    Model_tab_ReturnMoney = bll_tab_ReturnMoney.GetModel("OrderID=" + strOrderID);

                    Model_tab_ReturnMoney.ReturnMoneyContent = TextBox_ReturnMoney.Text;
                    Model_tab_ReturnMoney.RefundMoney = Decimal.Parse(TextBox_Ask_Money.Text);
                    Model_tab_ReturnMoney.UpdateTime = DateTime.Now;
                    bll_tab_ReturnMoney.Update(Model_tab_ReturnMoney);

                    string strGetShopClientNameFromShopClientID = Eggsoft_Public_CL.Pub.GetShopClientNameFromShopClientID(pSTR_webuy8_ClientAdmin_Users_ID);
                    string strInfo = strGetShopClientNameFromShopClientID + " 订单号" + Eggsoft_Public_CL.GoodP.GetOrderNum_From_OrderID(Int32.Parse(strOrderID));
                    strInfo += TextBox_ReturnMoney.Text;
                    string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];
                    strInfo += "<a href=\"" + strClientAdminURL + "/ClientAdmin/13Order_Cancel/IS_FinanceAdmin_check_ReturnMoney.aspx\">单击这里进行处理</a>";

                    Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName + "退款信息", strEmailTo, "重复退款信息", strInfo);
                    Eggsoft.Common.JsUtil.ShowMsg("重复保存成功！\\n请通过第三方支付平台或者银行转账后到财务管理中确认取消订单！！\\n 请不要重复提交信息！");
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("退款错误：" + Exceptione.ToString());
            }
            finally
            {

            }
        }
    }
}