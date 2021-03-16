using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05ClientEggsoftCn.ClientAdmin._02GuWuQuanChange
{
    public partial class Manage_29RolePower : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private string strBoardAsx = "Board_29RolePower.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                if (type.ToLower() == "delete")
                {
                    string ID = Request.QueryString["ID"];
                    if (!CommUtil.IsNumStr(ID))
                        MyError.ThrowException("传递参数错误!");
                    EggsoftWX.BLL.tab_ShopClient_Role_Power bll = new EggsoftWX.BLL.tab_ShopClient_Role_Power();


                    string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                    EggsoftWX.BLL.tab_Admin_ShopClientPower BLLtab_ShopClient_AdminUser = new EggsoftWX.BLL.tab_Admin_ShopClientPower();
                    BLLtab_ShopClient_AdminUser.Delete("ShopClientID=" + strShopClientID + " and PowerRoleID=" + ID);
                    ///EggsoftWX.Model.tab_Admin_ShopClientPower Modeltab_Admin_ShopClientPower = BLLtab_ShopClient_AdminUser.GetModel("ShopClientID=" + strShopClientID + " and PowerRoleID=" + intRoldID + "" + " and ShopClientPowerItemName='" + strItemPower + "'");

                    bll.Delete(Int32.Parse(ID));

                    JsUtil.ShowMsg("删除成功!", strBoardAsx);
                }
                else if ((type.ToLower() == "add") || (type.ToLower() == "modify"))
                {
                    SetClass(sender, e);
                }
            }
        }

        private void SetClass(object sender, EventArgs e)
        {


            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string RoleID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_Role_Power bll = new EggsoftWX.BLL.tab_ShopClient_Role_Power();
                EggsoftWX.Model.tab_ShopClient_Role_Power Model = bll.GetModel(Int32.Parse(RoleID));

                txtRoleName.Text = Model.RoleName;
                btnAdd.Text = "保 存";
                #region getRole
                string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                CheckBox_submenu1_BasicSetting.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "BasicSetting", false, Model.ID);
                CheckBox_submenu1_BasicSetting_BoardINC_Manage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "BasicSetting_BoardINC_Manage", false, Model.ID);
                CheckBox_submenu1_BasicSetting_ShopPar.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "BasicSetting_ShopPar", false, Model.ID);
                CheckBox_submenu1_BasicSetting_Style_Model.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "BasicSetting_Style_Model", false, Model.ID);
                CheckBox_submenu1_BasicSetting_MakeHtml.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "BasicSetting_MakeHtml", false, Model.ID);

                CheckBox_submenu2_ApplicationManage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_AgentChecked.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_AgentChecked", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_Good.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_Good", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_WeiTuanGou.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_WeiTuanGou", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_GuidePages.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_GuidePages", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_Vouchers.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_Vouchers", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_FreightTemplate.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_FreightTemplate", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_Class_BoardSet.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_Class_BoardSet", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_AnnouncePic.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_AnnouncePic", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_O2O_Shop.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_O2O_Shop", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_NetRootMenu.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_NetRootMenu", false, Model.ID);
                CheckBox_submenu2_ApplicationManage_URLShow.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ApplicationManage_URLShow", false, Model.ID);

                CheckBox_submenu3_OrderManage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "OrderManage", false, Model.ID);
                CheckBox_submenu3_OrderManage_NeedMoney.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "OrderManage_NeedMoney", false, Model.ID);
                CheckBox_submenu3_OrderManage_WaitGiveGoods.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "OrderManage_WaitGiveGoods", false, Model.ID);
                CheckBox_submenu3_OrderManage_UserGetGoods.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "OrderManage_UserGetGoods", false, Model.ID);
                CheckBox_submenu3_OrderManage_Wait_Finished.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "OrderManage_Wait_Finished", false, Model.ID);
                CheckBox_submenu3_OrderManage_Board.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "OrderManage_Board", false, Model.ID);///结算管理

                CheckBox_submenu411_ExtendManage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage", false, Model.ID);
                CheckBox_submenu411_ExtendManage_GuWuQuanChange.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_GuWuQuanChange", false, Model.ID);
                CheckBox_submenu411_ExtendManage_ZhuanZhuanChe.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_ZhuanZhuanChe", false, Model.ID);
                CheckBox_submenu411_ExtendManage_OnlineBaoMing.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_OnlineBaoMing", false, Model.ID);
                CheckBox_submenu411_ExtendManage_LightApp.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_LightApp", false, Model.ID);
                CheckBox_submenu411_ExtendManage_GameSendJiFenBoard.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_GameSendJiFenBoard", false, Model.ID);
                CheckBox_submenu411_ExtendManage_25WeiXianChang_BoardSet.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_25WeiXianChang_BoardSet", false, Model.ID);
                CheckBox_submenu411_ExtendManage_Board_WeiKanJian.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_Board_WeiKanJian", false, Model.ID);
                CheckBox_submenu411_ExtendManage_Board_01ZC_Project.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_Board_01ZC_Project", false, Model.ID);
                CheckBox_submenu411_ExtendManage_16SendMoney.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_16SendMoney", false, Model.ID);
                CheckBox_submenu411_ExtendManage_UserMoneyControl.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_UserMoneyControl", false, Model.ID);
                CheckBox_submenu411_ExtendManage_Board28Member.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_Board28Member", false, Model.ID);
                CheckBox_ExtendManage_32NoticeGuidePages.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_32NoticeGuidePages", false, Model.ID);///公告管理
                CheckBox_submenu411_ExtendManage_Board28Member_MemberBonus.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_Board28Member_MemberBonus", false, Model.ID);
                CheckBox_submenu411_ExtendManage_Board_29AdminPower.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ExtendManage_Board_29AdminPower", false, Model.ID);

                CheckBox_o2oShop.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "o2oShopManage", false, Model.ID);///陶醉积分
                CheckBox_o2oShop_cardMember.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "o2oShop_cardMember", false, Model.ID);///陶醉积分


                CheckBox_ConsumptionCapitalManage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_02OperationCenter.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_02OperationCenter", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_04OperationGoods.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_04OperationGoods", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_08FullEveryDay.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_08FullEveryDay", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_10BalanceofPaymentStatistics.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_10BalanceofPaymentStatistics", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_09OperationUserStatus.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_09OperationUserStatus", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_12OrderDetailEveryDay.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_12OrderDetailEveryDay", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_14WealthMoneyControlOperationCenter.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_14WealthMoneyControlOperationCenter", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_18OperationWtiteOrder.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_18OperationWtiteOrder", false, Model.ID);///运营中心线下收单管理
                CheckBox_16CheckModifyParent.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_16CheckModifyParent", false, Model.ID);///消费财富系统 线下收单管理
                CheckBox_13OperationCenter_OrderManage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ConsumptionCapitalManage_13OperationCenter_OrderManage", false, Model.ID);///消费财富系统 线下收单管理

                 CheckBox_submenu44_Devolopment.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "Devolopment", false, Model.ID);
                CheckBox_submenu44_Devolopment_BasicInfo.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "Devolopment_BasicInfo", false, Model.ID);
                CheckBox_submenu44_Devolopment_BoardJPG.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "Devolopment_BoardJPG", false, Model.ID);
                CheckBox_submenu44_Devolopment_Resource.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "Devolopment_Resource", false, Model.ID);
                 CheckBox_submenu44_Devolopment_Subscribe.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "Devolopment_Subscribe", false, Model.ID);
                CheckBox_submenu44_Devolopment_KeyAnswer.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "Devolopment_KeyAnswer", false, Model.ID);
                CheckBox_submenu44_Devolopment_KeyAnswer_Default.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "Devolopment_KeyAnswer_Default", false, Model.ID);
                CheckBox_submenu44_Devolopment_WeiXinMenu.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "Devolopment_WeiXinMenu", false, Model.ID);
               
                CheckBox_submenu4_DataStutus.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "DataStutus", false, Model.ID);
                CheckBox_submenu4_DataStutus_GuidePagesVisit.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "DataStutus_GuidePagesVisit", false, Model.ID);
                CheckBox_submenu4_DataStutus_GoodsVisit.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "DataStutus_GoodsVisit", false, Model.ID);
                CheckBox_submenu4_DataStutus_UserStatus.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "DataStutus_UserStatus", false, Model.ID);
                CheckBox_submenu4_DataStutus_AgentStatus.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "DataStutus_AgentStatus", false, Model.ID);
                CheckBox_submenu4_DataStutus_UserSignWorkingEveryDay.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "DataStutus_UserSignWorkingEveryDay", false, Model.ID);
                CheckBox_submenu4_DataStutus_UserOrderDetailEveryDay.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "DataStutus_UserOrderDetailEveryDay", false, Model.ID);//订单统计
                CheckBox_submenu434_MoneyManage.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "MoneyManage", false, Model.ID);
                CheckBox_submenu434_MoneyManage_IS_UserFinance_check_DrawMoney.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "MoneyManage_IS_UserFinance_check_DrawMoney", false, Model.ID);
                CheckBox_submenu434_MoneyManage_IS_FinanceAdmin_check_ReturnMoney.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "MoneyManage_IS_FinanceAdmin_check_ReturnMoney", false, Model.ID);

                CheckBox_submenu65_ShopAdvance.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ShopAdvance", false, Model.ID);
                CheckBox_submenu65_ShopAdvance_FenXiaoLevel.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ShopAdvance_FenXiaoLevel", false, Model.ID);
                CheckBox_submenu65_ShopAdvance_Agent_Level.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ShopAdvance_Agent_Level", false, Model.ID);
                CheckBox_submenu65_ShopAdvance_BoardGood_XML.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "ShopAdvance_BoardGood_XML", false, Model.ID);


                CheckBoxWuLIuAdvance.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "WuLIuAdvance", false, Model.ID);
                CheckBoxWuLIuAdvance_ChannelChange.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "WuLIuAdvance_ChannelChange", false, Model.ID);
                CheckBoxWuLIuAdvance_ZoneChange.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "WuLIuAdvance_ZoneChange", false, Model.ID);
                CheckBoxWuLIuAdvance_PriceChange.Checked = Eggsoft_Public_CL.Pub.boolShowPower(strShopClientID, "WuLIuAdvance_PriceChange", false, Model.ID);

                #endregion
                //RequiredFieldValidator3.Enabled = false;
            }
            else if (type.ToLower() == "add")
            {
                // RadioButtonList_ChangeWay_SelectedIndexChanged(sender, e);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

            string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            string type = Request.QueryString["type"];
            if (type.ToLower() == "modify")
            {
                string ID = Request.QueryString["ID"];// 修改ID
                EggsoftWX.BLL.tab_ShopClient_Role_Power bll = new EggsoftWX.BLL.tab_ShopClient_Role_Power();
                EggsoftWX.Model.tab_ShopClient_Role_Power Model = bll.GetModel(Int32.Parse(ID));

                Model.RoleName = txtRoleName.Text;
                Model.Updateby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                Model.Updatetime = DateTime.Now;
                //Model.InputMoney = Decimal.Parse(txtInputMoney.Text);
                //Model.BonusMoney = Decimal.Parse(TextBoxBonusMoney.Text);
                //Model.BonusGouWuQuan = Decimal.Parse(TextBoxBonusGouWuQuan.Text);
                SaveModel(Model);
                Model.Updatetime = DateTime.Now;
                bll.Update(Model);
                JsUtil.ShowMsg("修改成功!", strBoardAsx);

            }
            else if (type.ToLower() == "add")
            {
                EggsoftWX.BLL.tab_ShopClient_Role_Power bll = new EggsoftWX.BLL.tab_ShopClient_Role_Power();
                EggsoftWX.Model.tab_ShopClient_Role_Power Model = new EggsoftWX.Model.tab_ShopClient_Role_Power();
                Model.ShopClientID = Int32.Parse(strINCID);
                Model.RoleName = txtRoleName.Text;
                Model.Creatby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                Int32 Int32AddID = bll.Add(Model);
                Model.ID = Int32AddID;
                SaveModel(Model);
                bll.Update(Model);
                JsUtil.ShowMsg("添加成功!", strBoardAsx);
            }
        }


        protected void SaveModel(EggsoftWX.Model.tab_ShopClient_Role_Power Model)
        {
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();

            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu1_BasicSetting.Checked, "BasicSetting", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu1_BasicSetting_BoardINC_Manage.Checked, "BasicSetting_BoardINC_Manage", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu1_BasicSetting_ShopPar.Checked, "BasicSetting_ShopPar", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu1_BasicSetting_Style_Model.Checked, "BasicSetting_Style_Model", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu1_BasicSetting_MakeHtml.Checked, "BasicSetting_MakeHtml", Model.ID);

            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage.Checked, "ApplicationManage", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_AgentChecked.Checked, "ApplicationManage_AgentChecked", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_Good.Checked, "ApplicationManage_Good", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_WeiTuanGou.Checked, "ApplicationManage_WeiTuanGou", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_GuidePages.Checked, "ApplicationManage_GuidePages", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_Vouchers.Checked, "ApplicationManage_Vouchers", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_FreightTemplate.Checked, "ApplicationManage_FreightTemplate", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_Class_BoardSet.Checked, "ApplicationManage_Class_BoardSet", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_AnnouncePic.Checked, "ApplicationManage_AnnouncePic", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_O2O_Shop.Checked, "ApplicationManage_O2O_Shop", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_NetRootMenu.Checked, "ApplicationManage_NetRootMenu", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu2_ApplicationManage_URLShow.Checked, "ApplicationManage_URLShow", Model.ID);


            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu3_OrderManage.Checked, "OrderManage", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu3_OrderManage_NeedMoney.Checked, "OrderManage_NeedMoney", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu3_OrderManage_WaitGiveGoods.Checked, "OrderManage_WaitGiveGoods", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu3_OrderManage_UserGetGoods.Checked, "OrderManage_UserGetGoods", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu3_OrderManage_Wait_Finished.Checked, "OrderManage_Wait_Finished", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu3_OrderManage_Board.Checked, "OrderManage_Board", Model.ID);

            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage.Checked, "ExtendManage", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_GuWuQuanChange.Checked, "ExtendManage_GuWuQuanChange", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_ZhuanZhuanChe.Checked, "ExtendManage_ZhuanZhuanChe", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_OnlineBaoMing.Checked, "ExtendManage_OnlineBaoMing", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_LightApp.Checked, "ExtendManage_LightApp", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_GameSendJiFenBoard.Checked, "ExtendManage_GameSendJiFenBoard", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_25WeiXianChang_BoardSet.Checked, "ExtendManage_25WeiXianChang_BoardSet", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_Board_WeiKanJian.Checked, "ExtendManage_Board_WeiKanJian", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_Board_01ZC_Project.Checked, "ExtendManage_Board_01ZC_Project", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_16SendMoney.Checked, "ExtendManage_16SendMoney", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_UserMoneyControl.Checked, "ExtendManage_UserMoneyControl", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_Board28Member.Checked, "ExtendManage_Board28Member", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_ExtendManage_32NoticeGuidePages.Checked, "ExtendManage_32NoticeGuidePages", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_Board28Member_MemberBonus.Checked, "ExtendManage_Board28Member_MemberBonus", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu411_ExtendManage_Board_29AdminPower.Checked, "ExtendManage_Board_29AdminPower", Model.ID);


            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_o2oShop.Checked, "o2oShopManage", Model.ID);///陶醉积分
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_o2oShop_cardMember.Checked, "o2oShop_cardMember", Model.ID);///陶醉积分


            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_ConsumptionCapitalManage.Checked, "ConsumptionCapitalManage", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_02OperationCenter.Checked, "ConsumptionCapitalManage_02OperationCenter", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_04OperationGoods.Checked, "ConsumptionCapitalManage_04OperationGoods", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_08FullEveryDay.Checked, "ConsumptionCapitalManage_08FullEveryDay", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_10BalanceofPaymentStatistics.Checked, "ConsumptionCapitalManage_10BalanceofPaymentStatistics", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_09OperationUserStatus.Checked, "ConsumptionCapitalManage_09OperationUserStatus", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_12OrderDetailEveryDay.Checked, "ConsumptionCapitalManage_12OrderDetailEveryDay", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_14WealthMoneyControlOperationCenter.Checked, "ConsumptionCapitalManage_14WealthMoneyControlOperationCenter", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_18OperationWtiteOrder.Checked, "ConsumptionCapitalManage_18OperationWtiteOrder", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_16CheckModifyParent.Checked, "ConsumptionCapitalManage_16CheckModifyParent", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_13OperationCenter_OrderManage.Checked, "ConsumptionCapitalManage_13OperationCenter_OrderManage", Model.ID);
        
         



       

            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu44_Devolopment.Checked, "Devolopment", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu44_Devolopment_BasicInfo.Checked, "Devolopment_BasicInfo", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu44_Devolopment_BoardJPG.Checked, "Devolopment_BoardJPG", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu44_Devolopment_Resource.Checked, "Devolopment_Resource", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu44_Devolopment_Subscribe.Checked, "Devolopment_Subscribe", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu44_Devolopment_KeyAnswer.Checked, "Devolopment_KeyAnswer", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu44_Devolopment_KeyAnswer_Default.Checked, "Devolopment_KeyAnswer_Default", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu44_Devolopment_WeiXinMenu.Checked, "Devolopment_WeiXinMenu", Model.ID);
           

            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu4_DataStutus.Checked, "DataStutus", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu4_DataStutus_GoodsVisit.Checked, "DataStutus_GuidePagesVisit", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu4_DataStutus_GoodsVisit.Checked, "DataStutus_GoodsVisit", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu4_DataStutus_UserStatus.Checked, "DataStutus_UserStatus", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu4_DataStutus_AgentStatus.Checked, "DataStutus_AgentStatus", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu4_DataStutus_UserSignWorkingEveryDay.Checked, "DataStutus_UserSignWorkingEveryDay", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu4_DataStutus_UserOrderDetailEveryDay.Checked, "DataStutus_UserOrderDetailEveryDay", Model.ID);//订单统计

          

            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu434_MoneyManage.Checked, "MoneyManage", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu434_MoneyManage_IS_UserFinance_check_DrawMoney.Checked, "MoneyManage_IS_UserFinance_check_DrawMoney", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBox_submenu434_MoneyManage_IS_FinanceAdmin_check_ReturnMoney.Checked, "MoneyManage_IS_FinanceAdmin_check_ReturnMoney", Model.ID);

           

            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBoxWuLIuAdvance.Checked, "WuLIuAdvance", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBoxWuLIuAdvance_ChannelChange.Checked, "WuLIuAdvance_ChannelChange", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBoxWuLIuAdvance_ZoneChange.Checked, "WuLIuAdvance_ZoneChange", Model.ID);
            Eggsoft_Public_CL.Pub.BoolSaveRoleShowPower(strShopClientID, CheckBoxWuLIuAdvance_PriceChange.Checked, "WuLIuAdvance_PriceChange", Model.ID);
        }
    }
}