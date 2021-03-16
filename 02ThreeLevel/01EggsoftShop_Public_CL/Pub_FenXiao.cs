using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;
using Eggsoft.Common;

namespace Eggsoft_Public_CL
{

    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class Pub_FenXiao
    {
        public Pub_FenXiao()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        public static void FileUploadRedJPGCheck(int pub_Int_ShopClientID)
        {
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);


            string strCheckOnceFile = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + "" + Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/redWallet0_Share0.jpg";
            if (Eggsoft.Common.FileFolder.RemoteFileExists(strCheckOnceFile) == false)
            {
                string urlasmx = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/WS_Redwallet.asmx";
                string[] args = new string[1];
                args[0] = pub_Int_ShopClientID.ToString();// "/UpLoad/images/";
                WebServiceHelper.WsCaller.InvokeWebService(urlasmx, "WMeImage_GetWallet", args);
                //string strresult = result.ToString();
            }
        }



        public static void Write_This_Record_ShopClient(int intShopClientID, int intParentID, int intUserID)
        {
            //String strPriParentID = Request.QueryString["ParentID"];
            try
            {
                EggsoftWX.BLL.tab_User_ShopClient_History my_BLL_tab_User_ShopClient_History = new EggsoftWX.BLL.tab_User_ShopClient_History();
                EggsoftWX.Model.tab_User_ShopClient_History my_Model_tab_User_ShopClient_History = new EggsoftWX.Model.tab_User_ShopClient_History();


                String strmy_BLL_tab_User_ShopClient_History = "UserID=" + intUserID + " and ShopClientID=" + intShopClientID + "" + " and Parent_UserID=" + intParentID;

                if (my_BLL_tab_User_ShopClient_History.Exists(strmy_BLL_tab_User_ShopClient_History))//重复访问  
                {
                    my_Model_tab_User_ShopClient_History = my_BLL_tab_User_ShopClient_History.GetModel(strmy_BLL_tab_User_ShopClient_History);
                    my_Model_tab_User_ShopClient_History.Count_Visit = my_Model_tab_User_ShopClient_History.Count_Visit + 1;
                    my_Model_tab_User_ShopClient_History.UpdateTime = DateTime.Now;
                    my_BLL_tab_User_ShopClient_History.Update(my_Model_tab_User_ShopClient_History);
                }
                else
                {


                    //写入分享数据
                    my_Model_tab_User_ShopClient_History.UserID = intUserID;
                    my_Model_tab_User_ShopClient_History.Parent_UserID = intParentID;

                    my_Model_tab_User_ShopClient_History.ShopClientID = intShopClientID;
                    my_Model_tab_User_ShopClient_History.UpdateTime = DateTime.Now;
                    my_Model_tab_User_ShopClient_History.Count_Visit = 1;
                    my_Model_tab_User_ShopClient_History.Type_Visit = "Visit";
                    my_BLL_tab_User_ShopClient_History.Add(my_Model_tab_User_ShopClient_History);


                    if (intParentID > 0)
                    {
                        EggsoftWX.BLL.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_bll = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                        EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = tab_ShopClient_ShopPar_bll.GetModel("ShopClientID=" + intShopClientID);
                        if (tab_ShopClient_ShopPar_Model != null)
                        {
                            if (Decimal.Round(tab_ShopClient_ShopPar_Model.ShopShareGiveMoney.toDecimal(), 2) > 0)
                            {
                                ///赠送一个钱
                                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = tab_ShopClient_ShopPar_Model.ShopShareGiveMoney;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享微店送";
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = intParentID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 40;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intShopClientID;
                                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                #region 增加账户余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加账户余额未处理信息 

                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享微店送" + Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.ShopShareGiveMoney.toDecimal()) + "现金");

                            }
                            if (Decimal.Round(tab_ShopClient_ShopPar_Model.ShopShareGiveVouchers.toDecimal(), 2) > 0)
                            {
                                ///赠送购物券
                                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = tab_ShopClient_ShopPar_Model.ShopShareGiveVouchers;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享微店送";
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = intParentID;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intShopClientID;
                                int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                                #region 增加购物券未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                                Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加购物券未处理信息 

                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享微店送" + Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.ShopShareGiveVouchers.toDecimal()) + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intShopClientID.ToString()) + "");

                            }
                        }

                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }


        public static void IFOver7_Default_U_GetGoods()
        {
            string strWhere = "PayStatus=1 and isReceipt=0 and DeliveryText<>\'\'  ";
            strWhere += " and datediff(d,PayDateTime,getdate())> 7";
            EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();
            bll_tab_Order.Update("isReceipt=1", strWhere);

        }



        public static String Get7DayS90Percent(int intArgUserID)
        {
            IFOver7_Default_U_GetGoods();
            #region over 7 day
            //DecimalTotalMoneyCount = 0;
            Decimal DecimalFenXiaoMoneyCount = 0;

            // intstrCountCount = 0;
            EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();

            string strWhere = "ShopClient_ID=" + intArgUserID + " and PayStatus=1 and isReceipt=1 and DeliveryText<>\'\'  ";
            strWhere += " and datediff(d,PayDateTime,getdate())> 7";
            if (bll_tab_Order.ExistsCount(strWhere) > 0)
            {
                strWhere += " order by id desc";

                System.Data.DataTable myOrderDataTable = bll_tab_Order.GetList(strWhere).Tables[0];
                //  intstrCountCount = myOrderDataTable.Rows.Count;

                string strOrderNumList = "";
                for (int i = 0; i < myOrderDataTable.Rows.Count; i++)
                {
                    string strOrder_ID = myOrderDataTable.Rows[i]["id"].ToString();

                    EggsoftWX.BLL.View_SalesGoods bll_View_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();
                    //Eggsoft.Common.JsUtil.ShowMsg("OrderID='" + strOrder_ID);
                    if (i < myOrderDataTable.Rows.Count - 1)
                    {
                        strOrderNumList += strOrder_ID + ",";
                    }
                    else
                    {
                        strOrderNumList += strOrder_ID;
                    }
                    System.Data.DataTable myDataTable = bll_View_SalesGoods.GetList("*", "OrderID='" + strOrder_ID + "' order by ID_Orderdetails asc").Tables[0];
                    for (int inti = 0; inti < myDataTable.Rows.Count; inti++)
                    {

                        String strGoodPrice = myDataTable.Rows[inti]["GoodPrice"].ToString();
                        String strOrderCount = (myDataTable.Rows[inti]["OrderCount"].ToString());
                        #region


                        #endregion

                    }


                }
                //Label_Over7Days.Text = "已完成订单数量 <a href=\"/ClientAdmin/1919tab_Order/tab_Order_Board_Wait_Finished.aspx\"><font color=blue>" + intstrCountCount + "</font></a>   ,总金额：¥" + Pub.getPubMoney(DecimalFenXiaoMoneyCount) + ",分销所得(90%):¥" + Pub.getPubMoney(DecimalFenXiaoMoneyCount) + ",";
                //Label_Over7Days.Text += "订单号列表：" + strOrderNumList;
            }
            #endregion

            return Pub.getPubMoney(DecimalFenXiaoMoneyCount);

        }
        /// <summary>
        /// ,,,, 分割 用于 createby  updateby  等字段
        /// </summary>
        /// <param name="strUpdatebyUsername"></param>
        /// <param name="intShopClientID"></param>
        /// <returns></returns>
        public static string getAllMySonAdminPowerList(string strUpdatebyUsername, Int32? intShopClientID)
        {
            string strAllAdinData_DataTable = "";
            string CacheKey = strUpdatebyUsername + ".AllMySonAdminPowerList";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    EggsoftWX.BLL.tab_ShopClient_AdminUser BLL_tab_ShopClient_AdminUser = new EggsoftWX.BLL.tab_ShopClient_AdminUser();

                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    bool boolShowAdmin = BLL_tab_ShopClient.Exists("Username=@Username and ID=@ID", strUpdatebyUsername, intShopClientID);
                    if (boolShowAdmin)///有所有权限
                    {
                        System.Data.DataTable Data_DataTable = BLL_tab_ShopClient_AdminUser.GetList("ShopClientAdmin", "isnull(isdeleted,0)=0 and ShopClientID=" + intShopClientID).Tables[0];
                        for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                        {
                            strAllAdinData_DataTable += "" + Data_DataTable.Rows[i]["ShopClientAdmin"].toString() + ",";
                        }
                    }
                    else
                    {
                        System.Data.DataTable Data_EnterpriseOrganizationIDDataTable = BLL_tab_ShopClient_AdminUser.GetList("EnterpriseOrganizationID", "isnull(isdeleted,0)=0 and ShopClientID=" + intShopClientID + " and ShopClientAdmin=@ShopClientAdmin", strUpdatebyUsername).Tables[0];
                        if (Data_EnterpriseOrganizationIDDataTable.Rows.Count > 0)
                        {
                            Int32? Int32EnterpriseOrganizationID = Data_EnterpriseOrganizationIDDataTable.Rows[0]["EnterpriseOrganizationID"].toInt32();
                            if (Int32EnterpriseOrganizationID < 0)
                            {
                                strAllAdinData_DataTable = strUpdatebyUsername;///负数只能本人
                            }
                            else
                            {
                                string strAddOrganizationID = getAllMySonAdminPowerListRepeat(Int32EnterpriseOrganizationID, intShopClientID, "");
                                if (strAddOrganizationID.Substring(strAddOrganizationID.Length - 1, 1) == ",") strAddOrganizationID = strAddOrganizationID.Substring(0, strAddOrganizationID.Length - 1);
                                System.Data.DataTable Data_DataTable = BLL_tab_ShopClient_AdminUser.GetList("ShopClientAdmin", "isnull(isdeleted,0)=0 and ShopClientID=" + intShopClientID + " and EnterpriseOrganizationID in (" + strAddOrganizationID + ")").Tables[0];
                                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                                {
                                    strAllAdinData_DataTable += "" + Data_DataTable.Rows[i]["ShopClientAdmin"].toString() + ",";
                                }
                            }
                        }
                    }
                    if (strAllAdinData_DataTable.Substring(strAllAdinData_DataTable.Length - 1, 1) == ",") strAllAdinData_DataTable = strAllAdinData_DataTable.Substring(0, strAllAdinData_DataTable.Length - 1);

                    Eggsoft.Common.DataCache.SetCache(CacheKey, strAllAdinData_DataTable);// 写入缓存
                }
                catch (Exception ddd)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(ddd, "updateby权限列表", "程序报错");
                }
            }
            else
            {
                strAllAdinData_DataTable = objType.toString();
            }
            return strAllAdinData_DataTable;
        }

        public static string getAllMySonAdminPowerListRepeat(Int32? intParent, Int32? intShopClientID, string strAddOrganizationID)
        {
            EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization BLL_tab_ShopClient_EnterpriseOrganization = new EggsoftWX.BLL.tab_ShopClient_EnterpriseOrganization();


            string strParentID = "";
            strParentID = "ParentID=" + intParent;


            System.Data.DataTable Data_EnterpriseOrganizationIDDataTable = BLL_tab_ShopClient_EnterpriseOrganization.GetList("ID", "isnull(isdeleted,0)=0 and ShopClientID=" + intShopClientID + " and " + strParentID).Tables[0];
            for (int i = 0; i < Data_EnterpriseOrganizationIDDataTable.Rows.Count; i++)
            {
                Int32 Int32ID = Data_EnterpriseOrganizationIDDataTable.Rows[i]["ID"].toInt32();

                strAddOrganizationID += getAllMySonAdminPowerListRepeat(Int32ID, intShopClientID, strAddOrganizationID);
            }
            return strAddOrganizationID += -intParent + "," + intParent + ",";
        }
        /// <summary>
        /// 用户的现金
        /// </summary>
        /// <param name="intArgUserID"></param>
        /// <param name="myargTotalCredits"></param>
        public static void Do_CountyuEArgMoney(int intArgUserID, out Decimal myargTotalCredits)
        {
            Decimal CountyuEArgMoeny = 0;

            try
            {
                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();

                //EggsoftWX.Model.tab_Goods Modeltab_Goods = new EggsoftWX.Model.tab_Goods();
                string maxID = "0";
                if (BLL_tab_TotalCredits_Consume_Or_Recharge.Exists("UserID=" + intArgUserID))
                {
                    maxID = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList("select max(id) as maxID from tab_TotalCredits_Consume_Or_Recharge where UserID=" + intArgUserID).Tables[0].Rows[0]["maxID"].ToString();
                }
                string maxIDMoney = "0";
                if (BLL_tab_TotalCredits_Consume_Or_Recharge.Exists("ID=" + maxID))
                {
                    maxIDMoney = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList("select RemainingSum from tab_TotalCredits_Consume_Or_Recharge where ID=" + maxID).Tables[0].Rows[0]["RemainingSum"].ToString();
                }
                Decimal.TryParse(maxIDMoney, out CountyuEArgMoeny);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            myargTotalCredits = CountyuEArgMoeny;

        }
        /// <summary>
        /// 用户的购物券 
        /// </summary>
        /// <param name="intArgUserID"></param>
        /// <param name="myargTotal_Vouchers"></param>
        public static void Do_CountyuEArgMoney_Vouchers(int intArgUserID, out Decimal myargTotal_Vouchers)
        {
            Decimal CountyuEArgMoeny_Vouchers = 0;

            try
            {
                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                string maxID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.SelectList("select max(id) as maxID from tab_Total_Vouchers_Consume_Or_Recharge where UserID=" + intArgUserID).Tables[0].Rows[0]["maxID"].ToString();
                if (String.IsNullOrEmpty(maxID) == false)
                {
                    string maxIDMoney = BLL_tab_Total_Vouchers_Consume_Or_Recharge.SelectList("select RemainingSum_Vouchers from tab_Total_Vouchers_Consume_Or_Recharge where ID=" + maxID).Tables[0].Rows[0]["RemainingSum_Vouchers"].ToString();
                    Decimal.TryParse(maxIDMoney, out CountyuEArgMoeny_Vouchers);
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            myargTotal_Vouchers = CountyuEArgMoeny_Vouchers;

        }


        public static void Do_CountyuEArgBeans(int intArgUserID, out long myargBeans)
        {
            long CountyuEArgBeans = 0;

            try
            {
                EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge BLL_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge();



                string maxID = BLL_tab_TotalBeans_Consume_Or_Recharge.SelectList("select max(id) as maxID from tab_TotalBeans_Consume_Or_Recharge where UserID=" + intArgUserID).Tables[0].Rows[0]["maxID"].ToString();
                string maxIDMoney = BLL_tab_TotalBeans_Consume_Or_Recharge.SelectList("select RemainingSumBean from tab_TotalBeans_Consume_Or_Recharge where ID=" + maxID).Tables[0].Rows[0]["RemainingSumBean"].ToString();
                Int64.TryParse(maxIDMoney, out CountyuEArgBeans);

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            myargBeans = CountyuEArgBeans;

        }

        /// <summary>
        /// 统计用户的财富
        /// </summary>
        /// <param name="intArgUserID"></param>
        /// <param name="myargTotalWealth"></param>
        public static void Do_CountyuEArgWealth(int intArgUserID, out Decimal myargTotalWealth)
        {
            Decimal CountyuEArgWealth = 0;

            try
            {
                EggsoftWX.BLL.b006_TotalWealth_OperationUser BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();

                string maxID = "0";
                if (BLL_b006_TotalWealth_OperationUser.Exists("UserID=" + intArgUserID))
                {
                    maxID = BLL_b006_TotalWealth_OperationUser.SelectList("select max(id) as maxID from b006_TotalWealth_OperationUser where UserID=" + intArgUserID).Tables[0].Rows[0]["maxID"].ToString();
                }
                string maxIDMoney = "0";
                if (BLL_b006_TotalWealth_OperationUser.Exists("ID=" + maxID))
                {
                    maxIDMoney = BLL_b006_TotalWealth_OperationUser.SelectList("select RemainingSum from b006_TotalWealth_OperationUser where ID=" + maxID).Tables[0].Rows[0]["RemainingSum"].ToString();
                }
                Decimal.TryParse(maxIDMoney, out CountyuEArgWealth);

                #region 取活动订单表中较小的值
                string strSQL = "select sum(ReturnMoneyUnit) from b008_OpterationUserActiveReturnMoneyOrderNum where UserID=@UserID and OrderDetailID is not null";
                Decimal DecimalReturnMoneyUnit = BLL_b006_TotalWealth_OperationUser.SelectList(strSQL, intArgUserID).Tables[0].Rows[0][0].toDecimal();
                #endregion 取活动订单表中较小的值
                CountyuEArgWealth = CountyuEArgWealth > DecimalReturnMoneyUnit ? DecimalReturnMoneyUnit : CountyuEArgWealth;

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "统计用户的财富");
            }
            finally
            {

            }
            myargTotalWealth = CountyuEArgWealth;

        }
        /// <summary>
        /// 统计运营中心用户的财富
        /// </summary>
        /// <param name="intArgUserID"></param>
        /// <param name="myargTotalWealth"></param>
        public static void Do_CountyuETotalCredits_OperationCenter(int intArgCenterUserID, out Decimal myargTotalCredits_OperationCenterWealth)
        {
            Decimal CountyuEArgWealth = 0;

            try
            {
                EggsoftWX.BLL.b003_TotalCredits_OperationCenter BLL_b003_TotalCredits_OperationCenter = new EggsoftWX.BLL.b003_TotalCredits_OperationCenter();

                //EggsoftWX.Model.tab_Goods Modeltab_Goods = new EggsoftWX.Model.tab_Goods();
                string maxID = "0";
                if (BLL_b003_TotalCredits_OperationCenter.Exists("UserID=" + intArgCenterUserID))
                {
                    maxID = BLL_b003_TotalCredits_OperationCenter.SelectList("select max(id) as maxID from b003_TotalCredits_OperationCenter where UserID=" + intArgCenterUserID).Tables[0].Rows[0]["maxID"].ToString();
                }
                string maxIDMoney = "0";
                if (BLL_b003_TotalCredits_OperationCenter.Exists("ID=" + maxID))
                {
                    maxIDMoney = BLL_b003_TotalCredits_OperationCenter.SelectList("select RemainingSum from b003_TotalCredits_OperationCenter where ID=" + maxID).Tables[0].Rows[0]["RemainingSum"].ToString();
                }
                Decimal.TryParse(maxIDMoney, out CountyuEArgWealth);

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            myargTotalCredits_OperationCenterWealth = CountyuEArgWealth;

        }


        public static String Get_Shopping_Vouchers_SList(int intArgUserID, out int outCount_Shopping_Vouchers_, out String stroutMoney_Shopping_Vouchers_)
        {

            EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
            //EggsoftWX.Model.tab_Shopping_Vouchers Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_Shopping_Vouchers();

            String strBody = "";
            strBody += "<li ms-repeat=\"items\">\n";
            strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";

            strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>额度</strong>¥" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>号码</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>描述</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>是否可用</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
            strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\"><strong>有效时间</strong>" + "</div>\n";
            strBody += "				</div>\n";
            strBody += "			</div>\n";
            strBody += "		</li>\n";

            string str_Shopping_Vouchers_Money = BLL_tab_Shopping_Vouchers.GetList("sum(Money) as Money_Vouchers", "UserID=" + intArgUserID).Tables[0].Rows[0][0].ToString();
            Decimal meoutMoney_Shopping_Vouchers_ = 0;
            Decimal.TryParse(str_Shopping_Vouchers_Money, out meoutMoney_Shopping_Vouchers_);
            stroutMoney_Shopping_Vouchers_ = Pub.getPubMoney(meoutMoney_Shopping_Vouchers_);

            System.Data.DataTable myDataTable = BLL_tab_Shopping_Vouchers.GetList("UserID=" + intArgUserID + " order by id").Tables[0];
            outCount_Shopping_Vouchers_ = myDataTable.Rows.Count;
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {

                string strMoney = Pub.getPubMoney(Decimal.Parse(myDataTable.Rows[i]["Money"].ToString()));
                string strVouchersNum = myDataTable.Rows[i]["VouchersNum"].ToString();
                string strVouchers_Title = myDataTable.Rows[i]["Vouchers_Title"].ToString();
                string strVouchers_Des = myDataTable.Rows[i]["Vouchers_Des"].ToString();
                string strValidate = myDataTable.Rows[i]["Validate"].ToString();
                bool boolConsumed = bool.Parse(myDataTable.Rows[i]["Consumed"].ToString());
                Int16 intOrderDetailID = Int16.Parse(myDataTable.Rows[i]["OrderDetailID"].ToString());
                string strValidateStartTime = myDataTable.Rows[i]["ValidateStartTime"].ToString();
                string strValidateEndTime = myDataTable.Rows[i]["ValidateEndTime"].ToString();

                if (boolConsumed == false)
                {
                    DateTime myDateTime = DateTime.Now;

                    if (Boolean.Parse(strValidate) && (myDateTime > DateTime.Parse(strValidateStartTime)) && (myDateTime < DateTime.Parse(strValidateEndTime)))
                    {

                        if ((myDateTime > DateTime.Parse(strValidateStartTime)) && (myDateTime < DateTime.Parse(strValidateEndTime)))
                        {
                            strValidate = "可用";
                        }
                        else
                        {
                            strValidate = "已过期";
                        }
                    }
                    else
                    {
                        strValidate = "不可用";
                    }
                }
                else
                {
                    if (intOrderDetailID > 0)
                    {
                        EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                        EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel(intOrderDetailID);
                        if (Model_tab_Orderdetails != null)
                        {
                            strValidate = "已购" + Model_tab_Orderdetails.GoodName;
                        }
                    }
                    else
                    {
                        strValidate = "在购物车中";
                    }
                }
                strBody += "<li ms-repeat=\"items\">\n";
                strBody += "			<div class=\"ShowQuanBeanMoneythelist_ul_li\">\n";
                strBody += "				<div class=\"ul_li_Classs_10_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">¥" + strMoney + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strVouchersNum + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_40_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strVouchers_Title + " " + strVouchers_Des + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + strValidate + "</div>\n";
                strBody += "				</div>\n";
                strBody += "				<div class=\"ul_li_Classs_15_Percent\">\n";
                strBody += "					<div class=\"ul_li_trade_item_ShowQuanBeanMoney\">" + DateTime.Parse(strValidateStartTime).ToString("yyyy年MM月dd日") + "<br />" + DateTime.Parse(strValidateEndTime).ToString("yyyy年MM月dd日") + "</div>\n";
                strBody += "				</div>\n";
                strBody += "			</div>\n";
                strBody += "		</li>\n";

                #region

                #endregion
            }
            //myArgMoney = CountmyArgMoney;
            return strBody;
        }

        /// <summary>
        /// 分享者A（间接推）% 
        /// </summary>
        public static void DoOver7daysCountMySonMoney_UpdateEvertDay_ShareA(int intShopClientID = 0)
        {
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();

            EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


            EggsoftWX.BLL.b009_EveryGetOrderIDDetailIDWillActiveReturnMoney BLLb009_EveryGetOrderIDDetailIDWillActiveReturnMoney = new EggsoftWX.BLL.b009_EveryGetOrderIDDetailIDWillActiveReturnMoney();
            EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();


            EggsoftWX.BLL.View_SalesGoods BLLView_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();//
            String strYesTodaySQLBody = "(";
            strYesTodaySQLBody += "((DateDiff(dd, PayDateTime, GetDate()) > 6 and GoodType<>6) OR (PayStatus=1 and isReceipt=1 and GoodType<>6 and DeliveryText<>\'\'))";
            strYesTodaySQLBody += " or ((DateDiff(dd, PayDateTime, GetDate()) > 6) and GoodType=6 and DeliveryText<>\'\')";///并且要发货
            strYesTodaySQLBody += " )";
            strYesTodaySQLBody += " and Over7DaysToBeans=0 and IsDeleted=0";
            if (intShopClientID > 0) strYesTodaySQLBody += " and ShopClient_ID=" + intShopClientID;


            //myperationReturnMoneyEveryDayList = new List<perationReturnMoneyEveryDay>();

            System.Data.DataTable myDataTable = BLLView_SalesGoods.GetList("*", " " + strYesTodaySQLBody + " order by ID_Orderdetails desc").Tables[0];
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                string ID_Orderdetails = myDataTable.Rows[i]["ID_Orderdetails"].ToString();
                string Over7DaysToBeans = myDataTable.Rows[i]["Over7DaysToBeans"].ToString();
                bool boolOver7DaysToBeans = Convert.ToBoolean(myDataTable.Rows[i]["Over7DaysToBeans"].ToString());

                string strShopClient_ID = myDataTable.Rows[i]["ShopClient_ID"].ToString();
                string strParentID = myDataTable.Rows[i]["ParentID"].ToString();
                string strGrandParentID = myDataTable.Rows[i]["GrandParentID"].ToString();
                string strGreatParentID = myDataTable.Rows[i]["GreatParentID"].ToString();

                string strUserID = myDataTable.Rows[i]["UserID"].ToString();
                if (ID_Orderdetails == "10001")
                {
                    var ddd = 0;
                }
                string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                string strGoodName = myDataTable.Rows[i]["GoodName"].ToString();
                string strNickName = myDataTable.Rows[i]["NickName"].ToString();
                string strPinglun = myDataTable.Rows[i]["Pinglun"].ToString();
                string strCreatDateTime = myDataTable.Rows[i]["CreatDateTime"].ToString();
                string strPayTimeTime = myDataTable.Rows[i]["PayDateTime"].ToString();
                string strGoodPrice = myDataTable.Rows[i]["GoodPrice"].ToString();
                string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();
                string strGoodType = myDataTable.Rows[i]["GoodType"].ToString();
                string strGoodTypeId = myDataTable.Rows[i]["GoodTypeId"].ToString();///运营中心ID
                string strGoodTypeIdBuyInfo = myDataTable.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                string strOrderID = myDataTable.Rows[i]["OrderID"].ToString();


                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Int32.Parse(strShopClient_ID));
                string strmywebuyURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/mywebuy.aspx";
                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(strShopClient_ID, "TempletVisitMessage");///是否可以发模板消息


                if (strGoodType == "3")///取众筹档位的销售价格
                {
                    EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                    EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Int32.Parse(strGoodTypeIdBuyInfo));
                    strGoodPrice = Convert.ToDecimal(Model_tab_ZC_01Product_Support.SalesPrice).ToString();
                }
                else if (strGoodType == "2")///取团购的销售价格
                {
                    EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                    EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(Int32.Parse(strGoodTypeId));
                    strGoodPrice = Convert.ToDecimal(Model_tab_TuanGou.EachPeoplePrice).ToString();
                }

                decimal decimaAllMoney = decimal.Multiply(decimal.Parse(strGoodPrice), decimal.Parse(strOrderCount));
                //int intstrParentID = 0;
                //int.TryParse(strParentID, out intstrParentID);
                //#region 天使的自己不享受享受收入
                //if ((intstrParentID == strUserID.toInt32()) && (BLL_tab_ShopClient_Agent_.Exists("UserID=" + intstrParentID + " and OnlyIsAngel=1 and IsDeleted=0 ")))
                //{
                //    intstrParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intstrParentID);///天使的父亲不要是自己。天使本人不享受分销提成
                //}
                //#endregion 天使的自己不享受享受收入

                //int.TryParse(strParentID, out intstrParentID);
                //Decimal AgentGet = 0;
                //Decimal ManagerAgentGet = 0;
                //int ManagerAgentParentID = 0;
                //Decimal ManagerGrandAgentGet = 0;
                //int ManagerGrandAgentParentID = 0;
                //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(Int32.Parse(strGoodID), intstrParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, strGoodType, strGoodTypeId, strGoodTypeIdBuyInfo);

                Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                myModel_MultiFenXiaoLevel.strGoodType = strGoodType;
                myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                myModel_MultiFenXiaoLevel.UserID = strUserID.toInt32();
                Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);


                #region  计算分销所得  代理 所得  或者团长所得  运营中心
                #region 奖励团购  运营中心的
                bool boolTuanZhangBonus_AgentGet = false; bool boolOperationCenterBonus = false; Decimal DecimalAgentPrice = 0;
                string strTuanZhangIFFinshedCurMemberShipID = "0"; string strTuanZhangUserID = "0";
                if (strGoodType == "2")///团购
                {


                }
                else if (strGoodType == "6")///运营中心的
                {
                    boolOperationCenterBonus = true;
                    Decimal ALLDecimalMoney = strGoodPrice.toDecimal() * strOrderCount.toDecimal();
                    EggsoftWX.BLL.b004_OperationGoods my_BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                    EggsoftWX.Model.b004_OperationGoods my_Model_BLL_b004_OperationGoods = my_BLL_b004_OperationGoods.GetModel(strGoodTypeIdBuyInfo.toInt32());
                    EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(strGoodID));



                    #region 结算普通用户裂变所得
                    myModel_MultiFenXiaoLevel.ManagerAgentParentID = strGrandParentID.toInt32();
                    int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(myModel_MultiFenXiaoLevel.ManagerAgentParentID);
                    if (myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0)
                    {
                        Decimal DecimalMoneyReturnMoneyShareA = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyShareA.toDecimal());
                        DecimalMoneyReturnMoneyShareA = decimal.Multiply(DecimalMoneyReturnMoneyShareA, (decimal)0.01);
                        if (DecimalMoneyReturnMoneyShareA >= (Decimal)0.01)
                        {
                            if (intIF_Agent_From_Database_ == 3)
                            {
                                Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalMoneyReturnMoneyShareA;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",二级天使" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  T+7转化收入", 46);
                                int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                                #region 增加购物券未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加购物券未处理信息

                            }
                            else
                            {
                                Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 10;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalMoneyReturnMoneyShareA;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",二级代理" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  T+7转化收入", 46);
                                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                #region 增加现金余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加未处理信息增加现金余额未处理信息

                            }
                        }
                    }
                    #endregion 结算普通用户裂变所得    
                }
                #endregion 奖励团购

                #endregion

                BLLtab_Orderdetails.Update("Over7DaysToBeans=1,UpdateDateTime=getdate()", "id=" + ID_Orderdetails);
            }

        }

        /// <summary>
        /// 给各个用户发钱 支付 超过7  或者 马上确认收货的 分销的结算一下  并且Over7DaysToBeans=1  如果是运营中心 必须是支付超过7天的 并且设置过发货的才结算
        /// </summary>
        public static void DoOver7daysCountMySonMoney_UpdateEvertDay(int intShopClientID = 0, Int32 intOrderID = 0)
        {
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();

            EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


            EggsoftWX.BLL.b009_EveryGetOrderIDDetailIDWillActiveReturnMoney BLLb009_EveryGetOrderIDDetailIDWillActiveReturnMoney = new EggsoftWX.BLL.b009_EveryGetOrderIDDetailIDWillActiveReturnMoney();
            EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();


            string strView_SalesGoodsSQL = @"SELECT   tab_Orderdetails.OrderCount, tab_Order.ID AS OrderID, tab_Order.PayStatus, tab_Orderdetails.GoodID, 
                tab_Order.UserID, tab_User.NickName, tab_Order.OrderNum, tab_Goods.PromotePrice, 
                tab_Orderdetails.GoodPrice, tab_Goods.Name AS GoodName, tab_Orderdetails.ID AS ID_Orderdetails, 
                tab_Orderdetails.Pinglun, tab_Orderdetails.CreatDateTime, tab_Order.PayDateTime, 
                tab_Orderdetails.ParentID, tab_Orderdetails.Over7DaysToBeans, tab_Goods.IsDeleted, 
                tab_Order.TotalMoney, tab_Goods.IS_Admin_check, 
                tab_ShopClient_Agent__ProductClassID.Empowered AS ParentID_Empowered, 
                tab_ShopClient_Agent_.Empowered AS GrandParentID_Empowered, tab_Order.DeliveryText,
                tab_Order.isReceipt, tab_Orderdetails.GrandParentID, tab_Orderdetails.GreatParentID, 
                tab_Order.UpdateDateTime, tab_Order.ShopClient_ID, tab_Goods.Send_Money_IfBuy, 
                tab_Goods.Send_Vouchers_IfBuy, tab_Orderdetails.GoodType, tab_Orderdetails.GoodTypeId, 
                tab_Orderdetails.GoodTypeIdBuyInfo, tab_Orderdetails.TeamID
FROM      tab_ShopClient_Agent_ RIGHT OUTER JOIN
                tab_ShopClient_Agent__ProductClassID ON 
                tab_ShopClient_Agent_.UserID = tab_ShopClient_Agent__ProductClassID.UserID RIGHT OUTER JOIN
                tab_Order INNER JOIN
                tab_Orderdetails ON tab_Order.ID = tab_Orderdetails.OrderID INNER JOIN
                tab_Goods ON tab_Orderdetails.GoodID = tab_Goods.ID INNER JOIN
                tab_User ON tab_Order.UserID = tab_User.ID ON 
                tab_ShopClient_Agent__ProductClassID.UserID = tab_Order.UserID AND 
                tab_ShopClient_Agent__ProductClassID.ProductID = tab_Orderdetails.GoodID
WHERE   (tab_Order.PayStatus = 1) AND (tab_Goods.IsDeleted = 0) AND (tab_Orderdetails.isdeleted = 0)";
            strView_SalesGoodsSQL += " and tab_Orderdetails.Over7DaysToBeans=0 and tab_Goods.IsDeleted=0 ";
            strView_SalesGoodsSQL += "  and tab_Orderdetails.Over7DaysToBeans=0 and tab_Goods.IsDeleted=0 ";

            String strYesTodaySQLBody = " and (";
            strYesTodaySQLBody += "((DateDiff(dd, tab_Order.PayDateTime, GetDate()) > 6 and tab_Orderdetails.GoodType<>6) OR (tab_Order.PayStatus=1 and tab_Order.isReceipt=1 and tab_Orderdetails.GoodType<>6 and tab_Order.DeliveryText<>\'\'))";
            strYesTodaySQLBody += " or ((DateDiff(dd, PayDateTime, GetDate()) > 6) and tab_Orderdetails.GoodType=6 and tab_Order.DeliveryText<>\'\')";///并且要发货
            strYesTodaySQLBody += " )";
            strView_SalesGoodsSQL += strYesTodaySQLBody;
            if (intShopClientID > 0) strView_SalesGoodsSQL += " and tab_Order.ShopClient_ID=" + intShopClientID;
            if (intOrderID > 0) strView_SalesGoodsSQL += " and tab_Order.ID=" + intOrderID;
            strView_SalesGoodsSQL += " order by ID_Orderdetails desc";

            //EggsoftWX.BLL.View_SalesGoods BLLView_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();//
            //String strYesTodaySQLBody = "(";
            //strYesTodaySQLBody += "((DateDiff(dd, PayDateTime, GetDate()) > 6 and GoodType<>6) OR (PayStatus=1 and isReceipt=1 and GoodType<>6 and DeliveryText<>\'\'))";
            //strYesTodaySQLBody += " or ((DateDiff(dd, PayDateTime, GetDate()) > 6) and GoodType=6 and DeliveryText<>\'\')";///并且要发货
            //strYesTodaySQLBody += " )";
            //strYesTodaySQLBody += " and Over7DaysToBeans=0 and IsDeleted=0";
            //if (intShopClientID > 0) strYesTodaySQLBody += " and ShopClient_ID=" + intShopClientID;
            //if (intOrderID > 0) strYesTodaySQLBody += " and OrderID=" + intOrderID;

            #region 用户重新购买了 消除以前的警告提示 （需要继续下单的警告提示）
            EggsoftWX.BLL.tab_User_Message_NeedShow BLL_tab_User_Message_NeedShow = new EggsoftWX.BLL.tab_User_Message_NeedShow();
            EggsoftWX.Model.tab_User_Message_NeedShow Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
            #endregion 用户重新购买了 消除以前的警告提示 （需要继续下单的警告提示）
            //myperationReturnMoneyEveryDayList = new List<perationReturnMoneyEveryDay>();
            //System.Data.DataTable myDataTable = BLLView_SalesGoods.GetList("*", " " + strYesTodaySQLBody + " order by ID_Orderdetails desc").Tables[0];

            System.Data.DataTable myDataTable = my_BLL_tab_Goods.SelectList(strView_SalesGoodsSQL).Tables[0];
            for (int i = 0; i < myDataTable.Rows.Count; i++)
            {
                string ID_Orderdetails = myDataTable.Rows[i]["ID_Orderdetails"].ToString();
                string Over7DaysToBeans = myDataTable.Rows[i]["Over7DaysToBeans"].ToString();
                bool boolOver7DaysToBeans = Convert.ToBoolean(myDataTable.Rows[i]["Over7DaysToBeans"].ToString());
                string strShopClient_ID = myDataTable.Rows[i]["ShopClient_ID"].ToString();
                string strParentID = myDataTable.Rows[i]["ParentID"].ToString();
                string strGrandParentID = myDataTable.Rows[i]["GrandParentID"].ToString();
                string strGreatParentID = myDataTable.Rows[i]["GreatParentID"].ToString();
                string strTeamID = myDataTable.Rows[i]["TeamID"].ToString();

                string strUserID = myDataTable.Rows[i]["UserID"].ToString();
                if (strUserID == "44266")
                {
                    var ddd = 0;
                }
                string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                string strGoodName = myDataTable.Rows[i]["GoodName"].ToString();
                string strNickName = myDataTable.Rows[i]["NickName"].ToString();
                string strPinglun = myDataTable.Rows[i]["Pinglun"].ToString();
                string strCreatDateTime = myDataTable.Rows[i]["CreatDateTime"].ToString();
                string strPayTimeTime = myDataTable.Rows[i]["PayDateTime"].ToString();
                string strGoodPrice = myDataTable.Rows[i]["GoodPrice"].ToString();
                string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();
                string strGoodType = myDataTable.Rows[i]["GoodType"].ToString();
                string strGoodTypeId = myDataTable.Rows[i]["GoodTypeId"].ToString();///运营中心ID
                string strGoodTypeIdBuyInfo = myDataTable.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                string strOrderID = myDataTable.Rows[i]["OrderID"].ToString();


                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Int32.Parse(strShopClient_ID));
                string strmywebuyURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/mywebuy.aspx";
                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(strShopClient_ID, "TempletVisitMessage");///是否可以发模板消息


                if (strGoodType == "3")///取众筹档位的销售价格
                {
                    EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                    EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Int32.Parse(strGoodTypeIdBuyInfo));
                    strGoodPrice = Convert.ToDecimal(Model_tab_ZC_01Product_Support.SalesPrice).ToString();
                }
                else if (strGoodType == "2")///取团购的销售价格
                {
                    EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                    EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(Int32.Parse(strGoodTypeId));
                    strGoodPrice = Convert.ToDecimal(Model_tab_TuanGou.EachPeoplePrice).ToString();
                }

                decimal decimaAllMoney = decimal.Multiply(decimal.Parse(strGoodPrice), decimal.Parse(strOrderCount));
                //int intstrParentID = 0;
                //int.TryParse(strParentID, out intstrParentID);
                //#region 天使的自己不享受享受收入
                //if ((intstrParentID == strUserID.toInt32()) && (BLL_tab_ShopClient_Agent_.Exists("UserID=" + intstrParentID + " and OnlyIsAngel=1 and IsDeleted=0 ")))
                //{
                //    intstrParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intstrParentID);///天使的父亲不要是自己。天使本人不享受分销提成
                //}
                //#endregion 天使的自己不享受享受收入

                //int.TryParse(strParentID, out intstrParentID);
                //Decimal AgentGet = 0;
                //Decimal ManagerAgentGet = 0;
                //int ManagerAgentParentID = 0;
                //Decimal ManagerGrandAgentGet = 0;
                //int ManagerGrandAgentParentID = 0;
                //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(Int32.Parse(strGoodID), intstrParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, strGoodType, strGoodTypeId, strGoodTypeIdBuyInfo);
                Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                myModel_MultiFenXiaoLevel.UserID = strUserID.toInt32();
                myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
                myModel_MultiFenXiaoLevel.strGoodType = strGoodType;
                myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel, true);
                myModel_MultiFenXiaoLevel.FenXiaoLevelLength = (myModel_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (myModel_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (myModel_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();


                #region  计算普通分销所得  代理 所得  或者团长所得  运营中心 //////常见的分销  就是这里了  老张 都这里找
                #region 奖励团购  运营中心的
                bool boolTuanZhangBonus_AgentGet = false; bool boolOperationCenterBonus = false; Decimal DecimalAgentPrice = 0;
                string strTuanZhangIFFinshedCurMemberShipID = "0"; string strTuanZhangUserID = "0";
                if (strGoodType == "2")///团购
                {
                    #region 团购处理
                    EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                    EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(Int32.Parse(strGoodTypeId));
                    boolTuanZhangBonus_AgentGet = Convert.ToBoolean(my_Model_tab_TuanGou.TuanZhangBonus_AgentGet);
                    DecimalAgentPrice = Convert.ToDecimal(my_Model_tab_TuanGou.AgentPrice);

                    string strtab_TuanGou_NumberIFFinshedCurMemberShipID = "select IFFinshedCurMemberShip from tab_TuanGou_Number where ID=" + strGoodTypeIdBuyInfo + " and IFFinshedCurMemberShip=1 and ShopClientID=" + strShopClient_ID;
                    System.Data.DataTable DataTableGetTuanIFFinshedCurMemberShipID = my_BLL_tab_TuanGou.SelectList(strtab_TuanGou_NumberIFFinshedCurMemberShipID).Tables[0];
                    if (DataTableGetTuanIFFinshedCurMemberShipID.Rows.Count > 0)
                    {
                        strTuanZhangIFFinshedCurMemberShipID = DataTableGetTuanIFFinshedCurMemberShipID.Rows[0]["IFFinshedCurMemberShip"].ToString();
                    }
                    string strGetTuanZhangUserID = "select UserID from tab_TuanGou_Partner where TuanGouIDNumber=" + strGoodTypeIdBuyInfo + " and ParterRole=1 and ShopClientID=" + strShopClient_ID;
                    System.Data.DataTable DataTableGetTuanZhangUserID = my_BLL_tab_TuanGou.SelectList(strGetTuanZhangUserID).Tables[0];
                    if (DataTableGetTuanZhangUserID.Rows.Count > 0)
                    {
                        strTuanZhangUserID = DataTableGetTuanZhangUserID.Rows[0]["UserID"].ToString();
                    }
                    #region 团长奖励
                    if (strTuanZhangIFFinshedCurMemberShipID == "1")////可以奖励所得
                    {
                        if (strTuanZhangUserID != "0" && ((my_Model_tab_TuanGou.TuanZhangBonus_Money) > 0))
                        {
                            string strNeedAddString = Eggsoft.Common.CommUtil.getShortText("团长奖励团购单号(" + strGoodTypeIdBuyInfo + ") 团ID=" + my_Model_tab_TuanGou.ID + " " + strGoodName + "  组团成功转化收入", 46);
                            if (Decimal.Round(Convert.ToDecimal(my_Model_tab_TuanGou.TuanZhangBonus_Money), 2) > 0)
                            {
                                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_SendTuangzhang_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                if (!BLL_SendTuangzhang_tab_TotalCredits_Consume_Or_Recharge.Exists("UserID=" + strTuanZhangUserID + " and ConsumeOrRechargeMoney='" + strNeedAddString + "'"))
                                {
                                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                    Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                    Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 13;
                                    Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge.UserID = Int32.Parse(strTuanZhangUserID);
                                    Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                    Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Convert.ToDecimal(my_Model_tab_TuanGou.TuanZhangBonus_Money);
                                    Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = strNeedAddString;
                                    int intTableID = BLL_SendTuangzhang_tab_TotalCredits_Consume_Or_Recharge.Add(Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge);

                                    #region 增加现金余额未处理信息
                                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                    Model_b011_InfoAlertMessage.InfoTip = Model_SendTuanZhang_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                    Model_b011_InfoAlertMessage.CreateBy = strNeedAddString;
                                    Model_b011_InfoAlertMessage.UpdateBy = strNeedAddString;
                                    Model_b011_InfoAlertMessage.UserID = Int32.Parse(strTuanZhangUserID);
                                    Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                    Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                    #endregion 增加未处理信息增加现金余额未处理信息
                                }
                            }
                        }

                        if (strTuanZhangUserID != "0" && ((my_Model_tab_TuanGou.TuanZhangBonus_GouWuQuan) > 0))
                        {
                            string strNeedAddString = Eggsoft.Common.CommUtil.getShortText("团长奖励团购单号(" + strGoodTypeIdBuyInfo + ") 团ID=" + my_Model_tab_TuanGou.ID + " " + strGoodName + "  组团成功转化收入", 46);
                            if (Decimal.Round(Convert.ToDecimal(my_Model_tab_TuanGou.TuanZhangBonus_GouWuQuan), 2) > 0)
                            {
                                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                if (!BLL_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge.Exists("UserID=" + strTuanZhangUserID + " and ConsumeOrRechargeMoney='" + strNeedAddString + "'"))
                                {
                                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                    Model_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                    Model_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Int32.Parse(strTuanZhangUserID);
                                    Model_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                    Model_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Convert.ToDecimal(my_Model_tab_TuanGou.TuanZhangBonus_GouWuQuan);
                                    Model_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = strNeedAddString;
                                    int intTableID = BLL_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_TuanZhang_Send_tab_Total_Vouchers_Consume_Or_Recharge);


                                    #region 增加购物券未处理信息
                                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                    Model_b011_InfoAlertMessage.InfoTip = strNeedAddString;
                                    Model_b011_InfoAlertMessage.CreateBy = strNeedAddString;
                                    Model_b011_InfoAlertMessage.UpdateBy = strNeedAddString;
                                    Model_b011_InfoAlertMessage.UserID = Int32.Parse(strTuanZhangUserID);
                                    Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                    Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                    #endregion 增加购物券未处理信息

                                }
                            }
                        }

                    }

                    #endregion
                    #endregion 团购处理

                }
                else if (strGoodType == "6")///运营中心的
                {
                    boolOperationCenterBonus = true;
                    Decimal ALLDecimalMoney = strGoodPrice.toDecimal() * strOrderCount.toDecimal();
                    EggsoftWX.BLL.b004_OperationGoods my_BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                    EggsoftWX.Model.b004_OperationGoods my_Model_BLL_b004_OperationGoods = my_BLL_b004_OperationGoods.GetModel(strGoodTypeIdBuyInfo.toInt32());
                    EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(strGoodID));



                    #region 结算普通用户裂变所得  一级及二级

                    int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strParentID.toInt32());
                    if (strParentID.toInt32() > 0)
                    {
                        Decimal DecimalMoneyReturnMoneyShareB = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyShareB.toDecimal());
                        DecimalMoneyReturnMoneyShareB = decimal.Multiply(DecimalMoneyReturnMoneyShareB, (decimal)0.01);
                        if (DecimalMoneyReturnMoneyShareB >= (Decimal)0.01)
                        {
                            if (intIF_Agent_From_Database_ == 3)
                            {
                                Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = strParentID.toInt32();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalMoneyReturnMoneyShareB;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",一级天使" + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46);
                                int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                                #region 增加购物券未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = strParentID.toInt32();
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加购物券未处理信息
                            }
                            else
                            {
                                Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 10;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strParentID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalMoneyReturnMoneyShareB;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",一级代理" + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46);
                                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                #region 增加现金余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = strParentID.toInt32();
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加未处理信息增加现金余额未处理信息
                            }
                        }
                    }


                    myModel_MultiFenXiaoLevel.ManagerAgentParentID = strGrandParentID.toInt32();
                    intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(myModel_MultiFenXiaoLevel.ManagerAgentParentID);
                    if (myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0)
                    {
                        Decimal DecimalMoneyReturnMoneyShareA = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyShareA.toDecimal());
                        DecimalMoneyReturnMoneyShareA = decimal.Multiply(DecimalMoneyReturnMoneyShareA, (decimal)0.01);
                        if (DecimalMoneyReturnMoneyShareA >= (Decimal)0.01)
                        {
                            if (intIF_Agent_From_Database_ == 3)
                            {
                                Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalMoneyReturnMoneyShareA;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",二级天使" + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46);
                                int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                                #region 增加购物券未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加购物券未处理信息
                            }
                            else
                            {
                                Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 10;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalMoneyReturnMoneyShareA;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",二级代理" + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46);
                                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);


                                #region 增加现金余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加未处理信息增加现金余额未处理信息
                            }
                        }
                    }

                    #endregion 结算普通用户裂变所得
                    #region 结算运营中心裂变所得
                    EggsoftWX.BLL.b003_TotalCredits_OperationCenter my_BLL_b003_TotalCredits_OperationCenter = new EggsoftWX.BLL.b003_TotalCredits_OperationCenter();

                    EggsoftWX.BLL.b002_OperationCenter my_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                    EggsoftWX.Model.b002_OperationCenter my_Model_b002_OperationCenter = my_b002_OperationCenter.GetModel("ID=" + strGoodTypeId.toInt32() + " and ShopClient_ID=" + strShopClient_ID);
                    if (my_Model_b002_OperationCenter == null)
                    {
                        continue;///程序里面有测试的额垃圾数据
                    }
                    EggsoftWX.Model.b002_OperationCenter my_Parent_Model_b002_OperationCenter = my_b002_OperationCenter.GetModel(my_Model_b002_OperationCenter.ParentID.toInt32());


                    if (my_Model_b002_OperationCenter != null && my_Model_b002_OperationCenter.UserID > 0)
                    {
                        Decimal DecimalMoneyReturnMoneyOperationShareB = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyOperationShareB.toDecimal());
                        DecimalMoneyReturnMoneyOperationShareB = decimal.Multiply(DecimalMoneyReturnMoneyOperationShareB, (decimal)0.01);
                        if (DecimalMoneyReturnMoneyOperationShareB >= (Decimal)0.01)
                        {
                            EggsoftWX.Model.b003_TotalCredits_OperationCenter Model_b003_TotalCredits_OperationCenter = new EggsoftWX.Model.b003_TotalCredits_OperationCenter();
                            Model_b003_TotalCredits_OperationCenter.Bool_ConsumeOrRecharge = true;
                            Model_b003_TotalCredits_OperationCenter.UserID = my_Model_b002_OperationCenter.UserID;
                            Model_b003_TotalCredits_OperationCenter.ShopClient_ID = strShopClient_ID.toInt32();
                            Model_b003_TotalCredits_OperationCenter.ConsumeOrRechargeMoney = DecimalMoneyReturnMoneyOperationShareB;
                            Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",一级运营" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46);
                            int intTableID = my_BLL_b003_TotalCredits_OperationCenter.Add(Model_b003_TotalCredits_OperationCenter);

                            #region 增加现金余额未处理信息
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.CreateBy = Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.UpdateBy = Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.UserID = my_Model_b002_OperationCenter.UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                            Model_b011_InfoAlertMessage.Type = "Info_myYunYingMoney";
                            Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加未处理信息增加现金余额未处理信息 }
                        }
                    }
                    intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(myModel_MultiFenXiaoLevel.ManagerAgentParentID);
                    if (my_Parent_Model_b002_OperationCenter != null && my_Parent_Model_b002_OperationCenter.UserID > 0)
                    {
                        Decimal DecimalMoneyReturnMoneyOperationShareA = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyOperationShareA.toDecimal());
                        DecimalMoneyReturnMoneyOperationShareA = decimal.Multiply(DecimalMoneyReturnMoneyOperationShareA, (decimal)0.01);
                        if (DecimalMoneyReturnMoneyOperationShareA >= (Decimal)0.01)
                        {
                            EggsoftWX.Model.b003_TotalCredits_OperationCenter Model_b003_TotalCredits_OperationCenter = new EggsoftWX.Model.b003_TotalCredits_OperationCenter();
                            Model_b003_TotalCredits_OperationCenter.Bool_ConsumeOrRecharge = true;
                            Model_b003_TotalCredits_OperationCenter.UserID = my_Parent_Model_b002_OperationCenter.UserID;
                            Model_b003_TotalCredits_OperationCenter.ShopClient_ID = strShopClient_ID.toInt32();
                            Model_b003_TotalCredits_OperationCenter.ConsumeOrRechargeMoney = DecimalMoneyReturnMoneyOperationShareA;
                            Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",二级运营" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46);
                            int intTableID = my_BLL_b003_TotalCredits_OperationCenter.Add(Model_b003_TotalCredits_OperationCenter);

                            #region 增加现金余额未处理信息
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.CreateBy = Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.UpdateBy = Model_b003_TotalCredits_OperationCenter.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.UserID = Model_b003_TotalCredits_OperationCenter.UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                            Model_b011_InfoAlertMessage.Type = "Info_myYunYingMoney";
                            Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加未处理信息增加现金余额未处理信息
                        }
                    }

                    #endregion 结算运营中心裂变所得

                    #region   增加总积分记录 结算用户本身的财富返还
                    EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();

                    Decimal DecimalMoneyReturnConsumerWealth = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnConsumerWealth.toDecimal());///这个是倍数返回
                    if (DecimalMoneyReturnConsumerWealth >= (Decimal)0.01)
                    {
                        EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                        Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = true;
                        Model_b006_TotalWealth_OperationUser.OrderDetailID = ID_Orderdetails.toInt32();
                        Model_b006_TotalWealth_OperationUser.UserID = strUserID.toInt32();
                        Model_b006_TotalWealth_OperationUser.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalMoneyReturnConsumerWealth;
                        Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText("财富积分" + my_Model_BLL_b004_OperationGoods.ReturnConsumerWealth.toString() + "倍 共" + ALLDecimalMoney.toString() + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46);
                        int intTableID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);
                        #region 增加财富积分余额未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_b006_TotalWealth_OperationUser.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_b011_InfoAlertMessage.Type = "Info_TotalWealth";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加财富积分余额未处理信息



                        #region 表示给完了沁加币补贴扣税方式：  5994 / 单 * 0.2 = 1198.80 * 0.135 = 162个沁加币／单
                        #region  一次性减去财富值
                        EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser_Once = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                        Model_b006_TotalWealth_OperationUser_Once.Bool_ConsumeOrRecharge = false;
                        Model_b006_TotalWealth_OperationUser_Once.OrderDetailID = ID_Orderdetails.toInt32();
                        Model_b006_TotalWealth_OperationUser_Once.UserID = strUserID.toInt32();
                        Model_b006_TotalWealth_OperationUser_Once.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_b006_TotalWealth_OperationUser_Once.ConsumeOrRechargeWealth = DecimalMoneyReturnConsumerWealth * (Decimal)0.2;
                        Model_b006_TotalWealth_OperationUser_Once.ConsumeTypeOrRecharge = "20%的寄售管理费OrderID" + strOrderID;
                        my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser_Once);
                        #endregion 一次性减去财富值


                        Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = strUserID.toInt32();
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalMoneyReturnConsumerWealth * (Decimal)0.2 * (Decimal)0.135;
                        Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "馈赠13.5%的沁加币OrderID" + strOrderID;
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
                        #endregion 表示给完了沁加币补贴扣税方式


                    }
                    #endregion 结算用户本身的财富返还

                    #region 就是今天进了这些订单  DecimalMoneyConsumerWeighting是28%的值 。以后 7天后可以参考使用
                    Decimal DecimalMoneyConsumerWeighting = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.MoneyConsumerWeighting.toDecimal());
                    DecimalMoneyConsumerWeighting = decimal.Multiply(DecimalMoneyConsumerWeighting, (decimal)0.01);


                    EggsoftWX.Model.b009_EveryGetOrderIDDetailIDWillActiveReturnMoney Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney = new EggsoftWX.Model.b009_EveryGetOrderIDDetailIDWillActiveReturnMoney();
                    Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney.ShopClientID = strShopClient_ID.toInt32();
                    Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney.OperationGoodsID = strGoodTypeIdBuyInfo.toInt32();
                    Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney.DecimalReturnMoney = DecimalMoneyConsumerWeighting;
                    Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney.ThisDay = DateTime.Now.ToString("yyyy-MM-dd");
                    Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney.OrderID = strOrderID.toInt32();
                    Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney.OrderDatailID = ID_Orderdetails.toInt32();
                    Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney.OrderCount = strOrderCount.toInt32();
                    Int32 Int32B009 = BLLb009_EveryGetOrderIDDetailIDWillActiveReturnMoney.Add(Model_b009_EveryGetOrderIDDetailIDWillActiveReturnMoney);

                    #region 计算用户有多少个可以返现   这是进局子

                    #region 用户重新购买了 消除以前的警告提示 （需要继续下单的警告提示）
                    //EggsoftWX.Model.tab_User_Message_NeedShow Model_tab_User_Message_NeedShow = new EggsoftWX.Model.tab_User_Message_NeedShow();
                    string strInfoType = "NeedByNewOrder" + strGoodID;
                    bool boolIFExsit = BLL_tab_User_Message_NeedShow.Exists("InfoType=@InfoType and UserID=@UserID", strInfoType, strUserID.toInt32());
                    if (boolIFExsit)
                    {
                        BLL_tab_User_Message_NeedShow.Update("Isdeleted=1 and UpdateTime=getdate() and UpdateBy='用户重新购买了消除以前的警告提示'", "InfoType=@InfoType and UserID=@UserID", strInfoType, strUserID.toInt32());
                    }
                    #endregion 用户重新购买了

                    string strAddLogs = "（返还记录" + Int32B009 + " 订单号" + strOrderID + " 订单短号" + ID_Orderdetails + " 数量" + strOrderCount + "）；";

                    EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                    EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and ShopClient_ID=@ShopClient_ID and b004_OperationGoodsID=@b004_OperationGoodsID", strUserID.toInt32(), strShopClient_ID.toInt32(), strGoodTypeIdBuyInfo.toInt32());

                    Model_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.InputShouldReturnCount = strOrderCount.toInt32();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = strOrderCount.toInt32();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UserID = strUserID.toInt32();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.OrderCount = strOrderCount.toInt32();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.PayDateTime = strPayTimeTime.toDateTime();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.OrderID = strOrderID.toInt32();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.OrderDetailID = ID_Orderdetails.toInt32();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = strAddLogs;
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ShopClient_ID = strShopClient_ID.toInt32();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.b004_OperationGoodsID = strGoodTypeIdBuyInfo.toInt32();
                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = DecimalMoneyReturnConsumerWealth - DecimalMoneyReturnConsumerWealth * (Decimal)0.2;///记录这款商品 还有多少钱没还给用户
                    BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Add(Model_b008_OpterationUserActiveReturnMoneyOrderNum);

                    #endregion 计算用户有多少个可以返现
                    #endregion  就是今天进了这些订单

                }
                #endregion 奖励团购
                #region 计算普通分销所得  代理 所得  或者团长所得常见的分销  就是这里了  老张 都这里找
                if (boolTuanZhangBonus_AgentGet == false && boolOperationCenterBonus == false)////////////////////////常见的分销  就是这里了  老张 都这里找
                {
                    #region 把钱给代理 团队 上级团队
                    #region 用户没有支付现金  什么也不干
                    ///用户实际支付支付的现金 包含已有现金  不包含购物券  计算代理所得的钱  从订单详细表中
                    Decimal DecimalUserPayMoney = Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_PriceFromtab_OrderdetailsID(ID_Orderdetails.toInt32());
                    if (DecimalUserPayMoney <= 0) continue;//用户没有支付现金 什么也不干
                                                           //if (Model_tab_ShopClient_Agent__UserID.AgentLevelSelect > 0) return;/////是代理商浏览 什么也不干  直接退出
                    #endregion 用户没有支付现金  什么也不干

                    #region  各级代理自己的价格
                    //Decimal myAdvancePriceProductPricepParentUserd_ID = 0;
                    //Decimal myAdvanceProductPricepManagerAgentParentID = 0;
                    //Decimal myAdvanceProductPriceManagerGrandAgentParentID = 0;
                    //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_AdvanceAgentProduct(strGoodID.toInt32(), strParentID.toInt32(), strGrandParentID.toInt32(), strGreatParentID.toInt32(), out myAdvancePriceProductPricepParentUserd_ID, out myAdvanceProductPricepManagerAgentParentID, out myAdvanceProductPriceManagerGrandAgentParentID);

                    //my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(strGoodID.toInt32());
                    //Decimal? AdvanceParentAgentGet = DecimalUserPayMoney - myAdvancePriceProductPricepParentUserd_ID * strOrderCount.toInt32(); if (AdvanceParentAgentGet < 0) AdvanceParentAgentGet = 0;
                    //if (myAdvancePriceProductPricepParentUserd_ID == 0) AdvanceParentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelParentTrueGet = (AdvanceParentAgentGet > (myModel_MultiFenXiaoLevel.AgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceParentAgentGet : (myModel_MultiFenXiaoLevel.AgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得


                    //Decimal? AdvanceManagerAgentAgentGet = DecimalUserPayMoney - myAdvanceProductPricepManagerAgentParentID * strOrderCount.toInt32() - LevelParentTrueGet; if (AdvanceManagerAgentAgentGet < 0) AdvanceManagerAgentAgentGet = 0;
                    //if (myAdvanceProductPricepManagerAgentParentID == 0) AdvanceManagerAgentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelManagerParentTrueGet = (AdvanceManagerAgentAgentGet > (myModel_MultiFenXiaoLevel.ManagerAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceManagerAgentAgentGet : (myModel_MultiFenXiaoLevel.ManagerAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得

                    //Decimal? AdvanceManagerGrandAgentParentIDGet = DecimalUserPayMoney - myAdvanceProductPriceManagerGrandAgentParentID * strOrderCount.toInt32() - LevelManagerParentTrueGet - LevelParentTrueGet; if (AdvanceManagerGrandAgentParentIDGet < 0) AdvanceManagerGrandAgentParentIDGet = 0;
                    //if (myAdvanceProductPriceManagerGrandAgentParentID == 0) AdvanceManagerGrandAgentParentIDGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelGrandParentTrueGet = (AdvanceManagerGrandAgentParentIDGet > (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceManagerGrandAgentParentIDGet : (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得

                    #region  代理的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    //if (DecimalUserPayMoney < AdvanceParentAgentGet) AdvanceParentAgentGet = 0;
                    //if ((DecimalUserPayMoney - AdvanceParentAgentGet) < AdvanceManagerAgentAgentGet) AdvanceManagerAgentAgentGet = 0;
                    //if ((DecimalUserPayMoney - AdvanceParentAgentGet - AdvanceManagerAgentAgentGet) < AdvanceManagerGrandAgentParentIDGet) AdvanceManagerGrandAgentParentIDGet = 0;
                    #endregion   由于用户可能是购物券付款 要计算实际购物所得

                    #endregion  各级代理自己的价格


                    #region  1 三级分销的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    //Decimal? AgentGetDis = myModel_MultiFenXiaoLevel.AgentGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? ManagerAgentDis = myModel_MultiFenXiaoLevel.ManagerAgentGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? ManagerGrandAgentParentDis = myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //if (DecimalUserPayMoney < AgentGetDis) myModel_MultiFenXiaoLevel.AgentGet = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis) < ManagerAgentDis) myModel_MultiFenXiaoLevel.ManagerAgentGet = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis - ManagerAgentDis) < ManagerGrandAgentParentDis) myModel_MultiFenXiaoLevel.ManagerGrandAgentGet = 0;
                    #region 重新计算一下
                    Decimal? AgentGetDis = myModel_MultiFenXiaoLevel.AgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? ManagerAgentDis = myModel_MultiFenXiaoLevel.ManagerAgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? ManagerGrandAgentParentDis = myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    #endregion 重新计算一下
                    #endregion  1 由于用户可能是购物券付款 要计算实际购物所得

                    #region   1处理上级分销 
                    ///三级代理的
                    if ((strGreatParentID.toInt32() > 0) && (ManagerGrandAgentParentDis > 0))
                    {
                        Decimal DecimalMoney = ManagerGrandAgentParentDis.toDecimal();

                        //woshiGreatParent += decimal.Multiply(decimal.Multiply(decimaAllMoney, ManagerGrandAgentGet), (decimal)0.01);
                        if (Decimal.Round(DecimalMoney, 2) >= (Decimal)0.01)
                        {
                            int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strGreatParentID.toInt32());

                            sendMoneyOrGouWuQuan(!(intIF_Agent_From_Database_ == 3), strGreatParentID.toInt32(), strShopClient_ID.toInt32(),
                                   Eggsoft.Common.CommUtil.getShortText(strNickName + ",三级" + (!(intIF_Agent_From_Database_ == 3) ? "代理" : "天使") + "" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                   , 10);


                        }
                    }
                    ///二级代理的
                    if ((strGrandParentID.toInt32() > 0) && (ManagerAgentDis > 0))
                    {
                        Decimal DecimalMoney = ManagerAgentDis.toDecimal();

                        //woshiGrandParent += decimal.Multiply(decimal.Multiply(decimaAllMoney, ManagerAgentGet), (decimal)0.01);

                        if (Decimal.Round(DecimalMoney, 2) >= (Decimal)0.01)
                        {
                            int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strGrandParentID.toInt32());

                            sendMoneyOrGouWuQuan(!(intIF_Agent_From_Database_ == 3), strGrandParentID.toInt32(), strShopClient_ID.toInt32(),
                                  Eggsoft.Common.CommUtil.getShortText(strNickName + ",二级" + (!(intIF_Agent_From_Database_ == 3) ? "代理" : "天使") + "" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                  , 10);


                        }
                    }
                    ///一级代理的
                    if ((strParentID.toInt32() > 0) && (AgentGetDis > 0))
                    {
                        Decimal DecimalMoney = AgentGetDis.toDecimal();

                        //woshiParent += decimal.Multiply(decimal.Multiply(decimaAllMoney, AgentGet), (decimal)0.01);

                        if (Decimal.Round(DecimalMoney, 2) >= (Decimal)0.01)
                        {
                            int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strParentID.toInt32());
                            sendMoneyOrGouWuQuan(!(intIF_Agent_From_Database_ == 3), strParentID.toInt32(), strShopClient_ID.toInt32(),
                                 Eggsoft.Common.CommUtil.getShortText(strNickName + ",一级" + (!(intIF_Agent_From_Database_ == 3) ? "代理" : "天使") + "" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                 , 10);



                        }
                    }

                    #endregion 处理上级分销


                    #region  2 下三级分销的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    //Decimal? ChildGetDis = myModel_MultiFenXiaoLevel.ChildGetbyGoodsPercent * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? GrandsonGetDis = myModel_MultiFenXiaoLevel.GrandsonGetbyGoodsPercent * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? GreatsonGetDis = myModel_MultiFenXiaoLevel.GreatsonGetbyGoodsPercent * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //if (DecimalUserPayMoney - AgentGetDis - ManagerAgentDis - ManagerGrandAgentParentDis < ChildGetDis) myModel_MultiFenXiaoLevel.ChildGet = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis - ManagerAgentDis - ManagerGrandAgentParentDis - ChildGetDis) < GrandsonGetDis) myModel_MultiFenXiaoLevel.GrandsonGet = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis - ManagerAgentDis - ManagerGrandAgentParentDis - ChildGetDis - GrandsonGetDis) < GreatsonGetDis) myModel_MultiFenXiaoLevel.GreatsonGet = 0;
                    #region 重新计算一下 
                    Decimal? ChildGetDis = myModel_MultiFenXiaoLevel.ChildGetbyGoodsPercent * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? GrandsonGetDis = myModel_MultiFenXiaoLevel.GrandsonGetbyGoodsPercent * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? GreatsonGetDis = myModel_MultiFenXiaoLevel.GreatsonGetbyGoodsPercent * DecimalUserPayMoney * (Decimal)0.01;
                    #endregion 重新计算一下 
                    #endregion  2 由于用户可能是购物券付款 要计算实际购物所得

                    #region   2处理下级分销 
                    ///一级下级
                    if ((myModel_MultiFenXiaoLevel.ChildGetList != null) && (myModel_MultiFenXiaoLevel.ChildGetList.Rows.Count > 0) && (myModel_MultiFenXiaoLevel.ChildGet > 0))
                    {
                        Decimal DecimalMoney = ChildGetDis.toDecimal() / myModel_MultiFenXiaoLevel.ChildGetList.Rows.Count;
                        if (DecimalMoney >= (Decimal)0.01)
                        {
                            for (int j = 0; j < myModel_MultiFenXiaoLevel.ChildGetList.Rows.Count; j++)
                            {
                                Int32 Int32UserID = myModel_MultiFenXiaoLevel.ChildGetList.Rows[j]["ID"].toInt32();
                                Boolean BooleanOnlyIsAngel = myModel_MultiFenXiaoLevel.ChildGetList.Rows[j]["OnlyIsAngel"].toBoolean();
                                Boolean tSendMoneyOrVouchers = true;
                                if (myModel_MultiFenXiaoLevel.ChildGet_Money.toBoolean())
                                {
                                    tSendMoneyOrVouchers = true;
                                }
                                else
                                {
                                    if (BooleanOnlyIsAngel)
                                    {
                                        tSendMoneyOrVouchers = false;
                                    }
                                    else
                                    {
                                        tSendMoneyOrVouchers = true;
                                    }
                                }

                                sendMoneyOrGouWuQuan(tSendMoneyOrVouchers, Int32UserID, strShopClient_ID.toInt32(),
                                    Eggsoft.Common.CommUtil.getShortText(strNickName + ",一级真情" + (tSendMoneyOrVouchers ? "代理" : "天使") + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                    , 12);
                            }


                        }
                    }
                    ///二级下级
                    if ((myModel_MultiFenXiaoLevel.GrandsonGetList != null) && (myModel_MultiFenXiaoLevel.GrandsonGetList.Rows.Count > 0) && (myModel_MultiFenXiaoLevel.GrandsonGet > 0))
                    {
                        Decimal DecimalMoney = GrandsonGetDis.toDecimal() / myModel_MultiFenXiaoLevel.GrandsonGetList.Rows.Count;

                        //woshiGreatParent += decimal.Multiply(decimal.Multiply(decimaAllMoney, ManagerGrandAgentGet), (decimal)0.01);
                        if (DecimalMoney >= (Decimal)0.01)
                        {
                            for (int j = 0; j < myModel_MultiFenXiaoLevel.GrandsonGetList.Rows.Count; j++)
                            {
                                /// //tab_User.ID, tab_ShopClient_Agent_.OnlyIsAngel, tab_User.ParentID///
                                Int32 Int32UserID = myModel_MultiFenXiaoLevel.GrandsonGetList.Rows[j]["ID"].toInt32();
                                Boolean BooleanOnlyIsAngel = myModel_MultiFenXiaoLevel.GrandsonGetList.Rows[j]["OnlyIsAngel"].toBoolean();


                                Boolean tSendMoneyOrVouchers = true;
                                if (myModel_MultiFenXiaoLevel.Grandson_Money.toBoolean())
                                {
                                    tSendMoneyOrVouchers = true;
                                }
                                else
                                {
                                    if (BooleanOnlyIsAngel)
                                    {
                                        tSendMoneyOrVouchers = false;
                                    }
                                    else
                                    {
                                        tSendMoneyOrVouchers = true;
                                    }
                                }

                                sendMoneyOrGouWuQuan(tSendMoneyOrVouchers, Int32UserID, strShopClient_ID.toInt32(),
                                    Eggsoft.Common.CommUtil.getShortText(strNickName + ",二级真情" + (tSendMoneyOrVouchers ? "代理" : "天使") + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                    , 12);
                            }
                        }
                    }
                    ///三级下级
                    if ((myModel_MultiFenXiaoLevel.GreatsonGetList != null) && (myModel_MultiFenXiaoLevel.GreatsonGetList.Rows.Count > 0) && (myModel_MultiFenXiaoLevel.GreatsonGet > 0))
                    {
                        Decimal DecimalMoney = GreatsonGetDis.toDecimal() / myModel_MultiFenXiaoLevel.GreatsonGetList.Rows.Count;

                        //woshiGreatParent += decimal.Multiply(decimal.Multiply(decimaAllMoney, ManagerGrandAgentGet), (decimal)0.01);
                        if (DecimalMoney >= (Decimal)0.01)
                        {
                            for (int j = 0; j < myModel_MultiFenXiaoLevel.GreatsonGetList.Rows.Count; j++)
                            {
                                /// //tab_User.ID, tab_ShopClient_Agent_.OnlyIsAngel, tab_User.ParentID///
                                Int32 Int32UserID = myModel_MultiFenXiaoLevel.GreatsonGetList.Rows[j]["ID"].toInt32();
                                Boolean BooleanOnlyIsAngel = myModel_MultiFenXiaoLevel.GreatsonGetList.Rows[j]["OnlyIsAngel"].toBoolean();


                                Boolean tSendMoneyOrVouchers = true;
                                if (myModel_MultiFenXiaoLevel.Grandson_Money.toBoolean())
                                {
                                    tSendMoneyOrVouchers = true;
                                }
                                else
                                {
                                    if (BooleanOnlyIsAngel)
                                    {
                                        tSendMoneyOrVouchers = false;
                                    }
                                    else
                                    {
                                        tSendMoneyOrVouchers = true;
                                    }
                                }

                                sendMoneyOrGouWuQuan(tSendMoneyOrVouchers, Int32UserID, strShopClient_ID.toInt32(),
                                    Eggsoft.Common.CommUtil.getShortText(strNickName + ",三级真情" + (tSendMoneyOrVouchers ? "代理" : "天使") + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                    , 12);
                            }
                        }
                    }

                    #endregion 处理上级分销


                    #region  3 团队所得 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    //Decimal? OperationGetDis = myModel_MultiFenXiaoLevel.OperationGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? OperationParentGetDis = myModel_MultiFenXiaoLevel.OperationParentGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? OperationGrandParentGetDis = myModel_MultiFenXiaoLevel.OperationGrandParentGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //if (DecimalUserPayMoney - AgentGetDis - ManagerAgentDis - ManagerGrandAgentParentDis - ChildGetDis - GrandsonGetDis - GreatsonGetDis < OperationGetDis) myModel_MultiFenXiaoLevel.OperationGet = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis - ManagerAgentDis - ManagerGrandAgentParentDis - ChildGetDis - GrandsonGetDis - GreatsonGetDis - OperationGetDis) < OperationParentGetDis) myModel_MultiFenXiaoLevel.OperationParentGet = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis - ManagerAgentDis - ManagerGrandAgentParentDis - ChildGetDis - GrandsonGetDis - GreatsonGetDis - OperationGetDis - OperationParentGetDis) < OperationGrandParentGetDis) myModel_MultiFenXiaoLevel.OperationGrandParentGet = 0;
                    #region 重新计算一下 
                    Decimal? OperationGetDis = myModel_MultiFenXiaoLevel.OperationGet * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? OperationParentGetDis = myModel_MultiFenXiaoLevel.OperationParentGet * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? OperationGrandParentGetDis = myModel_MultiFenXiaoLevel.OperationGrandParentGet * DecimalUserPayMoney * (Decimal)0.01;
                    #endregion 重新计算一下 
                    #endregion  3 团队所得 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    #region   3处理团队所得分销 
                    ///当前团队
                    if ((OperationGetDis > 0) && (myModel_MultiFenXiaoLevel.OperationGet > 0))
                    {
                        Decimal DecimalMoney = OperationGetDis.toDecimal();
                        if (Decimal.Round(DecimalMoney, 2) > 0)
                        {
                            Int32 Int32UserID = Eggsoft_Public_CL.Pub.GetUserIDFromTeamID(strTeamID.toInt32());
                            int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(Int32UserID);
                            sendMoneyOrGouWuQuan(!(intIF_Agent_From_Database_ == 3), Int32UserID, strShopClient_ID.toInt32(),
                                    Eggsoft.Common.CommUtil.getShortText(strNickName + ",团队" + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                    , 11);
                        }
                    }
                    ///上级团队
                    if ((OperationParentGetDis > 0) && (myModel_MultiFenXiaoLevel.OperationParentGet > 0))
                    {
                        Decimal DecimalMoney = OperationParentGetDis.toDecimal();
                        if (Decimal.Round(DecimalMoney, 2) >= (Decimal)0.01)
                        {
                            Int32 Int32TeamID = strTeamID.toInt32();
                            Int32 Int32ParentTeamID = Eggsoft_Public_CL.Pub.GetParentTeamIDFromTeamID(Int32TeamID);

                            #region 检查上级是否有资格取得这个收入
                            string strSelectOperationGetChild = @"SELECT   tab_ShopClient_Agent_Level.OperationGetChild
FROM      tab_ShopClient_Agent_Level LEFT OUTER JOIN
                tab_ShopClient_Agent_ ON tab_ShopClient_Agent_Level.ID = tab_ShopClient_Agent_.AgentLevelSelect AND 
                tab_ShopClient_Agent_Level.ShopClientID = tab_ShopClient_Agent_.ShopClientID
WHERE   (tab_ShopClient_Agent_.ID = @tab_ShopClient_Agent_ID)";
                            System.Data.DataTable Data_DataTable = my_BLL_tab_Goods.SelectList(strSelectOperationGetChild, Int32ParentTeamID).Tables[0];
                            if (Data_DataTable.Rows.Count > 0 && Data_DataTable.Rows[0]["OperationGetChild"].toBoolean())
                            {
                                Int32 Int32UserID = Eggsoft_Public_CL.Pub.GetUserIDFromTeamID(Int32ParentTeamID.toInt32());
                                int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_((Int32UserID));
                                sendMoneyOrGouWuQuan(!(intIF_Agent_From_Database_ == 3), Int32UserID, strShopClient_ID.toInt32(),
                                        Eggsoft.Common.CommUtil.getShortText(strNickName + ",上级团队" + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                        , 11);
                            }
                            #endregion 检查上级是否有资格取得这个收入

                        }
                    }
                    ///上上团队
                    if ((OperationGrandParentGetDis > 0) && (myModel_MultiFenXiaoLevel.OperationGrandParentGet > 0))
                    {
                        Decimal DecimalMoney = OperationGrandParentGetDis.toDecimal();
                        if (Decimal.Round(DecimalMoney, 2) >= (Decimal)0.01)
                        {
                            Int32 Int32TeamID = strTeamID.toInt32();
                            Int32 Int32ParentTeamID = Eggsoft_Public_CL.Pub.GetParentTeamIDFromTeamID(Int32TeamID);
                            Int32 Int32GrandParentTeamID = Eggsoft_Public_CL.Pub.GetParentTeamIDFromTeamID(Int32ParentTeamID);

                            #region 检查上上级是否有资格取得这个收入
                            string strSelectOperationGetChild = @"SELECT   tab_ShopClient_Agent_Level.OperationGetGrandChild
FROM      tab_ShopClient_Agent_Level LEFT OUTER JOIN
                tab_ShopClient_Agent_ ON tab_ShopClient_Agent_Level.ID = tab_ShopClient_Agent_.AgentLevelSelect AND 
                tab_ShopClient_Agent_Level.ShopClientID = tab_ShopClient_Agent_.ShopClientID
WHERE   (tab_ShopClient_Agent_.ID = @tab_ShopClient_Agent_ID)";
                            System.Data.DataTable Data_DataTable = my_BLL_tab_Goods.SelectList(strSelectOperationGetChild, Int32GrandParentTeamID).Tables[0];
                            if (Data_DataTable.Rows.Count > 0 && Data_DataTable.Rows[0]["OperationGetGrandChild"].toBoolean())
                            {
                                Int32 Int32UserID = Eggsoft_Public_CL.Pub.GetUserIDFromTeamID(Int32GrandParentTeamID.toInt32());

                                int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_((Int32UserID));
                                sendMoneyOrGouWuQuan(!(intIF_Agent_From_Database_ == 3), Int32UserID, strShopClient_ID.toInt32(),
                                        Eggsoft.Common.CommUtil.getShortText(strNickName + ",上上级团队" + "收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  " + ((intOrderID > 0) ? "确认收货" : "T+7转化收入"), 46), DecimalMoney
                                        , 11);
                            }
                            #endregion 检查上上级是否有资格取得这个收入
                        }
                    }

                    #endregion 处理上级分销

                    #endregion 把钱给代理


                }
                else if (boolTuanZhangBonus_AgentGet == true && boolOperationCenterBonus == false)
                {
                    #region 如果组团成功 把钱给团长 团长的代理商利润

                    if (strTuanZhangIFFinshedCurMemberShipID == "1")////可以结算
                    {

                        if (strTuanZhangUserID != "0" && ((DecimalAgentPrice) > 0))
                        {
                            if (Decimal.Round(DecimalAgentPrice, 2) >= (Decimal)0.01)
                            //       if (Decimal.Round(DecimalAgentPrice, 2) > 0)
                            {
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 13;
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Int32.Parse(strTuanZhangUserID);
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = decimal.Multiply(DecimalAgentPrice, decimal.Parse(strOrderCount));
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",团长所得" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "收入(订单" + strOrderID + ") 团购单号(" + strGoodTypeIdBuyInfo + ") " + strGoodName + "¥" + strGoodPrice + "  组团成功转化收入", 46);
                                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                #region 增加现金余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加未处理信息增加现金余额未处理信息

                            }
                        }
                    }
                    #endregion 如果组团成功 把钱给团长

                }
                #endregion 计算分销所得  代理 所得  或者团长所得
                #endregion
                #region 奖励购买人  结算购买商品 赠送的 现金 或者 购物券
                if (boolOperationCenterBonus == false)
                {
                    string strSend_Money_IfBuy = myDataTable.Rows[i]["Send_Money_IfBuy"].ToString();
                    string strSend_Vouchers_IfBuy = myDataTable.Rows[i]["Send_Vouchers_IfBuy"].ToString();
                    Decimal DecimalSend_Money_IfBuy = 0;
                    Decimal DecimalSend_Vouchers_IfBuy = 0;
                    Decimal.TryParse(strSend_Money_IfBuy, out DecimalSend_Money_IfBuy);
                    Decimal.TryParse(strSend_Vouchers_IfBuy, out DecimalSend_Vouchers_IfBuy);


                    if (DecimalSend_Money_IfBuy > 0)
                    {
                        decimal decimaAllSendMoney = decimal.Multiply(DecimalSend_Money_IfBuy, decimal.Parse(strOrderCount));

                        ///赠送一个钱
                        EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_Sendtab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_Sendtab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                        Model_Sendtab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 21;
                        Model_Sendtab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = decimaAllSendMoney;
                        Model_Sendtab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购买商品" + strGoodName + "赠送(订单" + strOrderID + ")";
                        Model_Sendtab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_Sendtab_TotalCredits_Consume_Or_Recharge.UserID = Int32.Parse(strUserID);
                        Model_Sendtab_TotalCredits_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                        int intTableID = BLL_Sendtab_TotalCredits_Consume_Or_Recharge.Add(Model_Sendtab_TotalCredits_Consume_Or_Recharge);

                        #region 增加现金余额未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_Sendtab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_Sendtab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_Sendtab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_Sendtab_TotalCredits_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加未处理信息增加现金余额未处理信息


                        Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送现金通知", "", Eggsoft_Public_CL.Pub.GetNickName(strUserID.ToString()) + "购买商品" + strGoodName + "送" + Eggsoft_Public_CL.Pub.getPubMoney(decimaAllSendMoney) + "元现金,点击'我'查看", strmywebuyURL);
                        System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                        WeiXinTuWens_ArrayList.Add(First);


                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strUserID), 0, Eggsoft_Public_CL.Pub.GetNickName(strUserID) + "购买商品" + strGoodName + "送" + Eggsoft_Public_CL.Pub.getPubMoney(decimaAllSendMoney) + "元现金");
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(strUserID), intShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }

                    }
                    if (DecimalSend_Vouchers_IfBuy > 0)
                    {
                        decimal decimaAllSendVouchers = decimal.Multiply(DecimalSend_Vouchers_IfBuy, decimal.Parse(strOrderCount));

                        ///赠送购物券
                        EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_Send_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                        EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_Send_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                        Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                        Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = decimaAllSendVouchers;
                        Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "购买商品" + strGoodName + "赠送(订单" + strOrderID + ")";
                        Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Int32.Parse(strUserID);
                        Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                        int intTableID = BLL_Send_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_Send_tab_Total_Vouchers_Consume_Or_Recharge);
                        #region 增加购物券未处理信息
                        EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                        EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                        Model_b011_InfoAlertMessage.InfoTip = Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.CreateBy = Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UpdateBy = Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                        Model_b011_InfoAlertMessage.UserID = Model_Send_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                        Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                        Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                        Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                        bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                        #endregion 增加购物券未处理信息


                        Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intShopClientID.ToString()) + "通知", "", Eggsoft_Public_CL.Pub.GetNickName(strUserID) + "购买商品" + strGoodName + "送" + Eggsoft_Public_CL.Pub.getPubMoney(decimaAllSendVouchers) + "元购物券,点击'我'查看", strmywebuyURL);
                        System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                        WeiXinTuWens_ArrayList.Add(First);

                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(strUserID), 0, Eggsoft_Public_CL.Pub.GetNickName(strUserID) + "购买商品" + strGoodName + "送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intShopClientID.ToString()) + "元" + Eggsoft_Public_CL.Pub.stringShowPower(intShopClientID.ToString(), "VouchersShopName") + "");
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(strUserID), intShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }

                    }
                }
                #endregion

                BLLtab_Orderdetails.Update("Over7DaysToBeans=1,UpdateDateTime=getdate()", "id=" + ID_Orderdetails);
            }

        }

        /// <summary>
        /// 公用函数   应该都改掉  但是懒啊  有多少算是多少
        /// </summary>
        /// <param name="boolMoneyORGouQuquan"></param>
        /// <param name="intuserID"></param>
        /// <param name="intShopClient_ID"></param>
        /// <param name="StrDesc"></param>
        /// <param name="DecimalMoney"></param>
        ///  /// <param name="intConsumeOrRechargeType">收入类型  从 0 到 255 的整型数据。存储大小为 1 字节。  消费类型或者收入类型。1-100 表示 收入类型（1表示充值收入 10表示分销收入，11表示团队收入，12表示真情收入13组团收入 20表示下级访问商品收入，21购买赠送，22游戏赠送，30表示平台增加商家调节收入 40表示商品访问收入 41表示扫描代理赠送42关注赠送43首次访问赠送44签到赠送 50表示咨询访问收入 60表示签到收入 70表示红包收入 71红包退回72取消提现 80表示清除购物车81自动兑现现金 90表示待支付订单取消 91已支付取消92市场调查推广费发放）     101-200表示消费类型。（110表示购物车消耗 120表示红包消耗  130提现 140平台减少）  tinyint 长度3 占用字节数1 小数位数0 允许空 默认值无 </param>
        public static void sendMoneyOrGouWuQuan(
            Boolean boolMoneyORGouQuquan,
            Int32 intuserID, Int32 intShopClient_ID,
            string StrDesc, Decimal DecimalMoney,
            int intConsumeOrRechargeType
            )
        {
            if (intuserID == 0) return;

            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();




            if (!boolMoneyORGouQuquan)
            {
                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = intuserID;
                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intShopClient_ID;
                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalMoney;
                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = StrDesc;
                int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);


                #region 增加购物券未处理信息
                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                Model_b011_InfoAlertMessage.UserID = intuserID;
                Model_b011_InfoAlertMessage.ShopClient_ID = intShopClient_ID;
                Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);

                #region 发送到账通知
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClient_ID);
                    string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                    string strURL = "https://" + strErJiYuMing + "/multibutton_showmoney_vouchers.aspx";

                    Pub_GetOpenID_And_.AccountNoticeTempletMessage myAccountNoticeTempletMessage = new Pub_GetOpenID_And_.AccountNoticeTempletMessage();
                    myAccountNoticeTempletMessage.LookURL = strURL;
                    myAccountNoticeTempletMessage.GetMoney = DecimalMoney;
                    myAccountNoticeTempletMessage.Des = StrDesc;

                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempleAccountNotice(intuserID, intShopClient_ID, myAccountNoticeTempletMessage);
                });
                #endregion 发送到账通知
                #endregion 增加购物券未处理信息
            }
            else
            {
                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = intConsumeOrRechargeType;
                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = intuserID;
                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intShopClient_ID;
                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalMoney;
                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = StrDesc;
                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                #region 增加现金余额未处理信息
                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                Model_b011_InfoAlertMessage.ShopClient_ID = intShopClient_ID;
                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);

                #region 发送到账通知
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClient_ID);
                    string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                    string strURL = "https://" + strErJiYuMing + "/multibutton_showmoneydata.aspx";

                    Pub_GetOpenID_And_.AccountNoticeTempletMessage myAccountNoticeTempletMessage = new Pub_GetOpenID_And_.AccountNoticeTempletMessage();
                    myAccountNoticeTempletMessage.LookURL = strURL;
                    myAccountNoticeTempletMessage.GetMoney = DecimalMoney;
                    myAccountNoticeTempletMessage.Des = StrDesc;

                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempleAccountNotice(intuserID, intShopClient_ID, myAccountNoticeTempletMessage);
                });
                #endregion 发送到账通知
                #endregion 增加未处理信息增加现金余额未处理信息
            }


        }

        public static void DoOver7daysCountMySonMoney_Then_CountyuEArgMoney(int intArgUserID, out Decimal myDoOver7daysCountMySonMoney_Then_CountyuEArgMoney)
        {
            Decimal CountyuEArgMoney = 0;

            try
            {

                EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();

                #region 转换T+7
                EggsoftWX.BLL.View_SalesGoods BLLView_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();//
                String strYesTodaySQLBody = "(((DateDiff(dd, PayDateTime, GetDate()) > 7) and (GoodType<>6) and (PayStatus=1) and (DateDiff(dd, UpdateDateTime, GetDate()) > 7)) OR (PayStatus=1 and (GoodType<>6) and isReceipt=1 and DeliveryText<>\'\'))  and Over7DaysToBeans=0 and IsDeleted=0";

                //String strBody = "";
                System.Data.DataTable myDataTable = BLLView_SalesGoods.GetList("(ParentID=" + intArgUserID + " or GrandParentID=" + intArgUserID + " or GreatParentID=" + intArgUserID + ") and " + strYesTodaySQLBody + " order by ID_Orderdetails").Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    lock ("Over7DaysToBeans" + i.ToString())
                    {
                        string ID_Orderdetails = myDataTable.Rows[i]["ID_Orderdetails"].ToString();
                        string Over7DaysToBeans = myDataTable.Rows[i]["Over7DaysToBeans"].ToString();
                        bool boolOver7DaysToBeans = Convert.ToBoolean(myDataTable.Rows[i]["Over7DaysToBeans"].ToString());


                        string strParentID = myDataTable.Rows[i]["ParentID"].ToString();
                        string strShopClient_ID = myDataTable.Rows[i]["ShopClient_ID"].ToString();
                        string strUserID = myDataTable.Rows[i]["UserID"].ToString();
                        string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                        string strGoodName = myDataTable.Rows[i]["GoodName"].ToString();
                        string strNickName = myDataTable.Rows[i]["NickName"].ToString();
                        string strPinglun = myDataTable.Rows[i]["Pinglun"].ToString();
                        string strCreatDateTime = myDataTable.Rows[i]["CreatDateTime"].ToString();
                        string strPayTimeTime = myDataTable.Rows[i]["PayDateTime"].ToString();
                        string strGoodPrice = myDataTable.Rows[i]["GoodPrice"].ToString();
                        //string strDecimal_FenXiaoMoney = myDataTable.Rows[i]["FenXiaoMoney"].ToString();
                        string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();
                        string strOrderID = myDataTable.Rows[i]["OrderID"].ToString();

                        //Modeltab_Goods = BLLtab_Goods.GetModel(Int32.Parse(strGoodID));
                        decimal decimaAllMoney = decimal.Multiply(decimal.Parse(strGoodPrice), decimal.Parse(strOrderCount));

                        int intstrParentID = 0;
                        int.TryParse(strParentID, out intstrParentID);

                        decimal woshiParent = (decimal)0;
                        decimal woshiGrandParent = (decimal)0;
                        decimal woshiGreatParent = (decimal)0;
                        //decimal woshiGreatParent = (decimal)0;
                        //decimal woshiPartner = (decimal)0;
                        //decimal woshiRecommend = (decimal)0;


                        Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                        myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                        myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
                        myModel_MultiFenXiaoLevel.UserID = strUserID.toInt32();
                        Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);



                        if ((myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID > 0) && (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet > 0))
                        {
                            woshiGreatParent += decimal.Multiply(decimal.Multiply(decimaAllMoney, myModel_MultiFenXiaoLevel.ManagerGrandAgentGet), (decimal)0.01);

                            if (Decimal.Round(woshiGreatParent, 2) > 0)
                            {
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 10;
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = woshiGreatParent;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",三级代理收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  T+7转化收入", 46);
                                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                #region 增加现金余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加未处理信息增加现金余额未处理信息
                            }
                        }
                        if ((myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0) && (myModel_MultiFenXiaoLevel.ManagerAgentGet > 0))
                        {
                            woshiGrandParent += decimal.Multiply(decimal.Multiply(decimaAllMoney, myModel_MultiFenXiaoLevel.ManagerAgentGet), (decimal)0.01);

                            if (Decimal.Round(woshiGrandParent, 2) > 0)
                            {
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 10;
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = myModel_MultiFenXiaoLevel.ManagerAgentParentID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = woshiGrandParent;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",二级级代理收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  T+7转化收入", 46);
                                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                #region 增加现金余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加未处理信息增加现金余额未处理信息
                            }
                        }
                        if ((intstrParentID > 0) && (myModel_MultiFenXiaoLevel.AgentGet > 0))
                        {
                            woshiParent += decimal.Multiply(decimal.Multiply(decimaAllMoney, myModel_MultiFenXiaoLevel.AgentGet), (decimal)0.01);

                            if (Decimal.Round(woshiParent, 2) > 0)
                            {
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 10;
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = intstrParentID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = woshiParent;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft.Common.CommUtil.getShortText(strNickName + ",一级代理业务收入(订单" + strOrderID + ")" + strGoodName + "¥" + strGoodPrice + "  T+7转化收入", 46);
                                int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                                #region 增加现金余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加未处理信息增加现金余额未处理信息
                            }
                        }
                        BLLtab_Orderdetails.Update("Over7DaysToBeans=1,UpdateDateTime=getdate()", "id=" + ID_Orderdetails);
                    }
                }
                #endregion

                string maxID = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList("select max(id) as maxID from tab_TotalCredits_Consume_Or_Recharge where UserID=" + intArgUserID).Tables[0].Rows[0]["maxID"].ToString();
                if (String.IsNullOrEmpty(maxID)) maxID = "0";
                if (BLL_tab_TotalCredits_Consume_Or_Recharge.Exists("ID=" + maxID))
                {
                    string maxIDMoney = BLL_tab_TotalCredits_Consume_Or_Recharge.SelectList("select RemainingSum from tab_TotalCredits_Consume_Or_Recharge where ID=" + maxID).Tables[0].Rows[0]["RemainingSum"].ToString();
                    Decimal.TryParse(maxIDMoney, out CountyuEArgMoney);
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "每日更新");
            }
            finally
            {

            }
            myDoOver7daysCountMySonMoney_Then_CountyuEArgMoney = CountyuEArgMoney;
            //return strBody;
        }

        /// <summary>
        /// 检查用户是否支付过订单
        /// </summary>
        /// <param name="intOrderID"></param>
        public static bool DoCheckIfPayedMoney(Int32 intShopClientID, Int32 intUserID)
        {
            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            bool boolIfPayed = BLL_tab_Order.Exists("PayStatus=1 and UserID=@UserID and PayStatus=1 and isnull(IsDeleted,0)=0 and ShopClient_ID=@ShopClient_ID", intUserID, intShopClientID);
            return boolIfPayed;
        }


        public static void DoReturnMoney_By_OrderIDByIDCountyuEArgMoney(int intOrderID)
        {

            try
            {

                //int userID = Eggsoft_Public_CL.Pub_GetOpenID_And_.getUserIDFromCookies();

                //int OrderINT = Convert.ToInt32(Request.QueryString["OrderINT"]);//订单记录的ID
                EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();


                #region 当然要归还钱了
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();

                if (BLL_tab_Order.Exists(intOrderID))
                {
                    my_Model_tab_Order = BLL_tab_Order.GetModel(intOrderID);




                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                    EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

                    System.Data.DataTable Data_DataTable = BLL_tab_Orderdetails.GetList("OrderID=" + intOrderID + "  and isdeleted<>1  order by id asc").Tables[0];

                    for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                    {
                        string strGoodIDCountID = Data_DataTable.Rows[i]["OrderCount"].ToString();
                        string strShopClient_ID = Data_DataTable.Rows[i]["ShopClient_ID"].ToString();
                        string strGoodID = Data_DataTable.Rows[i]["GoodID"].ToString();
                        string strVouchersNum_List = Data_DataTable.Rows[i]["VouchersNum_List"].ToString();
                        string strBeans = Data_DataTable.Rows[i]["Beans"].ToString();
                        string strMoneyCredits = Data_DataTable.Rows[i]["MoneyCredits"].ToString();
                        string strMoneyWeBuy8Credits = Data_DataTable.Rows[i]["MoneyWeBuy8Credits"].ToString();

                        EggsoftWX.BLL.tab_Goods my_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        my_tab_Goods.Update("KuCunCount=KuCunCount+" + strGoodIDCountID, "id=" + strGoodID);


                        #region---归还金额
                        if (strMoneyCredits == "0.00") strMoneyCredits = "";
                        if (String.IsNullOrEmpty(strMoneyCredits) == false)
                        {
                            if (Decimal.Round(Decimal.Parse(strMoneyCredits), 2) > 0)
                            {
                                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge MeBLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge MeModel_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                MeModel_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 91;
                                MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Decimal.Parse(strMoneyCredits);
                                MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "已付取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                                MeModel_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                MeModel_tab_TotalCredits_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                                int intTableID = MeBLL_tab_TotalCredits_Consume_Or_Recharge.Add(MeModel_tab_TotalCredits_Consume_Or_Recharge);
                                #region 增加现金余额未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = MeModel_tab_TotalCredits_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加未处理信息增加现金余额未处理信息
                            }
                        }
                        #endregion

                        #region---归还wei8金额
                        if (strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";
                        if (String.IsNullOrEmpty(strMoneyWeBuy8Credits) == false)
                        {
                            if (Decimal.Round(Decimal.Parse(strMoneyWeBuy8Credits), 2) > 0)
                            {
                                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Decimal.Parse(strMoneyWeBuy8Credits);
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "待付取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                int intTableID = BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                                #region 增加购物券未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                #endregion 增加购物券未处理信息

                            }
                        }
                        #endregion

                        #region---归还微店豆

                        //
                        if (strBeans == "0") strBeans = "";
                        if (String.IsNullOrEmpty(strBeans) == false)
                        {
                            if (Int32.Parse(strBeans) > 0)
                            {
                                EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge MeBLL_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge MeModel_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge();
                                MeModel_tab_TotalBeans_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                MeModel_tab_TotalBeans_Consume_Or_Recharge.ConsumeOrRechargeBean = Int32.Parse(strBeans);
                                MeModel_tab_TotalBeans_Consume_Or_Recharge.ConsumeTypeOrRecharge = "已支付订单取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                                MeModel_tab_TotalBeans_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                                MeModel_tab_TotalBeans_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                MeBLL_tab_TotalBeans_Consume_Or_Recharge.Add(MeModel_tab_TotalBeans_Consume_Or_Recharge);
                            }
                        }
                        #endregion

                        #region---归还购物券
                        //---归还购物券
                        if (String.IsNullOrEmpty(strVouchersNum_List) == false)
                        {
                            string[] strEachList = strVouchersNum_List.Split(',');

                            for (int k = 0; k < strEachList.Length; k++)
                            {

                                if (String.IsNullOrEmpty(strEachList[k]) == false)
                                {
                                    string[] strEachListString = strEachList[k].Split('#');
                                    String strVouchersNum = strEachListString[0];

                                    Model_tab_Shopping_Vouchers = BLL_tab_Shopping_Vouchers.GetModel("VouchersNum='" + strVouchersNum + "'");
                                    Model_tab_Shopping_Vouchers.GuWuCheIDOrOrderDetailID = 0;
                                    Model_tab_Shopping_Vouchers.UserID = 0;
                                    Model_tab_Shopping_Vouchers.MoneyUsed = 0;
                                    BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                                }
                            }

                        }
                        #endregion

                    }

                }
                #endregion


                #region 还要归还分销所得啊

                try
                {

                    EggsoftWX.BLL.tab_Orderdetails BLLtab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();

                    #region 转换T+7



                    #endregion
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }


                #endregion
            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="intArgUserID"></param>
        /// <param name="myArgMoney"></param>
        /// <param name="myGetSQLMySonMoneyArrayList"></param>
        /// <param name="onlyEveryDayStatus">是否仅仅是每天的统计  统计是不要带路径信息之类的 只是要钱。路径信息 可能要做cookis path</param>
        /// <returns></returns>
        public static String GetCountMySonMoney(int intArgUserID, out Decimal myArgMoney, ArrayList myGetSQLMySonMoneyArrayList = null, bool onlyEveryDayStatus = false)
        {
            string strPub_Agent_Path = "";
            if (onlyEveryDayStatus)
            {///仅仅是数据统计 不要做 cookies 检测之类的
            }
            else
            {
                strPub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(intArgUserID);
            }

            EggsoftWX.BLL.View_SalesGoods BLLView_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();

            EggsoftWX.BLL.tab_Goods BLLtab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Modeltab_Goods = new EggsoftWX.Model.tab_Goods();

            EggsoftWX.BLL.tab_ShopClient_Agent_ BLLtab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Modeltab_ShopClient_Agent_ = BLLtab_ShopClient_Agent_.GetModel("UserID=" + intArgUserID + " and IsDeleted=0 ");


            #region    sql          string strView_SalesGoods = "";

            string strView_SalesGoods = "";
            strView_SalesGoods += "select * from (SELECT   tab_Orderdetails.OrderCount, tab_Order.ID AS OrderID, tab_Order.PayStatus, tab_Orderdetails.GoodID, ";
            strView_SalesGoods += " tab_Order.UserID, tab_User.NickName, tab_Order.OrderNum, tab_Goods.PromotePrice,  ";
            strView_SalesGoods += " tab_Orderdetails.GoodPrice, tab_Goods.Name AS GoodName, tab_Orderdetails.ID AS ID_Orderdetails,  ";
            strView_SalesGoods += " tab_Orderdetails.Pinglun, tab_Orderdetails.CreatDateTime, tab_Order.PayDateTime,  ";
            strView_SalesGoods += " tab_Orderdetails.ParentID, tab_Orderdetails.Over7DaysToBeans, tab_Goods.IsDeleted,  ";
            strView_SalesGoods += " tab_Order.TotalMoney, tab_Goods.IS_Admin_check,  ";
            strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.Empowered AS ParentID_Empowered,  ";
            strView_SalesGoods += " tab_ShopClient_Agent_.Empowered AS GrandParentID_Empowered, tab_Order.DeliveryText,  ";
            strView_SalesGoods += " tab_Order.isReceipt, tab_Orderdetails.GrandParentID, tab_Orderdetails.GreatParentID,  ";
            strView_SalesGoods += " tab_Order.UpdateDateTime, tab_Order.ShopClient_ID, tab_Goods.Send_Money_IfBuy,  ";
            strView_SalesGoods += " tab_Goods.Send_Vouchers_IfBuy, tab_Orderdetails.GoodType, tab_Orderdetails.GoodTypeId ";
            strView_SalesGoods += "  FROM      tab_ShopClient_Agent_  with(nolock) RIGHT OUTER JOIN ";
            strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID  with(nolock) ON  ";
            strView_SalesGoods += " tab_ShopClient_Agent_.UserID = tab_ShopClient_Agent__ProductClassID.UserID RIGHT OUTER JOIN ";
            strView_SalesGoods += " tab_Order with(nolock)  INNER JOIN ";
            strView_SalesGoods += " tab_Orderdetails with(nolock) ON tab_Order.ID = tab_Orderdetails.OrderID INNER JOIN ";
            strView_SalesGoods += " tab_Goods with(nolock) ON tab_Orderdetails.GoodID = tab_Goods.ID INNER JOIN ";
            strView_SalesGoods += " tab_User  with(nolock) ON tab_Order.UserID = tab_User.ID ON  ";
            strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.UserID = tab_Order.UserID AND  ";
            strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.ProductID = tab_Orderdetails.GoodID ";
            strView_SalesGoods += " WHERE   (tab_Order.PayStatus = 1) AND (tab_Goods.IsDeleted = 0)  and (tab_Orderdetails.ParentID=" + intArgUserID + " or tab_Orderdetails.GrandParentID=" + intArgUserID + " or tab_Orderdetails.GreatParentID=" + intArgUserID + ")) as newdddTable order by ID_Orderdetails";

            #endregion

            String strBody = "";
            Decimal CountmyArgMoney = 0;
            System.Data.DataTable myDataTable = BLLView_SalesGoods.GetList("(ParentID=" + intArgUserID + " or GrandParentID=" + intArgUserID + " or GreatParentID=" + intArgUserID + ")  order by ID_Orderdetails").Tables[0];

            if (myDataTable.Rows.Count > 0)
            {
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string strParentID = myDataTable.Rows[i]["ParentID"].ToString();
                    string strID_Orderdetails = myDataTable.Rows[i]["ID_Orderdetails"].ToString();
                    string strUserID = myDataTable.Rows[i]["UserID"].ToString();
                    string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                    string strNickName = myDataTable.Rows[i]["NickName"].ToString();
                    string strPinglun = myDataTable.Rows[i]["Pinglun"].ToString();
                    string strCreatDateTime = myDataTable.Rows[i]["CreatDateTime"].ToString();
                    string strThisGoodPrice = myDataTable.Rows[i]["GoodPrice"].ToString();
                    string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();
                    string strOrderNum = myDataTable.Rows[i]["OrderNum"].ToString();

                    string strOver7DaysToBeans = myDataTable.Rows[i]["Over7DaysToBeans"].ToString();
                    string strDeliveryText = myDataTable.Rows[i]["DeliveryText"].ToString();

                    #region 用户没有支付现金  什么也不干
                    ///用户需要支付的现金 包含已有现金  不包含购物券  计算代理所得的钱  从订单详细表中
                    Decimal DecimalUserPayMoney = Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_PriceFromtab_OrderdetailsID(strID_Orderdetails.toInt32());
                    if (DecimalUserPayMoney <= 0) continue;//用户没有支付现金 什么也不干
                                                           //if (Model_tab_ShopClient_Agent__UserID.AgentLevelSelect > 0) return;/////是代理商浏览 什么也不干  直接退出
                    #endregion 用户没有支付现金  什么也不干


                    Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                    myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                    myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
                    myModel_MultiFenXiaoLevel.UserID = strUserID.toInt32();
                    Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);
                    //int intstrParentID = 0;
                    //int.TryParse(strParentID, out intstrParentID);

                    decimal woshiParent = (decimal)0;
                    decimal woshiGrandParent = (decimal)0;
                    decimal woshiGreatParent = (decimal)0;


                    //Decimal AgentGet = 0;
                    //Decimal ManagerAgentGet = 0;
                    //int ManagerAgentParentID = 0;
                    //Decimal ManagerGrandAgentGet = 0;
                    //int ManagerGrandAgentParentID = 0;
                    //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(Int32.Parse(strGoodID), intstrParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, "0", "0", "0");


                    #region  各级代理自己的价格
                    //Decimal AdvancePriceProductPricepParentUserd_ID = 0;
                    //Decimal AdvanceProductPricepManagerAgentParentID = 0;
                    //Decimal AdvanceProductPriceManagerGrandAgentParentID = 0;
                    //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_AdvanceAgentProduct(strGoodID.toInt32(), strParentID.toInt32(), myModel_MultiFenXiaoLevel.ManagerAgentParentID, myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, out AdvancePriceProductPricepParentUserd_ID, out AdvanceProductPricepManagerAgentParentID, out AdvanceProductPriceManagerGrandAgentParentID);

                    //Decimal? AdvanceParentAgentGet = DecimalUserPayMoney - AdvancePriceProductPricepParentUserd_ID * strOrderCount.toInt32(); if (AdvanceParentAgentGet < 0) AdvanceParentAgentGet = 0;
                    //if (AdvancePriceProductPricepParentUserd_ID == 0) AdvanceParentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? AdvanceManagerAgentAgentGet = DecimalUserPayMoney - AdvanceProductPricepManagerAgentParentID * strOrderCount.toInt32() - AdvanceParentAgentGet; if (AdvanceManagerAgentAgentGet < 0) AdvanceManagerAgentAgentGet = 0;
                    //if (AdvanceProductPricepManagerAgentParentID == 0) AdvanceManagerAgentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? AdvanceManagerGrandAgentParentIDGet = DecimalUserPayMoney - AdvanceProductPriceManagerGrandAgentParentID * strOrderCount.toInt32() - AdvanceParentAgentGet - AdvanceManagerAgentAgentGet; if (AdvanceManagerGrandAgentParentIDGet < 0) AdvanceManagerGrandAgentParentIDGet = 0;
                    //if (AdvanceProductPriceManagerGrandAgentParentID == 0) AdvanceManagerGrandAgentParentIDGet = 0;//父亲不是代理 就不要拿代理差价



                    #endregion  各级代理自己的价格

                    #region  三级分销的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    //Decimal? AgentGetDis = myModel_MultiFenXiaoLevel.AgentGet * strThisGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? ManagerAgentDis = myModel_MultiFenXiaoLevel.ManagerAgentGet * strThisGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? ManagerGrandAgentParentDis = myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strThisGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //if (DecimalUserPayMoney < AgentGetDis) AgentGetDis = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis) < ManagerAgentDis) ManagerAgentDis = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis - ManagerAgentDis) < ManagerGrandAgentParentDis) ManagerGrandAgentParentDis = 0;

                    #region 重新计算一下
                    Decimal? AgentGetDis = myModel_MultiFenXiaoLevel.AgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? ManagerAgentDis = myModel_MultiFenXiaoLevel.ManagerAgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? ManagerGrandAgentParentDis = myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    #endregion 重新计算一下

                    #endregion   由于用户可能是购物券付款 要计算实际购物所得
                    int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(intArgUserID);
                    if ((intArgUserID == myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID) || (intArgUserID == myModel_MultiFenXiaoLevel.ManagerAgentParentID) || (intArgUserID == strParentID.toInt32()))
                    {///订单中的父系  与 生活中的父系 发生聊改变  谁都不计


                        string strAllMoney = decimal.Multiply(decimal.Parse(strThisGoodPrice), decimal.Parse(strOrderCount)).ToString();

                        if (string.IsNullOrEmpty(strPinglun)) strPinglun = strNickName + ":不能太懒啊，点击,找到下方的商品评价，输入评论，分发朋友圈，创造致富神话！";

                        Modeltab_Goods = BLLtab_Goods.GetModel(Int32.Parse(strGoodID));


                        decimal decimaAllMoney = decimal.Parse(strAllMoney);



                        if (myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID > 0)
                        {


                            if (intIF_Agent_From_Database_ > 3)
                            {
                                Decimal DecimalMoney = ManagerGrandAgentParentDis.toDecimal();
                                //if (AdvanceManagerGrandAgentParentIDGet > DecimalMoney) DecimalMoney = AdvanceManagerGrandAgentParentIDGet.toDecimal();///给多的
                                woshiGreatParent += Math.Round(DecimalMoney, 2);
                            }
                        }

                        if (myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0)
                        {

                            if (intIF_Agent_From_Database_ > 3)
                            {
                                Decimal DecimalMoney = ManagerAgentDis.toDecimal();
                                //if (AdvanceManagerAgentAgentGet > DecimalMoney) DecimalMoney = AdvanceManagerAgentAgentGet.toDecimal();///给多的
                                woshiGrandParent += Math.Round(DecimalMoney, 2);
                            }
                        }

                        if (strParentID.toInt32() > 0)
                        {
                            if (intIF_Agent_From_Database_ > 3)
                            {
                                Decimal DecimalMoney = AgentGetDis.toDecimal();
                                //if (AdvanceParentAgentGet > DecimalMoney) DecimalMoney = AdvanceParentAgentGet.toDecimal();///给多的
                                woshiParent += Math.Round(DecimalMoney, 2);
                            }
                        }
                        string GoodIcon = "";
                        if (String.IsNullOrEmpty(Modeltab_Goods.Icon) == false)
                        {
                            GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(Modeltab_Goods.Icon, 100);
                        }
                        /////后台统计分销使用
                        //if (intArgUserID == 11594)
                        //{
                        //    int ddddd = 0;
                        //}

                        string strmyGetSQLMySonMoneyArrayList = "";
                        string strHeadSQL = "insert into tab_tab_ShopClient_myGetFenXiaoMoneyHistory ([ID],[UserID],[BuyUserID],[BuyUserIDNickName],[CreatDateTime],[TotalMoney],[OrderNum],[MyGetPercent],[MyWillGetMoney],[IFGetToMoneyDesc],[IFGetToMoney],[ShopClientID])";
                        int intThisID = 0;
                        if (myGetSQLMySonMoneyArrayList != null) intThisID = myGetSQLMySonMoneyArrayList.Count;
                        strmyGetSQLMySonMoneyArrayList = strHeadSQL + " values (" + intThisID + "," + intArgUserID + "," + strUserID + ",'" + GoodP.Get_NickName_From_CurUser_ID(Int32.Parse(strUserID), Int32.Parse(strGoodID)) + "'," + "'" + strCreatDateTime + "'," + strAllMoney + ",'" + strOrderNum + "'";
                        ////
                        strBody += "<a href=\"" + strPub_Agent_Path + "/product-" + strGoodID + ".aspx\"><li ms-repeat=\"items\">\n";
                        strBody += "			<div class=\"ul_li\">\n";
                        strBody += "				<div class=\"ul_li_div_img\">\n";
                        strBody += "					<div class=\"div_img_center\"><img alt=\"图片\" width=\"90px\" height=\"90px\"\n";
                        strBody += "						src=\"" + Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon + "\"></div>\n";
                        strBody += "					<div class=\"ul_li_div_name\">" + Modeltab_Goods.Name + "</div>\n";

                        strBody += "				</div>\n";

                        strBody += "				<div class=\"ul_li_trade\"><ul class=\"OliverModi\">\n";
                        strBody += "						<li>顾客昵称：" + GoodP.Get_NickName_From_CurUser_ID(Int32.Parse(strUserID), Int32.Parse(strGoodID)) + "</li>\n";
                        strBody += "						<li>成交时间：" + strCreatDateTime + "</li>\n";
                        strBody += "						<li>成交价格：￥" + Pub.getPubMoney(Decimal.Parse(strAllMoney)) + "</li>\n";
                        strBody += "						<li>订单号：" + strOrderNum + "</li>\n";
                        if (intArgUserID == myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID)
                        {
                            strmyGetSQLMySonMoneyArrayList += "," + myModel_MultiFenXiaoLevel.ManagerGrandAgentGet + "";
                            strBody += "						<li>所得数：" + myModel_MultiFenXiaoLevel.ManagerGrandAgentGet + "%</li>\n"; ;
                        }
                        else if (intArgUserID == myModel_MultiFenXiaoLevel.ManagerAgentParentID)
                        {
                            strmyGetSQLMySonMoneyArrayList += "," + myModel_MultiFenXiaoLevel.ManagerAgentGet + "";
                            strBody += "						<li>所得数：" + myModel_MultiFenXiaoLevel.ManagerAgentGet + "%</li>\n"; ;
                        }
                        else if (intArgUserID == strParentID.toInt32())
                        {
                            strmyGetSQLMySonMoneyArrayList += "," + myModel_MultiFenXiaoLevel.AgentGet + "";
                            strBody += "						<li>所得数：" + myModel_MultiFenXiaoLevel.AgentGet + "%</li>\n"; ;
                        }
                        else
                        {
                            #region 使div闭合
                            strBody += "				</ul></div>\n";
                            strBody += "			</div>\n";
                            strBody += "		</li></a>\n";
                            #endregion 使div闭合
                            continue;
                        }
                        #region  以下2个语句  耦合度高一点
                        if (intArgUserID == myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID)
                        {
                            string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID.ToString()) : "现金");
                            strBody += "						<li>" + strMoneyShow + "三级所得收入：" + Pub.getPubMoney(woshiGreatParent) + "￥</li>\n";
                            CountmyArgMoney += woshiGreatParent;
                            strmyGetSQLMySonMoneyArrayList += "," + woshiGreatParent + ",'" + strMoneyShow + "三级所得收入:";
                        }
                        else if (intArgUserID == myModel_MultiFenXiaoLevel.ManagerAgentParentID)
                        {
                            string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(myModel_MultiFenXiaoLevel.ManagerAgentParentID.ToString()) : "现金");
                            strBody += "						<li>" + strMoneyShow + "二级所得收入：" + Pub.getPubMoney(woshiGrandParent) + "￥</li>\n";
                            CountmyArgMoney += woshiGrandParent;
                            strmyGetSQLMySonMoneyArrayList += "," + woshiGrandParent + ",'" + strMoneyShow + "二级所得收入：";
                        }
                        else if (intArgUserID == strParentID.toInt32())
                        {
                            string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(strParentID.ToString()) : "现金");
                            strBody += "						<li>" + strMoneyShow + "一级所得收入：" + Pub.getPubMoney(woshiParent) + "￥</li>\n";
                            CountmyArgMoney += woshiParent;
                            strmyGetSQLMySonMoneyArrayList += "," + woshiParent + ",'" + strMoneyShow + "一级所得收入：";
                        }
                        else
                        {
                            #region 使div闭合
                            strBody += "				</ul></div>\n";
                            strBody += "			</div>\n";
                            strBody += "		</li></a>\n";
                            #endregion 使div闭合

                            continue;////订单中的父系  与 生活中的父系 发生聊改变 谁都不计

                        }

                        bool boolOver7DaysToBeans = false;
                        bool.TryParse(strOver7DaysToBeans, out boolOver7DaysToBeans);
                        if (boolOver7DaysToBeans)
                        {
                            strBody += "<li>本收入已转化到帐户</li>";
                            strmyGetSQLMySonMoneyArrayList += "本收入已转化到帐户',1";
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(strDeliveryText) == true)
                            {
                                strBody += "<li>未发货,代理收入未转化</li>";
                                strmyGetSQLMySonMoneyArrayList += "未发货,代理收入未转化',0";
                            }
                            else
                            {
                                strBody += "<li>未确认收货,代理收入未转化</li>";
                                strmyGetSQLMySonMoneyArrayList += "未确认收货,代理收入未转化',0";
                            }
                        }
                        #endregion

                        strmyGetSQLMySonMoneyArrayList += "," + Modeltab_Goods.ShopClient_ID;

                        strmyGetSQLMySonMoneyArrayList += ")";

                        strBody += "				</ul></div>\n";
                        strBody += "			</div>\n";
                        strBody += "		</li></a>\n";

                        if (myGetSQLMySonMoneyArrayList != null) myGetSQLMySonMoneyArrayList.Add(strmyGetSQLMySonMoneyArrayList);
                    }
                }
            }
            else
            {
                strBody = "暂无数据";
            }

            myArgMoney = CountmyArgMoney;
            return strBody;
        }




    }

}