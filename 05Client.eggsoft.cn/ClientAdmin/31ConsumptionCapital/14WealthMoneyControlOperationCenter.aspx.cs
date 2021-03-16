using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _05Client.eggsoft.cn.ClientAdmin._31ConsumptionCapital
{
    public partial class _14WealthMoneyControlOperationCenter : Eggsoft.Common.DotAdminPage_ClientAdmin
    {
        private EggsoftWX.BLL.tab_ShopClient tab_ShopClient_bll = new EggsoftWX.BLL.tab_ShopClient();
        private EggsoftWX.Model.tab_ShopClient tab_ShopClient_Model = new EggsoftWX.Model.tab_ShopClient();


        protected void Page_Load(object sender, EventArgs e)
        {
            #region 没有打开运营中心的权限
            if (!String.IsNullOrEmpty(Eggsoft_Public_CL.PubMember.DisPlayPower("ConsumptionCapitalManage_14WealthMoneyControlOperationCenter")))
            {
                Response.Write("<script>window.close()</script>");
                return;
            }
            #endregion 没有打开运营中心的权限
            if (!IsPostBack)
            {




            }
        }

        protected void btn_Button_DoAdd_Click(object sender, EventArgs e)
        {
            string strShopUserID = Label_Number.Text;
            string strShopClientID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
            string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");

            string strRowNickID = Label_Number.Text;

            Decimal myTotalCredits_OperationCenter = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuETotalCredits_OperationCenter(Int32.Parse(strRowNickID), out myTotalCredits_OperationCenter);
            Decimal myCountArgWealth = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Int32.Parse(strRowNickID), out myCountArgWealth);

            string strAddOrMinus = RadioButtonList_AddOrMinus.SelectedValue;
            string strDouWuQuanOrMoney = RadioButtonList_DouWuQuanOrMoney.SelectedValue;

            String strTextBox_AddMoney = TextBox_AddMoney.Text;
            Decimal deMoney = 0;
            Decimal.TryParse(strTextBox_AddMoney, out deMoney);

            // string strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");


            if (Decimal.Round(deMoney, 2) > 0)
            {
                EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();

                EggsoftWX.BLL.b003_TotalCredits_OperationCenter BLL_b003_TotalCredits_OperationCenter = new EggsoftWX.BLL.b003_TotalCredits_OperationCenter();
                EggsoftWX.Model.b003_TotalCredits_OperationCenter Model_b003_TotalCredits_OperationCenter = new EggsoftWX.Model.b003_TotalCredits_OperationCenter();

                if ((strAddOrMinus == "0") && (strDouWuQuanOrMoney == "0"))
                {
                    #region 扣除财富积分
                    System.Collections.ArrayList myArrayListb008_OpterationUserActiveReturnMoneyOrderNum = new System.Collections.ArrayList();

                    EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum my_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                    EggsoftWX.BLL.b015_OrderDetail_WealthBuy BLL_b015_OrderDetail_WealthBuy = new EggsoftWX.BLL.b015_OrderDetail_WealthBuy();

                    EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                    //string strSQL = "select sum(ReturnMoneyUnit) from b008_OpterationUserActiveReturnMoneyOrderNum where UserID=@UserID and ShopClient_ID=@ShopClient_ID and OrderID is null and ActiveOrderNum>0";
                    //System.Data.DataTable DataDataTable = my_BLL_b006_TotalWealth_OperationUser.SelectList(strSQL, strRowNickID.toInt32(), strShopClientID.toInt32()).Tables[0];
                    //Decimal DecimalReturnMoneyUnit = DataDataTable.Rows[0][0].toDecimal();

                    Decimal DecimalReturnMoneyUnit = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Int32.Parse(strRowNickID), out DecimalReturnMoneyUnit);
                    if (DecimalReturnMoneyUnit >= deMoney)
                    {
                        #region  按照支付时间排序  减去相应 财富表中的积分  这是一个算法
                        string strSQLTable = "select ID,ReturnMoneyUnit,OrderDetailID,OrderID,ActiveOrderNum from b008_OpterationUserActiveReturnMoneyOrderNum where UserID=@UserID and ShopClient_ID=@ShopClient_ID and OrderID is not null order by PayDateTime desc";
                        System.Data.DataTable Data_DataTable = my_BLL_b006_TotalWealth_OperationUser.SelectList(strSQLTable, strRowNickID.toInt32(), strShopClientID.toInt32()).Tables[0];


                        for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                        {

                            Eggsoft_Public_CL.Wealth_OperationUserList CurWealth_OperationUserList = new Eggsoft_Public_CL.Wealth_OperationUserList();
                            CurWealth_OperationUserList.CurMoney = Data_DataTable.Rows[i]["ReturnMoneyUnit"].toDecimal();
                            CurWealth_OperationUserList.b008_OpterationUserActiveReturnMoneyOrderNumID = Data_DataTable.Rows[i]["ID"].toInt32();
                            CurWealth_OperationUserList.OrderID = Data_DataTable.Rows[i]["OrderID"].toInt32();
                            CurWealth_OperationUserList.OrderDetailID = Data_DataTable.Rows[i]["OrderDetailID"].toInt32();
                            myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Add(CurWealth_OperationUserList);
                        }

                        Decimal DecimalNeedMoney = deMoney;
                        for (int i = 0; i < myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Count; i++)
                        {
                            Eggsoft_Public_CL.Wealth_OperationUserList curWealth_OperationUser = (Eggsoft_Public_CL.Wealth_OperationUserList)myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i];
                            if (DecimalNeedMoney <= curWealth_OperationUser.CurMoney)
                            {
                                curWealth_OperationUser.UsedMoney = DecimalNeedMoney;///curWealth_OperationUser.CurMoney - 
                                myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i] = curWealth_OperationUser;
                                break;
                            }
                            else
                            {
                                curWealth_OperationUser.UsedMoney = curWealth_OperationUser.CurMoney;
                                myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i] = curWealth_OperationUser;
                                DecimalNeedMoney = DecimalNeedMoney - (Decimal)curWealth_OperationUser.CurMoney;
                            }

                        }
                        #endregion  按照支付时间排序  减去相应 财富表中的积分


                        #region  按照支付时间排序  减去相应 财富表中的积分  这是动作
                        for (int i = 0; i < myArrayListb008_OpterationUserActiveReturnMoneyOrderNum.Count; i++)
                        {
                            Eggsoft_Public_CL.Wealth_OperationUserList curWealth_OperationUser = (Eggsoft_Public_CL.Wealth_OperationUserList)myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i];
                            if (curWealth_OperationUser.UsedMoney > 0)
                            {
                                EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUserThis = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                                Model_b006_TotalWealth_OperationUserThis.Bool_ConsumeOrRecharge = false;
                                Model_b006_TotalWealth_OperationUserThis.OrderDetailID = curWealth_OperationUser.OrderDetailID;
                                Model_b006_TotalWealth_OperationUserThis.UserID = strRowNickID.toInt32();
                                Model_b006_TotalWealth_OperationUserThis.ShopClient_ID = strShopClientID.toInt32();
                                Model_b006_TotalWealth_OperationUserThis.ConsumeOrRechargeWealth = curWealth_OperationUser.UsedMoney;
                                Model_b006_TotalWealth_OperationUserThis.ConsumeTypeOrRecharge = "财富积分 后台操作";
                                int intAddID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUserThis);
                                if (!(intAddID > 0))
                                {
                                    //intWealth_List = 2;
                                    break;
                                }
                                //return -1;

                                EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = my_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel(curWealth_OperationUser.b008_OpterationUserActiveReturnMoneyOrderNumID);
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit - curWealth_OperationUser.UsedMoney;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                if (Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit <= 0)
                                {
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = 0;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + "出局";
                                }
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                my_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                                EggsoftWX.Model.b015_OrderDetail_WealthBuy Model_b015_OrderDetail_WealthBuy = new EggsoftWX.Model.b015_OrderDetail_WealthBuy();
                                Model_b015_OrderDetail_WealthBuy.UserID = strRowNickID.toInt32();
                                Model_b015_OrderDetail_WealthBuy.OrdetailID = curWealth_OperationUser.OrderDetailID;
                                Model_b015_OrderDetail_WealthBuy.ShopClientID = strShopClientID.toInt32();
                                Model_b015_OrderDetail_WealthBuy.OrderID = curWealth_OperationUser.OrderID;
                                Model_b015_OrderDetail_WealthBuy.ShopClientID = strShopClientID.toInt32();
                                Model_b015_OrderDetail_WealthBuy.UseOrNotuse = true;
                                Model_b015_OrderDetail_WealthBuy.HowMuchWealth = curWealth_OperationUser.UsedMoney;
                                Model_b015_OrderDetail_WealthBuy.CreateBy = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                curWealth_OperationUser.b015_OrderDetail_WealthBuyID = BLL_b015_OrderDetail_WealthBuy.Add(Model_b015_OrderDetail_WealthBuy);
                                myArrayListb008_OpterationUserActiveReturnMoneyOrderNum[i] = curWealth_OperationUser;
                            }
                        }



                        #endregion   按照支付时间排序  减去相应 财富表中的积分  这是动作
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('不支持太多的财富积分')", true);
                        return;
                    }
                    #endregion 扣除财富积分

                }
                else if ((strAddOrMinus == "0") && (strDouWuQuanOrMoney == "1"))
                {
                    if (myTotalCredits_OperationCenter >= deMoney)
                    {
                        Model_b003_TotalCredits_OperationCenter.Creatby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b003_TotalCredits_OperationCenter.Bool_ConsumeOrRecharge = false;
                        Model_b003_TotalCredits_OperationCenter.ConsumeOrRechargeMoney = deMoney;
                        Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge = "平台操作" + (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" ? "Oliver" : strwebuy8_ClientAdmin_Users_ClientUserAccount);
                        Model_b003_TotalCredits_OperationCenter.UserID = Int32.Parse(strRowNickID);
                        Model_b003_TotalCredits_OperationCenter.ShopClient_ID = strShopClientID.toInt32();
                        int intTableID = BLL_b003_TotalCredits_OperationCenter.Add(Model_b003_TotalCredits_OperationCenter);
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strRowNickID), 0, "运营现金平台操作减少" + Eggsoft_Public_CL.Pub.getPubMoney(deMoney) + "元");

                        #region 增加未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge;

                        Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                        Model_b011_InfoAlertMessage.UserID = Int32.Parse(strRowNickID);
                        Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                        Model_b011_InfoAlertMessage.Type = "Info_myYunYingMoney";
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
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert('不支持财富积分的增加')", true);
                    return;
                    // Eggsoft.Common.JsUtil.ShowMsg("不支持财富积分的增加", -1);
                    //EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                    //System.Data.DataTable Data_DataTable = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList("select sum(ActiveOrderNum),count(1) from b008_OpterationUserActiveReturnMoneyOrderNum where ShopClient_ID=@ShopClient_ID and UserID=@UserID", strShopClientID.toInt32(), strRowNickID.toInt32()).Tables[0];
                    //if (Data_DataTable.Rows[0][1].toInt32() == 1)
                    //{
                    //    Model_b006_TotalWealth_OperationUser.Creatby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    //    Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = true;
                    //    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = deMoney;
                    //    Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "平台操作" + (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" ? "Oliver" : strwebuy8_ClientAdmin_Users_ClientUserAccount);
                    //    Model_b006_TotalWealth_OperationUser.UserID = Int32.Parse(strRowNickID);
                    //    Model_b006_TotalWealth_OperationUser.ShopClient_ID = strShopClientID.toInt32();
                    //    int intTableID = BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
                    //    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strRowNickID), 0, "财富积分平台操作增加" + Eggsoft_Public_CL.Pub.getPubMoney(deMoney) + "元");

                    //    #region 增加未处理信息
                    //    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    //    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    //    Model_b011_InfoAlertMessage.InfoTip = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;

                    //    Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    //    Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    //    Model_b011_InfoAlertMessage.UserID = Int32.Parse(strRowNickID);
                    //    Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                    //    Model_b011_InfoAlertMessage.Type = "Info_TotalWealth";
                    //    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                    //    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    //    #endregion 增加未处理信息

                    //}
                }
                else if ((strAddOrMinus == "1") && (strDouWuQuanOrMoney == "1"))
                {

                    Model_b003_TotalCredits_OperationCenter.Creatby = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b003_TotalCredits_OperationCenter.Bool_ConsumeOrRecharge = true;
                    Model_b003_TotalCredits_OperationCenter.ConsumeOrRechargeMoney = deMoney;
                    Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge = "平台操作" + (strwebuy8_ClientAdmin_Users_ClientUserAccount == "gotouserFrom_Adminoliver" ? "Oliver" : strwebuy8_ClientAdmin_Users_ClientUserAccount);
                    Model_b003_TotalCredits_OperationCenter.UserID = Int32.Parse(strRowNickID);
                    Model_b003_TotalCredits_OperationCenter.ShopClient_ID = strShopClientID.toInt32();
                    int intTableID = BLL_b003_TotalCredits_OperationCenter.Add(Model_b003_TotalCredits_OperationCenter);
                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strRowNickID), 0, "运营现金平台操作增加" + Eggsoft_Public_CL.Pub.getPubMoney(deMoney) + "元");

                    #region 增加未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge;

                    Model_b011_InfoAlertMessage.CreateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UpdateBy = strwebuy8_ClientAdmin_Users_ClientUserAccount;
                    Model_b011_InfoAlertMessage.UserID = Int32.Parse(strRowNickID);
                    Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                    Model_b011_InfoAlertMessage.Type = "Info_myYunYingMoney";
                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加未处理信息
                }

                #region 操作活动订单数的 财富积分余额
                //if (strDouWuQuanOrMoney == "0")
                //{
                //    Decimal myCountMoney_Vouchers = 0;
                //    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Int32.Parse(strRowNickID), out myCountMoney_Vouchers);

                //    EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                //    System.Data.DataTable Data_DataTable = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList("select sum(ActiveOrderNum),count(1) from b008_OpterationUserActiveReturnMoneyOrderNum where ShopClient_ID=@ShopClient_ID and UserID=@UserID", strShopClientID.toInt32(), strRowNickID.toInt32()).Tables[0];
                //    if (Data_DataTable.Rows[0][1].toInt32() == 1)
                //    {
                //        BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update("ReturnMoneyUnit=@ReturnMoneyUnit,UpdateTime=@UpdateTime,UpdateBy=@UpdateBy", "ShopClient_ID=@ShopClient_ID and UserID=@UserID", myCountMoney_Vouchers, DateTime.Now, Eggsoft_Public_CL.Pub.geLoginShow(), strShopClientID.toInt32(), strRowNickID.toInt32());
                //    }
                //}
                #endregion 操作活动订单数的 财富积分余额


                btnChaXun_Click(sender, e);///刷新本页
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
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Int32.Parse(strRowNickID), out myCountMoney_Vouchers);
                    string myCount_Voucherslink = "../31ConsumptionCapital/07User_WealthStatus.aspx?userid=" + strRowNickID;
                    HyperLink_UserInfo_Wealth.Text = "财富余额" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney_Vouchers);
                    HyperLink_UserInfo_Wealth.NavigateUrl = myCount_Voucherslink;
                    HyperLink_UserInfo_Wealth.Target = "_blank";

                    Decimal myCountMoney = 0;
                    Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuETotalCredits_OperationCenter(Int32.Parse(strRowNickID), out myCountMoney);
                    string myCountMoneylink = "../31ConsumptionCapital/11CenterUser_MoneyStatus.aspx?userid=" + strRowNickID;
                    HyperLink_Center_Money.Text = "运营现金余额" + Eggsoft_Public_CL.Pub.getPubMoney(myCountMoney);
                    HyperLink_Center_Money.NavigateUrl = myCountMoneylink;
                    HyperLink_Center_Money.Target = "_blank";

                    #region 活动订单数
                    EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                    System.Data.DataTable Data_DataTable = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList("select sum(ActiveOrderNum),count(1) from b008_OpterationUserActiveReturnMoneyOrderNum where ShopClient_ID=@ShopClient_ID and UserID=@UserID and OrderDetailID is not NULL", strShopClientID.toInt32(), strRowNickID.toInt32()).Tables[0];
                    if (Data_DataTable.Rows[0][0].toInt32() == 0)
                    {
                        RadioButtonList_DouWuQuanOrMoney.Items[0].Enabled = false;
                    }
                    else
                    {
                        RadioButtonList_DouWuQuanOrMoney.Items[0].Enabled = true;
                    }
                    Label1ActiveOrderNum.Text = "活动订单数：" + Data_DataTable.Rows[0][0].toString();
                    #endregion 活动订单数



                    Panel_UserInfo.Visible = true;
                }
            }

        }
    }
}