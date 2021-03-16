using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Data;
using Eggsoft.Common;
using System.Xml;

///// <summary>
/////ClassP 的摘要说明
///// </summary>
///// 
//public class FahuoDan
//{
//    public string FaHuoGongSi { get; set; }
//    public string FaHuoDanHao { get; set; }
//    public string ShouHuoRenXinMing { get; set; }
//    public string ShouHuoRenDianHua { get; set; }
//    public string ShouHuoRenDiZhi { get; set; }
//    public string FaHuoRenXingMing { get; set; }
//    public string FaHuoRenXDianHua { get; set; }
//    public string FaHuoRenDiZhi { get; set; }   
//}

namespace Eggsoft_Public_CL
{
    /// <summary>
    /// 处理订单 等相关  例如退款
    /// </summary>
    public class FaHuoOrderDo
    {
        private static object lockmyOrderDoCancel = new object();
        public static String OrderDoCancel(int OrderID, string strShopClientID, Decimal DecimalReturnMoney)
        {
            lock (lockmyOrderDoCancel)
            {
                EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();

                #region 当然要归还钱了
                string strReturnMoneyDesc = "";///原路退回失败，请手动处理。订单已成功删除
                bool boolISCanDeleteOrder = false;

                #region 查找已退金额
                Decimal myHavedReturnDecimal = 0;
                EggsoftWX.BLL.b012_RefundMoney my_BLL_b012_RefundMoney = new EggsoftWX.BLL.b012_RefundMoney();
                EggsoftWX.Model.b012_RefundMoney my_Model_b012_RefundMoney = new EggsoftWX.Model.b012_RefundMoney();
                System.Data.DataTable Data_DataTable_returnMoney = my_BLL_b012_RefundMoney.SelectList("select sum(returnMoney) from b012_RefundMoney where orderID=@orderID and success=1 and ShopClient_ID=@ShopClient_ID", OrderID, strShopClientID.toInt32()).Tables[0];
                if (Data_DataTable_returnMoney.Rows.Count > 0) myHavedReturnDecimal = Data_DataTable_returnMoney.Rows[0][0].toDecimal();
                #endregion 查找已退金额


                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = BLL_tab_Order.GetModel(OrderID);

                #region 注销已支付未读消息 运营中心订单的取消
                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                bll_b011_InfoAlertMessage.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and (Type='Info_cart' or Type='Info_cart_good' or Type='Info_cart_good2' or Type='Info_cart_good3' or Type='Info_myYunYingOrder') and Readed=0 and TypeTableID=" + my_Model_tab_Order.ID, my_Model_tab_Order.UserID, my_Model_tab_Order.ShopClient_ID);
                #endregion  注销已支付未读消息 运营中心订单的取消


                if (DecimalReturnMoney > 0)
                {
                    if (Decimal.Round(Convert.ToDecimal(my_Model_tab_Order.TotalMoney), 2) > 0 && my_Model_tab_Order.TotalMoney >= DecimalReturnMoney && my_Model_tab_Order.TotalMoney >= myHavedReturnDecimal)
                    {

                        #region 生成退单号码
                        //String strwebuy8_ClientAdmin_Users_ClientUserAccount = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users_ClientUserAccount");


                        my_Model_b012_RefundMoney.ShopClient_ID = strShopClientID.toInt32();
                        my_Model_b012_RefundMoney.orderID = OrderID;
                        my_Model_b012_RefundMoney.returnMoney = DecimalReturnMoney;
                        my_Model_b012_RefundMoney.CreateBy = strShopClientID;
                        Int32 NumberInt32 = my_BLL_b012_RefundMoney.Add(my_Model_b012_RefundMoney);
                        my_Model_b012_RefundMoney.RefundNumber = Eggsoft.Common.StringNum.Add000000Num(strShopClientID.toInt32(), 6) + Eggsoft.Common.StringNum.Add000000Num(NumberInt32, 6);
                        my_Model_b012_RefundMoney.UpdateTime = DateTime.Now;
                        my_BLL_b012_RefundMoney.Update(my_Model_b012_RefundMoney);

                        #endregion

                        #region 原路退回                   
                        string strSafeCode = "uiajkwefdhaskdfasdjfaskfdhkasdfasd";////api通讯用
                        string strAAAMe = Eggsoft.Common.DESCrypt.GetMd5Str32(OrderID.toString() + strShopClientID + DecimalReturnMoney + my_Model_b012_RefundMoney.RefundNumber + strSafeCode);


                        EggsoftWX.BLL.b012_RefundMoney BLL_b012_RefundMoney = new EggsoftWX.BLL.b012_RefundMoney();


                        string strvarGetAppConfiugServicesURL = Eggsoft_Public_CL.Pub.GetAppConfiugServicesURL();
                        // string strINCID = Eggsoft.Common.Session.Read("webuy8_ClientAdmin_Users").ToString();
                        var urlasmx = strvarGetAppConfiugServicesURL + "/Order/DoOrder.asmx/refundMoney?OrderID=" + OrderID + "&ShopClient_ID=" + strShopClientID + "&RefundMoney=" + DecimalReturnMoney + "&RefundNumber=" + my_Model_b012_RefundMoney.RefundNumber + "&SafeCode=" + strAAAMe;
                        Eggsoft.Common.debug_Log.Call_WriteLog(urlasmx, "原路返回功能", "访问网址");

                        string strresult = "";
                        try
                        {
                            strresult = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(urlasmx);
                        }
                        catch (Exception eeee)
                        {
                            Eggsoft.Common.debug_Log.Call_WriteLog(eeee, "原路退回远程服务访问");
                            boolISCanDeleteOrder = true;
                            strReturnMoneyDesc = "退回失败,请联系统管理员：" + strresult;
                            my_Model_b012_RefundMoney.Description = strresult;
                            my_Model_b012_RefundMoney.UpdateTime = DateTime.Now;
                            my_Model_b012_RefundMoney.UpdateBy = strShopClientID;
                        }
                        Eggsoft.Common.debug_Log.Call_WriteLog(strresult, "撤销订单", "返回数据");
                        if (string.IsNullOrEmpty(strresult))
                        {

                        }
                        else
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(strresult);
                            XmlNamespaceManager xnm = new XmlNamespaceManager(doc.NameTable);
                            xnm.AddNamespace("mxh", "http://tempuri.org/");

                            XmlNode dddd = doc.SelectSingleNode(@"/mxh:string", xnm);
                            if (dddd != null)
                            {
                                strresult = dddd.InnerText;
                            }


                            if (strresult == "SUCCESS")
                            {
                                boolISCanDeleteOrder = true;
                                my_Model_b012_RefundMoney.success = true;
                                my_Model_b012_RefundMoney.UpdateTime = DateTime.Now;
                                my_Model_b012_RefundMoney.UpdateBy = strShopClientID;

                                strReturnMoneyDesc = "财付通原路退回成功。退回金额" + DecimalReturnMoney + "元";
                            }
                            else
                            {
                                //INTsHOWaLertime = 20;
                                boolISCanDeleteOrder = false;
                                strReturnMoneyDesc = "退回失败,请联系统管理员：" + strresult;
                                my_Model_b012_RefundMoney.Description = strresult;
                                my_Model_b012_RefundMoney.UpdateTime = DateTime.Now;
                                my_Model_b012_RefundMoney.UpdateBy = strShopClientID;
                            }
                        }

                        my_BLL_b012_RefundMoney.Update(my_Model_b012_RefundMoney);
                        #endregion 原路退回
                    }


                }
                else
                {
                    boolISCanDeleteOrder = true;
                    strReturnMoneyDesc = "申请退款金额为0,不需要原路退回,订单已经删除";
                }
                #endregion

                if (boolISCanDeleteOrder)
                {
                    #region //归还库存 记录 购物券

                    EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                    EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

                    System.Data.DataTable Data_DataTable = BLL_tab_Orderdetails.GetList("OrderID=" + OrderID + " and isdeleted<>1 order by id asc").Tables[0];

                    for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                    {
                        string strOrderDetailID = Data_DataTable.Rows[i]["ID"].ToString();
                        string strGoodIDCountID = Data_DataTable.Rows[i]["OrderCount"].ToString();
                        string strGoodID = Data_DataTable.Rows[i]["GoodID"].ToString();
                        string strVouchersNum_List = Data_DataTable.Rows[i]["VouchersNum_List"].ToString();
                        string strBeans = Data_DataTable.Rows[i]["Beans"].ToString();
                        string strMoneyCredits = Data_DataTable.Rows[i]["MoneyCredits"].ToString();
                        string strMoneyWeBuy8Credits = Data_DataTable.Rows[i]["MoneyWeBuy8Credits"].ToString();
                        string strMoneyWealth = Data_DataTable.Rows[i]["WealthMoney"].ToString();



                        EggsoftWX.BLL.tab_Goods my_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        my_tab_Goods.Update("KuCunCount=KuCunCount+" + strGoodIDCountID, "id=" + strGoodID);
                        //#region 同步减少代理商的发货数量  因为代理商是免支付  必须记住一个数量  有利于程序的伸缩
                        //EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                        //EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("UserID=" + my_Model_tab_Order.UserID + " and ProductID=" + strGoodID + " and Empowered=1");
                        //if (Model_tab_ShopClient_Agent__ProductClassID != null)
                        //{
                        //    Model_tab_ShopClient_Agent__ProductClassID.StockNum_MeHavebuyNum -= Int32.Parse(strGoodIDCountID);
                        //    BLL_tab_ShopClient_Agent__ProductClassID.Update(Model_tab_ShopClient_Agent__ProductClassID);
                        //}
                        //#endregion

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


                        #region---归还财富积分

                        if (strMoneyWealth.toDecimal() > 0)
                        {
                            //intThisCartID
                            EggsoftWX.BLL.b015_OrderDetail_WealthBuy BLL_b015_OrderDetail_WealthBuy = new EggsoftWX.BLL.b015_OrderDetail_WealthBuy();
                            System.Data.DataTable Data_DataTable_015 = BLL_b015_OrderDetail_WealthBuy.GetList("userid=@userid and WealthDetailID=@WealthDetailID and UseOrNotuse=1", Convert.ToInt32(my_Model_tab_Order.UserID), strOrderDetailID).Tables[0];
                            for (int j = 0; j < Data_DataTable_015.Rows.Count; j++)
                            {
                                Int32 Int32b015_OrderDetail_WealthBuy = Data_DataTable_015.Rows[j]["ID"].toInt32();
                                Int32 OrdetailID = Data_DataTable_015.Rows[j]["OrdetailID"].toInt32();
                                string strBuyDesc = Data_DataTable_015.Rows[j]["CreateBy"].toString();
                                Decimal DecimalHowMuchWealth = Data_DataTable_015.Rows[j]["HowMuchWealth"].toDecimal();

                                EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                                EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                                Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = true;
                                Model_b006_TotalWealth_OperationUser.OrderDetailID = OrdetailID;
                                Model_b006_TotalWealth_OperationUser.UserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                                Model_b006_TotalWealth_OperationUser.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                                Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalHowMuchWealth;
                                Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "取消已支付订单" + strBuyDesc;
                                int intTableID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

                                #region 增加财富积分未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage_b006_TotalWealth = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage_b006_TotalWealth = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage_b006_TotalWealth.InfoTip = "取消已支付订单" + strBuyDesc;
                                Model_b011_InfoAlertMessage_b006_TotalWealth.CreateBy = "取消已支付订单";
                                Model_b011_InfoAlertMessage_b006_TotalWealth.UpdateBy = "取消已支付订单";
                                Model_b011_InfoAlertMessage_b006_TotalWealth.UserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                                Model_b011_InfoAlertMessage_b006_TotalWealth.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                                Model_b011_InfoAlertMessage_b006_TotalWealth.Type = "Info_TotalWealth";
                                Model_b011_InfoAlertMessage_b006_TotalWealth.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage_b006_TotalWealth.Add(Model_b011_InfoAlertMessage_b006_TotalWealth);
                                #endregion 增加财富积分未处理信息  


                                EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum my_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                                EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = my_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and OrderDetailID=@OrderDetailID and ShopClient_ID=@ShopClient_ID", Convert.ToInt32(my_Model_tab_Order.UserID), OrdetailID, my_Model_tab_Order.ShopClient_ID);
                                if (Model_b008_OpterationUserActiveReturnMoneyOrderNum != null)
                                {
                                    if (Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum.toInt32() == 0)
                                    {
                                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.ActiveOrderNum = Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum.toInt32();
                                        Model_b008_OpterationUserActiveReturnMoneyOrderNum.OutHadGivedUserNum = 0;
                                    }
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit = Model_b008_OpterationUserActiveReturnMoneyOrderNum.ReturnMoneyUnit.toDecimal() + DecimalHowMuchWealth;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateTime = DateTime.Now;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy = Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge;
                                    Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss = Model_b008_OpterationUserActiveReturnMoneyOrderNum.Logss.toString() + Model_b008_OpterationUserActiveReturnMoneyOrderNum.UpdateBy;
                                    my_b008_OpterationUserActiveReturnMoneyOrderNum.Update(Model_b008_OpterationUserActiveReturnMoneyOrderNum);
                                }

                                EggsoftWX.Model.b015_OrderDetail_WealthBuy Model_b015_OrderDetail_WealthBuy = BLL_b015_OrderDetail_WealthBuy.GetModel(Int32b015_OrderDetail_WealthBuy);
                                Model_b015_OrderDetail_WealthBuy.UseOrNotuse = false;
                                Model_b015_OrderDetail_WealthBuy.CreateBy = "来自未支付订单";
                                Model_b015_OrderDetail_WealthBuy.UpdateBy = "取消已支付订单";
                                BLL_b015_OrderDetail_WealthBuy.Add(Model_b015_OrderDetail_WealthBuy);
                            }

                        }
                        #endregion---归还财富积分

                        #region---归还微店豆

                        //
                        if (strBeans == "0") strBeans = "";
                        Decimal Beans = 0;
                        Decimal.TryParse(strBeans, out Beans);

                        if (Beans > 0)
                        {
                            EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge MeBLL_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge MeModel_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge();
                            MeModel_tab_TotalBeans_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            MeModel_tab_TotalBeans_Consume_Or_Recharge.ConsumeOrRechargeBean = Int32.Parse(strBeans);
                            MeModel_tab_TotalBeans_Consume_Or_Recharge.ConsumeTypeOrRecharge = "已支付订单取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                            MeModel_tab_TotalBeans_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                            MeModel_tab_TotalBeans_Consume_Or_Recharge.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                            MeBLL_tab_TotalBeans_Consume_Or_Recharge.Add(MeModel_tab_TotalBeans_Consume_Or_Recharge);
                        }
                        #endregion

                        #region---归还购物红包
                        if (strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";
                        Decimal MoneyWeBuy8Credits = 0;
                        Decimal.TryParse(strMoneyWeBuy8Credits, out MoneyWeBuy8Credits);
                        if (Decimal.Round(MoneyWeBuy8Credits, 2) > 0)
                        {
                            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge MeBLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge MeModel_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                            MeModel_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            MeModel_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = MoneyWeBuy8Credits;
                            MeModel_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "已支付订单取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                            MeModel_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                            MeModel_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                            int intTableID = MeBLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(MeModel_tab_Total_Vouchers_Consume_Or_Recharge);


                            #region 增加未处理信息
                            //EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = MeModel_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;

                            Model_b011_InfoAlertMessage.CreateBy = MeModel_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.UpdateBy = MeModel_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.UserID = MeModel_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = MeModel_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                            Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                            Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加未处理信息 
                        }
                        #endregion


                        #region---归还金额
                        if (strMoneyCredits == "0.00") strMoneyCredits = "";
                        Decimal MoneyCredits = 0;
                        Decimal.TryParse(strMoneyCredits, out MoneyCredits);
                        if (Decimal.Round(MoneyCredits, 2) > 0)
                        {
                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge MeBLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge MeModel_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                            MeModel_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 91;
                            MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = MoneyCredits;
                            MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "已支付订单取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                            MeModel_tab_TotalCredits_Consume_Or_Recharge.UserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                            MeModel_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                            int intTableID = MeBLL_tab_TotalCredits_Consume_Or_Recharge.Add(MeModel_tab_TotalCredits_Consume_Or_Recharge);

                            #region 增加未处理信息
                            //EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.CreateBy = MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.UpdateBy = MeModel_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.UserID = MeModel_tab_TotalCredits_Consume_Or_Recharge.UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = MeModel_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                            Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                            Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加未处理信息 
                        }
                        #endregion
                    }
                    #endregion
                    //BLL_tab_Order.Delete(OrderID);
                    BLL_tab_Order.Update("PayStatus=3,UpdateDateTime=getdate(),IsDeleted=1", "ID=@ID", OrderID.toInt32());
                    BLL_tab_Orderdetails.Delete("OrderID=" + OrderID);
                }


                return strReturnMoneyDesc;
            }
        }
    }

    public class MultiPhotoSet_DataTable
    {
        public static DataTable getMultiTable()
        {
            DataTable mtReadTable = new DataTable();

            DataColumn column;

            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "_stringTileName";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the Column to the DataColumnCollection.
            mtReadTable.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "_stringFilename";
            column.AutoIncrement = false;
            column.ReadOnly = true;
            column.Unique = false;
            // Add the column to the table.
            mtReadTable.Columns.Add(column);

            return mtReadTable;
        }
    }

    public class XmlHelper_Instance_Online
    {

        #region XmlHelper_Instance_Online
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,

        private bool _BoolNeedCheck;
        private bool _NeedWrite_NotChoiceShengFeng;
        private DateTime _NeedWrite_DeadlineTime;


        /// <summary>
        /// 
        /// </summary>
        public bool BoolNeedCheck
        {
            set { _BoolNeedCheck = value; }
            get { return _BoolNeedCheck; }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool NeedWrite_NotChoiceShengFeng
        {
            set { _NeedWrite_NotChoiceShengFeng = value; }
            get { return _NeedWrite_NotChoiceShengFeng; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime NeedWrite_DeadlineTime
        {
            set { _NeedWrite_DeadlineTime = value; }
            get { return _NeedWrite_DeadlineTime; }
        }


        #endregion Model XmlHelper_Instance_Custom_XMLAdvance_LightApp
    }


    public class XmlHelper_Instance_Custom_XMLAdvance_ZhuanZhuanChe
    {







        #region Model XML_Custom_XMLAdvance_GuaGuaKa_ZhuanZhuanChe
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,
        private bool _boolEnable;


        private int _intValue0;
        private int _intValue1;
        private int _intValue2;
        private int _intValue3;
        private int _intValue4;
        private int _intALLCountValue;
        private int _intSecondGetBonusTime;

        private string _stringValue0;
        private string _stringValue1;
        private string _stringValue2;
        private string _stringValue3;
        private string _stringValue4;


        /// <summary>
        /// 
        /// </summary>
        public bool boolEnable
        {
            set { _boolEnable = value; }
            get { return _boolEnable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int intValue0
        {
            set { _intValue0 = value; }
            get { return _intValue0; }
        }
        // <summary>
        /// 
        /// </summary>
        public int intValue1
        {
            set { _intValue1 = value; }
            get { return _intValue1; }
        }
        // <summary>
        /// 
        /// </summary>
        public int intValue2
        {
            set { _intValue2 = value; }
            get { return _intValue2; }
        }
        // <summary>
        /// 
        /// </summary>
        public int intValue3
        {
            set { _intValue3 = value; }
            get { return _intValue3; }
        }
        // <summary>
        /// 
        /// </summary>
        public int intValue4
        {
            set { _intValue4 = value; }
            get { return _intValue4; }
        }
        // <summary>
        /// 
        /// </summary>
        public int intALLCountValue
        {
            set { _intALLCountValue = value; }
            get { return _intALLCountValue; }
        }

        // <summary>
        /// 第二次有效抽奖时间
        /// </summary>
        public int intSecondGetBonusTime
        {
            set { _intSecondGetBonusTime = value; }
            get { return _intSecondGetBonusTime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string stringValue0
        {
            set { _stringValue0 = value; }
            get { return _stringValue0; }
        }
        // <summary>
        /// 
        /// </summary>
        public string stringValue1
        {
            set { _stringValue1 = value; }
            get { return _stringValue1; }
        }
        // <summary>
        /// 
        /// </summary>
        public string stringValue2
        {
            set { _stringValue2 = value; }
            get { return _stringValue2; }
        }
        // <summary>
        /// 
        /// </summary>
        public string stringValue3
        {
            set { _stringValue3 = value; }
            get { return _stringValue3; }
        }
        // <summary>
        /// 
        /// </summary>
        public string stringValue4
        {
            set { _stringValue4 = value; }
            get { return _stringValue4; }
        }
        #endregion Model XML_Custom_XMLAdvance_GuaGuaKa_ZhuanZhuanChe
    }

    public class FahuoDan
    {
        public static String Pub_Take__PathBarCode(int UserID, int intShopClientID, out string stringHttp)
        {
            //int intShopClientID = Pub.GetShopClientIDFromOrderID(OrderID);
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient  = BLL_tab_ShopClient.GetModel(intShopClientID);
            string strMapPath = "/QRCodeImage/TakeMiniProgramBarCode"+ UserID + ".jpg";
            string ls_Des_fileName = Model_tab_ShopClient.UpLoadPath + strMapPath;
            stringHttp = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + ls_Des_fileName;
            return ls_Des_fileName;

        }
        public static String Pub_Take__Path(int UserID, int intShopClientID, out string stringHttp)
        {
            //int intShopClientID = Pub.GetShopClientIDFromOrderID(OrderID);
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);
            string strMapPath = "/QRCodeImage/TakeMiniProgram" + UserID + ".jpg";
            string ls_Des_fileName = Model_tab_ShopClient.UpLoadPath + strMapPath;
            stringHttp = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + ls_Des_fileName;
            return ls_Des_fileName;

        }
        public static String Pub_Take_Goods_Path(int OrderID, out string stringHttp)
        {
            int intShopClientID = Pub.GetShopClientIDFromOrderID(OrderID);
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);
            string strMapPath = "/QRCodeImage/TakeGood" + OrderID + ".jpg";
            string ls_Des_fileName = Model_tab_ShopClient.UpLoadPath + strMapPath;
            stringHttp = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + ls_Des_fileName;
            return ls_Des_fileName;

        }

        public FahuoDan()
        { }
        #region Model
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,

        private string _FaHuoGongSi;
        private string _FaHuoDanHao;
        private string _ShouHuoRenXinMing;
        private string _ShouHuoRenDianHua;
        private string _ShouHuoRenDiZhi;
        private string _FaHuoRenXingMing;
        private string _FaHuoRenXDianHua;
        private string _FaHuoRenDiZhi;


        /// <summary>
        /// 
        /// </summary>
        public string FaHuoGongSi
        {
            set { _FaHuoGongSi = value; }
            get { return _FaHuoGongSi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaHuoDanHao
        {
            set { _FaHuoDanHao = value; }
            get { return _FaHuoDanHao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShouHuoRenXinMing
        {
            set { _ShouHuoRenXinMing = value; }
            get { return _ShouHuoRenXinMing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShouHuoRenDianHua
        {
            set { _ShouHuoRenDianHua = value; }
            get { return _ShouHuoRenDianHua; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShouHuoRenDiZhi
        {
            set { _ShouHuoRenDiZhi = value; }
            get { return _ShouHuoRenDiZhi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaHuoRenXingMing
        {
            set { _FaHuoRenXingMing = value; }
            get { return _FaHuoRenXingMing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaHuoRenXDianHua
        {
            set { _FaHuoRenXDianHua = value; }
            get { return _FaHuoRenXDianHua; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaHuoRenDiZhi
        {
            set { _FaHuoRenDiZhi = value; }
            get { return _FaHuoRenDiZhi; }
        }

        #endregion Model
    }

    /// <summary>
    /// 聚合数据物流查询
    /// </summary>
    public class FahuoDanJsonExp
    {

        /// <summary>
        ///    "datetime":"2013-06-25 10:44:05",	/*时间*/
        //"remark":"已收件",					/*描述*/
        //"zone":"台州市"						/*区域*/
        /// </summary>
        public class resultList
        {
            /// <summary>
            /// /*时间*/
            /// </summary>
            public string datetime { get; set; }
            /// <summary>
            /// /*描述*/
            /// </summary>
            public string remark { get; set; }
            /// <summary>
            /// /*区域*/
            /// </summary>
            public string zone { get; set; }
        }

        public class resultOneLine
        {
            /// <summary>
            /// "company":"顺丰",
            /// </summary>
            public string company { get; set; }
            /// <summary>
            /// "com":"sf",
            /// </summary>
            public string com { get; set; }
            /// <summary>
            /// "no":"575677355677",
            /// </summary>
            public string no { get; set; }
            public resultList[] list { get; set; }

        }
        /// <summary>
        /// "resultcode":"200",/*返回标识码*/
        /// </summary>
        public string resultcode { get; set; }
        /// <summary>
        /// "reason":"查询成功!",
        /// </summary>
        public string reason { get; set; }
        public resultOneLine result { get; set; }

    }
}
