using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _03WAWapShop_Oliver.Status
{
    public partial class Default_DoOrderShow : System.Web.UI.Page
    {
        private static Object myLockDefault_DoOrderShow = new object();


        protected void Page_Load(object sender, EventArgs e)
        {
            doaction();
        }

        private void doaction()
        {
            try
            {

                lock (myLockDefault_DoOrderShow)
                {
                    //return;
                    int intShopClientID = Request.QueryString["ShopClientID"].toString().toInt32();
                    System.Web.HttpContext.Current.Response.Write("开始执行 超过7  或者 马上确认收货的" + DateTime.Now + "<br />");
                    // 21;////0 表示 制作所有的
                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(" EXEC sp_UpdateStats");///对当前数据库中所有用户定义表和内部表运行 UPDATE STATISTICS。

                    #region  给支付 超过7  并且 最后更新时间超过7 的 并且发过货的 。收货状态值修改为1
                    string strWhereUpdateDateTimeOver7Days = "update tab_Order set isReceipt=1,UpdateDateTime=getdate() where id in( select OrderID from [View_SalesGoods]  where  ((DateDiff(dd, UpdateDateTime, GetDate()) > 6))  and isnull(DeliveryText,'')<>'' and (PayStatus=1) and isReceipt=0)";
                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strWhereUpdateDateTimeOver7Days);
                    #endregion

                    #region  给运营中心的商品 4天没有发货的 自动设为收货状态
                    string strWhereUpdateDateTimeOver5Days = "update tab_Order set DeliveryText='系统自动发货',UpdateDateTime=getdate() where id in( select OrderID from [View_SalesGoods]  where  ((DateDiff(dd, PayDateTime, GetDate()) > 4))  and isnull(DeliveryText,'')='' and (PayStatus=1) and (GoodType=6))";
                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(strWhereUpdateDateTimeOver5Days);
                    #endregion

                    #region 取消购物车中 待付款中 超过一个月的商品
                    try
                    {
                        EggsoftWX.BLL.tab_Order_ShopingCart my_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                        System.Data.DataTable Data_DataTable = my_tab_Order_ShopingCart.SelectList("select USERID FROM [tab_Order_ShopingCart]  where IsDeleted=0 and DATEDIFF(DAY,CreatTime,GETDATE())>30 group by userid").Tables[0];
                        for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                        {
                            string strUserID = Data_DataTable.Rows[i]["USERID"].toString();
                            Eggsoft_Public_CL.ShoppingCart.ClearShoppingCart(true, strUserID.toInt32(), "一个月系统删除");



                        }


                        string strSelect = "select id from tab_Order where  PayStatus=0 and  IsDeleted=0 and DATEDIFF(DAY,CreatTime,GETDATE())>30  order by id desc";
                        Data_DataTable = my_tab_Order_ShopingCart.SelectList(strSelect).Tables[0];
                        for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                        {
                            string strOrderINT = Data_DataTable.Rows[i]["id"].toString();
                            Eggsoft_Public_CL.GoodP.DeleteOrder(strOrderINT, "一个月系统删除");
                        }



                    }
                    catch (Exception rrrr)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(rrrr, "每天更新取消购物车未支付");
                    }
                    #endregion



                    #region 取消超过1天的红包 退回红包至账户
                    try
                    {
                        EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers my_tab_tab_RedWallet_Money_Credits_Vouchers = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                        System.Data.DataTable Data_DataTableRedWallet_Money_Credits_Vouchers = my_tab_tab_RedWallet_Money_Credits_Vouchers.SelectList("select Type_Or_Money_Credits_Vouchers,Money,ID,HowmanyPeople,UserID,ShopClientID FROM [tab_RedWallet_Money_Credits_Vouchers]  where SendMoneyByRedBagID=0 and isnull(ISClosed,0)=0 and DATEDIFF(DAY,CreatTime,GETDATE())>1 and DATEDIFF(DAY,CreatTime,GETDATE())<30").Tables[0];////只处理 前29天的
                        for (int i = 0; i < Data_DataTableRedWallet_Money_Credits_Vouchers.Rows.Count; i++)
                        {

                            Int32 intMoney_Credits_Vouchers = Data_DataTableRedWallet_Money_Credits_Vouchers.Rows[i]["Type_Or_Money_Credits_Vouchers"].toInt32();
                            string strMoney = Data_DataTableRedWallet_Money_Credits_Vouchers.Rows[i]["Money"].toString();
                            string strShopClientID = Data_DataTableRedWallet_Money_Credits_Vouchers.Rows[i]["ShopClientID"].toString();
                            string strID = Data_DataTableRedWallet_Money_Credits_Vouchers.Rows[i]["ID"].toString();
                            string strUserID = Data_DataTableRedWallet_Money_Credits_Vouchers.Rows[i]["UserID"].toString();
                            string strHowmanyPeople = Data_DataTableRedWallet_Money_Credits_Vouchers.Rows[i]["HowmanyPeople"].toString();

                            System.Data.DataTable Data_Count = my_tab_tab_RedWallet_Money_Credits_Vouchers.SelectList("select count(1),sum(Money) FROM [tab_RedWallet_Money_Credits_Vouchers] where PID=" + strID).Tables[0];
                            int intHowManyGet = Data_Count.Rows[0][0].toInt32();
                            Decimal DecimalHowManyGet = Data_Count.Rows[0][1].toDecimal();

                            if (intHowManyGet == strHowmanyPeople.toInt32() || DecimalHowManyGet >= strMoney.toDecimal())////已经人数够了
                            {


                            }
                            else
                            {
                                Decimal DecimalKKKK = strMoney.toDecimal() - DecimalHowManyGet;
                                if (DecimalKKKK > 0)
                                {
                                    #region  红包DB记录中增加得到钱的记录  发给自己
                                    EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers IGet_Model_tab_RedWallet_Money_Credits = new EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers();

                                    IGet_Model_tab_RedWallet_Money_Credits.ShopClientID = strShopClientID.toInt32();
                                    IGet_Model_tab_RedWallet_Money_Credits.PID = Int32.Parse(strID);
                                    IGet_Model_tab_RedWallet_Money_Credits.UserID = strUserID.toInt32();
                                    IGet_Model_tab_RedWallet_Money_Credits.Money = DecimalKKKK;
                                    IGet_Model_tab_RedWallet_Money_Credits.UpdateTime = DateTime.Now;
                                    IGet_Model_tab_RedWallet_Money_Credits.Type_Or_Money_Credits_Vouchers = intMoney_Credits_Vouchers;
                                    int myID = my_tab_tab_RedWallet_Money_Credits_Vouchers.Add(IGet_Model_tab_RedWallet_Money_Credits);
                                    if (!(myID > 0))
                                    {
                                        Eggsoft.Common.debug_Log.Call_WriteLog(IGet_Model_tab_RedWallet_Money_Credits.toJsonString(), "取消超过1天的红包", "退回超过一天的金额失败");
                                        continue;
                                    }

                                    #endregion 红包中增加钱



                                    if (intMoney_Credits_Vouchers == 1)
                                    {
                                        if (Decimal.Round(DecimalKKKK, 2) > 0)
                                        {
                                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge Bll_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();

                                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = strUserID.toInt32();
                                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalKKKK;
                                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "红包退回(编号" + strID + ")";
                                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 71;
                                            Model_tab_TotalCredits_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                            int intmyID = Bll_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);
                                            if (!(intmyID > 0))
                                            {
                                                #region 增加未处理信息
                                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                                Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                                                Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                                                Model_b011_InfoAlertMessage.TypeTableID = intmyID;
                                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                                #endregion 增加未处理信息

                                                Eggsoft.Common.debug_Log.Call_WriteLog(Model_tab_TotalCredits_Consume_Or_Recharge.toJsonString(), "取消超过1天的红包", "退回失败");
                                            }
                                        }
                                    }
                                    else if (intMoney_Credits_Vouchers == 2)
                                    {
                                        if (Decimal.Round(DecimalKKKK, 2) > 0)
                                        {
                                            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge Bll_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();

                                            Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = strUserID.toInt32();
                                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalKKKK;
                                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "红包退回(编号" + strID + ")";
                                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                            int intmyID = Bll_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                                            if (!(intmyID > 0))
                                            {
                                                #region 增加未处理信息
                                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                                Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                                Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                                Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                                Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                                                Model_b011_InfoAlertMessage.ShopClient_ID = strShopClientID.toInt32();
                                                Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                                Model_b011_InfoAlertMessage.TypeTableID = intmyID;
                                                bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                                #endregion 增加未处理信息

                                                Eggsoft.Common.debug_Log.Call_WriteLog(Model_tab_Total_Vouchers_Consume_Or_Recharge.toJsonString(), "取消超过1天的红包", "退回失败");
                                            }
                                        }
                                    }

                                }
                            }
                            my_tab_tab_RedWallet_Money_Credits_Vouchers.Update("ISClosed=1,UpdateTime=getdate(),UpdateBy='系统过期处理'", "ID=" + strID);

                            //Eggsoft_Public_CL.ShoppingCart.ClearShoppingCart(true, strUserID.toInt32(), "一个月系统删除");
                        }






                    }
                    catch (Exception rrrr)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(rrrr, "取消超过1天的红包");
                    }
                    #endregion





                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSql(" EXEC sp_UpdateStats");///对当前数据库中所有用户定义表和内部表运行 UPDATE STATISTICS。

                    #region 给各个用户发钱 给支付 超过7  或者 马上确认收货的 分销真情的结算一下  并且Over7DaysToBeans=1。。。。如果是运营中心 必须是支付超过7天的 并且设置过发货的才结算
                    Eggsoft.Common.debug_Log.Call_WriteLog("DoOver7daysCountMySonMoney_UpdateEvertDay开始执行T+7  1", "每天更新");
                    Eggsoft_Public_CL.Pub_FenXiao.DoOver7daysCountMySonMoney_UpdateEvertDay(intShopClientID);
                    Eggsoft.Common.debug_Log.Call_WriteLog("DoOver7daysCountMySonMoney_UpdateEvertDay执行完毕T+7  1", "每天更新");
                    #endregion
                    System.Web.HttpContext.Current.Response.Write("结束执行 超过7  或者 马上确认收货的" + DateTime.Now + "<br />");

                    System.Web.HttpContext.Current.Response.Write("开始执行" + DateTime.Now + "<br />");
                    //#region 找出一个人的所有上线
                    //Eggsoft_Public_CL.Pub_Default_Status_User_AllLeader mmPub_Default_Status_User_AllLeader = new Eggsoft_Public_CL.Pub_Default_Status_User_AllLeader(intShopClientID);
                    //#endregion

                    #region  获取 一个 用户 的 所有 出售的 商品 信息 。包含  支付  未支付 发货 未发货。。。
                    //Eggsoft.Common.debug_Log.Call_WriteLog("Pub_Default_DoAllOrderDatailShow开始执行2", "每天更新");
                    //Eggsoft_Public_CL.Pub_Default_DoAllOrderDatailShow mmPub_Pub_Default_DoAllOrderDatailShow = new Eggsoft_Public_CL.Pub_Default_DoAllOrderDatailShow(intShopClientID);
                    //Eggsoft.Common.debug_Log.Call_WriteLog("Pub_Default_DoAllOrderDatailShow执行完毕2", "每天更新");
                    #endregion




                   

                    System.Web.HttpContext.Current.Response.Write("结束执行" + DateTime.Now + "<br />");
                }
            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception euuuu)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(euuuu, "每天更新", "程序报错");

            }
            finally { }



        }
    }
}