using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._21GuWuQuanAndMoneyControl
{
    public partial class UserMoneyControl : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();
        private EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();


        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ExtendManage_UserMoneyControl")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有的权限

            if (!IsPostBack)
            {




            }
        }

        protected void btn_Button_DoAdd_Click(object sender, EventArgs e)
        {
            string strShopUserID = Label_Number.Text;
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string strRowNickID = Label_Number.Text;

            Decimal myCountMoney = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(strRowNickID), out myCountMoney);
            Decimal myCountMoney_Vouchers = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(strRowNickID), out myCountMoney_Vouchers);

            string strAddOrMinus = RadioButtonList_AddOrMinus.SelectedValue;
            string strDouWuQuanOrMoney = RadioButtonList_DouWuQuanOrMoney.SelectedValue;

            String strTextBox_AddMoney = TextBox_AddMoney.Text;
            Decimal deMoney = 0;
            Decimal.TryParse(strTextBox_AddMoney, out deMoney);

            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");


            if (Decimal.Round(deMoney, 2) > 0)
            {
                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();

                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();

                if ((strAddOrMinus == "0") && (strDouWuQuanOrMoney == "0"))
                {
                    if (myCountMoney_Vouchers >= deMoney)
                    {
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = deMoney;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "平台操作" + (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" ? "Oliver" : strwebuy8_ClientAdmin_Users_ClientUserAccount);
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Int32.Parse(strRowNickID);
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                        int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strRowNickID), 0, Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "VouchersShopName") + "平台操作减少" + Eggsoft_Public_CL.Pub.getPubMoney(deMoney) + "元");

                        #region 增加未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UserID = Int32.Parse(strRowNickID);
                        Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                        Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加未处理信息

                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("余额不足", -1);
                    }

                }
                else if ((strAddOrMinus == "0") && (strDouWuQuanOrMoney == "1"))
                {
                    if (myCountMoney >= deMoney)
                    {
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 140;
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = deMoney;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "平台操作" + (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" ? "Oliver" : strwebuy8_ClientAdmin_Users_ClientUserAccount);
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Int32.Parse(strRowNickID);
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                        int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strRowNickID), 0, "现金平台操作减少" + Eggsoft_Public_CL.Pub.getPubMoney(deMoney) + "元");

                        #region 增加未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UserID = Int32.Parse(strRowNickID);
                        Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                        Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加未处理信息
                    }
                    else
                    {
                        Eggsoft.Common.JsUtil.ShowMsg("余额不足", -1);
                    }
                }
                else if ((strAddOrMinus == "1") && (strDouWuQuanOrMoney == "0"))
                {

                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = deMoney;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "平台操作" + (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" ? "Oliver" : strwebuy8_ClientAdmin_Users_ClientUserAccount);
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Int32.Parse(strRowNickID);
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                    int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strRowNickID), 0, Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "VouchersShopName") + "平台操作增加" + Eggsoft_Public_CL.Pub.getPubMoney(deMoney) + "元");


                    #region 增加未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UserID = Int32.Parse(strRowNickID);
                    Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                    Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加未处理信息
                }
                else if ((strAddOrMinus == "1") && (strDouWuQuanOrMoney == "1"))
                {

                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 30;
                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = deMoney;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "平台操作" + (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" ? "Oliver" : strwebuy8_ClientAdmin_Users_ClientUserAccount);
                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Int32.Parse(strRowNickID);
                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                    int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strRowNickID), 0, "现金平台操作增加" + Eggsoft_Public_CL.Pub.getPubMoney(deMoney) + "元");


                    #region 增加未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UserID = Int32.Parse(strRowNickID);
                    Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                    Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加未处理信息
                }
                btnChaXun_Click(sender, e);///刷新本页
                //Eggsoft.Common.JsUtil.ShowMsg("执行完毕");
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('执行成功')", true);
            }

        }
        protected void btnChaXun_Click(object sender, EventArgs e)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();


            string strNickName = TextBox_NickName.Text.Trim();
            string strShopUserID = TextBox_ShopUserID.Text.Trim();

            string strSQL = "";
            if (strNickName.Length > 0)
            {
                strSQL += "nickname like '%" + strNickName + "%' ";
            }
            if (strShopClientID == "26") {
                if (strShopUserID.Length > 10) {
                    ///026000000208
                    int intLength = strShopUserID.Length;
                    strShopUserID = strShopUserID.Substring(3, intLength-4);
                }
            }


            int intUserID = 0;
            int.TryParse(strShopUserID, out intUserID);
            if (intUserID > 0)
            {
                strSQL = "shopuserid=" + strShopUserID;
            }
            if (strSQL.Length > 0)
            {
                strSQL += "  and ShopClientID=" + strShopClientID;

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                System.Data.DataTable myDataTable = BLL_tab_User.GetList(strSQL).Tables[0];

                if (myDataTable.Rows.Count > 0)
                {
                    string strRowNickName = myDataTable.Rows[0]["NickName"].ToString();
                    string strRowNickID = myDataTable.Rows[0]["ID"].ToString();
                    string strRowShopUserID = myDataTable.Rows[0]["ShopUserID"].ToString();

                    Label_Number.Text = strRowNickID;
                    Label_UserInfo.Text = "用户昵称：<span style=\"color:red\">" + strRowNickName + "</span><br />  用户编号:" + strRowShopUserID;


                    Decimal myCountMoney_Vouchers = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(strRowNickID), out myCountMoney_Vouchers);
                    string myCount_Voucherslink = "../09System_Status/UserStatus_Quan.aspx?userid=" + strRowNickID;
                    HyperLink_UserInfo_Money.Text = "购物券余额" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers);
                    HyperLink_UserInfo_Money.NavigateUrl = myCount_Voucherslink;
                    HyperLink_UserInfo_Money.Target = "_blank";

                    Decimal myCountMoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Int32.Parse(strRowNickID), out myCountMoney);
                    string myCountMoneylink = "../09System_Status/UserStatus_Money.aspx?userid=" + strRowNickID;
                    HyperLink_UserInfo_GouWuQuan.Text = "现金余额" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney);
                    HyperLink_UserInfo_GouWuQuan.NavigateUrl = myCountMoneylink;
                    HyperLink_UserInfo_GouWuQuan.Target = "_blank";

                    Panel_UserInfo.Visible = true;
                }
            }

        }
    }
}