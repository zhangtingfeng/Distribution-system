using Eggsoft.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver
{


    public partial class WebFormReadExcel : System.Web.UI.Page
    {
        private static Object thisLockWebFormReadExcel = new Object();

        protected void Page_Load(object sender, EventArgs e)
        {
            lock (thisLockWebFormReadExcel)
            {
                Response.Write(DateTime.Now.ToString());




                //updateShopTeamIDonceGiveAllBenJin(); //2017 12 21
                //updateShopTeamIDonceGiveAllBenJinUserTeamID();


                //#region 给各个用户发钱 给支付 超过7  或者 马上确认收货的 分销的结算一下  并且Over7DaysToBeans=1。。。。如果是运营中心 必须是支付超过7天的 并且设置过发货的才结算
                //Eggsoft.Common.debug_Log.Call_WriteLog("DoOver7daysCountMySonMoney_UpdateEvertDay开始执行T+7  1", "每天更新");
                //Eggsoft_Public_CL.Pub_FenXiao.DoOver7daysCountMySonMoney_UpdateEvertDay(0);
                //Eggsoft.Common.debug_Log.Call_WriteLog("DoOver7daysCountMySonMoney_UpdateEvertDay执行完毕T+7  1", "每天更新");
                //#endregion


                //onceJishu();
                onceJishuEveryDay();
                //onceJishuEveryDay();


                //trans_Wealth_OldOrderDetailID_From_to_NewOrderDetailID(41295, 9848, 9862);///9848多发钱  9862少发钱
                //onceGiveToOneOrderDetailID(12501, 44756, 21, (Decimal)503.20*3, true, "取消购物车购物车沁加活水宝");
                //trans_Wealth_OldOrderDetailID_From_to_NewOrderDetailID(41434, 9849, 9863);
                //trans_Wealth_OldOrderDetailID_From_to_NewOrderDetailID(41324, 9851, 9887);
                //trans_Wealth_OldOrderDetailID_From_to_NewOrderDetailID(42570, 9851, 9887);
                //trans_Wealth_OldOrderDetailID_From_to_NewOrderDetailID(41324, 9851, 9887);
                //trans_Wealth_OldOrderDetailID_From_to_NewOrderDetailID(41324, 9851, 9887);
                //trans_Wealth_OldOrderDetailID_From_to_NewOrderDetailID(41324, 9851, 9887);
                Response.Write("do over");
                //checkEveryDayMoney();
            }
        }

        private void updateShopTeamIDonceGiveAllBenJinUserTeamID()
        {
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            System.Data.DataTable DataDataTable = BLL_tab_ShopClient.GetList("*", "1=1 order by id asc").Tables[0];
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            ArrayList ArrayListSQL = new ArrayList();
            for (int i = 0; i < DataDataTable.Rows.Count; i++)
            {
                string strShopClientIDID = DataDataTable.Rows[i]["ID"].toString();
                if (strShopClientIDID == "21")
                {
                    //continue;
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    System.Data.DataTable curDataDataTable = BLL_tab_User.GetList("ID", "ShopClientID=@ShopClientID order by id asc", strShopClientIDID).Tables[0];

                    for (int j = 0; j < curDataDataTable.Rows.Count; j++)
                    {
                        string strUserID = curDataDataTable.Rows[j]["ID"].toString();
                        try
                        {
                            EggsoftWX.BLL.b005_UserID_Operation_ID BLL_b005_UserID_Operation_ID = new EggsoftWX.BLL.b005_UserID_Operation_ID();
                            EggsoftWX.Model.b005_UserID_Operation_ID Model_b005_UserID_Operation_ID = BLL_b005_UserID_Operation_ID.GetModel("UserID=@UserID", strUserID);
                            EggsoftWX.Model.tab_ShopClient_Agent_ Modeltab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=@UserID", Model_b005_UserID_Operation_ID.OperationCenterID_UserID);
                            string strTeamID = Modeltab_ShopClient_Agent_.ID.toString();
                            //BLL_tab_User.Update("TeamID=@TeamID", "ID=@ID", strTeamID, strUserID);
                            ArrayListSQL.Add("update tab_User set TeamID=" + strTeamID + "  where ID = " + strUserID);

                        }
                        catch (Exception eee)
                        {
                            //BLL_tab_User.Update("TeamID=@TeamID", "ID=@ID", 12607, strUserID);
                            ArrayListSQL.Add("update tab_User set TeamID=" + 12607 + "  where ID = " + strUserID);

                        }
                    }
                }
                else
                {
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    System.Data.DataTable curDataDataTable = BLL_tab_User.GetList("ID", "ShopClientID=@ShopClientID order by id asc", strShopClientIDID).Tables[0];

                    for (int j = 0; j < curDataDataTable.Rows.Count; j++)
                    {
                        string strUserID = curDataDataTable.Rows[j]["ID"].toString();
                        try
                        {
                            EggsoftWX.Model.tab_ShopClient_Agent_ Modeltab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + strUserID + " and AgentLevelSelect>0 and isnull(IsDeleted,0)=0");
                            if (Modeltab_ShopClient_Agent_ != null)
                            {
                                ArrayListSQL.Add("update tab_User set TeamID=" + Modeltab_ShopClient_Agent_.ID + "  where ID = " + strUserID);

                                // BLL_tab_User.Update("TeamID=@TeamID", "ID=@ID", Modeltab_ShopClient_Agent_.ID, strUserID);
                            }
                            else
                            {
                                System.Data.DataTable Data_DataTable = BLL_tab_ShopClient_Agent_.SelectList("select top 1 ID from tab_ShopClient_Agent_ where ShopTeamID>0 and ShopClientID=" + strShopClientIDID + " and AgentLevelSelect>0 order by ShopTeamID asc").Tables[0];
                                if (Data_DataTable.Rows.Count > 0)
                                {
                                    ArrayListSQL.Add("update tab_User set TeamID=" + Data_DataTable.Rows[0]["ID"].toInt32() + "  where ID = " + strUserID);

                                    //BLL_tab_User.Update("TeamID=@TeamID", "ID=@ID", Data_DataTable.Rows[0]["ID"].toInt32(), strUserID);
                                }
                                else
                                {
                                    Response.Write("Error" + strShopClientIDID + "<br />");
                                }
                            }

                        }
                        catch (Exception eee)
                        {
                            //BLL_tab_User.Update("TeamID=@ShopTeamID", "ID=@ID", 12607, strUserID);
                        }
                    }
                }
            }
            EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(ArrayListSQL);
        }
        /// <summary>
        /// 批量更新 商铺 代理ID
        /// </summary>
        private void updateShopTeamIDonceGiveAllBenJin()
        {
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            System.Data.DataTable DataDataTable = BLL_tab_ShopClient.GetList("*", "1=1 order by id asc").Tables[0];
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

            for (int i = 0; i < DataDataTable.Rows.Count; i++)
            {
                string strShopClientIDID = DataDataTable.Rows[i]["ID"].toString();

                System.Data.DataTable curDataDataTable = BLL_tab_ShopClient_Agent_.GetList("ID", "AgentLevelSelect>0 and ShopClientID=@ShopClientID order by id asc", strShopClientIDID).Tables[0];

                for (int j = 0; j < curDataDataTable.Rows.Count; j++)
                {
                    BLL_tab_ShopClient_Agent_.Update("ShopTeamID=@ShopTeamID", "ID=@ID", (j + 1), curDataDataTable.Rows[j]["ID"].toString());
                }
            }

        }
        /// <summary>
        /// 一次性归还所有本金
        /// </summary>
        private void onceGiveAllBenJin()
        {
            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
            EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();


            string strReturnMoneyWhere = @"SELECT tab_Orderdetails.GoodTypeId AS 运营中心编号, V5_1.OrderDetailID, 
      V5_1.OrderCount AS 数量, V5_1.PayDateTime AS 支付时间, 
      V5_1.ActiveOrderNum AS 活动订单数, V5_1.ReturnMoneyUnit AS 还有多少需要还, 
      V5_1.Got AS 已经得到, V5_1.myPay AS 我支付的, 
      V5_1.myneed AS 我需要多少够本金, tab_User.ShopUserID AS ID,tab_User.ID AS UserID, 
      tab_User.UserRealName, tab_User.NickName, 
      b002_OperationCenter.MasterName AS 运营中心
FROM b002_OperationCenter RIGHT OUTER JOIN
      tab_Orderdetails ON 
      b002_OperationCenter.ID = tab_Orderdetails.GoodTypeId RIGHT OUTER JOIN
          (SELECT TOP (10000) ID, UserID, OrderID, OrderDetailID, OrderCount, 
               PayDateTime, ShopClient_ID, InputShouldReturnCount, ActiveOrderNum, 
               OutHadGivedUserNum, ReturnMoneyUnit, b004_OperationGoodsID, 
               (ActiveOrderNum * 1998 * 3 - ReturnMoneyUnit) 
               - ActiveOrderNum * 1998 * 3 * 0.2 AS Got, ActiveOrderNum * 1998 AS myPay, 
               ActiveOrderNum * 1998 - ((ActiveOrderNum * 1998 * 3 - ReturnMoneyUnit) 
               - ActiveOrderNum * 1998 * 3 * 0.2) AS myneed
         FROM b008_OpterationUserActiveReturnMoneyOrderNum
         WHERE ((ActiveOrderNum * 1998 * 3 - ReturnMoneyUnit) 
               - ActiveOrderNum * 1998 * 3 * 0.2 < ActiveOrderNum * 1998) AND 
               (OrderDetailID IS NOT NULL) AND (ActiveOrderNum > 0)
         ORDER BY PayDateTime) AS V5_1 LEFT OUTER JOIN
      tab_User ON V5_1.UserID = tab_User.ID ON 
      tab_Orderdetails.ID = V5_1.OrderDetailID ";

            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            System.Data.DataTable DataTableUserWillGetMoney_b008 = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetList("ID,UserID,ActiveOrderNum,b004_OperationGoodsID,ReturnMoneyUnit,OrderDetailID,ShopClient_ID", " OrderDetailID is not null and b004_OperationGoodsID=@b004_OperationGoodsID and ShopClient_ID=@ShopClient_ID and ActiveOrderNum>0", 1, 21).Tables[0];

            for (int ppppp = 0; ppppp < DataTableUserWillGetMoney_b008.Rows.Count; ppppp++)
            {
                string strID = DataTableUserWillGetMoney_b008.Rows[ppppp]["ID"].ToString();
                string strUserID = DataTableUserWillGetMoney_b008.Rows[ppppp]["UserID"].ToString();
                string strShopClient_ID = DataTableUserWillGetMoney_b008.Rows[ppppp]["ShopClient_ID"].ToString();
                string strOrderDetailID = DataTableUserWillGetMoney_b008.Rows[ppppp]["OrderDetailID"].ToString();
                int intActiveOrderNum = DataTableUserWillGetMoney_b008.Rows[ppppp]["ActiveOrderNum"].toInt32();
                int intb004_OperationGoodsID = DataTableUserWillGetMoney_b008.Rows[ppppp]["b004_OperationGoodsID"].toInt32();
                Decimal DecimalReturnMoneyUnit = DataTableUserWillGetMoney_b008.Rows[ppppp]["ReturnMoneyUnit"].toDecimal();////本订单 还剩下多少钱

                Decimal DecimalWillDis = intActiveOrderNum * (Decimal)0.2 * 1998 * 3;///将要一次性扣除的钱
                if (DecimalReturnMoneyUnit > DecimalWillDis)
                {

                    #region  一次性减去财富值
                    EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser_Once = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                    Model_b006_TotalWealth_OperationUser_Once.Bool_ConsumeOrRecharge = false;
                    Model_b006_TotalWealth_OperationUser_Once.OrderDetailID = strOrderDetailID.toInt32();
                    Model_b006_TotalWealth_OperationUser_Once.UserID = strUserID.toInt32();
                    Model_b006_TotalWealth_OperationUser_Once.ShopClient_ID = strShopClient_ID.toInt32();
                    Model_b006_TotalWealth_OperationUser_Once.ConsumeOrRechargeWealth = DecimalWillDis;
                    Model_b006_TotalWealth_OperationUser_Once.ConsumeTypeOrRecharge = "20%财富积分是您应支付给公司寄售服务费，其中含依法为您代缴税部分OrderDetailID" + strOrderDetailID;
                    my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser_Once);
                    #endregion 一次性减去财富值


                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID and OrderDetailID=@OrderDetailID", strUserID.toInt32(), strShopClient_ID.toInt32(), intb004_OperationGoodsID, strOrderDetailID);
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = DecimalReturnMoneyUnit - DecimalWillDis;
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "减增进入现金,给差额";
                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);



                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = strUserID.toInt32();
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalWillDis * (Decimal)0.135;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "已经馈赠13.5%的沁加币到您的账户（沁加币可在商城抵用22%现金）OrderDetailID" + strOrderDetailID;
                    int intTableReturn__ID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                    #region 增加未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage01 = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage01 = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage01.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage01.CreateBy = "补贴扣税方式";
                    Model_b011_InfoAlertMessage01.UpdateBy = "补贴扣税方式";
                    Model_b011_InfoAlertMessage01.UserID = strUserID.toInt32();
                    Model_b011_InfoAlertMessage01.ShopClient_ID = strShopClient_ID.toInt32();
                    Model_b011_InfoAlertMessage01.Type = "Info_GouWuHongBao";
                    Model_b011_InfoAlertMessage01.TypeTableID = intTableReturn__ID;
                    bll_b011_InfoAlertMessage01.Add(Model_b011_InfoAlertMessage01);
                    #endregion 增加未处理信息

                }
                else
                {
                    Response.Write("strUserID=" + strUserID + "  strID=" + strID + " 出现意外");

                }

            }


        }




        /// <summary>
        /// 给某个详细订单加减财富值
        /// </summary>
        private void onceGiveToOneOrderDetailID(int intOrderDetailID, int UserID, int intShopClientID, Decimal DecimalAddMoney, bool boolAddOrMinus, string strDesc)
        {
            EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();


            #region  给某个详细订单加减财富值
            EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser_Once = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
            Model_b006_TotalWealth_OperationUser_Once.Bool_ConsumeOrRecharge = boolAddOrMinus;
            Model_b006_TotalWealth_OperationUser_Once.OrderDetailID = intOrderDetailID;
            Model_b006_TotalWealth_OperationUser_Once.UserID = UserID;
            Model_b006_TotalWealth_OperationUser_Once.ShopClient_ID = intShopClientID;
            Model_b006_TotalWealth_OperationUser_Once.ConsumeOrRechargeWealth = DecimalAddMoney;
            Model_b006_TotalWealth_OperationUser_Once.ConsumeTypeOrRecharge = strDesc + "OrderDetailID" + intOrderDetailID;
            my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser_Once);
            #endregion 给某个详细订单加减财富值

            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("OrderDetailID=@intOrderDetailID and UserID=@UserID", intOrderDetailID, UserID);

            if (boolAddOrMinus == true)
            {
                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() + DecimalAddMoney;
            }
            else
            {
                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() - DecimalAddMoney;
            }

            Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "给某个详细订单加减财富值ByOliver";
            Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
            BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);


        }
        private void onceJishuEveryDay()
        {
            #region 每天运营中心加权分红
            Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红开始执行4", "每天更新");
            Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay mmPub_Default_DoYunYingZhongXin28EveryDay = new Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay(21);
            Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红执行完毕4", "每天更新");
            //string strFilePath = "~/File/21doYunYinZhongXin28Action.txt";
            //Response.Write(Eggsoft.Common.FileFolder.ReadFile((strFilePath)));

            //string strFilePath = "~/File/21do" + DateTime.Now.ToString("yyyyMMdd") + "YunYinZhongXin28Action.txt";
            //Response.Write(Eggsoft.Common.FileFolder.ReadFile((strFilePath)));
            #endregion 每天运营中心加权分红
        }



        /// <summary>
        /// 一次性扣除所有的技术管理服务费
        /// </summary>
        private void onceJishu()
        {
            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
            EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();


            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            System.Data.DataTable DataTableUserWillGetMoney_b008 = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetList("ID,UserID,ActiveOrderNum,b004_OperationGoodsID,ReturnMoneyUnit,OrderDetailID,ShopClient_ID", " OrderDetailID is not null and b004_OperationGoodsID=@b004_OperationGoodsID and ShopClient_ID=@ShopClient_ID and ActiveOrderNum>0", 1, 21).Tables[0];

            for (int ppppp = 0; ppppp < DataTableUserWillGetMoney_b008.Rows.Count; ppppp++)
            {
                string strID = DataTableUserWillGetMoney_b008.Rows[ppppp]["ID"].ToString();
                string strUserID = DataTableUserWillGetMoney_b008.Rows[ppppp]["UserID"].ToString();
                string strShopClient_ID = DataTableUserWillGetMoney_b008.Rows[ppppp]["ShopClient_ID"].ToString();
                string strOrderDetailID = DataTableUserWillGetMoney_b008.Rows[ppppp]["OrderDetailID"].ToString();
                int intActiveOrderNum = DataTableUserWillGetMoney_b008.Rows[ppppp]["ActiveOrderNum"].toInt32();
                int intb004_OperationGoodsID = DataTableUserWillGetMoney_b008.Rows[ppppp]["b004_OperationGoodsID"].toInt32();
                Decimal DecimalReturnMoneyUnit = DataTableUserWillGetMoney_b008.Rows[ppppp]["ReturnMoneyUnit"].toDecimal();////本订单 还剩下多少钱

                Decimal DecimalWillDis = intActiveOrderNum * (Decimal)0.2 * 1998 * 3;///将要一次性扣除的钱
                if (DecimalReturnMoneyUnit > DecimalWillDis)
                {

                    #region  一次性减去财富值
                    EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser_Once = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                    Model_b006_TotalWealth_OperationUser_Once.Bool_ConsumeOrRecharge = false;
                    Model_b006_TotalWealth_OperationUser_Once.OrderDetailID = strOrderDetailID.toInt32();
                    Model_b006_TotalWealth_OperationUser_Once.UserID = strUserID.toInt32();
                    Model_b006_TotalWealth_OperationUser_Once.ShopClient_ID = strShopClient_ID.toInt32();
                    Model_b006_TotalWealth_OperationUser_Once.ConsumeOrRechargeWealth = DecimalWillDis;
                    Model_b006_TotalWealth_OperationUser_Once.ConsumeTypeOrRecharge = "20%财富积分是您应支付给公司寄售服务费，其中含依法为您代缴税部分OrderDetailID" + strOrderDetailID;
                    my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser_Once);
                    #endregion 一次性减去财富值


                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID and OrderDetailID=@OrderDetailID", strUserID.toInt32(), strShopClient_ID.toInt32(), intb004_OperationGoodsID, strOrderDetailID);
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = DecimalReturnMoneyUnit - DecimalWillDis;
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "减增进入现金,给差额";
                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);



                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = strUserID.toInt32();
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalWillDis * (Decimal)0.135;
                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "已经馈赠13.5%的沁加币到您的账户（沁加币可在商城抵用22%现金）OrderDetailID" + strOrderDetailID;
                    int intTableReturn__ID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                    #region 增加未处理信息
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage01 = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage01 = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage01.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                    Model_b011_InfoAlertMessage01.CreateBy = "补贴扣税方式";
                    Model_b011_InfoAlertMessage01.UpdateBy = "补贴扣税方式";
                    Model_b011_InfoAlertMessage01.UserID = strUserID.toInt32();
                    Model_b011_InfoAlertMessage01.ShopClient_ID = strShopClient_ID.toInt32();
                    Model_b011_InfoAlertMessage01.Type = "Info_GouWuHongBao";
                    Model_b011_InfoAlertMessage01.TypeTableID = intTableReturn__ID;
                    bll_b011_InfoAlertMessage01.Add(Model_b011_InfoAlertMessage01);
                    #endregion 增加未处理信息

                }
                else
                {
                    Response.Write("strUserID=" + strUserID + "  strID=" + strID + " 出现意外");

                }

            }


        }



        private void PingHezhi()
        {
            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            EggsoftWX.Model.b006_TotalWealth_OperationUser old_Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
            old_Model_b006_TotalWealth_OperationUser.UserID = 41256;
            old_Model_b006_TotalWealth_OperationUser.OrderDetailID = 9852;
            old_Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
            old_Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
            old_Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "订单系统事后跟踪oliverToNewOrderDetailID" + 9852;
            old_Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = (decimal)-5994;
            int intddddd = BLL_b006_TotalWealth_OperationUser.Add(old_Model_b006_TotalWealth_OperationUser);
        }



        private void trans_Wealth_OldOrderDetailID_From_to_NewOrderDetailID(int intUserID, int intOLDOrderDetailID, int intNewOrderDetailID)
        {
            EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.Model.tab_Orderdetails OLD_Model_Orderdetails = BLL_tab_Orderdetails.GetModel(intOLDOrderDetailID);
            EggsoftWX.Model.tab_Orderdetails New_Model_Orderdetails = BLL_tab_Orderdetails.GetModel(intNewOrderDetailID);



            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            string strWhere = "select sum(ConsumeOrRechargeWealth) from b006_TotalWealth_OperationUser where UserID=@UserID and OrderDetailID=@OrderDetailID";

            Decimal OldDecimal = BLL_b006_TotalWealth_OperationUser.SelectList(strWhere, intUserID, intOLDOrderDetailID).Tables[0].Rows[0][0].toDecimal();
            Decimal NewDecimal = BLL_b006_TotalWealth_OperationUser.SelectList(strWhere, intUserID, intNewOrderDetailID).Tables[0].Rows[0][0].toDecimal();

            Decimal tOldWillNow = OLD_Model_Orderdetails.OrderCount.toInt32() * 1998 * 3 * (Decimal)0.8 - OLD_Model_Orderdetails.OrderCount.toInt32() * (Decimal)0.04;//   OLD_Model_Orderdetails.OrderCount.toInt32() * (Decimal)0.01 + ;
            Decimal tOldWillDis = tOldWillNow - OldDecimal;


            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and OrderDetailID=@OrderDetailID", intUserID, intOLDOrderDetailID);
            EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum NEW_Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and OrderDetailID=@OrderDetailID", intUserID, intNewOrderDetailID);
            if (Math.Abs(OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() - (OLD_Model_Orderdetails.OrderCount.toInt32() * 1998 * 3 - OldDecimal)) > (Decimal)0.10 || Math.Abs(NEW_Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() - (New_Model_Orderdetails.OrderCount.toInt32() * 1998 * 3 - NewDecimal)) > (Decimal)0.10)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("int intUserID, int intOLDOrderDetailID, int intNewOrderDetailID " + intUserID + " " + intOLDOrderDetailID + " " + intNewOrderDetailID, "出现误差，需要跟踪");
                return;
            }



            EggsoftWX.Model.b006_TotalWealth_OperationUser old_Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
            old_Model_b006_TotalWealth_OperationUser.UserID = intUserID;
            old_Model_b006_TotalWealth_OperationUser.OrderDetailID = intOLDOrderDetailID;
            old_Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
            old_Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
            old_Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "订单系统事后跟踪oliverToNewOrderDetailID" + intNewOrderDetailID;
            old_Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = tOldWillDis;
            int intddddd = BLL_b006_TotalWealth_OperationUser.Add(old_Model_b006_TotalWealth_OperationUser);



            EggsoftWX.Model.b006_TotalWealth_OperationUser New_Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
            New_Model_b006_TotalWealth_OperationUser.UserID = intUserID;
            New_Model_b006_TotalWealth_OperationUser.OrderDetailID = intNewOrderDetailID;
            New_Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
            New_Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
            New_Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "订单系统事后跟踪oliverFromOLDOrderDetailID" + intOLDOrderDetailID;
            New_Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = -tOldWillDis;
            int intddddd35634 = BLL_b006_TotalWealth_OperationUser.Add(New_Model_b006_TotalWealth_OperationUser);

            OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit - tOldWillDis;
            OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + old_Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
            OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString();
            OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
            BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(OLD_Model_b008_OpterationUserActiveReturnMoneyOrderNum);


            NEW_Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = NEW_Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit + tOldWillDis;
            NEW_Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = NEW_Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + New_Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
            NEW_Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
            BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(NEW_Model_b008_OpterationUserActiveReturnMoneyOrderNum);

        }

        private void _009_0930()
        {
            //EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            //EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();


            //Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
            //Model_b006_TotalWealth_OperationUser.UserID = 43380;
            //Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
            //Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = (Decimal)3084.43;
            //Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("请帮忙把微店号803剩余的财富积分3084.43转给微店号519张晓辉", 46);
            //int intID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);



            //_009ResetTo008_step1();
            //_009ResetTo008_step4();
            //_009ResetTo008_step5();
            //_009ResetTo008_step6();////仅统计用 看页面文件就行了


            //--去掉表b006_TotalWealth_OperationUser的主键
            //_009ResetTo008_step7();
            //恢复 再增加上去主键b006_TotalWealth_OperationUser
            /// 手动删除 IDAdd


            /// 接轨活动订单数 。如果 有出局的 让订单也出局
            //_009ResetTo008_step8();
            //_009ResetTo008_step9();

            Response.Write(DateTime.Now.ToString());


            //#region 每天运营中心加权分红
            //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红开始执行4", "每天更新");
            //Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay mmPub_Default_DoYunYingZhongXin28EveryDay = new Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay(21);
            //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红执行完毕4", "每天更新");
            //#endregion 每天运营中心加权分红

        }



        /// <summary>
        /// 删除一些数据 。order is not null  单数不存在 order是空的
        /// </summary>
        private void _009ResetTo008_step8()
        {
            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLLb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            System.Data.DataTable Data_DataTableIDADD = BLLb008_OpterationUserActiveReturnMoneyOrderNum.SelectList("select userID from b008_OpterationUserActiveReturnMoneyOrderNum where orderid is not null group by userID order by min(id) asc").Tables[0];
            for (int i = 0; i < Data_DataTableIDADD.Rows.Count; i++)
            {
                string strUserID = Data_DataTableIDADD.Rows[i]["userID"].toString();
                //if (strUserID != "44387") continue;

                string strExsit = "select count(1) from b008_OpterationUserActiveReturnMoneyOrderNum where orderid is null and userid=" + strUserID;
                bool boolean = BLLb008_OpterationUserActiveReturnMoneyOrderNum.Exists("1=1 and orderid is null and userid = " + strUserID);
                if (boolean)
                {

                    String strSQLExitCount = "select sum(ActiveOrderNum) from b008_OpterationUserActiveReturnMoneyOrderNum where orderid is not null and userid=" + strUserID;
                    int intCount32 = BLLb008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strSQLExitCount).Tables[0].Rows[0][0].toInt32();


                    String strSQLOLDCount = "select sum(ActiveOrderNum) from b008_OpterationUserActiveReturnMoneyOrderNum where orderid is null and userid=" + strUserID;
                    int intOLDCount32 = BLLb008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strSQLOLDCount).Tables[0].Rows[0][0].toInt32();

                    if (intOLDCount32 <= intCount32)
                    {
                        string strOut = "<br />这是可以接受的 strUserID=" + strUserID;
                        Response.Write(strOut);


                    }
                    else
                    {
                        string strOut = "<br />这是有问题的 必须进一步观察 strUserID=" + strUserID;
                        Response.Write(strOut);


                    }
                }
                else
                {
                    string strOut = "<br />这是有问题的用户  删除才正常 删除他们 strUserID=" + strUserID;
                    Response.Write(strOut);

                    BLLb008_OpterationUserActiveReturnMoneyOrderNum.Delete("userid = " + strUserID);
                }


            }
            #region
            /*2017/9/24 23:42:32
这是可以接受的 strUserID=41230
这是可以接受的 strUserID=41210
这是可以接受的 strUserID=41263
这是可以接受的 strUserID=41199
这是可以接受的 strUserID=41296
这是可以接受的 strUserID=41205
这是可以接受的 strUserID=42147
这是可以接受的 strUserID=41595
这是可以接受的 strUserID=42577
这是可以接受的 strUserID=41522
这是可以接受的 strUserID=41295
这是可以接受的 strUserID=41434
这是可以接受的 strUserID=42781
这是可以接受的 strUserID=41324
这是可以接受的 strUserID=41256
这是可以接受的 strUserID=42570
这是可以接受的 strUserID=42642
这是可以接受的 strUserID=41833
这是可以接受的 strUserID=42583
这是可以接受的 strUserID=43108
这是可以接受的 strUserID=43123
这是可以接受的 strUserID=42780
这是可以接受的 strUserID=41220
这是可以接受的 strUserID=42989
这是可以接受的 strUserID=42977
这是可以接受的 strUserID=43153
这是可以接受的 strUserID=43177
这是可以接受的 strUserID=41257
这是可以接受的 strUserID=43131
这是可以接受的 strUserID=43132
这是可以接受的 strUserID=43113
这是可以接受的 strUserID=43117
这是可以接受的 strUserID=43140
这是可以接受的 strUserID=43145
这是可以接受的 strUserID=43111
这是可以接受的 strUserID=43142
这是可以接受的 strUserID=43136
这是可以接受的 strUserID=42985
这是可以接受的 strUserID=43157
这是可以接受的 strUserID=43141
这是可以接受的 strUserID=43126
这是可以接受的 strUserID=43077
这是可以接受的 strUserID=43161
这是可以接受的 strUserID=43137
这是可以接受的 strUserID=41306
这是可以接受的 strUserID=43072
这是可以接受的 strUserID=43112
这是可以接受的 strUserID=43114
这是可以接受的 strUserID=43119
这是可以接受的 strUserID=43165
这是可以接受的 strUserID=43169
这是可以接受的 strUserID=43162
这是可以接受的 strUserID=43127
这是可以接受的 strUserID=43122
这是可以接受的 strUserID=43120
这是可以接受的 strUserID=43146
这是可以接受的 strUserID=43155
这是可以接受的 strUserID=43133
这是可以接受的 strUserID=43143
这是可以接受的 strUserID=43181
这是可以接受的 strUserID=43179
这是可以接受的 strUserID=43019
这是可以接受的 strUserID=43158
这是可以接受的 strUserID=43156
这是可以接受的 strUserID=43160
这是可以接受的 strUserID=43200
这是可以接受的 strUserID=43182
这是可以接受的 strUserID=43116
这是可以接受的 strUserID=43185
这是可以接受的 strUserID=43144
这是可以接受的 strUserID=43082
这是可以接受的 strUserID=43212
这是可以接受的 strUserID=43215
这是可以接受的 strUserID=43209
这是可以接受的 strUserID=43149
这是可以接受的 strUserID=43213
这是可以接受的 strUserID=43211
这是可以接受的 strUserID=43134
这是可以接受的 strUserID=43110
这是可以接受的 strUserID=43173
这是可以接受的 strUserID=43163
这是可以接受的 strUserID=43183
这是可以接受的 strUserID=43154
这是可以接受的 strUserID=43178
这是可以接受的 strUserID=43118
这是可以接受的 strUserID=43124
这是可以接受的 strUserID=43138
这是可以接受的 strUserID=43139
这是可以接受的 strUserID=43226
这是可以接受的 strUserID=43105
这是可以接受的 strUserID=43216
这是可以接受的 strUserID=42448
这是可以接受的 strUserID=43168
这是可以接受的 strUserID=43042
这是可以接受的 strUserID=43221
这是可以接受的 strUserID=43206
这是可以接受的 strUserID=43086
这是可以接受的 strUserID=43232
这是可以接受的 strUserID=43240
这是可以接受的 strUserID=43125
这是可以接受的 strUserID=43214
这是可以接受的 strUserID=43167
这是可以接受的 strUserID=43210
这是可以接受的 strUserID=43159
这是可以接受的 strUserID=43203
这是可以接受的 strUserID=43164
这是可以接受的 strUserID=42674
这是可以接受的 strUserID=41192
这是可以接受的 strUserID=43310
这是可以接受的 strUserID=43204
这是可以接受的 strUserID=43278
这是可以接受的 strUserID=43239
这是可以接受的 strUserID=43220
这是可以接受的 strUserID=43236
这是可以接受的 strUserID=43150
这是可以接受的 strUserID=43296
这是可以接受的 strUserID=43268
这是可以接受的 strUserID=43260
这是可以接受的 strUserID=43274
这是可以接受的 strUserID=43270
这是可以接受的 strUserID=42975
这是可以接受的 strUserID=43430
这是可以接受的 strUserID=43255
这是可以接受的 strUserID=41977
这是可以接受的 strUserID=43346
这是可以接受的 strUserID=43099
这是可以接受的 strUserID=43295
这是可以接受的 strUserID=43307
这是可以接受的 strUserID=43333
这是可以接受的 strUserID=43329
这是可以接受的 strUserID=42768
这是可以接受的 strUserID=43319
这是可以接受的 strUserID=43277
这是可以接受的 strUserID=43279
这是可以接受的 strUserID=43365
这是可以接受的 strUserID=43276
这是可以接受的 strUserID=43272
这是可以接受的 strUserID=43345
这是可以接受的 strUserID=43352
这是可以接受的 strUserID=43338
这是可以接受的 strUserID=43327
这是可以接受的 strUserID=43322
这是可以接受的 strUserID=43281
这是可以接受的 strUserID=43323
这是可以接受的 strUserID=43324
这是可以接受的 strUserID=43337
这是可以接受的 strUserID=43326
这是可以接受的 strUserID=43362
这是可以接受的 strUserID=43381
这是可以接受的 strUserID=43382
这是可以接受的 strUserID=43372
这是可以接受的 strUserID=43410
这是可以接受的 strUserID=43422
这是可以接受的 strUserID=43288
这是可以接受的 strUserID=43612
这是可以接受的 strUserID=43432
这是可以接受的 strUserID=42767
这是可以接受的 strUserID=43614
这是可以接受的 strUserID=43618
这是可以接受的 strUserID=41526
这是可以接受的 strUserID=43444
这是可以接受的 strUserID=43464
这是可以接受的 strUserID=43441
这是可以接受的 strUserID=43301
这是可以接受的 strUserID=43152
这是可以接受的 strUserID=43355
这是可以接受的 strUserID=43621
这是可以接受的 strUserID=43596
这是可以接受的 strUserID=43599
这是可以接受的 strUserID=43592
这是可以接受的 strUserID=43562
这是可以接受的 strUserID=43561
这是可以接受的 strUserID=43540
这是可以接受的 strUserID=43570
这是可以接受的 strUserID=43471
这是可以接受的 strUserID=43787
这是可以接受的 strUserID=43829
这是可以接受的 strUserID=43558
这是可以接受的 strUserID=43607
这是可以接受的 strUserID=43619
这是可以接受的 strUserID=43660
这是可以接受的 strUserID=43664
这是可以接受的 strUserID=43584
这是可以接受的 strUserID=43661
这是可以接受的 strUserID=43668
这是可以接受的 strUserID=43733
这是可以接受的 strUserID=43581
这是可以接受的 strUserID=43632
这是可以接受的 strUserID=43710
这是可以接受的 strUserID=43766
这是可以接受的 strUserID=43817
这是可以接受的 strUserID=43735
这是可以接受的 strUserID=43828
这是可以接受的 strUserID=43704
这是可以接受的 strUserID=43907
这是可以接受的 strUserID=43594
这是可以接受的 strUserID=43895
这是可以接受的 strUserID=42957
这是可以接受的 strUserID=43217
这是可以接受的 strUserID=43650
这是可以接受的 strUserID=43949
这是可以接受的 strUserID=43908
这是可以接受的 strUserID=43380
这是可以接受的 strUserID=43421
这是可以接受的 strUserID=43899
这是可以接受的 strUserID=43869
这是可以接受的 strUserID=43290
这是可以接受的 strUserID=43904
这是可以接受的 strUserID=43826
这是可以接受的 strUserID=43915
这是可以接受的 strUserID=43950
这是可以接受的 strUserID=43984
这是可以接受的 strUserID=43640
这是可以接受的 strUserID=43978
这是可以接受的 strUserID=44004
这是可以接受的 strUserID=43385
这是可以接受的 strUserID=43994
这是可以接受的 strUserID=43972
这是可以接受的 strUserID=44002
这是可以接受的 strUserID=43447
这是可以接受的 strUserID=43325
这是可以接受的 strUserID=43863
这是可以接受的 strUserID=44065
这是可以接受的 strUserID=44079
这是可以接受的 strUserID=44075
这是可以接受的 strUserID=44058
这是可以接受的 strUserID=44082
这是可以接受的 strUserID=44084
这是可以接受的 strUserID=44155
这是可以接受的 strUserID=43649
这是可以接受的 strUserID=44121
这是可以接受的 strUserID=43330
这是可以接受的 strUserID=44110
这是可以接受的 strUserID=43331
这是可以接受的 strUserID=43665
这是可以接受的 strUserID=44183
这是可以接受的 strUserID=44133
这是可以接受的 strUserID=44191
这是可以接受的 strUserID=44100
这是可以接受的 strUserID=43988
这是可以接受的 strUserID=44219
这是可以接受的 strUserID=44283
这是可以接受的 strUserID=44240
这是可以接受的 strUserID=44290
这是可以接受的 strUserID=44292
这是可以接受的 strUserID=44105
这是可以接受的 strUserID=44266
这是可以接受的 strUserID=44308
这是可以接受的 strUserID=44334
这是可以接受的 strUserID=44087
这是可以接受的 strUserID=44332
这是可以接受的 strUserID=43802
这是可以接受的 strUserID=44403
这是可以接受的 strUserID=44248
这是可以接受的 strUserID=43347
这是可以接受的 strUserID=44433
这是可以接受的 strUserID=42906
这是可以接受的 strUserID=44448
这是可以接受的 strUserID=44494
这是可以接受的 strUserID=43579
这是可以接受的 strUserID=44474
这是可以接受的 strUserID=44170
这是可以接受的 strUserID=44431
这是可以接受的 strUserID=44475
这是可以接受的 strUserID=41310
这是可以接受的 strUserID=44103
这是可以接受的 strUserID=44455
这是可以接受的 strUserID=41774
这是可以接受的 strUserID=44496
这是可以接受的 strUserID=43686
这是可以接受的 strUserID=44316
这是可以接受的 strUserID=44346
这是可以接受的 strUserID=44405
这是可以接受的 strUserID=44567
这是可以接受的 strUserID=44253
这是可以接受的 strUserID=43413
这是可以接受的 strUserID=44596
这是可以接受的 strUserID=43539
这是可以接受的 strUserID=43400
这是可以接受的 strUserID=44646
这是可以接受的 strUserID=44623
这是可以接受的 strUserID=43837
这是可以接受的 strUserID=44481
这是可以接受的 strUserID=44499
这是可以接受的 strUserID=44525
这是可以接受的 strUserID=43792
这是可以接受的 strUserID=44647
这是可以接受的 strUserID=44520
这是可以接受的 strUserID=44592
这是可以接受的 strUserID=44556
这是可以接受的 strUserID=44563
这是可以接受的 strUserID=44564
这是可以接受的 strUserID=44559
这是可以接受的 strUserID=44554
这是可以接受的 strUserID=44660
这是可以接受的 strUserID=44662
这是可以接受的 strUserID=43469
这是可以接受的 strUserID=44051
这是可以接受的 strUserID=44642
这是可以接受的 strUserID=44631
这是可以接受的 strUserID=41905
这是可以接受的 strUserID=44692
这是可以接受的 strUserID=44689
这是可以接受的 strUserID=44549
这是可以接受的 strUserID=44690
这是可以接受的 strUserID=44698
这是可以接受的 strUserID=44711
这是可以接受的 strUserID=44714
这是可以接受的 strUserID=44658
这是可以接受的 strUserID=44691
这是可以接受的 strUserID=44709
这是可以接受的 strUserID=44715
这是可以接受的 strUserID=43697
这是可以接受的 strUserID=44635
这是可以接受的 strUserID=44605
这是可以接受的 strUserID=44171
这是可以接受的 strUserID=44733
这是可以接受的 strUserID=44688
这是可以接受的 strUserID=44264
这是可以接受的 strUserID=44665
这是可以接受的 strUserID=44418
这是可以接受的 strUserID=44717
这是可以接受的 strUserID=44620
这是可以接受的 strUserID=44911
这是可以接受的 strUserID=44735
这是可以接受的 strUserID=44723
这是可以接受的 strUserID=44728
这是可以接受的 strUserID=44736
这是可以接受的 strUserID=44341
这是可以接受的 strUserID=44738
这是可以接受的 strUserID=44739
这是可以接受的 strUserID=43456
这是可以接受的 strUserID=44847
这是可以接受的 strUserID=44849
这是可以接受的 strUserID=44886
这是可以接受的 strUserID=44601
这是可以接受的 strUserID=44541
这是可以接受的 strUserID=44756
这是可以接受的 strUserID=44786
这是可以接受的 strUserID=44773
这是可以接受的 strUserID=44676
这是可以接受的 strUserID=44828
这是可以接受的 strUserID=44825
这是可以接受的 strUserID=44095
这是可以接受的 strUserID=44864
这是可以接受的 strUserID=44866
这是可以接受的 strUserID=43971
这是可以接受的 strUserID=44447
这是可以接受的 strUserID=43683
这是可以接受的 strUserID=45066
这是可以接受的 strUserID=43107
这是可以接受的 strUserID=44720
这是可以接受的 strUserID=44874
这是可以接受的 strUserID=44939
这是可以接受的 strUserID=44950
这是可以接受的 strUserID=44970
这是可以接受的 strUserID=44965
这是可以接受的 strUserID=43591
这是可以接受的 strUserID=45095
这是可以接受的 strUserID=41506
这是可以接受的 strUserID=44930
这是可以接受的 strUserID=45028
这是可以接受的 strUserID=45054
这是可以接受的 strUserID=45046
这是可以接受的 strUserID=44944
这是可以接受的 strUserID=45084
这是可以接受的 strUserID=45092
这是可以接受的 strUserID=43653
这是可以接受的 strUserID=44261
这是可以接受的 strUserID=45117
这是可以接受的 strUserID=45129
这是可以接受的 strUserID=44539
这是可以接受的 strUserID=44973
这是可以接受的 strUserID=44288
这是可以接受的 strUserID=41398
这是可以接受的 strUserID=45136
这是可以接受的 strUserID=45106
这是可以接受的 strUserID=45156
这是可以接受的 strUserID=41411
这是可以接受的 strUserID=45053
这是可以接受的 strUserID=45140
这是可以接受的 strUserID=44971
这是可以接受的 strUserID=44990
这是可以接受的 strUserID=45075
这是可以接受的 strUserID=45044
这是可以接受的 strUserID=45027
这是可以接受的 strUserID=45026
这是可以接受的 strUserID=45018
这是可以接受的 strUserID=45072
这是可以接受的 strUserID=45138
这是可以接受的 strUserID=45174
这是可以接受的 strUserID=45111
这是可以接受的 strUserID=45192
这是可以接受的 strUserID=45190
这是可以接受的 strUserID=45199
这是可以接受的 strUserID=45170
这是可以接受的 strUserID=45130
这是可以接受的 strUserID=45222
这是可以接受的 strUserID=43280
这是可以接受的 strUserID=45208
这是可以接受的 strUserID=43833
这是可以接受的 strUserID=45226
这是可以接受的 strUserID=43737
这是可以接受的 strUserID=43412
这是可以接受的 strUserID=45252
这是可以接受的 strUserID=45244
这是可以接受的 strUserID=44838
这是可以接受的 strUserID=45001
这是可以接受的 strUserID=44801
这是可以接受的 strUserID=45265
这是可以接受的 strUserID=45272
这是可以接受的 strUserID=44027
这是可以接受的 strUserID=45296
这是可以接受的 strUserID=45307
这是可以接受的 strUserID=45341
这是可以接受的 strUserID=43608
这是可以接受的 strUserID=44161
这是可以接受的 strUserID=45364
这是可以接受的 strUserID=45096
这是可以接受的 strUserID=45253
这是可以接受的 strUserID=45413
这是可以接受的 strUserID=43790
这是可以接受的 strUserID=44681
这是可以接受的 strUserID=45201
这是可以接受的 strUserID=45417
这是可以接受的 strUserID=45375
这是可以接受的 strUserID=45335
这是可以接受的 strUserID=45488
这是可以接受的 strUserID=43565
这是可以接受的 strUserID=45209
这是可以接受的 strUserID=45440
这是可以接受的 strUserID=42166
这是可以接受的 strUserID=45507
这是可以接受的 strUserID=43452
这是可以接受的 strUserID=41524
这是可以接受的 strUserID=45108
这是可以接受的 strUserID=45407
这是可以接受的 strUserID=45573
这是可以接受的 strUserID=45585
这是可以接受的 strUserID=45543
这是可以接受的 strUserID=45554
这是可以接受的 strUserID=45551
这是可以接受的 strUserID=42211
这是可以接受的 strUserID=45560
这是可以接受的 strUserID=45114
这是可以接受的 strUserID=45401
这是可以接受的 strUserID=45697
这是可以接受的 strUserID=45729
这是可以接受的 strUserID=44622
这是可以接受的 strUserID=45544
这是可以接受的 strUserID=45576
这是可以接受的 strUserID=45721
这是可以接受的 strUserID=45741
这是可以接受的 strUserID=45626
这是可以接受的 strUserID=45406
这是可以接受的 strUserID=45733
这是可以接受的 strUserID=43563
这是可以接受的 strUserID=45765
这是可以接受的 strUserID=45770
这是可以接受的 strUserID=43135
这是可以接受的 strUserID=45385
这是可以接受的 strUserID=45439
这是可以接受的 strUserID=45784
这是可以接受的 strUserID=45783
这是可以接受的 strUserID=45803
这是可以接受的 strUserID=45649
这是可以接受的 strUserID=45652
这是可以接受的 strUserID=45384
这是可以接受的 strUserID=45608
这是可以接受的 strUserID=45827
这是可以接受的 strUserID=45816
这是可以接受的 strUserID=45821
这是可以接受的 strUserID=45822
这是可以接受的 strUserID=43768
这是可以接受的 strUserID=44607
这是可以接受的 strUserID=45472
这是可以接受的 strUserID=43085
这是可以接受的 strUserID=45752
这是可以接受的 strUserID=43624
这是可以接受的 strUserID=45807
这是可以接受的 strUserID=45826
这是可以接受的 strUserID=45863
这是可以接受的 strUserID=45823
这是可以接受的 strUserID=44845
这是可以接受的 strUserID=45880
这是可以接受的 strUserID=44920
这是可以接受的 strUserID=45788
这是可以接受的 strUserID=45884
这是可以接受的 strUserID=45842
这是可以接受的 strUserID=45899
这是可以接受的 strUserID=45902
这是可以接受的 strUserID=45850
这是可以接受的 strUserID=45910
这是可以接受的 strUserID=45929
这是可以接受的 strUserID=45846
这是可以接受的 strUserID=45658
这是可以接受的 strUserID=45720
这是可以接受的 strUserID=45801
这是可以接受的 strUserID=44762
这是可以接受的 strUserID=45944
这是可以接受的 strUserID=45885
这是可以接受的 strUserID=45928
这是可以接受的 strUserID=45937
这是可以接受的 strUserID=45940
这是可以接受的 strUserID=45948
这是可以接受的 strUserID=41372
这是可以接受的 strUserID=45855
这是可以接受的 strUserID=44011
这是可以接受的 strUserID=43761
这是可以接受的 strUserID=44309
这是可以接受的 strUserID=46003
这是可以接受的 strUserID=45596
这是可以接受的 strUserID=46007
这是可以接受的 strUserID=45868
这是可以接受的 strUserID=45675
这是可以接受的 strUserID=45972
这是可以接受的 strUserID=43923
这是可以接受的 strUserID=45860
这是可以接受的 strUserID=46029
这是可以接受的 strUserID=46017
这是可以接受的 strUserID=46010
这是可以接受的 strUserID=46032
这是可以接受的 strUserID=42167
这是可以接受的 strUserID=44380
这是可以接受的 strUserID=44721
这是可以接受的 strUserID=46067
这是可以接受的 strUserID=45242
这是可以接受的 strUserID=46054
这是可以接受的 strUserID=45875
这是可以接受的 strUserID=45104
这是可以接受的 strUserID=46161
这是可以接受的 strUserID=46031
这是可以接受的 strUserID=46162
这是可以接受的 strUserID=46151
这是可以接受的 strUserID=46206
这是可以接受的 strUserID=46207
这是可以接受的 strUserID=45983
这是可以接受的 strUserID=45997
这是可以接受的 strUserID=45656
这是可以接受的 strUserID=44207
这是可以接受的 strUserID=46043
这是可以接受的 strUserID=46060
这是可以接受的 strUserID=46156
这是可以接受的 strUserID=46103
这是可以接受的 strUserID=46145
这是可以接受的 strUserID=45245
这是可以接受的 strUserID=46135
这是可以接受的 strUserID=46140
这是可以接受的 strUserID=46185
这是可以接受的 strUserID=43926
这是可以接受的 strUserID=46194
这是可以接受的 strUserID=43151
这是可以接受的 strUserID=46181
这是可以接受的 strUserID=46180
这是可以接受的 strUserID=44581
这是可以接受的 strUserID=46164
这是可以接受的 strUserID=46100
这是可以接受的 strUserID=46155
这是可以接受的 strUserID=46321
这是可以接受的 strUserID=46222
这是可以接受的 strUserID=46218
这是可以接受的 strUserID=46049
这是可以接受的 strUserID=46286
这是可以接受的 strUserID=46131
这是可以接受的 strUserID=46035
这是可以接受的 strUserID=45808
这是可以接受的 strUserID=46271
这是可以接受的 strUserID=46300
这是可以接受的 strUserID=45897
这是可以接受的 strUserID=46276
这是可以接受的 strUserID=45958
这是可以接受的 strUserID=46307
这是可以接受的 strUserID=46299
这是可以接受的 strUserID=43208
这是可以接受的 strUserID=46288
这是可以接受的 strUserID=41415
这是可以接受的 strUserID=46213
这是可以接受的 strUserID=46392
这是可以接受的 strUserID=46267
这是可以接受的 strUserID=46265
这是可以接受的 strUserID=46259
这是可以接受的 strUserID=46315
这是可以接受的 strUserID=46211
这是可以接受的 strUserID=46415
这是可以接受的 strUserID=45494
这是可以接受的 strUserID=44109
这是可以接受的 strUserID=46014
这是可以接受的 strUserID=46071
这是可以接受的 strUserID=46012
这是有问题的 必须进一步观察 strUserID=46365
这是可以接受的 strUserID=46379
这是可以接受的 strUserID=46391
这是可以接受的 strUserID=46406
这是可以接受的 strUserID=46492
这是可以接受的 strUserID=44624
这是可以接受的 strUserID=46412
这是可以接受的 strUserID=46523
这是可以接受的 strUserID=44750
这是可以接受的 strUserID=43918
这是可以接受的 strUserID=46326
这是可以接受的 strUserID=46493
这是可以接受的 strUserID=46311
这是可以接受的 strUserID=46416
这是可以接受的 strUserID=46445
这是可以接受的 strUserID=46027
这是可以接受的 strUserID=46023
这是可以接受的 strUserID=46467
这是可以接受的 strUserID=45968
这是可以接受的 strUserID=46486
这是可以接受的 strUserID=46005
这是可以接受的 strUserID=46015
这是可以接受的 strUserID=46536
这是可以接受的 strUserID=46526
这是可以接受的 strUserID=45769
这是可以接受的 strUserID=46563
这是可以接受的 strUserID=46291
这是可以接受的 strUserID=43459
这是可以接受的 strUserID=46554
这是可以接受的 strUserID=46498
这是可以接受的 strUserID=45988
这是可以接受的 strUserID=46565
这是可以接受的 strUserID=46570
这是可以接受的 strUserID=46556
这是可以接受的 strUserID=46594
这是可以接受的 strUserID=46595
这是可以接受的 strUserID=45280
这是可以接受的 strUserID=46628
这是可以接受的 strUserID=46648
这是可以接受的 strUserID=46375
这是可以接受的 strUserID=46696
这是可以接受的 strUserID=46654
这是可以接受的 strUserID=44230
这是可以接受的 strUserID=44108
这是可以接受的 strUserID=46643
这是可以接受的 strUserID=46642
这是可以接受的 strUserID=46653
这是可以接受的 strUserID=46673
这是可以接受的 strUserID=45657
这是可以接受的 strUserID=46349
这是可以接受的 strUserID=46489
这是可以接受的 strUserID=46138
这是可以接受的 strUserID=42952
这是可以接受的 strUserID=46543
这是可以接受的 strUserID=45978
这是可以接受的 strUserID=42479
这是可以接受的 strUserID=46717
这是可以接受的 strUserID=46724
这是可以接受的 strUserID=46721
这是可以接受的 strUserID=45176
这是可以接受的 strUserID=46726
这是可以接受的 strUserID=46727
这是可以接受的 strUserID=46677
这是可以接受的 strUserID=46756
这是可以接受的 strUserID=45954
这是可以接受的 strUserID=46757
这是可以接受的 strUserID=46792
这是可以接受的 strUserID=46747
这是可以接受的 strUserID=46837
这是可以接受的 strUserID=46847
这是可以接受的 strUserID=46801
这是可以接受的 strUserID=46804
这是可以接受的 strUserID=41419
这是可以接受的 strUserID=46844
这是可以接受的 strUserID=46835
这是可以接受的 strUserID=46063
这是可以接受的 strUserID=43261
这是可以接受的 strUserID=46231
这是可以接受的 strUserID=46640
这是可以接受的 strUserID=46879
这是可以接受的 strUserID=46773
这是可以接受的 strUserID=46903
这是可以接受的 strUserID=46532
这是可以接受的 strUserID=45337
这是可以接受的 strUserID=46783
这是可以接受的 strUserID=43586
这是可以接受的 strUserID=46571
这是可以接受的 strUserID=46871
这是可以接受的 strUserID=46833
这是可以接受的 strUserID=46566
这是可以接受的 strUserID=46894
这是可以接受的 strUserID=46896
这是可以接受的 strUserID=43687
这是可以接受的 strUserID=46919
这是可以接受的 strUserID=46946
这是可以接受的 strUserID=46967
这是可以接受的 strUserID=46968
这是可以接受的 strUserID=46965
这是可以接受的 strUserID=46982
这是可以接受的 strUserID=45736
这是可以接受的 strUserID=46562
这是可以接受的 strUserID=46995
这是可以接受的 strUserID=47051
这是可以接受的 strUserID=47072
这是可以接受的 strUserID=47075
这是可以接受的 strUserID=47081
这是可以接受的 strUserID=46739
这是可以接受的 strUserID=47088
这是可以接受的 strUserID=46970
这是可以接受的 strUserID=46999
这是可以接受的 strUserID=46989
这是可以接受的 strUserID=45663
这是可以接受的 strUserID=46612
这是可以接受的 strUserID=47001
这是可以接受的 strUserID=47006
这是可以接受的 strUserID=47047
这是可以接受的 strUserID=47010
这是可以接受的 strUserID=47049
这是可以接受的 strUserID=45235
这是可以接受的 strUserID=47092
这是可以接受的 strUserID=46553
这是可以接受的 strUserID=46770
这是可以接受的 strUserID=47099
这是可以接受的 strUserID=47111
这是可以接受的 strUserID=47116
这是可以接受的 strUserID=47123
这是可以接受的 strUserID=47098
这是可以接受的 strUserID=47112
这是可以接受的 strUserID=46641
这是可以接受的 strUserID=47085
这是可以接受的 strUserID=46859
这是可以接受的 strUserID=46688
这是可以接受的 strUserID=47157
这是可以接受的 strUserID=47131
这是可以接受的 strUserID=41365
这是可以接受的 strUserID=47198
这是可以接受的 strUserID=47119
这是可以接受的 strUserID=47201
这是可以接受的 strUserID=45887
这是可以接受的 strUserID=47136
这是可以接受的 strUserID=47205
这是可以接受的 strUserID=47219
这是可以接受的 strUserID=47216
这是可以接受的 strUserID=47215
这是可以接受的 strUserID=46795
这是可以接受的 strUserID=46204
这是可以接受的 strUserID=47256
这是可以接受的 strUserID=46929
这是可以接受的 strUserID=47122
这是可以接受的 strUserID=47304
这是可以接受的 strUserID=47325
这是可以接受的 strUserID=45761
这是可以接受的 strUserID=41274
这是可以接受的 strUserID=47195
这是可以接受的 strUserID=47252
这是可以接受的 strUserID=46745
这是可以接受的 strUserID=46491
这是可以接受的 strUserID=47312
这是可以接受的 strUserID=44323
这是可以接受的 strUserID=47153
这是可以接受的 strUserID=47017
这是可以接受的 strUserID=47184
这是可以接受的 strUserID=47310
这是可以接受的 strUserID=47370
这是可以接受的 strUserID=47269
这是可以接受的 strUserID=47401
这是可以接受的 strUserID=44687
这是可以接受的 strUserID=46980
这是可以接受的 strUserID=46081
这是可以接受的 strUserID=47306
这是可以接受的 strUserID=47217
这是可以接受的 strUserID=45279
这是可以接受的 strUserID=45867
这是可以接受的 strUserID=47346
这是可以接受的 strUserID=47347
这是可以接受的 strUserID=47355
这是可以接受的 strUserID=46977
这是可以接受的 strUserID=45960
这是可以接受的 strUserID=47404
这是可以接受的 strUserID=47379
这是可以接受的 strUserID=43175
这是可以接受的 strUserID=47282
这是可以接受的 strUserID=47397
这是可以接受的 strUserID=47095
这是可以接受的 strUserID=47436
这是可以接受的 strUserID=43582
这是可以接受的 strUserID=47452
这是可以接受的 strUserID=47483
这是可以接受的 strUserID=45029
这是可以接受的 strUserID=47508
这是可以接受的 strUserID=47255
这是可以接受的 strUserID=47220
这是可以接受的 strUserID=47259
这是可以接受的 strUserID=47572
这是可以接受的 strUserID=47413
这是可以接受的 strUserID=47509
这是可以接受的 strUserID=43983
这是可以接受的 strUserID=46530
这是可以接受的 strUserID=47684
这是可以接受的 strUserID=46167
这是可以接受的 strUserID=45503
这是可以接受的 strUserID=46703
这是可以接受的 strUserID=47726
这是可以接受的 strUserID=44181
这是可以接受的 strUserID=47843
这是可以接受的 strUserID=47810
这是可以接受的 strUserID=47725
这是可以接受的 strUserID=47899
这是可以接受的 strUserID=47926
这是可以接受的 strUserID=47937
这是可以接受的 strUserID=48002
这是可以接受的 strUserID=47496
这是可以接受的 strUserID=48029
这是可以接受的 strUserID=45909
这是可以接受的 strUserID=47667
这是可以接受的 strUserID=47705
这是可以接受的 strUserID=47691
这是可以接受的 strUserID=44680
这是可以接受的 strUserID=47740
这是可以接受的 strUserID=47728
这是可以接受的 strUserID=47776
这是可以接受的 strUserID=44677
这是可以接受的 strUserID=47590
这是可以接受的 strUserID=47752
这是可以接受的 strUserID=48072
这是可以接受的 strUserID=46856
这是可以接受的 strUserID=47774
这是可以接受的 strUserID=47772
这是可以接受的 strUserID=47424
这是可以接受的 strUserID=43595
这是可以接受的 strUserID=47822
这是可以接受的 strUserID=46350
这是可以接受的 strUserID=47832
这是可以接受的 strUserID=47813
这是可以接受的 strUserID=44841
这是可以接受的 strUserID=47849
这是可以接受的 strUserID=47851
这是可以接受的 strUserID=47914
这是可以接受的 strUserID=47901
这是可以接受的 strUserID=47527
这是可以接受的 strUserID=47917
这是可以接受的 strUserID=47921
这是可以接受的 strUserID=47948
这是可以接受的 strUserID=47685
这是可以接受的 strUserID=47840
这是可以接受的 strUserID=46922
这是可以接受的 strUserID=44632
这是可以接受的 strUserID=47799
这是可以接受的 strUserID=46777
这是可以接受的 strUserID=41229
这是可以接受的 strUserID=47998
这是可以接受的 strUserID=47671
这是可以接受的 strUserID=47570
这是可以接受的 strUserID=43351
这是可以接受的 strUserID=47888
这是可以接受的 strUserID=48070
这是可以接受的 strUserID=47876
这是可以接受的 strUserID=47941
这是可以接受的 strUserID=47872
这是可以接受的 strUserID=44943
这是可以接受的 strUserID=47875
这是可以接受的 strUserID=48027
这是可以接受的 strUserID=47946
这是可以接受的 strUserID=41426
这是可以接受的 strUserID=48034
这是可以接受的 strUserID=48042
这是可以接受的 strUserID=48121
这是有问题的用户 删除才正常 删除他们 strUserID=48094
这是有问题的用户 删除才正常 删除他们 strUserID=48099
这是可以接受的 strUserID=48126
这是可以接受的 strUserID=48044
这是可以接受的 strUserID=48102
这是可以接受的 strUserID=48101
这是可以接受的 strUserID=48135
这是有问题的用户 删除才正常 删除他们 strUserID=48091
这是有问题的用户 删除才正常 删除他们 strUserID=48092
这是有问题的用户 删除才正常 删除他们 strUserID=48096
这是有问题的用户 删除才正常 删除他们 strUserID=48098
这是有问题的用户 删除才正常 删除他们 strUserID=44387
这是有问题的用户 删除才正常 删除他们 strUserID=47428
这是有问题的用户 删除才正常 删除他们 strUserID=48093
这是有问题的用户 删除才正常 删除他们 strUserID=46765
这是可以接受的 strUserID=47375
这是可以接受的 strUserID=47974
这是可以接受的 strUserID=48085
这是可以接受的 strUserID=47423
这是可以接受的 strUserID=48049
这是可以接受的 strUserID=48069
这是可以接受的 strUserID=48226
这是可以接受的 strUserID=48109
这是可以接受的 strUserID=48089
这是可以接受的 strUserID=48140
这是可以接受的 strUserID=46269
这是可以接受的 strUserID=41399
这是可以接受的 strUserID=48118
这是可以接受的 strUserID=48313
这是可以接受的 strUserID=48249
这是可以接受的 strUserID=48338
这是可以接受的 strUserID=48132
这是可以接受的 strUserID=48174
这是可以接受的 strUserID=48123
这是可以接受的 strUserID=48186
这是可以接受的 strUserID=48227
这是可以接受的 strUserID=48238
这是可以接受的 strUserID=48179
这是可以接受的 strUserID=46487
这是可以接受的 strUserID=48194
这是可以接受的 strUserID=47834
这是可以接受的 strUserID=48387
这是可以接受的 strUserID=48439
这是可以接受的 strUserID=44667
这是可以接受的 strUserID=46126
这是可以接受的 strUserID=48407
这是可以接受的 strUserID=44869
这是可以接受的 strUserID=47254
这是可以接受的 strUserID=48315
这是可以接受的 strUserID=48054
这是可以接受的 strUserID=48511
这是可以接受的 strUserID=48473
这是可以接受的 strUserID=48553
这是可以接受的 strUserID=47826
这是可以接受的 strUserID=48360
这是可以接受的 strUserID=48362
这是可以接受的 strUserID=48397
这是可以接受的 strUserID=48411
这是可以接受的 strUserID=48409
这是可以接受的 strUserID=48463
这是可以接受的 strUserID=48376
这是可以接受的 strUserID=48404
这是可以接受的 strUserID=48406
这是可以接受的 strUserID=47959
这是可以接受的 strUserID=48634
这是可以接受的 strUserID=48642
这是可以接受的 strUserID=47531
这是可以接受的 strUserID=46891
这是可以接受的 strUserID=48680
这是可以接受的 strUserID=48648
这是可以接受的 strUserID=48722
这是可以接受的 strUserID=48460
这是可以接受的 strUserID=48430
这是可以接受的 strUserID=48425
这是可以接受的 strUserID=46807
这是可以接受的 strUserID=48232
这是可以接受的 strUserID=48456
这是可以接受的 strUserID=48500
这是可以接受的 strUserID=48528
这是可以接受的 strUserID=48506
这是可以接受的 strUserID=48530
这是可以接受的 strUserID=48410
这是可以接受的 strUserID=48537
这是可以接受的 strUserID=45200
这是可以接受的 strUserID=48566
这是可以接受的 strUserID=48761
这是可以接受的 strUserID=48772
这是可以接受的 strUserID=48308
这是可以接受的 strUserID=48288
这是可以接受的 strUserID=48429
这是可以接受的 strUserID=48363
这是可以接受的 strUserID=48575
这是可以接受的 strUserID=48442
这是可以接受的 strUserID=47603
这是可以接受的 strUserID=48517
这是可以接受的 strUserID=44781
这是可以接受的 strUserID=48479
这是可以接受的 strUserID=47445
这是可以接受的 strUserID=48715
这是可以接受的 strUserID=48716
这是可以接受的 strUserID=48538
这是可以接受的 strUserID=48639
这是可以接受的 strUserID=48625
这是可以接受的 strUserID=48578
这是可以接受的 strUserID=46809
这是可以接受的 strUserID=48599
这是可以接受的 strUserID=48710
这是可以接受的 strUserID=48620
这是可以接受的 strUserID=46351
这是可以接受的 strUserID=46731
这是可以接受的 strUserID=48155
这是可以接受的 strUserID=47202
这是可以接受的 strUserID=48602
这是可以接受的 strUserID=48603
这是可以接受的 strUserID=43980
这是可以接受的 strUserID=48144
这是可以接受的 strUserID=47784
这是可以接受的 strUserID=48646
这是可以接受的 strUserID=48691
这是可以接受的 strUserID=47477
这是可以接受的 strUserID=48619
这是可以接受的 strUserID=48721
这是可以接受的 strUserID=48688
这是可以接受的 strUserID=46141
这是可以接受的 strUserID=43830
这是可以接受的 strUserID=47623
这是可以接受的 strUserID=48678
这是可以接受的 strUserID=48690
这是可以接受的 strUserID=48615
这是可以接受的 strUserID=48444
这是可以接受的 strUserID=45962
这是可以接受的 strUserID=48427
这是可以接受的 strUserID=48757
这是可以接受的 strUserID=48647
这是可以接受的 strUserID=48763
这是可以接受的 strUserID=48459
这是可以接受的 strUserID=48784
这是可以接受的 strUserID=48843
这是可以接受的 strUserID=48859
这是可以接受的 strUserID=48906
这是可以接受的 strUserID=48920
这是可以接受的 strUserID=48621
这是可以接受的 strUserID=48775
这是可以接受的 strUserID=48782
这是可以接受的 strUserID=48841
这是可以接受的 strUserID=48967
这是可以接受的 strUserID=47106
这是可以接受的 strUserID=48800
这是可以接受的 strUserID=48708
这是可以接受的 strUserID=47102
这是可以接受的 strUserID=48868
这是可以接受的 strUserID=48882
这是可以接受的 strUserID=47911
这是可以接受的 strUserID=48869
这是可以接受的 strUserID=48842
这是可以接受的 strUserID=45107
这是可以接受的 strUserID=48883
这是可以接受的 strUserID=48877
这是可以接受的 strUserID=48867
这是可以接受的 strUserID=48909
这是可以接受的 strUserID=48934
这是可以接受的 strUserID=48885
这是可以接受的 strUserID=48888
这是可以接受的 strUserID=48774
这是可以接受的 strUserID=48901
这是可以接受的 strUserID=48954
这是可以接受的 strUserID=48955
这是可以接受的 strUserID=48915
这是可以接受的 strUserID=49026
这是可以接受的 strUserID=47689
这是可以接受的 strUserID=48932
这是可以接受的 strUserID=48985
这是可以接受的 strUserID=48986
这是可以接受的 strUserID=49034
这是可以接受的 strUserID=44908
这是可以接受的 strUserID=48484
这是可以接受的 strUserID=48476
这是可以接受的 strUserID=48593
这是可以接受的 strUserID=48534
这是可以接受的 strUserID=48912
这是可以接受的 strUserID=48998
这是可以接受的 strUserID=49032
这是可以接受的 strUserID=49069
这是可以接受的 strUserID=46289
这是可以接受的 strUserID=47722
这是可以接受的 strUserID=49196
这是可以接受的 strUserID=48983
这是可以接受的 strUserID=49107
这是可以接受的 strUserID=43975
这是可以接受的 strUserID=49152
这是可以接受的 strUserID=49007
这是可以接受的 strUserID=49087
这是可以接受的 strUserID=49166
这是可以接受的 strUserID=49165
这是可以接受的 strUserID=49157
这是可以接受的 strUserID=49158
这是可以接受的 strUserID=49159
这是可以接受的 strUserID=47110
这是可以接受的 strUserID=48966
这是可以接受的 strUserID=49188
这是可以接受的 strUserID=49051
这是可以接受的 strUserID=49217
这是可以接受的 strUserID=49219
这是可以接受的 strUserID=48981
这是可以接受的 strUserID=45449
这是可以接受的 strUserID=49214
这是可以接受的 strUserID=49284
这是可以接受的 strUserID=49331
这是可以接受的 strUserID=47980
这是可以接受的 strUserID=49249
这是可以接受的 strUserID=49392
这是可以接受的 strUserID=49394
这是可以接受的 strUserID=46505
这是可以接受的 strUserID=49472
这是可以接受的 strUserID=42487
这是可以接受的 strUserID=49224
这是可以接受的 strUserID=48773
这是可以接受的 strUserID=48635
这是可以接受的 strUserID=49277
这是可以接受的 strUserID=49038
这是可以接受的 strUserID=45105
这是可以接受的 strUserID=48508
这是可以接受的 strUserID=49041
这是可以接受的 strUserID=49319
这是可以接受的 strUserID=49320
这是可以接受的 strUserID=49198
这是可以接受的 strUserID=49287
这是可以接受的 strUserID=46228
这是可以接受的 strUserID=48940
这是可以接受的 strUserID=49292
这是可以接受的 strUserID=49408
这是可以接受的 strUserID=49404
这是可以接受的 strUserID=49405
这是可以接受的 strUserID=49204
这是可以接受的 strUserID=49346
这是可以接受的 strUserID=49397
这是可以接受的 strUserID=49398
这是可以接受的 strUserID=48958
这是可以接受的 strUserID=49401
这是可以接受的 strUserID=49474
这是可以接受的 strUserID=49432
这是可以接受的 strUserID=48976
这是可以接受的 strUserID=49555
这是可以接受的 strUserID=49113
这是可以接受的 strUserID=48803
这是可以接受的 strUserID=48489
这是可以接受的 strUserID=47891
这是可以接受的 strUserID=49046
这是可以接受的 strUserID=47089
这是可以接受的 strUserID=49411
这是可以接受的 strUserID=49412
这是可以接受的 strUserID=49419
这是可以接受的 strUserID=49386
这是可以接受的 strUserID=49483
这是可以接受的 strUserID=49036
这是可以接受的 strUserID=49550
这是可以接受的 strUserID=49535
这是可以接受的 strUserID=49536
这是可以接受的 strUserID=49546
这是可以接受的 strUserID=49566
这是可以接受的 strUserID=49565
这是可以接受的 strUserID=49571
这是可以接受的 strUserID=49545
这是可以接受的 strUserID=49229
这是可以接受的 strUserID=49414
这是可以接受的 strUserID=49458
这是可以接受的 strUserID=49327
这是可以接受的 strUserID=47057
这是可以接受的 strUserID=49010
这是可以接受的 strUserID=49459
这是可以接受的 strUserID=49184
这是可以接受的 strUserID=49303
这是可以接受的 strUserID=49306
这是可以接受的 strUserID=46469
这是可以接受的 strUserID=49473
这是可以接受的 strUserID=49647
这是可以接受的 strUserID=49692
这是可以接受的 strUserID=49029
这是可以接受的 strUserID=49599
这是可以接受的 strUserID=49606
这是可以接受的 strUserID=49614
这是可以接受的 strUserID=49612
这是可以接受的 strUserID=49629
这是可以接受的 strUserID=46341
这是可以接受的 strUserID=49421
这是可以接受的 strUserID=49720
这是可以接受的 strUserID=49728
这是可以接受的 strUserID=48630
这是可以接受的 strUserID=49285
这是可以接受的 strUserID=49286
这是可以接受的 strUserID=49431
这是可以接受的 strUserID=49429
这是可以接受的 strUserID=49433
这是可以接受的 strUserID=49753
这是可以接受的 strUserID=47857
这是可以接受的 strUserID=49810
这是可以接受的 strUserID=49722
这是可以接受的 strUserID=49743
这是可以接受的 strUserID=49734
这是可以接受的 strUserID=49730
这是可以接受的 strUserID=49785
这是可以接受的 strUserID=49744
这是可以接受的 strUserID=49805
这是可以接受的 strUserID=49687
这是可以接受的 strUserID=49427
订单配对正常 可以研究出局情况 strUserID=41199
这是有问题的用户 保存他们 等待投诉吧 strUserID=41205
订单配对正常 可以研究出局情况 strUserID=41205
这是有问题的用户 保存他们 等待投诉吧 strUserID=41210
订单配对正常 可以研究出局情况 strUserID=41210
这是有问题的用户 保存他们 等待投诉吧 strUserID=41230
这是有问题的用户 保存他们 等待投诉吧 strUserID=41230
这是有问题的用户 保存他们 等待投诉吧 strUserID=41230
这是有问题的用户 保存他们 等待投诉吧 strUserID=41230
这是有问题的用户 保存他们 等待投诉吧 strUserID=41230
订单配对正常 可以研究出局情况 strUserID=41230
订单配对正常 可以研究出局情况 strUserID=41263
这是有问题的用户 保存他们 等待投诉吧 strUserID=41296
订单配对正常 可以研究出局情况 strUserID=41296
这是有问题的用户 保存他们 等待投诉吧 strUserID=42147
订单配对正常 可以研究出局情况 strUserID=42147
订单配对正常 可以研究出局情况 strUserID=431132017/9/24 23:42:38do over
*/
            #endregion

        }

        /// <summary>
        /// 接轨活动订单数 。如果 有出局的 让订单也出局
        /// </summary>
        private void _009ResetTo008_step9()
        {
            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLLb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            System.Data.DataTable Data_DataTableIDADD = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetList("*", "orderid is null and outHadGivedUserNum is not null and ShopClient_ID=21 order by id").Tables[0];
            for (int i = 0; i < Data_DataTableIDADD.Rows.Count; i++)
            {
                string stroutHadGiveUserNum = Data_DataTableIDADD.Rows[i]["outHadGivedUserNum"].ToString();
                string strUserID = Data_DataTableIDADD.Rows[i]["UserID"].ToString();
                string strActiveOrderNum = Data_DataTableIDADD.Rows[i]["ActiveOrderNum"].ToString();
                int intMemoryCount = 0;
                System.Data.DataTable myCheckData_DataTableIDADD = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetList("*", "userID=" + strUserID + " and orderid is not null and outHadGivedUserNum is null and ShopClient_ID=21 order by ID asc").Tables[0];
                for (int j = 0; j < myCheckData_DataTableIDADD.Rows.Count; j++)
                {
                    string strCurID = myCheckData_DataTableIDADD.Rows[j]["ID"].ToString();
                    string strCurActiveOrderNum = myCheckData_DataTableIDADD.Rows[j]["ActiveOrderNum"].ToString();
                    intMemoryCount += strCurActiveOrderNum.toInt32();
                    if (intMemoryCount <= stroutHadGiveUserNum.toInt32())
                    {
                        if (strActiveOrderNum.toInt32() == 0)
                        {////这种的 不需要补税

                        }
                        else
                        {
                            string strOut = "<br />这是有问题的用户 保存他们 等待投诉吧 strUserID=" + strUserID;
                            Response.Write(strOut);
                        }
                        string stringUpdate = "update [b008_OpterationUserActiveReturnMoneyOrderNum] set ActiveOrderNum=0,outHadGivedUserNum=" + strCurActiveOrderNum + " where id=" + strCurID;
                        EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(stringUpdate);
                    }
                    else
                    {
                        break;
                    }
                }


                string strExsitCountSQL = "select sum(ActiveOrderNum) from [b008_OpterationUserActiveReturnMoneyOrderNum] where userID=" + strUserID + " and orderID is not null";
                int intOrderNum = BLLb008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strExsitCountSQL).Tables[0].Rows[0][0].toInt32();
                if (strActiveOrderNum.toInt32() != intOrderNum)
                {
                    string strOut = "<br />订单出局配对错误 strUserID=" + strUserID;
                    Response.Write(strOut);
                }
                else
                {
                    string strOut = "<br />订单配对正常 可以研究出局情况 strUserID=" + strUserID;
                    Response.Write(strOut);
                }

                #region 
                /*
                2017 / 9 / 30 20:56:36
这是可以接受的 strUserID = 41230
这是可以接受的 strUserID = 41210
这是可以接受的 strUserID = 41263
这是可以接受的 strUserID = 41199
这是可以接受的 strUserID = 41296
这是可以接受的 strUserID = 41205
这是可以接受的 strUserID = 42147
这是可以接受的 strUserID = 41595
这是可以接受的 strUserID = 42577
这是可以接受的 strUserID = 41522
这是可以接受的 strUserID = 41295
这是可以接受的 strUserID = 41434
这是可以接受的 strUserID = 42781
这是可以接受的 strUserID = 41324
这是可以接受的 strUserID = 41256
这是可以接受的 strUserID = 42570
这是可以接受的 strUserID = 42642
这是可以接受的 strUserID = 41833
这是可以接受的 strUserID = 42583
这是可以接受的 strUserID = 43108
这是可以接受的 strUserID = 43123
这是可以接受的 strUserID = 42780
这是可以接受的 strUserID = 41220
这是可以接受的 strUserID = 42989
这是可以接受的 strUserID = 42977
这是可以接受的 strUserID = 43153
这是可以接受的 strUserID = 43177
这是可以接受的 strUserID = 41257
这是可以接受的 strUserID = 43131
这是可以接受的 strUserID = 43132
这是可以接受的 strUserID = 43113
这是可以接受的 strUserID = 43117
这是可以接受的 strUserID = 43140
这是可以接受的 strUserID = 43145
这是可以接受的 strUserID = 43111
这是可以接受的 strUserID = 43142
这是可以接受的 strUserID = 43136
这是可以接受的 strUserID = 42985
这是可以接受的 strUserID = 43157
这是可以接受的 strUserID = 43141
这是可以接受的 strUserID = 43126
这是可以接受的 strUserID = 43077
这是可以接受的 strUserID = 43161
这是可以接受的 strUserID = 43137
这是可以接受的 strUserID = 41306
这是可以接受的 strUserID = 43072
这是可以接受的 strUserID = 43112
这是可以接受的 strUserID = 43114
这是可以接受的 strUserID = 43119
这是可以接受的 strUserID = 43165
这是可以接受的 strUserID = 43169
这是可以接受的 strUserID = 43162
这是可以接受的 strUserID = 43127
这是可以接受的 strUserID = 43122
这是可以接受的 strUserID = 43120
这是可以接受的 strUserID = 43146
这是可以接受的 strUserID = 43155
这是可以接受的 strUserID = 43133
这是可以接受的 strUserID = 43143
这是可以接受的 strUserID = 43181
这是可以接受的 strUserID = 43179
这是可以接受的 strUserID = 43019
这是可以接受的 strUserID = 43158
这是可以接受的 strUserID = 43156
这是可以接受的 strUserID = 43160
这是可以接受的 strUserID = 43200
这是可以接受的 strUserID = 43182
这是可以接受的 strUserID = 43116
这是可以接受的 strUserID = 43185
这是可以接受的 strUserID = 43144
这是可以接受的 strUserID = 43082
这是可以接受的 strUserID = 43212
这是可以接受的 strUserID = 43215
这是可以接受的 strUserID = 43209
这是可以接受的 strUserID = 43149
这是可以接受的 strUserID = 43213
这是可以接受的 strUserID = 43211
这是可以接受的 strUserID = 43134
这是可以接受的 strUserID = 43110
这是可以接受的 strUserID = 43173
这是可以接受的 strUserID = 43163
这是可以接受的 strUserID = 43183
这是可以接受的 strUserID = 43154
这是可以接受的 strUserID = 43178
这是可以接受的 strUserID = 43118
这是可以接受的 strUserID = 43124
这是可以接受的 strUserID = 43138
这是可以接受的 strUserID = 43139
这是可以接受的 strUserID = 43226
这是可以接受的 strUserID = 43105
这是可以接受的 strUserID = 43216
这是可以接受的 strUserID = 42448
这是可以接受的 strUserID = 43168
这是可以接受的 strUserID = 43042
这是可以接受的 strUserID = 43221
这是可以接受的 strUserID = 43206
这是可以接受的 strUserID = 43086
这是可以接受的 strUserID = 43232
这是可以接受的 strUserID = 43240
这是可以接受的 strUserID = 43125
这是可以接受的 strUserID = 43214
这是可以接受的 strUserID = 43167
这是可以接受的 strUserID = 43210
这是可以接受的 strUserID = 43159
这是可以接受的 strUserID = 43203
这是可以接受的 strUserID = 43164
这是可以接受的 strUserID = 42674
这是可以接受的 strUserID = 41192
这是可以接受的 strUserID = 43310
这是可以接受的 strUserID = 43204
这是可以接受的 strUserID = 43278
这是可以接受的 strUserID = 43239
这是可以接受的 strUserID = 43220
这是可以接受的 strUserID = 43236
这是可以接受的 strUserID = 43150
这是可以接受的 strUserID = 43296
这是可以接受的 strUserID = 43268
这是可以接受的 strUserID = 43260
这是可以接受的 strUserID = 43274
这是可以接受的 strUserID = 43270
这是可以接受的 strUserID = 42975
这是可以接受的 strUserID = 43430
这是可以接受的 strUserID = 43255
这是可以接受的 strUserID = 41977
这是可以接受的 strUserID = 43346
这是可以接受的 strUserID = 43099
这是可以接受的 strUserID = 43295
这是可以接受的 strUserID = 43307
这是可以接受的 strUserID = 43333
这是可以接受的 strUserID = 43329
这是可以接受的 strUserID = 42768
这是可以接受的 strUserID = 43319
这是可以接受的 strUserID = 43277
这是可以接受的 strUserID = 43279
这是可以接受的 strUserID = 43365
这是可以接受的 strUserID = 43276
这是可以接受的 strUserID = 43272
这是可以接受的 strUserID = 43345
这是可以接受的 strUserID = 43352
这是可以接受的 strUserID = 43338
这是可以接受的 strUserID = 43327
这是可以接受的 strUserID = 43322
这是可以接受的 strUserID = 43281
这是可以接受的 strUserID = 43323
这是可以接受的 strUserID = 43324
这是可以接受的 strUserID = 43337
这是可以接受的 strUserID = 43326
这是可以接受的 strUserID = 43362
这是可以接受的 strUserID = 43381
这是可以接受的 strUserID = 43382
这是可以接受的 strUserID = 43372
这是可以接受的 strUserID = 43410
这是可以接受的 strUserID = 43422
这是可以接受的 strUserID = 43288
这是可以接受的 strUserID = 43612
这是可以接受的 strUserID = 43432
这是可以接受的 strUserID = 42767
这是可以接受的 strUserID = 43614
这是可以接受的 strUserID = 43618
这是可以接受的 strUserID = 41526
这是可以接受的 strUserID = 43444
这是可以接受的 strUserID = 43464
这是可以接受的 strUserID = 43441
这是可以接受的 strUserID = 43301
这是可以接受的 strUserID = 43152
这是可以接受的 strUserID = 43355
这是可以接受的 strUserID = 43621
这是可以接受的 strUserID = 43596
这是可以接受的 strUserID = 43599
这是可以接受的 strUserID = 43592
这是可以接受的 strUserID = 43562
这是可以接受的 strUserID = 43561
这是可以接受的 strUserID = 43540
这是可以接受的 strUserID = 43570
这是可以接受的 strUserID = 43471
这是可以接受的 strUserID = 43787
这是可以接受的 strUserID = 43829
这是可以接受的 strUserID = 43558
这是可以接受的 strUserID = 43607
这是可以接受的 strUserID = 43619
这是可以接受的 strUserID = 43660
这是可以接受的 strUserID = 43664
这是可以接受的 strUserID = 43584
这是可以接受的 strUserID = 43661
这是可以接受的 strUserID = 43668
这是可以接受的 strUserID = 43733
这是可以接受的 strUserID = 43581
这是可以接受的 strUserID = 43632
这是可以接受的 strUserID = 43710
这是可以接受的 strUserID = 43766
这是可以接受的 strUserID = 43817
这是可以接受的 strUserID = 43735
这是可以接受的 strUserID = 43828
这是可以接受的 strUserID = 43704
这是可以接受的 strUserID = 43907
这是可以接受的 strUserID = 43594
这是可以接受的 strUserID = 43895
这是可以接受的 strUserID = 42957
这是可以接受的 strUserID = 43217
这是可以接受的 strUserID = 43650
这是可以接受的 strUserID = 43949
这是可以接受的 strUserID = 43908
这是可以接受的 strUserID = 43380
这是可以接受的 strUserID = 43421
这是可以接受的 strUserID = 43899
这是可以接受的 strUserID = 43869
这是可以接受的 strUserID = 43290
这是可以接受的 strUserID = 43904
这是可以接受的 strUserID = 43826
这是可以接受的 strUserID = 43915
这是可以接受的 strUserID = 43950
这是可以接受的 strUserID = 43984
这是可以接受的 strUserID = 43640
这是可以接受的 strUserID = 43978
这是可以接受的 strUserID = 44004
这是可以接受的 strUserID = 43385
这是可以接受的 strUserID = 43994
这是可以接受的 strUserID = 43972
这是可以接受的 strUserID = 44002
这是可以接受的 strUserID = 43447
这是可以接受的 strUserID = 43325
这是可以接受的 strUserID = 43863
这是可以接受的 strUserID = 44065
这是可以接受的 strUserID = 44079
这是可以接受的 strUserID = 44075
这是可以接受的 strUserID = 44058
这是可以接受的 strUserID = 44082
这是可以接受的 strUserID = 44084
这是可以接受的 strUserID = 44155
这是可以接受的 strUserID = 43649
这是可以接受的 strUserID = 44121
这是可以接受的 strUserID = 43330
这是可以接受的 strUserID = 44110
这是可以接受的 strUserID = 43331
这是可以接受的 strUserID = 43665
这是可以接受的 strUserID = 44183
这是可以接受的 strUserID = 44133
这是可以接受的 strUserID = 44191
这是可以接受的 strUserID = 44100
这是可以接受的 strUserID = 43988
这是可以接受的 strUserID = 44219
这是可以接受的 strUserID = 44283
这是可以接受的 strUserID = 44240
这是可以接受的 strUserID = 44290
这是可以接受的 strUserID = 44292
这是可以接受的 strUserID = 44105
这是可以接受的 strUserID = 44266
这是可以接受的 strUserID = 44308
这是可以接受的 strUserID = 44334
这是可以接受的 strUserID = 44087
这是可以接受的 strUserID = 44332
这是可以接受的 strUserID = 43802
这是可以接受的 strUserID = 44403
这是可以接受的 strUserID = 44248
这是可以接受的 strUserID = 43347
这是可以接受的 strUserID = 44433
这是可以接受的 strUserID = 42906
这是可以接受的 strUserID = 44448
这是可以接受的 strUserID = 44494
这是可以接受的 strUserID = 43579
这是可以接受的 strUserID = 44474
这是可以接受的 strUserID = 44170
这是可以接受的 strUserID = 44431
这是可以接受的 strUserID = 44475
这是可以接受的 strUserID = 41310
这是可以接受的 strUserID = 44103
这是可以接受的 strUserID = 44455
这是可以接受的 strUserID = 41774
这是可以接受的 strUserID = 44496
这是可以接受的 strUserID = 43686
这是可以接受的 strUserID = 44316
这是可以接受的 strUserID = 44346
这是可以接受的 strUserID = 44405
这是可以接受的 strUserID = 44567
这是可以接受的 strUserID = 44253
这是可以接受的 strUserID = 43413
这是可以接受的 strUserID = 44596
这是可以接受的 strUserID = 43539
这是可以接受的 strUserID = 43400
这是可以接受的 strUserID = 44646
这是可以接受的 strUserID = 44623
这是可以接受的 strUserID = 43837
这是可以接受的 strUserID = 44481
这是可以接受的 strUserID = 44499
这是可以接受的 strUserID = 44525
这是可以接受的 strUserID = 43792
这是可以接受的 strUserID = 44647
这是可以接受的 strUserID = 44520
这是可以接受的 strUserID = 44592
这是可以接受的 strUserID = 44556
这是可以接受的 strUserID = 44563
这是可以接受的 strUserID = 44564
这是可以接受的 strUserID = 44559
这是可以接受的 strUserID = 44554
这是可以接受的 strUserID = 44660
这是可以接受的 strUserID = 44662
这是可以接受的 strUserID = 43469
这是可以接受的 strUserID = 44051
这是可以接受的 strUserID = 44642
这是可以接受的 strUserID = 44631
这是可以接受的 strUserID = 41905
这是可以接受的 strUserID = 44692
这是可以接受的 strUserID = 44689
这是可以接受的 strUserID = 44549
这是可以接受的 strUserID = 44690
这是可以接受的 strUserID = 44698
这是可以接受的 strUserID = 44711
这是可以接受的 strUserID = 44714
这是可以接受的 strUserID = 44658
这是可以接受的 strUserID = 44691
这是可以接受的 strUserID = 44709
这是可以接受的 strUserID = 44715
这是可以接受的 strUserID = 43697
这是可以接受的 strUserID = 44635
这是可以接受的 strUserID = 44605
这是可以接受的 strUserID = 44171
这是可以接受的 strUserID = 44733
这是可以接受的 strUserID = 44688
这是可以接受的 strUserID = 44264
这是可以接受的 strUserID = 44665
这是可以接受的 strUserID = 44418
这是可以接受的 strUserID = 44717
这是可以接受的 strUserID = 44620
这是可以接受的 strUserID = 44911
这是可以接受的 strUserID = 44735
这是可以接受的 strUserID = 44723
这是可以接受的 strUserID = 44728
这是可以接受的 strUserID = 44736
这是可以接受的 strUserID = 44341
这是可以接受的 strUserID = 44738
这是可以接受的 strUserID = 44739
这是可以接受的 strUserID = 43456
这是可以接受的 strUserID = 44847
这是可以接受的 strUserID = 44849
这是可以接受的 strUserID = 44886
这是可以接受的 strUserID = 44601
这是可以接受的 strUserID = 44541
这是可以接受的 strUserID = 44756
这是可以接受的 strUserID = 44786
这是可以接受的 strUserID = 44773
这是可以接受的 strUserID = 44676
这是可以接受的 strUserID = 44828
这是可以接受的 strUserID = 44825
这是可以接受的 strUserID = 44095
这是可以接受的 strUserID = 44864
这是可以接受的 strUserID = 44866
这是可以接受的 strUserID = 43971
这是可以接受的 strUserID = 44447
这是可以接受的 strUserID = 43683
这是可以接受的 strUserID = 45066
这是可以接受的 strUserID = 43107
这是可以接受的 strUserID = 44720
这是可以接受的 strUserID = 44874
这是可以接受的 strUserID = 44939
这是可以接受的 strUserID = 44950
这是可以接受的 strUserID = 44970
这是可以接受的 strUserID = 44965
这是可以接受的 strUserID = 43591
这是可以接受的 strUserID = 45095
这是可以接受的 strUserID = 41506
这是可以接受的 strUserID = 44930
这是可以接受的 strUserID = 45028
这是可以接受的 strUserID = 45054
这是可以接受的 strUserID = 45046
这是可以接受的 strUserID = 44944
这是可以接受的 strUserID = 45084
这是可以接受的 strUserID = 45092
这是可以接受的 strUserID = 43653
这是可以接受的 strUserID = 44261
这是可以接受的 strUserID = 45117
这是可以接受的 strUserID = 45129
这是可以接受的 strUserID = 44539
这是可以接受的 strUserID = 44973
这是可以接受的 strUserID = 44288
这是可以接受的 strUserID = 41398
这是可以接受的 strUserID = 45136
这是可以接受的 strUserID = 45106
这是可以接受的 strUserID = 45156
这是可以接受的 strUserID = 41411
这是可以接受的 strUserID = 45053
这是可以接受的 strUserID = 45140
这是可以接受的 strUserID = 44971
这是可以接受的 strUserID = 44990
这是可以接受的 strUserID = 45075
这是可以接受的 strUserID = 45044
这是可以接受的 strUserID = 45027
这是可以接受的 strUserID = 45026
这是可以接受的 strUserID = 45018
这是可以接受的 strUserID = 45072
这是可以接受的 strUserID = 45138
这是可以接受的 strUserID = 45174
这是可以接受的 strUserID = 45111
这是可以接受的 strUserID = 45192
这是可以接受的 strUserID = 45190
这是可以接受的 strUserID = 45199
这是可以接受的 strUserID = 45170
这是可以接受的 strUserID = 45130
这是可以接受的 strUserID = 45222
这是可以接受的 strUserID = 43280
这是可以接受的 strUserID = 45208
这是可以接受的 strUserID = 43833
这是可以接受的 strUserID = 45226
这是可以接受的 strUserID = 43737
这是可以接受的 strUserID = 43412
这是可以接受的 strUserID = 45252
这是可以接受的 strUserID = 45244
这是可以接受的 strUserID = 44838
这是可以接受的 strUserID = 45001
这是可以接受的 strUserID = 44801
这是可以接受的 strUserID = 45265
这是可以接受的 strUserID = 45272
这是可以接受的 strUserID = 44027
这是可以接受的 strUserID = 45296
这是可以接受的 strUserID = 45307
这是可以接受的 strUserID = 45341
这是可以接受的 strUserID = 43608
这是可以接受的 strUserID = 44161
这是可以接受的 strUserID = 45364
这是可以接受的 strUserID = 45096
这是可以接受的 strUserID = 45253
这是可以接受的 strUserID = 45413
这是可以接受的 strUserID = 43790
这是可以接受的 strUserID = 44681
这是可以接受的 strUserID = 45201
这是可以接受的 strUserID = 45417
这是可以接受的 strUserID = 45375
这是可以接受的 strUserID = 45335
这是可以接受的 strUserID = 45488
这是可以接受的 strUserID = 43565
这是可以接受的 strUserID = 45209
这是可以接受的 strUserID = 45440
这是可以接受的 strUserID = 42166
这是可以接受的 strUserID = 45507
这是可以接受的 strUserID = 43452
这是可以接受的 strUserID = 41524
这是可以接受的 strUserID = 45108
这是可以接受的 strUserID = 45407
这是可以接受的 strUserID = 45573
这是可以接受的 strUserID = 45585
这是可以接受的 strUserID = 45543
这是可以接受的 strUserID = 45554
这是可以接受的 strUserID = 45551
这是可以接受的 strUserID = 42211
这是可以接受的 strUserID = 45560
这是可以接受的 strUserID = 45114
这是可以接受的 strUserID = 45401
这是可以接受的 strUserID = 45697
这是可以接受的 strUserID = 45729
这是可以接受的 strUserID = 44622
这是可以接受的 strUserID = 45544
这是可以接受的 strUserID = 45576
这是可以接受的 strUserID = 45721
这是可以接受的 strUserID = 45741
这是可以接受的 strUserID = 45626
这是可以接受的 strUserID = 45406
这是可以接受的 strUserID = 45733
这是可以接受的 strUserID = 43563
这是可以接受的 strUserID = 45765
这是可以接受的 strUserID = 45770
这是可以接受的 strUserID = 43135
这是可以接受的 strUserID = 45385
这是可以接受的 strUserID = 45439
这是可以接受的 strUserID = 45784
这是可以接受的 strUserID = 45783
这是可以接受的 strUserID = 45803
这是可以接受的 strUserID = 45649
这是可以接受的 strUserID = 45652
这是可以接受的 strUserID = 45384
这是可以接受的 strUserID = 45608
这是可以接受的 strUserID = 45827
这是可以接受的 strUserID = 45816
这是可以接受的 strUserID = 45821
这是可以接受的 strUserID = 45822
这是可以接受的 strUserID = 43768
这是可以接受的 strUserID = 44607
这是可以接受的 strUserID = 45472
这是可以接受的 strUserID = 43085
这是可以接受的 strUserID = 45752
这是可以接受的 strUserID = 43624
这是可以接受的 strUserID = 45807
这是可以接受的 strUserID = 45826
这是可以接受的 strUserID = 45863
这是可以接受的 strUserID = 45823
这是可以接受的 strUserID = 44845
这是可以接受的 strUserID = 45880
这是可以接受的 strUserID = 44920
这是可以接受的 strUserID = 45788
这是可以接受的 strUserID = 45884
这是可以接受的 strUserID = 45842
这是可以接受的 strUserID = 45899
这是可以接受的 strUserID = 45902
这是可以接受的 strUserID = 45850
这是可以接受的 strUserID = 45910
这是可以接受的 strUserID = 45929
这是可以接受的 strUserID = 45846
这是可以接受的 strUserID = 45658
这是可以接受的 strUserID = 45720
这是可以接受的 strUserID = 45801
这是可以接受的 strUserID = 44762
这是可以接受的 strUserID = 45944
这是可以接受的 strUserID = 45885
这是可以接受的 strUserID = 45928
这是可以接受的 strUserID = 45937
这是可以接受的 strUserID = 45940
这是可以接受的 strUserID = 45948
这是可以接受的 strUserID = 41372
这是可以接受的 strUserID = 45855
这是可以接受的 strUserID = 44011
这是可以接受的 strUserID = 43761
这是可以接受的 strUserID = 44309
这是可以接受的 strUserID = 46003
这是可以接受的 strUserID = 45596
这是可以接受的 strUserID = 46007
这是可以接受的 strUserID = 45868
这是可以接受的 strUserID = 45675
这是可以接受的 strUserID = 45972
这是可以接受的 strUserID = 43923
这是可以接受的 strUserID = 45860
这是可以接受的 strUserID = 46029
这是可以接受的 strUserID = 46017
这是可以接受的 strUserID = 46010
这是可以接受的 strUserID = 46032
这是可以接受的 strUserID = 42167
这是可以接受的 strUserID = 44380
这是可以接受的 strUserID = 44721
这是可以接受的 strUserID = 46067
这是可以接受的 strUserID = 45242
这是可以接受的 strUserID = 46054
这是可以接受的 strUserID = 45875
这是可以接受的 strUserID = 45104
这是可以接受的 strUserID = 46161
这是可以接受的 strUserID = 46031
这是可以接受的 strUserID = 46162
这是可以接受的 strUserID = 46151
这是可以接受的 strUserID = 46206
这是可以接受的 strUserID = 46207
这是可以接受的 strUserID = 45983
这是可以接受的 strUserID = 45997
这是可以接受的 strUserID = 45656
这是可以接受的 strUserID = 44207
这是可以接受的 strUserID = 46043
这是可以接受的 strUserID = 46060
这是可以接受的 strUserID = 46156
这是可以接受的 strUserID = 46103
这是可以接受的 strUserID = 46145
这是可以接受的 strUserID = 45245
这是可以接受的 strUserID = 46135
这是可以接受的 strUserID = 46140
这是可以接受的 strUserID = 46185
这是可以接受的 strUserID = 43926
这是可以接受的 strUserID = 46194
这是可以接受的 strUserID = 43151
这是可以接受的 strUserID = 46181
这是可以接受的 strUserID = 46180
这是可以接受的 strUserID = 44581
这是可以接受的 strUserID = 46164
这是可以接受的 strUserID = 46100
这是可以接受的 strUserID = 46155
这是可以接受的 strUserID = 46321
这是可以接受的 strUserID = 46222
这是可以接受的 strUserID = 46218
这是可以接受的 strUserID = 46049
这是可以接受的 strUserID = 46286
这是可以接受的 strUserID = 46131
这是可以接受的 strUserID = 46035
这是可以接受的 strUserID = 45808
这是可以接受的 strUserID = 46271
这是可以接受的 strUserID = 46300
这是可以接受的 strUserID = 45897
这是可以接受的 strUserID = 46276
这是可以接受的 strUserID = 45958
这是可以接受的 strUserID = 46307
这是可以接受的 strUserID = 46299
这是可以接受的 strUserID = 43208
这是可以接受的 strUserID = 46288
这是可以接受的 strUserID = 41415
这是可以接受的 strUserID = 46213
这是可以接受的 strUserID = 46392
这是可以接受的 strUserID = 46267
这是可以接受的 strUserID = 46265
这是可以接受的 strUserID = 46259
这是可以接受的 strUserID = 46315
这是可以接受的 strUserID = 46211
这是可以接受的 strUserID = 46415
这是可以接受的 strUserID = 45494
这是可以接受的 strUserID = 44109
这是可以接受的 strUserID = 46014
这是可以接受的 strUserID = 46071
这是可以接受的 strUserID = 46012
这是有问题的 必须进一步观察 strUserID = 46365
这是可以接受的 strUserID = 46379
这是可以接受的 strUserID = 46391
这是可以接受的 strUserID = 46406
这是可以接受的 strUserID = 46492
这是可以接受的 strUserID = 44624
这是可以接受的 strUserID = 46412
这是可以接受的 strUserID = 46523
这是可以接受的 strUserID = 44750
这是可以接受的 strUserID = 43918
这是可以接受的 strUserID = 46326
这是可以接受的 strUserID = 46493
这是可以接受的 strUserID = 46311
这是可以接受的 strUserID = 46416
这是可以接受的 strUserID = 46445
这是可以接受的 strUserID = 46027
这是可以接受的 strUserID = 46023
这是可以接受的 strUserID = 46467
这是可以接受的 strUserID = 45968
这是可以接受的 strUserID = 46486
这是可以接受的 strUserID = 46005
这是可以接受的 strUserID = 46015
这是可以接受的 strUserID = 46536
这是可以接受的 strUserID = 46526
这是可以接受的 strUserID = 45769
这是可以接受的 strUserID = 46563
这是可以接受的 strUserID = 46291
这是可以接受的 strUserID = 43459
这是可以接受的 strUserID = 46554
这是可以接受的 strUserID = 46498
这是可以接受的 strUserID = 45988
这是可以接受的 strUserID = 46565
这是可以接受的 strUserID = 46570
这是可以接受的 strUserID = 46556
这是可以接受的 strUserID = 46594
这是可以接受的 strUserID = 46595
这是可以接受的 strUserID = 45280
这是可以接受的 strUserID = 46628
这是可以接受的 strUserID = 46648
这是可以接受的 strUserID = 46375
这是可以接受的 strUserID = 46696
这是可以接受的 strUserID = 46654
这是可以接受的 strUserID = 44230
这是可以接受的 strUserID = 44108
这是可以接受的 strUserID = 46643
这是可以接受的 strUserID = 46642
这是可以接受的 strUserID = 46653
这是可以接受的 strUserID = 46673
这是可以接受的 strUserID = 45657
这是可以接受的 strUserID = 46349
这是可以接受的 strUserID = 46489
这是可以接受的 strUserID = 46138
这是可以接受的 strUserID = 42952
这是可以接受的 strUserID = 46543
这是可以接受的 strUserID = 45978
这是可以接受的 strUserID = 42479
这是可以接受的 strUserID = 46717
这是可以接受的 strUserID = 46724
这是可以接受的 strUserID = 46721
这是可以接受的 strUserID = 45176
这是可以接受的 strUserID = 46726
这是可以接受的 strUserID = 46727
这是可以接受的 strUserID = 46677
这是可以接受的 strUserID = 46756
这是可以接受的 strUserID = 45954
这是可以接受的 strUserID = 46757
这是可以接受的 strUserID = 46792
这是可以接受的 strUserID = 46747
这是可以接受的 strUserID = 46837
这是可以接受的 strUserID = 46847
这是可以接受的 strUserID = 46801
这是可以接受的 strUserID = 46804
这是可以接受的 strUserID = 41419
这是可以接受的 strUserID = 46844
这是可以接受的 strUserID = 46835
这是可以接受的 strUserID = 46063
这是可以接受的 strUserID = 43261
这是可以接受的 strUserID = 46231
这是可以接受的 strUserID = 46640
这是可以接受的 strUserID = 46879
这是可以接受的 strUserID = 46773
这是可以接受的 strUserID = 46903
这是可以接受的 strUserID = 46532
这是可以接受的 strUserID = 45337
这是可以接受的 strUserID = 46783
这是可以接受的 strUserID = 43586
这是可以接受的 strUserID = 46571
这是可以接受的 strUserID = 46871
这是可以接受的 strUserID = 46833
这是可以接受的 strUserID = 46566
这是可以接受的 strUserID = 46894
这是可以接受的 strUserID = 46896
这是可以接受的 strUserID = 43687
这是可以接受的 strUserID = 46919
这是可以接受的 strUserID = 46946
这是可以接受的 strUserID = 46967
这是可以接受的 strUserID = 46968
这是可以接受的 strUserID = 46965
这是可以接受的 strUserID = 46982
这是可以接受的 strUserID = 45736
这是可以接受的 strUserID = 46562
这是可以接受的 strUserID = 46995
这是可以接受的 strUserID = 47051
这是可以接受的 strUserID = 47072
这是可以接受的 strUserID = 47075
这是可以接受的 strUserID = 47081
这是可以接受的 strUserID = 46739
这是可以接受的 strUserID = 47088
这是可以接受的 strUserID = 46970
这是可以接受的 strUserID = 46999
这是可以接受的 strUserID = 46989
这是可以接受的 strUserID = 45663
这是可以接受的 strUserID = 46612
这是可以接受的 strUserID = 47001
这是可以接受的 strUserID = 47006
这是可以接受的 strUserID = 47047
这是可以接受的 strUserID = 47010
这是可以接受的 strUserID = 47049
这是可以接受的 strUserID = 45235
这是可以接受的 strUserID = 47092
这是可以接受的 strUserID = 46553
这是可以接受的 strUserID = 46770
这是可以接受的 strUserID = 47099
这是可以接受的 strUserID = 47111
这是可以接受的 strUserID = 47116
这是可以接受的 strUserID = 47123
这是可以接受的 strUserID = 47098
这是可以接受的 strUserID = 47112
这是可以接受的 strUserID = 46641
这是可以接受的 strUserID = 47085
这是可以接受的 strUserID = 46859
这是可以接受的 strUserID = 46688
这是可以接受的 strUserID = 47157
这是可以接受的 strUserID = 47131
这是可以接受的 strUserID = 41365
这是可以接受的 strUserID = 47198
这是可以接受的 strUserID = 47119
这是可以接受的 strUserID = 47201
这是可以接受的 strUserID = 45887
这是可以接受的 strUserID = 47136
这是可以接受的 strUserID = 47205
这是可以接受的 strUserID = 47219
这是可以接受的 strUserID = 47216
这是可以接受的 strUserID = 47215
这是可以接受的 strUserID = 46795
这是可以接受的 strUserID = 46204
这是可以接受的 strUserID = 47256
这是可以接受的 strUserID = 46929
这是可以接受的 strUserID = 47122
这是可以接受的 strUserID = 47304
这是可以接受的 strUserID = 47325
这是可以接受的 strUserID = 45761
这是可以接受的 strUserID = 41274
这是可以接受的 strUserID = 47195
这是可以接受的 strUserID = 47252
这是可以接受的 strUserID = 46745
这是可以接受的 strUserID = 46491
这是可以接受的 strUserID = 47312
这是可以接受的 strUserID = 44323
这是可以接受的 strUserID = 47153
这是可以接受的 strUserID = 47017
这是可以接受的 strUserID = 47184
这是可以接受的 strUserID = 47310
这是可以接受的 strUserID = 47370
这是可以接受的 strUserID = 47269
这是可以接受的 strUserID = 47401
这是可以接受的 strUserID = 44687
这是可以接受的 strUserID = 46980
这是可以接受的 strUserID = 46081
这是可以接受的 strUserID = 47306
这是可以接受的 strUserID = 47217
这是可以接受的 strUserID = 45279
这是可以接受的 strUserID = 45867
这是可以接受的 strUserID = 47346
这是可以接受的 strUserID = 47347
这是可以接受的 strUserID = 47355
这是可以接受的 strUserID = 46977
这是可以接受的 strUserID = 45960
这是可以接受的 strUserID = 47404
这是可以接受的 strUserID = 47379
这是可以接受的 strUserID = 43175
这是可以接受的 strUserID = 47282
这是可以接受的 strUserID = 47397
这是可以接受的 strUserID = 47095
这是可以接受的 strUserID = 47436
这是可以接受的 strUserID = 43582
这是可以接受的 strUserID = 47452
这是可以接受的 strUserID = 47483
这是可以接受的 strUserID = 45029
这是可以接受的 strUserID = 47508
这是可以接受的 strUserID = 47255
这是可以接受的 strUserID = 47220
这是可以接受的 strUserID = 47259
这是可以接受的 strUserID = 47572
这是可以接受的 strUserID = 47413
这是可以接受的 strUserID = 47509
这是可以接受的 strUserID = 43983
这是可以接受的 strUserID = 46530
这是可以接受的 strUserID = 47684
这是可以接受的 strUserID = 46167
这是可以接受的 strUserID = 45503
这是可以接受的 strUserID = 46703
这是可以接受的 strUserID = 47726
这是可以接受的 strUserID = 44181
这是可以接受的 strUserID = 47843
这是可以接受的 strUserID = 47810
这是可以接受的 strUserID = 47725
这是可以接受的 strUserID = 47899
这是可以接受的 strUserID = 47926
这是可以接受的 strUserID = 47937
这是可以接受的 strUserID = 48002
这是可以接受的 strUserID = 47496
这是可以接受的 strUserID = 48029
这是可以接受的 strUserID = 45909
这是可以接受的 strUserID = 47667
这是可以接受的 strUserID = 47705
这是可以接受的 strUserID = 47691
这是可以接受的 strUserID = 44680
这是可以接受的 strUserID = 47740
这是可以接受的 strUserID = 47728
这是可以接受的 strUserID = 47776
这是可以接受的 strUserID = 44677
这是可以接受的 strUserID = 47590
这是可以接受的 strUserID = 47752
这是可以接受的 strUserID = 48072
这是可以接受的 strUserID = 46856
这是可以接受的 strUserID = 47774
这是可以接受的 strUserID = 47772
这是可以接受的 strUserID = 47424
这是可以接受的 strUserID = 43595
这是可以接受的 strUserID = 47822
这是可以接受的 strUserID = 46350
这是可以接受的 strUserID = 47832
这是可以接受的 strUserID = 47813
这是可以接受的 strUserID = 44841
这是可以接受的 strUserID = 47849
这是可以接受的 strUserID = 47851
这是可以接受的 strUserID = 47914
这是可以接受的 strUserID = 47901
这是可以接受的 strUserID = 47527
这是可以接受的 strUserID = 47917
这是可以接受的 strUserID = 47921
这是可以接受的 strUserID = 47948
这是可以接受的 strUserID = 47685
这是可以接受的 strUserID = 47840
这是可以接受的 strUserID = 46922
这是可以接受的 strUserID = 44632
这是可以接受的 strUserID = 47799
这是可以接受的 strUserID = 46777
这是可以接受的 strUserID = 41229
这是可以接受的 strUserID = 47998
这是可以接受的 strUserID = 47671
这是可以接受的 strUserID = 47570
这是可以接受的 strUserID = 43351
这是可以接受的 strUserID = 47888
这是可以接受的 strUserID = 48070
这是可以接受的 strUserID = 47876
这是可以接受的 strUserID = 47941
这是可以接受的 strUserID = 47872
这是可以接受的 strUserID = 44943
这是可以接受的 strUserID = 47875
这是可以接受的 strUserID = 48027
这是可以接受的 strUserID = 47946
这是可以接受的 strUserID = 41426
这是可以接受的 strUserID = 48034
这是可以接受的 strUserID = 48042
这是可以接受的 strUserID = 48121
这是有问题的用户 删除才正常 删除他们 strUserID = 48094
这是有问题的用户 删除才正常 删除他们 strUserID = 48099
这是可以接受的 strUserID = 48126
这是可以接受的 strUserID = 48044
这是可以接受的 strUserID = 48102
这是可以接受的 strUserID = 48101
这是可以接受的 strUserID = 48135
这是有问题的用户 删除才正常 删除他们 strUserID = 48091
这是有问题的用户 删除才正常 删除他们 strUserID = 48092
这是有问题的用户 删除才正常 删除他们 strUserID = 48096
这是有问题的用户 删除才正常 删除他们 strUserID = 48098
这是有问题的用户 删除才正常 删除他们 strUserID = 44387
这是有问题的用户 删除才正常 删除他们 strUserID = 47428
这是有问题的用户 删除才正常 删除他们 strUserID = 48093
这是有问题的用户 删除才正常 删除他们 strUserID = 46765
这是可以接受的 strUserID = 47375
这是可以接受的 strUserID = 47974
这是可以接受的 strUserID = 48085
这是可以接受的 strUserID = 47423
这是可以接受的 strUserID = 48049
这是可以接受的 strUserID = 48069
这是可以接受的 strUserID = 48226
这是可以接受的 strUserID = 48109
这是可以接受的 strUserID = 48089
这是可以接受的 strUserID = 48140
这是可以接受的 strUserID = 46269
这是可以接受的 strUserID = 41399
这是可以接受的 strUserID = 48118
这是可以接受的 strUserID = 48313
这是可以接受的 strUserID = 48249
这是可以接受的 strUserID = 48338
这是可以接受的 strUserID = 48132
这是可以接受的 strUserID = 48174
这是可以接受的 strUserID = 48123
这是可以接受的 strUserID = 48186
这是可以接受的 strUserID = 48227
这是可以接受的 strUserID = 48238
这是可以接受的 strUserID = 48179
这是可以接受的 strUserID = 46487
这是可以接受的 strUserID = 48194
这是可以接受的 strUserID = 47834
这是可以接受的 strUserID = 48387
这是可以接受的 strUserID = 48439
这是可以接受的 strUserID = 44667
这是可以接受的 strUserID = 46126
这是可以接受的 strUserID = 48407
这是可以接受的 strUserID = 44869
这是可以接受的 strUserID = 47254
这是可以接受的 strUserID = 48315
这是可以接受的 strUserID = 48054
这是可以接受的 strUserID = 48511
这是可以接受的 strUserID = 48473
这是可以接受的 strUserID = 48553
这是可以接受的 strUserID = 47826
这是可以接受的 strUserID = 48360
这是可以接受的 strUserID = 48362
这是可以接受的 strUserID = 48397
这是可以接受的 strUserID = 48411
这是可以接受的 strUserID = 48409
这是可以接受的 strUserID = 48463
这是可以接受的 strUserID = 48376
这是可以接受的 strUserID = 48404
这是可以接受的 strUserID = 48406
这是可以接受的 strUserID = 47959
这是可以接受的 strUserID = 48634
这是可以接受的 strUserID = 48642
这是可以接受的 strUserID = 47531
这是可以接受的 strUserID = 46891
这是可以接受的 strUserID = 48680
这是可以接受的 strUserID = 48648
这是可以接受的 strUserID = 48722
这是可以接受的 strUserID = 48460
这是可以接受的 strUserID = 48430
这是可以接受的 strUserID = 48425
这是可以接受的 strUserID = 46807
这是可以接受的 strUserID = 48232
这是可以接受的 strUserID = 48456
这是可以接受的 strUserID = 48500
这是可以接受的 strUserID = 48528
这是可以接受的 strUserID = 48506
这是可以接受的 strUserID = 48530
这是可以接受的 strUserID = 48410
这是可以接受的 strUserID = 48537
这是可以接受的 strUserID = 45200
这是可以接受的 strUserID = 48566
这是可以接受的 strUserID = 48761
这是可以接受的 strUserID = 48772
这是可以接受的 strUserID = 48308
这是可以接受的 strUserID = 48288
这是可以接受的 strUserID = 48429
这是可以接受的 strUserID = 48363
这是可以接受的 strUserID = 48575
这是可以接受的 strUserID = 48442
这是可以接受的 strUserID = 47603
这是可以接受的 strUserID = 48517
这是可以接受的 strUserID = 44781
这是可以接受的 strUserID = 48479
这是可以接受的 strUserID = 47445
这是可以接受的 strUserID = 48715
这是可以接受的 strUserID = 48716
这是可以接受的 strUserID = 48538
这是可以接受的 strUserID = 48639
这是可以接受的 strUserID = 48625
这是可以接受的 strUserID = 48578
这是可以接受的 strUserID = 46809
这是可以接受的 strUserID = 48599
这是可以接受的 strUserID = 48710
这是可以接受的 strUserID = 48620
这是可以接受的 strUserID = 46351
这是可以接受的 strUserID = 46731
这是可以接受的 strUserID = 48155
这是可以接受的 strUserID = 47202
这是可以接受的 strUserID = 48602
这是可以接受的 strUserID = 48603
这是可以接受的 strUserID = 43980
这是可以接受的 strUserID = 48144
这是可以接受的 strUserID = 47784
这是可以接受的 strUserID = 48646
这是可以接受的 strUserID = 48691
这是可以接受的 strUserID = 47477
这是可以接受的 strUserID = 48619
这是可以接受的 strUserID = 48721
这是可以接受的 strUserID = 48688
这是可以接受的 strUserID = 46141
这是可以接受的 strUserID = 43830
这是可以接受的 strUserID = 47623
这是可以接受的 strUserID = 48678
这是可以接受的 strUserID = 48690
这是可以接受的 strUserID = 48615
这是可以接受的 strUserID = 48444
这是可以接受的 strUserID = 45962
这是可以接受的 strUserID = 48427
这是可以接受的 strUserID = 48757
这是可以接受的 strUserID = 48647
这是可以接受的 strUserID = 48763
这是可以接受的 strUserID = 48459
这是可以接受的 strUserID = 48784
这是可以接受的 strUserID = 48843
这是可以接受的 strUserID = 48859
这是可以接受的 strUserID = 48906
这是可以接受的 strUserID = 48920
这是可以接受的 strUserID = 48621
这是可以接受的 strUserID = 48775
这是可以接受的 strUserID = 48782
这是可以接受的 strUserID = 48841
这是可以接受的 strUserID = 48967
这是可以接受的 strUserID = 47106
这是可以接受的 strUserID = 48800
这是可以接受的 strUserID = 48708
这是可以接受的 strUserID = 47102
这是可以接受的 strUserID = 48868
这是可以接受的 strUserID = 48882
这是可以接受的 strUserID = 47911
这是可以接受的 strUserID = 48869
这是可以接受的 strUserID = 48842
这是可以接受的 strUserID = 45107
这是可以接受的 strUserID = 48883
这是可以接受的 strUserID = 48877
这是可以接受的 strUserID = 48867
这是可以接受的 strUserID = 48909
这是可以接受的 strUserID = 48934
这是可以接受的 strUserID = 48885
这是可以接受的 strUserID = 48888
这是可以接受的 strUserID = 48774
这是可以接受的 strUserID = 48901
这是可以接受的 strUserID = 48954
这是可以接受的 strUserID = 48955
这是可以接受的 strUserID = 48915
这是可以接受的 strUserID = 49026
这是可以接受的 strUserID = 47689
这是可以接受的 strUserID = 48932
这是可以接受的 strUserID = 48985
这是可以接受的 strUserID = 48986
这是可以接受的 strUserID = 49034
这是可以接受的 strUserID = 44908
这是可以接受的 strUserID = 48484
这是可以接受的 strUserID = 48476
这是可以接受的 strUserID = 48593
这是可以接受的 strUserID = 48534
这是可以接受的 strUserID = 48912
这是可以接受的 strUserID = 48998
这是可以接受的 strUserID = 49032
这是可以接受的 strUserID = 49069
这是可以接受的 strUserID = 46289
这是可以接受的 strUserID = 47722
这是可以接受的 strUserID = 49196
这是可以接受的 strUserID = 48983
这是可以接受的 strUserID = 49107
这是可以接受的 strUserID = 43975
这是可以接受的 strUserID = 49152
这是可以接受的 strUserID = 49007
这是可以接受的 strUserID = 49087
这是可以接受的 strUserID = 49166
这是可以接受的 strUserID = 49165
这是可以接受的 strUserID = 49157
这是可以接受的 strUserID = 49158
这是可以接受的 strUserID = 49159
这是可以接受的 strUserID = 47110
这是可以接受的 strUserID = 48966
这是可以接受的 strUserID = 49188
这是可以接受的 strUserID = 49051
这是可以接受的 strUserID = 49217
这是可以接受的 strUserID = 49219
这是可以接受的 strUserID = 48981
这是可以接受的 strUserID = 45449
这是可以接受的 strUserID = 49214
这是可以接受的 strUserID = 49284
这是可以接受的 strUserID = 49331
这是可以接受的 strUserID = 47980
这是可以接受的 strUserID = 49249
这是可以接受的 strUserID = 49392
这是可以接受的 strUserID = 49394
这是可以接受的 strUserID = 46505
这是可以接受的 strUserID = 49472
这是可以接受的 strUserID = 42487
这是可以接受的 strUserID = 49224
这是可以接受的 strUserID = 48773
这是可以接受的 strUserID = 48635
这是可以接受的 strUserID = 49277
这是可以接受的 strUserID = 49038
这是可以接受的 strUserID = 45105
这是可以接受的 strUserID = 48508
这是可以接受的 strUserID = 49041
这是可以接受的 strUserID = 49319
这是可以接受的 strUserID = 49320
这是可以接受的 strUserID = 49198
这是可以接受的 strUserID = 49287
这是可以接受的 strUserID = 46228
这是可以接受的 strUserID = 48940
这是可以接受的 strUserID = 49292
这是可以接受的 strUserID = 49408
这是可以接受的 strUserID = 49404
这是可以接受的 strUserID = 49405
这是可以接受的 strUserID = 49204
这是可以接受的 strUserID = 49346
这是可以接受的 strUserID = 49397
这是可以接受的 strUserID = 49398
这是可以接受的 strUserID = 48958
这是可以接受的 strUserID = 49401
这是可以接受的 strUserID = 49474
这是可以接受的 strUserID = 49432
这是可以接受的 strUserID = 48976
这是可以接受的 strUserID = 49555
这是可以接受的 strUserID = 49113
这是可以接受的 strUserID = 48803
这是可以接受的 strUserID = 48489
这是可以接受的 strUserID = 47891
这是可以接受的 strUserID = 49046
这是可以接受的 strUserID = 47089
这是可以接受的 strUserID = 49411
这是可以接受的 strUserID = 49412
这是可以接受的 strUserID = 49419
这是可以接受的 strUserID = 49386
这是可以接受的 strUserID = 49483
这是可以接受的 strUserID = 49036
这是可以接受的 strUserID = 49550
这是可以接受的 strUserID = 49535
这是可以接受的 strUserID = 49536
这是可以接受的 strUserID = 49546
这是可以接受的 strUserID = 49566
这是可以接受的 strUserID = 49565
这是可以接受的 strUserID = 49571
这是可以接受的 strUserID = 49545
这是可以接受的 strUserID = 49229
这是可以接受的 strUserID = 49414
这是可以接受的 strUserID = 49458
这是可以接受的 strUserID = 49327
这是可以接受的 strUserID = 47057
这是可以接受的 strUserID = 49010
这是可以接受的 strUserID = 49459
这是可以接受的 strUserID = 49184
这是可以接受的 strUserID = 49303
这是可以接受的 strUserID = 49306
这是可以接受的 strUserID = 46469
这是可以接受的 strUserID = 49473
这是可以接受的 strUserID = 49647
这是可以接受的 strUserID = 49692
这是可以接受的 strUserID = 49029
这是可以接受的 strUserID = 49599
这是可以接受的 strUserID = 49606
这是可以接受的 strUserID = 49614
这是可以接受的 strUserID = 49612
这是可以接受的 strUserID = 49629
这是可以接受的 strUserID = 46341
这是可以接受的 strUserID = 49421
这是可以接受的 strUserID = 49720
这是可以接受的 strUserID = 49728
这是可以接受的 strUserID = 48630
这是可以接受的 strUserID = 49285
这是可以接受的 strUserID = 49286
这是可以接受的 strUserID = 49431
这是可以接受的 strUserID = 49429
这是可以接受的 strUserID = 49433
这是可以接受的 strUserID = 49753
这是可以接受的 strUserID = 47857
这是可以接受的 strUserID = 49810
这是可以接受的 strUserID = 49074
这是可以接受的 strUserID = 49841
这是可以接受的 strUserID = 49722
这是可以接受的 strUserID = 49743
这是可以接受的 strUserID = 49734
这是可以接受的 strUserID = 49730
这是可以接受的 strUserID = 49785
这是可以接受的 strUserID = 49744
这是可以接受的 strUserID = 49805
这是可以接受的 strUserID = 48377
这是可以接受的 strUserID = 49872
这是可以接受的 strUserID = 49893
这是可以接受的 strUserID = 41294
这是可以接受的 strUserID = 49687
这是可以接受的 strUserID = 49427
这是可以接受的 strUserID = 49825
这是可以接受的 strUserID = 49855
这是可以接受的 strUserID = 49801
这是可以接受的 strUserID = 46483
这是可以接受的 strUserID = 46229
这是可以接受的 strUserID = 49908
这是可以接受的 strUserID = 49907
这是可以接受的 strUserID = 49944
这是可以接受的 strUserID = 49946
这是可以接受的 strUserID = 49940
这是可以接受的 strUserID = 43035
这是可以接受的 strUserID = 49969
这是可以接受的 strUserID = 49977
这是可以接受的 strUserID = 49934
这是可以接受的 strUserID = 50100
这是可以接受的 strUserID = 49161
这是可以接受的 strUserID = 50125
这是可以接受的 strUserID = 50163
这是可以接受的 strUserID = 49904
这是可以接受的 strUserID = 49824
这是可以接受的 strUserID = 47197
这是可以接受的 strUserID = 49965
这是可以接受的 strUserID = 49973
这是可以接受的 strUserID = 49997
这是可以接受的 strUserID = 49981
这是可以接受的 strUserID = 50025
这是可以接受的 strUserID = 43858
这是可以接受的 strUserID = 49994
这是可以接受的 strUserID = 43681
这是可以接受的 strUserID = 49678
这是可以接受的 strUserID = 49714
这是可以接受的 strUserID = 50130
这是可以接受的 strUserID = 50058
这是可以接受的 strUserID = 46273
这是可以接受的 strUserID = 49839
这是可以接受的 strUserID = 50053
这是可以接受的 strUserID = 50055
这是可以接受的 strUserID = 49520
这是可以接受的 strUserID = 50048
这是可以接受的 strUserID = 49718
这是可以接受的 strUserID = 50071
这是可以接受的 strUserID = 50029
这是可以接受的 strUserID = 50075
这是可以接受的 strUserID = 48740
这是可以接受的 strUserID = 50245
这是可以接受的 strUserID = 49854
这是可以接受的 strUserID = 50012
这是可以接受的 strUserID = 50027
这是可以接受的 strUserID = 50140
这是可以接受的 strUserID = 49865
这是可以接受的 strUserID = 49348
这是可以接受的 strUserID = 47686
这是可以接受的 strUserID = 49799
这是可以接受的 strUserID = 49874
这是可以接受的 strUserID = 49979
这是可以接受的 strUserID = 50060
这是可以接受的 strUserID = 50062
这是可以接受的 strUserID = 50111
这是可以接受的 strUserID = 50128
这是可以接受的 strUserID = 50137
这是可以接受的 strUserID = 50129
这是可以接受的 strUserID = 50211
这是可以接受的 strUserID = 47818
这是可以接受的 strUserID = 50136
这是可以接受的 strUserID = 48074
这是可以接受的 strUserID = 50253
这是可以接受的 strUserID = 44102
这是可以接受的 strUserID = 50215
这是可以接受的 strUserID = 48189
这是可以接受的 strUserID = 48200
这是可以接受的 strUserID = 50228
这是可以接受的 strUserID = 50308
这是可以接受的 strUserID = 50316
这是可以接受的 strUserID = 50285
这是可以接受的 strUserID = 49174
这是可以接受的 strUserID = 49857
这是可以接受的 strUserID = 50078
这是可以接受的 strUserID = 50072
这是可以接受的 strUserID = 50300
这是可以接受的 strUserID = 49077
这是可以接受的 strUserID = 50173
这是可以接受的 strUserID = 50041
这是可以接受的 strUserID = 50162
这是可以接受的 strUserID = 50166
订单配对正常 可以研究出局情况 strUserID = 41199
这是有问题的用户 保存他们 等待投诉吧 strUserID = 41205
订单配对正常 可以研究出局情况 strUserID = 41205
这是有问题的用户 保存他们 等待投诉吧 strUserID = 41210
订单配对正常 可以研究出局情况 strUserID = 41210
这是有问题的用户 保存他们 等待投诉吧 strUserID = 41230
这是有问题的用户 保存他们 等待投诉吧 strUserID = 41230
这是有问题的用户 保存他们 等待投诉吧 strUserID = 41230
这是有问题的用户 保存他们 等待投诉吧 strUserID = 41230
这是有问题的用户 保存他们 等待投诉吧 strUserID = 41230
订单配对正常 可以研究出局情况 strUserID = 41230
订单配对正常 可以研究出局情况 strUserID = 41256
订单配对正常 可以研究出局情况 strUserID = 41263
这是有问题的用户 保存他们 等待投诉吧 strUserID = 41296
订单配对正常 可以研究出局情况 strUserID = 41296
这是有问题的用户 保存他们 等待投诉吧 strUserID = 42147
订单配对正常 可以研究出局情况 strUserID = 42147
订单配对正常 可以研究出局情况 strUserID = 431132017 / 9 / 30 20:56:46do over*/

                #endregion
            }


        }

        private ArrayList pub_ArrayList = null;

        /// <summary>
        /// 循环 改变表 b008_OpterationUserActiveReturnMoneyOrderNum 为正常值
        /// </summary>
        private void _009ResetTo008_step7()
        {

            pub_ArrayList = new ArrayList();


            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLLb006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            System.Data.DataTable Data_DataTableIDADD = BLLb006_TotalWealth_OperationUser.GetList("IDAdd", " 1=1 order by IDAdd asc").Tables[0];
            for (int i = 0; i < Data_DataTableIDADD.Rows.Count; i++)
            {
                string strIDAddID = Data_DataTableIDADD.Rows[i]["IDAdd"].ToString();
                string strSQL = "update b006_TotalWealth_OperationUser set id=" + (i + 1) + " where IDAdd='" + strIDAddID + "'";
                pub_ArrayList.Add(strSQL);

                //BLLb006_TotalWealth_OperationUser.Update("ID=" + (i + 1), "IDAdd='" + strIDAddID + "'");
            }
            EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(pub_ArrayList);


            //string strDeop = "alter table BLLb006_TotalWealth_OperationUser DROP COLUMN IDAdd";
            //EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strDeop);


        }
        /// <summary>
        /// 核对 出局情况
        /// </summary>
        private void _009ResetTo008_step6()
        {
            #region  执行 留存记录
            /*       2017 / 9 / 24 12:59:02
       
        */


            #endregion

            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLLb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            System.Data.DataTable Data_DataTableIDADD = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetList("*", "orderid is null and ShopClient_ID=21 order by id").Tables[0];
            for (int i = 0; i < Data_DataTableIDADD.Rows.Count; i++)
            {
                Decimal ReturnALlMoneyUnit = Data_DataTableIDADD.Rows[i]["ReturnMoneyUnit"].toDecimal();
                string strUserID = Data_DataTableIDADD.Rows[i]["UserID"].toString();

                string strSQL = "select sum(ReturnMoneyUnit) from b008_OpterationUserActiveReturnMoneyOrderNum where userid=" + strUserID + " and orderid is not null";
                Decimal orderDescimal = BLLb008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strSQL).Tables[0].Rows[0][0].toDecimal();

                if (ReturnALlMoneyUnit > orderDescimal + (Decimal)0.9)
                {
                    string strOut = "<br />已经返还的 大于现在订单统计的 strUserID=" + strUserID + " ReturnALlMoneyUnit =" + ReturnALlMoneyUnit + " orderDescimal =" + orderDescimal;
                    Response.Write(strOut);
                }
                else if (ReturnALlMoneyUnit < orderDescimal - (Decimal)0.9)
                {
                    string strOut = "<br />已经返还的 小于现在订单统计的 strUserID=" + strUserID + " ReturnALlMoneyUnit =" + ReturnALlMoneyUnit + " orderDescimal =" + orderDescimal;
                    Response.Write(strOut);
                }
                else
                {

                    System.Data.DataTable Chchek = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetList("*", "userid=" + strUserID + " and orderid is not null").Tables[0];
                    for (int j = 0; j < Chchek.Rows.Count; j++)
                    {
                        Decimal curReturnMoneyUnit = Chchek.Rows[j]["ReturnMoneyUnit"].toDecimal();
                        Decimal ActiveOrderNumcurOrderID = Chchek.Rows[j]["ActiveOrderNum"].toDecimal();


                        if ((curReturnMoneyUnit > 0) && (ActiveOrderNumcurOrderID > 0) && (curReturnMoneyUnit / ActiveOrderNumcurOrderID < 1200))
                        {
                            string strOut1 = "<br />应该出局 strUserID=" + strUserID + " curReturnMoneyUnit =" + curReturnMoneyUnit + " ActiveOrderNumcurOrderID =" + ActiveOrderNumcurOrderID;
                            Response.Write(strOut1);
                        }


                    }

                    string strOut = "<br />可以接受的误差 strUserID=" + strUserID + " ReturnALlMoneyUnit =" + ReturnALlMoneyUnit + " strOrderDetailID =" + orderDescimal;
                    Response.Write(strOut);
                }
            }
        }

        /// <summary>
        /// 写008表的ReturnMoneyUnit
        /// </summary>
        private void _009ResetTo008_step5()
        {
            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLLb006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();

            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLLb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            System.Data.DataTable Data_DataTableIDADD = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetList("*", "OrderDetailID is not null and ShopClient_ID=21 order by id").Tables[0];
            for (int i = 0; i < Data_DataTableIDADD.Rows.Count; i++)
            {
                string strActiveOrderNum = Data_DataTableIDADD.Rows[i]["ActiveOrderNum"].toString();
                string strUserID = Data_DataTableIDADD.Rows[i]["UserID"].toString();
                string strID = Data_DataTableIDADD.Rows[i]["ID"].toString();
                string strOrderDetailID = Data_DataTableIDADD.Rows[i]["OrderDetailID"].toString();
                string strSQL006 = "select sum(ConsumeOrRechargeWealth) from b006_TotalWealth_OperationUser where OrderDetailID=" + strOrderDetailID;
                Decimal mySum = BLLb006_TotalWealth_OperationUser.SelectList(strSQL006).Tables[0].Rows[0][0].toDecimal();
                Decimal UnitMoney = strActiveOrderNum.toInt32() * 1998 * 3 - mySum;

                bool boolWorng = BLLb006_TotalWealth_OperationUser.Exists(" userID=" + strUserID + " and ConsumeTypeOrRecharge like '%平台操作%'");
                if (boolWorng)
                {
                    string strOut = "<br />平台操作的问题 strUserID=" + strUserID + " strID =" + strID + " strOrderDetailID =" + strOrderDetailID;
                    Response.Write(strOut + "  " + mySum);
                }
                string strUpdate = "update b008_OpterationUserActiveReturnMoneyOrderNum set ReturnMoneyUnit=" + UnitMoney + " where id=" + strID + " and OrderDetailID=" + strOrderDetailID;
                int intRow = EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strUpdate);
                if (intRow != 1)
                {
                    string strOut = "<br />返回行数不对为strUserID=" + strUserID + " strID =" + strID + " strOrderDetailID =" + strOrderDetailID;
                    Response.Write(strOut + "  " + mySum);
                }




            }



        }
        private ArrayList pub_ArrayListUpdate = null;
        /// <summary>
        /// 拆分财富返还表  按照每天的订单去拆分
        /// </summary>
        private void _009ResetTo008_step4()
        {
            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLLb006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();


            #region 
            if (true)
            {
                String stralterSQL = "alter   table   b006_TotalWealth_OperationUser   add   IDAdd   nvarchar(50)";

                EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(stralterSQL);
                stralterSQL = "create nonclustered Index IX_Table_column_1IDAddIDAdd  on b006_TotalWealth_OperationUser(IDAdd)";
                EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(stralterSQL);


                string str11SQL = "select ID from b006_TotalWealth_OperationUser";
                System.Data.DataTable Data_DataTableIDADD = BLLb006_TotalWealth_OperationUser.SelectList(str11SQL).Tables[0];
                for (int i = 0; i < Data_DataTableIDADD.Rows.Count; i++)
                {
                    string strID = Data_DataTableIDADD.Rows[i]["ID"].toString();
                    string strADD = Eggsoft.Common.StringNum.Add000000Num(strID.toInt32(), 8);
                    BLLb006_TotalWealth_OperationUser.Update("IDAdd=@IDAdd", "ID=@ID", strADD, strID);
                }
            }
            #endregion

            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLLb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();

            string strSQL = @"SELECT TOP 10000000 *  FROM (SELECT b008_OpterationUserActiveReturnMoneyOrderNum.PayDateTime, b008_OpterationUserActiveReturnMoneyOrderNum.OrderCount, 
      b006_TotalWealth_OperationUser.*
FROM b006_TotalWealth_OperationUser LEFT OUTER JOIN
      b008_OpterationUserActiveReturnMoneyOrderNum ON 
      b006_TotalWealth_OperationUser.OrderDetailID = b008_OpterationUserActiveReturnMoneyOrderNum.OrderDetailID
       AND 
      b006_TotalWealth_OperationUser.ShopClient_ID = b008_OpterationUserActiveReturnMoneyOrderNum.ShopClient_ID
       AND 
      b006_TotalWealth_OperationUser.UserID = b008_OpterationUserActiveReturnMoneyOrderNum.UserID where b006_TotalWealth_OperationUser.ShopClient_ID=21)[V666664] where Bool_ConsumeOrRecharge=0 and OrderDetailID is null order by UserID asc,id asc
";
            pub_ArrayListUpdate = new ArrayList();
            System.Data.DataTable Data_DataTable = BLLb006_TotalWealth_OperationUser.SelectList(strSQL).Tables[0];
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                string strConsumeTypeOrRechare = Data_DataTable.Rows[i]["ConsumeTypeOrRecharge"].toString();
                string strUserID = Data_DataTable.Rows[i]["UserID"].toString();
                string strID = Data_DataTable.Rows[i]["ID"].toString();
                string strIDAdd = Data_DataTable.Rows[i]["IDAdd"].toString();
                Decimal ConsumeOrRechargeWealth = Data_DataTable.Rows[i]["ConsumeOrRechargeWealth"].toDecimal();

                DateTime strupdatetime = Convert.ToDateTime(Data_DataTable.Rows[i]["updatetime"].toString().toDateTime());
                DateTime dateWhere = strupdatetime.AddDays(-6);///DateDiff(dd, PayDateTime, GetDate()) > 6

                //if (strID != "46497") continue;
                //if (strID != "46977") continue;
                if (strID == "46497")
                {
                    int ddd = 0;
                }
                //每天分红财富，减增进入现金(96671640016004038)
                if (strConsumeTypeOrRechare.Contains("每天分红财富，减增进入现金(") && !strConsumeTypeOrRechare.Contains(".") && !strConsumeTypeOrRechare.Contains("-"))
                {
                    string strCountDesc = strConsumeTypeOrRechare.Replace("每天分红财富，减增进入现金(", "").Replace(")", "");
                    string strCount = strCountDesc.Substring(strCountDesc.Length - 9, 5);
                    int intActionDoCount = strCount.toInt32() / 100;

                    System.Data.DataTable Data_DataTable008 = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetList("*", " userID=@userID and datediff( dd, '" + dateWhere.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', Paydatetime)<0 order by Paydatetime desc", strUserID).Tables[0];

                    if (Data_DataTable008.Rows.Count == 0)
                    {
                        string strOut = "<br />旧的数据行数0为strUserID=" + strUserID + " strID =" + strID;
                        Response.Write(strOut + "  " + strConsumeTypeOrRechare);
                        Eggsoft.Common.debug_Log.Call_WriteLog(strOut, "旧的0数据行数", strOut);

                        //Response.Write();
                    }
                    else
                    {
                        string strUpdate = "update b006_TotalWealth_OperationUser set IDAdd='" + strIDAdd + Eggsoft.Common.StringNum.Add000000Num(9999, 6) + "' where id=" + strID;
                        pub_ArrayListUpdate.Add(strUpdate);
                        //EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strUpdate);
                    }

                    int intMemoryCountCount = 0;
                    for (int j = 0; j < Data_DataTable008.Rows.Count; j++)
                    {


                        int intOrderID = Data_DataTable008.Rows[j]["OrderID"].toInt32();

                        /////看这个order 在主表中是否存在 存在就分红 不存在 就不分
                        string strSQLIfHaveRunThisTable = "select count(1) from b006_TotalWealth_OperationUser where bool_ConsumeOrRecharge=1 and id<" + strID + " and consumeTypeOrRecharge like '%(订单" + intOrderID + ")%'";
                        Boolean boolExsit = BLLb006_TotalWealth_OperationUser.SelectList(strSQLIfHaveRunThisTable).Tables[0].Rows[0][0].toBoolean();
                        if (boolExsit == false) continue;
                        int intOrderCount = Data_DataTable008.Rows[j]["OrderCount"].toInt32();
                        intMemoryCountCount += intOrderCount;
                        if (intMemoryCountCount > intActionDoCount)
                        {
                            string strOut = "<br />strUserID=" + strUserID + " 打断当前循环strID=" + strID;
                            Response.Write(strOut + "  " + strConsumeTypeOrRechare);
                            Eggsoft.Common.debug_Log.Call_WriteLog(strOut, "打断当前循环", strOut);
                            break;////打断当前循环
                        }
                        int OrderDetailID = Data_DataTable008.Rows[j]["OrderDetailID"].toInt32();
                        Decimal DecimalOut = (ConsumeOrRechargeWealth * (Decimal)1.0 / intActionDoCount) * intOrderCount;

                        ///INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)
                        string strInsert = "insert into b006_TotalWealth_OperationUser ";
                        string strTable = @"([ID]
      ,[IDAdd]
      ,[UserID]
      ,[ShopClient_ID]
      ,[OrderDetailID]
      ,[UpdateTime]
      ,[ConsumeOrRechargeWealth]
      ,[ConsumeTypeOrRecharge]
      ,[RemainingSum]
      ,[Bool_ConsumeOrRecharge]
      ,[BoolIfOnlyonceUpdate]
      ,[CreatTime]
      ,[Creatby])";
                        string strValueTable = " values (";
                        strValueTable += ((BLLb006_TotalWealth_OperationUser.GetMaxId()).toString() + ",");
                        strValueTable += ("'" + strIDAdd + Eggsoft.Common.StringNum.Add000000Num(j, 6) + "',");
                        strValueTable += (strUserID + ",");
                        strValueTable += (21 + ",");
                        strValueTable += (OrderDetailID + ",");
                        strValueTable += ("getdate(),");
                        strValueTable += (DecimalOut + ",");
                        strValueTable += ("'" + strID + "订单详情返还" + strConsumeTypeOrRechare + "',");
                        strValueTable += ("null,");
                        strValueTable += ("0,");
                        strValueTable += ("1,");
                        strValueTable += ("getdate(),");
                        strValueTable += ("'订单详情追述'");
                        strValueTable += ")";
                        EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strInsert + strTable + strValueTable);

                    }

                    #region  循环完进行检查 总数是否正确
                    string strCheckSQLIfHaveRunThisTable = @"  SELECT sum(OrderCount) FROM (SELECT b008_OpterationUserActiveReturnMoneyOrderNum.PayDateTime, b008_OpterationUserActiveReturnMoneyOrderNum.OrderCount, 
      b006_TotalWealth_OperationUser.*
        FROM b006_TotalWealth_OperationUser LEFT OUTER JOIN
      b008_OpterationUserActiveReturnMoneyOrderNum ON 
      b006_TotalWealth_OperationUser.OrderDetailID = b008_OpterationUserActiveReturnMoneyOrderNum.OrderDetailID
       AND 
      b006_TotalWealth_OperationUser.ShopClient_ID = b008_OpterationUserActiveReturnMoneyOrderNum.ShopClient_ID
       AND 
      b006_TotalWealth_OperationUser.UserID = b008_OpterationUserActiveReturnMoneyOrderNum.UserID)[V666664]  where bool_ConsumeOrRecharge=0 and paydatetime is not null and consumeTypeOrRecharge like '" + strID + "订单详情返还每天分红财富，减增进入现金%'";
                    int intRecordCount = BLLb006_TotalWealth_OperationUser.SelectList(strCheckSQLIfHaveRunThisTable).Tables[0].Rows[0][0].toInt32();
                    if (intActionDoCount != intRecordCount)
                    {
                        Response.Write("<br />订单数目出错strUserID=" + strUserID + " strID = " + strID + "  intActionDoCount= " + intActionDoCount + " intRecordCount=  " + intRecordCount);
                    }
                    #endregion
                }
                else
                {
                    System.Data.DataTable Data_DataTable008 = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetList("*", "userID=@userID and datediff(dd,'" + dateWhere.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', Paydatetime)<0 order by Paydatetime desc", strUserID).Tables[0];



                    int intAllOrderCount = BLLb008_OpterationUserActiveReturnMoneyOrderNum.SelectList("select sum(OrderCount) from b008_OpterationUserActiveReturnMoneyOrderNum where userID=" + strUserID + " and datediff(ss,'" + dateWhere.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', Paydatetime)<0 ").Tables[0].Rows[0][0].toInt32();

                    if (Data_DataTable008.Rows.Count == 0)
                    {

                        string strOut = "<br />旧的数据行数为strUserID=" + strUserID + " strID =" + strID;
                        Response.Write(strOut + "  " + strConsumeTypeOrRechare);
                        Eggsoft.Common.debug_Log.Call_WriteLog(strOut, "旧的1数据行数", strOut);
                    }
                    else
                    {
                        string strUpdate = "update b006_TotalWealth_OperationUser set IDAdd='" + strIDAdd + Eggsoft.Common.StringNum.Add000000Num(8888, 6) + "' where id=" + strID;
                        //EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strUpdate);
                        pub_ArrayListUpdate.Add(strUpdate);
                    }


                    for (int j = 0; j < Data_DataTable008.Rows.Count; j++)
                    {
                        int intOrderCount = Data_DataTable008.Rows[j]["OrderCount"].toInt32();
                        int OrderDetailID = Data_DataTable008.Rows[j]["OrderDetailID"].toInt32();
                        Decimal DecimalOut = (ConsumeOrRechargeWealth * (Decimal)1.0 / intAllOrderCount) * intOrderCount;

                        ///INSERT INTO table_name (列1, 列2,...) VALUES (值1, 值2,....)
                        string strInsert = "insert into b006_TotalWealth_OperationUser ";
                        string strTable = @"([ID]
      ,[IDAdd]
      ,[UserID]
      ,[ShopClient_ID]
      ,[OrderDetailID]
      ,[UpdateTime]
      ,[ConsumeOrRechargeWealth]
      ,[ConsumeTypeOrRecharge]
      ,[RemainingSum]
      ,[Bool_ConsumeOrRecharge]
      ,[BoolIfOnlyonceUpdate]
      ,[CreatTime]
      ,[Creatby])";
                        string strValueTable = "  values (";
                        strValueTable += ((BLLb006_TotalWealth_OperationUser.GetMaxId()).toString() + ",");
                        strValueTable += ("'" + strIDAdd + Eggsoft.Common.StringNum.Add000000Num(j, 6) + "',");
                        strValueTable += (strUserID + ",");
                        strValueTable += (21 + ",");
                        strValueTable += (OrderDetailID + ",");
                        strValueTable += ("getdate(),");
                        strValueTable += (DecimalOut + ",");
                        strValueTable += ("'" + strID + "订单详情返还" + strConsumeTypeOrRechare + "',");
                        strValueTable += ("null,");
                        strValueTable += ("0,");
                        strValueTable += ("1,");
                        strValueTable += ("getdate(),");
                        strValueTable += ("'订单详情追述'");
                        strValueTable += ")";
                        EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strInsert + strTable + strValueTable);

                    }
                }
            }
            EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(pub_ArrayListUpdate);

            #region  打断当前循环strID
            /*
            2017 / 9 / 30 18:46:47
strUserID = 41205 打断当前循环strID = 67331 每天分红财富，减增进入现金(12605541001006877)
strUserID = 41205 打断当前循环strID = 68481 每天分红财富，减增进入现金(11866085001006903)
strUserID = 41205 打断当前循环strID = 69633 每天分红财富，减增进入现金(11891810001006934)
strUserID = 41205 打断当前循环strID = 70802 每天分红财富，减增进入现金(11980990001006987)
strUserID = 41205 打断当前循环strID = 71971 每天分红财富，减增进入现金(14087099001007034)
strUserID = 41205 打断当前循环strID = 73147 每天分红财富，减增进入现金(7448922001007075)
strUserID = 41205 打断当前循环strID = 74433 每天分红财富，减增进入现金(21982715001007765)
strUserID = 41205 打断当前循环strID = 75656 每天分红财富，减增进入现金(11891584001007805)
strUserID = 41205 打断当前循环strID = 76892 每天分红财富，减增进入现金(13520660001007865)
strUserID = 41205 打断当前循环strID = 78137 每天分红财富，减增进入现金(12872520001007941)
strUserID = 41210 打断当前循环strID = 61678 每天分红财富，减增进入现金(108049330027006272)
strUserID = 41210 打断当前循环strID = 62813 每天分红财富，减增进入现金(172679320027006612)
strUserID = 41210 打断当前循环strID = 63929 每天分红财富，减增进入现金(122207310027006714)
strUserID = 41210 打断当前循环strID = 65053 每天分红财富，减增进入现金(129452710027006767)
strUserID = 41210 打断当前循环strID = 66191 每天分红财富，减增进入现金(136816680027006835)
strUserID = 41210 打断当前循环strID = 67332 每天分红财富，减增进入现金(126055410027006877)
strUserID = 41210 打断当前循环strID = 68482 每天分红财富，减增进入现金(118660850027006903)
strUserID = 41210 打断当前循环strID = 69634 每天分红财富，减增进入现金(118918100028006934)
strUserID = 41210 打断当前循环strID = 70803 每天分红财富，减增进入现金(119809900028006987)
strUserID = 41210 打断当前循环strID = 71972 每天分红财富，减增进入现金(140870990028007034)
strUserID = 41210 打断当前循环strID = 73148 每天分红财富，减增进入现金(74489220028007075)
strUserID = 41210 打断当前循环strID = 74434 每天分红财富，减增进入现金(219827150028007765)
strUserID = 41210 打断当前循环strID = 75657 每天分红财富，减增进入现金(118915840028007805)
strUserID = 41210 打断当前循环strID = 76893 每天分红财富，减增进入现金(135206600028007865)
strUserID = 41210 打断当前循环strID = 78138 每天分红财富，减增进入现金(128725200028007941)
strUserID = 41230 打断当前循环strID = 61680 每天分红财富，减增进入现金(108049330041006272)
strUserID = 41230 打断当前循环strID = 62815 每天分红财富，减增进入现金(172679320041006612)
strUserID = 41230 打断当前循环strID = 63931 每天分红财富，减增进入现金(122207310041006714)
strUserID = 41230 打断当前循环strID = 65055 每天分红财富，减增进入现金(129452710041006767)
strUserID = 41230 打断当前循环strID = 66193 每天分红财富，减增进入现金(136816680042006835)
strUserID = 41230 打断当前循环strID = 67334 每天分红财富，减增进入现金(126055410042006877)
strUserID = 41230 打断当前循环strID = 68484 每天分红财富，减增进入现金(118660850042006903)
strUserID = 41230 打断当前循环strID = 69636 每天分红财富，减增进入现金(118918100042006934)
strUserID = 41230 打断当前循环strID = 70805 每天分红财富，减增进入现金(119809900042006987)
strUserID = 41230 打断当前循环strID = 71974 每天分红财富，减增进入现金(140870990042007034)
strUserID = 41230 打断当前循环strID = 73150 每天分红财富，减增进入现金(74489220042007075)
strUserID = 41230 打断当前循环strID = 74436 每天分红财富，减增进入现金(219827150042007765)
strUserID = 41230 打断当前循环strID = 75659 每天分红财富，减增进入现金(118915840042007805)
strUserID = 41230 打断当前循环strID = 76895 每天分红财富，减增进入现金(135206600042007865)
strUserID = 41230 打断当前循环strID = 78140 每天分红财富，减增进入现金(128725200042007941)
strUserID = 41296 打断当前循环strID = 61684 每天分红财富，减增进入现金(10804933001006272)
strUserID = 41296 打断当前循环strID = 62819 每天分红财富，减增进入现金(17267932002006612)
strUserID = 41296 打断当前循环strID = 63935 每天分红财富，减增进入现金(12220731002006714)
strUserID = 41296 打断当前循环strID = 65059 每天分红财富，减增进入现金(12945271002006767)
strUserID = 41296 打断当前循环strID = 66197 每天分红财富，减增进入现金(13681668002006835)
strUserID = 41296 打断当前循环strID = 67338 每天分红财富，减增进入现金(12605541002006877)
strUserID = 41296 打断当前循环strID = 68488 每天分红财富，减增进入现金(11866085002006903)
strUserID = 41296 打断当前循环strID = 69640 每天分红财富，减增进入现金(11891810002006934)
strUserID = 41296 打断当前循环strID = 70809 每天分红财富，减增进入现金(11980990002006987)
strUserID = 41296 打断当前循环strID = 71978 每天分红财富，减增进入现金(14087099002007034)
strUserID = 41296 打断当前循环strID = 73154 每天分红财富，减增进入现金(7448922002007075)
strUserID = 41296 打断当前循环strID = 74440 每天分红财富，减增进入现金(21982715002007765)
strUserID = 41296 打断当前循环strID = 75662 每天分红财富，减增进入现金(11891584002007805)
strUserID = 41296 打断当前循环strID = 76898 每天分红财富，减增进入现金(13520660002007865)
strUserID = 41296 打断当前循环strID = 78143 每天分红财富，减增进入现金(12872520002007941)
strUserID = 42147 打断当前循环strID = 61690 每天分红财富，减增进入现金(10804933005006272)
strUserID = 42147 打断当前循环strID = 62825 每天分红财富，减增进入现金(17267932005006612)
strUserID = 42147 打断当前循环strID = 63941 每天分红财富，减增进入现金(12220731005006714)
strUserID = 42147 打断当前循环strID = 65065 每天分红财富，减增进入现金(12945271005006767)
strUserID = 42147 打断当前循环strID = 66203 每天分红财富，减增进入现金(13681668005006835)
strUserID = 42147 打断当前循环strID = 67344 每天分红财富，减增进入现金(12605541005006877)
strUserID = 42147 打断当前循环strID = 68494 每天分红财富，减增进入现金(11866085005006903)
strUserID = 42147 打断当前循环strID = 69646 每天分红财富，减增进入现金(11891810005006934)
strUserID = 42147 打断当前循环strID = 70815 每天分红财富，减增进入现金(11980990005006987)
strUserID = 42147 打断当前循环strID = 71984 每天分红财富，减增进入现金(14087099005007034)
strUserID = 42147 打断当前循环strID = 73160 每天分红财富，减增进入现金(7448922005007075)
strUserID = 42147 打断当前循环strID = 74446 每天分红财富，减增进入现金(21982715005007765)
strUserID = 42147 打断当前循环strID = 75668 每天分红财富，减增进入现金(11891584005007805)
strUserID = 42147 打断当前循环strID = 76904 每天分红财富，减增进入现金(13520660005007865)
strUserID = 42147 打断当前循环strID = 78149 每天分红财富，减增进入现金(12872520005007941)
订单数目出错strUserID = 43327 strID = 39818 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 40629 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 41438 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 42315 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 43174 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 44051 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 44958 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 45878 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 46779 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 47692 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 48620 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 49558 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 50511 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 51515 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 52504 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 53514 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 54520 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 55537 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 56561 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 57597 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 58637 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 59688 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 60754 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 61817 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 62952 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 64068 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 65192 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 66330 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 67471 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 68621 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43327 strID = 69773 intActionDoCount = 3 intRecordCount = 2
订单数目出错strUserID = 43430 strID = 39785 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 40596 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 41405 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 42282 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 43141 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 44018 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 44925 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 45845 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 46746 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 47659 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 48587 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 49525 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 50478 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 51482 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 52471 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 53481 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 54487 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 55504 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 56528 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 57564 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 58604 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 59655 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 60721 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 61784 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 62919 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 64035 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 65159 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 66297 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 67438 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 68588 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 69740 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 70909 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 72078 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 73254 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 74540 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 75762 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 76998 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 43430 strID = 78243 intActionDoCount = 4 intRecordCount = 0
订单数目出错strUserID = 44323 strID = 45577 intActionDoCount = 12 intRecordCount = 2
旧的数据行数为strUserID = 44477 strID = 5204 每天分红财富，减增进入现金(24079.1400 - 200 - 1163)
旧的数据行数为strUserID = 44477 strID = 5498 每天分红财富，减增进入现金(37837.8000 - 200 - 1190)
旧的数据行数为strUserID = 44477 strID = 5803 每天分红财富，减增进入现金(35592.4800 - 200 - 1236)
旧的数据行数为strUserID = 44477 strID = 6122 每天分红财富，减增进入现金(36423.9600 - 200 - 1299)
旧的数据行数为strUserID = 44477 strID = 6158 平台操作
   旧的数据行数为strUserID = 45149 strID = 10433 每天分红财富，减增进入现金(39765.2100 - 100 - 1763)
旧的数据行数为strUserID = 45149 strID = 10438 平台操作
   旧的数据行数为strUserID = 46259 strID = 18637 每天分红财富，减增进入现金(57504.6000 - 900 - 2507)
旧的数据行数为strUserID = 46259 strID = 18659 平台操作
   strUserID = 46259 打断当前循环strID = 46323 每天分红财富，减增进入现金(11872920001004916)
订单数目出错strUserID = 46259 strID = 46323 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 47224 每天分红财富，减增进入现金(12925248001004957)
订单数目出错strUserID = 46259 strID = 47224 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 48137 每天分红财富，减增进入现金(12680198001005034)
订单数目出错strUserID = 46259 strID = 48137 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 49065 每天分红财富，减增进入现金(11779075001005101)
订单数目出错strUserID = 46259 strID = 49065 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 50003 每天分红财富，减增进入现金(13036218001005169)
订单数目出错strUserID = 46259 strID = 50003 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 50956 每天分红财富，减增进入现金(11694600001005262)
订单数目出错strUserID = 46259 strID = 50956 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 51960 每天分红财富，减增进入现金(14688108001005529)
订单数目出错strUserID = 46259 strID = 51960 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 52949 每天分红财富，减增进入现金(14200296001005666)
订单数目出错strUserID = 46259 strID = 52949 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 53959 每天分红财富，减增进入现金(14855165001005841)
订单数目出错strUserID = 46259 strID = 53959 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 54965 每天分红财富，减增进入现金(12576003001005913)
订单数目出错strUserID = 46259 strID = 54965 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 55982 每天分红财富，减增进入现金(14499810001005973)
订单数目出错strUserID = 46259 strID = 55982 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 57006 每天分红财富，减增进入现金(15185061001006023)
订单数目出错strUserID = 46259 strID = 57006 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 58042 每天分红财富，减增进入现金(15245430001006087)
订单数目出错strUserID = 46259 strID = 58042 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 59082 每天分红财富，减增进入现金(14188635001006135)
订单数目出错strUserID = 46259 strID = 59082 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 60133 每天分红财富，减增进入现金(15428669001006179)
订单数目出错strUserID = 46259 strID = 60133 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 61199 每天分红财富，减增进入现金(13323528001006266)
订单数目出错strUserID = 46259 strID = 61199 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 62262 每天分红财富，减增进入现金(10804933001006272)
订单数目出错strUserID = 46259 strID = 62262 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 63397 每天分红财富，减增进入现金(17267932001006612)
订单数目出错strUserID = 46259 strID = 63397 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 64513 每天分红财富，减增进入现金(12220731001006714)
订单数目出错strUserID = 46259 strID = 64513 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 65637 每天分红财富，减增进入现金(12945271001006767)
订单数目出错strUserID = 46259 strID = 65637 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 66775 每天分红财富，减增进入现金(13681668001006835)
订单数目出错strUserID = 46259 strID = 66775 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 67916 每天分红财富，减增进入现金(12605541001006877)
订单数目出错strUserID = 46259 strID = 67916 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 69066 每天分红财富，减增进入现金(11866085001006903)
订单数目出错strUserID = 46259 strID = 69066 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 70218 每天分红财富，减增进入现金(11891810001006934)
订单数目出错strUserID = 46259 strID = 70218 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 71387 每天分红财富，减增进入现金(11980990001006987)
订单数目出错strUserID = 46259 strID = 71387 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 72556 每天分红财富，减增进入现金(14087099001007034)
订单数目出错strUserID = 46259 strID = 72556 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 73732 每天分红财富，减增进入现金(7448922001007075)
订单数目出错strUserID = 46259 strID = 73732 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 75018 每天分红财富，减增进入现金(21982715001007765)
订单数目出错strUserID = 46259 strID = 75018 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 76240 每天分红财富，减增进入现金(11891584001007805)
订单数目出错strUserID = 46259 strID = 76240 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 77476 每天分红财富，减增进入现金(13520660001007865)
订单数目出错strUserID = 46259 strID = 77476 intActionDoCount = 1 intRecordCount = 0
strUserID = 46259 打断当前循环strID = 78721 每天分红财富，减增进入现金(12872520001007941)
订单数目出错strUserID = 46259 strID = 78721 intActionDoCount = 1 intRecordCount = 0
strUserID = 46269 打断当前循环strID = 77772 每天分红财富，减增进入现金(135206600011007865)
订单数目出错strUserID = 46269 strID = 77772 intActionDoCount = 11 intRecordCount = 10
strUserID = 46269 打断当前循环strID = 79017 每天分红财富，减增进入现金(128725200011007941)
订单数目出错strUserID = 46269 strID = 79017 intActionDoCount = 11 intRecordCount = 10
订单数目出错strUserID = 46365 strID = 46258 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 47159 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 48072 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 49000 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 49938 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 50891 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 51895 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 52884 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 53894 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 54900 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 55917 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 56941 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 57977 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 59017 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 60068 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 61134 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 62197 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 63332 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 64448 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 65572 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 66710 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 67851 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 69001 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 70153 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 71322 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 72491 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 73667 intActionDoCount = 19 intRecordCount = 9
订单数目出错strUserID = 46365 strID = 74953 intActionDoCount = 39 intRecordCount = 29
订单数目出错strUserID = 46365 strID = 76175 intActionDoCount = 39 intRecordCount = 29
订单数目出错strUserID = 46365 strID = 77411 intActionDoCount = 39 intRecordCount = 29
订单数目出错strUserID = 46365 strID = 78656 intActionDoCount = 39 intRecordCount = 29
平台操作的问题 strUserID = 41263 strID = 1260 strOrderDetailID = 9840 5994.00
平台操作的问题 strUserID = 41199 strID = 1261 strOrderDetailID = 9841 11988.00
平台操作的问题 strUserID = 41205 strID = 1263 strOrderDetailID = 9843 5994.00
平台操作的问题 strUserID = 41324 strID = 1271 strOrderDetailID = 9851 4423.79
平台操作的问题 strUserID = 42977 strID = 1285 strOrderDetailID = 9865 7643.90
平台操作的问题 strUserID = 43113 strID = 1291 strOrderDetailID = 9871 33497.68
平台操作的问题 strUserID = 43117 strID = 1292 strOrderDetailID = 9872 7621.41
平台操作的问题 strUserID = 41324 strID = 1306 strOrderDetailID = 9887 13969.99
平台操作的问题 strUserID = 43072 strID = 1307 strOrderDetailID = 9888 3335.77
平台操作的问题 strUserID = 43072 strID = 1312 strOrderDetailID = 9894 16598.29
平台操作的问题 strUserID = 42977 strID = 1317 strOrderDetailID = 9899 9898.15
平台操作的问题 strUserID = 43122 strID = 1318 strOrderDetailID = 9900 3403.44
平台操作的问题 strUserID = 43120 strID = 1319 strOrderDetailID = 9901 11797.88
平台操作的问题 strUserID = 43019 strID = 1326 strOrderDetailID = 9927 3591.66
平台操作的问题 strUserID = 43110 strID = 1344 strOrderDetailID = 9954 3406.13
平台操作的问题 strUserID = 43072 strID = 1379 strOrderDetailID = 10002 3335.77
平台操作的问题 strUserID = 41192 strID = 1381 strOrderDetailID = 10015 8255.81
平台操作的问题 strUserID = 43327 strID = 1423 strOrderDetailID = 10094 6354.95
平台操作的问题 strUserID = 43612 strID = 1450 strOrderDetailID = 10121 2803.56
平台操作的问题 strUserID = 43122 strID = 1468 strOrderDetailID = 10150 2860.25
平台操作的问题 strUserID = 43122 strID = 1479 strOrderDetailID = 10180 2833.69
平台操作的问题 strUserID = 43120 strID = 1506 strOrderDetailID = 10216 15987.55
平台操作的问题 strUserID = 43072 strID = 1507 strOrderDetailID = 10217 5402.48
平台操作的问题 strUserID = 43594 strID = 1517 strOrderDetailID = 10229 2626.32
平台操作的问题 strUserID = 43594 strID = 1553 strOrderDetailID = 10274 2511.15
平台操作的问题 strUserID = 43122 strID = 1581 strOrderDetailID = 10316 2791.85
平台操作的问题 strUserID = 43594 strID = 1583 strOrderDetailID = 10318 2447.47
平台操作的问题 strUserID = 44266 strID = 1593 strOrderDetailID = 10335 2408.84
平台操作的问题 strUserID = 43122 strID = 1600 strOrderDetailID = 10347 2517.01
平台操作的问题 strUserID = 43594 strID = 1610 strOrderDetailID = 10365 4634.16
平台操作的问题 strUserID = 44499 strID = 1648 strOrderDetailID = 10425 4479.11
平台操作的问题 strUserID = 44051 strID = 1669 strOrderDetailID = 10448 2566.24
平台操作的问题 strUserID = 44631 strID = 1674 strOrderDetailID = 10456 4535.19
平台操作的问题 strUserID = 43612 strID = 1678 strOrderDetailID = 10460 2498.54
平台操作的问题 strUserID = 43019 strID = 1692 strOrderDetailID = 10475 2333.20
平台操作的问题 strUserID = 44171 strID = 1703 strOrderDetailID = 10487 4949.64
平台操作的问题 strUserID = 43971 strID = 1753 strOrderDetailID = 10557 5013.06
平台操作的问题 strUserID = 44539 strID = 1797 strOrderDetailID = 10614 2209.03
平台操作的问题 strUserID = 44288 strID = 1799 strOrderDetailID = 10616 10413.28
平台操作的问题 strUserID = 43122 strID = 1800 strOrderDetailID = 10618 2225.73
平台操作的问题 strUserID = 41324 strID = 1812 strOrderDetailID = 10648 4002.09
平台操作的问题 strUserID = 43113 strID = 1847 strOrderDetailID = 10692 5924.23
平台操作的问题 strUserID = 43117 strID = 1849 strOrderDetailID = 10694 3992.60
平台操作的问题 strUserID = 44288 strID = 1851 strOrderDetailID = 10710 9397.98
平台操作的问题 strUserID = 44288 strID = 1852 strOrderDetailID = 10711 9354.78
平台操作的问题 strUserID = 43594 strID = 1854 strOrderDetailID = 10715 4577.35
平台操作的问题 strUserID = 43072 strID = 1855 strOrderDetailID = 10716 20744.17
平台操作的问题 strUserID = 45265 strID = 1857 strOrderDetailID = 10719 1952.30
平台操作的问题 strUserID = 43594 strID = 1864 strOrderDetailID = 10728 3842.35
平台操作的问题 strUserID = 44171 strID = 1870 strOrderDetailID = 10734 5664.38
平台操作的问题 strUserID = 41324 strID = 1875 strOrderDetailID = 10740 1862.23
平台操作的问题 strUserID = 45253 strID = 1876 strOrderDetailID = 10741 2080.00
平台操作的问题 strUserID = 44288 strID = 1879 strOrderDetailID = 10748 9317.57
平台操作的问题 strUserID = 41324 strID = 1889 strOrderDetailID = 10758 3679.73
平台操作的问题 strUserID = 44288 strID = 1904 strOrderDetailID = 10774 9084.37
平台操作的问题 strUserID = 44051 strID = 1929 strOrderDetailID = 10818 1789.80
平台操作的问题 strUserID = 43019 strID = 1935 strOrderDetailID = 10833 1988.94
平台操作的问题 strUserID = 45406 strID = 1947 strOrderDetailID = 10846 5206.18
平台操作的问题 strUserID = 45384 strID = 1968 strOrderDetailID = 10871 1888.15
平台操作的问题 strUserID = 45406 strID = 2012 strOrderDetailID = 10923 5369.26
平台操作的问题 strUserID = 43072 strID = 2047 strOrderDetailID = 10977 4726.08
平台操作的问题 strUserID = 46162 strID = 2076 strOrderDetailID = 11020 7483.83
平台操作的问题 strUserID = 45265 strID = 2085 strOrderDetailID = 11030 1522.63
平台操作的问题 strUserID = 43594 strID = 2088 strOrderDetailID = 11034 7497.89
平台操作的问题 strUserID = 43019 strID = 2100 strOrderDetailID = 11047 3004.49
平台操作的问题 strUserID = 43117 strID = 2115 strOrderDetailID = 11062 2980.08
平台操作的问题 strUserID = 43327 strID = 2127 strOrderDetailID = 11080 4924.60
平台操作的问题 strUserID = 46259 strID = 2152 strOrderDetailID = 11105 6207.96
平台操作的问题 strUserID = 46365 strID = 2162 strOrderDetailID = 11118 13496.12
平台操作的问题 strUserID = 41192 strID = 2166 strOrderDetailID = 11123 6650.49
平台操作的问题 strUserID = 45968 strID = 2186 strOrderDetailID = 11167 1381.32
平台操作的问题 strUserID = 45968 strID = 2300 strOrderDetailID = 11318 1381.32
平台操作的问题 strUserID = 43117 strID = 2325 strOrderDetailID = 11350 2340.40
平台操作的问题 strUserID = 46999 strID = 2342 strOrderDetailID = 11384 1332.42
平台操作的问题 strUserID = 43019 strID = 2347 strOrderDetailID = 11389 2302.63
平台操作的问题 strUserID = 43612 strID = 2414 strOrderDetailID = 11468 5106.68
平台操作的问题 strUserID = 44323 strID = 2435 strOrderDetailID = 11506 61648.72
平台操作的问题 strUserID = 42977 strID = 2442 strOrderDetailID = 11514 10042.09
平台操作的问题 strUserID = 45279 strID = 2459 strOrderDetailID = 11552 5417.89
平台操作的问题 strUserID = 41324 strID = 2487 strOrderDetailID = 11580 1847.27
平台操作的问题 strUserID = 41324 strID = 2488 strOrderDetailID = 11586 2695.80
平台操作的问题 strUserID = 43113 strID = 2521 strOrderDetailID = 11635 822.97
平台操作的问题 strUserID = 42977 strID = 2538 strOrderDetailID = 11672 9405.05
平台操作的问题 strUserID = 46259 strID = 2630 strOrderDetailID = 11787 55565.00
平台操作的问题 strUserID = 46999 strID = 2631 strOrderDetailID = 11788 912.59
平台操作的问题 strUserID = 46999 strID = 2646 strOrderDetailID = 11804 879.40
平台操作的问题 strUserID = 48094 strID = 2680 strOrderDetailID = 11850 59940.00
平台操作的问题 strUserID = 48099 strID = 2681 strOrderDetailID = 11851 59940.00
平台操作的问题 strUserID = 48091 strID = 2692 strOrderDetailID = 11864 30102.99
平台操作的问题 strUserID = 48092 strID = 2693 strOrderDetailID = 11865 30102.99
平台操作的问题 strUserID = 48096 strID = 2694 strOrderDetailID = 11866 30102.99
平台操作的问题 strUserID = 48098 strID = 2695 strOrderDetailID = 11867 30102.99
平台操作的问题 strUserID = 48091 strID = 2696 strOrderDetailID = 11869 29837.02
平台操作的问题 strUserID = 48092 strID = 2697 strOrderDetailID = 11870 29837.02
平台操作的问题 strUserID = 48096 strID = 2698 strOrderDetailID = 11872 29837.02
平台操作的问题 strUserID = 48098 strID = 2699 strOrderDetailID = 11873 29837.02
平台操作的问题 strUserID = 44387 strID = 2700 strOrderDetailID = 11874 0
平台操作的问题 strUserID = 44387 strID = 2704 strOrderDetailID = 11878 59940.00
平台操作的问题 strUserID = 43072 strID = 2719 strOrderDetailID = 11893 3463.93
平台操作的问题 strUserID = 43072 strID = 2724 strOrderDetailID = 11898 6927.83
平台操作的问题 strUserID = 46269 strID = 2727 strOrderDetailID = 11901 2505.93
平台操作的问题 strUserID = 43612 strID = 2730 strOrderDetailID = 11907 3264.73
平台操作的问题 strUserID = 43019 strID = 2753 strOrderDetailID = 11943 668.88
平台操作的问题 strUserID = 43072 strID = 2756 strOrderDetailID = 11946 6661.87
平台操作的问题 strUserID = 43594 strID = 2771 strOrderDetailID = 11961 642.05
平台操作的问题 strUserID = 45968 strID = 2802 strOrderDetailID = 12020 615.98
平台操作的问题 strUserID = 43019 strID = 2827 strOrderDetailID = 12051 493.69
平台操作的问题 strUserID = 43019 strID = 2829 strOrderDetailID = 12056 644.73
平台操作的问题 strUserID = 43117 strID = 2851 strOrderDetailID = 12079 468.63
平台操作的问题 strUserID = 43110 strID = 2866 strOrderDetailID = 12095 629.36
平台操作的问题 strUserID = 48308 strID = 2867 strOrderDetailID = 12096 1285.30
平台操作的问题 strUserID = 43117 strID = 3064 strOrderDetailID = 12330 443.20
平台操作的问题 strUserID = 46999 strID = 3067 strOrderDetailID = 12333 1164.32
平台操作的问题 strUserID = 43113 strID = 3111 strOrderDetailID = 12387 521.56
平台操作的问题 strUserID = 45279 strID = 3131 strOrderDetailID = 12425 2102.64
平台操作的问题 strUserID = 46269 strID = 3148 strOrderDetailID = 12446 10502.54
平台操作的问题 strUserID = 41205 strID = 3246 strOrderDetailID = 12566 177.33
平台操作的问题 strUserID = 46269 strID = 3314 strOrderDetailID = 12658 9803.00
平台操作的问题 strUserID = 43122 strID = 3328 strOrderDetailID = 12689 48.64
平台操作的问题 strUserID = 48308 strID = 3354 strOrderDetailID = 12721 384.74
平台操作的问题 strUserID = 43612 strID = 3356 strOrderDetailID = 12723 384.74
平台操作的问题 strUserID = 46269 strID = 3404 strOrderDetailID = 12774 9750.36
平台操作的问题 strUserID = 45279 strID = 3409 strOrderDetailID = 12779 769.47
平台操作的问题 strUserID = 43019 strID = 3414 strOrderDetailID = 12784 76.95
平台操作的问题 strUserID = 45279 strID = 3436 strOrderDetailID = 12810 243.18
平台操作的问题 strUserID = 43117 strID = 3464 strOrderDetailID = 12855 230.84
平台操作的问题 strUserID = 42977 strID = 3469 strOrderDetailID = 12860 1538.94
平台操作的问题 strUserID = 46365 strID = 3473 strOrderDetailID = 12864 1538.95
可以接受的误差 strUserID = 41199 ReturnALlMoneyUnit = 0.00 strOrderDetailID = 0.00
可以接受的误差 strUserID = 41205 ReturnALlMoneyUnit = 5816.67 strOrderDetailID = 5816.67
已经返还的 大于现在订单统计的 strUserID = 41210 ReturnALlMoneyUnit = 149068.84 orderDescimal = 144072.66
可以接受的误差 strUserID = 41220 ReturnALlMoneyUnit = 3959.96 strOrderDetailID = 3959.96
已经返还的 小于现在订单统计的 strUserID = 41230 ReturnALlMoneyUnit = 220473.00 orderDescimal = 220530.82
已经返还的 大于现在订单统计的 strUserID = 41256 ReturnALlMoneyUnit = 0.00 orderDescimal = -4795.20
可以接受的误差 strUserID = 41257 ReturnALlMoneyUnit = 2104.50 strOrderDetailID = 2104.50
可以接受的误差 strUserID = 41263 ReturnALlMoneyUnit = 0.00 strOrderDetailID = 0.00
可以接受的误差 strUserID = 41295 ReturnALlMoneyUnit = 3350.92 strOrderDetailID = 3350.44
可以接受的误差 strUserID = 41296 ReturnALlMoneyUnit = 9773.76 strOrderDetailID = 9773.29
可以接受的误差 strUserID = 41324 ReturnALlMoneyUnit = 57429.12 strOrderDetailID = 57429.10
可以接受的误差 strUserID = 41434 ReturnALlMoneyUnit = 5907.93 strOrderDetailID = 5907.93
可以接受的误差 strUserID = 41522 ReturnALlMoneyUnit = 14575.99 strOrderDetailID = 14575.99
可以接受的误差 strUserID = 41595 ReturnALlMoneyUnit = 1296.91 strOrderDetailID = 1296.91
可以接受的误差 strUserID = 41833 ReturnALlMoneyUnit = 1467.91 strOrderDetailID = 1467.91
应该出局 strUserID = 42147 curReturnMoneyUnit = 1186.89 ActiveOrderNumcurOrderID = 1
可以接受的误差 strUserID = 42147 ReturnALlMoneyUnit = 19516.02 strOrderDetailID = 19515.88
可以接受的误差 strUserID = 42570 ReturnALlMoneyUnit = 1467.91 strOrderDetailID = 1467.91
可以接受的误差 strUserID = 42577 ReturnALlMoneyUnit = 39456.81 strOrderDetailID = 39456.67
可以接受的误差 strUserID = 42583 ReturnALlMoneyUnit = 1617.91 strOrderDetailID = 1617.91
可以接受的误差 strUserID = 42642 ReturnALlMoneyUnit = 1467.91 strOrderDetailID = 1467.91
可以接受的误差 strUserID = 42780 ReturnALlMoneyUnit = 8407.76 strOrderDetailID = 8407.76
可以接受的误差 strUserID = 42781 ReturnALlMoneyUnit = 1370.91 strOrderDetailID = 1370.91
已经返还的 小于现在订单统计的 strUserID = 42977 ReturnALlMoneyUnit = 230891.79 orderDescimal = 231201.87
已经返还的 大于现在订单统计的 strUserID = 42985 ReturnALlMoneyUnit = 191425.06 orderDescimal = 79113.67
可以接受的误差 strUserID = 42989 ReturnALlMoneyUnit = 19959.94 strOrderDetailID = 19959.94
可以接受的误差 strUserID = 43108 ReturnALlMoneyUnit = 1679.91 strOrderDetailID = 1679.91
可以接受的误差 strUserID = 43111 ReturnALlMoneyUnit = 145633.30 strOrderDetailID = 145632.99
已经返还的 大于现在订单统计的 strUserID = 43113 ReturnALlMoneyUnit = 37514.76 orderDescimal = 19173.56
可以接受的误差 strUserID = 43117 ReturnALlMoneyUnit = 59844.92 strOrderDetailID = 59844.84
可以接受的误差 strUserID = 43123 ReturnALlMoneyUnit = 12627.44 strOrderDetailID = 12627.44
已经返还的 大于现在订单统计的 strUserID = 43131 ReturnALlMoneyUnit = 5928.49 orderDescimal = 5744.74
可以接受的误差 strUserID = 43132 ReturnALlMoneyUnit = 29350.88 strOrderDetailID = 29350.84
可以接受的误差 strUserID = 43136 ReturnALlMoneyUnit = 11515.20 strOrderDetailID = 11515.20
可以接受的误差 strUserID = 43140 ReturnALlMoneyUnit = 2195.84 strOrderDetailID = 2195.84
可以接受的误差 strUserID = 43141 ReturnALlMoneyUnit = 6246.26 strOrderDetailID = 6245.88
可以接受的误差 strUserID = 43142 ReturnALlMoneyUnit = 2254.79 strOrderDetailID = 2254.79
可以接受的误差 strUserID = 43145 ReturnALlMoneyUnit = 2195.84 strOrderDetailID = 2195.84
可以接受的误差 strUserID = 43153 ReturnALlMoneyUnit = 2077.86 strOrderDetailID = 2077.86
可以接受的误差 strUserID = 43157 ReturnALlMoneyUnit = 2399.17 strOrderDetailID = 2399.17
可以接受的误差 strUserID = 43177 ReturnALlMoneyUnit = 14657.14 strOrderDetailID = 14656.75
可以接受的误差 strUserID = 43164 ReturnALlMoneyUnit = 2439.13 strOrderDetailID = 2439.13
可以接受的误差 strUserID = 43203 ReturnALlMoneyUnit = 2439.13 strOrderDetailID = 2439.13
可以接受的误差 strUserID = 43159 ReturnALlMoneyUnit = 2439.13 strOrderDetailID = 2439.13
可以接受的误差 strUserID = 43178 ReturnALlMoneyUnit = 2495.07 strOrderDetailID = 2495.07
可以接受的误差 strUserID = 43144 ReturnALlMoneyUnit = 2547.11 strOrderDetailID = 2547.11
可以接受的误差 strUserID = 43185 ReturnALlMoneyUnit = 2547.11 strOrderDetailID = 2547.11
可以接受的误差 strUserID = 43116 ReturnALlMoneyUnit = 5094.28 strOrderDetailID = 5094.28
可以接受的误差 strUserID = 43182 ReturnALlMoneyUnit = 2495.07 strOrderDetailID = 2495.07
可以接受的误差 strUserID = 43200 ReturnALlMoneyUnit = 2495.07 strOrderDetailID = 2495.07
可以接受的误差 strUserID = 43160 ReturnALlMoneyUnit = 2439.13 strOrderDetailID = 2439.13
可以接受的误差 strUserID = 43156 ReturnALlMoneyUnit = 2439.13 strOrderDetailID = 2439.13
可以接受的误差 strUserID = 43158 ReturnALlMoneyUnit = 37942.55 strOrderDetailID = 37942.52
已经返还的 小于现在订单统计的 strUserID = 43181 ReturnALlMoneyUnit = 128058.52 orderDescimal = 128146.16
已经返还的 大于现在订单统计的 strUserID = 43019 ReturnALlMoneyUnit = 50957.05 orderDescimal = 50828.83
可以接受的误差 strUserID = 43179 ReturnALlMoneyUnit = 12305.25 strOrderDetailID = 12304.67
可以接受的误差 strUserID = 43143 ReturnALlMoneyUnit = 12125.26 strOrderDetailID = 12125.11
可以接受的误差 strUserID = 43133 ReturnALlMoneyUnit = 2254.79 strOrderDetailID = 2254.79
可以接受的误差 strUserID = 43155 ReturnALlMoneyUnit = 2143.50 strOrderDetailID = 2143.50
可以接受的误差 strUserID = 43146 ReturnALlMoneyUnit = 2195.84 strOrderDetailID = 2195.84
可以接受的误差 strUserID = 41306 ReturnALlMoneyUnit = 2547.11 strOrderDetailID = 2547.11
可以接受的误差 strUserID = 43137 ReturnALlMoneyUnit = 2547.11 strOrderDetailID = 2547.11
可以接受的误差 strUserID = 43161 ReturnALlMoneyUnit = 2547.11 strOrderDetailID = 2547.11
可以接受的误差 strUserID = 43077 ReturnALlMoneyUnit = 2495.07 strOrderDetailID = 2495.07
可以接受的误差 strUserID = 43126 ReturnALlMoneyUnit = 33531.03 strOrderDetailID = 33530.65
已经返还的 大于现在订单统计的 strUserID = 43072 ReturnALlMoneyUnit = 210759.90 orderDescimal = 210521.81
可以接受的误差 strUserID = 43210 ReturnALlMoneyUnit = 91733.51 strOrderDetailID = 91733.34
可以接受的误差 strUserID = 43110 ReturnALlMoneyUnit = 7952.63 strOrderDetailID = 7952.51
可以接受的误差 strUserID = 43134 ReturnALlMoneyUnit = 17195.08 strOrderDetailID = 17194.94
可以接受的误差 strUserID = 43211 ReturnALlMoneyUnit = 88908.00 strOrderDetailID = 88907.92
可以接受的误差 strUserID = 43213 ReturnALlMoneyUnit = 64098.72 strOrderDetailID = 64098.27
可以接受的误差 strUserID = 43149 ReturnALlMoneyUnit = 11739.16 strOrderDetailID = 11739.16
已经返还的 大于现在订单统计的 strUserID = 43209 ReturnALlMoneyUnit = 57658.52 orderDescimal = 13464.10
已经返还的 大于现在订单统计的 strUserID = 43215 ReturnALlMoneyUnit = 67109.23 orderDescimal = 62567.88
可以接受的误差 strUserID = 43212 ReturnALlMoneyUnit = 769745.45 strOrderDetailID = 769744.90
已经返还的 小于现在订单统计的 strUserID = 43082 ReturnALlMoneyUnit = 48842.63 orderDescimal = 49116.25
可以接受的误差 strUserID = 43119 ReturnALlMoneyUnit = 7803.81 strOrderDetailID = 7803.81
可以接受的误差 strUserID = 43114 ReturnALlMoneyUnit = 2601.25 strOrderDetailID = 2601.25
可以接受的误差 strUserID = 43112 ReturnALlMoneyUnit = 5202.56 strOrderDetailID = 5202.56
可以接受的误差 strUserID = 43167 ReturnALlMoneyUnit = 12146.11 strOrderDetailID = 12145.86
可以接受的误差 strUserID = 43214 ReturnALlMoneyUnit = 6834.74 strOrderDetailID = 6834.44
可以接受的误差 strUserID = 43154 ReturnALlMoneyUnit = 2657.57 strOrderDetailID = 2657.57
可以接受的误差 strUserID = 43183 ReturnALlMoneyUnit = 7314.61 strOrderDetailID = 7314.39
可以接受的误差 strUserID = 43163 ReturnALlMoneyUnit = 111442.30 strOrderDetailID = 111442.21
可以接受的误差 strUserID = 43173 ReturnALlMoneyUnit = 2657.57 strOrderDetailID = 2657.57
可以接受的误差 strUserID = 43127 ReturnALlMoneyUnit = 2657.57 strOrderDetailID = 2657.57
可以接受的误差 strUserID = 43162 ReturnALlMoneyUnit = 53787.28 strOrderDetailID = 53787.14
可以接受的误差 strUserID = 43169 ReturnALlMoneyUnit = 2657.57 strOrderDetailID = 2657.57
可以接受的误差 strUserID = 43165 ReturnALlMoneyUnit = 120912.92 strOrderDetailID = 120912.56
可以接受的误差 strUserID = 43125 ReturnALlMoneyUnit = 78520.70 strOrderDetailID = 78520.53
可以接受的误差 strUserID = 43086 ReturnALlMoneyUnit = 238345.52 strOrderDetailID = 238345.07
可以接受的误差 strUserID = 43105 ReturnALlMoneyUnit = 103937.79 strOrderDetailID = 103937.79
可以接受的误差 strUserID = 43226 ReturnALlMoneyUnit = 99850.87 strOrderDetailID = 99850.84
可以接受的误差 strUserID = 43139 ReturnALlMoneyUnit = 2706.22 strOrderDetailID = 2706.22
可以接受的误差 strUserID = 43138 ReturnALlMoneyUnit = 2706.22 strOrderDetailID = 2706.22
可以接受的误差 strUserID = 43124 ReturnALlMoneyUnit = 2706.22 strOrderDetailID = 2706.22
可以接受的误差 strUserID = 43118 ReturnALlMoneyUnit = 11249.21 strOrderDetailID = 11248.96
已经返还的 大于现在订单统计的 strUserID = 43120 ReturnALlMoneyUnit = 14706.51 orderDescimal = 2184.57
已经返还的 大于现在订单统计的 strUserID = 43122 ReturnALlMoneyUnit = 25377.77 orderDescimal = 25277.39
可以接受的误差 strUserID = 43240 ReturnALlMoneyUnit = 2757.50 strOrderDetailID = 2757.50
可以接受的误差 strUserID = 43168 ReturnALlMoneyUnit = 5515.06 strOrderDetailID = 5515.06
可以接受的误差 strUserID = 42448 ReturnALlMoneyUnit = 2757.50 strOrderDetailID = 2757.50
可以接受的误差 strUserID = 43216 ReturnALlMoneyUnit = 13513.57 strOrderDetailID = 13513.57
可以接受的误差 strUserID = 43204 ReturnALlMoneyUnit = 173211.40 strOrderDetailID = 173211.28
可以接受的误差 strUserID = 43310 ReturnALlMoneyUnit = 11592.78 strOrderDetailID = 11592.73
可以接受的误差 strUserID = 43042 ReturnALlMoneyUnit = 2777.72 strOrderDetailID = 2777.72
可以接受的误差 strUserID = 43232 ReturnALlMoneyUnit = 40335.40 strOrderDetailID = 40335.27
可以接受的误差 strUserID = 43206 ReturnALlMoneyUnit = 27332.26 strOrderDetailID = 27332.32
可以接受的误差 strUserID = 43221 ReturnALlMoneyUnit = 2813.47 strOrderDetailID = 2813.47
可以接受的误差 strUserID = 43099 ReturnALlMoneyUnit = 2706.22 strOrderDetailID = 2706.22
已经返还的 小于现在订单统计的 strUserID = 43430 ReturnALlMoneyUnit = 10876.05 orderDescimal = 15884.28
可以接受的误差 strUserID = 43346 ReturnALlMoneyUnit = 8475.19 strOrderDetailID = 8475.11
可以接受的误差 strUserID = 41977 ReturnALlMoneyUnit = 7999.42 strOrderDetailID = 7999.26
可以接受的误差 strUserID = 43255 ReturnALlMoneyUnit = 98837.32 strOrderDetailID = 98837.22
可以接受的误差 strUserID = 42975 ReturnALlMoneyUnit = 5707.20 strOrderDetailID = 5707.20
已经返还的 小于现在订单统计的 strUserID = 43150 ReturnALlMoneyUnit = 684372.90 orderDescimal = 684695.54
可以接受的误差 strUserID = 43236 ReturnALlMoneyUnit = 55057.64 strOrderDetailID = 55057.64
可以接受的误差 strUserID = 43220 ReturnALlMoneyUnit = 5707.20 strOrderDetailID = 5707.20
可以接受的误差 strUserID = 43239 ReturnALlMoneyUnit = 15576.43 strOrderDetailID = 15576.25
可以接受的误差 strUserID = 43278 ReturnALlMoneyUnit = 2853.57 strOrderDetailID = 2853.57
可以接受的误差 strUserID = 43270 ReturnALlMoneyUnit = 162931.03 strOrderDetailID = 162930.64
可以接受的误差 strUserID = 43274 ReturnALlMoneyUnit = 11221.31 strOrderDetailID = 11220.94
可以接受的误差 strUserID = 43260 ReturnALlMoneyUnit = 139723.56 strOrderDetailID = 139723.38
可以接受的误差 strUserID = 43268 ReturnALlMoneyUnit = 2905.21 strOrderDetailID = 2905.21
可以接受的误差 strUserID = 43296 ReturnALlMoneyUnit = 15921.55 strOrderDetailID = 15921.55
可以接受的误差 strUserID = 43276 ReturnALlMoneyUnit = 23827.88 strOrderDetailID = 23827.78
已经返还的 小于现在订单统计的 strUserID = 43365 ReturnALlMoneyUnit = 32922.49 orderDescimal = 33050.35
可以接受的误差 strUserID = 43279 ReturnALlMoneyUnit = 59305.42 strOrderDetailID = 59304.82
可以接受的误差 strUserID = 43277 ReturnALlMoneyUnit = 5884.48 strOrderDetailID = 5884.48
可以接受的误差 strUserID = 43319 ReturnALlMoneyUnit = 24487.91 strOrderDetailID = 24487.85
可以接受的误差 strUserID = 42768 ReturnALlMoneyUnit = 8088.06 strOrderDetailID = 8087.90
可以接受的误差 strUserID = 43329 ReturnALlMoneyUnit = 2942.21 strOrderDetailID = 2942.21
可以接受的误差 strUserID = 43333 ReturnALlMoneyUnit = 2942.21 strOrderDetailID = 2942.21
可以接受的误差 strUserID = 43307 ReturnALlMoneyUnit = 15464.19 strOrderDetailID = 15463.73
可以接受的误差 strUserID = 43295 ReturnALlMoneyUnit = 14711.15 strOrderDetailID = 14711.15
已经返还的 大于现在订单统计的 strUserID = 41192 ReturnALlMoneyUnit = 7581.93 orderDescimal = -2918.30
已经返还的 小于现在订单统计的 strUserID = 42674 ReturnALlMoneyUnit = 404169.95 orderDescimal = 404205.01
可以接受的误差 strUserID = 43326 ReturnALlMoneyUnit = 5976.60 strOrderDetailID = 5976.60
可以接受的误差 strUserID = 43337 ReturnALlMoneyUnit = 25184.08 strOrderDetailID = 25184.04
可以接受的误差 strUserID = 43324 ReturnALlMoneyUnit = 49780.68 strOrderDetailID = 49780.61
可以接受的误差 strUserID = 43323 ReturnALlMoneyUnit = 146000.75 strOrderDetailID = 146000.73
可以接受的误差 strUserID = 43281 ReturnALlMoneyUnit = 2988.27 strOrderDetailID = 2988.27
可以接受的误差 strUserID = 43322 ReturnALlMoneyUnit = 14941.45 strOrderDetailID = 14941.45
已经返还的 大于现在订单统计的 strUserID = 43327 ReturnALlMoneyUnit = 5979.10 orderDescimal = 708.45
可以接受的误差 strUserID = 43338 ReturnALlMoneyUnit = 33660.35 strOrderDetailID = 33659.94
可以接受的误差 strUserID = 43352 ReturnALlMoneyUnit = 2988.27 strOrderDetailID = 2988.27
可以接受的误差 strUserID = 43345 ReturnALlMoneyUnit = 8634.93 strOrderDetailID = 8634.86
可以接受的误差 strUserID = 43272 ReturnALlMoneyUnit = 5976.60 strOrderDetailID = 5976.60
可以接受的误差 strUserID = 43355 ReturnALlMoneyUnit = 37648.17 strOrderDetailID = 37647.93
可以接受的误差 strUserID = 43362 ReturnALlMoneyUnit = 39196.35 strOrderDetailID = 39196.26
可以接受的误差 strUserID = 43372 ReturnALlMoneyUnit = 59031.12 strOrderDetailID = 59031.12
可以接受的误差 strUserID = 43382 ReturnALlMoneyUnit = 12269.68 strOrderDetailID = 12269.68
可以接受的误差 strUserID = 43381 ReturnALlMoneyUnit = 3067.39 strOrderDetailID = 3067.39
可以接受的误差 strUserID = 43288 ReturnALlMoneyUnit = 6190.77 strOrderDetailID = 6190.77
可以接受的误差 strUserID = 43422 ReturnALlMoneyUnit = 14367.98 strOrderDetailID = 14367.85
可以接受的误差 strUserID = 43410 ReturnALlMoneyUnit = 3095.36 strOrderDetailID = 3095.36
可以接受的误差 strUserID = 42767 ReturnALlMoneyUnit = 3122.86 strOrderDetailID = 3122.86
可以接受的误差 strUserID = 43432 ReturnALlMoneyUnit = 6245.77 strOrderDetailID = 6245.77
可以接受的误差 strUserID = 43612 ReturnALlMoneyUnit = 87840.17 strOrderDetailID = 87839.75
可以接受的误差 strUserID = 43650 ReturnALlMoneyUnit = 11551.32 strOrderDetailID = 11551.10
已经返还的 大于现在订单统计的 strUserID = 43217 ReturnALlMoneyUnit = 6881.26 orderDescimal = 1273.99
可以接受的误差 strUserID = 42957 ReturnALlMoneyUnit = 3162.80 strOrderDetailID = 3162.80
可以接受的误差 strUserID = 43152 ReturnALlMoneyUnit = 336352.45 strOrderDetailID = 336352.28
可以接受的误差 strUserID = 43301 ReturnALlMoneyUnit = 16100.13 strOrderDetailID = 16099.92
可以接受的误差 strUserID = 43441 ReturnALlMoneyUnit = 18976.96 strOrderDetailID = 18976.96
可以接受的误差 strUserID = 43464 ReturnALlMoneyUnit = 31628.30 strOrderDetailID = 31628.30
可以接受的误差 strUserID = 43444 ReturnALlMoneyUnit = 15814.12 strOrderDetailID = 15814.12
可以接受的误差 strUserID = 41526 ReturnALlMoneyUnit = 3162.80 strOrderDetailID = 3162.80
可以接受的误差 strUserID = 43618 ReturnALlMoneyUnit = 25164.38 strOrderDetailID = 25164.38
可以接受的误差 strUserID = 43614 ReturnALlMoneyUnit = 57279.47 strOrderDetailID = 57279.42
可以接受的误差 strUserID = 43949 ReturnALlMoneyUnit = 15999.96 strOrderDetailID = 15999.96
可以接受的误差 strUserID = 43558 ReturnALlMoneyUnit = 3199.97 strOrderDetailID = 3199.97
可以接受的误差 strUserID = 43570 ReturnALlMoneyUnit = 3199.97 strOrderDetailID = 3199.97
可以接受的误差 strUserID = 43540 ReturnALlMoneyUnit = 3199.97 strOrderDetailID = 3199.97
可以接受的误差 strUserID = 43561 ReturnALlMoneyUnit = 3199.97 strOrderDetailID = 3199.97
可以接受的误差 strUserID = 43562 ReturnALlMoneyUnit = 3199.97 strOrderDetailID = 3199.97
可以接受的误差 strUserID = 43592 ReturnALlMoneyUnit = 15999.96 strOrderDetailID = 15999.96
可以接受的误差 strUserID = 43599 ReturnALlMoneyUnit = 70768.04 strOrderDetailID = 70767.94
可以接受的误差 strUserID = 43596 ReturnALlMoneyUnit = 41713.48 strOrderDetailID = 41713.33
可以接受的误差 strUserID = 43621 ReturnALlMoneyUnit = 3199.97 strOrderDetailID = 3199.97
可以接受的误差 strUserID = 43619 ReturnALlMoneyUnit = 358863.28 strOrderDetailID = 358863.18
可以接受的误差 strUserID = 43607 ReturnALlMoneyUnit = 65944.21 strOrderDetailID = 65943.68
可以接受的误差 strUserID = 43908 ReturnALlMoneyUnit = 3249.41 strOrderDetailID = 3249.41
可以接受的误差 strUserID = 43907 ReturnALlMoneyUnit = 361483.13 strOrderDetailID = 361482.63
可以接受的误差 strUserID = 43733 ReturnALlMoneyUnit = 3249.41 strOrderDetailID = 3249.41
可以接受的误差 strUserID = 43668 ReturnALlMoneyUnit = 8071.02 strOrderDetailID = 8070.83
可以接受的误差 strUserID = 43661 ReturnALlMoneyUnit = 46369.83 strOrderDetailID = 46369.80
可以接受的误差 strUserID = 43584 ReturnALlMoneyUnit = 6498.88 strOrderDetailID = 6498.88
可以接受的误差 strUserID = 43664 ReturnALlMoneyUnit = 10491.79 strOrderDetailID = 10491.79
可以接受的误差 strUserID = 43660 ReturnALlMoneyUnit = 32878.41 strOrderDetailID = 32878.41
可以接受的误差 strUserID = 44065 ReturnALlMoneyUnit = 29966.35 strOrderDetailID = 29966.19
可以接受的误差 strUserID = 43972 ReturnALlMoneyUnit = 8727.55 strOrderDetailID = 8727.43
可以接受的误差 strUserID = 43380 ReturnALlMoneyUnit = 3275.97 strOrderDetailID = 3275.97
已经返还的 大于现在订单统计的 strUserID = 43594 ReturnALlMoneyUnit = 61773.66 orderDescimal = 61131.26
可以接受的误差 strUserID = 43704 ReturnALlMoneyUnit = 132002.83 strOrderDetailID = 132002.71
可以接受的误差 strUserID = 43710 ReturnALlMoneyUnit = 99778.25 strOrderDetailID = 99778.18
可以接受的误差 strUserID = 43632 ReturnALlMoneyUnit = 12734.83 strOrderDetailID = 12734.79
可以接受的误差 strUserID = 43581 ReturnALlMoneyUnit = 16379.97 strOrderDetailID = 16379.97
可以接受的误差 strUserID = 43994 ReturnALlMoneyUnit = 6606.49 strOrderDetailID = 6606.49
可以接受的误差 strUserID = 43828 ReturnALlMoneyUnit = 3303.22 strOrderDetailID = 3303.22
可以接受的误差 strUserID = 43735 ReturnALlMoneyUnit = 9909.72 strOrderDetailID = 9909.72
可以接受的误差 strUserID = 43817 ReturnALlMoneyUnit = 68193.80 strOrderDetailID = 68193.81
可以接受的误差 strUserID = 43766 ReturnALlMoneyUnit = 11676.82 strOrderDetailID = 11676.82
可以接受的误差 strUserID = 43829 ReturnALlMoneyUnit = 16516.20 strOrderDetailID = 16516.20
已经返还的 小于现在订单统计的 strUserID = 43787 ReturnALlMoneyUnit = 7872.21 orderDescimal = 7883.03
可以接受的误差 strUserID = 43471 ReturnALlMoneyUnit = 18208.84 strOrderDetailID = 18208.82
可以接受的误差 strUserID = 43649 ReturnALlMoneyUnit = 3338.74 strOrderDetailID = 3338.74
可以接受的误差 strUserID = 43984 ReturnALlMoneyUnit = 49908.84 strOrderDetailID = 49908.78
可以接受的误差 strUserID = 43950 ReturnALlMoneyUnit = 16693.80 strOrderDetailID = 16693.80
可以接受的误差 strUserID = 43915 ReturnALlMoneyUnit = 32278.00 strOrderDetailID = 32277.73
可以接受的误差 strUserID = 43826 ReturnALlMoneyUnit = 12131.57 strOrderDetailID = 12131.57
可以接受的误差 strUserID = 43904 ReturnALlMoneyUnit = 39313.66 strOrderDetailID = 39313.64
可以接受的误差 strUserID = 43290 ReturnALlMoneyUnit = 6677.53 strOrderDetailID = 6677.53
可以接受的误差 strUserID = 43869 ReturnALlMoneyUnit = 3338.74 strOrderDetailID = 3338.74
可以接受的误差 strUserID = 43899 ReturnALlMoneyUnit = 70651.87 strOrderDetailID = 70651.42
可以接受的误差 strUserID = 43895 ReturnALlMoneyUnit = 16983.01 strOrderDetailID = 16982.92
可以接受的误差 strUserID = 43385 ReturnALlMoneyUnit = 3370.34 strOrderDetailID = 3370.34
可以接受的误差 strUserID = 44004 ReturnALlMoneyUnit = 120893.46 strOrderDetailID = 120893.02
可以接受的误差 strUserID = 43978 ReturnALlMoneyUnit = 3370.34 strOrderDetailID = 3370.34
可以接受的误差 strUserID = 43640 ReturnALlMoneyUnit = 234827.84 strOrderDetailID = 234827.72
可以接受的误差 strUserID = 43421 ReturnALlMoneyUnit = 14741.31 strOrderDetailID = 14740.90
可以接受的误差 strUserID = 44084 ReturnALlMoneyUnit = 186164.40 strOrderDetailID = 186164.38
可以接受的误差 strUserID = 43863 ReturnALlMoneyUnit = 3391.14 strOrderDetailID = 3391.14
可以接受的误差 strUserID = 43325 ReturnALlMoneyUnit = 210121.89 strOrderDetailID = 210121.71
可以接受的误差 strUserID = 43447 ReturnALlMoneyUnit = 20346.95 strOrderDetailID = 20346.95
可以接受的误差 strUserID = 44002 ReturnALlMoneyUnit = 17214.36 strOrderDetailID = 17214.03
可以接受的误差 strUserID = 44087 ReturnALlMoneyUnit = 68558.10 strOrderDetailID = 68558.10
可以接受的误差 strUserID = 44082 ReturnALlMoneyUnit = 3427.88 strOrderDetailID = 3427.88
可以接受的误差 strUserID = 44058 ReturnALlMoneyUnit = 70730.23 strOrderDetailID = 70729.75
可以接受的误差 strUserID = 44075 ReturnALlMoneyUnit = 3427.88 strOrderDetailID = 3427.88
可以接受的误差 strUserID = 44079 ReturnALlMoneyUnit = 172059.70 strOrderDetailID = 172059.41
可以接受的误差 strUserID = 43988 ReturnALlMoneyUnit = 7389.51 strOrderDetailID = 7389.17
可以接受的误差 strUserID = 44100 ReturnALlMoneyUnit = 105714.68 strOrderDetailID = 105714.57
可以接受的误差 strUserID = 43331 ReturnALlMoneyUnit = 172984.51 strOrderDetailID = 172984.26
可以接受的误差 strUserID = 44110 ReturnALlMoneyUnit = 14718.22 strOrderDetailID = 14717.99
可以接受的误差 strUserID = 43330 ReturnALlMoneyUnit = 3454.82 strOrderDetailID = 3454.82
可以接受的误差 strUserID = 44121 ReturnALlMoneyUnit = 63328.61 strOrderDetailID = 63328.51
可以接受的误差 strUserID = 43665 ReturnALlMoneyUnit = 3487.45 strOrderDetailID = 3487.45
可以接受的误差 strUserID = 44155 ReturnALlMoneyUnit = 3487.45 strOrderDetailID = 3487.45
可以接受的误差 strUserID = 44191 ReturnALlMoneyUnit = 127279.10 strOrderDetailID = 127279.10
可以接受的误差 strUserID = 44133 ReturnALlMoneyUnit = 35191.98 strOrderDetailID = 35191.98
可以接受的误差 strUserID = 44183 ReturnALlMoneyUnit = 3519.17 strOrderDetailID = 3519.17
可以接受的误差 strUserID = 44646 ReturnALlMoneyUnit = 171887.68 strOrderDetailID = 171887.64
可以接受的误差 strUserID = 44596 ReturnALlMoneyUnit = 222837.19 strOrderDetailID = 222837.01
可以接受的误差 strUserID = 44253 ReturnALlMoneyUnit = 118836.71 strOrderDetailID = 118836.68
可以接受的误差 strUserID = 44567 ReturnALlMoneyUnit = 3542.95 strOrderDetailID = 3542.95
可以接受的误差 strUserID = 44292 ReturnALlMoneyUnit = 98957.29 strOrderDetailID = 98957.24
可以接受的误差 strUserID = 44240 ReturnALlMoneyUnit = 3542.95 strOrderDetailID = 3542.95
可以接受的误差 strUserID = 44283 ReturnALlMoneyUnit = 35429.76 strOrderDetailID = 35429.76
可以接受的误差 strUserID = 44219 ReturnALlMoneyUnit = 158136.41 strOrderDetailID = 158136.04
可以接受的误差 strUserID = 44051 ReturnALlMoneyUnit = 7632.26 strOrderDetailID = 7631.96
可以接受的误差 strUserID = 44346 ReturnALlMoneyUnit = 7170.37 strOrderDetailID = 7170.37
可以接受的误差 strUserID = 44316 ReturnALlMoneyUnit = 14340.75 strOrderDetailID = 14340.75
可以接受的误差 strUserID = 44474 ReturnALlMoneyUnit = 10755.52 strOrderDetailID = 10755.52
可以接受的误差 strUserID = 43579 ReturnALlMoneyUnit = 14784.65 strOrderDetailID = 14784.25
可以接受的误差 strUserID = 43802 ReturnALlMoneyUnit = 7370.15 strOrderDetailID = 7369.77
可以接受的误差 strUserID = 44332 ReturnALlMoneyUnit = 29687.19 strOrderDetailID = 29687.14
可以接受的误差 strUserID = 44334 ReturnALlMoneyUnit = 41912.88 strOrderDetailID = 41912.82
可以接受的误差 strUserID = 44308 ReturnALlMoneyUnit = 3585.16 strOrderDetailID = 3585.16
可以接受的误差 strUserID = 44105 ReturnALlMoneyUnit = 3585.16 strOrderDetailID = 3585.16
可以接受的误差 strUserID = 44290 ReturnALlMoneyUnit = 3585.16 strOrderDetailID = 3585.16
可以接受的误差 strUserID = 44171 ReturnALlMoneyUnit = 19355.98 strOrderDetailID = 19355.98
可以接受的误差 strUserID = 44623 ReturnALlMoneyUnit = 3613.57 strOrderDetailID = 3613.57
可以接受的误差 strUserID = 44405 ReturnALlMoneyUnit = 25401.89 strOrderDetailID = 25401.89
可以接受的误差 strUserID = 44431 ReturnALlMoneyUnit = 3613.57 strOrderDetailID = 3613.57
可以接受的误差 strUserID = 44170 ReturnALlMoneyUnit = 507153.60 strOrderDetailID = 507153.21
可以接受的误差 strUserID = 44433 ReturnALlMoneyUnit = 7227.18 strOrderDetailID = 7227.18
可以接受的误差 strUserID = 43347 ReturnALlMoneyUnit = 3613.57 strOrderDetailID = 3613.57
可以接受的误差 strUserID = 44248 ReturnALlMoneyUnit = 3613.57 strOrderDetailID = 3613.57
可以接受的误差 strUserID = 44403 ReturnALlMoneyUnit = 18727.67 strOrderDetailID = 18727.67
可以接受的误差 strUserID = 44266 ReturnALlMoneyUnit = 3585.16 strOrderDetailID = 3585.16
可以接受的误差 strUserID = 44647 ReturnALlMoneyUnit = 7399.55 strOrderDetailID = 7399.16
已经返还的 小于现在订单统计的 strUserID = 44499 ReturnALlMoneyUnit = 7290.22 orderDescimal = 7508.89
可以接受的误差 strUserID = 44481 ReturnALlMoneyUnit = 7290.22 strOrderDetailID = 7290.22
可以接受的误差 strUserID = 43837 ReturnALlMoneyUnit = 8343.29 strOrderDetailID = 8343.08
可以接受的误差 strUserID = 44494 ReturnALlMoneyUnit = 33459.99 strOrderDetailID = 33459.77
可以接受的误差 strUserID = 44496 ReturnALlMoneyUnit = 36451.15 strOrderDetailID = 36451.15
可以接受的误差 strUserID = 41774 ReturnALlMoneyUnit = 3645.09 strOrderDetailID = 3645.09
可以接受的误差 strUserID = 44455 ReturnALlMoneyUnit = 8512.05 strOrderDetailID = 8511.87
可以接受的误差 strUserID = 44103 ReturnALlMoneyUnit = 64966.61 strOrderDetailID = 64966.42
可以接受的误差 strUserID = 41310 ReturnALlMoneyUnit = 3645.09 strOrderDetailID = 3645.09
可以接受的误差 strUserID = 44475 ReturnALlMoneyUnit = 3645.09 strOrderDetailID = 3645.09
可以接受的误差 strUserID = 44448 ReturnALlMoneyUnit = 3645.09 strOrderDetailID = 3645.09
可以接受的误差 strUserID = 42906 ReturnALlMoneyUnit = 14580.47 strOrderDetailID = 14580.47
可以接受的误差 strUserID = 44541 ReturnALlMoneyUnit = 3665.79 strOrderDetailID = 3665.79
可以接受的误差 strUserID = 44601 ReturnALlMoneyUnit = 3665.79 strOrderDetailID = 3665.79
可以接受的误差 strUserID = 43400 ReturnALlMoneyUnit = 14723.89 strOrderDetailID = 14723.66
可以接受的误差 strUserID = 43792 ReturnALlMoneyUnit = 3665.79 strOrderDetailID = 3665.79
可以接受的误差 strUserID = 44525 ReturnALlMoneyUnit = 3665.79 strOrderDetailID = 3665.79
可以接受的误差 strUserID = 43686 ReturnALlMoneyUnit = 14106.60 strOrderDetailID = 14106.60
可以接受的误差 strUserID = 43971 ReturnALlMoneyUnit = 6974.94 strOrderDetailID = 6974.94
可以接受的误差 strUserID = 44605 ReturnALlMoneyUnit = 7551.63 strOrderDetailID = 7551.27
可以接受的误差 strUserID = 44554 ReturnALlMoneyUnit = 3697.59 strOrderDetailID = 3697.59
可以接受的误差 strUserID = 44559 ReturnALlMoneyUnit = 7395.22 strOrderDetailID = 7395.22
可以接受的误差 strUserID = 44564 ReturnALlMoneyUnit = 7395.22 strOrderDetailID = 7395.22
可以接受的误差 strUserID = 44563 ReturnALlMoneyUnit = 85857.77 strOrderDetailID = 85857.69
可以接受的误差 strUserID = 44556 ReturnALlMoneyUnit = 3697.59 strOrderDetailID = 3697.59
可以接受的误差 strUserID = 44592 ReturnALlMoneyUnit = 36976.15 strOrderDetailID = 36976.15
可以接受的误差 strUserID = 44520 ReturnALlMoneyUnit = 3697.59 strOrderDetailID = 3697.59
可以接受的误差 strUserID = 43413 ReturnALlMoneyUnit = 3697.59 strOrderDetailID = 3697.59
可以接受的误差 strUserID = 44688 ReturnALlMoneyUnit = 3726.39 strOrderDetailID = 3726.39
可以接受的误差 strUserID = 44658 ReturnALlMoneyUnit = 3726.39 strOrderDetailID = 3726.39
可以接受的误差 strUserID = 41905 ReturnALlMoneyUnit = 3726.39 strOrderDetailID = 3726.39
可以接受的误差 strUserID = 44642 ReturnALlMoneyUnit = 32966.38 strOrderDetailID = 32966.30
可以接受的误差 strUserID = 43469 ReturnALlMoneyUnit = 3726.39 strOrderDetailID = 3726.39
可以接受的误差 strUserID = 44662 ReturnALlMoneyUnit = 3726.39 strOrderDetailID = 3726.39
可以接受的误差 strUserID = 44660 ReturnALlMoneyUnit = 3726.39 strOrderDetailID = 3726.39
可以接受的误差 strUserID = 43539 ReturnALlMoneyUnit = 3726.39 strOrderDetailID = 3726.39
可以接受的误差 strUserID = 44631 ReturnALlMoneyUnit = 7452.81 strOrderDetailID = 7452.81
可以接受的误差 strUserID = 44911 ReturnALlMoneyUnit = 282561.73 strOrderDetailID = 282561.35
可以接受的误差 strUserID = 44620 ReturnALlMoneyUnit = 12492.44 strOrderDetailID = 12492.44
可以接受的误差 strUserID = 44717 ReturnALlMoneyUnit = 3754.43 strOrderDetailID = 3754.43
可以接受的误差 strUserID = 44418 ReturnALlMoneyUnit = 3754.43 strOrderDetailID = 3754.43
可以接受的误差 strUserID = 44665 ReturnALlMoneyUnit = 18772.21 strOrderDetailID = 18772.21
可以接受的误差 strUserID = 44264 ReturnALlMoneyUnit = 3754.43 strOrderDetailID = 3754.43
可以接受的误差 strUserID = 44709 ReturnALlMoneyUnit = 7508.89 strOrderDetailID = 7508.50
可以接受的误差 strUserID = 43697 ReturnALlMoneyUnit = 3754.43 strOrderDetailID = 3754.43
可以接受的误差 strUserID = 44715 ReturnALlMoneyUnit = 216424.57 strOrderDetailID = 216424.58
可以接受的误差 strUserID = 44691 ReturnALlMoneyUnit = 122486.83 strOrderDetailID = 122486.47
可以接受的误差 strUserID = 44714 ReturnALlMoneyUnit = 93265.47 strOrderDetailID = 93265.41
可以接受的误差 strUserID = 44711 ReturnALlMoneyUnit = 56290.40 strOrderDetailID = 56290.40
可以接受的误差 strUserID = 44698 ReturnALlMoneyUnit = 3754.43 strOrderDetailID = 3754.43
可以接受的误差 strUserID = 44690 ReturnALlMoneyUnit = 37890.87 strOrderDetailID = 37890.77
可以接受的误差 strUserID = 44549 ReturnALlMoneyUnit = 93632.60 strOrderDetailID = 93632.65
可以接受的误差 strUserID = 44689 ReturnALlMoneyUnit = 11263.32 strOrderDetailID = 11263.32
可以接受的误差 strUserID = 44692 ReturnALlMoneyUnit = 3754.43 strOrderDetailID = 3754.43
可以接受的误差 strUserID = 41398 ReturnALlMoneyUnit = 3784.97 strOrderDetailID = 3784.97
已经返还的 大于现在订单统计的 strUserID = 44539 ReturnALlMoneyUnit = 3904.14 orderDescimal = 3784.97
可以接受的误差 strUserID = 44261 ReturnALlMoneyUnit = 23230.14 strOrderDetailID = 23230.03
可以接受的误差 strUserID = 43653 ReturnALlMoneyUnit = 3784.97 strOrderDetailID = 3784.97
可以接受的误差 strUserID = 44676 ReturnALlMoneyUnit = 3784.97 strOrderDetailID = 3784.97
可以接受的误差 strUserID = 44773 ReturnALlMoneyUnit = 18924.89 strOrderDetailID = 18924.89
可以接受的误差 strUserID = 43456 ReturnALlMoneyUnit = 7687.82 strOrderDetailID = 7687.46
可以接受的误差 strUserID = 44786 ReturnALlMoneyUnit = 28518.55 strOrderDetailID = 28518.49
可以接受的误差 strUserID = 44756 ReturnALlMoneyUnit = 23848.78 strOrderDetailID = 23848.74
可以接受的误差 strUserID = 44739 ReturnALlMoneyUnit = 7569.96 strOrderDetailID = 7569.96
可以接受的误差 strUserID = 44738 ReturnALlMoneyUnit = 3784.97 strOrderDetailID = 3784.97
可以接受的误差 strUserID = 44341 ReturnALlMoneyUnit = 3784.97 strOrderDetailID = 3784.97
可以接受的误差 strUserID = 44736 ReturnALlMoneyUnit = 7569.96 strOrderDetailID = 7569.96
可以接受的误差 strUserID = 44728 ReturnALlMoneyUnit = 86493.69 strOrderDetailID = 86493.56
可以接受的误差 strUserID = 44723 ReturnALlMoneyUnit = 3784.97 strOrderDetailID = 3784.97
可以接受的误差 strUserID = 44735 ReturnALlMoneyUnit = 7569.96 strOrderDetailID = 7569.96
可以接受的误差 strUserID = 44733 ReturnALlMoneyUnit = 3784.97 strOrderDetailID = 3784.97
可以接受的误差 strUserID = 44635 ReturnALlMoneyUnit = 3784.97 strOrderDetailID = 3784.97
可以接受的误差 strUserID = 44874 ReturnALlMoneyUnit = 7640.63 strOrderDetailID = 7640.26
可以接受的误差 strUserID = 44720 ReturnALlMoneyUnit = 22842.73 strOrderDetailID = 22842.73
可以接受的误差 strUserID = 44866 ReturnALlMoneyUnit = 19035.58 strOrderDetailID = 19035.58
可以接受的误差 strUserID = 44864 ReturnALlMoneyUnit = 19035.58 strOrderDetailID = 19035.58
可以接受的误差 strUserID = 44095 ReturnALlMoneyUnit = 19035.58 strOrderDetailID = 19035.58
可以接受的误差 strUserID = 44825 ReturnALlMoneyUnit = 3807.11 strOrderDetailID = 3807.11
可以接受的误差 strUserID = 44828 ReturnALlMoneyUnit = 226915.64 strOrderDetailID = 226915.29
可以接受的误差 strUserID = 44950 ReturnALlMoneyUnit = 62957.66 strOrderDetailID = 62957.66
可以接受的误差 strUserID = 44939 ReturnALlMoneyUnit = 13567.38 strOrderDetailID = 13567.38
可以接受的误差 strUserID = 44886 ReturnALlMoneyUnit = 3833.49 strOrderDetailID = 3833.49
可以接受的误差 strUserID = 44849 ReturnALlMoneyUnit = 3833.49 strOrderDetailID = 3833.49
可以接受的误差 strUserID = 44847 ReturnALlMoneyUnit = 3833.49 strOrderDetailID = 3833.49
可以接受的误差 strUserID = 45026 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 45027 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 45044 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 45075 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 44990 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 44971 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 45140 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 44973 ReturnALlMoneyUnit = 8675.61 strOrderDetailID = 8675.42
可以接受的误差 strUserID = 44930 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 41506 ReturnALlMoneyUnit = 3854.00 strOrderDetailID = 3854.00
可以接受的误差 strUserID = 43591 ReturnALlMoneyUnit = 77436.87 strOrderDetailID = 77436.87
可以接受的误差 strUserID = 44965 ReturnALlMoneyUnit = 19926.45 strOrderDetailID = 19926.45
可以接受的误差 strUserID = 44970 ReturnALlMoneyUnit = 13707.57 strOrderDetailID = 13707.57
可以接受的误差 strUserID = 43683 ReturnALlMoneyUnit = 14557.92 strOrderDetailID = 14557.92
可以接受的误差 strUserID = 44447 ReturnALlMoneyUnit = 35561.37 strOrderDetailID = 35561.25
可以接受的误差 strUserID = 45072 ReturnALlMoneyUnit = 7748.81 strOrderDetailID = 7748.81
可以接受的误差 strUserID = 45018 ReturnALlMoneyUnit = 119604.55 strOrderDetailID = 119604.52
可以接受的误差 strUserID = 45054 ReturnALlMoneyUnit = 3874.38 strOrderDetailID = 3874.38
可以接受的误差 strUserID = 45028 ReturnALlMoneyUnit = 198807.57 strOrderDetailID = 198807.41
可以接受的误差 strUserID = 45095 ReturnALlMoneyUnit = 38744.06 strOrderDetailID = 38744.06
可以接受的误差 strUserID = 45066 ReturnALlMoneyUnit = 3874.38 strOrderDetailID = 3874.38
已经返还的 大于现在订单统计的 strUserID = 44288 ReturnALlMoneyUnit = 102358.78 orderDescimal = 102282.02
可以接受的误差 strUserID = 45092 ReturnALlMoneyUnit = 184679.68 strOrderDetailID = 184679.33
可以接受的误差 strUserID = 45084 ReturnALlMoneyUnit = 7805.64 strOrderDetailID = 7805.64
可以接受的误差 strUserID = 44944 ReturnALlMoneyUnit = 17946.33 strOrderDetailID = 17946.16
可以接受的误差 strUserID = 45046 ReturnALlMoneyUnit = 3902.79 strOrderDetailID = 3902.79
可以接受的误差 strUserID = 43107 ReturnALlMoneyUnit = 3902.79 strOrderDetailID = 3902.79
可以接受的误差 strUserID = 45138 ReturnALlMoneyUnit = 3934.63 strOrderDetailID = 3934.63
可以接受的误差 strUserID = 41411 ReturnALlMoneyUnit = 7869.32 strOrderDetailID = 7869.32
可以接受的误差 strUserID = 45156 ReturnALlMoneyUnit = 7869.32 strOrderDetailID = 7869.32
可以接受的误差 strUserID = 45106 ReturnALlMoneyUnit = 20461.81 strOrderDetailID = 20461.81
可以接受的误差 strUserID = 45136 ReturnALlMoneyUnit = 3934.63 strOrderDetailID = 3934.63
可以接受的误差 strUserID = 45129 ReturnALlMoneyUnit = 32230.95 strOrderDetailID = 32230.84
可以接受的误差 strUserID = 45117 ReturnALlMoneyUnit = 3934.63 strOrderDetailID = 3934.63
可以接受的误差 strUserID = 45413 ReturnALlMoneyUnit = 7934.87 strOrderDetailID = 7934.87
可以接受的误差 strUserID = 45253 ReturnALlMoneyUnit = 3914.00 strOrderDetailID = 3914.00
可以接受的误差 strUserID = 45199 ReturnALlMoneyUnit = 3967.40 strOrderDetailID = 3967.40
可以接受的误差 strUserID = 45190 ReturnALlMoneyUnit = 8247.41 strOrderDetailID = 8247.12
可以接受的误差 strUserID = 45192 ReturnALlMoneyUnit = 19837.12 strOrderDetailID = 19837.12
可以接受的误差 strUserID = 45111 ReturnALlMoneyUnit = 3967.40 strOrderDetailID = 3967.40
可以接受的误差 strUserID = 45174 ReturnALlMoneyUnit = 3967.40 strOrderDetailID = 3967.40
可以接受的误差 strUserID = 45053 ReturnALlMoneyUnit = 51609.57 strOrderDetailID = 51609.57
可以接受的误差 strUserID = 45209 ReturnALlMoneyUnit = 3992.91 strOrderDetailID = 3992.91
可以接受的误差 strUserID = 43737 ReturnALlMoneyUnit = 11978.80 strOrderDetailID = 11978.80
可以接受的误差 strUserID = 45226 ReturnALlMoneyUnit = 7985.89 strOrderDetailID = 7985.89
可以接受的误差 strUserID = 43833 ReturnALlMoneyUnit = 3992.91 strOrderDetailID = 3992.91
可以接受的误差 strUserID = 45208 ReturnALlMoneyUnit = 156433.25 strOrderDetailID = 156433.09
可以接受的误差 strUserID = 43280 ReturnALlMoneyUnit = 3992.91 strOrderDetailID = 3992.91
可以接受的误差 strUserID = 45222 ReturnALlMoneyUnit = 3992.91 strOrderDetailID = 3992.91
可以接受的误差 strUserID = 45130 ReturnALlMoneyUnit = 7985.89 strOrderDetailID = 7985.89
可以接受的误差 strUserID = 45170 ReturnALlMoneyUnit = 9294.17 strOrderDetailID = 9294.03
已经返还的 小于现在订单统计的 strUserID = 45265 ReturnALlMoneyUnit = 8490.77 orderDescimal = 8513.07
可以接受的误差 strUserID = 44838 ReturnALlMoneyUnit = 94387.21 strOrderDetailID = 94387.15
可以接受的误差 strUserID = 45244 ReturnALlMoneyUnit = 4019.24 strOrderDetailID = 4019.24
可以接受的误差 strUserID = 45252 ReturnALlMoneyUnit = 99363.28 strOrderDetailID = 99363.28
可以接受的误差 strUserID = 43412 ReturnALlMoneyUnit = 20096.33 strOrderDetailID = 20096.33
可以接受的误差 strUserID = 44027 ReturnALlMoneyUnit = 4041.80 strOrderDetailID = 4041.80
可以接受的误差 strUserID = 45272 ReturnALlMoneyUnit = 102727.13 strOrderDetailID = 102726.98
可以接受的误差 strUserID = 43608 ReturnALlMoneyUnit = 53536.46 strOrderDetailID = 53536.46
可以接受的误差 strUserID = 45341 ReturnALlMoneyUnit = 19126.39 strOrderDetailID = 19126.14
可以接受的误差 strUserID = 45307 ReturnALlMoneyUnit = 4072.79 strOrderDetailID = 4072.79
可以接受的误差 strUserID = 45296 ReturnALlMoneyUnit = 4072.79 strOrderDetailID = 4072.79
可以接受的误差 strUserID = 43790 ReturnALlMoneyUnit = 20529.36 strOrderDetailID = 20529.36
可以接受的误差 strUserID = 45096 ReturnALlMoneyUnit = 9579.66 strOrderDetailID = 9579.55
可以接受的误差 strUserID = 45364 ReturnALlMoneyUnit = 4105.85 strOrderDetailID = 4105.85
可以接受的误差 strUserID = 44161 ReturnALlMoneyUnit = 8906.44 strOrderDetailID = 8906.25
可以接受的误差 strUserID = 44801 ReturnALlMoneyUnit = 14833.20 strOrderDetailID = 14833.11
可以接受的误差 strUserID = 45001 ReturnALlMoneyUnit = 4105.85 strOrderDetailID = 4105.85
可以接受的误差 strUserID = 45401 ReturnALlMoneyUnit = 8263.60 strOrderDetailID = 8263.60
可以接受的误差 strUserID = 45440 ReturnALlMoneyUnit = 8263.60 strOrderDetailID = 8263.60
可以接受的误差 strUserID = 45335 ReturnALlMoneyUnit = 4131.77 strOrderDetailID = 4131.77
可以接受的误差 strUserID = 45375 ReturnALlMoneyUnit = 4131.77 strOrderDetailID = 4131.77
可以接受的误差 strUserID = 45417 ReturnALlMoneyUnit = 4131.77 strOrderDetailID = 4131.77
可以接受的误差 strUserID = 45201 ReturnALlMoneyUnit = 123815.59 strOrderDetailID = 123815.41
可以接受的误差 strUserID = 44681 ReturnALlMoneyUnit = 4131.77 strOrderDetailID = 4131.77
可以接受的误差 strUserID = 45384 ReturnALlMoneyUnit = 4105.85 strOrderDetailID = 4105.85
可以接受的误差 strUserID = 41524 ReturnALlMoneyUnit = 4154.10 strOrderDetailID = 4154.10
可以接受的误差 strUserID = 43452 ReturnALlMoneyUnit = 8308.26 strOrderDetailID = 8308.26
可以接受的误差 strUserID = 45507 ReturnALlMoneyUnit = 8522.73 strOrderDetailID = 8522.46
可以接受的误差 strUserID = 42166 ReturnALlMoneyUnit = 20770.59 strOrderDetailID = 20770.59
可以接受的误差 strUserID = 43565 ReturnALlMoneyUnit = 4154.10 strOrderDetailID = 4154.10
可以接受的误差 strUserID = 45488 ReturnALlMoneyUnit = 4154.10 strOrderDetailID = 4154.10
可以接受的误差 strUserID = 42211 ReturnALlMoneyUnit = 41771.34 strOrderDetailID = 41771.34
可以接受的误差 strUserID = 45551 ReturnALlMoneyUnit = 4177.11 strOrderDetailID = 4177.11
可以接受的误差 strUserID = 45554 ReturnALlMoneyUnit = 16011.22 strOrderDetailID = 16011.22
可以接受的误差 strUserID = 45543 ReturnALlMoneyUnit = 4177.11 strOrderDetailID = 4177.11
可以接受的误差 strUserID = 45585 ReturnALlMoneyUnit = 4177.11 strOrderDetailID = 4177.11
可以接受的误差 strUserID = 45573 ReturnALlMoneyUnit = 4177.11 strOrderDetailID = 4177.11
可以接受的误差 strUserID = 45407 ReturnALlMoneyUnit = 8354.28 strOrderDetailID = 8354.28
可以接受的误差 strUserID = 45108 ReturnALlMoneyUnit = 4177.11 strOrderDetailID = 4177.11
可以接受的误差 strUserID = 45807 ReturnALlMoneyUnit = 4204.32 strOrderDetailID = 4204.32
可以接受的误差 strUserID = 45827 ReturnALlMoneyUnit = 4204.32 strOrderDetailID = 4204.32
可以接受的误差 strUserID = 45608 ReturnALlMoneyUnit = 4204.32 strOrderDetailID = 4204.32
可以接受的误差 strUserID = 45544 ReturnALlMoneyUnit = 84388.04 strOrderDetailID = 84387.68
可以接受的误差 strUserID = 45114 ReturnALlMoneyUnit = 4204.32 strOrderDetailID = 4204.32
可以接受的误差 strUserID = 45560 ReturnALlMoneyUnit = 4204.32 strOrderDetailID = 4204.32
可以接受的误差 strUserID = 45720 ReturnALlMoneyUnit = 20303.02 strOrderDetailID = 20303.02
可以接受的误差 strUserID = 45658 ReturnALlMoneyUnit = 4234.43 strOrderDetailID = 4234.43
可以接受的误差 strUserID = 45406 ReturnALlMoneyUnit = 25388.69 strOrderDetailID = 25388.56
可以接受的误差 strUserID = 45846 ReturnALlMoneyUnit = 4234.43 strOrderDetailID = 4234.43
可以接受的误差 strUserID = 45652 ReturnALlMoneyUnit = 21172.22 strOrderDetailID = 21172.22
可以接受的误差 strUserID = 45741 ReturnALlMoneyUnit = 86651.63 strOrderDetailID = 86651.65
可以接受的误差 strUserID = 45721 ReturnALlMoneyUnit = 84689.10 strOrderDetailID = 84689.10
可以接受的误差 strUserID = 45576 ReturnALlMoneyUnit = 21540.60 strOrderDetailID = 21540.60
可以接受的误差 strUserID = 45697 ReturnALlMoneyUnit = 12703.34 strOrderDetailID = 12703.34
可以接受的误差 strUserID = 43563 ReturnALlMoneyUnit = 21293.12 strOrderDetailID = 21293.12
可以接受的误差 strUserID = 45733 ReturnALlMoneyUnit = 8517.27 strOrderDetailID = 8517.27
可以接受的误差 strUserID = 45626 ReturnALlMoneyUnit = 4258.61 strOrderDetailID = 4258.61
可以接受的误差 strUserID = 45729 ReturnALlMoneyUnit = 4258.61 strOrderDetailID = 4258.61
可以接受的误差 strUserID = 44762 ReturnALlMoneyUnit = 4279.96 strOrderDetailID = 4279.96
可以接受的误差 strUserID = 45801 ReturnALlMoneyUnit = 4279.96 strOrderDetailID = 4279.96
可以接受的误差 strUserID = 45863 ReturnALlMoneyUnit = 26752.13 strOrderDetailID = 26752.05
可以接受的误差 strUserID = 45649 ReturnALlMoneyUnit = 4279.96 strOrderDetailID = 4279.96
可以接受的误差 strUserID = 45803 ReturnALlMoneyUnit = 31433.33 strOrderDetailID = 31433.19
可以接受的误差 strUserID = 45783 ReturnALlMoneyUnit = 4279.96 strOrderDetailID = 4279.96
可以接受的误差 strUserID = 45784 ReturnALlMoneyUnit = 4279.96 strOrderDetailID = 4279.96
可以接受的误差 strUserID = 45439 ReturnALlMoneyUnit = 17119.94 strOrderDetailID = 17119.94
可以接受的误差 strUserID = 45385 ReturnALlMoneyUnit = 4279.96 strOrderDetailID = 4279.96
可以接受的误差 strUserID = 43135 ReturnALlMoneyUnit = 14011.47 strOrderDetailID = 14011.47
可以接受的误差 strUserID = 45770 ReturnALlMoneyUnit = 4279.96 strOrderDetailID = 4279.96
可以接受的误差 strUserID = 45765 ReturnALlMoneyUnit = 8559.97 strOrderDetailID = 8559.97
可以接受的误差 strUserID = 45823 ReturnALlMoneyUnit = 4309.85 strOrderDetailID = 4309.85
可以接受的误差 strUserID = 45826 ReturnALlMoneyUnit = 39377.34 strOrderDetailID = 39377.13
可以接受的误差 strUserID = 45752 ReturnALlMoneyUnit = 79088.65 strOrderDetailID = 79088.54
可以接受的误差 strUserID = 43085 ReturnALlMoneyUnit = 26767.89 strOrderDetailID = 26767.80
可以接受的误差 strUserID = 45472 ReturnALlMoneyUnit = 9293.46 strOrderDetailID = 9293.29
可以接受的误差 strUserID = 44607 ReturnALlMoneyUnit = 43098.70 strOrderDetailID = 43098.70
可以接受的误差 strUserID = 43768 ReturnALlMoneyUnit = 12929.59 strOrderDetailID = 12929.59
可以接受的误差 strUserID = 45822 ReturnALlMoneyUnit = 35761.44 strOrderDetailID = 35761.33
可以接受的误差 strUserID = 45821 ReturnALlMoneyUnit = 9293.46 strOrderDetailID = 9293.29
可以接受的误差 strUserID = 45816 ReturnALlMoneyUnit = 4309.85 strOrderDetailID = 4309.85
可以接受的误差 strUserID = 44622 ReturnALlMoneyUnit = 4309.85 strOrderDetailID = 4309.85
可以接受的误差 strUserID = 45850 ReturnALlMoneyUnit = 43371.84 strOrderDetailID = 43371.84
可以接受的误差 strUserID = 44845 ReturnALlMoneyUnit = 4337.16 strOrderDetailID = 4337.16
可以接受的误差 strUserID = 43624 ReturnALlMoneyUnit = 4337.16 strOrderDetailID = 4337.16
可以接受的误差 strUserID = 45910 ReturnALlMoneyUnit = 4368.58 strOrderDetailID = 4368.58
可以接受的误差 strUserID = 45902 ReturnALlMoneyUnit = 4368.58 strOrderDetailID = 4368.58
可以接受的误差 strUserID = 45899 ReturnALlMoneyUnit = 4368.58 strOrderDetailID = 4368.58
可以接受的误差 strUserID = 45842 ReturnALlMoneyUnit = 4368.58 strOrderDetailID = 4368.58
可以接受的误差 strUserID = 45884 ReturnALlMoneyUnit = 21842.96 strOrderDetailID = 21842.96
可以接受的误差 strUserID = 45788 ReturnALlMoneyUnit = 8737.21 strOrderDetailID = 8737.21
可以接受的误差 strUserID = 44920 ReturnALlMoneyUnit = 8737.21 strOrderDetailID = 8737.21
可以接受的误差 strUserID = 45880 ReturnALlMoneyUnit = 4368.58 strOrderDetailID = 4368.58
可以接受的误差 strUserID = 45675 ReturnALlMoneyUnit = 21982.02 strOrderDetailID = 21982.02
可以接受的误差 strUserID = 45937 ReturnALlMoneyUnit = 15052.01 strOrderDetailID = 15052.01
可以接受的误差 strUserID = 45928 ReturnALlMoneyUnit = 13189.22 strOrderDetailID = 13189.22
可以接受的误差 strUserID = 45885 ReturnALlMoneyUnit = 8792.83 strOrderDetailID = 8792.83
可以接受的误差 strUserID = 45944 ReturnALlMoneyUnit = 4396.39 strOrderDetailID = 4396.39
可以接受的误差 strUserID = 45929 ReturnALlMoneyUnit = 4396.39 strOrderDetailID = 4396.39
可以接受的误差 strUserID = 45983 ReturnALlMoneyUnit = 8837.29 strOrderDetailID = 8837.29
可以接受的误差 strUserID = 45860 ReturnALlMoneyUnit = 8837.29 strOrderDetailID = 8837.29
可以接受的误差 strUserID = 43923 ReturnALlMoneyUnit = 4418.62 strOrderDetailID = 4418.62
可以接受的误差 strUserID = 45972 ReturnALlMoneyUnit = 14433.63 strOrderDetailID = 14433.63
可以接受的误差 strUserID = 43761 ReturnALlMoneyUnit = 4418.62 strOrderDetailID = 4418.62
可以接受的误差 strUserID = 44011 ReturnALlMoneyUnit = 15366.15 strOrderDetailID = 15366.15
可以接受的误差 strUserID = 45855 ReturnALlMoneyUnit = 4418.62 strOrderDetailID = 4418.62
可以接受的误差 strUserID = 41372 ReturnALlMoneyUnit = 4418.62 strOrderDetailID = 4418.62
可以接受的误差 strUserID = 45948 ReturnALlMoneyUnit = 4418.62 strOrderDetailID = 4418.62
可以接受的误差 strUserID = 45940 ReturnALlMoneyUnit = 4418.62 strOrderDetailID = 4418.62
可以接受的误差 strUserID = 46014 ReturnALlMoneyUnit = 51011.61 strOrderDetailID = 51011.61
可以接受的误差 strUserID = 45997 ReturnALlMoneyUnit = 22207.63 strOrderDetailID = 22207.63
可以接受的误差 strUserID = 46032 ReturnALlMoneyUnit = 35532.29 strOrderDetailID = 35532.29
可以接受的误差 strUserID = 46010 ReturnALlMoneyUnit = 50780.11 strOrderDetailID = 50780.15
可以接受的误差 strUserID = 46017 ReturnALlMoneyUnit = 13324.59 strOrderDetailID = 13324.59
可以接受的误差 strUserID = 46029 ReturnALlMoneyUnit = 8883.08 strOrderDetailID = 8883.08
可以接受的误差 strUserID = 46003 ReturnALlMoneyUnit = 9286.89 strOrderDetailID = 9286.70
可以接受的误差 strUserID = 44309 ReturnALlMoneyUnit = 4441.51 strOrderDetailID = 4441.51
已经返还的 大于现在订单统计的 strUserID = 46365 ReturnALlMoneyUnit = 211307.63 orderDescimal = 158790.93
可以接受的误差 strUserID = 46012 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 46071 ReturnALlMoneyUnit = 24541.58 strOrderDetailID = 24541.56
可以接受的误差 strUserID = 46035 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 46131 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 46286 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 46049 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 46060 ReturnALlMoneyUnit = 380346.21 strOrderDetailID = 380346.10
可以接受的误差 strUserID = 46043 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 44207 ReturnALlMoneyUnit = 44714.92 strOrderDetailID = 44714.92
可以接受的误差 strUserID = 45656 ReturnALlMoneyUnit = 8943.00 strOrderDetailID = 8943.00
可以接受的误差 strUserID = 45104 ReturnALlMoneyUnit = 9996.90 strOrderDetailID = 9996.80
可以接受的误差 strUserID = 45875 ReturnALlMoneyUnit = 39040.34 strOrderDetailID = 39040.17
可以接受的误差 strUserID = 46054 ReturnALlMoneyUnit = 26709.04 strOrderDetailID = 26708.98
可以接受的误差 strUserID = 45242 ReturnALlMoneyUnit = 15664.22 strOrderDetailID = 15664.22
可以接受的误差 strUserID = 46067 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 44721 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 44380 ReturnALlMoneyUnit = 8943.00 strOrderDetailID = 8943.00
可以接受的误差 strUserID = 42167 ReturnALlMoneyUnit = 22357.43 strOrderDetailID = 22357.43
可以接受的误差 strUserID = 45868 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 46007 ReturnALlMoneyUnit = 25312.44 strOrderDetailID = 25312.41
可以接受的误差 strUserID = 45596 ReturnALlMoneyUnit = 4471.47 strOrderDetailID = 4471.47
可以接受的误差 strUserID = 46218 ReturnALlMoneyUnit = 68500.52 strOrderDetailID = 68500.52
可以接受的误差 strUserID = 46222 ReturnALlMoneyUnit = 4494.41 strOrderDetailID = 4494.41
可以接受的误差 strUserID = 46155 ReturnALlMoneyUnit = 4494.41 strOrderDetailID = 4494.41
可以接受的误差 strUserID = 46100 ReturnALlMoneyUnit = 45296.42 strOrderDetailID = 45296.42
可以接受的误差 strUserID = 46164 ReturnALlMoneyUnit = 44944.30 strOrderDetailID = 44944.30
可以接受的误差 strUserID = 46140 ReturnALlMoneyUnit = 8988.88 strOrderDetailID = 8988.88
可以接受的误差 strUserID = 46135 ReturnALlMoneyUnit = 4494.41 strOrderDetailID = 4494.41
可以接受的误差 strUserID = 45245 ReturnALlMoneyUnit = 23145.07 strOrderDetailID = 23145.07
可以接受的误差 strUserID = 46145 ReturnALlMoneyUnit = 90199.50 strOrderDetailID = 90199.22
可以接受的误差 strUserID = 46103 ReturnALlMoneyUnit = 44944.30 strOrderDetailID = 44944.30
可以接受的误差 strUserID = 46156 ReturnALlMoneyUnit = 9339.79 strOrderDetailID = 9339.60
可以接受的误差 strUserID = 46161 ReturnALlMoneyUnit = 8988.88 strOrderDetailID = 8988.88
可以接受的误差 strUserID = 46213 ReturnALlMoneyUnit = 22627.54 strOrderDetailID = 22627.54
已经返还的 大于现在订单统计的 strUserID = 46162 ReturnALlMoneyUnit = 4504.17 orderDescimal = -1489.83
可以接受的误差 strUserID = 44581 ReturnALlMoneyUnit = 4525.49 strOrderDetailID = 4525.49
可以接受的误差 strUserID = 46180 ReturnALlMoneyUnit = 16118.83 strOrderDetailID = 16118.83
可以接受的误差 strUserID = 46181 ReturnALlMoneyUnit = 19558.22 strOrderDetailID = 19558.06
可以接受的误差 strUserID = 43151 ReturnALlMoneyUnit = 9051.05 strOrderDetailID = 9051.05
可以接受的误差 strUserID = 46194 ReturnALlMoneyUnit = 4525.49 strOrderDetailID = 4525.49
可以接受的误差 strUserID = 43926 ReturnALlMoneyUnit = 4525.49 strOrderDetailID = 4525.49
可以接受的误差 strUserID = 46185 ReturnALlMoneyUnit = 22627.54 strOrderDetailID = 22627.54
可以接受的误差 strUserID = 46207 ReturnALlMoneyUnit = 4525.49 strOrderDetailID = 4525.49
可以接受的误差 strUserID = 46206 ReturnALlMoneyUnit = 16359.60 strOrderDetailID = 16359.60
可以接受的误差 strUserID = 46151 ReturnALlMoneyUnit = 9303.58 strOrderDetailID = 9303.39
可以接受的误差 strUserID = 46031 ReturnALlMoneyUnit = 9051.05 strOrderDetailID = 9051.05
可以接受的误差 strUserID = 46288 ReturnALlMoneyUnit = 4546.81 strOrderDetailID = 4546.81
可以接受的误差 strUserID = 43208 ReturnALlMoneyUnit = 4546.81 strOrderDetailID = 4546.81
可以接受的误差 strUserID = 46299 ReturnALlMoneyUnit = 33637.24 strOrderDetailID = 33637.24
可以接受的误差 strUserID = 46307 ReturnALlMoneyUnit = 43053.11 strOrderDetailID = 43053.00
可以接受的误差 strUserID = 45958 ReturnALlMoneyUnit = 31915.66 strOrderDetailID = 31915.64
可以接受的误差 strUserID = 46276 ReturnALlMoneyUnit = 4546.81 strOrderDetailID = 4546.81
可以接受的误差 strUserID = 45897 ReturnALlMoneyUnit = 4546.81 strOrderDetailID = 4546.81
可以接受的误差 strUserID = 46300 ReturnALlMoneyUnit = 4546.81 strOrderDetailID = 4546.81
可以接受的误差 strUserID = 46271 ReturnALlMoneyUnit = 9093.69 strOrderDetailID = 9093.69
可以接受的误差 strUserID = 45808 ReturnALlMoneyUnit = 96922.50 strOrderDetailID = 96922.49
可以接受的误差 strUserID = 46321 ReturnALlMoneyUnit = 4546.81 strOrderDetailID = 4546.81
可以接受的误差 strUserID = 46326 ReturnALlMoneyUnit = 4568.93 strOrderDetailID = 4568.93
可以接受的误差 strUserID = 43918 ReturnALlMoneyUnit = 4568.93 strOrderDetailID = 4568.93
可以接受的误差 strUserID = 46406 ReturnALlMoneyUnit = 22844.73 strOrderDetailID = 22844.73
可以接受的误差 strUserID = 46391 ReturnALlMoneyUnit = 125988.99 strOrderDetailID = 125988.96
可以接受的误差 strUserID = 46379 ReturnALlMoneyUnit = 4568.93 strOrderDetailID = 4568.93
可以接受的误差 strUserID = 46211 ReturnALlMoneyUnit = 14582.23 strOrderDetailID = 14582.23
可以接受的误差 strUserID = 46315 ReturnALlMoneyUnit = 10042.74 strOrderDetailID = 10042.63
已经返还的 小于现在订单统计的 strUserID = 46259 ReturnALlMoneyUnit = 4049.84 orderDescimal = 4161.04
可以接受的误差 strUserID = 46265 ReturnALlMoneyUnit = 4568.93 strOrderDetailID = 4568.93
可以接受的误差 strUserID = 46267 ReturnALlMoneyUnit = 45689.51 strOrderDetailID = 45689.51
可以接受的误差 strUserID = 46392 ReturnALlMoneyUnit = 4568.93 strOrderDetailID = 4568.93
可以接受的误差 strUserID = 41415 ReturnALlMoneyUnit = 26274.22 strOrderDetailID = 26274.22
可以接受的误差 strUserID = 46445 ReturnALlMoneyUnit = 4593.59 strOrderDetailID = 4593.59
可以接受的误差 strUserID = 46416 ReturnALlMoneyUnit = 4593.59 strOrderDetailID = 4593.59
可以接受的误差 strUserID = 46311 ReturnALlMoneyUnit = 123366.43 strOrderDetailID = 123366.31
可以接受的误差 strUserID = 46493 ReturnALlMoneyUnit = 22968.05 strOrderDetailID = 22968.05
可以接受的误差 strUserID = 44109 ReturnALlMoneyUnit = 4593.59 strOrderDetailID = 4593.59
可以接受的误差 strUserID = 45494 ReturnALlMoneyUnit = 10093.96 strOrderDetailID = 10093.86
可以接受的误差 strUserID = 46415 ReturnALlMoneyUnit = 4593.59 strOrderDetailID = 4593.59
可以接受的误差 strUserID = 45769 ReturnALlMoneyUnit = 28018.22 strOrderDetailID = 28018.22
可以接受的误差 strUserID = 46526 ReturnALlMoneyUnit = 9833.18 strOrderDetailID = 9833.03
可以接受的误差 strUserID = 46536 ReturnALlMoneyUnit = 4612.74 strOrderDetailID = 4612.74
可以接受的误差 strUserID = 46015 ReturnALlMoneyUnit = 136699.99 strOrderDetailID = 136699.99
可以接受的误差 strUserID = 46005 ReturnALlMoneyUnit = 4612.74 strOrderDetailID = 4612.74
可以接受的误差 strUserID = 46486 ReturnALlMoneyUnit = 9225.55 strOrderDetailID = 9225.55
可以接受的误差 strUserID = 45968 ReturnALlMoneyUnit = 14603.52 strOrderDetailID = 14603.38
可以接受的误差 strUserID = 46467 ReturnALlMoneyUnit = 4612.74 strOrderDetailID = 4612.74
可以接受的误差 strUserID = 46023 ReturnALlMoneyUnit = 58200.90 strOrderDetailID = 58200.84
可以接受的误差 strUserID = 46027 ReturnALlMoneyUnit = 15699.91 strOrderDetailID = 15699.78
可以接受的误差 strUserID = 46523 ReturnALlMoneyUnit = 9225.55 strOrderDetailID = 9225.55
可以接受的误差 strUserID = 46412 ReturnALlMoneyUnit = 16374.79 strOrderDetailID = 16374.77
可以接受的误差 strUserID = 44624 ReturnALlMoneyUnit = 4612.74 strOrderDetailID = 4612.74
可以接受的误差 strUserID = 46492 ReturnALlMoneyUnit = 23063.81 strOrderDetailID = 23063.81
可以接受的误差 strUserID = 46556 ReturnALlMoneyUnit = 4632.96 strOrderDetailID = 4632.96
可以接受的误差 strUserID = 46570 ReturnALlMoneyUnit = 4632.96 strOrderDetailID = 4632.96
可以接受的误差 strUserID = 46565 ReturnALlMoneyUnit = 23164.92 strOrderDetailID = 23164.92
可以接受的误差 strUserID = 45988 ReturnALlMoneyUnit = 21604.37 strOrderDetailID = 21604.23
可以接受的误差 strUserID = 46498 ReturnALlMoneyUnit = 4632.96 strOrderDetailID = 4632.96
可以接受的误差 strUserID = 46554 ReturnALlMoneyUnit = 13898.95 strOrderDetailID = 13898.95
可以接受的误差 strUserID = 43459 ReturnALlMoneyUnit = 87614.02 strOrderDetailID = 87614.00
可以接受的误差 strUserID = 46291 ReturnALlMoneyUnit = 154068.45 strOrderDetailID = 154068.43
可以接受的误差 strUserID = 46563 ReturnALlMoneyUnit = 14548.16 strOrderDetailID = 14548.11
可以接受的误差 strUserID = 44750 ReturnALlMoneyUnit = 13898.95 strOrderDetailID = 13898.95
可以接受的误差 strUserID = 42952 ReturnALlMoneyUnit = 4656.98 strOrderDetailID = 4656.98
可以接受的误差 strUserID = 46673 ReturnALlMoneyUnit = 4656.98 strOrderDetailID = 4656.98
可以接受的误差 strUserID = 46653 ReturnALlMoneyUnit = 129714.57 strOrderDetailID = 129714.53
可以接受的误差 strUserID = 46642 ReturnALlMoneyUnit = 9314.02 strOrderDetailID = 9314.02
可以接受的误差 strUserID = 46643 ReturnALlMoneyUnit = 4656.98 strOrderDetailID = 4656.98
可以接受的误差 strUserID = 44108 ReturnALlMoneyUnit = 26068.69 strOrderDetailID = 26068.67
可以接受的误差 strUserID = 44230 ReturnALlMoneyUnit = 4656.98 strOrderDetailID = 4656.98
可以接受的误差 strUserID = 46696 ReturnALlMoneyUnit = 47511.80 strOrderDetailID = 47511.64
可以接受的误差 strUserID = 46375 ReturnALlMoneyUnit = 23515.24 strOrderDetailID = 23515.24
可以接受的误差 strUserID = 46648 ReturnALlMoneyUnit = 4656.98 strOrderDetailID = 4656.98
可以接受的误差 strUserID = 46628 ReturnALlMoneyUnit = 133987.45 strOrderDetailID = 133987.34
可以接受的误差 strUserID = 45280 ReturnALlMoneyUnit = 4656.98 strOrderDetailID = 4656.98
可以接受的误差 strUserID = 46595 ReturnALlMoneyUnit = 4656.98 strOrderDetailID = 4656.98
可以接受的误差 strUserID = 46594 ReturnALlMoneyUnit = 4656.98 strOrderDetailID = 4656.98
可以接受的误差 strUserID = 45176 ReturnALlMoneyUnit = 145029.76 strOrderDetailID = 145029.66
可以接受的误差 strUserID = 46721 ReturnALlMoneyUnit = 4675.09 strOrderDetailID = 4675.09
可以接受的误差 strUserID = 46724 ReturnALlMoneyUnit = 4675.09 strOrderDetailID = 4675.09
可以接受的误差 strUserID = 46717 ReturnALlMoneyUnit = 4675.09 strOrderDetailID = 4675.09
可以接受的误差 strUserID = 42479 ReturnALlMoneyUnit = 4675.09 strOrderDetailID = 4675.09
可以接受的误差 strUserID = 46543 ReturnALlMoneyUnit = 4675.09 strOrderDetailID = 4675.09
可以接受的误差 strUserID = 46138 ReturnALlMoneyUnit = 9350.23 strOrderDetailID = 9350.23
可以接受的误差 strUserID = 46489 ReturnALlMoneyUnit = 4675.09 strOrderDetailID = 4675.09
可以接受的误差 strUserID = 46349 ReturnALlMoneyUnit = 41484.57 strOrderDetailID = 41484.50
可以接受的误差 strUserID = 45657 ReturnALlMoneyUnit = 10027.09 strOrderDetailID = 10026.96
可以接受的误差 strUserID = 46757 ReturnALlMoneyUnit = 4698.15 strOrderDetailID = 4698.15
可以接受的误差 strUserID = 45954 ReturnALlMoneyUnit = 9726.40 strOrderDetailID = 9726.23
可以接受的误差 strUserID = 46756 ReturnALlMoneyUnit = 4698.15 strOrderDetailID = 4698.15
可以接受的误差 strUserID = 46677 ReturnALlMoneyUnit = 49592.84 strOrderDetailID = 49592.71
可以接受的误差 strUserID = 46727 ReturnALlMoneyUnit = 23490.84 strOrderDetailID = 23490.84
可以接受的误差 strUserID = 46726 ReturnALlMoneyUnit = 10344.81 strOrderDetailID = 10344.74
可以接受的误差 strUserID = 45978 ReturnALlMoneyUnit = 16164.59 strOrderDetailID = 16164.59
可以接受的误差 strUserID = 46783 ReturnALlMoneyUnit = 66053.78 strOrderDetailID = 66053.72
可以接受的误差 strUserID = 45337 ReturnALlMoneyUnit = 9437.48 strOrderDetailID = 9437.48
可以接受的误差 strUserID = 41419 ReturnALlMoneyUnit = 48834.98 strOrderDetailID = 48834.84
可以接受的误差 strUserID = 46804 ReturnALlMoneyUnit = 47187.36 strOrderDetailID = 47187.36
可以接受的误差 strUserID = 46801 ReturnALlMoneyUnit = 9437.48 strOrderDetailID = 9437.48
可以接受的误差 strUserID = 46747 ReturnALlMoneyUnit = 4718.72 strOrderDetailID = 4718.72
可以接受的误差 strUserID = 46792 ReturnALlMoneyUnit = 4718.72 strOrderDetailID = 4718.72
可以接受的误差 strUserID = 46654 ReturnALlMoneyUnit = 4718.72 strOrderDetailID = 4718.72
可以接受的误差 strUserID = 46833 ReturnALlMoneyUnit = 4740.10 strOrderDetailID = 4740.10
可以接受的误差 strUserID = 46871 ReturnALlMoneyUnit = 4740.10 strOrderDetailID = 4740.10
可以接受的误差 strUserID = 46571 ReturnALlMoneyUnit = 14220.33 strOrderDetailID = 14220.33
可以接受的误差 strUserID = 43586 ReturnALlMoneyUnit = 83781.23 strOrderDetailID = 83781.17
可以接受的误差 strUserID = 46835 ReturnALlMoneyUnit = 4740.10 strOrderDetailID = 4740.10
可以接受的误差 strUserID = 46844 ReturnALlMoneyUnit = 4740.10 strOrderDetailID = 4740.10
可以接受的误差 strUserID = 46847 ReturnALlMoneyUnit = 4740.10 strOrderDetailID = 4740.10
可以接受的误差 strUserID = 46837 ReturnALlMoneyUnit = 28440.67 strOrderDetailID = 28440.67
可以接受的误差 strUserID = 46739 ReturnALlMoneyUnit = 23805.20 strOrderDetailID = 23805.20
可以接受的误差 strUserID = 46063 ReturnALlMoneyUnit = 4761.03 strOrderDetailID = 4761.03
可以接受的误差 strUserID = 46896 ReturnALlMoneyUnit = 47780.69 strOrderDetailID = 47780.69
可以接受的误差 strUserID = 46894 ReturnALlMoneyUnit = 9556.15 strOrderDetailID = 9556.15
可以接受的误差 strUserID = 46566 ReturnALlMoneyUnit = 48225.02 strOrderDetailID = 48224.87
可以接受的误差 strUserID = 46903 ReturnALlMoneyUnit = 9556.15 strOrderDetailID = 9556.15
可以接受的误差 strUserID = 46773 ReturnALlMoneyUnit = 37670.97 strOrderDetailID = 37670.95
可以接受的误差 strUserID = 46879 ReturnALlMoneyUnit = 4778.06 strOrderDetailID = 4778.06
可以接受的误差 strUserID = 46640 ReturnALlMoneyUnit = 4778.06 strOrderDetailID = 4778.06
可以接受的误差 strUserID = 46231 ReturnALlMoneyUnit = 10251.87 strOrderDetailID = 10251.76
可以接受的误差 strUserID = 43261 ReturnALlMoneyUnit = 4778.06 strOrderDetailID = 4778.06
可以接受的误差 strUserID = 46999 ReturnALlMoneyUnit = 25681.33 strOrderDetailID = 25681.27
可以接受的误差 strUserID = 46919 ReturnALlMoneyUnit = 4800.56 strOrderDetailID = 4800.56
可以接受的误差 strUserID = 43687 ReturnALlMoneyUnit = 213161.00 strOrderDetailID = 213160.92
可以接受的误差 strUserID = 46532 ReturnALlMoneyUnit = 4800.56 strOrderDetailID = 4800.56
可以接受的误差 strUserID = 46989 ReturnALlMoneyUnit = 4821.58 strOrderDetailID = 4821.58
可以接受的误差 strUserID = 46965 ReturnALlMoneyUnit = 96431.89 strOrderDetailID = 96431.89
可以接受的误差 strUserID = 46968 ReturnALlMoneyUnit = 4821.58 strOrderDetailID = 4821.58
可以接受的误差 strUserID = 46967 ReturnALlMoneyUnit = 4821.58 strOrderDetailID = 4821.58
可以接受的误差 strUserID = 46946 ReturnALlMoneyUnit = 10173.58 strOrderDetailID = 10173.45
可以接受的误差 strUserID = 47006 ReturnALlMoneyUnit = 4845.35 strOrderDetailID = 4845.35
可以接受的误差 strUserID = 47001 ReturnALlMoneyUnit = 26963.69 strOrderDetailID = 26963.60
可以接受的误差 strUserID = 46612 ReturnALlMoneyUnit = 152168.66 strOrderDetailID = 152168.55
可以接受的误差 strUserID = 45663 ReturnALlMoneyUnit = 9690.73 strOrderDetailID = 9690.73
可以接受的误差 strUserID = 46995 ReturnALlMoneyUnit = 147862.57 strOrderDetailID = 147862.58
可以接受的误差 strUserID = 46562 ReturnALlMoneyUnit = 9690.73 strOrderDetailID = 9690.73
可以接受的误差 strUserID = 46982 ReturnALlMoneyUnit = 9690.73 strOrderDetailID = 9690.73
可以接受的误差 strUserID = 45235 ReturnALlMoneyUnit = 48669.35 strOrderDetailID = 48669.35
可以接受的误差 strUserID = 47049 ReturnALlMoneyUnit = 4866.92 strOrderDetailID = 4866.92
可以接受的误差 strUserID = 47010 ReturnALlMoneyUnit = 208699.44 strOrderDetailID = 208699.21
可以接受的误差 strUserID = 47047 ReturnALlMoneyUnit = 97541.67 strOrderDetailID = 97541.46
可以接受的误差 strUserID = 47051 ReturnALlMoneyUnit = 9733.88 strOrderDetailID = 9733.88
可以接受的误差 strUserID = 45736 ReturnALlMoneyUnit = 4866.92 strOrderDetailID = 4866.92
可以接受的误差 strUserID = 46688 ReturnALlMoneyUnit = 176645.47 strOrderDetailID = 176645.38
可以接受的误差 strUserID = 46770 ReturnALlMoneyUnit = 26714.61 strOrderDetailID = 26714.58
可以接受的误差 strUserID = 46553 ReturnALlMoneyUnit = 15679.71 strOrderDetailID = 15679.59
可以接受的误差 strUserID = 47092 ReturnALlMoneyUnit = 48872.23 strOrderDetailID = 48872.23
可以接受的误差 strUserID = 47088 ReturnALlMoneyUnit = 4887.21 strOrderDetailID = 4887.21
可以接受的误差 strUserID = 47081 ReturnALlMoneyUnit = 31155.19 strOrderDetailID = 31155.14
可以接受的误差 strUserID = 47075 ReturnALlMoneyUnit = 41143.67 strOrderDetailID = 41143.57
可以接受的误差 strUserID = 47072 ReturnALlMoneyUnit = 4887.21 strOrderDetailID = 4887.21
可以接受的误差 strUserID = 47085 ReturnALlMoneyUnit = 49057.31 strOrderDetailID = 49057.31
可以接受的误差 strUserID = 46641 ReturnALlMoneyUnit = 4905.72 strOrderDetailID = 4905.72
可以接受的误差 strUserID = 47112 ReturnALlMoneyUnit = 9811.48 strOrderDetailID = 9811.48
可以接受的误差 strUserID = 47098 ReturnALlMoneyUnit = 50630.66 strOrderDetailID = 50630.53
可以接受的误差 strUserID = 47123 ReturnALlMoneyUnit = 9976.10 strOrderDetailID = 9975.94
可以接受的误差 strUserID = 47116 ReturnALlMoneyUnit = 10527.34 strOrderDetailID = 10527.26
可以接受的误差 strUserID = 47111 ReturnALlMoneyUnit = 26216.97 strOrderDetailID = 26216.97
可以接受的误差 strUserID = 47099 ReturnALlMoneyUnit = 10233.57 strOrderDetailID = 10233.44
可以接受的误差 strUserID = 47017 ReturnALlMoneyUnit = 4926.77 strOrderDetailID = 4926.77
可以接受的误差 strUserID = 47153 ReturnALlMoneyUnit = 4926.77 strOrderDetailID = 4926.77
可以接受的误差 strUserID = 47131 ReturnALlMoneyUnit = 9853.58 strOrderDetailID = 9853.58
可以接受的误差 strUserID = 47157 ReturnALlMoneyUnit = 9853.58 strOrderDetailID = 9853.58
可以接受的误差 strUserID = 46859 ReturnALlMoneyUnit = 4926.77 strOrderDetailID = 4926.77
可以接受的误差 strUserID = 46970 ReturnALlMoneyUnit = 15205.50 strOrderDetailID = 15205.50
可以接受的误差 strUserID = 47184 ReturnALlMoneyUnit = 10200.33 strOrderDetailID = 10200.18
可以接受的误差 strUserID = 47215 ReturnALlMoneyUnit = 99302.90 strOrderDetailID = 99302.70
可以接受的误差 strUserID = 47216 ReturnALlMoneyUnit = 99130.40 strOrderDetailID = 99130.20
可以接受的误差 strUserID = 47219 ReturnALlMoneyUnit = 25529.98 strOrderDetailID = 25529.98
可以接受的误差 strUserID = 47205 ReturnALlMoneyUnit = 19806.48 strOrderDetailID = 19806.38
可以接受的误差 strUserID = 45887 ReturnALlMoneyUnit = 4946.70 strOrderDetailID = 4946.70
可以接受的误差 strUserID = 47201 ReturnALlMoneyUnit = 9893.44 strOrderDetailID = 9893.44
可以接受的误差 strUserID = 47119 ReturnALlMoneyUnit = 9893.44 strOrderDetailID = 9893.44
可以接受的误差 strUserID = 47198 ReturnALlMoneyUnit = 24733.55 strOrderDetailID = 24733.55
可以接受的误差 strUserID = 41365 ReturnALlMoneyUnit = 4946.70 strOrderDetailID = 4946.70
可以接受的误差 strUserID = 47269 ReturnALlMoneyUnit = 14898.97 strOrderDetailID = 14898.97
可以接受的误差 strUserID = 47252 ReturnALlMoneyUnit = 10440.12 strOrderDetailID = 10440.01
可以接受的误差 strUserID = 47195 ReturnALlMoneyUnit = 4966.31 strOrderDetailID = 4966.31
可以接受的误差 strUserID = 47122 ReturnALlMoneyUnit = 4966.31 strOrderDetailID = 4966.31
可以接受的误差 strUserID = 46929 ReturnALlMoneyUnit = 15568.74 strOrderDetailID = 15568.74
可以接受的误差 strUserID = 47256 ReturnALlMoneyUnit = 20484.47 strOrderDetailID = 20484.31
可以接受的误差 strUserID = 46204 ReturnALlMoneyUnit = 61012.70 strOrderDetailID = 61012.66
可以接受的误差 strUserID = 46795 ReturnALlMoneyUnit = 10088.64 strOrderDetailID = 10088.48
可以接受的误差 strUserID = 47136 ReturnALlMoneyUnit = 10137.41 strOrderDetailID = 10137.25
可以接受的误差 strUserID = 45867 ReturnALlMoneyUnit = 14950.72 strOrderDetailID = 14950.72
可以接受的误差 strUserID = 45279 ReturnALlMoneyUnit = 141316.86 strOrderDetailID = 141316.82
可以接受的误差 strUserID = 47217 ReturnALlMoneyUnit = 4983.56 strOrderDetailID = 4983.56
可以接受的误差 strUserID = 47306 ReturnALlMoneyUnit = 104303.50 strOrderDetailID = 104303.40
可以接受的误差 strUserID = 46081 ReturnALlMoneyUnit = 65368.80 strOrderDetailID = 65368.74
可以接受的误差 strUserID = 46980 ReturnALlMoneyUnit = 4983.56 strOrderDetailID = 4983.56
可以接受的误差 strUserID = 47370 ReturnALlMoneyUnit = 25803.66 strOrderDetailID = 25803.66
可以接受的误差 strUserID = 47310 ReturnALlMoneyUnit = 4983.56 strOrderDetailID = 4983.56
可以接受的误差 strUserID = 47312 ReturnALlMoneyUnit = 4983.56 strOrderDetailID = 4983.56
可以接受的误差 strUserID = 46491 ReturnALlMoneyUnit = 24917.87 strOrderDetailID = 24917.87
可以接受的误差 strUserID = 46745 ReturnALlMoneyUnit = 9967.17 strOrderDetailID = 9967.17
可以接受的误差 strUserID = 47304 ReturnALlMoneyUnit = 4983.56 strOrderDetailID = 4983.56
可以接受的误差 strUserID = 45960 ReturnALlMoneyUnit = 15909.71 strOrderDetailID = 15909.71
可以接受的误差 strUserID = 46977 ReturnALlMoneyUnit = 5006.63 strOrderDetailID = 5006.63
可以接受的误差 strUserID = 47355 ReturnALlMoneyUnit = 5006.63 strOrderDetailID = 5006.63
可以接受的误差 strUserID = 47347 ReturnALlMoneyUnit = 10013.31 strOrderDetailID = 10013.31
可以接受的误差 strUserID = 47346 ReturnALlMoneyUnit = 15799.13 strOrderDetailID = 15799.01
已经返还的 大于现在订单统计的 strUserID = 44323 ReturnALlMoneyUnit = 10013.31 orderDescimal = -49660.72
可以接受的误差 strUserID = 41274 ReturnALlMoneyUnit = 10013.31 strOrderDetailID = 10013.31
可以接受的误差 strUserID = 45761 ReturnALlMoneyUnit = 20920.89 strOrderDetailID = 20920.79
可以接受的误差 strUserID = 47325 ReturnALlMoneyUnit = 103569.58 strOrderDetailID = 103569.49
可以接受的误差 strUserID = 47397 ReturnALlMoneyUnit = 5028.20 strOrderDetailID = 5028.20
可以接受的误差 strUserID = 47282 ReturnALlMoneyUnit = 5028.20 strOrderDetailID = 5028.20
可以接受的误差 strUserID = 43175 ReturnALlMoneyUnit = 5028.20 strOrderDetailID = 5028.20
可以接受的误差 strUserID = 47379 ReturnALlMoneyUnit = 5028.20 strOrderDetailID = 5028.20
可以接受的误差 strUserID = 47404 ReturnALlMoneyUnit = 5028.20 strOrderDetailID = 5028.20
可以接受的误差 strUserID = 47401 ReturnALlMoneyUnit = 5028.20 strOrderDetailID = 5028.20
可以接受的误差 strUserID = 47452 ReturnALlMoneyUnit = 10096.55 strOrderDetailID = 10096.55
可以接受的误差 strUserID = 43582 ReturnALlMoneyUnit = 41451.00 strOrderDetailID = 41450.97
可以接受的误差 strUserID = 47436 ReturnALlMoneyUnit = 31128.25 strOrderDetailID = 31128.19
可以接受的误差 strUserID = 47095 ReturnALlMoneyUnit = 21469.59 strOrderDetailID = 21469.54
可以接受的误差 strUserID = 44687 ReturnALlMoneyUnit = 5048.25 strOrderDetailID = 5048.25
可以接受的误差 strUserID = 47413 ReturnALlMoneyUnit = 75252.94 strOrderDetailID = 75252.88
可以接受的误差 strUserID = 47483 ReturnALlMoneyUnit = 5070.34 strOrderDetailID = 5070.34
可以接受的误差 strUserID = 47509 ReturnALlMoneyUnit = 55062.26 strOrderDetailID = 55062.25
可以接受的误差 strUserID = 47220 ReturnALlMoneyUnit = 16239.59 strOrderDetailID = 16239.59
可以接受的误差 strUserID = 47255 ReturnALlMoneyUnit = 5095.39 strOrderDetailID = 5095.39
可以接受的误差 strUserID = 47508 ReturnALlMoneyUnit = 64831.22 strOrderDetailID = 64831.09
可以接受的误差 strUserID = 45029 ReturnALlMoneyUnit = 10190.82 strOrderDetailID = 10190.82
可以接受的误差 strUserID = 47590 ReturnALlMoneyUnit = 10423.55 strOrderDetailID = 10423.41
可以接受的误差 strUserID = 47572 ReturnALlMoneyUnit = 5122.29 strOrderDetailID = 5122.29
可以接受的误差 strUserID = 47259 ReturnALlMoneyUnit = 5122.29 strOrderDetailID = 5122.29
可以接受的误差 strUserID = 47667 ReturnALlMoneyUnit = 40648.13 strOrderDetailID = 40648.13
可以接受的误差 strUserID = 45909 ReturnALlMoneyUnit = 143601.02 strOrderDetailID = 143601.03
可以接受的误差 strUserID = 46530 ReturnALlMoneyUnit = 37660.47 strOrderDetailID = 37660.47
可以接受的误差 strUserID = 43983 ReturnALlMoneyUnit = 5145.81 strOrderDetailID = 5145.81
可以接受的误差 strUserID = 47728 ReturnALlMoneyUnit = 16158.84 strOrderDetailID = 16158.84
可以接受的误差 strUserID = 47740 ReturnALlMoneyUnit = 5171.06 strOrderDetailID = 5171.06
可以接受的误差 strUserID = 44680 ReturnALlMoneyUnit = 5171.06 strOrderDetailID = 5171.06
可以接受的误差 strUserID = 47691 ReturnALlMoneyUnit = 96636.80 strOrderDetailID = 96636.67
可以接受的误差 strUserID = 47705 ReturnALlMoneyUnit = 10865.81 strOrderDetailID = 10865.75
可以接受的误差 strUserID = 45503 ReturnALlMoneyUnit = 5171.06 strOrderDetailID = 5171.06
可以接受的误差 strUserID = 46167 ReturnALlMoneyUnit = 25855.34 strOrderDetailID = 25855.34
可以接受的误差 strUserID = 47684 ReturnALlMoneyUnit = 5171.06 strOrderDetailID = 5171.06
可以接受的误差 strUserID = 47774 ReturnALlMoneyUnit = 5195.00 strOrderDetailID = 5195.00
可以接受的误差 strUserID = 46856 ReturnALlMoneyUnit = 5195.00 strOrderDetailID = 5195.00
可以接受的误差 strUserID = 44677 ReturnALlMoneyUnit = 37521.63 strOrderDetailID = 37521.63
可以接受的误差 strUserID = 47776 ReturnALlMoneyUnit = 10390.04 strOrderDetailID = 10390.04
可以接受的误差 strUserID = 47726 ReturnALlMoneyUnit = 10390.04 strOrderDetailID = 10390.04
可以接受的误差 strUserID = 46703 ReturnALlMoneyUnit = 32821.98 strOrderDetailID = 32821.97
可以接受的误差 strUserID = 47375 ReturnALlMoneyUnit = 5220.39 strOrderDetailID = 5220.39
可以接受的误差 strUserID = 43351 ReturnALlMoneyUnit = 167129.47 strOrderDetailID = 167129.45
可以接受的误差 strUserID = 47570 ReturnALlMoneyUnit = 5220.39 strOrderDetailID = 5220.39
可以接受的误差 strUserID = 47671 ReturnALlMoneyUnit = 59710.61 strOrderDetailID = 59710.55
可以接受的误差 strUserID = 47810 ReturnALlMoneyUnit = 137476.58 strOrderDetailID = 137476.59
可以接受的误差 strUserID = 44632 ReturnALlMoneyUnit = 55607.86 strOrderDetailID = 55607.85
可以接受的误差 strUserID = 46922 ReturnALlMoneyUnit = 53470.84 strOrderDetailID = 53470.76
可以接受的误差 strUserID = 47840 ReturnALlMoneyUnit = 26102.01 strOrderDetailID = 26102.01
可以接受的误差 strUserID = 47685 ReturnALlMoneyUnit = 26102.01 strOrderDetailID = 26102.01
可以接受的误差 strUserID = 47948 ReturnALlMoneyUnit = 10440.83 strOrderDetailID = 10440.83
可以接受的误差 strUserID = 47921 ReturnALlMoneyUnit = 10440.83 strOrderDetailID = 10440.83
可以接受的误差 strUserID = 47917 ReturnALlMoneyUnit = 10440.83 strOrderDetailID = 10440.83
可以接受的误差 strUserID = 47527 ReturnALlMoneyUnit = 5220.39 strOrderDetailID = 5220.39
可以接受的误差 strUserID = 47901 ReturnALlMoneyUnit = 16174.03 strOrderDetailID = 16174.03
可以接受的误差 strUserID = 47914 ReturnALlMoneyUnit = 54484.29 strOrderDetailID = 54484.29
可以接受的误差 strUserID = 47851 ReturnALlMoneyUnit = 5220.39 strOrderDetailID = 5220.39
可以接受的误差 strUserID = 47849 ReturnALlMoneyUnit = 5220.39 strOrderDetailID = 5220.39
可以接受的误差 strUserID = 44841 ReturnALlMoneyUnit = 49579.41 strOrderDetailID = 49579.41
可以接受的误差 strUserID = 47813 ReturnALlMoneyUnit = 52204.01 strOrderDetailID = 52204.01
可以接受的误差 strUserID = 47832 ReturnALlMoneyUnit = 52204.01 strOrderDetailID = 52204.01
可以接受的误差 strUserID = 46350 ReturnALlMoneyUnit = 52204.01 strOrderDetailID = 52204.01
可以接受的误差 strUserID = 47822 ReturnALlMoneyUnit = 26102.01 strOrderDetailID = 26102.01
可以接受的误差 strUserID = 43595 ReturnALlMoneyUnit = 26102.01 strOrderDetailID = 26102.01
可以接受的误差 strUserID = 47424 ReturnALlMoneyUnit = 26102.01 strOrderDetailID = 26102.01
可以接受的误差 strUserID = 47772 ReturnALlMoneyUnit = 26268.52 strOrderDetailID = 26268.52
可以接受的误差 strUserID = 47843 ReturnALlMoneyUnit = 26102.01 strOrderDetailID = 26102.01
可以接受的误差 strUserID = 44181 ReturnALlMoneyUnit = 5220.39 strOrderDetailID = 5220.39
可以接受的误差 strUserID = 47974 ReturnALlMoneyUnit = 26267.96 strOrderDetailID = 26267.96
可以接受的误差 strUserID = 44943 ReturnALlMoneyUnit = 52535.91 strOrderDetailID = 52535.91
可以接受的误差 strUserID = 47872 ReturnALlMoneyUnit = 105295.03 strOrderDetailID = 105294.87
可以接受的误差 strUserID = 47941 ReturnALlMoneyUnit = 52535.91 strOrderDetailID = 52535.91
可以接受的误差 strUserID = 47876 ReturnALlMoneyUnit = 16643.04 strOrderDetailID = 16643.04
可以接受的误差 strUserID = 48070 ReturnALlMoneyUnit = 5253.58 strOrderDetailID = 5253.58
可以接受的误差 strUserID = 47888 ReturnALlMoneyUnit = 5253.58 strOrderDetailID = 5253.58
可以接受的误差 strUserID = 46777 ReturnALlMoneyUnit = 10507.21 strOrderDetailID = 10507.21
可以接受的误差 strUserID = 47799 ReturnALlMoneyUnit = 26267.96 strOrderDetailID = 26267.96
可以接受的误差 strUserID = 47937 ReturnALlMoneyUnit = 15783.08 strOrderDetailID = 15783.08
可以接受的误差 strUserID = 47926 ReturnALlMoneyUnit = 26267.96 strOrderDetailID = 26267.96
可以接受的误差 strUserID = 47899 ReturnALlMoneyUnit = 26976.10 strOrderDetailID = 26975.96
可以接受的误差 strUserID = 47725 ReturnALlMoneyUnit = 10507.21 strOrderDetailID = 10507.21
可以接受的误差 strUserID = 48132 ReturnALlMoneyUnit = 26379.53 strOrderDetailID = 26379.53
可以接受的误差 strUserID = 48049 ReturnALlMoneyUnit = 5275.89 strOrderDetailID = 5275.89
可以接受的误差 strUserID = 47423 ReturnALlMoneyUnit = 5275.89 strOrderDetailID = 5275.89
可以接受的误差 strUserID = 48085 ReturnALlMoneyUnit = 37256.79 strOrderDetailID = 37256.71
可以接受的误差 strUserID = 48044 ReturnALlMoneyUnit = 83466.46 strOrderDetailID = 83466.40
可以接受的误差 strUserID = 48042 ReturnALlMoneyUnit = 15827.71 strOrderDetailID = 15827.71
可以接受的误差 strUserID = 41426 ReturnALlMoneyUnit = 92372.68 strOrderDetailID = 92372.63
可以接受的误差 strUserID = 47946 ReturnALlMoneyUnit = 16003.34 strOrderDetailID = 16003.34
可以接受的误差 strUserID = 48027 ReturnALlMoneyUnit = 105518.18 strOrderDetailID = 105518.18
可以接受的误差 strUserID = 47875 ReturnALlMoneyUnit = 10551.84 strOrderDetailID = 10551.84
可以接受的误差 strUserID = 47998 ReturnALlMoneyUnit = 10551.84 strOrderDetailID = 10551.84
可以接受的误差 strUserID = 41229 ReturnALlMoneyUnit = 26379.53 strOrderDetailID = 26379.53
可以接受的误差 strUserID = 48072 ReturnALlMoneyUnit = 32979.17 strOrderDetailID = 32979.16
可以接受的误差 strUserID = 47752 ReturnALlMoneyUnit = 107212.97 strOrderDetailID = 107212.84
可以接受的误差 strUserID = 48029 ReturnALlMoneyUnit = 10551.84 strOrderDetailID = 10551.84
可以接受的误差 strUserID = 47496 ReturnALlMoneyUnit = 5275.89 strOrderDetailID = 5275.89
可以接受的误差 strUserID = 48002 ReturnALlMoneyUnit = 53018.59 strOrderDetailID = 53018.48
可以接受的误差 strUserID = 48226 ReturnALlMoneyUnit = 497886.56 strOrderDetailID = 497886.37
可以接受的误差 strUserID = 47826 ReturnALlMoneyUnit = 26506.07 strOrderDetailID = 26506.07
可以接受的误差 strUserID = 48054 ReturnALlMoneyUnit = 5301.20 strOrderDetailID = 5301.20
可以接受的误差 strUserID = 47254 ReturnALlMoneyUnit = 5301.20 strOrderDetailID = 5301.20
可以接受的误差 strUserID = 48123 ReturnALlMoneyUnit = 5301.20 strOrderDetailID = 5301.20
可以接受的误差 strUserID = 48174 ReturnALlMoneyUnit = 227197.00 strOrderDetailID = 227196.89
可以接受的误差 strUserID = 41399 ReturnALlMoneyUnit = 5301.20 strOrderDetailID = 5301.20
可以接受的误差 strUserID = 48089 ReturnALlMoneyUnit = 26506.07 strOrderDetailID = 26506.07
可以接受的误差 strUserID = 48109 ReturnALlMoneyUnit = 74953.73 strOrderDetailID = 74953.73
可以接受的误差 strUserID = 48069 ReturnALlMoneyUnit = 21204.87 strOrderDetailID = 21204.87
可以接受的误差 strUserID = 48135 ReturnALlMoneyUnit = 26506.07 strOrderDetailID = 26506.07
可以接受的误差 strUserID = 48101 ReturnALlMoneyUnit = 26506.07 strOrderDetailID = 26506.07
可以接受的误差 strUserID = 48102 ReturnALlMoneyUnit = 26506.07 strOrderDetailID = 26506.07
可以接受的误差 strUserID = 48126 ReturnALlMoneyUnit = 55401.57 strOrderDetailID = 55401.58
可以接受的误差 strUserID = 48121 ReturnALlMoneyUnit = 26506.07 strOrderDetailID = 26506.07
可以接受的误差 strUserID = 48034 ReturnALlMoneyUnit = 26506.07 strOrderDetailID = 26506.07
可以接受的误差 strUserID = 47959 ReturnALlMoneyUnit = 10655.65 strOrderDetailID = 10655.65
可以接受的误差 strUserID = 46487 ReturnALlMoneyUnit = 5327.80 strOrderDetailID = 5327.80
可以接受的误差 strUserID = 48179 ReturnALlMoneyUnit = 5327.80 strOrderDetailID = 5327.80
可以接受的误差 strUserID = 48238 ReturnALlMoneyUnit = 84206.18 strOrderDetailID = 84206.13
可以接受的误差 strUserID = 48227 ReturnALlMoneyUnit = 391862.21 strOrderDetailID = 391862.03
可以接受的误差 strUserID = 48186 ReturnALlMoneyUnit = 10655.65 strOrderDetailID = 10655.65
已经返还的 小于现在订单统计的 strUserID = 46269 ReturnALlMoneyUnit = 63308.79 orderDescimal = 63342.17
可以接受的误差 strUserID = 48140 ReturnALlMoneyUnit = 26639.05 strOrderDetailID = 26639.05
可以接受的误差 strUserID = 47834 ReturnALlMoneyUnit = 5351.95 strOrderDetailID = 5351.95
可以接受的误差 strUserID = 48194 ReturnALlMoneyUnit = 11186.97 strOrderDetailID = 11186.95
可以接受的误差 strUserID = 48249 ReturnALlMoneyUnit = 26759.81 strOrderDetailID = 26759.81
可以接受的误差 strUserID = 48313 ReturnALlMoneyUnit = 5351.95 strOrderDetailID = 5351.95
可以接受的误差 strUserID = 48118 ReturnALlMoneyUnit = 10973.57 strOrderDetailID = 10973.49
可以接受的误差 strUserID = 48363 ReturnALlMoneyUnit = 5378.02 strOrderDetailID = 5378.02
可以接受的误差 strUserID = 48429 ReturnALlMoneyUnit = 64548.56 strOrderDetailID = 64548.56
可以接受的误差 strUserID = 48288 ReturnALlMoneyUnit = 108518.04 strOrderDetailID = 108517.94
可以接受的误差 strUserID = 48308 ReturnALlMoneyUnit = 40287.96 strOrderDetailID = 40287.96
可以接受的误差 strUserID = 48406 ReturnALlMoneyUnit = 108043.60 strOrderDetailID = 108043.49
可以接受的误差 strUserID = 48404 ReturnALlMoneyUnit = 109034.32 strOrderDetailID = 109034.24
可以接受的误差 strUserID = 48376 ReturnALlMoneyUnit = 26890.18 strOrderDetailID = 26890.18
可以接受的误差 strUserID = 48463 ReturnALlMoneyUnit = 26890.18 strOrderDetailID = 26890.18
可以接受的误差 strUserID = 48409 ReturnALlMoneyUnit = 5378.02 strOrderDetailID = 5378.02
可以接受的误差 strUserID = 48411 ReturnALlMoneyUnit = 26890.18 strOrderDetailID = 26890.18
可以接受的误差 strUserID = 48397 ReturnALlMoneyUnit = 27727.41 strOrderDetailID = 27727.41
可以接受的误差 strUserID = 48362 ReturnALlMoneyUnit = 5378.02 strOrderDetailID = 5378.02
可以接受的误差 strUserID = 48360 ReturnALlMoneyUnit = 26890.18 strOrderDetailID = 26890.18
可以接受的误差 strUserID = 48338 ReturnALlMoneyUnit = 21512.17 strOrderDetailID = 21512.17
可以接受的误差 strUserID = 48442 ReturnALlMoneyUnit = 54032.27 strOrderDetailID = 54032.27
可以接受的误差 strUserID = 48575 ReturnALlMoneyUnit = 5403.21 strOrderDetailID = 5403.21
可以接受的误差 strUserID = 46807 ReturnALlMoneyUnit = 27161.07 strOrderDetailID = 27161.07
可以接受的误差 strUserID = 48425 ReturnALlMoneyUnit = 168957.73 strOrderDetailID = 168957.71
可以接受的误差 strUserID = 48430 ReturnALlMoneyUnit = 27016.13 strOrderDetailID = 27016.13
可以接受的误差 strUserID = 48460 ReturnALlMoneyUnit = 5403.21 strOrderDetailID = 5403.21
可以接受的误差 strUserID = 44869 ReturnALlMoneyUnit = 38309.41 strOrderDetailID = 38309.41
可以接受的误差 strUserID = 48407 ReturnALlMoneyUnit = 44875.73 strOrderDetailID = 44875.73
可以接受的误差 strUserID = 46126 ReturnALlMoneyUnit = 5403.21 strOrderDetailID = 5403.21
可以接受的误差 strUserID = 44667 ReturnALlMoneyUnit = 5403.21 strOrderDetailID = 5403.21
可以接受的误差 strUserID = 48439 ReturnALlMoneyUnit = 5403.21 strOrderDetailID = 5403.21
可以接受的误差 strUserID = 48387 ReturnALlMoneyUnit = 5403.21 strOrderDetailID = 5403.21
可以接受的误差 strUserID = 47445 ReturnALlMoneyUnit = 5426.30 strOrderDetailID = 5426.30
可以接受的误差 strUserID = 48479 ReturnALlMoneyUnit = 5426.30 strOrderDetailID = 5426.30
可以接受的误差 strUserID = 44781 ReturnALlMoneyUnit = 11343.36 strOrderDetailID = 11343.35
可以接受的误差 strUserID = 48517 ReturnALlMoneyUnit = 11261.32 strOrderDetailID = 11261.30
可以接受的误差 strUserID = 47603 ReturnALlMoneyUnit = 10852.66 strOrderDetailID = 10852.66
可以接受的误差 strUserID = 48506 ReturnALlMoneyUnit = 27131.59 strOrderDetailID = 27131.59
可以接受的误差 strUserID = 48528 ReturnALlMoneyUnit = 55605.22 strOrderDetailID = 55605.16
可以接受的误差 strUserID = 48500 ReturnALlMoneyUnit = 27131.59 strOrderDetailID = 27131.59
可以接受的误差 strUserID = 48456 ReturnALlMoneyUnit = 56716.85 strOrderDetailID = 56716.84
可以接受的误差 strUserID = 48232 ReturnALlMoneyUnit = 27131.59 strOrderDetailID = 27131.59
可以接受的误差 strUserID = 48473 ReturnALlMoneyUnit = 11312.81 strOrderDetailID = 11312.79
可以接受的误差 strUserID = 48511 ReturnALlMoneyUnit = 28786.03 strOrderDetailID = 28786.03
可以接受的误差 strUserID = 48315 ReturnALlMoneyUnit = 5426.30 strOrderDetailID = 5426.30
可以接受的误差 strUserID = 48538 ReturnALlMoneyUnit = 5451.52 strOrderDetailID = 5451.52
可以接受的误差 strUserID = 48716 ReturnALlMoneyUnit = 132105.23 strOrderDetailID = 132105.16
可以接受的误差 strUserID = 48715 ReturnALlMoneyUnit = 54515.39 strOrderDetailID = 54515.39
可以接受的误差 strUserID = 48566 ReturnALlMoneyUnit = 39136.23 strOrderDetailID = 39136.23
可以接受的误差 strUserID = 45200 ReturnALlMoneyUnit = 5451.52 strOrderDetailID = 5451.52
可以接受的误差 strUserID = 48537 ReturnALlMoneyUnit = 109030.81 strOrderDetailID = 109030.81
可以接受的误差 strUserID = 48410 ReturnALlMoneyUnit = 10903.10 strOrderDetailID = 10903.10
可以接受的误差 strUserID = 48530 ReturnALlMoneyUnit = 5451.52 strOrderDetailID = 5451.52
可以接受的误差 strUserID = 48553 ReturnALlMoneyUnit = 10903.10 strOrderDetailID = 10903.10
可以接受的误差 strUserID = 48621 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 48427 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 45962 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 48444 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 48646 ReturnALlMoneyUnit = 10947.55 strOrderDetailID = 10947.55
可以接受的误差 strUserID = 47784 ReturnALlMoneyUnit = 109475.30 strOrderDetailID = 109475.30
可以接受的误差 strUserID = 48144 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 43980 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 48603 ReturnALlMoneyUnit = 39421.17 strOrderDetailID = 39421.17
可以接受的误差 strUserID = 48602 ReturnALlMoneyUnit = 10947.55 strOrderDetailID = 10947.55
可以接受的误差 strUserID = 47202 ReturnALlMoneyUnit = 109740.95 strOrderDetailID = 109740.86
可以接受的误差 strUserID = 48155 ReturnALlMoneyUnit = 16421.27 strOrderDetailID = 16421.27
可以接受的误差 strUserID = 46731 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 46351 ReturnALlMoneyUnit = 10947.55 strOrderDetailID = 10947.55
可以接受的误差 strUserID = 48620 ReturnALlMoneyUnit = 56352.12 strOrderDetailID = 56352.08
可以接受的误差 strUserID = 48710 ReturnALlMoneyUnit = 10947.55 strOrderDetailID = 10947.55
可以接受的误差 strUserID = 48599 ReturnALlMoneyUnit = 10947.55 strOrderDetailID = 10947.55
可以接受的误差 strUserID = 46809 ReturnALlMoneyUnit = 10947.55 strOrderDetailID = 10947.55
可以接受的误差 strUserID = 48578 ReturnALlMoneyUnit = 67894.79 strOrderDetailID = 67894.76
可以接受的误差 strUserID = 48625 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 48639 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 47531 ReturnALlMoneyUnit = 5473.74 strOrderDetailID = 5473.74
可以接受的误差 strUserID = 48642 ReturnALlMoneyUnit = 113908.17 strOrderDetailID = 113908.17
可以接受的误差 strUserID = 48634 ReturnALlMoneyUnit = 16421.27 strOrderDetailID = 16421.27
可以接受的误差 strUserID = 48615 ReturnALlMoneyUnit = 56298.29 strOrderDetailID = 56298.25
可以接受的误差 strUserID = 48690 ReturnALlMoneyUnit = 27501.64 strOrderDetailID = 27501.64
可以接受的误差 strUserID = 48678 ReturnALlMoneyUnit = 55003.30 strOrderDetailID = 55003.30
可以接受的误差 strUserID = 47623 ReturnALlMoneyUnit = 5500.31 strOrderDetailID = 5500.31
可以接受的误差 strUserID = 43830 ReturnALlMoneyUnit = 5500.31 strOrderDetailID = 5500.31
可以接受的误差 strUserID = 46141 ReturnALlMoneyUnit = 11000.68 strOrderDetailID = 11000.68
可以接受的误差 strUserID = 48688 ReturnALlMoneyUnit = 5500.31 strOrderDetailID = 5500.31
可以接受的误差 strUserID = 48721 ReturnALlMoneyUnit = 55003.30 strOrderDetailID = 55003.30
可以接受的误差 strUserID = 48619 ReturnALlMoneyUnit = 55003.30 strOrderDetailID = 55003.30
可以接受的误差 strUserID = 47477 ReturnALlMoneyUnit = 55003.30 strOrderDetailID = 55003.30
可以接受的误差 strUserID = 48691 ReturnALlMoneyUnit = 5500.31 strOrderDetailID = 5500.31
可以接受的误差 strUserID = 48722 ReturnALlMoneyUnit = 16500.97 strOrderDetailID = 16500.97
可以接受的误差 strUserID = 48648 ReturnALlMoneyUnit = 5500.31 strOrderDetailID = 5500.31
可以接受的误差 strUserID = 48680 ReturnALlMoneyUnit = 11000.68 strOrderDetailID = 11000.68
可以接受的误差 strUserID = 46891 ReturnALlMoneyUnit = 5500.31 strOrderDetailID = 5500.31
可以接受的误差 strUserID = 48593 ReturnALlMoneyUnit = 5525.37 strOrderDetailID = 5525.37
可以接受的误差 strUserID = 48476 ReturnALlMoneyUnit = 11050.80 strOrderDetailID = 11050.80
可以接受的误差 strUserID = 48484 ReturnALlMoneyUnit = 16576.16 strOrderDetailID = 16576.16
可以接受的误差 strUserID = 48841 ReturnALlMoneyUnit = 27626.95 strOrderDetailID = 27626.95
可以接受的误差 strUserID = 48782 ReturnALlMoneyUnit = 16828.33 strOrderDetailID = 16828.33
可以接受的误差 strUserID = 48775 ReturnALlMoneyUnit = 5525.37 strOrderDetailID = 5525.37
可以接受的误差 strUserID = 48784 ReturnALlMoneyUnit = 5525.37 strOrderDetailID = 5525.37
可以接受的误差 strUserID = 48459 ReturnALlMoneyUnit = 169932.70 strOrderDetailID = 169932.64
可以接受的误差 strUserID = 48763 ReturnALlMoneyUnit = 22609.57 strOrderDetailID = 22609.55
可以接受的误差 strUserID = 48647 ReturnALlMoneyUnit = 57212.21 strOrderDetailID = 57212.20
可以接受的误差 strUserID = 48757 ReturnALlMoneyUnit = 86797.48 strOrderDetailID = 86797.48
可以接受的误差 strUserID = 48772 ReturnALlMoneyUnit = 11050.80 strOrderDetailID = 11050.80
可以接受的误差 strUserID = 48761 ReturnALlMoneyUnit = 5525.37 strOrderDetailID = 5525.37
可以接受的误差 strUserID = 48774 ReturnALlMoneyUnit = 16880.41 strOrderDetailID = 16880.33
可以接受的误差 strUserID = 48534 ReturnALlMoneyUnit = 5550.80 strOrderDetailID = 5550.80
可以接受的误差 strUserID = 48708 ReturnALlMoneyUnit = 16652.46 strOrderDetailID = 16652.46
可以接受的误差 strUserID = 48800 ReturnALlMoneyUnit = 5550.80 strOrderDetailID = 5550.80
可以接受的误差 strUserID = 47106 ReturnALlMoneyUnit = 5550.80 strOrderDetailID = 5550.80
可以接受的误差 strUserID = 48843 ReturnALlMoneyUnit = 5550.80 strOrderDetailID = 5550.80
可以接受的误差 strUserID = 48867 ReturnALlMoneyUnit = 11144.21 strOrderDetailID = 11144.21
可以接受的误差 strUserID = 48877 ReturnALlMoneyUnit = 27860.45 strOrderDetailID = 27860.45
可以接受的误差 strUserID = 48883 ReturnALlMoneyUnit = 5572.07 strOrderDetailID = 5572.07
可以接受的误差 strUserID = 45107 ReturnALlMoneyUnit = 5572.07 strOrderDetailID = 5572.07
可以接受的误差 strUserID = 48842 ReturnALlMoneyUnit = 16716.27 strOrderDetailID = 16716.27
可以接受的误差 strUserID = 48869 ReturnALlMoneyUnit = 5572.07 strOrderDetailID = 5572.07
可以接受的误差 strUserID = 47911 ReturnALlMoneyUnit = 5572.07 strOrderDetailID = 5572.07
可以接受的误差 strUserID = 48882 ReturnALlMoneyUnit = 11144.21 strOrderDetailID = 11144.21
可以接受的误差 strUserID = 48868 ReturnALlMoneyUnit = 62519.33 strOrderDetailID = 62519.33
可以接受的误差 strUserID = 47102 ReturnALlMoneyUnit = 5572.07 strOrderDetailID = 5572.07
可以接受的误差 strUserID = 48859 ReturnALlMoneyUnit = 5572.07 strOrderDetailID = 5572.07
可以接受的误差 strUserID = 48912 ReturnALlMoneyUnit = 5596.35 strOrderDetailID = 5596.35
可以接受的误差 strUserID = 48901 ReturnALlMoneyUnit = 5596.35 strOrderDetailID = 5596.35
可以接受的误差 strUserID = 48888 ReturnALlMoneyUnit = 27981.83 strOrderDetailID = 27981.83
可以接受的误差 strUserID = 48885 ReturnALlMoneyUnit = 27981.83 strOrderDetailID = 27981.83
可以接受的误差 strUserID = 48934 ReturnALlMoneyUnit = 5596.35 strOrderDetailID = 5596.35
可以接受的误差 strUserID = 48909 ReturnALlMoneyUnit = 40415.20 strOrderDetailID = 40415.20
可以接受的误差 strUserID = 48920 ReturnALlMoneyUnit = 5596.35 strOrderDetailID = 5596.35
可以接受的误差 strUserID = 48906 ReturnALlMoneyUnit = 5596.35 strOrderDetailID = 5596.35
可以接受的误差 strUserID = 46505 ReturnALlMoneyUnit = 5621.56 strOrderDetailID = 5621.56
可以接受的误差 strUserID = 48983 ReturnALlMoneyUnit = 5621.56 strOrderDetailID = 5621.56
可以接受的误差 strUserID = 48998 ReturnALlMoneyUnit = 11243.18 strOrderDetailID = 11243.18
可以接受的误差 strUserID = 48986 ReturnALlMoneyUnit = 5621.56 strOrderDetailID = 5621.56
可以接受的误差 strUserID = 48985 ReturnALlMoneyUnit = 5621.56 strOrderDetailID = 5621.56
可以接受的误差 strUserID = 48932 ReturnALlMoneyUnit = 5621.56 strOrderDetailID = 5621.56
可以接受的误差 strUserID = 47689 ReturnALlMoneyUnit = 28107.89 strOrderDetailID = 28107.89
可以接受的误差 strUserID = 48915 ReturnALlMoneyUnit = 5621.56 strOrderDetailID = 5621.56
可以接受的误差 strUserID = 48955 ReturnALlMoneyUnit = 5621.56 strOrderDetailID = 5621.56
可以接受的误差 strUserID = 48954 ReturnALlMoneyUnit = 56215.81 strOrderDetailID = 56215.81
可以接受的误差 strUserID = 48967 ReturnALlMoneyUnit = 11243.18 strOrderDetailID = 11243.18
可以接受的误差 strUserID = 49069 ReturnALlMoneyUnit = 86051.54 strOrderDetailID = 86051.54
可以接受的误差 strUserID = 49032 ReturnALlMoneyUnit = 5646.61 strOrderDetailID = 5646.61
可以接受的误差 strUserID = 49034 ReturnALlMoneyUnit = 5646.61 strOrderDetailID = 5646.61
可以接受的误差 strUserID = 49026 ReturnALlMoneyUnit = 11293.27 strOrderDetailID = 11293.27
可以接受的误差 strUserID = 49113 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 47110 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49159 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49158 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49157 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49165 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49166 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49087 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49007 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49152 ReturnALlMoneyUnit = 28348.76 strOrderDetailID = 28348.76
可以接受的误差 strUserID = 43975 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49107 ReturnALlMoneyUnit = 46156.41 strOrderDetailID = 46156.38
可以接受的误差 strUserID = 47722 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 46289 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 44908 ReturnALlMoneyUnit = 5669.74 strOrderDetailID = 5669.74
可以接受的误差 strUserID = 49229 ReturnALlMoneyUnit = 56947.24 strOrderDetailID = 56947.24
可以接受的误差 strUserID = 48803 ReturnALlMoneyUnit = 5694.71 strOrderDetailID = 5694.71
可以接受的误差 strUserID = 45449 ReturnALlMoneyUnit = 5694.71 strOrderDetailID = 5694.71
可以接受的误差 strUserID = 48981 ReturnALlMoneyUnit = 5694.71 strOrderDetailID = 5694.71
可以接受的误差 strUserID = 49219 ReturnALlMoneyUnit = 11389.46 strOrderDetailID = 11389.46
可以接受的误差 strUserID = 49217 ReturnALlMoneyUnit = 23128.17 strOrderDetailID = 23128.14
可以接受的误差 strUserID = 49051 ReturnALlMoneyUnit = 5694.71 strOrderDetailID = 5694.71
可以接受的误差 strUserID = 49188 ReturnALlMoneyUnit = 28473.61 strOrderDetailID = 28473.61
可以接受的误差 strUserID = 48966 ReturnALlMoneyUnit = 52683.24 strOrderDetailID = 52683.18
可以接受的误差 strUserID = 49196 ReturnALlMoneyUnit = 28473.61 strOrderDetailID = 28473.61
可以接受的误差 strUserID = 49286 ReturnALlMoneyUnit = 5715.97 strOrderDetailID = 5715.97
可以接受的误差 strUserID = 49285 ReturnALlMoneyUnit = 5715.97 strOrderDetailID = 5715.97
可以接受的误差 strUserID = 46341 ReturnALlMoneyUnit = 11431.99 strOrderDetailID = 11431.99
可以接受的误差 strUserID = 48773 ReturnALlMoneyUnit = 5715.97 strOrderDetailID = 5715.97
可以接受的误差 strUserID = 49224 ReturnALlMoneyUnit = 57159.87 strOrderDetailID = 57159.87
可以接受的误差 strUserID = 49284 ReturnALlMoneyUnit = 64886.51 strOrderDetailID = 64886.51
可以接受的误差 strUserID = 49214 ReturnALlMoneyUnit = 23102.02 strOrderDetailID = 23101.98
可以接受的误差 strUserID = 47857 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 46469 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 49306 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49303 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 49184 ReturnALlMoneyUnit = 28666.07 strOrderDetailID = 28666.07
可以接受的误差 strUserID = 49459 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49010 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 47057 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49327 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 49458 ReturnALlMoneyUnit = 17199.64 strOrderDetailID = 17199.64
可以接受的误差 strUserID = 49414 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49386 ReturnALlMoneyUnit = 115107.48 strOrderDetailID = 115107.46
可以接受的误差 strUserID = 48489 ReturnALlMoneyUnit = 57332.14 strOrderDetailID = 57332.14
可以接受的误差 strUserID = 48958 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49398 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49397 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49346 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49204 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 49405 ReturnALlMoneyUnit = 289606.98 strOrderDetailID = 289606.99
可以接受的误差 strUserID = 49404 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49408 ReturnALlMoneyUnit = 233266.52 strOrderDetailID = 233266.50
可以接受的误差 strUserID = 49292 ReturnALlMoneyUnit = 28666.07 strOrderDetailID = 28666.07
可以接受的误差 strUserID = 48940 ReturnALlMoneyUnit = 28666.07 strOrderDetailID = 28666.07
可以接受的误差 strUserID = 46228 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 49287 ReturnALlMoneyUnit = 28666.07 strOrderDetailID = 28666.07
可以接受的误差 strUserID = 49198 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 49320 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 49319 ReturnALlMoneyUnit = 11466.44 strOrderDetailID = 11466.44
可以接受的误差 strUserID = 49041 ReturnALlMoneyUnit = 57332.14 strOrderDetailID = 57332.14
可以接受的误差 strUserID = 48508 ReturnALlMoneyUnit = 28666.07 strOrderDetailID = 28666.07
可以接受的误差 strUserID = 45105 ReturnALlMoneyUnit = 17199.64 strOrderDetailID = 17199.64
可以接受的误差 strUserID = 49038 ReturnALlMoneyUnit = 57332.14 strOrderDetailID = 57332.14
可以接受的误差 strUserID = 49277 ReturnALlMoneyUnit = 57332.14 strOrderDetailID = 57332.14
可以接受的误差 strUserID = 48635 ReturnALlMoneyUnit = 57332.14 strOrderDetailID = 57332.14
可以接受的误差 strUserID = 49249 ReturnALlMoneyUnit = 87836.60 strOrderDetailID = 87836.61
可以接受的误差 strUserID = 47980 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49331 ReturnALlMoneyUnit = 5733.20 strOrderDetailID = 5733.20
可以接受的误差 strUserID = 49433 ReturnALlMoneyUnit = 5759.32 strOrderDetailID = 5759.32
可以接受的误差 strUserID = 49429 ReturnALlMoneyUnit = 5759.32 strOrderDetailID = 5759.32
可以接受的误差 strUserID = 49431 ReturnALlMoneyUnit = 5759.32 strOrderDetailID = 5759.32
可以接受的误差 strUserID = 49565 ReturnALlMoneyUnit = 115368.64 strOrderDetailID = 115368.62
可以接受的误差 strUserID = 49566 ReturnALlMoneyUnit = 115368.64 strOrderDetailID = 115368.62
可以接受的误差 strUserID = 49419 ReturnALlMoneyUnit = 28796.65 strOrderDetailID = 28796.65
可以接受的误差 strUserID = 49412 ReturnALlMoneyUnit = 28796.65 strOrderDetailID = 28796.65
可以接受的误差 strUserID = 49411 ReturnALlMoneyUnit = 57593.30 strOrderDetailID = 57593.30
可以接受的误差 strUserID = 47089 ReturnALlMoneyUnit = 5759.32 strOrderDetailID = 5759.32
可以接受的误差 strUserID = 49046 ReturnALlMoneyUnit = 46863.25 strOrderDetailID = 46863.25
可以接受的误差 strUserID = 47891 ReturnALlMoneyUnit = 5759.32 strOrderDetailID = 5759.32
可以接受的误差 strUserID = 49432 ReturnALlMoneyUnit = 69111.98 strOrderDetailID = 69111.98
可以接受的误差 strUserID = 49401 ReturnALlMoneyUnit = 29130.31 strOrderDetailID = 29130.31
可以接受的误差 strUserID = 49394 ReturnALlMoneyUnit = 11518.67 strOrderDetailID = 11518.67
可以接受的误差 strUserID = 49392 ReturnALlMoneyUnit = 11518.67 strOrderDetailID = 11518.67
可以接受的误差 strUserID = 49473 ReturnALlMoneyUnit = 5777.52 strOrderDetailID = 5777.52
可以接受的误差 strUserID = 49483 ReturnALlMoneyUnit = 11555.07 strOrderDetailID = 11555.07
可以接受的误差 strUserID = 48976 ReturnALlMoneyUnit = 5777.52 strOrderDetailID = 5777.52
可以接受的误差 strUserID = 49474 ReturnALlMoneyUnit = 5777.52 strOrderDetailID = 5777.52
可以接受的误差 strUserID = 42487 ReturnALlMoneyUnit = 5777.52 strOrderDetailID = 5777.52
可以接受的误差 strUserID = 49472 ReturnALlMoneyUnit = 57870.97 strOrderDetailID = 57870.93
可以接受的误差 strUserID = 49536 ReturnALlMoneyUnit = 58628.63 strOrderDetailID = 58628.63
可以接受的误差 strUserID = 49421 ReturnALlMoneyUnit = 5796.65 strOrderDetailID = 5796.65
可以接受的误差 strUserID = 49571 ReturnALlMoneyUnit = 11593.33 strOrderDetailID = 11593.33
可以接受的误差 strUserID = 49535 ReturnALlMoneyUnit = 5796.65 strOrderDetailID = 5796.65
可以接受的误差 strUserID = 49550 ReturnALlMoneyUnit = 5796.65 strOrderDetailID = 5796.65
可以接受的误差 strUserID = 49036 ReturnALlMoneyUnit = 5796.65 strOrderDetailID = 5796.65
可以接受的误差 strUserID = 49555 ReturnALlMoneyUnit = 11593.33 strOrderDetailID = 11593.33
可以接受的误差 strUserID = 49629 ReturnALlMoneyUnit = 58166.79 strOrderDetailID = 58166.79
可以接受的误差 strUserID = 49612 ReturnALlMoneyUnit = 11633.36 strOrderDetailID = 11633.36
可以接受的误差 strUserID = 49614 ReturnALlMoneyUnit = 58515.88 strOrderDetailID = 58515.85
可以接受的误差 strUserID = 49606 ReturnALlMoneyUnit = 5816.67 strOrderDetailID = 5816.67
可以接受的误差 strUserID = 49599 ReturnALlMoneyUnit = 5816.67 strOrderDetailID = 5816.67
可以接受的误差 strUserID = 49029 ReturnALlMoneyUnit = 5816.67 strOrderDetailID = 5816.67
可以接受的误差 strUserID = 49692 ReturnALlMoneyUnit = 5816.67 strOrderDetailID = 5816.67
可以接受的误差 strUserID = 49647 ReturnALlMoneyUnit = 5816.67 strOrderDetailID = 5816.67
可以接受的误差 strUserID = 49545 ReturnALlMoneyUnit = 5816.67 strOrderDetailID = 5816.67
可以接受的误差 strUserID = 49546 ReturnALlMoneyUnit = 5816.67 strOrderDetailID = 5816.67
可以接受的误差 strUserID = 49687 ReturnALlMoneyUnit = 17505.03 strOrderDetailID = 17505.03
可以接受的误差 strUserID = 49730 ReturnALlMoneyUnit = 17505.03 strOrderDetailID = 17505.03
可以接受的误差 strUserID = 49734 ReturnALlMoneyUnit = 11670.02 strOrderDetailID = 11670.02
可以接受的误差 strUserID = 49743 ReturnALlMoneyUnit = 5835.00 strOrderDetailID = 5835.00
可以接受的误差 strUserID = 49722 ReturnALlMoneyUnit = 5835.00 strOrderDetailID = 5835.00
可以接受的误差 strUserID = 49753 ReturnALlMoneyUnit = 5835.00 strOrderDetailID = 5835.00
可以接受的误差 strUserID = 48630 ReturnALlMoneyUnit = 5835.00 strOrderDetailID = 5835.00
可以接受的误差 strUserID = 49728 ReturnALlMoneyUnit = 5835.00 strOrderDetailID = 5835.00
可以接受的误差 strUserID = 49720 ReturnALlMoneyUnit = 5835.00 strOrderDetailID = 5835.00
可以接受的误差 strUserID = 49427 ReturnALlMoneyUnit = 5852.19 strOrderDetailID = 5852.19
可以接受的误差 strUserID = 49805 ReturnALlMoneyUnit = 29261.00 strOrderDetailID = 29261.00
可以接受的误差 strUserID = 49744 ReturnALlMoneyUnit = 29261.00 strOrderDetailID = 29261.00
可以接受的误差 strUserID = 49785 ReturnALlMoneyUnit = 29261.00 strOrderDetailID = 29261.00
可以接受的误差 strUserID = 49810 ReturnALlMoneyUnit = 65022.73 strOrderDetailID = 65022.73
可以接受的误差 strUserID = 46229 ReturnALlMoneyUnit = 5869.34 strOrderDetailID = 5869.34
可以接受的误差 strUserID = 46483 ReturnALlMoneyUnit = 11738.70 strOrderDetailID = 11738.70
可以接受的误差 strUserID = 49801 ReturnALlMoneyUnit = 29346.75 strOrderDetailID = 29346.75
可以接受的误差 strUserID = 49855 ReturnALlMoneyUnit = 29346.75 strOrderDetailID = 29346.75
可以接受的误差 strUserID = 49825 ReturnALlMoneyUnit = 58693.49 strOrderDetailID = 58693.49
可以接受的误差 strUserID = 49872 ReturnALlMoneyUnit = 29346.75 strOrderDetailID = 29346.75
可以接受的误差 strUserID = 48377 ReturnALlMoneyUnit = 5869.34 strOrderDetailID = 5869.34
可以接受的误差 strUserID = 49841 ReturnALlMoneyUnit = 11738.70 strOrderDetailID = 11738.70
可以接受的误差 strUserID = 49074 ReturnALlMoneyUnit = 5869.34 strOrderDetailID = 5869.34
可以接受的误差 strUserID = 49824 ReturnALlMoneyUnit = 117930.22 strOrderDetailID = 117930.21
可以接受的误差 strUserID = 49904 ReturnALlMoneyUnit = 5886.49 strOrderDetailID = 5886.49
可以接受的误差 strUserID = 49907 ReturnALlMoneyUnit = 5886.49 strOrderDetailID = 5886.49
可以接受的误差 strUserID = 49908 ReturnALlMoneyUnit = 58864.97 strOrderDetailID = 58864.97
可以接受的误差 strUserID = 49893 ReturnALlMoneyUnit = 5886.49 strOrderDetailID = 5886.49
可以接受的误差 strUserID = 49973 ReturnALlMoneyUnit = 5906.52 strOrderDetailID = 5906.52
可以接受的误差 strUserID = 49965 ReturnALlMoneyUnit = 5906.52 strOrderDetailID = 5906.52
可以接受的误差 strUserID = 47197 ReturnALlMoneyUnit = 11813.05 strOrderDetailID = 11813.05
可以接受的误差 strUserID = 49969 ReturnALlMoneyUnit = 5906.52 strOrderDetailID = 5906.52
可以接受的误差 strUserID = 43035 ReturnALlMoneyUnit = 17719.57 strOrderDetailID = 17719.57
可以接受的误差 strUserID = 49940 ReturnALlMoneyUnit = 5906.52 strOrderDetailID = 5906.52
可以接受的误差 strUserID = 49946 ReturnALlMoneyUnit = 5906.52 strOrderDetailID = 5906.52
可以接受的误差 strUserID = 49944 ReturnALlMoneyUnit = 5906.52 strOrderDetailID = 5906.52
可以接受的误差 strUserID = 41294 ReturnALlMoneyUnit = 5906.52 strOrderDetailID = 5906.52
可以接受的误差 strUserID = 49077 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 50300 ReturnALlMoneyUnit = 29585.27 strOrderDetailID = 29585.27
可以接受的误差 strUserID = 50072 ReturnALlMoneyUnit = 11834.11 strOrderDetailID = 11834.11
可以接受的误差 strUserID = 50078 ReturnALlMoneyUnit = 11834.11 strOrderDetailID = 11834.11
可以接受的误差 strUserID = 49857 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 49174 ReturnALlMoneyUnit = 29585.27 strOrderDetailID = 29585.27
可以接受的误差 strUserID = 50285 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 50316 ReturnALlMoneyUnit = 29585.27 strOrderDetailID = 29585.27
可以接受的误差 strUserID = 50308 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 47818 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 49979 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 49874 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 49799 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 47686 ReturnALlMoneyUnit = 11834.11 strOrderDetailID = 11834.11
可以接受的误差 strUserID = 49348 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 50048 ReturnALlMoneyUnit = 237724.94 strOrderDetailID = 237724.95
可以接受的误差 strUserID = 50071 ReturnALlMoneyUnit = 237724.94 strOrderDetailID = 237724.95
可以接受的误差 strUserID = 49718 ReturnALlMoneyUnit = 237724.94 strOrderDetailID = 237724.95
可以接受的误差 strUserID = 49865 ReturnALlMoneyUnit = 29585.27 strOrderDetailID = 29585.27
可以接受的误差 strUserID = 50140 ReturnALlMoneyUnit = 59170.53 strOrderDetailID = 59170.53
可以接受的误差 strUserID = 50027 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 50012 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 49854 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 50245 ReturnALlMoneyUnit = 17751.16 strOrderDetailID = 17751.16
可以接受的误差 strUserID = 48740 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 50075 ReturnALlMoneyUnit = 29585.27 strOrderDetailID = 29585.27
可以接受的误差 strUserID = 50029 ReturnALlMoneyUnit = 17751.16 strOrderDetailID = 17751.16
可以接受的误差 strUserID = 49520 ReturnALlMoneyUnit = 59170.53 strOrderDetailID = 59170.53
可以接受的误差 strUserID = 50055 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 50053 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 49839 ReturnALlMoneyUnit = 59170.53 strOrderDetailID = 59170.53
可以接受的误差 strUserID = 46273 ReturnALlMoneyUnit = 35502.31 strOrderDetailID = 35502.31
可以接受的误差 strUserID = 50058 ReturnALlMoneyUnit = 118341.06 strOrderDetailID = 118341.06
可以接受的误差 strUserID = 49678 ReturnALlMoneyUnit = 29585.27 strOrderDetailID = 29585.27
可以接受的误差 strUserID = 43681 ReturnALlMoneyUnit = 59170.53 strOrderDetailID = 59170.53
可以接受的误差 strUserID = 49994 ReturnALlMoneyUnit = 29585.27 strOrderDetailID = 29585.27
可以接受的误差 strUserID = 43858 ReturnALlMoneyUnit = 88755.80 strOrderDetailID = 88755.80
可以接受的误差 strUserID = 50025 ReturnALlMoneyUnit = 59170.53 strOrderDetailID = 59170.53
可以接受的误差 strUserID = 49981 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 49997 ReturnALlMoneyUnit = 5917.05 strOrderDetailID = 5917.05
可以接受的误差 strUserID = 49977 ReturnALlMoneyUnit = 59170.53 strOrderDetailID = 59170.52
可以接受的误差 strUserID = 50062 ReturnALlMoneyUnit = 5945.36 strOrderDetailID = 5945.36
可以接受的误差 strUserID = 50060 ReturnALlMoneyUnit = 5945.36 strOrderDetailID = 5945.36
可以接受的误差 strUserID = 50100 ReturnALlMoneyUnit = 5945.36 strOrderDetailID = 5945.36
可以接受的误差 strUserID = 49934 ReturnALlMoneyUnit = 5945.36 strOrderDetailID = 5945.36
可以接受的误差 strUserID = 50166 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50162 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50041 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 48074 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50136 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50129 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50137 ReturnALlMoneyUnit = 11921.20 strOrderDetailID = 11921.20
可以接受的误差 strUserID = 50128 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50111 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50130 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50163 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50125 ReturnALlMoneyUnit = 59605.99 strOrderDetailID = 59605.98
可以接受的误差 strUserID = 49161 ReturnALlMoneyUnit = 5960.60 strOrderDetailID = 5960.60
可以接受的误差 strUserID = 50173 ReturnALlMoneyUnit = 59777.90 strOrderDetailID = 59777.90
可以接受的误差 strUserID = 50228 ReturnALlMoneyUnit = 5977.79 strOrderDetailID = 5977.79
可以接受的误差 strUserID = 48200 ReturnALlMoneyUnit = 5977.79 strOrderDetailID = 5977.79
可以接受的误差 strUserID = 48189 ReturnALlMoneyUnit = 5977.79 strOrderDetailID = 5977.79
可以接受的误差 strUserID = 50215 ReturnALlMoneyUnit = 29888.95 strOrderDetailID = 29888.95
可以接受的误差 strUserID = 44102 ReturnALlMoneyUnit = 5977.79 strOrderDetailID = 5977.79
可以接受的误差 strUserID = 50253 ReturnALlMoneyUnit = 5977.79 strOrderDetailID = 5977.79
可以接受的误差 strUserID = 50211 ReturnALlMoneyUnit = 5977.79 strOrderDetailID = 5977.79
可以接受的误差 strUserID = 49714 ReturnALlMoneyUnit = 5977.79 strOrderDetailID = 5977.792017 / 9 / 30 20:20:24do over*/

            #endregion
        }

        /// <summary>
        /// 写出所有 进账的 详单ID到 b006_TotalWealth_OperationUser 表
        /// </summary>
        private void _009ResetTo008_step3()
        {
            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLLb006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();


            string strSQL = "select * from b006_TotalWealth_OperationUser where orderDetailID is null and Bool_ConsumeOrRecharge=1 order by id asc";
            System.Data.DataTable Data_DataTable = BLLb006_TotalWealth_OperationUser.SelectList(strSQL).Tables[0];
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {

                ///财富积分3.00倍 共1998.00运营中心收入(订单11055)量子小分子团养生酒¥19.
                string strConsumeTypeOrRechare = Data_DataTable.Rows[i]["ConsumeTypeOrRecharge"].toString();
                string strUserID = Data_DataTable.Rows[i]["UserID"].toString();
                string strID = Data_DataTable.Rows[i]["ID"].toString();

                string orderID = "1111";
                int intDetail = BLLtab_Orderdetails.GetModel("orderID=" + orderID).ID;

                BLLb006_TotalWealth_OperationUser.Update("orderDetailID=" + intDetail + "", "ID=" + strID);


            }
        }


        /// <summary>
        /// 只有2条记录 应该是一次性购买的 。所以可以直接合并。返现 也是只有一天记录的，所以想合并这条数据 然后再想办法找思路
        /// </summary>
        private void _009ResetTo008_step2()
        {
            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLLb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLLb006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();

            string strSQLOnlyOneRecord = "SELECT UserID, count(UserID) as countUserID, max(id) FROM[b008_OpterationUserActiveReturnMoneyOrderNum] where ShopClient_ID = 21  group by UserID order by count(UserID) asc, max(id) asc";


            System.Data.DataTable Data_DataTable = BLLb008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strSQLOnlyOneRecord).Tables[0];
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                string strUserID = Data_DataTable.Rows[i]["UserID"].toString();
                string strcountUserID = Data_DataTable.Rows[i]["countUserID"].toString();
                if (strcountUserID.toInt32() == 2)
                {
                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model1 = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();
                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model2 = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();

                    Model1 = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetModel("userid=@userid and orderid is null", strUserID.toInt32());
                    Model2 = BLLb008_OpterationUserActiveReturnMoneyOrderNum.GetModel("userid=@userid and orderid  is not null", strUserID.toInt32());
                    if ((Model1 != null) && (Model2 != null))
                    {
                        Model1.OrderID = Model2.OrderID;
                        Model1.OrderDetailID = Model2.OrderDetailID;
                        Model1.OrderCount = Model2.OrderCount;
                        Model1.PayDateTime = Model2.PayDateTime;
                        Model1.UpdateTime = DateTime.Now;
                        BLLb008_OpterationUserActiveReturnMoneyOrderNum.Update(Model1);
                        BLLb008_OpterationUserActiveReturnMoneyOrderNum.Delete(Model2.ID);



                        BLLb006_TotalWealth_OperationUser.Update("OrderDetailID=@OrderDetailID,UpdateTime=getdate()", "UserID=@UserID and ShopClient_ID=@ShopClient_ID", Model2.OrderDetailID, strUserID.toInt32(), 21);
                        Response.Write("<br />ok userid=" + strUserID);
                    }
                    else if ((Model1 == null) && (Model2 != null))
                    {

                        BLLb008_OpterationUserActiveReturnMoneyOrderNum.Delete("userid=@userid", strUserID.toInt32());
                        Response.Write("<br />error delete userid=" + strUserID);
                    }
                    else
                    {
                        Response.Write("<br />error userid=" + strUserID);
                        int dddd = 0;
                    }
                }

            }

            #region 留存页面备查记录
            /*

      
             */
            #endregion
        }

        /// <summary>
        /// 订单表  补充到008 中去  改为每个订单的 详细至 。分红是按照订单分的 不是按照人头分的
        /// </summary>
        private void _009ResetTo008_step1()
        {
            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLLb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Modelb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();

            EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.BLL.tab_Order BLLtab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order Modeltab_Order = new EggsoftWX.Model.tab_Order();

            System.Data.DataTable Data_DataTable = BLLtab_Orderdetails.GetList(" shopclient_id=21  and goodtype=6 and goodtypeidbuyinfo='1' and over7daystobeans=1 and isdeleted=0 order by id asc").Tables[0];
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                int orderid = Data_DataTable.Rows[i]["orderid"].toInt32();
                int orderdetailid = Data_DataTable.Rows[i]["ID"].toInt32();
                int orderCount = Data_DataTable.Rows[i]["OrderCount"].toInt32();

                Modeltab_Order = BLLtab_Order.GetModel(orderid);
                if (Modeltab_Order.IsDeleted == true) continue;
                if (Modeltab_Order.PayStatus != 1) continue;
                if (Modeltab_Order.PayDateTime == null) continue;

                //DateTime dt1 = Convert.ToDateTime(Modeltab_Order.PayDateTime);
                //DateTime dt2 = DateTime.Now;
                //dt2 = new DateTime(2017, 9, 11, 23, 15, 20);////模拟该时刻的数据  发布到生产时  取当前 时刻。。要注意多次测试 来 检查数据是否正确
                //TimeSpan ts = dt2.Subtract(dt1);
                //if(ts.Days < 7) continue;

                Modelb008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.UserID = Modeltab_Order.UserID;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.OrderID = orderid;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.OrderDetailID = orderdetailid;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.OrderCount = orderCount;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.PayDateTime = Modeltab_Order.PayDateTime;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.ShopClient_ID = 21;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.InputShouldReturnCount = orderCount;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = orderCount;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.b004_OperationGoodsID = 1;
                Modelb008_OpterationUserActiveReturnMoneyOrderNum.CreateBy = "订单表  补充到008 中去  改为每个订单的 详细至";

                BLLb008_OpterationUserActiveReturnMoneyOrderNum.Add(Modelb008_OpterationUserActiveReturnMoneyOrderNum);
            }
        }


        /// <summary>
        /// 复制数据 到 每个 跟踪表  这里只能研究使用
        /// </summary>
        private void copy41199Data()
        {
            EggsoftWX.BLL.b014_OrderDetailEveryReturnWealth BLLb014_OrderDetailEveryReturnWealth = new EggsoftWX.BLL.b014_OrderDetailEveryReturnWealth();
            EggsoftWX.BLL.b006_TotalWealth_OperationUser b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();

            EggsoftWX.Model.b006_TotalWealth_OperationUser Modelb006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();

            //Modelb006_TotalWealth_OperationUser.ConsumeOrRechargeWealth
            System.Data.DataTable Data_DataTable = b006_TotalWealth_OperationUser.GetList(" Bool_ConsumeOrRecharge=0 and  UserID=41199 order by id desc").Tables[0];
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                decimal decimalWilladd = Data_DataTable.Rows[i]["ConsumeOrRechargeWealth"].toDecimal();
                DateTime? DateTimeWilladd = Data_DataTable.Rows[i]["CreatTime"].toDateTime();

                EggsoftWX.Model.b014_OrderDetailEveryReturnWealth Modelb014_OrderDetailEveryReturnWealth = new EggsoftWX.Model.b014_OrderDetailEveryReturnWealth();
                Modelb014_OrderDetailEveryReturnWealth.UserID = 41199;
                Modelb014_OrderDetailEveryReturnWealth.ShopClientID = 21;
                Modelb014_OrderDetailEveryReturnWealth.OrdetailID = 9841;
                Modelb014_OrderDetailEveryReturnWealth.OrderID = 8351;
                Modelb014_OrderDetailEveryReturnWealth.GoodID = 1779;
                Modelb014_OrderDetailEveryReturnWealth.ReturnMoney = decimalWilladd;
                Modelb014_OrderDetailEveryReturnWealth.CreatTime = DateTimeWilladd;
                Modelb014_OrderDetailEveryReturnWealth.CreateBy = "数据观察";
                Modelb014_OrderDetailEveryReturnWealth.OrderCount = 2;
                BLLb014_OrderDetailEveryReturnWealth.Add(Modelb014_OrderDetailEveryReturnWealth);
            }


        }
        private void checkEveryDayMoney()
        {
            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLLb006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();

            EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay BLLb007_OperationReturnMoneyEveryDay = new EggsoftWX.BLL.b007_OperationReturnMoneyEveryDay();
            EggsoftWX.Model.b007_OperationReturnMoneyEveryDay Model_b007_OperationReturnMoneyEveryDay = new EggsoftWX.Model.b007_OperationReturnMoneyEveryDay();



            System.Data.DataTable Data_DataTable = BLLb006_TotalWealth_OperationUser.GetList("userID=41192 and Bool_ConsumeOrRecharge=0 and ConsumeTypeOrRecharge like '每天分红财富，减增进入现金%' order by id asc").Tables[0];
            int intCountRow = Data_DataTable.Rows.Count;
            for (int i = 0; i < intCountRow; i++)
            {
                Decimal? ConsumeOrRechargeWealth = Data_DataTable.Rows[i]["ConsumeOrRechargeWealth"].toDecimal();
                string strDBConsumeTypeOrRecharge = Data_DataTable.Rows[i]["ConsumeTypeOrRecharge"].toString();
                DateTime? dateCreatTime = Data_DataTable.Rows[i]["CreatTime"].toDateTime();



                /// 每天分红财富，减增进入现金(23031.5400 - 100 - 491)
                string strConsumeTypeOrRecharge = strDBConsumeTypeOrRecharge.Replace("每天分红财富，减增进入现金(", "");
                strConsumeTypeOrRecharge = strConsumeTypeOrRecharge.Replace(")", "");

                if (i < 82)
                {

                    #region 82
                    string[] strlist = strConsumeTypeOrRecharge.Split('-');
                    if (strlist.Length == 3)
                    {

                        DateTime DateTimemy = Convert.ToDateTime(dateCreatTime);
                        string strShowDay = DateTimemy.Year.toString() + "-" + Eggsoft.Common.StringNum.Add000000Num(DateTimemy.Month, 2).toString() + "-" + Eggsoft.Common.StringNum.Add000000Num(DateTimemy.Day, 2).toString();
                        Model_b007_OperationReturnMoneyEveryDay = BLLb007_OperationReturnMoneyEveryDay.GetModel("ShopClient_ID=21 and ThisDay=@ThisDay", strShowDay);
                        if (Model_b007_OperationReturnMoneyEveryDay != null)
                        {
                            if (Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyByBoss != strlist[0].toDecimal())
                            {
                                int a = 0;
                            }
                            else
                            {
                                if (Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder != strlist[2].toInt32())
                                {
                                    Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder = strlist[2].toInt32();
                                    Model_b007_OperationReturnMoneyEveryDay.EveryOrderGet = ConsumeOrRechargeWealth / (strlist[1].toInt32() / 100);
                                    Model_b007_OperationReturnMoneyEveryDay.UpdateTime = DateTime.Now;
                                    Model_b007_OperationReturnMoneyEveryDay.UpdateBy = Model_b007_OperationReturnMoneyEveryDay.UpdateBy.toString() + " 追加订单统计";
                                    BLLb007_OperationReturnMoneyEveryDay.Update(Model_b007_OperationReturnMoneyEveryDay);
                                }
                                else
                                {
                                    int dddddd = 0;
                                }
                            }
                        }
                        else
                        {
                            int b = 0;
                        }
                    }
                    else
                    {
                        int ooooobc = 0;
                    }
                    #endregion
                }

                else
                {
                    ///continue;


                    if (i == 109)
                    {
                        int dddddd = 0;
                    }
                    strConsumeTypeOrRecharge = strConsumeTypeOrRecharge.Replace(".", "");
                    string strAllOrder = strConsumeTypeOrRecharge.Substring(strConsumeTypeOrRecharge.Length - 4, 4);
                    string strMeOrder = strConsumeTypeOrRecharge.Substring(strConsumeTypeOrRecharge.Length - 7, 3);
                    string strBoss = strConsumeTypeOrRecharge.Substring(0, strConsumeTypeOrRecharge.Length - 7);


                    if (i == 82)
                    {
                        strBoss = strBoss.Substring(0, strBoss.Length - 2);
                    }
                    /// 1001920000 200 3968
                    DateTime DateTimemy = Convert.ToDateTime(dateCreatTime);
                    string strShowDay = DateTimemy.Year.toString() + "-" + Eggsoft.Common.StringNum.Add000000Num(DateTimemy.Month, 2).toString() + "-" + Eggsoft.Common.StringNum.Add000000Num(DateTimemy.Day, 2).toString();
                    Model_b007_OperationReturnMoneyEveryDay = BLLb007_OperationReturnMoneyEveryDay.GetModel("ShopClient_ID=21 and ThisDay=@ThisDay", strShowDay);
                    if (Model_b007_OperationReturnMoneyEveryDay != null)
                    {
                        if (Model_b007_OperationReturnMoneyEveryDay.ThisDayMoneyByBoss != (strBoss.toDecimal() / (Decimal)10000.00))
                        {
                            int a = 0;
                        }
                        else
                        {
                            if (Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder != strAllOrder.toInt32())
                            {

                                Model_b007_OperationReturnMoneyEveryDay.ThisDayAllActiveOrder = strAllOrder.toInt32();
                                Model_b007_OperationReturnMoneyEveryDay.EveryOrderGet = ConsumeOrRechargeWealth / (strMeOrder.toInt32() / 100);
                                Model_b007_OperationReturnMoneyEveryDay.UpdateTime = DateTime.Now;
                                Model_b007_OperationReturnMoneyEveryDay.UpdateBy = Model_b007_OperationReturnMoneyEveryDay.UpdateBy.toString() + " 追加订单统计";
                                BLLb007_OperationReturnMoneyEveryDay.Update(Model_b007_OperationReturnMoneyEveryDay);
                            }
                            else
                            {
                                int dddddddddd = 0;
                            }
                        }
                    }
                    else
                    {
                        int b = 0;
                    }
                }

            }
        }




        /// <summary>
        /// 今天早上3点多，由于系统维护,今天分红未能准时。对此我们非常抱歉。我们的技术团队在紧急处理财富模块及账户余额模块、安全模块的维护升级，导致相关用户的账户余额未及时到账。在早上7:10左右恢复完毕。建议受影响的用户重试一下。同时，大家的资金和信息安全不会收到任何影响，请放心。这件事情由于技术团队未能及时通知用户，给部分用户造成了心里影响，我们感到万分羞愧。同时，我们给相关每个用户22个沁加币，并恳请原谅我们的过失。
        /// </summary>

        private void SorrysendSNS22GouWuQuan_WeiXin()

        {
            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();


            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            System.Data.DataTable Data_DataTable = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetList("[UserID],[ShopClient_ID]", " 1=1 order by id desc").Tables[0];
            for (int i = 0; i < Data_DataTable.Rows.Count; i++)
            {
                string strUserID = Data_DataTable.Rows[i]["UserID"].toString();
                string strShopClient_ID = Data_DataTable.Rows[i]["ShopClient_ID"].toString();
                EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = strUserID.toInt32();
                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = 22;
                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("今早3-7点未准时分红致歉", 46);
                int intID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
            }


        }


        private void SorrysendSNSToMyParentBonus_WeiXin
             ()
        {
            ///ReWrite_0609();
            #region 每天运营中心加权分红
            //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红开始执行4", "每天更新");
            //Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay mmPub_Default_DoYunYingZhongXin28EveryDay = new Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay(21);
            //Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红执行完毕4", "每天更新");
            string strList = @"44447
43683,
44970,
44965,
43591,
41506,
44930,
44973,
45140,
44971,
44990,
45075,
45044,
45027,
45026,
44847,
44849,
44886,
44939,
44950,
44828,
44825";
            //strList = "41192,43209";

            String[] struserList = strList.Split(',');
            for (int i = 0; i < struserList.Length; i++)
            {
                sendSNSToMyParentBonus_WeiXin(21, struserList[i].toInt32());
            }
            #endregion 每天运营中心加权分红
        }
        private void sendSNSToMyParentBonus_WeiXin(int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
            string strTitle = "致歉信:" + strUserNickName + "您好，由于今早机房网络更换，导致部分用户分红未到账。你的今天分红已补发完毕。给您带来的不便深表歉意";
            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pub_Int_Session_CurUserID, pub_Int_Session_CurUserID, strTitle);

        }



        #region Wealth
        /// <summary>
        /// 337737.05 郑军 588 账户实际财富积分余额 87088.23   仲从昌 700 账户实际财富积分余额
        /// </summary>
        private void ReWrite_0609()
        {
            EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();


            Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
            Model_b006_TotalWealth_OperationUser.UserID = 43114;
            Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
            Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = (Decimal)157.28;
            Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.20补追现金", 46);
            int intID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);




            //ReWrite0609((Decimal)337737.05, 42985);
            //337737.05  郑军 588 账户实际财富积分余额       42985
            //87088.23   仲从昌 700 账户实际财富积分余额    43209
            // ReWrite0609((Decimal)87088.23, 43209);

            #endregion Wealth
        }

        private void ReWrite0609(decimal decimalOne, Int32 Int32UserID)
        {
            #region one people

            Decimal myCountuserWealth = 0;
            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Int32UserID, out myCountuserWealth);

            EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();


            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum my_BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
            EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();

            if (decimalOne > myCountuserWealth)
            {
                Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = true;
                Model_b006_TotalWealth_OperationUser.UserID = Int32UserID;
                Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = decimalOne - myCountuserWealth;
                Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.9上线操作", 46);
                int intID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

            }
            else if (decimalOne < myCountuserWealth)
            {
                Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                Model_b006_TotalWealth_OperationUser.UserID = Int32UserID;
                Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = -(decimalOne - myCountuserWealth);
                Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.9上线操作", 46);
                int intID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
            }
            Model_b008_OpterationUserActiveReturnMoneyOrderNum = my_BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("ShopClient_ID=@ShopClient_ID and UserID=@UserID", 21, Int32UserID);
            Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = decimalOne;
            Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "平台6.9上线操作";
            my_BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);
            #endregion one people
        }



        private void ReWrite0606()
        {
            EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
            EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();

            Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
            Model_b006_TotalWealth_OperationUser.UserID = 43650;//1033;
            Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
            Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = (decimal)174.53;////返还这么多 就可以了
            Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "线下自动导入0606";
            int intReturnID = BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);


            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = 43650;//1033;
            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = (decimal)174.53;////返还这么多 就可以了
            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "现金补录" + intReturnID.toString();
            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
            BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


            Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
            Model_b006_TotalWealth_OperationUser.UserID = 43217;//708
            Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
            Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = (decimal)5994;////
            Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "线下补单导入0606";
            intReturnID = BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
            Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = (decimal)174.53;////返还这么多 就可以了
            intReturnID = BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = 43217;//708
            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = (decimal)174.53;////返还这么多 就可以了
            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "现金补录" + intReturnID.toString();
            BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

            // 22524.15
        }

        private void readExcelIntoOrder()
        {




            int i = 0;
            try
            {
                string strTablename = "[原始数据$]";
                string strAccessfilePath = @"E:\时仪文件\009微云基石\038沁加功能\18沁加运营中心-张工17-5-19(1).xlsx";
                String SQLtem_sql = "select * from " + strTablename + " order by F18 asc ";
                System.Data.DataSet myDataSet = EggsoftWX.SQLServerDAL.DbHelperSQL.ReadExcel(strAccessfilePath, SQLtem_sql);

                for (i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    string struserID = myDataSet.Tables[0].Rows[i]["F7"].toString();
                    string strParentID = myDataSet.Tables[0].Rows[i]["F9"].toString();
                    string strGrandParentID = myDataSet.Tables[0].Rows[i]["F11"].toString();
                    string strOperationCenterID = myDataSet.Tables[0].Rows[i]["F1-1"].toString();
                    string strPayTime = myDataSet.Tables[0].Rows[i]["F1"].toString();
                    string strPayMpney = myDataSet.Tables[0].Rows[i]["F17"].toString();
                    string strNum = myDataSet.Tables[0].Rows[i]["F_1"].toString();
                    string strF18Num = myDataSet.Tables[0].Rows[i]["F18"].toString();
                    int intPayCount = strPayMpney.toInt32() / 1998;

                    if (strOperationCenterID.toInt32() == 0)
                    {
                        continue;
                    }
                    if (struserID.toInt32() == 0)
                    {
                        continue;
                    }
                    EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.BLL.tab_Orderdetails blltab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                    EggsoftWX.BLL.b002_OperationCenter bllb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.BLL.b004_OperationGoods bllb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                    EggsoftWX.BLL.tab_Goods blltab_Goods = new EggsoftWX.BLL.tab_Goods();


                    #region 前端录入合法性检查
                    string strINCID = "21";

                    EggsoftWX.Model.tab_User Model_tab_User = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", struserID.toInt32(), strINCID);
                    if (Model_tab_User == null)
                    {
                        JsUtil.ShowMsg("添加失败，当前支付用户微店ID不存在!", -1);
                        return;
                    }
                    EggsoftWX.Model.tab_User Model_tab_UserParentID = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strParentID.toInt32(), strINCID);
                    if (Model_tab_UserParentID == null && strParentID.toInt32() > 0)
                    {
                        JsUtil.ShowMsg("添加失败，上级用户微店ID不存在!", -1);
                        return;
                    }
                    EggsoftWX.Model.tab_User Model_tab_UserGrandParentID = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strGrandParentID.toInt32(), strINCID);
                    if (Model_tab_UserGrandParentID == null && strGrandParentID.toInt32() > 0)
                    {
                        JsUtil.ShowMsg("添加失败，上上级用户微店ID不存在!", -1);
                        return;
                    }
                    if (intPayCount < 1 || intPayCount > 999999)
                    {
                        JsUtil.ShowMsg("添加失败，购买数量非法!", -1);
                        return;
                    }
                    EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = bllb004_OperationGoods.GetModel("ID=@OperationGoodsID and ShopClient_ID=@ShopClientID", 1, strINCID);
                    //if (Model_b004_OperationGoods == null)
                    //{
                    //    JsUtil.ShowMsg("添加失败，支付运营商品ID不存在!", -1);
                    //    return;
                    //}
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = bllb002_OperationCenter.GetModel("ID=@OperationCenterID and ShopClient_ID=@ShopClientID", strOperationCenterID.toInt32(), strINCID);
                    if (Model_b002_OperationCenter == null)
                    {
                        JsUtil.ShowMsg("添加失败，运营中心ID不存在!", -1);
                        return;
                    }


                    DateTime paydatetime = Convert.ToDateTime(strPayTime.toDateTime());
                    if (paydatetime <= DateTime.MinValue || paydatetime >= DateTime.MaxValue)
                    {
                        JsUtil.ShowMsg("添加失败，支付时间处理失败，可请求技术支持处理!", -1);
                        return;
                    }
                    #endregion 前端录入合法性检查
                    EggsoftWX.Model.tab_Goods Modeltab_Goods = blltab_Goods.GetModel("ID=" + Model_b004_OperationGoods.GoodID);
                    EggsoftWX.Model.tab_Order Modeltab_Order = new EggsoftWX.Model.tab_Order();
                    Modeltab_Order.CreateDateTime = DateTime.Now;
                    Modeltab_Order.DeliveryText = "Excel线下录单";
                    Modeltab_Order.OrderName = "线下录单" + Modeltab_Goods.Name;
                    Modeltab_Order.OrderNum = paydatetime.ToString("yyyyMMddHHmmss") + strNum + Eggsoft.Common.StringNum.Add000000Num(strF18Num.toInt32(), 3);
                    Modeltab_Order.PayDateTime = paydatetime;
                    Modeltab_Order.PayStatus = 1;
                    Modeltab_Order.ShopClient_ID = strINCID.toInt32();
                    Modeltab_Order.UserID = Model_tab_User.ID;
                    Modeltab_Order.TotalMoney = Modeltab_Goods.PromotePrice * intPayCount;
                    int intModeltab_OrderID = blltab_Order.Add(Modeltab_Order);

                    EggsoftWX.Model.tab_Orderdetails Modeltab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();
                    Modeltab_Orderdetails.GoodID = Modeltab_Goods.ID;
                    Modeltab_Orderdetails.OrderCount = intPayCount;
                    Modeltab_Orderdetails.OrderID = intModeltab_OrderID;
                    Modeltab_Orderdetails.Over7DaysToBeans = false;
                    Modeltab_Orderdetails.ParentID = (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID);
                    Modeltab_Orderdetails.GrandParentID = (Model_tab_UserGrandParentID == null ? 0 : Model_tab_UserGrandParentID.ID);
                    Modeltab_Orderdetails.ShopClient_ID = strINCID.toInt32();
                    Modeltab_Orderdetails.GoodName = "线下录单" + Modeltab_Goods.Name;
                    Modeltab_Orderdetails.GoodPrice = Modeltab_Goods.PromotePrice;
                    Modeltab_Orderdetails.GoodType = 6;
                    Modeltab_Orderdetails.GoodTypeId = strOperationCenterID.toInt32();
                    Modeltab_Orderdetails.GoodTypeIdBuyInfo = 1.toString();
                    blltab_Orderdetails.Add(Modeltab_Orderdetails);


                    #region 是否自动给予分销权  首次购买制作证书

                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + Model_tab_User.ID + "  and IsDeleted=0 ");
                    if (Model_tab_ShopClient_Agent_ == null || Model_tab_ShopClient_Agent_.Empowered != true)////首次购买制作证书
                    {
                        EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                        EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);
                        if (tab_ShopClient_ShopPar_Model != null)
                        {
                            if (tab_ShopClient_ShopPar_Model.AskAgentAutoAfterBuy.toBoolean())
                            {

                                Eggsoft_Public_CL.Pub_Agent.add_Agent_Default_OnlyOneKey(Convert.ToInt32(Model_tab_User.ID));
                            }
                        }
                    }
                    #endregion

                    //#region 初始化所有运营中心数据  粉丝数据
                    //System.Threading.Tasks.Task.Factory.StartNew(() =>
                    //{
                    //    if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strINCID.toInt32()))
                    //    {
                    //        Eggsoft_Public_CL.OperationCenter.update_b005_UserID_Operation_ID(Model_tab_User.ID, strINCID.toInt32(), TextBox3ParentID.Text.toInt32(), Model_b002_OperationCenter.ID, Model_b002_OperationCenter.UserID.toInt32());
                    //        Eggsoft_Public_CL.OperationCenter.update_Only_One_UserID_Operation_ID(Model_tab_User.ID, strINCID.toInt32(), TextBox3ParentID.Text.toInt32(), Model_b002_OperationCenter.ID);
                    //    }
                    //});
                    //#endregion 初始化所有运营中心数据
                }
            }
            catch (Exception ddddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddddd, "录入数据失败", "i=" + i);

            }
        }


        private void readMoneyIntoReturnMoney()
        {


            int i = 0;
            try
            {
                string strTablename = "[原始数据$]";
                string strAccessfilePath = @"E:\时仪文件\009微云基石\038沁加功能\18沁加运营中心-张工17-5-19(1).xlsx";
                String SQLtem_sql = "select * from " + strTablename + " order by F18 asc ";
                System.Data.DataSet myDataSet = EggsoftWX.SQLServerDAL.DbHelperSQL.ReadExcel(strAccessfilePath, SQLtem_sql);

                for (i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    string struserID = myDataSet.Tables[0].Rows[i]["F7"].toString();
                    string strParentID = myDataSet.Tables[0].Rows[i]["F9"].toString();
                    string strGrandParentID = myDataSet.Tables[0].Rows[i]["F11"].toString();
                    string strOperationCenterID = myDataSet.Tables[0].Rows[i]["F1-1"].toString();
                    string strPayTime = myDataSet.Tables[0].Rows[i]["F1"].toString();
                    string strPayMpney = myDataSet.Tables[0].Rows[i]["F17"].toString();
                    string strNum = myDataSet.Tables[0].Rows[i]["F_1"].toString();
                    string strF18Num = myDataSet.Tables[0].Rows[i]["F18"].toString();
                    Decimal DecimalConsumeOrRechargeWealth = myDataSet.Tables[0].Rows[i]["F14"].toDecimal();///已提金额

                    int intPayCount = strPayMpney.toInt32() / 1998;

                    if (strOperationCenterID.toInt32() == 0)
                    {
                        continue;
                    }
                    if (struserID.toInt32() == 0)
                    {
                        continue;
                    }
                    EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.BLL.tab_Orderdetails blltab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                    EggsoftWX.BLL.b002_OperationCenter bllb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.BLL.b004_OperationGoods bllb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                    EggsoftWX.BLL.tab_Goods blltab_Goods = new EggsoftWX.BLL.tab_Goods();


                    #region 前端录入合法性检查
                    string strINCID = "21";

                    EggsoftWX.Model.tab_User Model_tab_User = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", struserID.toInt32(), strINCID);
                    if (Model_tab_User == null)
                    {
                        JsUtil.ShowMsg("添加失败，当前支付用户微店ID不存在!", -1);
                        return;
                    }
                    EggsoftWX.Model.tab_User Model_tab_UserParentID = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strParentID.toInt32(), strINCID);
                    if (Model_tab_UserParentID == null && strParentID.toInt32() > 0)
                    {
                        JsUtil.ShowMsg("添加失败，上级用户微店ID不存在!", -1);
                        return;
                    }
                    EggsoftWX.Model.tab_User Model_tab_UserGrandParentID = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strGrandParentID.toInt32(), strINCID);
                    if (Model_tab_UserGrandParentID == null && strGrandParentID.toInt32() > 0)
                    {
                        JsUtil.ShowMsg("添加失败，上上级用户微店ID不存在!", -1);
                        return;
                    }
                    if (intPayCount < 1 || intPayCount > 999999)
                    {
                        JsUtil.ShowMsg("添加失败，购买数量非法!", -1);
                        return;
                    }
                    EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = bllb004_OperationGoods.GetModel("ID=@OperationGoodsID and ShopClient_ID=@ShopClientID", 1, strINCID);
                    //if (Model_b004_OperationGoods == null)
                    //{
                    //    JsUtil.ShowMsg("添加失败，支付运营商品ID不存在!", -1);
                    //    return;
                    //}
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = bllb002_OperationCenter.GetModel("ID=@OperationCenterID and ShopClient_ID=@ShopClientID", strOperationCenterID.toInt32(), strINCID);
                    if (Model_b002_OperationCenter == null)
                    {
                        JsUtil.ShowMsg("添加失败，运营中心ID不存在!", -1);
                        return;
                    }
                    if (DecimalConsumeOrRechargeWealth <= 0)////  还没有结算 到7天 
                    {
                        continue;
                    }

                    DateTime paydatetime = Convert.ToDateTime(strPayTime.toDateTime());
                    if (paydatetime <= DateTime.MinValue || paydatetime >= DateTime.MaxValue)
                    {
                        JsUtil.ShowMsg("添加失败，支付时间处理失败，可请求技术支持处理!", -1);
                        return;
                    }
                    #endregion 前端录入合法性检查

                    EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                    EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();


                    string strUserID = Model_tab_User.ID.toString();



                    Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                    Model_b006_TotalWealth_OperationUser.UserID = strUserID.toInt32();
                    Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalConsumeOrRechargeWealth;////返还这么多 就可以了
                    Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "每天分红财富，减增进入现金(总金额" + DecimalConsumeOrRechargeWealth + "线下自动导入" + strNum + ")";
                    int intReturnID = BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

                    if (intReturnID <= 0)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog("余额不足", "录入数据失败", strNum);
                    }
                    else if (intReturnID > 0)
                    {
                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "财富" + strNum + "返还进入现金 财富转化WealthID=" + intReturnID;
                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


                        Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "线下操作现金已提现" + strNum;
                        BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                    }


                    //EggsoftWX.Model.tab_Goods Modeltab_Goods = blltab_Goods.GetModel("ID=" + Model_b004_OperationGoods.GoodID);
                    //EggsoftWX.Model.tab_Order Modeltab_Order = new EggsoftWX.Model.tab_Order();
                    //Modeltab_Order.CreateDateTime = DateTime.Now;
                    //Modeltab_Order.DeliveryText = "Excel线下录单";
                    //Modeltab_Order.OrderName = "线下录单" + Modeltab_Goods.Name;
                    //Modeltab_Order.OrderNum = paydatetime.ToString("yyyyMMddHHmmss") + i.toString() + strNum;
                    //Modeltab_Order.PayDateTime = paydatetime;
                    //Modeltab_Order.PayStatus = true;
                    //Modeltab_Order.ShopClient_ID = strINCID.toInt32();
                    //Modeltab_Order.UserID = Model_tab_User.ID;
                    //Modeltab_Order.TotalMoney = Modeltab_Goods.PromotePrice * 1;
                    //int intModeltab_OrderID = blltab_Order.Add(Modeltab_Order);

                    //EggsoftWX.Model.tab_Orderdetails Modeltab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();
                    //Modeltab_Orderdetails.GoodID = Modeltab_Goods.ID;
                    //Modeltab_Orderdetails.OrderCount = intPayCount;
                    //Modeltab_Orderdetails.OrderID = intModeltab_OrderID;
                    //Modeltab_Orderdetails.Over7DaysToBeans = false;
                    //Modeltab_Orderdetails.ParentID = (Model_tab_UserParentID == null ? 0 : Model_tab_UserParentID.ID);
                    //Modeltab_Orderdetails.GrandParentID = (Model_tab_UserGrandParentID == null ? 0 : Model_tab_UserGrandParentID.ID);
                    //Modeltab_Orderdetails.ShopClient_ID = strINCID.toInt32();
                    //Modeltab_Orderdetails.GoodName = "线下录单" + Modeltab_Goods.Name;
                    //Modeltab_Orderdetails.GoodPrice = Modeltab_Goods.PromotePrice;
                    //Modeltab_Orderdetails.GoodType = 6;
                    //Modeltab_Orderdetails.GoodTypeId = strOperationCenterID.toInt32();
                    //Modeltab_Orderdetails.GoodTypeIdBuyInfo = 1.toString();
                    //blltab_Orderdetails.Add(Modeltab_Orderdetails);

                    //#region 初始化所有运营中心数据  粉丝数据
                    //System.Threading.Tasks.Task.Factory.StartNew(() =>
                    //{
                    //    if (Eggsoft_Public_CL.OperationCenter.ExsitMode_OperationCenter(strINCID.toInt32()))
                    //    {
                    //        Eggsoft_Public_CL.OperationCenter.update_b005_UserID_Operation_ID(Model_tab_User.ID, strINCID.toInt32(), TextBox3ParentID.Text.toInt32(), Model_b002_OperationCenter.ID, Model_b002_OperationCenter.UserID.toInt32());
                    //        Eggsoft_Public_CL.OperationCenter.update_Only_One_UserID_Operation_ID(Model_tab_User.ID, strINCID.toInt32(), TextBox3ParentID.Text.toInt32(), Model_b002_OperationCenter.ID);
                    //    }
                    //});
                    //#endregion 初始化所有运营中心数据
                }
            }
            catch (Exception ddddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddddd, "录入数据失败", "i=" + i);

            }

        }

        private void readMoneyIntoReturnMoney_P()
        {


            int i = 0;
            try
            {
                string strTablename = "[原始数据$]";
                string strAccessfilePath = @"E:\时仪文件\009微云基石\038沁加功能\19沁加运营中心-张工17-5-22.xlsx";
                String SQLtem_sql = "select * from " + strTablename + " where F19=1 order by F18 asc ";
                System.Data.DataSet myDataSet = EggsoftWX.SQLServerDAL.DbHelperSQL.ReadExcel(strAccessfilePath, SQLtem_sql);

                for (i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    string struserID = myDataSet.Tables[0].Rows[i]["F7"].toString();
                    string strParentID = myDataSet.Tables[0].Rows[i]["F9"].toString();
                    string strGrandParentID = myDataSet.Tables[0].Rows[i]["F11"].toString();
                    string strOperationCenterID = myDataSet.Tables[0].Rows[i]["F1-1"].toString();
                    string strPayTime = myDataSet.Tables[0].Rows[i]["F1"].toString();
                    string strPayMpney = myDataSet.Tables[0].Rows[i]["F17"].toString();
                    string strNum = myDataSet.Tables[0].Rows[i]["F_1"].toString();
                    string strF18Num = myDataSet.Tables[0].Rows[i]["F18"].toString();
                    Decimal DecimalConsumeOrRechargeWealth = myDataSet.Tables[0].Rows[i]["F14"].toDecimal();///已提金额

                    int intPayCount = strPayMpney.toInt32() / 1998;

                    if (strOperationCenterID.toInt32() == 0)
                    {
                        continue;
                    }
                    if (struserID.toInt32() == 0)
                    {
                        continue;
                    }
                    EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.BLL.tab_Orderdetails blltab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                    EggsoftWX.BLL.b002_OperationCenter bllb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.BLL.b004_OperationGoods bllb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                    EggsoftWX.BLL.tab_Goods blltab_Goods = new EggsoftWX.BLL.tab_Goods();


                    #region 前端录入合法性检查
                    string strINCID = "21";

                    EggsoftWX.Model.tab_User Model_tab_User = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", struserID.toInt32(), strINCID);
                    if (Model_tab_User == null)
                    {
                        JsUtil.ShowMsg("添加失败，当前支付用户微店ID不存在!", -1);
                        return;
                    }
                    EggsoftWX.Model.tab_User Model_tab_UserParentID = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strParentID.toInt32(), strINCID);
                    if (Model_tab_UserParentID == null && strParentID.toInt32() > 0)
                    {
                        JsUtil.ShowMsg("添加失败，上级用户微店ID不存在!", -1);
                        return;
                    }
                    EggsoftWX.Model.tab_User Model_tab_UserGrandParentID = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strGrandParentID.toInt32(), strINCID);
                    if (Model_tab_UserGrandParentID == null && strGrandParentID.toInt32() > 0)
                    {
                        JsUtil.ShowMsg("添加失败，上上级用户微店ID不存在!", -1);
                        return;
                    }
                    if (intPayCount < 1 || intPayCount > 999999)
                    {
                        JsUtil.ShowMsg("添加失败，购买数量非法!", -1);
                        return;
                    }
                    EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = bllb004_OperationGoods.GetModel("ID=@OperationGoodsID and ShopClient_ID=@ShopClientID", 1, strINCID);
                    //if (Model_b004_OperationGoods == null)
                    //{
                    //    JsUtil.ShowMsg("添加失败，支付运营商品ID不存在!", -1);
                    //    return;
                    //}
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = bllb002_OperationCenter.GetModel("ID=@OperationCenterID and ShopClient_ID=@ShopClientID", strOperationCenterID.toInt32(), strINCID);
                    if (Model_b002_OperationCenter == null)
                    {
                        JsUtil.ShowMsg("添加失败，运营中心ID不存在!", -1);
                        return;
                    }
                    if (DecimalConsumeOrRechargeWealth <= 0)////  还没有结算 到7天 
                    {
                        continue;
                    }

                    DateTime paydatetime = Convert.ToDateTime(strPayTime.toDateTime());
                    if (paydatetime <= DateTime.MinValue || paydatetime >= DateTime.MaxValue)
                    {
                        JsUtil.ShowMsg("添加失败，支付时间处理失败，可请求技术支持处理!", -1);
                        return;
                    }
                    #endregion 前端录入合法性检查

                    EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                    EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();


                    string strUserID = Model_tab_User.ID.toString();

                    Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                    Model_b006_TotalWealth_OperationUser.UserID = strUserID.toInt32();
                    Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalConsumeOrRechargeWealth;////返还这么多 就可以了
                    Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "每天分红财富，减增进入现金(总金额" + DecimalConsumeOrRechargeWealth + "线下自动导入" + strNum + ")";
                    int intReturnID = BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

                    if (intReturnID <= 0)
                    {

                        Eggsoft.Common.debug_Log.Call_WriteLog("余额不足", "录入数据失败", strNum);
                    }
                    else if (intReturnID > 0)
                    {
                        EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                        EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();
                        Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and shopClient_ID=21", strUserID.toInt32());
                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() - Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "紫色数据";
                        BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "财富" + strNum + "返还进入现金 财富转化WealthID=" + intReturnID;
                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


                        Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "线下操作现金已提现" + strNum;
                        BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                    }

                }
            }
            catch (Exception ddddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddddd, "录入数据失败", "i=" + i);

            }

        }

        private void readMoneyIntoReturnMoney_P_Min()
        {


            int i = 0;
            try
            {
                string strTablename = "[原始数据$]";
                string strAccessfilePath = @"E:\时仪文件\009微云基石\038沁加功能\19沁加运营中心-张工17-5-22.xlsx";
                String SQLtem_sql = "select * from " + strTablename + " where F19=2 order by F18 asc ";
                System.Data.DataSet myDataSet = EggsoftWX.SQLServerDAL.DbHelperSQL.ReadExcel(strAccessfilePath, SQLtem_sql);

                for (i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    string struserID = myDataSet.Tables[0].Rows[i]["F7"].toString();
                    string strParentID = myDataSet.Tables[0].Rows[i]["F9"].toString();
                    string strGrandParentID = myDataSet.Tables[0].Rows[i]["F11"].toString();
                    string strOperationCenterID = myDataSet.Tables[0].Rows[i]["F1-1"].toString();
                    string strPayTime = myDataSet.Tables[0].Rows[i]["F1"].toString();
                    string strPayMpney = myDataSet.Tables[0].Rows[i]["F17"].toString();
                    string strNum = myDataSet.Tables[0].Rows[i]["F_1"].toString();
                    string strF18Num = myDataSet.Tables[0].Rows[i]["F18"].toString();
                    //Decimal DecimalConsumeOrRechargeWealth = myDataSet.Tables[0].Rows[i]["F14"].toDecimal();///已提金额
                    Decimal DecimalConsumeOrRechargeWealthMin = myDataSet.Tables[0].Rows[i]["F21"].toDecimal();///最加已结算用户的提现

                    int intPayCount = strPayMpney.toInt32() / 1998;

                    if (strOperationCenterID.toInt32() == 0)
                    {
                        continue;
                    }
                    if (struserID.toInt32() == 0)
                    {
                        continue;
                    }
                    EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.BLL.tab_Orderdetails blltab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                    EggsoftWX.BLL.b002_OperationCenter bllb002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.BLL.b004_OperationGoods bllb004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                    EggsoftWX.BLL.tab_Goods blltab_Goods = new EggsoftWX.BLL.tab_Goods();


                    #region 前端录入合法性检查
                    string strINCID = "21";

                    EggsoftWX.Model.tab_User Model_tab_User = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", struserID.toInt32(), strINCID);
                    if (Model_tab_User == null)
                    {
                        JsUtil.ShowMsg("添加失败，当前支付用户微店ID不存在!", -1);
                        return;
                    }
                    EggsoftWX.Model.tab_User Model_tab_UserParentID = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strParentID.toInt32(), strINCID);
                    if (Model_tab_UserParentID == null && strParentID.toInt32() > 0)
                    {
                        JsUtil.ShowMsg("添加失败，上级用户微店ID不存在!", -1);
                        return;
                    }
                    EggsoftWX.Model.tab_User Model_tab_UserGrandParentID = blltab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", strGrandParentID.toInt32(), strINCID);
                    if (Model_tab_UserGrandParentID == null && strGrandParentID.toInt32() > 0)
                    {
                        JsUtil.ShowMsg("添加失败，上上级用户微店ID不存在!", -1);
                        return;
                    }
                    if (intPayCount < 1 || intPayCount > 999999)
                    {
                        JsUtil.ShowMsg("添加失败，购买数量非法!", -1);
                        return;
                    }
                    EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = bllb004_OperationGoods.GetModel("ID=@OperationGoodsID and ShopClient_ID=@ShopClientID", 1, strINCID);
                    //if (Model_b004_OperationGoods == null)
                    //{
                    //    JsUtil.ShowMsg("添加失败，支付运营商品ID不存在!", -1);
                    //    return;
                    //}
                    EggsoftWX.Model.b002_OperationCenter Model_b002_OperationCenter = bllb002_OperationCenter.GetModel("ID=@OperationCenterID and ShopClient_ID=@ShopClientID", strOperationCenterID.toInt32(), strINCID);
                    if (Model_b002_OperationCenter == null)
                    {
                        JsUtil.ShowMsg("添加失败，运营中心ID不存在!", -1);
                        return;
                    }
                    if (DecimalConsumeOrRechargeWealthMin <= 0)////  还没有结算 到7天 
                    {
                        continue;
                    }

                    DateTime paydatetime = Convert.ToDateTime(strPayTime.toDateTime());
                    if (paydatetime <= DateTime.MinValue || paydatetime >= DateTime.MaxValue)
                    {
                        JsUtil.ShowMsg("添加失败，支付时间处理失败，可请求技术支持处理!", -1);
                        return;
                    }
                    #endregion 前端录入合法性检查

                    EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                    EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();


                    string strUserID = Model_tab_User.ID.toString();

                    Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                    Model_b006_TotalWealth_OperationUser.UserID = strUserID.toInt32();
                    Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalConsumeOrRechargeWealthMin;////返还这么多 就可以了
                    Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "每天分红财富，减增进入现金(总金额" + DecimalConsumeOrRechargeWealthMin + "线下自动导入" + strNum + ")";
                    int intReturnID = BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

                    if (intReturnID <= 0)
                    {

                        Eggsoft.Common.debug_Log.Call_WriteLog("余额不足", "录入数据失败", strNum);
                    }
                    else if (intReturnID > 0)
                    {
                        EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                        EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();
                        Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and shopClient_ID=21", strUserID.toInt32());
                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() - Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = "紫色数据";
                        BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "财富" + strNum + "返还进入现金 财富转化WealthID=" + intReturnID;
                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


                        Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                        Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                        Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "线下操作现金已提现" + strNum;
                        BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                    }

                }
            }
            catch (Exception ddddd)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ddddd, "录入数据失败", "i=" + i);

            }

        }

        private void readCheckResultMoneyIntoReturnMoney_P_Min()
        {
            try
            {

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();
                EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();

                String SQLtem_sql = "select sum(F16) as  积分余额必填,f7 as 微店号ID   FROM [原始数据$] where F16 is not null group by F7 ";
                System.Data.DataSet myDataSet = EggsoftWX.SQLServerDAL.DbHelperSQL.Query(SQLtem_sql);
                for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    string strYuE = myDataSet.Tables[0].Rows[i]["积分余额必填"].toString();
                    string strShopUserID = myDataSet.Tables[0].Rows[i]["微店号ID"].toString();
                    int intShopUserID = strShopUserID.toInt32();
                    if (intShopUserID > 0 && strYuE.toDecimal() > 0)
                    {
                        Model_tab_User = BLL_tab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", intShopUserID, 21);
                        if (Model_tab_User != null)
                        {
                            EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID", Model_tab_User.ID, 21, 1);

                            Decimal myCountWealth = 0;
                            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Model_tab_User.ID, out myCountWealth);
                            if (Model_b008_OpterationUserActiveReturnMoneyOrderNum != null)
                            {
                                EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();

                                if (myCountWealth > strYuE.toDecimal())
                                {
                                    Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                                    Model_b006_TotalWealth_OperationUser.UserID = Model_tab_User.ID;
                                    Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                                    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = myCountWealth - strYuE.toDecimal();
                                    Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("财富积分线下转线上减少", 46);
                                    int intID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
                                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update("Exsit=1111", "UserID=" + Model_tab_User.ID);
                                }
                                else if (myCountWealth < strYuE.toDecimal())
                                {
                                    Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = true;
                                    Model_b006_TotalWealth_OperationUser.UserID = Model_tab_User.ID;
                                    Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                                    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = strYuE.toDecimal() - myCountWealth;
                                    Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("财富积分线下转线上增加", 46);
                                    int intID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
                                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update("Exsit=2222", "UserID=" + Model_tab_User.ID);
                                }
                                else
                                {
                                    Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = strYuE.toDecimal();
                                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update("Exsit=3333", "UserID=" + Model_tab_User.ID);
                                }

                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth;///记录这款商品 还有多少钱没还给用户
                                Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                if (Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit <= 0)///表示给完了
                                {
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = 0;
                                }
                                BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);


                            }
                            else
                            {
                                Eggsoft.Common.debug_Log.Call_WriteLog("strShopUserID=" + strShopUserID, "执行最终结算找不到用户", "程序报错strShopUserID=" + strShopUserID);
                            }
                        }
                        else
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("", "执行最终结算", "Excel的微店号码不存在  intShopUserID=" + intShopUserID);
                        }
                    }
                }
            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "执行最终结算", "程序报错");
            }
        }


        private void readCheckResultMoneyIntoReturnMoney_P_Min20170601()
        {
            try
            {

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = new EggsoftWX.Model.tab_User();


                String SQLtem_sql = "select F3 as realname,F4 as ShopuserID,	F6 as OperationCenterMoney, 	F7 as TotalCredits,	F8 as Total_Vouchers,	F9 as TotalWealth   FROM [原始数据$] order by F0 asc";
                System.Data.DataSet myDataSet = EggsoftWX.SQLServerDAL.DbHelperSQL.Query(SQLtem_sql);
                for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    string strrealname = myDataSet.Tables[0].Rows[i]["realname"].toString().Trim();
                    Int32 intShopuserID = myDataSet.Tables[0].Rows[i]["ShopuserID"].toString().Trim().toInt32();
                    Decimal DecimalOperationCenterMoneyDo = myDataSet.Tables[0].Rows[i]["OperationCenterMoney"].toString().toDecimal();
                    Decimal DecimalTotalCreditsDo = myDataSet.Tables[0].Rows[i]["TotalCredits"].toString().toDecimal();
                    Decimal DecimalTotal_VouchersDo = myDataSet.Tables[0].Rows[i]["Total_Vouchers"].toString().toDecimal();
                    Decimal DecimalTotalWealthDo = myDataSet.Tables[0].Rows[i]["TotalWealth"].toString().toDecimal();

                    Model_tab_User = BLL_tab_User.GetModel("ShopUserID=@ShopUserID and ShopClientID=@ShopClientID", intShopuserID, 21);
                    if (Model_tab_User != null)
                    {
                        #region OperationCenterMoney
                        //if (DecimalOperationCenterMoneyDo > 0)
                        //{

                        Decimal myCountOperationCenter = 0;
                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuETotalCredits_OperationCenter(Model_tab_User.ID, out myCountOperationCenter);

                        EggsoftWX.BLL.b003_TotalCredits_OperationCenter my_BLL_b003_TotalCredits_OperationCenter = new EggsoftWX.BLL.b003_TotalCredits_OperationCenter();
                        EggsoftWX.Model.b003_TotalCredits_OperationCenter Model_b003_TotalCredits_OperationCenter = new EggsoftWX.Model.b003_TotalCredits_OperationCenter();

                        if (DecimalOperationCenterMoneyDo > myCountOperationCenter)
                        {
                            Model_b003_TotalCredits_OperationCenter.Bool_ConsumeOrRecharge = true;
                            Model_b003_TotalCredits_OperationCenter.UserID = Model_tab_User.ID;
                            Model_b003_TotalCredits_OperationCenter.ShopClient_ID = 21;
                            Model_b003_TotalCredits_OperationCenter.ConsumeOrRechargeMoney = (DecimalOperationCenterMoneyDo - myCountOperationCenter);
                            Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.1上线操作", 46);
                            int intID = my_BLL_b003_TotalCredits_OperationCenter.Add(Model_b003_TotalCredits_OperationCenter);

                        }
                        else if (DecimalOperationCenterMoneyDo < myCountOperationCenter)
                        {
                            Model_b003_TotalCredits_OperationCenter.Bool_ConsumeOrRecharge = false;
                            Model_b003_TotalCredits_OperationCenter.UserID = Model_tab_User.ID;
                            Model_b003_TotalCredits_OperationCenter.ShopClient_ID = 21;
                            Model_b003_TotalCredits_OperationCenter.ConsumeOrRechargeMoney = -(DecimalOperationCenterMoneyDo - myCountOperationCenter);
                            Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.1上线操作", 46);
                            int intID = my_BLL_b003_TotalCredits_OperationCenter.Add(Model_b003_TotalCredits_OperationCenter);
                        }
                        else
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("0", "6.1操作为OperationCenter0", "intShopuserID=" + intShopuserID);
                        }
                        //}
                        #endregion OperationCenterMoney

                        #region TotalCredits
                        //if (DecimalTotalCreditsDo > 0)
                        //{

                        Decimal myCount_ArgMoney = 0;
                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney(Model_tab_User.ID, out myCount_ArgMoney);

                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge my_BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();

                        if (DecimalTotalCreditsDo > myCount_ArgMoney)
                        {
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalTotalCreditsDo - myCount_ArgMoney;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.1上线操作", 46);
                            int intID = my_BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                        }
                        else if (DecimalTotalCreditsDo < myCount_ArgMoney)
                        {
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = -(DecimalTotalCreditsDo - myCount_ArgMoney);
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.1上线操作", 46);
                            int intID = my_BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                        }
                        else
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("0", "6.1操作为TotalCredits0", "DecimalTotalCreditsDo ShopuserID=" + intShopuserID);
                        }
                        //}
                        #endregion TotalCredits

                        #region Vouchers
                        //if (DecimalTotal_VouchersDo > 0)
                        //{

                        Decimal myCount_Vouchers = 0;
                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Model_tab_User.ID, out myCount_Vouchers);

                        EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge my_BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();

                        if (DecimalTotal_VouchersDo > myCount_Vouchers)
                        {
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = 21;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalTotal_VouchersDo - myCount_Vouchers;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.1上线操作", 46);
                            int intID = my_BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                        }
                        else if (DecimalTotal_VouchersDo < myCount_Vouchers)
                        {
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = 21;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = -(DecimalTotal_VouchersDo - myCount_Vouchers);
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.1上线操作", 46);
                            int intID = my_BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                        }
                        else
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("0", "6.1操作为Vouchers0", "intShopuserID=" + intShopuserID);
                        }
                        //}
                        #endregion Vouchers

                        #region Wealth
                        //if (DecimalTotalWealthDo > 0)
                        //{

                        Decimal myCountuserWealth = 0;
                        Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgWealth(Model_tab_User.ID, out myCountuserWealth);

                        EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                        EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();

                        if (DecimalTotalWealthDo > myCountuserWealth)
                        {
                            Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = true;
                            Model_b006_TotalWealth_OperationUser.UserID = Model_tab_User.ID;
                            Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                            Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalTotalWealthDo - myCountuserWealth;
                            Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.1上线操作", 46);
                            int intID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

                        }
                        else if (DecimalTotalWealthDo < myCountuserWealth)
                        {
                            Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                            Model_b006_TotalWealth_OperationUser.UserID = Model_tab_User.ID;
                            Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                            Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = -(DecimalTotalWealthDo - myCountuserWealth);
                            Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("平台6.1上线操作", 46);
                            int intID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
                        }
                        else
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog("0", "6.1操作为Wealth0", "intShopuserID=" + intShopuserID);
                        }
                        //}
                        #endregion Wealth



                    }
                    else
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog("0", "6.1操作 找不到用户", "intShopuserID=" + intShopuserID);
                    }

                }
            }
            catch (Exception eeee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "执行最终结算", "程序报错");
            }
        }


        private void IteOrder()
        {
            //11055.73   5527.86   16583.59 

            string strUserID = "43112,43114,43119,43209,43149,43134,43110";
            string strActiverNum = "2,1,3,2,1,1,1";
            string strTodayGiveMoney = "18.83,9.41,28.24,18.83,9.41,9.41,9.41";
            decimal decimalEach = (Decimal)32.21;
            String[] strUserIDList = strUserID.Split(',');
            String[] strstrActiverNumList = strActiverNum.Split(',');
            String[] strstrTodayGiveMoneyList = strTodayGiveMoney.Split(',');
            decimal DecimalWillAddTotal = 0;

            for (int i = 0; i < strUserIDList.Length; i++)
            {
                int userID = strUserIDList[i].toInt32();
                int ActiverNum = strstrActiverNumList[i].toInt32();
                Decimal DecimalHaveGiven = strstrTodayGiveMoneyList[i].toDecimal();
                Decimal DecimalWillAdd = ActiverNum * decimalEach - DecimalHaveGiven;


                EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = false;
                Model_b006_TotalWealth_OperationUser.UserID = userID;
                Model_b006_TotalWealth_OperationUser.ShopClient_ID = 21;
                Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalWillAdd;
                Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("财富积分补录0603", 46);
                int intID = BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

                if (intID > 0)
                {
                    EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=21", userID);
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() - DecimalWillAdd;
                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge my_BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = userID;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = 21;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalWillAdd;
                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("财富积分补录0603", 46);
                    intID = my_BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                    DecimalWillAddTotal += DecimalWillAdd;
                }



            }



        }
    }
}
