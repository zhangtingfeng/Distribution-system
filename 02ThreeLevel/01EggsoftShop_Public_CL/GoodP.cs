using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using Eggsoft.Common;

namespace Eggsoft_Public_CL
{
    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class GoodP
    {
        public GoodP()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取购物车数量 商品
        /// </summary>
        /// <param name="strOrderID"></param>
        /// <param name="strDesc"></param>
        /// <returns></returns>
        public static Int32 getOrder_ShopingCart(Int32 IntUserID, Int32 intShopClientID)
        {
            EggsoftWX.BLL.tab_Order_ShopingCart BLL_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
            int int32 = BLL_tab_Order_ShopingCart.SelectList("  select sum(GoodIDCount) as sum_GoodIDCount from tab_Order_ShopingCart where UserID=" + IntUserID + " and ShopClientID=" + intShopClientID + " and IsDeleted=0").Tables[0].Rows[0][0].toInt32();
            return int32;
        }



        public static void DeleteOrder(string strOrderID, string strDesc = "")
        {

            lock ("Lock0094509923458945084568" + strOrderID)
            {
                EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order Model_tab_Order = BLL_tab_Order.GetModel(Int32.Parse(strOrderID));
                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();


                EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail BLL_tab_Shopping_Vouchers = new EggsoftWX.BLL.tab_ShopClient_Shopping_VouchersScheme_Detail();
                EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail Model_tab_Shopping_Vouchers = new EggsoftWX.Model.tab_ShopClient_Shopping_VouchersScheme_Detail();

                System.Data.DataTable Data_DataTable_Orderdetails = BLL_tab_Orderdetails.GetList("OrderID=" + strOrderID + "  and isdeleted<>1 order by id asc").Tables[0];
                #region //归还库存 记录 购物券

                if (Data_DataTable_Orderdetails.Rows.Count > 0)
                {

                    for (int i = 0; i < Data_DataTable_Orderdetails.Rows.Count; i++)
                    {
                        string strOrderDetailID = Data_DataTable_Orderdetails.Rows[i]["ID"].ToString();
                        string strShopClient_ID = Data_DataTable_Orderdetails.Rows[i]["ShopClient_ID"].ToString();
                        string strGoodIDCountID = Data_DataTable_Orderdetails.Rows[i]["OrderCount"].ToString();
                        string strGoodID = Data_DataTable_Orderdetails.Rows[i]["GoodID"].ToString();
                        string strVouchersNum_List = Data_DataTable_Orderdetails.Rows[i]["VouchersNum_List"].ToString();
                        string strBeans = Data_DataTable_Orderdetails.Rows[i]["Beans"].ToString();
                        string strMoneyCredits = Data_DataTable_Orderdetails.Rows[i]["MoneyCredits"].ToString();
                        string strMoneyWeBuy8Credits = Data_DataTable_Orderdetails.Rows[i]["MoneyWeBuy8Credits"].ToString();
                        string strMoneyWealth = Data_DataTable_Orderdetails.Rows[i]["WealthMoney"].ToString();

                        EggsoftWX.BLL.tab_Goods my_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        my_tab_Goods.Update("UpdateTime=getdate(),KuCunCount=KuCunCount+" + strGoodIDCountID, "id=" + strGoodID);
                        //#region 同步减少代理商的发货数量  因为代理商是免支付  必须记住一个数量  有利于程序的伸缩
                        //EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                        //EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("UserID=" + pub_Int_Session_CurUserID + " and ProductID=" + strGoodID + " and Empowered=1");
                        //if (Model_tab_ShopClient_Agent__ProductClassID != null)
                        //{
                        //    Model_tab_ShopClient_Agent__ProductClassID.StockNum_MeHavebuyNum -= Int32.Parse(strGoodIDCountID);
                        //    BLL_tab_ShopClient_Agent__ProductClassID.Update(Model_tab_ShopClient_Agent__ProductClassID);
                        //}
                        //#endregion
                        #region---归还金额
                        int userID = Convert.ToInt32(Model_tab_Order.UserID);


                        if (strMoneyCredits == "0.00") strMoneyCredits = "";
                        if (String.IsNullOrEmpty(strMoneyCredits) == false)
                        {
                            if (Decimal.Round(Decimal.Parse(strMoneyCredits), 2) > 0 && userID > 0)
                            {
                                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Decimal.Parse(strMoneyCredits);
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "待付取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 90;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = userID;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_TotalCredits_Consume_Or_Recharge.CreatTime = DateTime.Now;
                                Model_tab_TotalCredits_Consume_Or_Recharge.UpdateTime = DateTime.Now;
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
                            }
                        }
                        #endregion





                        #region---归还wei8金额
                        if (strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";
                        if (String.IsNullOrEmpty(strMoneyWeBuy8Credits) == false)
                        {
                            if (Decimal.Round(Decimal.Parse(strMoneyWeBuy8Credits), 2) > 0 && userID > 0)
                            {
                                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Decimal.Parse(strMoneyWeBuy8Credits);
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "待付取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = userID;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
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
                            }
                        }
                        #endregion


                        #region---归还财富积分

                        if (strMoneyWealth.toDecimal() > 0)
                        {
                            //intThisCartID
                            EggsoftWX.BLL.b015_OrderDetail_WealthBuy BLL_b015_OrderDetail_WealthBuy = new EggsoftWX.BLL.b015_OrderDetail_WealthBuy();
                            System.Data.DataTable Data_DataTable = BLL_b015_OrderDetail_WealthBuy.GetList("userid=@userid and WealthDetailID=@WealthDetailID and UseOrNotuse=1", userID, strOrderDetailID).Tables[0];
                            for (int j = 0; j < Data_DataTable.Rows.Count; j++)
                            {
                                Int32 Int32b015_OrderDetail_WealthBuy = Data_DataTable.Rows[j]["ID"].toInt32();
                                Int32 OrdetailID = Data_DataTable.Rows[j]["OrdetailID"].toInt32();
                                string strBuyDesc = Data_DataTable.Rows[j]["CreateBy"].toString();
                                Decimal DecimalHowMuchWealth = Data_DataTable.Rows[j]["HowMuchWealth"].toDecimal();

                                EggsoftWX.BLL.b006_TotalWealth_OperationUser my_BLL_b006_TotalWealth_OperationUser = new EggsoftWX.BLL.b006_TotalWealth_OperationUser();
                                EggsoftWX.Model.b006_TotalWealth_OperationUser Model_b006_TotalWealth_OperationUser = new EggsoftWX.Model.b006_TotalWealth_OperationUser();
                                Model_b006_TotalWealth_OperationUser.Bool_ConsumeOrRecharge = true;
                                Model_b006_TotalWealth_OperationUser.OrderDetailID = OrdetailID;
                                Model_b006_TotalWealth_OperationUser.UserID = userID;
                                Model_b006_TotalWealth_OperationUser.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b006_TotalWealth_OperationUser.ConsumeOrRechargeWealth = DecimalHowMuchWealth;
                                Model_b006_TotalWealth_OperationUser.ConsumeTypeOrRecharge = "取消订单" + strBuyDesc;
                                int intTableID = my_BLL_b006_TotalWealth_OperationUser.Add(Model_b006_TotalWealth_OperationUser);

                                #region 增加财富积分未处理信息
                                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage_b006_TotalWealth = new EggsoftWX.BLL.b011_InfoAlertMessage();
                                EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage_b006_TotalWealth = new EggsoftWX.Model.b011_InfoAlertMessage();
                                Model_b011_InfoAlertMessage_b006_TotalWealth.InfoTip = "取消订单" + strBuyDesc;
                                Model_b011_InfoAlertMessage_b006_TotalWealth.CreateBy = "取消订单";
                                Model_b011_InfoAlertMessage_b006_TotalWealth.UpdateBy = "取消订单";
                                Model_b011_InfoAlertMessage_b006_TotalWealth.UserID = userID;
                                Model_b011_InfoAlertMessage_b006_TotalWealth.ShopClient_ID = strShopClient_ID.toInt32();
                                Model_b011_InfoAlertMessage_b006_TotalWealth.Type = "Info_TotalWealth";
                                Model_b011_InfoAlertMessage_b006_TotalWealth.TypeTableID = intTableID;
                                bll_b011_InfoAlertMessage_b006_TotalWealth.Add(Model_b011_InfoAlertMessage_b006_TotalWealth);
                                #endregion 增加财富积分未处理信息  


                                EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum my_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();
                                EggsoftWX.Model.b008_OpterationUserActiveReturnMoneyOrderNum Model_b008_OpterationUserActiveReturnMoneyOrderNum = my_b008_OpterationUserActiveReturnMoneyOrderNum.GetModel("UserID=@UserID and OrderDetailID=@OrderDetailID and ShopClient_ID=@ShopClient_ID", userID, OrdetailID, strShopClient_ID.toInt32());
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
                                Model_b015_OrderDetail_WealthBuy.CreateBy = "来自购物车";
                                Model_b015_OrderDetail_WealthBuy.UpdateBy = "取消订单";
                                BLL_b015_OrderDetail_WealthBuy.Add(Model_b015_OrderDetail_WealthBuy);
                            }

                        }
                        #endregion---归还财富积分

                        #region---归还-微店豆

                        //
                        if (strBeans == "0") strBeans = "";
                        if (String.IsNullOrEmpty(strBeans) == false && userID > 0)
                        {
                            if (Int32.Parse(strBeans) > 0)
                            {
                                EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge BLL_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalBeans_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge Model_tab_TotalBeans_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalBeans_Consume_Or_Recharge();
                                Model_tab_TotalBeans_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalBeans_Consume_Or_Recharge.ConsumeOrRechargeBean = Int32.Parse(strBeans);
                                Model_tab_TotalBeans_Consume_Or_Recharge.ConsumeTypeOrRecharge = "待付取消" + Eggsoft_Public_CL.GoodP.Get_GoodNameFromGoodID(Int32.Parse(strGoodID));
                                Model_tab_TotalBeans_Consume_Or_Recharge.UserID = userID;
                                Model_tab_TotalBeans_Consume_Or_Recharge.ShopClient_ID = strShopClient_ID.toInt32();
                                BLL_tab_TotalBeans_Consume_Or_Recharge.Add(Model_tab_TotalBeans_Consume_Or_Recharge);
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
                                    // Model_tab_Shopping_Vouchers.UserID = 0;
                                    Model_tab_Shopping_Vouchers.MoneyUsed = 0;
                                    Model_tab_Shopping_Vouchers.UpdateTime = DateTime.Now;
                                    BLL_tab_Shopping_Vouchers.Update(Model_tab_Shopping_Vouchers);
                                }
                            }

                        }
                        #endregion

                    }
                }

                #region 注销订单未读消息
                EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage001 = new EggsoftWX.BLL.b011_InfoAlertMessage();
                bll_b011_InfoAlertMessage001.Update("Readed=1,UpdateTime=getdate(),UpdateBy='userID='+convert(varchar(40),@userID)", "ShopClient_ID=@ShopClient_ID and userID=@userID and (Type='Info_cart_good2' or Type='Info_cart_good3' or Type='Info_cart_good') and Readed=0", Model_tab_Order.UserID, Model_tab_Order.ShopClient_ID);
                #endregion 注销订单未读消息

                BLL_tab_Order.Delete(Int32.Parse(strOrderID));///BLL_tab_Orderdetails 的删除 已经在tab_Order 的触发器中处理
                BLL_tab_Order.Update("UpdateDateTime=getdate(),PayStatus=2,UpdateBy=@UpdateBy", "ID=" + strOrderID, strDesc);
                #endregion
            }

        }



        /// <summary>
        /// 读取聚合的物流数据并写入数据库
        /// </summary>
        /// <param name="strKuaidiCompanyCode"></param>
        /// <param name="FaHuoDanHao"><myFahuoDan.FaHuoDanHao/param>
        /// <returns></returns>
        public static void ReadHTTPjuheWuLiu(int intOrderID, int intShopClientID)
        {
            EggsoftWX.BLL.tab_ShopClient_Order_KuaiDiQuery blltab_ShopClient_Order_KuaiDiQuery = new EggsoftWX.BLL.tab_ShopClient_Order_KuaiDiQuery();
            EggsoftWX.Model.tab_ShopClient_Order_KuaiDiQuery Modeltab_ShopClient_Order_KuaiDiQuery = new EggsoftWX.Model.tab_ShopClient_Order_KuaiDiQuery();
            EggsoftWX.BLL.tab_Order blltab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order Modeltab_Order = new EggsoftWX.Model.tab_Order();
            Modeltab_ShopClient_Order_KuaiDiQuery = blltab_ShopClient_Order_KuaiDiQuery.GetModel("OrderID=" + intOrderID);
            if (Modeltab_ShopClient_Order_KuaiDiQuery == null)
            {
                #region 查找物流信息 ，如找到 就写入数据库   找不到 也要写 因为要钱的  查询 要钱的
                string strString = "ShopClient_ID=" + intShopClientID + " and OrderID=" + intOrderID; ;
                Modeltab_Order = blltab_Order.GetModel(intOrderID);


                string getGetFaHuoXML = "";
                getGetFaHuoXML = HttpContext.Current.Server.HtmlDecode(Modeltab_Order.DeliveryText);
                string getGetFaHuoTitleXML = "";

                try
                {
                    if ((getGetFaHuoXML != "自动发货") && (getGetFaHuoXML.Trim().Length > 0))
                    {
                        Eggsoft_Public_CL.XML__Class_FahuoDan myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_FahuoDan>(getGetFaHuoXML, System.Text.Encoding.UTF8);

                        getGetFaHuoTitleXML += myFahuoDan.FaHuoGongSi + "#";
                        getGetFaHuoTitleXML += myFahuoDan.FaHuoDanHao + "#";
                        getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenXinMing + "#";
                        getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenDianHua + "#";
                        getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenDiZhi + "#";
                        getGetFaHuoTitleXML += myFahuoDan.FaHuoRenXingMing + "#";
                        getGetFaHuoTitleXML += myFahuoDan.FaHuoRenXDianHua + "#";
                        getGetFaHuoTitleXML += myFahuoDan.FaHuoRenDiZhi;
                        if (myFahuoDan.FaHuoDanHao.Length > 9)///快递运单至少10位
                        {
                            EggsoftWX.BLL.tab_KuaiDiCompany blltab_KuaiDiCompany = new EggsoftWX.BLL.tab_KuaiDiCompany();
                            EggsoftWX.Model.tab_KuaiDiCompany Modeltab_KuaiDiCompany = blltab_KuaiDiCompany.GetModel("KuaidiName='" + myFahuoDan.FaHuoGongSi + "'");
                            if (Modeltab_KuaiDiCompany != null)
                            {
                                if (Modeltab_KuaiDiCompany.ID <= 10)
                                {
                                    string strKuaidiCompanyCode = Modeltab_KuaiDiCompany.KuaidiCompanyCode;
                                    //                            名称	类型	必填	说明
                                    //com	string	是	需要查询的快递公司编号
                                    //no	string	是	需要查询的订单号
                                    //key	string	是	应用APPKEY(应用详细页查询)
                                    //dtype	string	否	返回数据的格式,xml或json，默认json
                                    string strexp = "http://v.juhe.cn/exp/index?key=f830cbbc48ebfae07637c77ebc7e9083&com=" + strKuaidiCompanyCode + "&no=" + myFahuoDan.FaHuoDanHao;
                                    string strstrexpJson = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strexp);
                                    // string strstrexpJson ="{\"resultcode\":\"200\",\"reason\":\"成功的返回\",\"result\":{\"company\":\"顺丰\",\"com\":\"sf\",\"no\":\"590871291216\",\"status\":1,\"list\":[{\"datetime\":\"2015-10-31 09:22\",\"remark\":\"顺丰速运 已收取快件\",\"zone\":\"\"},{\"datetime\":\"2015-10-31 09:22\",\"remark\":\"快件离开苏州澄林大闸蟹项目服务点,正发往 苏州阳澄湖集散中心\",\"zone\":\"\"},{\"datetime\":\"2015-10-31 10:41\",\"remark\":\"快件到达 苏州阳澄湖集散中心\",\"zone\":\"\"},{\"datetime\":\"2015-10-31 11:10\",\"remark\":\"快件离开苏州阳澄湖集散中心,正发往 南京总集散中心\",\"zone\":\"\"},{\"datetime\":\"2015-10-31 15:53\",\"remark\":\"快件到达 南京总集散中心\",\"zone\":\"\"},{\"datetime\":\"2015-10-31 15:53\",\"remark\":\"快件离开南京总集散中心,正发往 深圳集散中心\",\"zone\":\"\"},{\"datetime\":\"2015-11-01 00:16\",\"remark\":\"快件到达 深圳集散中心\",\"zone\":\"\"},{\"datetime\":\"2015-11-01 01:00\",\"remark\":\"快件离开深圳集散中心,正发往 佛山顺德集散中心\",\"zone\":\"\"},{\"datetime\":\"2015-11-01 03:09\",\"remark\":\"快件到达 佛山顺德集散中心\",\"zone\":\"\"},{\"datetime\":\"2015-11-01 03:54\",\"remark\":\"快件离开佛山顺德集散中心,正发往 佛山北滘服务点\",\"zone\":\"\"},{\"datetime\":\"2015-11-01 06:44\",\"remark\":\"快件到达 佛山北滘服务点\",\"zone\":\"\"},{\"datetime\":\"2015-11-01 07:41\",\"remark\":\"正在派送途中,请您准备签收(派件人:原达坤,电话:13798676524)\",\"zone\":\"\"},{\"datetime\":\"2015-11-01 11:09\",\"remark\":\"已签收,感谢使用顺丰,期待再次为您服务\",\"zone\":\"\"},{\"datetime\":\"2015-11-01 11:09\",\"remark\":\"在官网运单资料&amp;签收图,可查看签收人信息\",\"zone\":\"\"}]},\"error_code\":0}";
                                    Eggsoft_Public_CL.FahuoDanJsonExp mmmFahuoDanJsonExp = Eggsoft_Public_CL.JsonHelper.JsonDeserialize<Eggsoft_Public_CL.FahuoDanJsonExp>(strstrexpJson);
                                    string strresultcode = mmmFahuoDanJsonExp.resultcode;
                                    if (strresultcode == "200")
                                    { ///成功查询
                                        int intLength = mmmFahuoDanJsonExp.result.list.Length;
                                        {
                                            if (intLength > 0)
                                            {
                                                string strJSON = Eggsoft_Public_CL.JsonHelper.JsonSerializer<Eggsoft_Public_CL.FahuoDanJsonExp.resultList[]>(mmmFahuoDanJsonExp.result.list);
                                                Modeltab_ShopClient_Order_KuaiDiQuery = new EggsoftWX.Model.tab_ShopClient_Order_KuaiDiQuery();
                                                Modeltab_ShopClient_Order_KuaiDiQuery.JuHeResultList = strJSON;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.UpdateTime = DateTime.Now;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.FreeXML = strstrexpJson;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.QueryCount = 1;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.InsertTime = DateTime.Now;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.KuaiDiCompany = mmmFahuoDanJsonExp.result.company;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.KuaiDiNumber = mmmFahuoDanJsonExp.result.no;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.ShopClient_ID = Modeltab_Order.ShopClient_ID;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.UserID = Modeltab_Order.UserID;
                                                Modeltab_ShopClient_Order_KuaiDiQuery.OrderID = intOrderID;
                                                blltab_ShopClient_Order_KuaiDiQuery.Add(Modeltab_ShopClient_Order_KuaiDiQuery);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Modeltab_ShopClient_Order_KuaiDiQuery = new EggsoftWX.Model.tab_ShopClient_Order_KuaiDiQuery();
                                        Modeltab_ShopClient_Order_KuaiDiQuery.JuHeResultList = "查询失败" + mmmFahuoDanJsonExp.reason;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.UpdateTime = DateTime.Now;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.FreeXML = strstrexpJson;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.QueryCount = -9999;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.InsertTime = DateTime.Now;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.KuaiDiCompany = myFahuoDan.FaHuoGongSi;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.KuaiDiNumber = myFahuoDan.FaHuoDanHao;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.ShopClient_ID = Modeltab_Order.ShopClient_ID;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.UserID = Modeltab_Order.UserID;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.OrderID = intOrderID;
                                        blltab_ShopClient_Order_KuaiDiQuery.Add(Modeltab_ShopClient_Order_KuaiDiQuery);

                                        Eggsoft.Common.JsUtil.ShowMsg(mmmFahuoDanJsonExp.reason, -1);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception rrrr)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("intOrderID intShopClientID" + intOrderID.toString() + " " + intShopClientID.toString(), "读取聚合的物流数据");
                    Eggsoft.Common.debug_Log.Call_WriteLog(rrrr, "读取聚合的物流数据");
                }
                finally { }
                #endregion



            }
            else
            { ////执行到这里说明 聚合数据查询成功过
                ////  检查 12小时只能查询一次
                DateTime DateTimeLast = Convert.ToDateTime(Modeltab_ShopClient_Order_KuaiDiQuery.UpdateTime);
                TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTimeLast.Ticks);

                TimeSpan ts = ts1.Subtract(ts2).Duration();
                if ((ts.TotalHours > 11) || (Modeltab_ShopClient_Order_KuaiDiQuery.InsertTime > Modeltab_ShopClient_Order_KuaiDiQuery.UpdateTime))///检查 6小时只能查询一次  或者后台重复物流号码更新了  。重复更新会更新该数据。一个订单只有一个物流查询
                {////查询失败 也要查
                    //if (Modeltab_ShopClient_Order_KuaiDiQuery.QueryCount <= 0) return;////以前属于查询失败的 现在也别查了  浪费钱啊

                    #region 查找物流信息 ，如找到 就更新数据库
                    string strString = "ShopClient_ID=" + intShopClientID + " and OrderID=" + intOrderID; ;
                    Modeltab_Order = blltab_Order.GetModel(intOrderID);


                    string getGetFaHuoXML = "";
                    getGetFaHuoXML = HttpContext.Current.Server.HtmlDecode(Modeltab_Order.DeliveryText);
                    string getGetFaHuoTitleXML = "";

                    try
                    {
                        if (getGetFaHuoXML.Trim().Length > 0)
                        {
                            Eggsoft_Public_CL.XML__Class_FahuoDan myFahuoDan = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_FahuoDan>(getGetFaHuoXML, System.Text.Encoding.UTF8);

                            getGetFaHuoTitleXML += myFahuoDan.FaHuoGongSi + "#";
                            getGetFaHuoTitleXML += myFahuoDan.FaHuoDanHao + "#";
                            getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenXinMing + "#";
                            getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenDianHua + "#";
                            getGetFaHuoTitleXML += myFahuoDan.ShouHuoRenDiZhi + "#";
                            getGetFaHuoTitleXML += myFahuoDan.FaHuoRenXingMing + "#";
                            getGetFaHuoTitleXML += myFahuoDan.FaHuoRenXDianHua + "#";
                            getGetFaHuoTitleXML += myFahuoDan.FaHuoRenDiZhi;
                            if (myFahuoDan.FaHuoDanHao.Length > 9)///快递运单至少10位
                            {
                                EggsoftWX.BLL.tab_KuaiDiCompany blltab_KuaiDiCompany = new EggsoftWX.BLL.tab_KuaiDiCompany();
                                EggsoftWX.Model.tab_KuaiDiCompany Modeltab_KuaiDiCompany = blltab_KuaiDiCompany.GetModel("KuaidiName='" + myFahuoDan.FaHuoGongSi + "'");
                                if (Modeltab_KuaiDiCompany != null)
                                {
                                    if (Modeltab_KuaiDiCompany.ID <= 10)///系统可查的
                                    {
                                        string strKuaidiCompanyCode = Modeltab_KuaiDiCompany.KuaidiCompanyCode;
                                        string strexp = "http://v.juhe.cn/exp/index?key=f830cbbc48ebfae07637c77ebc7e9083&com=" + strKuaidiCompanyCode + "&no=" + myFahuoDan.FaHuoDanHao;
                                        string strstrexpJson = Eggsoft.Common.CommUtil.HttpWebRequest_WebRequest_GET_JSON(strexp);

                                        Eggsoft_Public_CL.FahuoDanJsonExp mmmFahuoDanJsonExp = Eggsoft_Public_CL.JsonHelper.JsonDeserialize<Eggsoft_Public_CL.FahuoDanJsonExp>(strstrexpJson);
                                        string strresultcode = mmmFahuoDanJsonExp.resultcode;
                                        if (strresultcode == "200")
                                        { ///成功查询
                                            int intLength = mmmFahuoDanJsonExp.result.list.Length;
                                            {
                                                if (intLength > 0)
                                                {
                                                    string strJSON = Eggsoft_Public_CL.JsonHelper.JsonSerializer<Eggsoft_Public_CL.FahuoDanJsonExp.resultList[]>(mmmFahuoDanJsonExp.result.list);
                                                    Modeltab_ShopClient_Order_KuaiDiQuery.JuHeResultList = strJSON;
                                                    Modeltab_ShopClient_Order_KuaiDiQuery.KuaiDiCompany = mmmFahuoDanJsonExp.result.company;
                                                    Modeltab_ShopClient_Order_KuaiDiQuery.KuaiDiNumber = mmmFahuoDanJsonExp.result.no;
                                                }
                                            }
                                        }
                                        Modeltab_ShopClient_Order_KuaiDiQuery.UpdateTime = DateTime.Now;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.QueryCount = Modeltab_ShopClient_Order_KuaiDiQuery.QueryCount + 1;
                                        Modeltab_ShopClient_Order_KuaiDiQuery.FreeXML = Modeltab_ShopClient_Order_KuaiDiQuery.FreeXML + strstrexpJson;
                                        blltab_ShopClient_Order_KuaiDiQuery.Update(Modeltab_ShopClient_Order_KuaiDiQuery);
                                    }
                                }
                            }
                        }
                    }

                    catch (Exception rrrr)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(rrrr);
                    }
                    finally { }
                    #endregion
                }
            }

        }
        private static Object updateAllAgentPercentLock = new Object();

        public static void updateAllAgentPercent(int intGoodID)
        {
            try
            {
                lock (updateAllAgentPercentLock)
                {
                    EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods Model_tab_Goods = BLL_tab_Goods.GetModel(intGoodID);
                    Decimal AgentPercent_Money = Convert.ToDecimal(Model_tab_Goods.AgentPercent);
                    Decimal PromotePrice_Money = Convert.ToDecimal(Model_tab_Goods.PromotePrice);
                    Decimal myPercent = AgentPercent_Money / PromotePrice_Money;
                    int intPub_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(intGoodID);

                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intPub_ShopClientID, 0, intGoodID);
                    int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();



                    //Decimal[] myAgentFenXiaoList = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intPub_ShopClientID,0);

                    #region 顶级代理模式
                    //EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                    //EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + intPub_ShopClientID);
                    //bool boolTopSales = true;
                    //if (Model_tab_ShopClient_ShopPar != null)
                    //{
                    //    boolTopSales = Model_tab_ShopClient_ShopPar.TopAgent.toBoolean();
                    //}
                    #endregion

                    //if (intLength > 0)
                    //{
                    //    Model_tab_Goods.Price_Percent = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet * myPercent * 100;
                    //}
                    //if (intLength > 1)
                    //{
                    //    Model_tab_Goods.Price_Percent1 = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet * myPercent * 100;
                    //}
                    //if (intLength > 2)
                    //{
                    //    Model_tab_Goods.Price_Percent2 = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet * myPercent * 100;
                    //}

                    BLL_tab_Goods.Update(Model_tab_Goods);

                    ///  如果没有代理 就为其新增代理
                    ///  
                    EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID BLL_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.BLL.tab_ShopClient_Agent__ProductClassID();
                    EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID Model_tab_ShopClient_Agent__ProductClassID;

                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

                    EggsoftWX.BLL.View_ShopClient_Agent BLL_View_ShopClient_Agent = new EggsoftWX.BLL.View_ShopClient_Agent();
                    System.Data.DataTable Data_DataTable = BLL_View_ShopClient_Agent.GetList("UserID,ShopUserID", "ShopClientID=" + Model_tab_Goods.ShopClient_ID + " order by id asc").Tables[0];

                    ArrayList ArrayListSQL = new ArrayList();

                    for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                    {
                        string strUserID = Data_DataTable.Rows[i]["UserID"].ToString();
                        string strShopUserID = Data_DataTable.Rows[i]["ShopUserID"].ToString();

                        bool boolAgent_ProductID_Exsit = BLL_tab_ShopClient_Agent__ProductClassID.Exists("UserID=" + strUserID + " and ProductID=" + intGoodID + " and ShopClientID=" + intPub_ShopClientID);
                        int intParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(Int32.Parse(strUserID));
                        int intGrandParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intParentID);
                        EggsoftWX.Model.tab_ShopClient_Agent_ Modeltab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("ShopClientID=" + intPub_ShopClientID + "and UserID=" + strUserID);


                        if (boolAgent_ProductID_Exsit)
                        {
                            Model_tab_ShopClient_Agent__ProductClassID = BLL_tab_ShopClient_Agent__ProductClassID.GetModel("UserID=" + strUserID + " and ProductID=" + intGoodID + " and ShopClientID=" + intPub_ShopClientID);
                           

                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("update [tab_ShopClient_Agent__ProductClassID] set ");
                            strSql.Append("[UserID]=" + Model_tab_ShopClient_Agent__ProductClassID.UserID + ",");
                            strSql.Append("[ProductID]=" + Model_tab_ShopClient_Agent__ProductClassID.ProductID + ",");
                            strSql.Append("[UpdateTime]='" + Convert.ToDateTime(Model_tab_ShopClient_Agent__ProductClassID.UpdateTime).ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
                            strSql.Append("[OnlyIsAngel]=" + (Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel.toInt32()) + ",");
                            strSql.Append("[Empowered]=" + (Model_tab_ShopClient_Agent__ProductClassID.Empowered.toInt32()) + ",");
                            ////去除Price_Percent Price_Percent1  Price_Percent2 2017  12 23
                            //strSql.Append("[Price_Percent]=" + Model_tab_ShopClient_Agent__ProductClassID.Price_Percent.toDecimal() + ",");
                            //strSql.Append("[Price_Percent1]=" + Model_tab_ShopClient_Agent__ProductClassID.Price_Percent1.toDecimal() + ",");
                            //strSql.Append("[Price_Percent2]=" + Model_tab_ShopClient_Agent__ProductClassID.Price_Percent2.toDecimal() + ",");
                            strSql.Append("[StockNum_MeHavebuyNum]=" + Model_tab_ShopClient_Agent__ProductClassID.StockNum_MeHavebuyNum.toInt32() + ",");
                            strSql.Append("[ProductRightNum]=" + Model_tab_ShopClient_Agent__ProductClassID.ProductRightNum.toInt32() + ",");
                            // strSql.Append("[Full_Vouchers_]=" + (Model_tab_ShopClient_Agent__ProductClassID.Full_Vouchers_.toInt32()) + ",");
                            strSql.Append("[ProductPrice]=" + Model_tab_ShopClient_Agent__ProductClassID.ProductPrice.toDecimal() + "");
                            strSql.Append(" where ID=" + Model_tab_ShopClient_Agent__ProductClassID.ID + "");
                            ArrayListSQL.Add(strSql.ToString());
                        }
                        else///没有代理这个商品 就自动给他代理了
                        { ///检查是否存在上级代理
                          ///
                            Model_tab_ShopClient_Agent__ProductClassID = new EggsoftWX.Model.tab_ShopClient_Agent__ProductClassID();
                            

                            Model_tab_ShopClient_Agent__ProductClassID.UserID = Int32.Parse(strUserID);
                            Model_tab_ShopClient_Agent__ProductClassID.ProductID = intGoodID;
                            Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel = Modeltab_ShopClient_Agent_.OnlyIsAngel;
                            Model_tab_ShopClient_Agent__ProductClassID.Empowered = Modeltab_ShopClient_Agent_.Empowered;
                            Model_tab_ShopClient_Agent__ProductClassID.ShopClientID = intPub_ShopClientID;
                            Model_tab_ShopClient_Agent__ProductClassID.UpdateTime = DateTime.Now;

                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("INSERT INTO  [tab_ShopClient_Agent__ProductClassID] (UserID,ShopClientID,ProductID,UpdateTime,OnlyIsAngel,Empowered,StockNum_MeHavebuyNum,ProductRightNum,ProductPrice)  values ");
                            strSql.Append("(" + Model_tab_ShopClient_Agent__ProductClassID.UserID + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ShopClientID + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductID + ",");
                            strSql.Append("'" + Convert.ToDateTime(Model_tab_ShopClient_Agent__ProductClassID.UpdateTime).ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.OnlyIsAngel.toInt32() + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.Empowered.toInt32() + ",");
                            //strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.Price_Percent.toDecimal() + ",");
                            //strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.Price_Percent1.toDecimal() + ",");
                            //strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.Price_Percent2.toDecimal() + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.StockNum_MeHavebuyNum.toInt32() + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductRightNum.toInt32() + ",");
                            //strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.Full_Vouchers_.toInt32() + ",");
                            strSql.Append(Model_tab_ShopClient_Agent__ProductClassID.ProductPrice.toDecimal() + ")");
                            ArrayListSQL.Add(strSql.ToString());

                            Eggsoft.Common.debug_Log.Call_WriteLog("ArrayListSQL.Add(strSql.ToString())=" + strSql.ToString());
                        }
                    }

                    EggsoftWX.SQLServerDAL.DbHelperSQL.ExecuteSqlTran(ArrayListSQL);
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "自动更新代理商范围");
            }
            finally
            {
            }
        }


        public static string getAddress(string strUser_AddressID, string stringO2OTakedID)
        {
            EggsoftWX.BLL.tab_User_Address BLL_tab_User_Address = new EggsoftWX.BLL.tab_User_Address();
            EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods BLL_tab_ShopClient_O2O_TakeGoods = new EggsoftWX.BLL.tab_ShopClient_O2O_TakeGoods();
            EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
            String strXiangXiDiZhi = "无收货地址。";

            if (String.IsNullOrEmpty(strUser_AddressID) == false)
            {
                if (BLL_tab_User_Address.Exists("id=" + strUser_AddressID))
                {
                    strXiangXiDiZhi = BLL_tab_User_Address.GetList("XiangXiDiZhi", "id=" + strUser_AddressID).Tables[0].Rows[0]["XiangXiDiZhi"].ToString();
                }
            }
            if (String.IsNullOrEmpty(stringO2OTakedID) == false)
            {
                if (BLL_tab_ShopClient_O2O_TakeGoods.Exists("id=" + stringO2OTakedID))
                {

                    EggsoftWX.Model.tab_ShopClient_O2O_TakeGoods Model_tab_ShopClient_O2O_TakeGoods = BLL_tab_ShopClient_O2O_TakeGoods.GetModel(Int32.Parse(stringO2OTakedID));

                    EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(Model_tab_ShopClient_O2O_TakeGoods.TakeO2OShopID);
                    strXiangXiDiZhi = "上门自取：" + Model_tab_ShopClient_O2O_ShopInfo.ShopName + " " + Model_tab_ShopClient_O2O_ShopInfo.ShopAdress + " 营业时间：" + Model_tab_ShopClient_O2O_ShopInfo.ShopDayTime + "  <br /> 自取时间：" + Model_tab_ShopClient_O2O_TakeGoods.TakeDateTime.ToString("yyyy-MM-dd HH:mm") + "\n";

                    //strXiangXiDiZhi = BLL_tab_User_Address.GetList("XiangXiDiZhi", "id=" + strUser_AddressID).Tables[0].Rows[0]["XiangXiDiZhi"].ToString();
                }
            }
            return strXiangXiDiZhi;
        }
        public static string APPCODE_getBuyNow_String(int intGood)//强行获取
        {

            #region  处理字典 ShopClient_Dictionaries
            EggsoftWX.BLL.tab_Goods_XML_Goods_ID BLL_tab_Goods_XML_Goods_ID = new EggsoftWX.BLL.tab_Goods_XML_Goods_ID();
            bool bool_ShopClient_Dictionaries = BLL_tab_Goods_XML_Goods_ID.Exists("GoodID=" + intGood);
            Eggsoft_Public_CL.XML_Class.ShopClient_Dictionaries XML_Class_ShopClient_Dictionaries = new Eggsoft_Public_CL.XML_Class.ShopClient_Dictionaries();
            if (bool_ShopClient_Dictionaries == true)
            {
                try
                {
                    string strXMLName_ID = BLL_tab_Goods_XML_Goods_ID.GetList("XMLName_ID", "GoodID=" + intGood).Tables[0].Rows[0]["XMLName_ID"].ToString();
                    string strXML = new EggsoftWX.BLL.tab_Goods_XML().GetList("XMLContent", "ID=" + strXMLName_ID).Tables[0].Rows[0]["XMLContent"].ToString();
                    XML_Class_ShopClient_Dictionaries = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML_Class.ShopClient_Dictionaries>(strXML, System.Text.Encoding.UTF8);

                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);

                }
                finally { }
            }
            return XML_Class_ShopClient_Dictionaries.BuyNow;
            #endregion
        }
        public static string APPCODE_getFirstImage_String(String strTab_Goods_Icon)//强行获取
        {
            string strAPPCODE_getFirstImage = "";

            try
            {
                String[] StringmePicList = strTab_Goods_Icon.Split(';');

                ArrayList alStringPicList = new ArrayList();
                for (int i = 0; i < StringmePicList.Length; i++)
                {
                    if (String.IsNullOrEmpty(StringmePicList[i]) == false)
                    {
                        if (Eggsoft.Common.FileFolder.File_Exists(StringmePicList[i]))
                        {
                            alStringPicList.Add(StringmePicList[i]);
                        }
                    }
                }
                if (alStringPicList.Count > 0)
                {
                    strAPPCODE_getFirstImage = alStringPicList[0].ToString();
                }
                else
                {
                    strAPPCODE_getFirstImage = strTab_Goods_Icon;
                }
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }

            finally { }


            return strAPPCODE_getFirstImage;
        }

        public static void APPCODE_saveOtherImage(String strIcon)
        {
            try
            {
                //string strIcon = myDataTable.Rows[i]["Icon"].ToString();
                String[] myString = Eggsoft.Common.Image.getFileBMPWidthAndHeight(strIcon);
                //if (myString == null) continue;
                //Eggsoft.Common.FileFolder.
                if (myString == null) return;///本地没有文件


                String strNormalName = Eggsoft.Common.FileFolder.getFileNormalName(strIcon);
                String strDirectoryName = Eggsoft.Common.FileFolder.getDirectoryName(strIcon);
                String strFileType = Eggsoft.Common.FileFolder.getFileType(strIcon);


                if (Int32.Parse(myString[0]) > 640)
                {
                    string str640 = strDirectoryName + @"\" + strNormalName + "_640" + strFileType;
                    File.Copy(System.Web.HttpContext.Current.Server.MapPath(strIcon), str640, true);
                    Eggsoft_Public_CL.GoodP.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str640), 640, 0, "W");
                }
                if (Int32.Parse(myString[0]) > 200)
                {
                    string str200 = strDirectoryName + @"\" + strNormalName + "_200" + strFileType;
                    File.Copy(System.Web.HttpContext.Current.Server.MapPath(strIcon), str200, true);
                    Eggsoft_Public_CL.GoodP.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str200), 200, 150, "Cut");
                }
                if ((Int32.Parse(myString[0]) > 100) || (Int32.Parse(myString[1]) > 75))
                {
                    string str100 = strDirectoryName + @"\" + strNormalName + "_100" + strFileType;
                    File.Copy(System.Web.HttpContext.Current.Server.MapPath(strIcon), str100, true);
                    Eggsoft_Public_CL.GoodP.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str100), 100, 75, "hw");
                }
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }
            finally { }

        }



        public static string APPCODE_getFirstImage_String_OnlyGet(String strTab_Goods_Icon)//获取
        {
            string strAPPCODE_getFirstImage = "";

            try
            {
                String[] StringmePicList = strTab_Goods_Icon.Split(';');

                ArrayList alStringPicList = new ArrayList();
                for (int i = 0; i < StringmePicList.Length; i++)
                {
                    if ((String.IsNullOrEmpty(StringmePicList[i]) == false) && (StringmePicList[i].ToLower().IndexOf("upload") > 0))
                    {
                        strAPPCODE_getFirstImage = StringmePicList[i];
                        break;
                    }
                }

            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }

            finally { }


            return strAPPCODE_getFirstImage;
        }

        public static string APPCODE_OnlyGetMyFileName_Image_Force(String strIconList, int intWidth)
        {

            string strIntFilename = "";
            try
            {
                string strFirst = APPCODE_getFirstImage_String_OnlyGet(strIconList);


                int intDotPos = strFirst.IndexOf('.');
                strIntFilename = strFirst.Substring(0, intDotPos) + "_" + intWidth.ToString() + strFirst.Substring(intDotPos, strFirst.Length - intDotPos);
            }

            catch { }
            finally { }
            return strIntFilename;
        }

        public static void APPCODE_saveOtherImage_Force(String strIconList, int intShopClientID)
        {
            try
            {
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClientID);
                string strXML = Model_tab_ShopClient.XML;
                Eggsoft_Public_CL.XML__Class_Shop_Client XML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<Eggsoft_Public_CL.XML__Class_Shop_Client>(strXML, System.Text.Encoding.UTF8);
                bool myBool_AddWatermater_Logo_ = XML__Class_Shop_Client.Bool_AddWatermater_Logo_;
                string strShopLogoImage = XML__Class_Shop_Client.ShopLogoImage;

                string strFirst = APPCODE_getFirstImage_String(strIconList);
                if ((myBool_AddWatermater_Logo_) && (String.IsNullOrEmpty(strShopLogoImage) == false))
                {
                    Eggsoft.Common.Image.Mark_Logo_WithBase_GoodsPic_640_400_200_100(System.Web.HttpContext.Current.Server.MapPath(strFirst), System.Web.HttpContext.Current.Server.MapPath(strShopLogoImage));
                }

                String[] myString = Eggsoft.Common.Image.getFileBMPWidthAndHeight(strFirst);
                if (myString == null) return;///本地没有文件


                String strNormalName = Eggsoft.Common.FileFolder.getFileNormalName(strFirst);
                String strDirectoryName = Eggsoft.Common.FileFolder.getDirectoryName(strFirst);
                String strFileType = Eggsoft.Common.FileFolder.getFileType(strFirst);

                string str640 = strDirectoryName + @"\" + strNormalName + "_640" + strFileType;
                File.Copy(System.Web.HttpContext.Current.Server.MapPath(strFirst), str640, true);///直接从小的复制到大的  直接使用

                string str400 = strDirectoryName + @"\" + strNormalName + "_400" + strFileType;
                File.Copy(System.Web.HttpContext.Current.Server.MapPath(strFirst), str400, true);

                string str200 = strDirectoryName + @"\" + strNormalName + "_200" + strFileType;
                File.Copy(System.Web.HttpContext.Current.Server.MapPath(strFirst), str200, true);



                string str100 = strDirectoryName + @"\" + strNormalName + "_100" + strFileType;
                File.Copy(System.Web.HttpContext.Current.Server.MapPath(strFirst), str100, true);




                if (Int32.Parse(myString[0]) > 640)
                {
                    Eggsoft.Common.Image.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str640), 640, 640, "W");
                }

                if (Int32.Parse(myString[0]) > 400)
                {
                    Eggsoft.Common.Image.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str400), 400, 400, "hw");
                }

                if (Int32.Parse(myString[0]) > 200)
                {
                    Eggsoft.Common.Image.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str200), 200, 200, "hw");
                }
                if ((Int32.Parse(myString[0]) > 100) || (Int32.Parse(myString[1]) > 100))
                {
                    Eggsoft.Common.Image.ScaleBMP(Eggsoft.Common.FileFolder.urlconvertor(str100), 100, 100, "hw");
                }
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }
            finally { }

        }

        public static bool get_Bool_LimitTimer_FromGoodID(int goodID)
        {
            bool boolIfLimitTimerBuy = false;

            EggsoftWX.BLL.View_SecondSalesGoodList bll_View_SecondSalesGoodList = new EggsoftWX.BLL.View_SecondSalesGoodList();
            EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = new EggsoftWX.Model.View_SecondSalesGoodList();
            bool boolGoodID = bll_View_SecondSalesGoodList.Exists("ID=" + goodID);

            if (boolGoodID)
            {
                Model_View_SecondSalesGoodList = bll_View_SecondSalesGoodList.GetModel(goodID);
                if ((Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenSalesSecond) < 0) && (Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenEndSalesSecond) > 0))///处于秒杀期间
                {
                    boolIfLimitTimerBuy = true;
                }
            }
            return boolIfLimitTimerBuy;
        }



        public static int getLimitTimerBuy_MaxSalesCount_FromGoodID(int goodID)
        {
            int LimitTimerBuy_MaxSalesCount = 9999;

            EggsoftWX.BLL.View_SecondSalesGoodList bll_View_SecondSalesGoodList = new EggsoftWX.BLL.View_SecondSalesGoodList();
            EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = new EggsoftWX.Model.View_SecondSalesGoodList();
            bool boolGoodID = bll_View_SecondSalesGoodList.Exists("ID=" + goodID);

            if (boolGoodID)
            {
                Model_View_SecondSalesGoodList = bll_View_SecondSalesGoodList.GetModel(goodID);

                if ((Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenSalesSecond) < 0) && (Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenEndSalesSecond) > 0))///处于秒杀期间
                {
                    Model_View_SecondSalesGoodList = bll_View_SecondSalesGoodList.GetModel("ID=" + goodID);
                    LimitTimerBuy_MaxSalesCount = Model_View_SecondSalesGoodList.LimitTimerBuy_MaxSalesCount;
                }
            }

            return LimitTimerBuy_MaxSalesCount;
        }


        public static EggsoftWX.Model.View_SecondSalesGoodList GetSecondBuyInfoPrice(int goodID, out bool boolIFBuy)
        {
            EggsoftWX.BLL.View_SecondSalesGoodList bll_View_SecondSalesGoodList = new EggsoftWX.BLL.View_SecondSalesGoodList();
            boolIFBuy = false;
            bool boolGoodID = bll_View_SecondSalesGoodList.Exists("ID=" + goodID.ToString());
            if (boolGoodID)
            {
                boolIFBuy = true;
                EggsoftWX.Model.View_SecondSalesGoodList Model_View_SecondSalesGoodList = bll_View_SecondSalesGoodList.GetModel("ID=" + goodID.ToString());

                if ((Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenSalesSecond) < 0) && (Decimal.Parse(Model_View_SecondSalesGoodList.ShowWhenEndSalesSecond) > 0))
                {
                    return bll_View_SecondSalesGoodList.GetModel("ID=" + goodID);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }



        public static string GetCalendarJSFromDateTime(DateTime argDateTime)
        {



            string strDateTimeString = argDateTime.ToString("dd/MM/yyy HH:mm");
            //string strDateTimeString = "" + argDateTime.Day + "/" + argDateTime.Month + "/" + argDateTime.Year + " " + intDate + ":" + argDateTime.Minute  + strAMPM ;
            return strDateTimeString;
        }

        public static DateTime GetCalendarJS(string strDateTimeString)
        {
            string[] strTimeList = strDateTimeString.Split(' ');
            string[] strDateTimeList = strTimeList[0].Split('-');

            string[] strHHMMList = strTimeList[1].Split(':');

            DateTime dtStartTime = Convert.ToDateTime("" + strDateTimeList[2] + "-" + strDateTimeList[1] + "-" + strDateTimeList[0] + " " + strHHMMList[0] + ":" + strHHMMList[1] + ":00.002");
            return dtStartTime;
        }

        public static string APPCODE_getImage_ForceGet(String strPath, int intWidth, int intHeight)//强行获取
        {

            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[3];
            args[0] = strPath;
            args[1] = intWidth.ToString();
            args[2] = intHeight.ToString();
            object result = WebServiceHelper.WsCaller.InvokeWebService(url, "APPCODE_getImage_ForceGet", args);

            return result.ToString();
        }


        public static string APPCODE_getFirstImage(int intGoods)
        {

            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[1];
            args[0] = intGoods.ToString();
            object result = WebServiceHelper.WsCaller.InvokeWebService(url, "APPCODE_getFirstImage_Int", args);

            return result.ToString();

        }



        public static string APPCODE_getFirstImage(String strTab_Goods_Icon)
        {

            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[1];
            args[0] = strTab_Goods_Icon;
            object result = WebServiceHelper.WsCaller.InvokeWebService(url, "APPCODE_getFirstImage_String", args);

            return result.ToString();
        }

        public static string APPCODE_getImage_HW_(String strPath, int intWidth, int intHeight)
        {

            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[3];
            args[0] = strPath;
            args[1] = intWidth.ToString();
            args[2] = intHeight.ToString();
            object result = WebServiceHelper.WsCaller.InvokeWebService(url, "APPCODE_getImage_HW_", args);

            return result.ToString();
        }


        public static string APPCODE_getImage_First(String strGoodIconPath, int intWidth)
        {
            Eggsoft.Common.debug_Log.Call_WriteLog("APPCODE_getImage_First" + strGoodIconPath + intWidth.ToString());
            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[2];
            args[0] = strGoodIconPath;
            args[1] = intWidth.ToString();
            object result = WebServiceHelper.WsCaller.InvokeWebService(url, "APPCODE_getImage_First", args);

            return result.ToString();
        }


        public static string APPCODE_getImage888(String strPath, int intWidth)
        {

            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[2];
            args[0] = strPath;
            args[1] = intWidth.ToString();
            object result = WebServiceHelper.WsCaller.InvokeWebService(url, "APPCODE_getImage", args);

            return result.ToString();
        }


        public static void DownLoadFile_Service_ScaleBMP(string DownLoadOriginalImagePath, string originalImagePath, int width, int height, string mode)
        {
            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[5];
            args[0] = DownLoadOriginalImagePath;
            args[1] = originalImagePath;
            args[2] = width.ToString();
            args[3] = height.ToString();
            args[4] = mode;

            WebServiceHelper.WsCaller.InvokeWebService(url, "Scale_Down_BMP_HW_", args);

        }
        /// <summary>
        /// 确保路径存在
        /// </summary>
        /// <param name="DownLoadOriginalImagePath"></param>
        /// <param name="originalImagePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        public static void DownLoadFile_Service_ScaleBMP_makeHeadImage_User_path(string strHead_Parent_Image)
        {
            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[1];
            args[0] = strHead_Parent_Image;


            WebServiceHelper.WsCaller.InvokeWebService(url, "Scale_Down_BMP_HW__User_path", args);

        }

        public static void ScaleBMP(string originalImagePath, int width, int height, string mode)
        {

            string url = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + "/PubFile/GoodP.asmx";
            string[] args = new string[4];
            args[0] = originalImagePath;
            args[1] = width.ToString();
            args[2] = height.ToString();
            args[3] = mode;

            WebServiceHelper.WsCaller.InvokeWebService(url, "Scale_BMP_HW_", args);

        }

        public static string APPCODE_getImage(Int16 intGoodID, int intWidth)
        {

            string strIcon = "";
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();

            if (bll.Exists("ID=" + intGoodID))
            {
                strIcon = (bll.GetList("Icon", "ID=" + intGoodID).Tables[0].Rows[0]["Icon"].ToString());
            }
            return Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(strIcon, intWidth);
            //return APPCODE_getImage(APPCODE_getFirstImage(strIcon), intWidth);
        }


        private static Object thisLockIGetMoneyYunYingZhongXin = new Object();
        private static Object thisLockIGetMoneyZhongChou = new Object();
        private static Object thisLockIGetMoneyTuanGou = new Object();
        private static Object thisLockIGetMoneyWeiKanJia = new Object();
        private static Object thisLockIGetMoneySelfGetMoney = new Object();
        private static Object thisLockIGetMoneyInputMoney = new Object();///会员充值
        private static Object thisLockIGetMoney0 = new Object();///会员充值

        public static void IGetMoney(String OrderNum, String payWay, String strThreeOrderNum)
        {
            try
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("OrderNum payWay  strThreeOrderNum =" + OrderNum + "  " + payWay + "  " + strThreeOrderNum, "支付通知", "可用于调试");


                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");
                if (my_Model_tab_Order != null)
                {
                    #region 增加已支付未处理信息，确认收货
                    EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                    EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                    Model_b011_InfoAlertMessage.InfoTip = "已支付通知,待收货";
                    Model_b011_InfoAlertMessage.CreateBy = "微信已支付通知";
                    Model_b011_InfoAlertMessage.UpdateBy = "微信已支付通知";
                    Model_b011_InfoAlertMessage.UserID = my_Model_tab_Order.UserID;
                    Model_b011_InfoAlertMessage.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                    Model_b011_InfoAlertMessage.Type = "Info_cart_good2";
                    Model_b011_InfoAlertMessage.TypeTableID = my_Model_tab_Order.ID;
                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                    #endregion 增加已支付未处理信息 


                    bool boolP_PayStatus = Convert.ToBoolean(my_Model_tab_Order.PayStatus);
                    if (false == boolP_PayStatus)//可能已经得到通知了 避免重复发送 信息
                    {
                        my_Model_tab_Order.PayStatus = 1;
                        my_Model_tab_Order.PayWay = payWay;
                        my_Model_tab_Order.PayDateTime = DateTime.Now;
                        my_Model_tab_Order.UpdateDateTime = DateTime.Now;

                        my_Model_tab_Order.PaywayOrderNum = strThreeOrderNum;
                        my_BLL_tab_Order.Update(my_Model_tab_Order);  //暂时注释
                        int OrderID = my_Model_tab_Order.ID;

                        EggsoftWX.BLL.tab_Orderdetails my_BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                        bool boolIfGoodTypeNot0 = false;

                        #region 判断商品类型是否6 众筹购买成功的订单 商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                        if (boolIfGoodTypeNot0 == false)
                        {
                            System.Data.DataTable DataTableYunYingZhongXin_Orderdetails = my_BLL_tab_Orderdetails.GetList("OrderID=" + my_Model_tab_Order.ID + " and GoodType=6 order by id asc").Tables[0];

                            if (DataTableYunYingZhongXin_Orderdetails.Rows.Count > 0)
                            {
                                boolIfGoodTypeNot0 = true;


                                lock (thisLockIGetMoneyYunYingZhongXin)
                                {
                                    for (int i = 0; i < DataTableYunYingZhongXin_Orderdetails.Rows.Count; i++)
                                    {
                                        string strOrderdetailsID = DataTableYunYingZhongXin_Orderdetails.Rows[i]["ID"].ToString();
                                        string strGoodPrice = DataTableYunYingZhongXin_Orderdetails.Rows[i]["GoodPrice"].ToString();
                                        string GoodType = DataTableYunYingZhongXin_Orderdetails.Rows[i]["GoodType"].ToString();///商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                                        string strGoodTypeId = DataTableYunYingZhongXin_Orderdetails.Rows[i]["GoodTypeId"].ToString();
                                        string GoodTypeIdBuyInfo = DataTableYunYingZhongXin_Orderdetails.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                                        string GoodOrderCount = DataTableYunYingZhongXin_Orderdetails.Rows[i]["OrderCount"].ToString();
                                        string strGoodID = DataTableYunYingZhongXin_Orderdetails.Rows[i]["GoodID"].ToString();

                                        //EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                                        //EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Int32.Parse(GoodTypeIdBuyInfo));

                                        Decimal DecimalAllMoney = Decimal.Multiply(Convert.ToDecimal(strGoodPrice), Convert.ToDecimal(GoodOrderCount));

                                        int intGoodOrderCount = 0;
                                        int.TryParse(GoodOrderCount, out intGoodOrderCount);

                                        int intOperationCenterID = 0;
                                        int.TryParse(strGoodTypeId, out intOperationCenterID);

                                        int intb004_OperationGoods = 0;
                                        int.TryParse(GoodTypeIdBuyInfo, out intb004_OperationGoods);


                                        GoodP_YunYingZhongXin.tellShopClientID_UserPayMoney_ByWeiXin(DecimalAllMoney, my_Model_tab_Order.UserID.ToString(), strGoodID.toInt32(), intOperationCenterID, intb004_OperationGoods, OrderNum);
                                        GoodP_YunYingZhongXin.tellShopClientID_o2o_UserPayMoney_ByWeiXin(DecimalAllMoney, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodID), intOperationCenterID, intb004_OperationGoods, OrderNum);
                                        GoodP_YunYingZhongXin.tell_UserIGetMoneyPayMoney_ByWeiXin(DecimalAllMoney, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodID), intOperationCenterID, intb004_OperationGoods, OrderID, intGoodOrderCount, OrderNum);
                                        GoodP_YunYingZhongXin.tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin(DecimalAllMoney, OrderID, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodID), intOperationCenterID, intb004_OperationGoods);
                                        GoodP_YunYingZhongXin.tell_YunYingZhongXin_ParentID_IGetMoneyPayMoney_ByWeiXin(DecimalAllMoney, OrderID, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodID), intOperationCenterID, intb004_OperationGoods);

                                        #region 是否自动给予分销权  首次购买制作证书
                                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                        EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Convert.ToInt32(my_Model_tab_Order.UserID));
                                        EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                                        EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + my_Model_tab_Order.UserID + " and IsDeleted=0");
                                        if (Model_tab_ShopClient_Agent_ == null || Model_tab_ShopClient_Agent_.Empowered != true)////首次购买制作证书
                                        {
                                            EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                                            EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);
                                            if (tab_ShopClient_ShopPar_Model != null)
                                            {
                                                if (tab_ShopClient_ShopPar_Model.AskAgentAutoAfterBuy.toBoolean())
                                                {

                                                    Eggsoft_Public_CL.Pub_Agent.add_Agent_Default_OnlyOneKey(Convert.ToInt32(my_Model_tab_Order.UserID));
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        #endregion


                        #region 判断商品类型是否5   5会员充值  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                        System.Data.DataTable DataTableInputMoneyOrderdetails = my_BLL_tab_Orderdetails.GetList("OrderID=" + my_Model_tab_Order.ID + " and GoodType=5 order by id asc").Tables[0];
                        if (DataTableInputMoneyOrderdetails.Rows.Count > 0)
                        {
                            boolIfGoodTypeNot0 = true;
                            my_Model_tab_Order.isReceipt = true;
                            my_Model_tab_Order.UpdateDateTime = DateTime.Now;
                            my_BLL_tab_Order.Update(my_Model_tab_Order);///自动更新
                            lock (thisLockIGetMoneyInputMoney)
                            {
                                for (int i = 0; i < DataTableInputMoneyOrderdetails.Rows.Count; i++)
                                {
                                    string strGoodPrice = DataTableInputMoneyOrderdetails.Rows[i]["GoodPrice"].ToString();
                                    string GoodType = DataTableInputMoneyOrderdetails.Rows[i]["GoodType"].ToString();
                                    string strGoodTypeId = DataTableInputMoneyOrderdetails.Rows[i]["GoodTypeId"].ToString();
                                    string GoodTypeIdBuyInfo = DataTableInputMoneyOrderdetails.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                                    string GoodOrderCount = DataTableInputMoneyOrderdetails.Rows[i]["OrderCount"].ToString();


                                    GoodP_SelfPayMoney.tellShopClientID_UserPayMoney_ByWeiXin_SelfPayMoney(OrderNum, strGoodPrice, my_Model_tab_Order.UserID.ToString());
                                    GoodP_SelfPayMoney.tell_UserIGetMoneyPayMoney_ByWeiXin_SelfPayMoney(OrderNum, strGoodPrice, my_Model_tab_Order.UserID.ToString());

                                    #region 前端充值

                                    EggsoftWX.BLL.tab_ShopClient_MemberCard bll = new EggsoftWX.BLL.tab_ShopClient_MemberCard();
                                    EggsoftWX.Model.tab_ShopClient_MemberCard Model = new EggsoftWX.Model.tab_ShopClient_MemberCard();

                                    EggsoftWX.BLL.tab_User blltab_User = new EggsoftWX.BLL.tab_User();
                                    EggsoftWX.Model.tab_User Modeltab_User = blltab_User.GetModel(my_Model_tab_Order.UserID);


                                    Model.ShopClientID = my_Model_tab_Order.ShopClient_ID;
                                    Model.PhoneNum = Modeltab_User.UserAccount;
                                    Model.InputMoney = Convert.ToDecimal(strGoodPrice);
                                    Model.BankSeraillnum = strThreeOrderNum;
                                    EggsoftWX.Model.tab_ShopClient_MemberCardBonus Model_tab_MemberCardBonus = Eggsoft_Public_CL.PubMember.getBonusPolicy(my_Model_tab_Order.ShopClient_ID, Model.InputMoney);

                                    if (Model_tab_MemberCardBonus != null)
                                    {
                                        Model.BonusMoney = Model_tab_MemberCardBonus.BonusMoney;
                                        Model.BonusGouWuQuan = Model_tab_MemberCardBonus.BonusGouWuQuan;
                                        Model.BonusDesc = Model_tab_MemberCardBonus.BonusDesc + ",充值政策" + Model_tab_MemberCardBonus.ID;
                                    }
                                    else
                                    {
                                        Model.BonusDesc = "充值政策没有匹配成功";
                                    }
                                    Model.CreateBy = "用户自行支付";
                                    Model.UpdateBy = "用户自行支付";
                                    Model.UpdateTime = DateTime.Now;
                                    bool boolIFSend = Eggsoft_Public_CL.PubMember.CardBonusChangeToUserAccount(Model);
                                    Model.IfChangToWeiXinBonus = boolIFSend;

                                    string strDEC = "添加成功!";
                                    if (boolIFSend)
                                    {
                                        strDEC += "，已转化到账户";
                                        Model.BonusDesc += "，已转化到账户";
                                    }
                                    else
                                    {
                                        strDEC += "，尚未转化，程序出错";
                                    }
                                    bll.Add(Model);
                                    #endregion 后端充值


                                }
                            }
                        }
                        #endregion
                        #region 判断商品类型是否4   自助收款 支付成功商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值
                        if (boolIfGoodTypeNot0 == false)
                        {
                            System.Data.DataTable DataTableSelfGetMoneyOrderdetails = my_BLL_tab_Orderdetails.GetList("OrderID=" + my_Model_tab_Order.ID + " and GoodType=4 order by id asc").Tables[0];
                            if (DataTableSelfGetMoneyOrderdetails.Rows.Count > 0)
                            {
                                boolIfGoodTypeNot0 = true;
                                lock (thisLockIGetMoneySelfGetMoney)
                                {
                                    for (int i = 0; i < DataTableSelfGetMoneyOrderdetails.Rows.Count; i++)
                                    {
                                        string strGoodPrice = DataTableSelfGetMoneyOrderdetails.Rows[i]["GoodPrice"].ToString();
                                        string GoodType = DataTableSelfGetMoneyOrderdetails.Rows[i]["GoodType"].ToString();
                                        string strGoodTypeId = DataTableSelfGetMoneyOrderdetails.Rows[i]["GoodTypeId"].ToString();
                                        string GoodTypeIdBuyInfo = DataTableSelfGetMoneyOrderdetails.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                                        string GoodOrderCount = DataTableSelfGetMoneyOrderdetails.Rows[i]["OrderCount"].ToString();

                                        int intGoodOrderCount = 0;
                                        int.TryParse(GoodOrderCount, out intGoodOrderCount);

                                        int intTuanGou_Number = 0;
                                        int.TryParse(GoodTypeIdBuyInfo, out intTuanGou_Number);

                                        GoodP_SelfPayMoney.tellShopClientID_UserPayMoney_ByWeiXin_SelfPayMoney(OrderNum, strGoodPrice, my_Model_tab_Order.UserID.ToString());
                                        GoodP_SelfPayMoney.tell_UserIGetMoneyPayMoney_ByWeiXin_SelfPayMoney(OrderNum, strGoodPrice, my_Model_tab_Order.UserID.ToString());
                                    }
                                }
                            }
                        }
                        #endregion




                        #region 判断商品类型是否2 是否组团成功以支付为准，支付才能参团成功。如团已满会产生新的团长。如不满团，客服介入处理或者原路退回金额
                        if (boolIfGoodTypeNot0 == false)
                        {
                            System.Data.DataTable DataTableTuanGou_Orderdetails = my_BLL_tab_Orderdetails.GetList("OrderID=" + my_Model_tab_Order.ID + " and GoodType=2 order by id asc").Tables[0];

                            if (DataTableTuanGou_Orderdetails.Rows.Count > 0)
                            {
                                boolIfGoodTypeNot0 = true;

                                lock (thisLockIGetMoneyTuanGou)
                                {
                                    for (int i = 0; i < DataTableTuanGou_Orderdetails.Rows.Count; i++)
                                    {
                                        string strOrderdetailsID = DataTableTuanGou_Orderdetails.Rows[i]["ID"].ToString();
                                        string strGoodPrice = DataTableTuanGou_Orderdetails.Rows[i]["GoodPrice"].ToString();
                                        string GoodType = DataTableTuanGou_Orderdetails.Rows[i]["GoodType"].ToString();
                                        string strGoodTypeId = DataTableTuanGou_Orderdetails.Rows[i]["GoodTypeId"].ToString();
                                        string GoodTypeIdBuyInfo = DataTableTuanGou_Orderdetails.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                                        string GoodOrderCount = DataTableTuanGou_Orderdetails.Rows[i]["OrderCount"].ToString();

                                        int intGoodOrderCount = 0;
                                        int.TryParse(GoodOrderCount, out intGoodOrderCount);

                                        int intTuanGou_Number = 0;
                                        int.TryParse(GoodTypeIdBuyInfo, out intTuanGou_Number);
                                        bool boolNew_A_TuanGou_Number = true;///开辟新团 还是参加一个尚未封闭的团

                                        EggsoftWX.BLL.tab_TuanGou_Number BLL_tab_TuanGou_Number = new EggsoftWX.BLL.tab_TuanGou_Number();
                                        EggsoftWX.Model.tab_TuanGou_Number Model_tab_TuanGou_Number = new EggsoftWX.Model.tab_TuanGou_Number(); ;

                                        if (intTuanGou_Number > 0)
                                        { ///说明是别人的团
                                          ///
                                            Model_tab_TuanGou_Number = BLL_tab_TuanGou_Number.GetModel(intTuanGou_Number);
                                            if (Model_tab_TuanGou_Number.IFFinshedCurMemberShip == false && Model_tab_TuanGou_Number.Efficacy == true)
                                            {
                                                boolNew_A_TuanGou_Number = false;
                                            }
                                        }
                                        if (boolNew_A_TuanGou_Number == false)
                                        {
                                            #region 尚未封闭的团
                                            EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                                            EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(Int32.Parse(strGoodTypeId));
                                            EggsoftWX.BLL.tab_TuanGou_Partner BLL_tab_TuanGou_Partner = new EggsoftWX.BLL.tab_TuanGou_Partner();

                                            #region 参团成功  一个人买多份 就出现多次
                                            for (int k = 0; k < intGoodOrderCount; k++)
                                            {
                                                EggsoftWX.Model.tab_TuanGou_Partner Model_tab_TuanGou_Partner = new EggsoftWX.Model.tab_TuanGou_Partner();
                                                Model_tab_TuanGou_Partner.BuyPrice = Convert.ToDecimal(strGoodPrice); ;
                                                Model_tab_TuanGou_Partner.OrderID = OrderID;
                                                Model_tab_TuanGou_Partner.ParterRole = 2;
                                                Model_tab_TuanGou_Partner.ShopClientID = my_Model_tab_Order.ShopClient_ID;
                                                Model_tab_TuanGou_Partner.TuanGouID = Int32.Parse(strGoodTypeId);
                                                Model_tab_TuanGou_Partner.UserID = my_Model_tab_Order.UserID;
                                                Model_tab_TuanGou_Partner.TuanGouIDNumber = intTuanGou_Number;
                                                Model_tab_TuanGou_Partner.GetGoodsAddress = my_Model_tab_Order.User_Address;
                                                BLL_tab_TuanGou_Partner.Add(Model_tab_TuanGou_Partner);
                                            }
                                            #endregion

                                            int intExsitTuanGroupCount = BLL_tab_TuanGou_Partner.ExistsCount("TuanGouIDNumber=" + intTuanGou_Number + " and TuanGouID=" + strGoodTypeId + " and ShopClientID=" + my_Model_tab_Order.ShopClient_ID);

                                            //int intExsitTuanGroupCount = Int32.Parse(BLL_tab_TuanGou_Partner.SelectList(strIfSuccessWhere).Tables[0].Rows[0]["TuanGouOrderCount"].ToString());

                                            if ((intExsitTuanGroupCount) < Model_tab_TuanGou.HowManyPeople)////参团成功
                                            {

                                            }
                                            else////团组团 成功  封闭该团
                                            {
                                                #region 团组团 成功  封闭该团
                                                Model_tab_TuanGou_Number.IFFinshedCurMemberShip = true;
                                                Model_tab_TuanGou_Number.UpdateTime = DateTime.Now;
                                                BLL_tab_TuanGou_Number.Update(Model_tab_TuanGou_Number);
                                                #endregion 开辟新团
                                            }
                                            #endregion 尚未封闭的团
                                        }
                                        else
                                        {


                                            #region 开辟新团
                                            Model_tab_TuanGou_Number.Efficacy = true;
                                            Model_tab_TuanGou_Number.BuyPrice = Convert.ToDecimal(strGoodPrice);
                                            Model_tab_TuanGou_Number.IFFinshedCurMemberShip = false;
                                            Model_tab_TuanGou_Number.ShopClientID = my_Model_tab_Order.ShopClient_ID;
                                            Model_tab_TuanGou_Number.TuanGouID = Int32.Parse(strGoodTypeId);
                                            intTuanGou_Number = BLL_tab_TuanGou_Number.Add(Model_tab_TuanGou_Number);
                                            my_BLL_tab_Orderdetails.Update("GoodTypeIdBuyInfo='" + intTuanGou_Number + "',UpdateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'", "id=" + strOrderdetailsID);//支付完成后给予团长设置
                                            ///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                                            for (int k = 0; k < intGoodOrderCount; k++)///一个人买多份 就出现多次
                                            {
                                                EggsoftWX.BLL.tab_TuanGou_Partner BLL_tab_TuanGou_Partner = new EggsoftWX.BLL.tab_TuanGou_Partner();
                                                EggsoftWX.Model.tab_TuanGou_Partner Model_tab_TuanGou_Partner = new EggsoftWX.Model.tab_TuanGou_Partner();
                                                Model_tab_TuanGou_Partner.BuyPrice = Convert.ToDecimal(strGoodPrice); ;
                                                Model_tab_TuanGou_Partner.OrderID = OrderID;
                                                Model_tab_TuanGou_Partner.ParterRole = 1;
                                                Model_tab_TuanGou_Partner.ShopClientID = my_Model_tab_Order.ShopClient_ID;
                                                Model_tab_TuanGou_Partner.TuanGouID = Int32.Parse(strGoodTypeId);
                                                Model_tab_TuanGou_Partner.UserID = my_Model_tab_Order.UserID;
                                                Model_tab_TuanGou_Partner.TuanGouIDNumber = intTuanGou_Number;
                                                Model_tab_TuanGou_Partner.GetGoodsAddress = my_Model_tab_Order.User_Address;
                                                BLL_tab_TuanGou_Partner.Add(Model_tab_TuanGou_Partner);
                                            }
                                            #endregion 开辟新团
                                        }
                                        GoodP_TuanGou.tellShopClientID_UserPayMoney_ByWeiXin_TuanGou(my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intTuanGou_Number);
                                        GoodP_TuanGou.tellShopClientID_o2o_UserPayMoney_ByWeiXin_TuanGou(my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intTuanGou_Number);
                                        GoodP_TuanGou.tell_UserIGetMoneyPayMoney_ByWeiXin_TuanGou(my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intTuanGou_Number);
                                        GoodP_TuanGou.tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin_TuanGou(OrderID, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intTuanGou_Number);
                                    }
                                }
                            }
                        }
                        #endregion


                        #region 判断商品类型是否3 众筹购买成功的订单 商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                        if (boolIfGoodTypeNot0 == false)
                        {


                            System.Data.DataTable DataTableZhongChou_Orderdetails = my_BLL_tab_Orderdetails.GetList("OrderID=" + my_Model_tab_Order.ID + " and GoodType=3 order by id asc").Tables[0];

                            if (DataTableZhongChou_Orderdetails.Rows.Count > 0)
                            {
                                boolIfGoodTypeNot0 = true;


                                lock (thisLockIGetMoneyZhongChou)
                                {
                                    for (int i = 0; i < DataTableZhongChou_Orderdetails.Rows.Count; i++)
                                    {
                                        string strOrderdetailsID = DataTableZhongChou_Orderdetails.Rows[i]["ID"].ToString();
                                        string strGoodPrice = DataTableZhongChou_Orderdetails.Rows[i]["GoodPrice"].ToString();
                                        string GoodType = DataTableZhongChou_Orderdetails.Rows[i]["GoodType"].ToString();///商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                                        string strGoodTypeId = DataTableZhongChou_Orderdetails.Rows[i]["GoodTypeId"].ToString();
                                        string GoodTypeIdBuyInfo = DataTableZhongChou_Orderdetails.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                                        string GoodOrderCount = DataTableZhongChou_Orderdetails.Rows[i]["OrderCount"].ToString();
                                        string strPinglunInfo = DataTableZhongChou_Orderdetails.Rows[i]["Pinglun"].ToString();

                                        EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                                        EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Int32.Parse(GoodTypeIdBuyInfo));

                                        Decimal DecimalAllMoney = Decimal.Multiply(Convert.ToDecimal(strGoodPrice), Convert.ToDecimal(GoodOrderCount));

                                        int intGoodOrderCount = 0;
                                        int.TryParse(GoodOrderCount, out intGoodOrderCount);

                                        int intSupportID = 0;
                                        int.TryParse(GoodTypeIdBuyInfo, out intSupportID);


                                        EggsoftWX.BLL.tab_ZC_01Product_PartnerList BLL_tab_ZC_01Product_PartnerList = new EggsoftWX.BLL.tab_ZC_01Product_PartnerList();

                                        for (int j = 0; j < intGoodOrderCount; j++)
                                        {
                                            EggsoftWX.Model.tab_ZC_01Product_PartnerList Model_tab_ZC_01Product_PartnerList = new EggsoftWX.Model.tab_ZC_01Product_PartnerList();
                                            Model_tab_ZC_01Product_PartnerList.PayPrice = Convert.ToDecimal(strGoodPrice); ;
                                            Model_tab_ZC_01Product_PartnerList.OrderID = OrderID;
                                            Model_tab_ZC_01Product_PartnerList.Ispay = true;
                                            Model_tab_ZC_01Product_PartnerList.PayTime = DateTime.Now;
                                            Model_tab_ZC_01Product_PartnerList.ShopClientID = my_Model_tab_Order.ShopClient_ID;
                                            Model_tab_ZC_01Product_PartnerList.UserID = my_Model_tab_Order.UserID;
                                            Model_tab_ZC_01Product_PartnerList.ZC_01ProductID = Int32.Parse(strGoodTypeId);
                                            Model_tab_ZC_01Product_PartnerList.GetGoodsAddress = my_Model_tab_Order.User_Address;
                                            Model_tab_ZC_01Product_PartnerList.SupportID = intSupportID;
                                            Model_tab_ZC_01Product_PartnerList.ZCBuysSay = strPinglunInfo;
                                            if (Model_tab_ZC_01Product_Support.SupportWay == 0)
                                            {
                                                Model_tab_ZC_01Product_PartnerList.IsCanSendGoods = true;////直接购买 理解发货
                                            }
                                            else if (Model_tab_ZC_01Product_Support.SupportWay == 3 || Model_tab_ZC_01Product_Support.SupportWay == 4)
                                            {
                                                Model_tab_ZC_01Product_PartnerList.IsCanSendGoods = false;////无偿支持 或股权类 不需要发货
                                            }
                                            int intZC_01Product_PartnerListID = BLL_tab_ZC_01Product_PartnerList.Add(Model_tab_ZC_01Product_PartnerList);
                                            System.Threading.Thread.Sleep(1);////给存储过程与反应时间
                                        }
                                        GoodP_ZhongChou.tellShopClientID_UserPayMoney_ByWeiXin_ZhongChou(DecimalAllMoney, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intSupportID);
                                        GoodP_ZhongChou.tellShopClientID_o2o_UserPayMoney_ByWeiXin_ZhongChou(DecimalAllMoney, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intSupportID);
                                        GoodP_ZhongChou.tell_UserIGetMoneyPayMoney_ByWeiXin_ZhongChou(DecimalAllMoney, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intSupportID, OrderID);
                                        GoodP_ZhongChou.tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin_ZhongChou(DecimalAllMoney, OrderID, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intSupportID);

                                    }
                                }
                            }
                        }
                        #endregion

                        #region 判断商品类型是否1 微砍价付款成功  商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                        if (boolIfGoodTypeNot0 == false)
                        {
                            System.Data.DataTable DataTableWeiKanJiaOrderdetails = my_BLL_tab_Orderdetails.GetList("OrderID=" + my_Model_tab_Order.ID + " and GoodType=1 order by id asc").Tables[0];
                            if (DataTableWeiKanJiaOrderdetails.Rows.Count > 0)
                            {
                                boolIfGoodTypeNot0 = true;
                                lock (thisLockIGetMoneyTuanGou)
                                {
                                    for (int i = 0; i < DataTableWeiKanJiaOrderdetails.Rows.Count; i++)
                                    {
                                        string strGoodPrice = DataTableWeiKanJiaOrderdetails.Rows[i]["GoodPrice"].ToString();
                                        string GoodType = DataTableWeiKanJiaOrderdetails.Rows[i]["GoodType"].ToString();
                                        string strGoodTypeId = DataTableWeiKanJiaOrderdetails.Rows[i]["GoodTypeId"].ToString();
                                        string GoodTypeIdBuyInfo = DataTableWeiKanJiaOrderdetails.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                                        string GoodOrderCount = DataTableWeiKanJiaOrderdetails.Rows[i]["OrderCount"].ToString();

                                        int intGoodOrderCount = 0;
                                        int.TryParse(GoodOrderCount, out intGoodOrderCount);

                                        int intTuanGou_Number = 0;
                                        int.TryParse(GoodTypeIdBuyInfo, out intTuanGou_Number);

                                        GoodP_WeiKanJia.tellShopClientID_UserPayMoney_ByWeiXin_WeiKanJia(strGoodPrice, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intTuanGou_Number);
                                        GoodP_WeiKanJia.tellShopClientID_o2o_UserPayMoney_ByWeiXin_WeiKanJia(strGoodPrice, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intTuanGou_Number);
                                        GoodP_WeiKanJia.tell_UserIGetMoneyPayMoney_ByWeiXin_WeiKanJia(strGoodPrice, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intTuanGou_Number);
                                        GoodP_WeiKanJia.tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin_WeiKanJia(strGoodPrice, OrderID, my_Model_tab_Order.UserID.ToString(), Int32.Parse(strGoodTypeId), intTuanGou_Number);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 普通支付 分销+代理
                        if (boolIfGoodTypeNot0 == false)///不是特殊商品 才处理这个继续发送 友情提示的信息。
                        {
                            lock (thisLockIGetMoney0)
                            {
                                tellShopClientID_o2o_UserPayMoney_ByEmail(OrderNum);
                                tellShopClientID_UserPayMoney_ByEmail(OrderNum);
                                tellShopClientID_UserPayMoney_ByWeiXin(OrderNum);
                                tellShopClientID_o2o_UserPayMoney_ByWeiXin(OrderNum);
                                tell_UserIGetMoneyPayMoney_ByWeiXin(OrderID, Convert.ToInt32(my_Model_tab_Order.UserID), Convert.ToDecimal(my_Model_tab_Order.TotalMoney));
                                tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin(OrderID, Convert.ToInt32(my_Model_tab_Order.UserID));
                                #region 是否自动给予分销权  首次购买制作证书
                                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Convert.ToInt32(my_Model_tab_Order.UserID));
                                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + my_Model_tab_Order.UserID + " and IsDeleted=0");
                                if (Model_tab_ShopClient_Agent_ == null || Model_tab_ShopClient_Agent_.Empowered != true)////首次购买制作证书
                                {
                                    EggsoftWX.BLL.tab_ShopClient_ShopPar BLL_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                                    EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = BLL_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Model_tab_User.ShopClientID);
                                    if (tab_ShopClient_ShopPar_Model != null)
                                    {
                                        if (tab_ShopClient_ShopPar_Model.AskAgentAutoAfterBuy.toBoolean())
                                        {

                                            Eggsoft_Public_CL.Pub_Agent.add_Agent_Default_OnlyOneKey(Convert.ToInt32(my_Model_tab_Order.UserID));
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion 普通支付

                    }
                }
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee, "支付通知", "程序报错");
            }
            finally
            {

            }
        }

        public static void tell_UserIGetMoneyPayMoney_ByWeiXin(int intOrderNum, int UserID, decimal DecimalMoney)
        {
            try
            {
                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
                my_Model_tab_User = my_BLL_tab_User.GetModel("id='" + UserID + "'");

                string strTitle = "亲，我们已收到您的订单，金额是：¥" + Pub.getPubMoney(DecimalMoney);
                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(strGet_A_GoodIDList[0]));


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                int intShopClient_ID = Convert.ToInt32(my_Model_tab_Order.ShopClient_ID);
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(intShopClient_ID.ToString(), "TempletPayMessage");///是否可以发模板消息

                int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回


                string strDescription = "亲，我们给你发信，是" + Pub.GetNickName(UserID.ToString()) + "已付款通知 " + GoodP.GetGoodType(Int32.Parse(strGet_A_GoodIDList[1])) + "引起！会尽快安排发货。" + "\n";
                strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n  ";
                strDescription += DateTime.Now + "\n";

                int intParentID = 0;
                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                if (BLL_tab_Orderdetails.Exists("OrderID=" + my_Model_tab_Order.ID))
                {
                    EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel("OrderID=" + my_Model_tab_Order.ID);
                    intParentID = ((Model_tab_Orderdetails.ParentID == null) ? 0 : Convert.ToInt32(Model_tab_Orderdetails.ParentID));
                }
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = bll_tab_ShopClient_Agent_.GetModel("UserID=" + UserID + " and Empowered=1" + " and IsDeleted=0 and ShopClientID=" + intUserIDShopClientID);
                string strAgent = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(UserID);

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(UserID);
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                if (strGet_A_GoodIDList[1] == "0")
                {
                    strErJiYuMing = "https://" + strErJiYuMing + strAgent + "/product-" + myModel_tab_Goods.ID + ".aspx";
                }
                else if (strGet_A_GoodIDList[1] == "2")
                {
                    strErJiYuMing = "https://" + strErJiYuMing + "/AddFunction/02PingTuan/03Goods.html?tuangouid=" + strGet_A_GoodIDList[2] + "&tuangouidnumber=" + strGet_A_GoodIDList[3];
                }
                else if (strGet_A_GoodIDList[1] == "6")
                {
                    strErJiYuMing = "https://" + strErJiYuMing + "/op-" + strGet_A_GoodIDList[2] + "-" + strGet_A_GoodIDList[3] + ".aspx";
                }



                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                WeiXinTuWens_ArrayList.Add(First);


                string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(UserID, 0, WeiXinTuWens_ArrayList);

                string[] strCheckReSendList = { "45015", "45047" };
                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                if (exists)
                {

                    if (boolTempletPayMessage)
                    {
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(UserID, Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(UserID.ToString()) + "付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                    }
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "支付通知-分销", "程序报错");
            }
            finally
            {

            }
        }



        public static void tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin(int intOrderNum, int UserID)
        {
            try
            {
                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(Eggsoft_Public_CL.Pub.GetShopClientIDFromOrderID(intOrderNum).ToString(), "TempletPayMessage");///是否可以发模板消息
                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                System.Data.DataTable myDataTable = BLL_tab_Orderdetails.GetList("OrderID=" + intOrderNum + " and isnull(isdeleted,0)=0").Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {

                    string str_ShopClient_ID = myDataTable.Rows[i]["ShopClient_ID"].ToString();
                    string str_Orderdetails_ID = myDataTable.Rows[i]["ID"].ToString();
                    string strParentID = myDataTable.Rows[i]["ParentID"].ToString();
                    string strGrandParentID = myDataTable.Rows[i]["GrandParentID"].ToString();
                    string strGreatParentID = myDataTable.Rows[i]["GreatParentID"].ToString();
                    string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                    string strGoodPrice = myDataTable.Rows[i]["GoodPrice"].ToString();
                    string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();
                    string strGoodType = myDataTable.Rows[i]["GoodType"].ToString();///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                    string strGoodTypeId = myDataTable.Rows[i]["GoodTypeId"].ToString();
                    string strGoodTypeIdBuyInfo = myDataTable.Rows[i]["GoodTypeIdBuyInfo"].ToString();


                    int intParentID = 0;
                    int.TryParse(strParentID, out intParentID);
                    #region 天使的自己不享受享受收入
                    if ((intParentID == UserID) && (BLL_tab_ShopClient_Agent_.Exists("UserID=" + intParentID + " and OnlyIsAngel=1 and IsDeleted=0")))
                    {
                        intParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(UserID);///天使的父亲不要是自己。天使本人不享受分销提成
                    }
                    #endregion 天使的自己不享受享受收入


                    EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(GoodP.Get_A_GoodID_From_Order_ID(intOrderNum)[0]));
                    string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);
                    string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;

                    EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + UserID + " and ShopClientID=" + str_ShopClient_ID + " and IsDeleted=0 and isnull(Empowered,0)=1");///有代理啊
                    string strAgent = "";
                    if (boolAgent)
                    {
                        strAgent = "/sagent-" + UserID;
                    }
                    else if (intParentID > 0)
                    {
                        strAgent = "/sagent-" + strParentID;
                    }
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(UserID);
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));
                    string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                    strErJiYuMing = "https://" + strErJiYuMing + strAgent + "/product-" + myModel_tab_Goods.ID + ".aspx";


                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                    int intShopClient_ID = Convert.ToInt32(my_Model_tab_Order.ShopClient_ID);



                    #region 用户没有支付现金  什么也不干
                    ///用户需要支付的现金 包含已有现金  不包含购物券  计算代理所得的钱  从订单详细表中
                    Decimal DecimalUserPayMoney = Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_PriceFromtab_OrderdetailsID(str_Orderdetails_ID.toInt32());
                    if (DecimalUserPayMoney <= 0) continue;//用户没有支付现金 什么也不干
                    #endregion 用户没有支付现金  什么也不干

                    #region 普通三级分销计算收入+代理的收入
                    Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                    myModel_MultiFenXiaoLevel.UserID = UserID;
                    myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                    myModel_MultiFenXiaoLevel.intParentID = intParentID;
                    myModel_MultiFenXiaoLevel.strGoodType = strGoodType;
                    myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                    myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                    Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);
                    myModel_MultiFenXiaoLevel.FenXiaoLevelLength = (myModel_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (myModel_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (myModel_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();


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

                    //#region  代理的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    //if (DecimalUserPayMoney < AdvanceParentAgentGet) AdvanceParentAgentGet = 0;
                    //if ((DecimalUserPayMoney - AdvanceParentAgentGet) < AdvanceManagerAgentAgentGet) AdvanceManagerAgentAgentGet = 0;
                    //if ((DecimalUserPayMoney - AdvanceParentAgentGet - AdvanceManagerAgentAgentGet) < AdvanceManagerGrandAgentParentIDGet) AdvanceManagerGrandAgentParentIDGet = 0;
                    //#endregion   由于用户可能是购物券付款 要计算实际购物所得

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


                    //#region  三级分销的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    //Decimal? AgentGetDis = myModel_MultiFenXiaoLevel.AgentGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? ManagerAgentDis = myModel_MultiFenXiaoLevel.ManagerAgentGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //Decimal? ManagerGrandAgentParentDis = myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strGoodPrice.toDecimal() * strOrderCount.toDecimal() * (Decimal)0.01;
                    //if (DecimalUserPayMoney < AgentGetDis) AgentGetDis = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis) < ManagerAgentDis) ManagerAgentDis = 0;
                    //if ((DecimalUserPayMoney - AgentGetDis - ManagerAgentDis) < ManagerGrandAgentParentDis) ManagerGrandAgentParentDis = 0;
                    //#endregion   由于用户可能是购物券付款 要计算实际购物所得

                    int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(intParentID);
                    if ((intParentID > 0) && (AgentGetDis > 0))///代理自己下单不享受差价
                    {
                        string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(str_ShopClient_ID) : "现金");

                        Decimal DecimalMoney = AgentGetDis.toDecimal();
                        string strTitle = "一级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们已经计算出您分享链接" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                        ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                        //实例化几个WeiXinTuWen类对象  

                        string strDescription = "一级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们给你发信，是" + Pub.GetNickName(UserID.ToString()) + "已付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                        strDescription += "亲，T+7后，" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "所得将会自动转入您的" + strMoneyShow + "余额。" + "\n";
                        strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                        strDescription += DateTime.Now + "\n";

                        ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                        WeiXinTuWens_ArrayList.Add(First);


                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(strParentID), 0, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(strParentID), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(UserID.ToString()) + "付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                            }
                        }
                    }
                    intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strGrandParentID.toInt32());
                    if ((strGrandParentID.toInt32() > 0) && (ManagerAgentDis > 0))
                    {
                        string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(str_ShopClient_ID) : "现金");
                        Decimal DecimalMoney = ManagerAgentDis.toDecimal();

                        string strTitle = "二级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们已经计算出您分享链接" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                        ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                        //实例化几个WeiXinTuWen类对象  



                        string strDescription = "二级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们给你发信，是" + Pub.GetNickName(UserID.ToString()) + "已付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                        strDescription += "亲，T+7后，" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "所得将会自动转入您的" + strMoneyShow + "余额。" + "\n";
                        strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                        strDescription += DateTime.Now + "\n";

                        ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                        WeiXinTuWens_ArrayList.Add(First);


                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(strGrandParentID.toInt32(), 0, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {

                            if (boolTempletPayMessage)
                            {
                                //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)

                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(strGrandParentID.toInt32(), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(UserID.ToString()) + "付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                            }
                        }
                    }
                    intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strGreatParentID.toInt32());
                    if ((strGreatParentID.toInt32() > 0) && (ManagerGrandAgentParentDis > 0))
                    {
                        string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(str_ShopClient_ID) : "现金");
                        Decimal DecimalMoney = ManagerGrandAgentParentDis.toDecimal();

                        string strTitle = "三级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们已经计算出您分享链接" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                        ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                        //实例化几个WeiXinTuWen类对象  



                        string strDescription = "三级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们给你发信，是" + Pub.GetNickName(UserID.ToString()) + "已付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                        strDescription += "亲，T+7后，" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "所得将会自动转入您的" + strMoneyShow + "余额。" + "\n";
                        strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                        strDescription += DateTime.Now + "\n";

                        ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                        WeiXinTuWens_ArrayList.Add(First);


                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(strGreatParentID.toInt32(), 0, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(strGreatParentID.toInt32(), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(UserID.ToString()) + "付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                            }
                        }
                    }
                    #endregion 普通三级分销计算收入
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "支付通知");
            }
            finally
            {

            }
        }

        /// <summary>
        /// 告知用户 如购买 会赠送购物券 现金
        /// </summary>
        /// <param name="pIntGoodID"></param>
        /// <param name="pub_Int_ShopClientID"></param>
        /// <param name="pub_Int_Session_CurUserID"></param>
        public static void tell_User_UserWillPayMoney_ByWeiXin(String OrderNum)
        {
            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
            my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");

            int pInt_Session_CurUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
            EggsoftWX.BLL.tab_Orderdetails my_BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
            int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
            if (intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回
            bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(intUserIDShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

            System.Data.DataTable myDataTableOrderdetails = my_BLL_tab_Orderdetails.GetList("ID,ParentID,GoodID,GoodType,GoodTypeId,GoodTypeIdBuyInfo,GoodPrice,OrderCount", "OrderID=" + my_Model_tab_Order.ID + " and isdeleted<>1").Tables[0];

            EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intUserIDShopClientID);


            string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;



            for (int i = 0; i < myDataTableOrderdetails.Rows.Count; i++)
            {
                string strGoodPrice = myDataTableOrderdetails.Rows[i]["GoodPrice"].ToString();////微砍价 需要该值
                string strOrderdetailsID = myDataTableOrderdetails.Rows[i]["ID"].ToString();
                string strQueryString_ParentID = myDataTableOrderdetails.Rows[i]["ParentID"].ToString();
                string strGooid = myDataTableOrderdetails.Rows[i]["GoodID"].ToString();
                string strOrderCount = myDataTableOrderdetails.Rows[i]["OrderCount"].ToString();
                string strGoodType = myDataTableOrderdetails.Rows[i]["GoodType"].ToString();///商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                string strGoodTypeId = myDataTableOrderdetails.Rows[i]["GoodTypeId"].ToString();
                string strGoodTypeIdBuyInfo = myDataTableOrderdetails.Rows[i]["GoodTypeIdBuyInfo"].ToString();


                my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(strGooid.toInt32());
                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pInt_Session_CurUserID.ToString());
                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pInt_Session_CurUserID.ToString());
                string strTitle = strUserNickName + "亲,即将付款，“" + my_Model_tab_Goods.Name + "”," + GoodP.GetGoodType(Int32.Parse(strGoodType)) + ")";

                string strDescription = "";
                strDescription += my_Model_tab_Goods.ShortInfo + "。";
                if (strGoodType == "0")
                {
                    if (my_Model_tab_Goods.Send_Vouchers_IfBuy > 0)
                    {
                        strDescription += "购买赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intUserIDShopClientID.ToString()) + Convert.ToDecimal(my_Model_tab_Goods.Send_Vouchers_IfBuy) * strOrderCount.toDecimal() + "元。T+7后计入您的账户";
                    }
                    if (my_Model_tab_Goods.Send_Money_IfBuy > 0)
                    {
                        strDescription += "购买赠送现金" + Convert.ToDecimal(my_Model_tab_Goods.Send_Money_IfBuy) * strOrderCount.toDecimal() + "元。T+7后计入您的账户";
                    }
                }
                strDescription += "代理微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "\n";

                string strURL = "";
                if (strGoodType == "0")///一般分销+代理
                {
                    strURL = "https://" + strErJiYuMing + "/product-" + strGooid + ".aspx" + "?parentid=" + strQueryString_ParentID;
                }
                else if (strGoodType == "1")///微砍价
                {
                    strURL = "https://" + strErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=" + strGoodTypeId + "&parentid=" + strQueryString_ParentID;
                }
                else if (strGoodType == "2")///团购
                {
                    strURL = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId + "&tuangouidnumber=" + strGoodTypeIdBuyInfo + "&parentid=" + strQueryString_ParentID;
                }
                else if (strGoodType == "3")///众筹
                {
                    strURL = "https://" + strErJiYuMing + "/addfunction/04ZC_project/03ZC.html=" + strGoodTypeId + "&ZCid=" + strGoodTypeId + "&parentid=" + strQueryString_ParentID;
                }
                else if (strGoodType == "6")///运营中心
                {
                    strURL = "https://" + strErJiYuMing + "/addfunction/op-" + strGoodTypeId + "-" + strGoodTypeIdBuyInfo + ".aspx";
                }
                string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);
                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;

                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);
                string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pInt_Session_CurUserID, pInt_Session_CurUserID, WeiXinTuWens_ArrayList);
                string[] strCheckReSendList = { "45015", "45047" };
                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                if (exists)
                {
                    if (boolTempletVisitMessage)
                    {
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_Session_CurUserID, intUserIDShopClientID, WeiXinTuWens_ArrayList);
                    }
                }
            }

        }




        public static void tell_DistributionMoney_UserWillPayMoney_ByWeiXin(String OrderNum)
        {
            try
            {


                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");

                int pInt_Session_CurUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                EggsoftWX.BLL.tab_Orderdetails my_BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回
                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(intUserIDShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                System.Data.DataTable myDataTableOrderdetails = my_BLL_tab_Orderdetails.GetList("ID,ParentID,GrandParentID,GreatParentID,GoodID,GoodType,GoodTypeId,GoodTypeIdBuyInfo,GoodPrice,OrderCount", "OrderID=" + my_Model_tab_Order.ID + " and isdeleted<>1").Tables[0];


                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intUserIDShopClientID);


                string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;



                for (int i = 0; i < myDataTableOrderdetails.Rows.Count; i++)
                {
                    string strGoodPrice = myDataTableOrderdetails.Rows[i]["GoodPrice"].ToString();////微砍价 需要该值
                    string strOrderdetailsID = myDataTableOrderdetails.Rows[i]["ID"].ToString();
                    string strQueryString_ParentID = myDataTableOrderdetails.Rows[i]["ParentID"].ToString();
                    string strQueryString_GrandParentID = myDataTableOrderdetails.Rows[i]["GrandParentID"].ToString();
                    string strQueryString_GreatParentID = myDataTableOrderdetails.Rows[i]["GreatParentID"].ToString();

                    string strGooid = myDataTableOrderdetails.Rows[i]["GoodID"].ToString();
                    string strOrderCount = myDataTableOrderdetails.Rows[i]["OrderCount"].ToString();
                    string strGoodType = myDataTableOrderdetails.Rows[i]["GoodType"].ToString();///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                    string strGoodTypeId = myDataTableOrderdetails.Rows[i]["GoodTypeId"].ToString();
                    string strGoodTypeIdBuyInfo = myDataTableOrderdetails.Rows[i]["GoodTypeIdBuyInfo"].ToString();

                    int pIntGoodID = 0;//；
                    int pInt_QueryString_ParentID = 0;//；
                    if (string.IsNullOrEmpty(strGooid) == false) pIntGoodID = Int32.Parse(strGooid);
                    if (string.IsNullOrEmpty(strQueryString_ParentID) == false) pInt_QueryString_ParentID = Int32.Parse(strQueryString_ParentID);


                    if (pInt_QueryString_ParentID == 0) return;
                    my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);

                    string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);
                    string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;

                    string strURL = "";
                    if (strGoodType == "0")///一般分销+代理
                    {
                        strURL = "https://" + strErJiYuMing + "/product-" + pIntGoodID.ToString() + ".aspx" + "?parentid=" + pInt_QueryString_ParentID;
                    }
                    else if (strGoodType == "1")///微砍价
                    {
                        strURL = "https://" + strErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=" + strGoodTypeId + "&parentid=" + pInt_QueryString_ParentID;
                    }
                    else if (strGoodType == "2")///团购
                    {
                        strURL = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId + "&tuangouidnumber=" + strGoodTypeIdBuyInfo + "&parentid=" + pInt_QueryString_ParentID;
                    }
                    else if (strGoodType == "3")///众筹
                    {
                        strURL = "https://" + strErJiYuMing + "/addfunction/04ZC_project/03ZC.html=" + strGoodTypeId + "&ZCid=" + strGoodTypeId + "&parentid=" + pInt_QueryString_ParentID;
                    }
                    else if (strGoodType == "6")///运营中心
                    {
                        strURL = "https://" + strErJiYuMing + "/op-" + strGoodTypeId + "-" + strGoodTypeIdBuyInfo + ".aspx";
                    }
                    #region 检查是否运营中心的所得
                    bool boolOperationGoods_AgentGet = false;
                    if (strGoodType == "6")///团购  商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                    {
                        boolOperationGoods_AgentGet = true;


                    }
                    #endregion 检查是否运营中心的所得


                    #region 检查是否团购的团长所得
                    bool boolTuanZhangBonus_AgentGet = false;
                    EggsoftWX.BLL.tab_TuanGou my_BLLtab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                    EggsoftWX.Model.tab_TuanGou my_Modeltab_TuanGou = new EggsoftWX.Model.tab_TuanGou();
                    if (strGoodType == "2")///团购  商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                    {
                        my_Modeltab_TuanGou = my_BLLtab_TuanGou.GetModel(Int32.Parse(strGoodTypeId));
                        boolTuanZhangBonus_AgentGet = Convert.ToBoolean(my_Modeltab_TuanGou.TuanZhangBonus_AgentGet);
                    }
                    #endregion 检查是否团购的团长所得

                    if (boolOperationGoods_AgentGet)
                    {
                        if ((pInt_QueryString_ParentID == pInt_Session_CurUserID))
                        {
                            pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_Session_CurUserID);///父亲不要是自己。天使本人不享受分销提成
                        }

                        #region 是运营中心模式
                        EggsoftWX.BLL.b004_OperationGoods BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                        EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = BLL_b004_OperationGoods.GetModel("ID=" + strGoodTypeIdBuyInfo + " and ShopClient_ID=" + intUserIDShopClientID + " and RunningStatus=1");
                        if (Model_b004_OperationGoods != null)
                        {
                            //上级不能是本人
                            Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                            myModel_MultiFenXiaoLevel.GoodID = pIntGoodID.toInt32();
                            myModel_MultiFenXiaoLevel.intParentID = pInt_QueryString_ParentID;
                            myModel_MultiFenXiaoLevel.strGoodType = strGoodType;
                            myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                            myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                            myModel_MultiFenXiaoLevel.DecimalGoodPrice = Decimal.Parse(strGoodPrice);
                            myModel_MultiFenXiaoLevel.UserID = pInt_Session_CurUserID.toInt32();
                            Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);

                            #region 上级
                            int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(pInt_QueryString_ParentID);
                            if (intUserID != pInt_QueryString_ParentID && (pInt_QueryString_ParentID != 0) && (Model_b004_OperationGoods.ReturnMoneyShareB > 0) && (intIF_Agent_From_Database_ > 0))//处理父亲的消息
                            {
                                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString());
                                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(intUserID.ToString());
                                string strTitle = (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的财富一级分享链接“" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "即将付款";
                                string strShortInfoDescription = "";
                                strShortInfoDescription += my_Model_tab_Goods.ShortInfo + "。";




                                System.Collections.ArrayList WeiXinTuWens_ArrayListTemplet = new System.Collections.ArrayList();
                                Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTemplet = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strShortInfoDescription, strURL);
                                WeiXinTuWens_ArrayListTemplet.Add(FirstTemplet);

                                string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intUserIDShopClientID.ToString()) : "现金");
                                System.Collections.ArrayList WeiXinTuWens_ArrayListTuWen = new System.Collections.ArrayList();
                                String strTuwen = strShortInfoDescription + "代理亲。如果购买你财富一级分享的商品，将会获得所代理的商品的";
                                if (intIF_Agent_From_Database_ == 3)
                                {
                                    strTuwen += Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_b004_OperationGoods.ReturnMoneyShareB * my_Model_tab_Goods.PromotePrice * (Decimal)0.01)) + "" + strMoneyShow + "部分喔！" + "。";
                                }
                                else if (intIF_Agent_From_Database_ == 5 || intIF_Agent_From_Database_ == 7)
                                {
                                    strTuwen += Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_b004_OperationGoods.ReturnMoneyShareB * my_Model_tab_Goods.PromotePrice * (Decimal)0.01)) + "元" + "部分喔！" + "。";
                                }
                                strTuwen += "代理微店，真正一键微店。0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "。";
                                Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTuWen = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strTuwen, strURL);
                                WeiXinTuWens_ArrayListTuWen.Add(FirstTuWen);

                                string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pInt_QueryString_ParentID, intUserIDShopClientID, WeiXinTuWens_ArrayListTuWen);
                                string[] strCheckReSendList = { "45015", "45047" };
                                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                if (exists)
                                {
                                    if (boolTempletVisitMessage)
                                    {
                                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_ParentID, intUserIDShopClientID, WeiXinTuWens_ArrayListTemplet);
                                    }
                                }
                            }
                            #endregion 上级


                            #region 上上级
                            intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(myModel_MultiFenXiaoLevel.ManagerAgentParentID);
                            if (intUserID != myModel_MultiFenXiaoLevel.ManagerAgentParentID && intIF_Agent_From_Database_ > 0 && (pInt_QueryString_ParentID != 0) && (Model_b004_OperationGoods.ReturnMoneyShareA > 0) && (myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0)) //处理上级代理人的消息
                            {

                                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString());
                                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(intUserID.ToString());

                                string strTitle = (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的财富二级分享链接“" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "即将付款";

                                string strShortInfoDescription = "";
                                strShortInfoDescription += my_Model_tab_Goods.ShortInfo + "。";

                                System.Collections.ArrayList WeiXinTuWens_ArrayListTemplet = new System.Collections.ArrayList();
                                Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTemplet = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strShortInfoDescription, strURL);
                                WeiXinTuWens_ArrayListTemplet.Add(FirstTemplet);
                                string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intUserIDShopClientID.ToString()) : "现金");



                                System.Collections.ArrayList WeiXinTuWens_ArrayListTuWen = new System.Collections.ArrayList();
                                String strTuwen = strShortInfoDescription + "代理亲。如果有人购买你财富二级分享的商品，将会获得二级代理的商品的";
                                if (intIF_Agent_From_Database_ == 3)
                                {
                                    strTuwen += Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_b004_OperationGoods.ReturnMoneyShareA * my_Model_tab_Goods.PromotePrice * (Decimal)0.01)) + "" + strMoneyShow + "部分喔！" + "。";
                                }
                                else if (intIF_Agent_From_Database_ == 5 || intIF_Agent_From_Database_ == 7)
                                {
                                    strTuwen += Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_b004_OperationGoods.ReturnMoneyShareA * my_Model_tab_Goods.PromotePrice * (Decimal)0.01)) + "元" + "部分喔！" + "。";
                                }
                                strTuwen += "代理微店，真正一键微店。0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "。";
                                Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTuWen = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strTuwen, strURL);
                                WeiXinTuWens_ArrayListTuWen.Add(FirstTuWen);

                                string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, intUserID, WeiXinTuWens_ArrayListTuWen);
                                string[] strCheckReSendList = { "45015", "45047" };
                                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                if (exists)
                                {
                                    if (boolTempletVisitMessage)
                                    {
                                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, intUserIDShopClientID, WeiXinTuWens_ArrayListTemplet);
                                    }
                                }

                            }
                            #endregion 上上级

                        }





                        #endregion 是分销模式

                    }
                    else if (boolTuanZhangBonus_AgentGet)
                    {
                        #region 给团长发消息
                        string strTuanZhangUserID = "0";
                        string strGetTuanZhangUserID = "select UserID from tab_TuanGou_Partner where TuanGouIDNumber=" + strGoodTypeIdBuyInfo + " and ParterRole=1 and ShopClientID=" + my_Modeltab_TuanGou.ShopClientID;
                        System.Data.DataTable DataTableGetTuanZhangUserID = BLL_tab_ShopClient.SelectList(strGetTuanZhangUserID).Tables[0];
                        if (DataTableGetTuanZhangUserID.Rows.Count > 0)
                        {
                            strTuanZhangUserID = DataTableGetTuanZhangUserID.Rows[0]["UserID"].ToString();
                        }
                        if (strTuanZhangUserID != "0" && (Convert.ToDecimal(my_Modeltab_TuanGou.TuanZhangBonus_AgentGet) > 0))////还没有组团的 这里 应该是0  那就自动不发通知
                        {
                            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pInt_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pInt_Session_CurUserID.ToString());
                            string strTitle = "团长大人亲,你的分享链接“" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "即将付款 " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + ")";

                            string strDescription = "";
                            strDescription += my_Model_tab_Goods.ShortInfo + "\n";
                            strDescription += "团长大人亲。如果有人购买你组团的商品，将会获得所组团的商品的" + my_Modeltab_TuanGou.TuanZhangBonus_AgentGet + "现金部分喔！" + "\n";
                            strDescription += "组团微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "\n";


                            Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                            WeiXinTuWens_ArrayList.Add(First);
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pInt_QueryString_ParentID, pInt_Session_CurUserID, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {
                                if (boolTempletVisitMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(strTuanZhangUserID), intUserIDShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }
                        }
                        #endregion 给团长发消息
                    }
                    else
                    {
                        EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();

                        #region 用户没有支付现金  什么也不干
                        ///用户需要支付的现金 包含已有现金  不包含购物券  计算代理所得的钱  从订单详细表中
                        Decimal DecimalUserPayMoney = Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_PriceFromtab_OrderdetailsID(strOrderdetailsID.toInt32());
                        if (DecimalUserPayMoney <= 0) continue;//用户没有支付现金 什么也不干
                        #endregion 用户没有支付现金  什么也不干

                        //bool boolIfDaiLi = false;

                        #region 是普通分销模式+代理模式
                        int intParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_Session_CurUserID);
                        int intGrandID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intParentID);
                        int intGreatParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intGrandID);





                        Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                        myModel_MultiFenXiaoLevel.GoodID = pIntGoodID.toInt32();
                        myModel_MultiFenXiaoLevel.intParentID = pInt_QueryString_ParentID;
                        myModel_MultiFenXiaoLevel.strGoodType = strGoodType;
                        myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                        myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                        myModel_MultiFenXiaoLevel.UserID = intUserID;
                        myModel_MultiFenXiaoLevel.DecimalGoodPrice = Decimal.Parse(strGoodPrice);
                        Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);

                        //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(pIntGoodID, pInt_QueryString_ParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, strGoodType, strGoodTypeId, strGoodTypeIdBuyInfo, Decimal.Parse(strGoodPrice));
                        #region  各级代理自己的价格
                        //Decimal myAdvancePriceProductPricepParentUserd_ID = 0;
                        //Decimal myAdvanceProductPricepManagerAgentParentID = 0;
                        //Decimal myAdvanceProductPriceManagerGrandAgentParentID = 0;
                        //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_AdvanceAgentProduct(pIntGoodID, intParentID, intGrandID, intGreatParentID, out myAdvancePriceProductPricepParentUserd_ID, out myAdvanceProductPricepManagerAgentParentID, out myAdvanceProductPriceManagerGrandAgentParentID);

                        //my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);



                        //Decimal? AdvanceParentAgentGet = DecimalUserPayMoney - myAdvancePriceProductPricepParentUserd_ID * strOrderCount.toInt32(); if (AdvanceParentAgentGet < 0) AdvanceParentAgentGet = 0;
                        //if (myAdvancePriceProductPricepParentUserd_ID == 0) AdvanceParentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                        //Decimal? LevelParentTrueGet = (AdvanceParentAgentGet > (myModel_MultiFenXiaoLevel.AgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceParentAgentGet : (myModel_MultiFenXiaoLevel.AgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得


                        //Decimal? AdvanceManagerAgentAgentGet = DecimalUserPayMoney - myAdvanceProductPricepManagerAgentParentID * strOrderCount.toInt32() - LevelParentTrueGet; if (AdvanceManagerAgentAgentGet < 0) AdvanceManagerAgentAgentGet = 0;
                        //if (myAdvanceProductPricepManagerAgentParentID == 0) AdvanceManagerAgentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                        //Decimal? LevelManagerParentTrueGet = (AdvanceManagerAgentAgentGet > (myModel_MultiFenXiaoLevel.ManagerAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceManagerAgentAgentGet : (myModel_MultiFenXiaoLevel.ManagerAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得

                        //Decimal? AdvanceManagerGrandAgentParentIDGet = DecimalUserPayMoney - myAdvanceProductPriceManagerGrandAgentParentID * strOrderCount.toInt32() - LevelManagerParentTrueGet - LevelParentTrueGet; if (AdvanceManagerGrandAgentParentIDGet < 0) AdvanceManagerGrandAgentParentIDGet = 0;
                        //if (myAdvanceProductPriceManagerGrandAgentParentID == 0) AdvanceManagerGrandAgentParentIDGet = 0;//父亲不是代理 就不要拿代理差价
                        //Decimal? LevelGrandParentTrueGet = (AdvanceManagerGrandAgentParentIDGet > (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceManagerGrandAgentParentIDGet : (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得

                        //#region  代理的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                        //if (DecimalUserPayMoney < AdvanceParentAgentGet) AdvanceParentAgentGet = 0;
                        //if ((DecimalUserPayMoney - AdvanceParentAgentGet) < AdvanceManagerAgentAgentGet) AdvanceManagerAgentAgentGet = 0;
                        //if ((DecimalUserPayMoney - AdvanceParentAgentGet - AdvanceManagerAgentAgentGet) < AdvanceManagerGrandAgentParentIDGet) AdvanceManagerGrandAgentParentIDGet = 0;
                        //#endregion   由于用户可能是购物券付款 要计算实际购物所得

                        #endregion  各级代理自己的价格
                        //#region  各级代理自己的价格
                        //Decimal myAdvancePriceProductPricepParentUserd_ID = 0;
                        //Decimal myAdvanceProductPricepManagerAgentParentID = 0;
                        //Decimal myAdvanceProductPriceManagerGrandAgentParentID = 0;
                        //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_AdvanceAgentProduct(pIntGoodID, pInt_QueryString_ParentID, myModel_MultiFenXiaoLevel.ManagerAgentParentID, myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, out myAdvancePriceProductPricepParentUserd_ID, out myAdvanceProductPricepManagerAgentParentID, out myAdvanceProductPriceManagerGrandAgentParentID);

                        //Decimal? AdvanceParentAgentGet = DecimalUserPayMoney - myAdvancePriceProductPricepParentUserd_ID * strOrderCount.toInt32(); if (AdvanceParentAgentGet < 0) AdvanceParentAgentGet = 0;
                        //if (myAdvancePriceProductPricepParentUserd_ID == 0) AdvanceParentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                        //Decimal? LevelParentTrueGet = (AdvanceParentAgentGet > (myModel_MultiFenXiaoLevel.AgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01) ? AdvanceParentAgentGet : (myModel_MultiFenXiaoLevel.AgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01));////可能作为分销商的取得

                        //Decimal? AdvanceManagerAgentAgentGet = DecimalUserPayMoney - myAdvanceProductPricepManagerAgentParentID * strOrderCount.toInt32() - LevelParentTrueGet; if (AdvanceManagerAgentAgentGet < 0) AdvanceManagerAgentAgentGet = 0;
                        //if (myAdvanceProductPricepManagerAgentParentID == 0) AdvanceManagerAgentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                        //Decimal? LevelManagerParentTrueGet = (AdvanceManagerAgentAgentGet > (myModel_MultiFenXiaoLevel.ManagerAgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01) ? AdvanceManagerAgentAgentGet : (myModel_MultiFenXiaoLevel.ManagerAgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01));////可能作为分销商的取得

                        //Decimal? AdvanceManagerGrandAgentParentIDGet = DecimalUserPayMoney - myAdvanceProductPriceManagerGrandAgentParentID * strOrderCount.toInt32() - LevelParentTrueGet - LevelManagerParentTrueGet; if (AdvanceManagerGrandAgentParentIDGet < 0) AdvanceManagerGrandAgentParentIDGet = 0;
                        //if (myAdvanceProductPriceManagerGrandAgentParentID == 0) AdvanceManagerGrandAgentParentIDGet = 0;//父亲不是代理 就不要拿代理差价
                        //Decimal? LevelGrandParentTrueGet = (AdvanceManagerGrandAgentParentIDGet > (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01) ? AdvanceManagerGrandAgentParentIDGet : (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01));////可能作为分销商的取得

                        //#region  代理的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                        //if (DecimalUserPayMoney < AdvanceParentAgentGet) AdvanceParentAgentGet = 0;
                        //if ((DecimalUserPayMoney - AdvanceParentAgentGet) < AdvanceManagerAgentAgentGet) AdvanceManagerAgentAgentGet = 0;
                        //if ((DecimalUserPayMoney - AdvanceParentAgentGet - AdvanceManagerAgentAgentGet) < AdvanceManagerGrandAgentParentIDGet) AdvanceManagerGrandAgentParentIDGet = 0;
                        //#endregion   由于用户可能是购物券付款 要计算实际购物所得

                        //#endregion  各级代理自己的价格

                        #region  三级分销的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
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
                        #endregion   由于用户可能是购物券付款 要计算实际购物所得


                        int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(pInt_QueryString_ParentID);
                        if ((intParentID > 0) && (AgentGetDis > 0))///代理自己下单不享受差价
                        {
                            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pInt_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pInt_Session_CurUserID.ToString());
                            string strTitle = "一级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的分享链接“" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "即将付款 " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + ")";
                            string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intUserIDShopClientID.ToString()) : "现金");

                            string strDescription = "";
                            strDescription += my_Model_tab_Goods.ShortInfo + "\n";
                            strDescription += "一级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲。如果有人购买你分享的商品，将会获得所代理的商品的";
                            //if (LevelParentTrueGet > 0)
                            //{
                            strDescription += "" + Eggsoft_Public_CL.Pub.getBankPubMoney(Convert.ToDecimal(AgentGetDis)) + "元" + strMoneyShow + "部分喔！" + "\n";
                            //}
                            //else
                            //{
                            //strDescription += "百分比" + AgentGetDis + " % " + strMoneyShow + "部分！" + "\n";
                            //}
                            strDescription += "代理微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "\n";


                            Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                            WeiXinTuWens_ArrayList.Add(First);
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pInt_QueryString_ParentID, pInt_Session_CurUserID, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {
                                if (boolTempletVisitMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_ParentID, intUserIDShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }
                        }

                        intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strQueryString_GrandParentID.toInt32());
                        if (ManagerAgentDis > 0 && strQueryString_GrandParentID.toInt32() > 0) //处理上级代理人的消息
                        {

                            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                            //实例化几个WeiXinTuWen类对象  
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pInt_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pInt_Session_CurUserID.ToString());

                            string strTitle = "二级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的分享链接“" + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "浏览";
                            string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intUserIDShopClientID.ToString()) : "现金");

                            string strDescription = "";
                            strDescription += my_Model_tab_Goods.ShortInfo + "\n";
                            strDescription += "二级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲。如果有人购买你分享的商品，将会获得下级代理的商品的";
                            //if (LevelManagerParentTrueGet > 0)
                            //{
                            strDescription += "" + Eggsoft_Public_CL.Pub.getBankPubMoney(Convert.ToDecimal(ManagerAgentDis)) + "元" + strMoneyShow + "部分喔！" + "\n";
                            //}
                            //else
                            //{
                            // strDescription += "百分比" + ManagerAgentDis + " % " + strMoneyShow + "部分喔！" + "\n";
                            //}
                            strDescription += "代理微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "\n";



                            Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                            WeiXinTuWens_ArrayList.Add(First);
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(strQueryString_GrandParentID.toInt32(), pInt_Session_CurUserID, WeiXinTuWens_ArrayList);
                            if (("45015" == strMessageImage))
                            {
                                if (boolTempletVisitMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(strQueryString_GrandParentID.toInt32(), intUserIDShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }
                        }

                        intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strQueryString_GreatParentID.toInt32());
                        if (ManagerGrandAgentParentDis > 0 && strQueryString_GreatParentID.toInt32() > 0) //处理上上级代理人的消息
                        {

                            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                            //实例化几个WeiXinTuWen类对象  
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pInt_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pInt_Session_CurUserID.ToString());

                            string strTitle = "三级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的分享链接“ " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "浏览";
                            string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intUserIDShopClientID.ToString()) : "现金");

                            string strDescription = "";
                            strDescription += my_Model_tab_Goods.ShortInfo + "\n";
                            strDescription += "三级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲。如果有人购买你分享的商品，将会获得下级代理的商品的";
                            //if (LevelGrandParentTrueGet > 0)
                            //{
                            strDescription += "" + Eggsoft_Public_CL.Pub.getBankPubMoney(Convert.ToDecimal(ManagerGrandAgentParentDis)) + "元" + strMoneyShow + "部分喔！" + "\n";
                            //}
                            //else
                            //{
                            //   strDescription += "百分比" + myModel_MultiFenXiaoLevel.ManagerGrandAgentGet + "%" + strMoneyShow + "部分喔！" + "\n";
                            //}

                            strDescription += "代理微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "\n";



                            Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                            WeiXinTuWens_ArrayList.Add(First);
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(strQueryString_GreatParentID.toInt32(), pInt_Session_CurUserID, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {
                                if (boolTempletVisitMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(strQueryString_GreatParentID.toInt32(), intUserIDShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }
                        }
                        //}
                        #endregion 是分销模式
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
        public static void tellShopClientID_UserWillPayMoney_ByWeiXin(String OrderNum)
        {
            try
            {
                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");

                string ShopClientID = my_Model_tab_Order.ShopClient_ID.ToString();
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(my_Model_tab_Order.ShopClient_ID));
                int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回
                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(intUserIDShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "微店" + " 用户即将付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(strGet_A_GoodIDList[0]));

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                string strDescription = "亲，我们给你发信，是因为" + Pub.GetNickName(my_Model_tab_Order.UserID.ToString()) + "用户即将付款通知 " + GoodP.GetGoodType(Int32.Parse(strGet_A_GoodIDList[1])) + myModel_tab_Goods.Name + " 引起！请登录电脑后台，查看待付款消息" + "\n";
                strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                strDescription += "微店订单名称：" + my_Model_tab_Order.OrderName + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";

                string strErJiYuMing = my_Model_tab_ShopClient.ErJiYuMing;
                string strURL = "";
                if (strGet_A_GoodIDList[1] == "0")
                {
                    strURL = "https://" + strErJiYuMing + "/product-" + myModel_tab_Goods.ID + ".aspx";
                }
                else if (strGet_A_GoodIDList[1] == "2")
                {
                    strURL = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGet_A_GoodIDList[2] + "&tuangouidnumber=" + strGet_A_GoodIDList[3];
                }
                else if (strGet_A_GoodIDList[1] == "6")///运营中心的
                {
                    strURL = "https://" + strErJiYuMing + "/op-" + strGet_A_GoodIDList[2] + "-" + strGet_A_GoodIDList[3] + ".aspx";
                }
                //string strURL = Eggsoft.Common.Application.AppUrl + "/Goods-" + myModel_tab_Goods.ID + ".aspx";

                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(my_Model_tab_ShopClient.ID, "WinXinWillPayMoney"))
                {
                    string[] stringWeiXinRalationUserIDList = Pub.GetstringWeiXinRalationUserIDList(my_Model_tab_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(my_Model_tab_Order.UserID), WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), WeiXinTuWens_ArrayList);
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
        /// <summary>
        /// 商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
        /// </summary>
        /// <param name="intGoodType"></param>
        /// <returns></returns>
        public static String GetGoodType(int intGoodType)
        {
            ///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
            string strReturnGetGoodType = "";
            switch (intGoodType)
            {
                case 0:
                    strReturnGetGoodType = "";
                    break;
                case 1:
                    strReturnGetGoodType = "微砍价";
                    break;
                case 2:
                    strReturnGetGoodType = "微团购";
                    break;
                case 3:
                    strReturnGetGoodType = "微众筹";
                    break;
                case 6:
                    strReturnGetGoodType = "运营中心";
                    break;
                default:
                    strReturnGetGoodType = ""; ;
                    break;
            }
            return strReturnGetGoodType;
        }

        public static void tellShopClientID_O2O_UserWillPayMoney_ByWeiXin(String OrderNum)
        {
            try
            {
                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");
                int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回
                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(intUserIDShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息
                string ShopClientID = my_Model_tab_Order.ShopClient_ID.ToString();
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(my_Model_tab_Order.ShopClient_ID));
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + ShopClientID);
                if (boolExsit == false) return;


                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "o2o微店" + " 用户即将付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();

                string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(strGet_A_GoodIDList[0]));

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(Convert.ToInt32(my_Model_tab_Order.UserID), out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                string strDescription = "亲，我们给你发信，是因为" + Pub.GetNickName(my_Model_tab_Order.UserID.ToString()) + "用户即将付款通知 " + GoodP.GetGoodType(Int32.Parse(strGet_A_GoodIDList[1])) + myModel_tab_Goods.Name + "引起！请登录电脑后台，查看待付款消息" + "\n";
                strTitle += "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里" + "\n";
                strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                strDescription += "微店订单名称：" + my_Model_tab_Order.OrderName + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "\n";
                string strErJiYuMing = my_Model_tab_ShopClient.ErJiYuMing;

                string strURL = "";
                if (strGet_A_GoodIDList[1] == "0")
                {
                    strURL = "https://" + strErJiYuMing + "/product-" + myModel_tab_Goods.ID + ".aspx";
                }
                else if (strGet_A_GoodIDList[1] == "2")
                {
                    strURL = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGet_A_GoodIDList[2] + "&tuangouidnumber=" + strGet_A_GoodIDList[3];
                }
                else if (strGet_A_GoodIDList[1] == "6")///运营中心的
                {
                    strURL = "https://" + strErJiYuMing + "/op-" + strGet_A_GoodIDList[2] + "-" + strGet_A_GoodIDList[3] + ".aspx";
                }

                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(my_Model_tab_ShopClient.ID, "o2oWillPayMoney"))
                {
                    EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);
                    string[] stringWeiXinRalationUserIDList = Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strSenduserID = stringWeiXinRalationUserIDList[k];
                        if (Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strSenduserID) == Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(my_Model_tab_Order.UserID.ToString()))
                        {
                            string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(strSenduserID), Convert.ToInt32(my_Model_tab_Order.UserID), WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {
                                if (boolTempletVisitMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(strSenduserID), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), WeiXinTuWens_ArrayList);
                                }
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


        public static void tellShopClientID_o2o_UserPayMoney_ByWeiXin(String OrderNum)
        {
            try
            {


                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");

                string ShopClientID = my_Model_tab_Order.ShopClient_ID.ToString();
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(my_Model_tab_Order.ShopClient_ID));

                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + ShopClientID);
                if (boolExsit == false) return;


                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "o2o微店" + " 用户付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(strGet_A_GoodIDList[0]));

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(Convert.ToInt32(my_Model_tab_Order.UserID), out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                string strDescription = "亲，我们给你发信，是因为用户" + Pub.GetNickName(my_Model_tab_Order.UserID.ToString()) + "付款通知 " + GoodP.GetGoodType(Int32.Parse(strGet_A_GoodIDList[0])) + myModel_tab_Goods.Name + " 引起！" + "\n";
                strTitle += "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里" + "\n";
                strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                strDescription += "微店订单名称：" + my_Model_tab_Order.OrderName + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "\n";
                //strDescription += "请登陆PC端商户后台进行发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";
                string strClickURL = "";
                if (strGet_A_GoodIDList[1] == "0")
                {
                    strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/product-" + myModel_tab_Goods.ID + ".aspx";
                }
                else if (strGet_A_GoodIDList[1] == "2")
                {
                    strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGet_A_GoodIDList[2] + "&tuangouidnumber=" + strGet_A_GoodIDList[3];
                }
                else if (strGet_A_GoodIDList[1] == "6")///运营中心的
                {
                    strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/op-" + strGet_A_GoodIDList[2] + "-" + strGet_A_GoodIDList[3] + ".aspx";
                }
                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), "WinXinPayedMoney"))
                {
                    EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(my_Model_tab_Order.UserID), WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strClickURL, "用户" + Pub.GetNickName(my_Model_tab_Order.UserID.ToString()) + "付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e);
            }
            finally
            {

            }
        }

        public static void tellShopClientID_UserPayMoney_ByWeiXin(String OrderNum)
        {
            try
            {
                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");

                string ShopClientID = my_Model_tab_Order.ShopClient_ID.ToString();
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(my_Model_tab_Order.ShopClient_ID));

                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "微店" + " 用户付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(strGet_A_GoodIDList[0]));

                int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);


                string strDescription = "亲，我们给你发信，是因为用户" + Pub.GetNickName(my_Model_tab_Order.UserID.ToString()) + "付款通知 " + GoodP.GetGoodType(Int32.Parse(strGet_A_GoodIDList[0])) + myModel_tab_Goods.Name + " 引起！" + "\n";
                strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                strDescription += "微店订单名称：" + my_Model_tab_Order.OrderName + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "\n";
                strDescription += "请登陆PC端商户后台进行发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";

                string strClickURL = "";
                if (strGet_A_GoodIDList[1] == "0")
                {
                    strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/product-" + myModel_tab_Goods.ID + ".aspx";
                }
                else if (strGet_A_GoodIDList[1] == "2")
                {
                    strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGet_A_GoodIDList[2] + "&tuangouidnumber=" + strGet_A_GoodIDList[3];
                }
                else if (strGet_A_GoodIDList[1] == "6")///运营中心的
                {
                    strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/op-" + strGet_A_GoodIDList[2] + "-" + strGet_A_GoodIDList[3] + ".aspx";
                }
                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), "WinXinPayedMoney"))
                {
                    string[] stringWeiXinRalationUserIDList = Pub.GetstringWeiXinRalationUserIDList(my_Model_tab_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(my_Model_tab_Order.UserID), WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)

                        {
                            if (boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strClickURL, "用户" + Pub.GetNickName(my_Model_tab_Order.UserID.ToString()) + "付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e);
            }
            finally
            {

            }
        }

        public static void tellShopClientID_UserWillPayMoney_ByEmail1(String OrderNum)
        {
            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
            my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");

            //my_Model_tab_Order.OrderName;
            //my_Model_tab_Order.OrderNum;       
            //my_Model_tab_Order.TotalMoney;
            string ShopClientID = my_Model_tab_Order.ShopClient_ID.ToString();
            bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(my_Model_tab_Order.ShopClient_ID));
            if (String.IsNullOrEmpty(my_Model_tab_ShopClient.XML) == false)
            {
                XML__Class_Shop_Client myXML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(my_Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);

                if (myXML__Class_Shop_Client.CheckEmail == true)
                {
                    string strTo = myXML__Class_Shop_Client.Email;
                    string strSubject = tab_System_And_.getTab_System("CityName") + "微店" + " 用户" + Pub.GetNickName(my_Model_tab_Order.UserID.ToString()) + "即将付款通知！";
                    string strBody = "亲，我们给你发信，是因为" + tab_System_And_.getTab_System("CityName") + "微店" + "用户即将付款通知引起！" + "\n";
                    strBody += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                    strBody += "微店订单名称：" + my_Model_tab_Order.OrderName + "\n";
                    strBody += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "\n";

                    strBody += "请点击如下的连接进行消息查看。如果不能点击，请复制如下连接到浏览器地址栏！查看待付款消息" + "\n";


                    string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];

                    strBody += strClientAdminURL + "/ClientAdmin/default.aspx";
                    Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName, strTo, strSubject, strBody);

                }
            }
        }




        public static void tellShopClientID_UserPayMoney_ByEmail(String OrderNum)
        {
            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
            my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");

            string ShopClientID = my_Model_tab_Order.ShopClient_ID.ToString();
            bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
            EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(my_Model_tab_Order.ShopClient_ID));
            if (String.IsNullOrEmpty(my_Model_tab_ShopClient.XML) == false)
            {
                XML__Class_Shop_Client myXML__Class_Shop_Client = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_Client>(my_Model_tab_ShopClient.XML, System.Text.Encoding.UTF8);

                if (myXML__Class_Shop_Client.CheckEmail == true)
                {
                    string strTo = myXML__Class_Shop_Client.Email;
                    string strSubject = tab_System_And_.getTab_System("CityName") + "微店" + " 用户付款通知！";
                    string strBody = "你好，我们给你发信，是因为" + tab_System_And_.getTab_System("CityName") + "微店" + "用户付款通知引起！" + "\n";
                    strBody += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                    strBody += "微店订单名称：" + my_Model_tab_Order.OrderName + "\n";
                    strBody += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "\n";
                    string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];
                    strBody += "请点击如下的连接进行发货处理。如果不能点击，请复制如下连接到浏览器地址栏！" + "\n";
                    strBody += strClientAdminURL + "/ClientAdmin/19tab_Order/tab_Order_Board_WaitGiveGoods.aspx";
                    Eggsoft_Public_CL.Pub.SendEmail_AddTask(my_Model_tab_ShopClient.ShopClientName + "微店", strTo, strSubject, strBody);
                }
            }
        }


        public static void tellShopClientID_o2o_UserPayMoney_ByEmail(String OrderNum)
        {
            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
            my_Model_tab_Order = my_BLL_tab_Order.GetModel("OrderNum='" + OrderNum + "'");

            string strUserID = my_Model_tab_Order.UserID.ToString();
            string ShopClientID = my_Model_tab_Order.ShopClient_ID.ToString();
            bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
            int intShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID);
            if (intShopClientID.ToString() != ShopClientID) return;
            EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
            bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + ShopClientID);
            if (boolExsit == false) return;

            int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
            int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
            if (intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回


            int outintShopo2oID = 0;
            double outDecimalDistance = 0;
            string strUserBaiDuAdress = "";
            Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(Convert.ToInt32(my_Model_tab_Order.UserID), out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

            EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);

            if (String.IsNullOrEmpty(Model_tab_ShopClient_O2O_ShopInfo.XML) == false)
            {
                XML__Class_Shop_O2o myXML__Class_Shop_O2o = Eggsoft.Common.XmlHelper.XmlDeserialize<XML__Class_Shop_O2o>(Model_tab_ShopClient_O2O_ShopInfo.XML, System.Text.Encoding.UTF8);

                if (myXML__Class_Shop_O2o.CheckEmail == true)
                {
                    string strTo = myXML__Class_Shop_O2o.Email;
                    string strSubject = tab_System_And_.getTab_System("CityName") + "微店" + " 用户付款通知！";
                    string strBody = "你好，我们给你发信，是因为" + tab_System_And_.getTab_System("CityName") + "微店" + "用户付款通知引起！" + "\n";
                    strBody += "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里" + "\n";
                    strBody += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                    strBody += "微店订单名称：" + my_Model_tab_Order.OrderName + "\n";
                    strBody += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "\n";

                    Eggsoft_Public_CL.Pub.SendEmail_AddTask(Model_tab_ShopClient_O2O_ShopInfo.ShopName + "o2o微店", strTo, strSubject, strBody);

                }
            }
        }

        public static string Get_NickName_From_CurUser_ID(int CurUser_ID, int CurGood_ID)
        {

            string strView_SalesGoods = "";
            strView_SalesGoods += " SELECT   tab_Orderdetails.OrderCount, tab_Order.ID AS OrderID, tab_Order.PayStatus, tab_Orderdetails.GoodID, ";
            strView_SalesGoods += "     tab_Order.UserID, tab_User.NickName, tab_Order.OrderNum, tab_Goods.PromotePrice, ";
            strView_SalesGoods += "     tab_Orderdetails.GoodPrice, tab_Goods.Name AS GoodName, tab_Orderdetails.ID AS ID_Orderdetails, ";
            strView_SalesGoods += "     tab_Orderdetails.Pinglun, tab_Orderdetails.CreatDateTime, tab_Order.PayDateTime, ";
            strView_SalesGoods += "     tab_Orderdetails.ParentID, tab_Orderdetails.Over7DaysToBeans, tab_Goods.IsDeleted, ";
            strView_SalesGoods += "     tab_Order.TotalMoney, tab_Goods.IS_Admin_check, ";
            strView_SalesGoods += "     tab_ShopClient_Agent__ProductClassID.Empowered AS ParentID_Empowered, ";
            strView_SalesGoods += "     tab_ShopClient_Agent_.Empowered AS GrandParentID_Empowered, tab_Order.DeliveryText, ";
            strView_SalesGoods += "     tab_Order.isReceipt, tab_Orderdetails.GrandParentID, tab_Orderdetails.GreatParentID, ";
            strView_SalesGoods += "     tab_Order.UpdateDateTime, tab_Order.ShopClient_ID, tab_Goods.Send_Money_IfBuy, ";
            strView_SalesGoods += "     tab_Goods.Send_Vouchers_IfBuy, tab_Orderdetails.GoodType, tab_Orderdetails.GoodTypeId";
            strView_SalesGoods += " FROM      tab_ShopClient_Agent_ with(nolock) RIGHT OUTER JOIN ";
            strView_SalesGoods += "  tab_ShopClient_Agent__ProductClassID with(nolock) ON ";
            strView_SalesGoods += "   tab_ShopClient_Agent_.UserID = tab_ShopClient_Agent__ProductClassID.UserID RIGHT OUTER JOIN ";
            strView_SalesGoods += "  tab_Order with(nolock) INNER JOIN ";
            strView_SalesGoods += "  tab_Orderdetails ON tab_Order.ID = tab_Orderdetails.OrderID INNER JOIN ";
            strView_SalesGoods += "  tab_Goods with(nolock) ON tab_Orderdetails.GoodID = tab_Goods.ID INNER JOIN ";
            strView_SalesGoods += " tab_User with(nolock) ON tab_Order.UserID = tab_User.ID ON ";
            strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.UserID = tab_Order.UserID AND ";
            strView_SalesGoods += " tab_ShopClient_Agent__ProductClassID.ProductID = tab_Orderdetails.GoodID ";
            string strwhere = "tab_ShopClient_Agent_.UserID=" + CurUser_ID + " and PayStatus=1 and tab_Orderdetails.GoodID=" + CurGood_ID;

            strView_SalesGoods += " WHERE " + strwhere + " and (tab_Order.PayStatus = 1) ";///AND (tab_Goods.IsDeleted = 0)



            string strNickName = "微店";
            EggsoftWX.BLL.View_SalesGoods bllView_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();
            System.Data.DataTable Data_DataTable = bllView_SalesGoods.SelectList(strView_SalesGoods).Tables[0];
            if (Data_DataTable.Rows.Count > 0)
            {
                strNickName = Data_DataTable.Rows[0]["NickName"].ToString();

            }
            return strNickName.Replace("'", "");
        }

        public static string Get_GoodNameFromGoodID(int intGood_ID)
        {
            string GoodName = "";

            try
            {
                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();

                GoodName = BLL_tab_Goods.GetList("Name", "id=" + intGood_ID).Tables[0].Rows[0][0].ToString();


            }
            catch
            { }
            finally
            {
                // GoodName = "";
            }

            return GoodName;
        }

        public static string Get_UnitNameFromGoodID(int intGood_ID)
        {
            string UnitidName = "个";

            try
            {
                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();

                string Unitid = BLL_tab_Goods.GetList("Unit", "id=" + intGood_ID).Tables[0].Rows[0][0].ToString();

                EggsoftWX.BLL.tab_Goods_Unit bll_tab_Goods_Unit = new EggsoftWX.BLL.tab_Goods_Unit();
                UnitidName = bll_tab_Goods_Unit.GetList("Unit", "id=" + Unitid).Tables[0].Rows[0][0].ToString();
            }
            catch
            { }
            finally
            {
                UnitidName = "个";
            }

            return UnitidName;
        }

        public static string GetParent_ID_From_CurUser_ID(int CurUser_ID, int CurGood_ID)
        {
            string strParentIDList = CurUser_ID.ToString();
            EggsoftWX.BLL.View_SalesGoods bll = new EggsoftWX.BLL.View_SalesGoods();

            string strwhere = "UserID=" + CurUser_ID + " and PayStatus=1 and GoodID=" + CurGood_ID;
            if (bll.Exists(strwhere))
            {
                System.Data.DataTable Data_DataTable = bll.GetList("ParentID", strwhere).Tables[0];

                for (int i = 0; i < Data_DataTable.Rows.Count; i++)
                {
                    string strParentID = Data_DataTable.Rows[i]["ParentID"].ToString();
                    if (string.IsNullOrEmpty(strParentID) == false)
                    {
                        strParentIDList = strParentIDList + "," + GetParent_ID_From_CurUser_ID(Int32.Parse(strParentID), CurGood_ID);
                    }
                }
            }
            else
            {
                if (strParentIDList.IndexOf("," + CurUser_ID.ToString()) == -1)
                {
                    strParentIDList = strParentIDList + "," + CurUser_ID.ToString();
                }
            }
            return strParentIDList;
        }


        public static Int64 Get_KuCunCount_From_Good_ID(int Good_ID)
        {
            int Myint = 0;
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();

            if (bll.Exists("ID=" + Good_ID))
            {
                string strKuCunCount = bll.GetList("KuCunCount", "ID=" + Good_ID).Tables[0].Rows[0]["KuCunCount"].ToString();

                Myint = Convert.ToInt32(strKuCunCount);
            }
            return Myint;
        }



        public static int GetShopGood_Class_ID_From_Good_ID(int Good_ID)
        {
            int Myint = 0;
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();

            if (bll.Exists("ID=" + Good_ID))
            {
                Myint = Convert.ToInt32(bll.GetList("Good_Class", "ID=" + Good_ID).Tables[0].Rows[0]["Good_Class"].ToString());
            }
            return Myint;
        }


        public static string GetOrderNum_From_OrderID(int OrderID)
        {
            string strOrderNum = "";
            EggsoftWX.BLL.tab_Order bll_tab_Order = new EggsoftWX.BLL.tab_Order();

            if (bll_tab_Order.Exists("ID=" + OrderID))
            {
                strOrderNum = (bll_tab_Order.GetList("OrderNum", "ID=" + OrderID).Tables[0].Rows[0]["OrderNum"].ToString());
            }
            return strOrderNum;
        }

        public static int GetClass2_ID_From_Good_ID(int Good_ID)
        {
            int Myint = 0;
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();

            if (bll.Exists("ID=" + Good_ID))
            {
                Myint = Convert.ToInt32(bll.GetList("Class2_ID", "ID=" + Good_ID).Tables[0].Rows[0]["Class2_ID"].ToString());
            }
            return Myint;
        }

        public static int GetShopClient_ID_From_Good_ID(int Good_ID)
        {
            int Myint = 0;
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();

            if (bll.Exists("ID=" + Good_ID))
            {
                Myint = Convert.ToInt32(bll.GetList("ShopClient_ID", "ID=" + Good_ID).Tables[0].Rows[0]["ShopClient_ID"].ToString());
            }
            return Myint;
        }
        public static int GetShopClient_ID_From_Order_ID(int Order_ID)
        {
            int Myint = 0;
            EggsoftWX.BLL.tab_Order bll = new EggsoftWX.BLL.tab_Order();

            if (bll.Exists("ID=" + Order_ID))
            {
                Myint = Convert.ToInt32(bll.GetList("ShopClient_ID", "ID=" + Order_ID).Tables[0].Rows[0]["ShopClient_ID"].ToString());
            }
            return Myint;
        }



        public static void GetShopClient_PartnerID_RecommendWeiXinID_From_ShopClient_ID(int ShopClient_ID, out int PartnerID, out int RecommendID)
        {

            PartnerID = 0;
            RecommendID = 0;

            EggsoftWX.BLL.tab_ShopClient bll_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            if (bll_tab_ShopClient.Exists("ID=" + ShopClient_ID))
            {
                System.Data.DataTable myDataTable = bll_tab_ShopClient.GetList("PartnerWeiXinID,RecommendWeiXinID", "ID=" + ShopClient_ID).Tables[0];
                string strPartnerWeiXinID = myDataTable.Rows[0]["PartnerWeiXinID"].ToString();
                string strRecommendWeiXinID = myDataTable.Rows[0]["PartnerWeiXinID"].ToString();

                int.TryParse(strPartnerWeiXinID, out PartnerID);
                int.TryParse(strRecommendWeiXinID, out RecommendID);
            }
        }

        public static void GetShopClient_PartnerID_RecommendWeiXinID_From_Good_ID(int Good_ID, out int PartnerID, out int RecommendID)
        {
            int MyShopClient_ID = 0;
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();

            if (bll.Exists("ID=" + Good_ID))
            {
                MyShopClient_ID = Convert.ToInt32(bll.GetList("ShopClient_ID", "ID=" + Good_ID).Tables[0].Rows[0]["ShopClient_ID"].ToString());
            }



            PartnerID = 0;
            RecommendID = 0;

            GetShopClient_PartnerID_RecommendWeiXinID_From_ShopClient_ID(MyShopClient_ID, out PartnerID, out RecommendID);


            //return PartnerID;
        }

        public static EggsoftWX.Model.tab_ShopClient_DistributionMoney GetShopClient_DistributionMoney_List_Agent_From_Good_ID(int Good_ID)
        {
            int MyWebuy8_DistributionMoney_Value = 0;
            EggsoftWX.BLL.tab_Goods bll = new EggsoftWX.BLL.tab_Goods();

            if (bll.Exists("ID=" + Good_ID))
            {
                MyWebuy8_DistributionMoney_Value = Convert.ToInt32(bll.GetList("Webuy8_DistributionMoney_Value", "ID=" + Good_ID).Tables[0].Rows[0]["Webuy8_DistributionMoney_Value"].ToString());
            }
            if (MyWebuy8_DistributionMoney_Value == 0) MyWebuy8_DistributionMoney_Value = 1;
            EggsoftWX.BLL.tab_ShopClient_DistributionMoney bll_tab_DistributionMoney = new EggsoftWX.BLL.tab_ShopClient_DistributionMoney();
            EggsoftWX.Model.tab_ShopClient_DistributionMoney Model_tab_DistributionMoney = new EggsoftWX.Model.tab_ShopClient_DistributionMoney();
            Model_tab_DistributionMoney = bll_tab_DistributionMoney.GetModel(MyWebuy8_DistributionMoney_Value);


            return Model_tab_DistributionMoney;
        }


        /// <summary>
        /// 取得父子关系链接
        /// </summary>
        /// <param name="Good_ID"></param>
        /// <param name="pUserd_ID"></param>
        /// <param name="AgentGet"></param>
        /// <param name="ManagerAgentGet"></param>
        /// <param name="ManagerAgentParentID"></param>
        /// <param name="ManagerGrandAgentGet"></param>
        /// <param name="ManagerGrandAgentParentID"></param>
        /// <param name="DecimalGoodPrice">微砍价需要传入该值</param>
        ///商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
       // public static Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel Get_Agent_Product_Money_Percent(int Good_ID, int pUserd_ID, out Decimal AgentGet, out Decimal ManagerAgentGet, out int ManagerAgentParentID, out Decimal ManagerGrandAgentGet, out int ManagerGrandAgentParentID, string strGoodType, string strGoodTypeId, string strGoodTypeIdBuyInfo, Decimal DecimalGoodPrice = 0)
        public static void Get_Agent_Product_Money_Percent(Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel, bool boolGetChildList = false)
        {
            myModel.AgentGet = 0;
            myModel.ManagerAgentGet = 0;
            myModel.ManagerGrandAgentGet = 0;


            //AgentGet = 0;///初始值
            //ManagerAgentGet = 0;///初始值
            //ManagerGrandAgentGet = 0;///初始值
            //Decimal[] myAgentFenXiaoList = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(pUserd_ID.ToString()),0);


            int intPub_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(myModel.GoodID);
            EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(intPub_ShopClientID, myModel.UserID, myModel.GoodID.toInt32());
            int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();
            myModel.ShopClient_ID = Model_b019_tab_ShopClient_MultiFenXiaoLevel.ShopClient_ID;
            myModel.Name = Model_b019_tab_ShopClient_MultiFenXiaoLevel.Name;
            myModel.Sort = Model_b019_tab_ShopClient_MultiFenXiaoLevel.Sort;
            myModel.FenxiaoParentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet;
            myModel.FenxiaoGrandParentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet;
            myModel.FenxiaoGreatParentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet;

            myModel.ChildGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.ChildGet;
            myModel.GrandsonGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.GrandsonGet;
            myModel.GreatsonGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.GreatsonGet;

            myModel.ChildGet_Money = Model_b019_tab_ShopClient_MultiFenXiaoLevel.ChildGet_Money;
            myModel.Grandson_Money = Model_b019_tab_ShopClient_MultiFenXiaoLevel.Grandson_Money;
            myModel.GreatsonGet_Money = Model_b019_tab_ShopClient_MultiFenXiaoLevel.GreatsonGet_Money;

            myModel.OperationGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.OperationGet;
            myModel.OperationParentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.OperationParentGet;
            myModel.OperationGrandParentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.OperationGrandParentGet;
            //,[]
            //,[]
            //,[]
            //,[]
            //,[]
            //,[]
            //,[]
            //,[]
            //,[]


            if (myModel.strGoodType == "3")////众筹订单
            {
                #region 获取 团购的代理利润
                ///检查代理资格
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolIFAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + myModel.UserID + " and IsDeleted=0 and (Empowered=1  or  OnlyIsAngel=1)");
                if (boolIFAgent)
                {
                    EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                    EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Int32.Parse(myModel.strGoodTypeIdBuyInfo));
                    Decimal DecimalAgentPrice = Decimal.Multiply(Convert.ToDecimal(Model_tab_ZC_01Product_Support.AgentPrice), (Decimal)100.0) / Convert.ToDecimal(Model_tab_ZC_01Product_Support.SalesPrice);

                    if (Model_b019_tab_ShopClient_MultiFenXiaoLevel != null)
                    {
                        if (intLength == 3)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerGrandAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet.toDecimal() * DecimalAgentPrice;
                        }
                        else if (intLength == 2)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet.toDecimal() * DecimalAgentPrice;
                        }
                        else if (intLength == 1)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal();
                        }
                    }

                }
                #endregion
            }
            else if (myModel.strGoodType == "2")////团购订单
            {
                #region 获取 团购的代理利润
                ///检查代理资格
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolIFAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + myModel.UserID + " and IsDeleted=0 and Empowered=1");
                if (boolIFAgent)
                {
                    EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                    EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(Int32.Parse(myModel.strGoodTypeId));
                    Decimal DecimalAgentPrice = Decimal.Multiply(Convert.ToDecimal(Model_tab_TuanGou.AgentPrice), (Decimal)(100.0)) / Convert.ToDecimal(Model_tab_TuanGou.EachPeoplePrice);

                    if (Model_b019_tab_ShopClient_MultiFenXiaoLevel != null)
                    {
                        if (intLength == 3)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerGrandAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet.toDecimal() * DecimalAgentPrice;
                        }
                        else if (intLength == 2)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet.toDecimal() * DecimalAgentPrice;
                        }
                        else if (intLength == 1)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * DecimalAgentPrice;
                        }
                    }

                }
                #endregion
            }
            else if (myModel.strGoodType == "1")////砍价订单
            {
                #region 获取 砍价的代理利润
                ///检查代理资格
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolIFAgent = BLL_tab_ShopClient_Agent_.Exists("UserID=" + myModel.UserID + " and IsDeleted=0 and Empowered=1");
                if (boolIFAgent)
                {
                    EggsoftWX.BLL.tab_WeiKanJia BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                    EggsoftWX.Model.tab_WeiKanJia Model_tab_WeiKanJia = BLL_tab_WeiKanJia.GetModel(Int32.Parse(myModel.strGoodTypeId));
                    Decimal DecimalAgentPrice = Convert.ToDecimal(Model_tab_WeiKanJia.AgentPrice);
                    if (myModel.DecimalGoodPrice > 0) DecimalAgentPrice = Decimal.Multiply(DecimalAgentPrice, (Decimal)(100.0)) / myModel.DecimalGoodPrice;
                    if (Model_b019_tab_ShopClient_MultiFenXiaoLevel != null)
                    {
                        if (intLength == 3)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerGrandAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet.toDecimal() * DecimalAgentPrice;
                        }
                        else if (intLength == 2)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * DecimalAgentPrice;
                            myModel.ManagerAgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet.toDecimal() * DecimalAgentPrice;
                        }
                        else if (intLength == 1)
                        {
                            myModel.AgentGet = Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet.toDecimal() * DecimalAgentPrice;
                        }
                    }

                }
                #endregion
            }
            else if (myModel.strGoodType == "0" || myModel.strGoodType == "" || myModel.strGoodType == null)////////////////////////常见的分销  就是这里了  老张 都这里找
            {
                #region 获取某一个商品的分销
                Decimal AgentPercent = 0;///普通用户和代理商的价格是不同的
                Decimal PromotePrice = 0;///普通用户和代理商的价格是不同的

                EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods Model_tab_Goods1 = BLL_tab_Goods.GetModel(myModel.GoodID);

                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=@UserID and IsDeleted=0 and Empowered=1 ", myModel.UserID);
                if ((Model_tab_ShopClient_Agent_ != null) && Model_tab_ShopClient_Agent_.AgentLevelSelect.toInt32() > 0)////是代理商 就存在代理商价格
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();
                    EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Modeltab_ShopClient_Agent_Level_ProductInfo = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("ShopClient_Agent_Level_ID=@ShopClient_Agent_Level_ID and ProductID=@ProductID and ShopClientID=@ShopClientID", Model_tab_ShopClient_Agent_.AgentLevelSelect.toInt32(), myModel.GoodID, intPub_ShopClientID);
                    AgentPercent = Modeltab_ShopClient_Agent_Level_ProductInfo.AgentPercent.toDecimal();
                    PromotePrice = Modeltab_ShopClient_Agent_Level_ProductInfo.ProductPrice.toDecimal();

                    if (AgentPercent < Model_tab_Goods1.AgentPercent)
                    {
                        myModel.OperationGet = 0;///当前代理就不给钱了  因为你的价格低  你又是自己购买
                    }
                }
                else
                {
                    AgentPercent = Model_tab_Goods1.AgentPercent.toDecimal();
                    PromotePrice = Model_tab_Goods1.PromotePrice.toDecimal();
                }
                myModel.DecimalGoodPrice = PromotePrice;

                Decimal DecimalGet_Agent_Product_Money_Percent = AgentPercent * myModel.FenxiaoParentGet.toDecimal() / PromotePrice;
                Decimal Get_ManagerAgent_Product_Money_Percent = AgentPercent * myModel.FenxiaoGrandParentGet.toDecimal() / PromotePrice;
                Decimal Get_ManagerGrandAgent_Product_Money_Percent = AgentPercent * myModel.FenxiaoGreatParentGet.toDecimal() / PromotePrice;
                myModel.AgentGet = DecimalGet_Agent_Product_Money_Percent;
                myModel.ManagerAgentGet = Get_ManagerAgent_Product_Money_Percent;
                myModel.ManagerGrandAgentGet = Get_ManagerGrandAgent_Product_Money_Percent;

                myModel.ChildGetbyGoodsPercent = AgentPercent * myModel.ChildGet.toDecimal() / PromotePrice;
                myModel.GrandsonGetbyGoodsPercent = AgentPercent * myModel.GrandsonGet.toDecimal() / PromotePrice;
                myModel.GreatsonGetbyGoodsPercent = AgentPercent * myModel.GreatsonGet.toDecimal() / PromotePrice;


                #endregion

            }

            #region  获取父系列表
            myModel.ManagerAgentParentID = 0;
            myModel.ManagerGrandAgentParentID = 0;


            //Decimal[] myAgentLevelList = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(Good_ID),0);
            if ((myModel.ManagerAgentGet > 0) && (intLength > 1))
            {
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + myModel.UserID + " and IsDeleted=0");
                if (Model_tab_ShopClient_Agent_ != null)
                {
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_Parent = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + Model_tab_ShopClient_Agent_.ParentID.toInt32() + " and IsDeleted=0");
                    if (Model_tab_ShopClient_Agent_Parent != null)
                    {
                        myModel.ManagerAgentParentID = Model_tab_ShopClient_Agent_Parent.ParentID.toInt32();
                    }
                }


                if ((myModel.ManagerGrandAgentGet > 0) && (intLength > 2))
                {
                    Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + myModel.ManagerAgentParentID);
                    if (Model_tab_ShopClient_Agent_ != null)
                    {
                        myModel.ManagerGrandAgentParentID = Model_tab_ShopClient_Agent_.ParentID.toInt32();
                    }
                }
            }
            #endregion  获取父系列表



            if (myModel.strGoodType == "2")////团购订单
            {
                #region 团购等商品 。。修改顶级代理情况
                #region 顶级代理模式
                //EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                //EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(myModel.UserID.toString()));
                //bool boolTopSales = true;
                //if (Model_tab_ShopClient_ShopPar != null)
                //{
                //    boolTopSales = Model_tab_ShopClient_ShopPar.TopAgent.toBoolean();
                //}
                #endregion



                #endregion
            }
            else if (myModel.strGoodType == "3")////众筹订单
            {
                #region 众筹等商品 。。修改顶级代理情况
                #region 顶级代理模式
                ////去除Price_Percent Price_Percent1  Price_Percent2 2017  12 23
                //EggsoftWX.BLL.tab_ShopClient_ShopPar bll_tab_ShopClient_ShopPar = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                //EggsoftWX.Model.tab_ShopClient_ShopPar Model_tab_ShopClient_ShopPar = bll_tab_ShopClient_ShopPar.GetModel("ShopClientID=" + Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(myModel.UserID.ToString()));
                //bool boolTopSales = true;
                //if (Model_tab_ShopClient_ShopPar != null)
                //{
                //    boolTopSales = Model_tab_ShopClient_ShopPar.TopAgent.toBoolean();
                //}
                #endregion



                #endregion
            }


            if (boolGetChildList)////是否需要调取子孩子列表
            {
                string strSQLSELECT = @"SELECT   tab_User.ID, tab_ShopClient_Agent_.OnlyIsAngel, tab_User.ParentID
FROM tab_User LEFT OUTER JOIN
                tab_ShopClient_Agent_ ON tab_User.ShopClientID = tab_ShopClient_Agent_.ShopClientID AND
                tab_User.ID = tab_ShopClient_Agent_.UserID
                where tab_User.parentID=@parentID
";

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                if (myModel.ChildGet > 0)
                {
                    System.Data.DataTable Data_DataTable = BLL_tab_User.SelectList(strSQLSELECT, myModel.intParentID).Tables[0];
                    myModel.ChildGetList = Data_DataTable;
                }
                if (myModel.GrandsonGet > 0 && myModel.ChildGetList != null)
                {
                    myModel.GrandsonGetList = new System.Data.DataTable();
                    for (int i = 0; i < myModel.ChildGetList.Rows.Count; i++)
                    {
                        System.Data.DataTable Data_DataTable = BLL_tab_User.SelectList(strSQLSELECT, myModel.ChildGetList.Rows[i]["ID"].toInt32()).Tables[0];
                        myModel.GrandsonGetList.Merge(Data_DataTable);
                    }
                }
                else if (myModel.GrandsonGet > 0 && myModel.ChildGetList == null)////上级是空的人怎么版  要保证给自己的下级啊
                {
                    System.Data.DataTable Data_DataTable = BLL_tab_User.SelectList(strSQLSELECT, myModel.UserID).Tables[0];
                    myModel.GrandsonGetList = Data_DataTable;
                }



                if (myModel.GreatsonGet > 0 && myModel.GrandsonGetList != null)
                {
                    myModel.GreatsonGetList = new System.Data.DataTable();
                    for (int i = 0; i < myModel.GrandsonGetList.Rows.Count; i++)
                    {
                        System.Data.DataTable Data_DataTable = BLL_tab_User.SelectList(strSQLSELECT, myModel.GrandsonGetList.Rows[i]["ID"].toInt32()).Tables[0];
                        myModel.GreatsonGetList.Merge(Data_DataTable);
                    }

                }

                if (!(myModel.ChildGet > 0))
                {
                    myModel.ChildGetList = new System.Data.DataTable();
                }
                if (!(myModel.GrandsonGet > 0))
                {
                    myModel.GrandsonGetList = new System.Data.DataTable();
                }
                if (!(myModel.GreatsonGet > 0))
                {
                    myModel.GreatsonGetList = new System.Data.DataTable();
                }

                //myModel.UserID
            }

        }

        /// <summary>
        /// 计算高级代理之间的产品差额
        /// </summary>
        /// <param name="Good_ID"></param>
        /// <param name="pUserd_ID"></param>
        /// <param name="AgentGet"></param>
        /// <param name="ManagerAgentGet"></param>
        /// <param name="ManagerAgentParentID"></param>
        /// <param name="ManagerGrandAgentGet"></param>
        /// <param name="ManagerGrandAgentParentID"></param>
        /// <param name="strGoodType"></param>
        /// <param name="strGoodTypeId"></param>
        /// <param name="strGoodTypeIdBuyInfo"></param>
        /// <param name="DecimalGoodPrice"></param>
        public static void Get_Agent_Product_Money_AdvanceAgentProduct(int Good_ID, int pParentUserd_ID, int ManagerAgentParentID, int ManagerGrandAgentParentID, out Decimal ProductPricepParentUserd_ID, out Decimal ProductPricepManagerAgentParentID, out Decimal ProductPriceManagerGrandAgentParentID)
        {
            EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_pParentUserd_ID = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pParentUserd_ID + " and Empowered=1 and AgentLevelSelect>0 and IsDeleted=0");
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ManagerAgentParentID = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + ManagerAgentParentID + " and Empowered=1 and AgentLevelSelect>0 and IsDeleted=0");
            EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ManagerGrandAgentParentID = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + ManagerGrandAgentParentID + " and Empowered=1 and AgentLevelSelect>0 and IsDeleted=0");
            EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();

            Decimal ProductPrice_pParentUserd_ID = 0;
            Decimal ProductPrice_ManagerAgentParentID = 0;
            Decimal ProductPrice_ManagerGrandAgentParentID = 0;
            if (Model_tab_ShopClient_Agent_pParentUserd_ID != null)
            {
                EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_pParentUserd_ID = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("shopClient_Agent_Level_ID=" + Model_tab_ShopClient_Agent_pParentUserd_ID.AgentLevelSelect + " and ProductID=" + Good_ID);
                if (Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_pParentUserd_ID != null)
                {
                    ProductPrice_pParentUserd_ID = Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_pParentUserd_ID.ProductPrice.toDecimal();
                }
            }
            if (Model_tab_ShopClient_Agent_ManagerAgentParentID != null)
            {
                EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_ManagerAgentParentID = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("shopClient_Agent_Level_ID=" + Model_tab_ShopClient_Agent_ManagerAgentParentID.AgentLevelSelect + " and ProductID=" + Good_ID);
                if (Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_ManagerAgentParentID != null)
                {
                    ProductPrice_ManagerAgentParentID = Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_ManagerAgentParentID.ProductPrice.toDecimal();
                }
            }
            if (Model_tab_ShopClient_Agent_ManagerGrandAgentParentID != null)
            {
                EggsoftWX.Model.tab_ShopClient_Agent_Level_ProductInfo Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_ManagerGrandAgentParentID = BLL_tab_ShopClient_Agent_Level_ProductInfo.GetModel("shopClient_Agent_Level_ID=" + Model_tab_ShopClient_Agent_ManagerGrandAgentParentID.AgentLevelSelect + " and ProductID=" + Good_ID);
                if (Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_ManagerGrandAgentParentID != null)
                {
                    ProductPrice_ManagerGrandAgentParentID = Model_tab_ShopClient_Agent_Level_ProductInfo_ProductPrice_ManagerGrandAgentParentID.ProductPrice.toDecimal();
                }
            }
            ProductPricepParentUserd_ID = ProductPrice_pParentUserd_ID;
            ProductPricepManagerAgentParentID = ProductPrice_ManagerAgentParentID;
            ProductPriceManagerGrandAgentParentID = ProductPrice_ManagerGrandAgentParentID;
        }

        public static int Get_UserID_From_Order_ID(int Order_ID)
        {
            int Myint = 0;
            EggsoftWX.BLL.tab_Order bll = new EggsoftWX.BLL.tab_Order();

            if (bll.Exists("ID=" + Order_ID + " and isdeleted<>1"))
            {
                Myint = Convert.ToInt32(bll.GetList("UserID", "id=" + Order_ID + " and isdeleted<>1").Tables[0].Rows[0]["UserID"].ToString());
            }
            return Myint;
        }

        /// <summary>
        /// strMyintGoodID, strMyintGoodType, strMyintGoodTypeId, strMyintGoodTypeIdBuyInfo 
        /// </summary>
        /// <param name="Order_ID"></param>
        /// <returns></returns>
        public static string[] Get_A_GoodID_From_Order_ID(int Order_ID)
        {

            string strMyintGoodID = "0"; string strMyintGoodType = "0"; string strMyintGoodTypeId = "0"; string strMyintGoodTypeIdBuyInfo = "0";
            EggsoftWX.BLL.tab_Orderdetails bll_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            if (bll_Orderdetails.Exists("OrderID=" + Order_ID))
            {
                System.Data.DataTable Data_DataTable = new System.Data.DataTable();
                Data_DataTable = bll_Orderdetails.GetList("GoodID,GoodType,GoodTypeId,GoodTypeIdBuyInfo", "OrderID=" + Order_ID + " and isdeleted<>1").Tables[0];
                strMyintGoodID = (Data_DataTable.Rows[0]["GoodID"].ToString());
                strMyintGoodType = (Data_DataTable.Rows[0]["GoodType"].ToString());///商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                strMyintGoodTypeId = (Data_DataTable.Rows[0]["GoodTypeId"].ToString());///主键运营中心ID  
                strMyintGoodTypeIdBuyInfo = (Data_DataTable.Rows[0]["GoodTypeIdBuyInfo"].ToString());///商品运营中心ID
            }
            string[] strGet_A_GoodID_From_Order_ID = { strMyintGoodID, strMyintGoodType, strMyintGoodTypeId, strMyintGoodTypeIdBuyInfo };
            //                                             0                       1              2                 3
            return strGet_A_GoodID_From_Order_ID;
        }



        public static string GetGood_Num_ID_From_Good_ID(int Good_ID)
        {
            string Myint = "";
            Myint = Eggsoft.Common.StringNum.Add000000Num(GetShopClient_ID_From_Good_ID(Good_ID), 5) + Eggsoft.Common.StringNum.Add000000Num(Good_ID, 5);
            return Myint;
        }


        public static int getPreID(int Good_ID)
        {
            int Myint = Good_ID;

            int ShopClient_ID = GetShopClient_ID_From_Good_ID(Good_ID);

            ArrayList List = new ArrayList();

            System.Data.DataTable myDataTable = new EggsoftWX.BLL.tab_Goods().GetList("id", "IsDeleted=0 and ShopClient_ID=" + ShopClient_ID).Tables[0];

            int i = 0;
            for (i = 0; i < myDataTable.Rows.Count; i++)
            {
                if (myDataTable.Rows[i]["id"].ToString() == Good_ID.ToString()) break;

            }

            if (i == 0)
            {
                Myint = Good_ID;
            }
            else
            {
                Myint = Convert.ToInt32(myDataTable.Rows[i - 1]["id"].ToString());
            }

            //GoodP.getPreID()
            //GoodP.NextPreID()
            return Myint;
        }

        public static int NextPreID(int Good_ID)
        {
            int Myint = Good_ID;

            int ShopClient_ID = GetShopClient_ID_From_Good_ID(Good_ID);

            ArrayList List = new ArrayList();

            System.Data.DataTable myDataTable = new EggsoftWX.BLL.tab_Goods().GetList("id", "IsDeleted=0 and ShopClient_ID=" + ShopClient_ID).Tables[0];

            int i = 0;
            for (i = 0; i < myDataTable.Rows.Count; i++)
            {
                if (myDataTable.Rows[i]["id"].ToString() == Good_ID.ToString()) break;

            }

            if (i == myDataTable.Rows.Count - 1)
            {
                Myint = Good_ID;
            }
            else
            {
                Myint = Convert.ToInt32(myDataTable.Rows[i + 1]["id"].ToString());
            }

            //GoodP.getPreID()
            //GoodP.NextPreID()
            return Myint;
        }



        /// <summary>
        /// 模拟Get提交数据
        /// </summary>
        /// <param name="url">Get地址</param> 
        /// <returns></returns>
        public static string CreateGetHttpResponse(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=gb2312";
            var response = (HttpWebResponse)request.GetResponse();
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("gb2312"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        public static int ByShareAskCount(int intArgGoodID)
        {


            int ByShareAskCount = 0;

            try
            {
                EggsoftWX.BLL.tab_User_Good_History my_BLL_tab_User_Good_History = new EggsoftWX.BLL.tab_User_Good_History();

                System.Data.DataTable DataTable_tab_User_Good_History = my_BLL_tab_User_Good_History.SelectList("SELECT  SUM(Count_Visit) AS Sum_Visit FROM  tab_User_Good_History WHERE Parent_UserID>0 and  GoodID=" + intArgGoodID + "").Tables[0];

                string strHitCount = DataTable_tab_User_Good_History.Rows[0][0].ToString();
                int.TryParse(strHitCount, out ByShareAskCount);


            }
            catch { }
            finally { }

            return ByShareAskCount;
        }


        public static int BySalesCount(int intArgGoodID)
        {
            //int pShopClient_ID = GetShopClient_ID_From_Good_ID(intArgGoodID);

            int intAdd = 0;
            int SalesCount = 0;

            EggsoftWX.BLL.View_ShenMaShopIng BLL_View_ShenMaShopIng = new EggsoftWX.BLL.View_ShenMaShopIng();//先检查 耍单现象  小于0.99的是刷单
            System.Data.DataTable my_BLL_View_ShenMaShopIng_DataTable = BLL_View_ShenMaShopIng.GetList("SalesCount", "GoodID=" + intArgGoodID).Tables[0];
            if (my_BLL_View_ShenMaShopIng_DataTable.Rows.Count > 0)
            {

                if (String.IsNullOrEmpty(my_BLL_View_ShenMaShopIng_DataTable.Rows[0]["SalesCount"].ToString()) == false)
                {
                    SalesCount = Int32.Parse(my_BLL_View_ShenMaShopIng_DataTable.Rows[0]["SalesCount"].ToString());
                }
                else
                {
                    SalesCount = 0;
                }
            }
            else
            {

                EggsoftWX.BLL.View_SalesGoods BLL_View_SalesGoods = new EggsoftWX.BLL.View_SalesGoods();
                System.Data.DataTable myDataTable = BLL_View_SalesGoods.SelectList("select sum([OrderCount]) as Count from View_SalesGoods where GoodID=" + intArgGoodID + " and PayStatus=1").Tables[0];
                if (myDataTable.Rows.Count > 0)
                {

                    if (String.IsNullOrEmpty(myDataTable.Rows[0]["Count"].ToString()) == false)
                    {
                        SalesCount = Int32.Parse(myDataTable.Rows[0]["Count"].ToString());
                    }
                    else
                    {
                        SalesCount = 0;
                    }
                }
                else
                {
                    SalesCount = intAdd;
                }
            }
            return SalesCount;
        }

        public static int ByHitCount(int intArgGoodID)
        {
            EggsoftWX.BLL.tab_User_Good_History my_BLL_tab_User_Good_History = new EggsoftWX.BLL.tab_User_Good_History();
            System.Data.DataTable DataTable_tab_User_Good_History = my_BLL_tab_User_Good_History.SelectList("SELECT  SUM(Count_Visit) AS Sum_Visit FROM  tab_User_Good_History WHERE   GoodID=" + intArgGoodID + "").Tables[0];
            int intAdd = 0;
            string strHitCount = DataTable_tab_User_Good_History.Rows[0][0].ToString();
            int.TryParse(strHitCount, out intAdd);
            return intAdd;
        }

        public static bool GetShopClientO2OAccptPowerList(int intShopClient, int outintShopo2oID, string strPowerName)
        {
            //<asp:ListItem Selected="True">接收用户付款通知(EMail)</asp:ListItem>
            //              <asp:ListItem Selected="True">接收用户浏览商品通知(微信)</asp:ListItem>
            //              <asp:ListItem Selected="True">接收用户即将付款通知(微信)</asp:ListItem>
            //              <asp:ListItem Selected="True">接收用户付款通知(微信)</asp:ListItem>
            //              <asp:ListItem Selected="True">接收用户在线消息(微信)</asp:ListItem>
            //EmailPayedMoney WinXinLookGoods WinXinWillPayMoney WinXinPayedMoney  WinXinTalkwithYou
            bool boolReturn = false;
            try
            {
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("id=" + intShopClient + "");

                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);
                String[] stringAcceptMSGList = Pub.GetstringAcceptMSGList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                if (stringAcceptMSGList.Length > 4)
                {
                    if (strPowerName == "EmailPayedMoney")
                    {
                        if (stringAcceptMSGList[0] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "WinXinLookGoods")
                    {
                        if (stringAcceptMSGList[1] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "WinXinWillPayMoney")
                    {
                        if (stringAcceptMSGList[2] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "WinXinPayedMoney")
                    {
                        if (stringAcceptMSGList[3] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "WinXinTalkwithYou")
                    {
                        if (stringAcceptMSGList[4] == "1") boolReturn = true;
                    }
                }
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }
            finally { }

            return boolReturn;
        }

        public static bool GetShopClientAccptPowerList(int intShopClient, string strPowerName)
        {
            //<asp:ListItem Selected="True">接收用户付款通知(EMail)</asp:ListItem>
            //              <asp:ListItem Selected="True">接收用户浏览商品通知(微信)</asp:ListItem>
            //              <asp:ListItem Selected="True">接收用户即将付款通知(微信)</asp:ListItem>
            //              <asp:ListItem Selected="True">接收用户付款通知(微信)</asp:ListItem>
            //              <asp:ListItem Selected="True">接收用户在线消息(微信)</asp:ListItem>
            //EmailPayedMoney WinXinLookGoods WinXinWillPayMoney WinXinPayedMoney  WinXinTalkwithYou
            bool boolReturn = false;
            try
            {


                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel("id=" + intShopClient + "");

                String[] stringAcceptMSGList = Pub.GetstringAcceptMSGList(Model_tab_ShopClient.XML);
                if (stringAcceptMSGList.Length > 4)
                {
                    if (strPowerName == "EmailPayedMoney")
                    {
                        if (stringAcceptMSGList[0] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "WinXinLookGoods")
                    {
                        if (stringAcceptMSGList[1] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "WinXinWillPayMoney")
                    {
                        if (stringAcceptMSGList[2] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "WinXinPayedMoney")
                    {
                        if (stringAcceptMSGList[3] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "o2oLookGoods")
                    {
                        if (stringAcceptMSGList[4] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "o2oWillPayMoney")
                    {
                        if (stringAcceptMSGList[5] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "o2oPayedMoney")
                    {
                        if (stringAcceptMSGList[6] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "GameLook")
                    {
                        if (stringAcceptMSGList[7] == "1") boolReturn = true;
                    }
                    else if (strPowerName == "WeiKanJiaLook")
                    {
                        if (stringAcceptMSGList[8] == "1") boolReturn = true;
                    }
                }
            }
            catch (Exception ee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ee);
            }
            finally { }

            return boolReturn;
        }

        //DisPlay("Shopping_Vouchers")  //购物券管理  //商家发起购物券	可绑定3个微信号

        public static bool DisPlay_Shopping_Vouchers(int intShopClient)
        {
            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intShopClient);

            String[] stringPowerList = Eggsoft_Public_CL.Pub.GetstringPowerList(Model_tab_ShopClient.XML);

            bool boolReturn = false;
            if (stringPowerList.Length > 0)
            {

                if (stringPowerList[2] == "1")
                {
                    boolReturn = true;
                }
                else
                {
                    boolReturn = false;
                }

            }
            else
            {
                boolReturn = false;
            }
            return boolReturn;

        }


        public static Decimal GetThisOrderMoneyNotIncludeYunFei(int intOrderID)///很简单  支付总金额 减去运费的钱 是退款的钱
        {
            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order Model_tab_Order = new EggsoftWX.Model.tab_Order();
            Model_tab_Order = BLL_tab_Order.GetModel(intOrderID);


            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


            EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            System.Data.DataTable myDataTable = BLL_tab_Orderdetails.GetList("OrderID=" + intOrderID).Tables[0];
            int i = 0;
            Decimal AllDecimal_User = Convert.ToDecimal(Model_tab_Order.TotalMoney);
            for (i = 0; i < myDataTable.Rows.Count; i++)
            {
                string strGoodID = myDataTable.Rows[i]["Goodid"].ToString();
                string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();

                Model_tab_Goods = BLL_tab_Goods.GetModel(Int32.Parse(strGoodID));
                int intFreightTemplate_ID = Convert.ToInt32(Model_tab_Goods.FreightTemplate_ID);
                if ((intFreightTemplate_ID != 0))
                {
                    EggsoftWX.BLL.tab_FreightTemplate BLL_tab_FreightTemplate = new EggsoftWX.BLL.tab_FreightTemplate();
                    EggsoftWX.Model.tab_FreightTemplate Model_tab_FreightTemplate = BLL_tab_FreightTemplate.GetModel(intFreightTemplate_ID);
                    Decimal Decimal_My_Freight = 0;
                    if (Model_tab_FreightTemplate != null)
                    {
                        Decimal_My_Freight = Model_tab_FreightTemplate.Freight + Model_tab_FreightTemplate.FreightMore * (Int32.Parse(strOrderCount) - 1);
                    }
                    AllDecimal_User -= Decimal_My_Freight;
                }
            }
            //outDecimal_ShopGet = AllDecimal_ShopGet;
            return AllDecimal_User;
        }

        public static Decimal GetThisOrderShopGet(int intOrderID)
        {
            EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            System.Data.DataTable myDataTable = BLL_tab_Orderdetails.GetList("OrderID=" + intOrderID).Tables[0];
            int i = 0;
            Decimal AllDecimal_ShopGet = 0;
            for (i = 0; i < myDataTable.Rows.Count; i++)
            {
                string strOrderDetailID = myDataTable.Rows[i]["id"].ToString();
                Decimal Decimal_ShopGet = 0;
                GetThisOrderShopGet_showFenXiaoList(strOrderDetailID, out Decimal_ShopGet);
                AllDecimal_ShopGet += Decimal_ShopGet;
            }
            //outDecimal_ShopGet = AllDecimal_ShopGet;
            return AllDecimal_ShopGet;
        }
        public static string GetThisOrderShopGet_showFenXiaoList(string strOrderDetailID, out Decimal Decimal_ShopGet)
        {


            EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
            EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = new EggsoftWX.Model.tab_Orderdetails();

            Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel(Int32.Parse(strOrderDetailID));

            Decimal DecimalAll = Convert.ToDecimal(Model_tab_Orderdetails.GoodPrice) * Convert.ToInt32(Model_tab_Orderdetails.OrderCount);//总金额

            int intParentID = 0;
            string strParentID = Model_tab_Orderdetails.ParentID.ToString();
            int.TryParse(strParentID, out intParentID);

            int UserIDID = Eggsoft_Public_CL.GoodP.Get_UserID_From_Order_ID(Convert.ToInt32(Model_tab_Orderdetails.OrderID));


            EggsoftWX.BLL.tab_Goods BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            EggsoftWX.Model.tab_Goods Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
            Model_tab_Goods = BLL_tab_Goods.GetModel(Convert.ToInt32(Model_tab_Orderdetails.GoodID));

            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClients = new EggsoftWX.Model.tab_ShopClient();


            Model_tab_ShopClients = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_Goods.ShopClient_ID));

            Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
            myModel_MultiFenXiaoLevel.GoodID = Model_tab_Orderdetails.GoodID.toInt32();
            myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
            myModel_MultiFenXiaoLevel.UserID = UserIDID.toInt32();
            Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);


            //int intstrParentID = 0;
            //int.TryParse(strParentID, out intstrParentID);
            //Decimal AgentGet = 0;
            //Decimal ManagerAgentGet = 0;
            //int ManagerAgentParentID = 0;
            //Decimal ManagerGrandAgentGet = 0;
            //int ManagerGrandAgentParentID = 0;
            //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(Convert.ToInt32(Model_tab_Orderdetails.GoodID), intstrParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, "0", "0", "0");



            Decimal DecimalAllNotWeiBai = Convert.ToDecimal(Model_tab_Orderdetails.GoodPrice) * Convert.ToInt32(Model_tab_Orderdetails.OrderCount);

            string strShowMoneList = "<ul class=\"ulSalesData\">";

            #region 本商品总额


            strShowMoneList += "<li>本次购买商品总额¥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllNotWeiBai) + "</li>";
            #endregion

            if (intParentID > 0)
            {
                decimal decimalGetMoney = myModel_MultiFenXiaoLevel.AgentGet * DecimalAll;
                decimalGetMoney *= (decimal)0.01;
                strShowMoneList += "<li>代理，微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(intParentID.ToString()) + " " + Eggsoft_Public_CL.Pub.GetNickName(intParentID.ToString()) + " 得" + myModel_MultiFenXiaoLevel.AgentGet + "%" + "¥" + Eggsoft_Public_CL.Pub.getPubMoney(decimalGetMoney) + "</li>";
                DecimalAllNotWeiBai -= decimalGetMoney;
            }
            if (myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0)
            {
                decimal decimalGetMoney = myModel_MultiFenXiaoLevel.ManagerAgentGet * DecimalAll;
                decimalGetMoney *= (decimal)0.01;
                strShowMoneList += "<li>上级代理，微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(myModel_MultiFenXiaoLevel.ManagerAgentParentID.ToString()) + " " + Eggsoft_Public_CL.Pub.GetNickName(myModel_MultiFenXiaoLevel.ManagerAgentParentID.ToString()) + " 得" + myModel_MultiFenXiaoLevel.ManagerAgentGet + "%" + "¥" + Eggsoft_Public_CL.Pub.getPubMoney(decimalGetMoney) + "</li>";
                DecimalAllNotWeiBai -= decimalGetMoney;
            }
            if (myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID > 0)
            {
                decimal decimalGetMoney = myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * DecimalAll;
                decimalGetMoney *= (decimal)0.01;
                strShowMoneList += "<li>上上级代理，微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID.ToString()) + " " + Eggsoft_Public_CL.Pub.GetNickName(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID.ToString()) + " 得" + myModel_MultiFenXiaoLevel.ManagerGrandAgentGet + "%" + "¥" + Eggsoft_Public_CL.Pub.getPubMoney(decimalGetMoney) + "</li>";
                DecimalAllNotWeiBai -= decimalGetMoney;
            }
            #region 顾客 信息
            string strUserName = Eggsoft_Public_CL.Pub.GetNickName(UserIDID.ToString());

            EggsoftWX.BLL.tab_Order BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
            EggsoftWX.Model.tab_Order Model_tab_Order = new EggsoftWX.Model.tab_Order();
            Model_tab_Order = BLL_tab_Order.GetModel("ID=" + Model_tab_Orderdetails.OrderID);
            if (Model_tab_Order.TotalMoney >= 0)
            {
                strShowMoneList += "<li>顾客,微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(UserIDID.ToString()) + "  " + strUserName + " 微店现金余额支付¥" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_tab_Orderdetails.MoneyCredits)) + "," + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(Model_tab_Goods.ShopClient_ID.ToString()) + "支付¥" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_tab_Orderdetails.MoneyWeBuy8Credits)) + "</li>";
            }
            else if (Model_tab_Order.TotalMoney < 0)
            {
                string strAgentLevelName = "";
                EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent_ = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + Model_tab_Order.UserID + " and IsDeleted=0");
                if (Model_tab_ShopClient_Agent_ != null)
                {
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level BLL_tab_ShopClient_Agent_Level = new EggsoftWX.BLL.tab_ShopClient_Agent_Level();
                    EggsoftWX.Model.tab_ShopClient_Agent_Level Model_tab_ShopClient_Agent_Level = BLL_tab_ShopClient_Agent_Level.GetModel("ID=" + Model_tab_ShopClient_Agent_.AgentLevelSelect);
                    if (Model_tab_ShopClient_Agent_Level != null)
                    {
                        strAgentLevelName = Model_tab_ShopClient_Agent_Level.AgentLevelName;
                    }
                }

                strShowMoneList += "<li>顾客,微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(UserIDID.ToString()) + "  " + strUserName + " " + strAgentLevelName + "授权代理商发生额" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_tab_Order.TotalMoney)) + "," + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(Model_tab_Goods.ShopClient_ID.ToString()) + "支付¥" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(Model_tab_Orderdetails.MoneyWeBuy8Credits)) + "</li>";
            }
            DecimalAllNotWeiBai -= Convert.ToDecimal(Model_tab_Orderdetails.MoneyCredits);
            DecimalAllNotWeiBai -= Convert.ToDecimal(Model_tab_Orderdetails.MoneyWeBuy8Credits);
            #endregion

            //#region 微店费率
            //decimal decimalWeiVaiGetMoney = p_DistributionMoney_List_From_Good_ID.WeiBaiGet * DecimalAll;
            //decimalWeiVaiGetMoney *= (decimal)0.01;
            //strShowMoneList += "<li>微店费率" + p_DistributionMoney_List_From_Good_ID.WeiBaiGet + "%,¥" + Eggsoft_Public_CL.Pub.getPubMoney(decimalWeiVaiGetMoney) + "</li>";
            //DecimalAllNotWeiBai -= decimalWeiVaiGetMoney;
            //#endregion

            #region 商铺所得
            Decimal_ShopGet = DecimalAllNotWeiBai;
            strShowMoneList += "<li>商铺所得" + "¥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllNotWeiBai) + "</li>";
            #endregion


            strShowMoneList += "</ul>";
            return strShowMoneList;

        }



        /// <summary>
        /// 加载团购商品的图片轮播
        /// </summary>
        /// <returns></returns>
        public static string doAnnouncePic_Good(string strIcon)
        {
            string strMakeHtml_AnnouncePic_GoodList = "";

            try
            {



                strMakeHtml_AnnouncePic_GoodList += "    <div style=\"-webkit-transform: translate3d(0,0,0);\">\n";
                strMakeHtml_AnnouncePic_GoodList += "        <div style=\"visibility: visible;\" id=\"banner_box\" class=\"box_swipe\">\n";

                strMakeHtml_AnnouncePic_GoodList += doAnnouncePic_Good_MakeHtml_AnnouncePic_GoodList(strIcon);


                strMakeHtml_AnnouncePic_GoodList += " </div>\n";
                strMakeHtml_AnnouncePic_GoodList += "     </div>\n";
                strMakeHtml_AnnouncePic_GoodList += "     <script>\n";
                strMakeHtml_AnnouncePic_GoodList += "         $(function () {\n";
                strMakeHtml_AnnouncePic_GoodList += "             new Swipe(document.getElementById('banner_box'), {\n";
                strMakeHtml_AnnouncePic_GoodList += "                 speed: 500,\n";
                strMakeHtml_AnnouncePic_GoodList += "                 auto: 3000,\n";
                strMakeHtml_AnnouncePic_GoodList += "                 callback: function () {\n";
                strMakeHtml_AnnouncePic_GoodList += "                     var lis = $(this.element).next(\"ol\").children();\n";
                strMakeHtml_AnnouncePic_GoodList += "                    lis.removeClass(\"on\").eq(this.index).addClass(\"on\");\n";
                strMakeHtml_AnnouncePic_GoodList += "                 }\n";
                strMakeHtml_AnnouncePic_GoodList += "             });\n";
                strMakeHtml_AnnouncePic_GoodList += "        });\n";
                strMakeHtml_AnnouncePic_GoodList += "     </script>\n";



            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                Eggsoft.Common.debug_Log.Call_WriteLog("strIcon=" + strIcon);
            }
            finally
            {

            }

            return strMakeHtml_AnnouncePic_GoodList;
        }


        private static String doAnnouncePic_Good_MakeHtml_AnnouncePic_GoodList(string strIcon)
        {
            string strReturnAnnouncePic_GoodList = "";

            try
            {




                String[] StringmePicList = strIcon.Split(';');

                System.Collections.ArrayList alStringPicList = new System.Collections.ArrayList();
                for (int i = 0; i < StringmePicList.Length; i++)
                {
                    if (String.IsNullOrEmpty(StringmePicList[i]) == false)
                    {
                        if (Eggsoft.Common.FileFolder.RemoteFileExists(Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + StringmePicList[i]))
                        {
                            alStringPicList.Add(StringmePicList[i]);
                        }
                    }
                }


                string strAnnouncePic_GoodList = "";
                string strAnnouncePic_Dot_GoodList = "";
                ///start  保证有2个 在循环
                //if (alStringPicList.Count == 1)
                //{
                //    alStringPicList.Add(alStringPicList[0]);
                //}
                //end  保证有2个 在循环

                strReturnAnnouncePic_GoodList += "  <ul style=\"list-style: none outside none; width: 3200px; transition-duration: 500ms;transform: translate3d(0px, 0px, 0px);\">\n";
                strReturnAnnouncePic_GoodList += "            ###AnnouncePic_GoodList###\n";
                strReturnAnnouncePic_GoodList += "        </ul>\n";
                if (alStringPicList.Count > 1)///有2个才出现轮播图下面的位置示意图
                {
                    strReturnAnnouncePic_GoodList += "        <ol>\n";
                    strReturnAnnouncePic_GoodList += "            ###AnnouncePic_Dot_GoodList###\n";
                    strReturnAnnouncePic_GoodList += "        </ol>\n";
                }

                for (int k = 0; k < alStringPicList.Count; k++)
                {
                    string strimg = alStringPicList[k].ToString();
                    string GoodIcon = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + strimg;


                    strAnnouncePic_GoodList += "<li style=\"width: 640px; display: table-cell; vertical-align: top;\"><a href=\"#\"><img src=\"" + GoodIcon + "\" alt=\"" + "" + "\" style=\"width:100%;\"></a></li>\n";

                    if (k == 0)
                    {
                        strAnnouncePic_Dot_GoodList += "<li class=\"on\"></li>";
                    }
                    else
                    {
                        strAnnouncePic_Dot_GoodList += "<li class=\"\"></li>";
                    }

                }

                strReturnAnnouncePic_GoodList = strReturnAnnouncePic_GoodList.Replace("###AnnouncePic_GoodList###", strAnnouncePic_GoodList);
                strReturnAnnouncePic_GoodList = strReturnAnnouncePic_GoodList.Replace("###AnnouncePic_Dot_GoodList###", strAnnouncePic_Dot_GoodList);



            }
            catch (System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                Eggsoft.Common.debug_Log.Call_WriteLog("strIcon=" + strIcon);
            }
            finally
            {

            }

            return strReturnAnnouncePic_GoodList;
        }


    }



    public class ClassCart
    {
        public string getClassCart(string strQureyID_ShopingCart, string strQureyGoodCount, string strQureyUserID)
        {
            //
            // 商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
            //
            string strCartGoods = "";
            try
            {
                int pub_Int_ID_ShopingCart = 0;
                int.TryParse(strQureyID_ShopingCart, out pub_Int_ID_ShopingCart);

                int pub_GoodCount = 0;
                int.TryParse(strQureyGoodCount, out pub_GoodCount);

                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strQureyUserID, out pub_Int_Session_CurUserID);

                #region 用户在购物车中更改了选项
                EggsoftWX.BLL.tab_Order_ShopingCart my_BLL_tab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                if (pub_Int_ID_ShopingCart > 0)
                {
                    EggsoftWX.Model.tab_Order_ShopingCart my_Model_tab_Order_ShopingCart = new EggsoftWX.Model.tab_Order_ShopingCart();
                    my_Model_tab_Order_ShopingCart = my_BLL_tab_Order_ShopingCart.GetModel(pub_Int_ID_ShopingCart);

                    bool IFCanUpdateCart = true;
                    if (my_Model_tab_Order_ShopingCart.GoodType == 1)////微砍价不能更改数量商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                    {
                        IFCanUpdateCart = false;
                        ///微砍价  团购 等 订单 。不能 更改数量
                    }
                    else if (my_Model_tab_Order_ShopingCart.GoodType == 3)////
                    {

                        EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                        EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Convert.ToInt32(my_Model_tab_Order_ShopingCart.GoodTypeIdBuyInfo));
                        if (Model_tab_ZC_01Product_Support.OnlyBuyOneOnlyOneAccount == true)///一个微信账号 只能
                        {
                            IFCanUpdateCart = false;///微众筹一个微信账号 只能一个  不能更改数量
                        }
                        else
                        {
                            IFCanUpdateCart = true;
                        }

                        ///微砍价  团购 等 订单 。不能 更改数量
                    }
                    else if (my_Model_tab_Order_ShopingCart.GoodType == 2)////微团购是否能 重复购买  商品 来源 类型 0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                    {
                        EggsoftWX.BLL.tab_TuanGou BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                        EggsoftWX.Model.tab_TuanGou Model_tab_TuanGou = BLL_tab_TuanGou.GetModel(Convert.ToInt32(my_Model_tab_Order_ShopingCart.GoodTypeId));
                        if ((Model_tab_TuanGou != null) && (Model_tab_TuanGou.BuyMultiOnlyOneAccount == true))////有限制性购买的问题
                        {
                            string str_Limittab_TuanGouBuy_CanKunCunWhere_Order_AllGoods = "select sum(OrderCount) as OrderCount  from View_Order_AllGoods where GoodType=2 and GoodTypeId=" + my_Model_tab_Order_ShopingCart.GoodTypeId + " and GoodID=" + my_Model_tab_Order_ShopingCart.GoodID + " and UserID=" + my_Model_tab_Order_ShopingCart.UserID + "  ";
                            EggsoftWX.BLL.View_Order_AllGoods BLL_View_Order_AllGoods = new EggsoftWX.BLL.View_Order_AllGoods();
                            string str_LimitTuanGouBuy_ingintCheckHaveAddCartGoodID_Order_AllGoods = BLL_View_Order_AllGoods.SelectList(str_Limittab_TuanGouBuy_CanKunCunWhere_Order_AllGoods).Tables[0].Rows[0][0].ToString();
                            int int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods = 0;
                            int.TryParse(str_LimitTuanGouBuy_ingintCheckHaveAddCartGoodID_Order_AllGoods, out int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods);
                            if (int_LimitTuanGouBuy_CheckHaveAddCart_Order_AllGoods > 0)
                            {
                                ///订单表中已存在。。。可能是未支付
                                IFCanUpdateCart = false; ;
                            }
                            else
                            {
                                ///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                                EggsoftWX.BLL.tab_Order_ShopingCart MMMtab_Order_ShopingCart = new EggsoftWX.BLL.tab_Order_ShopingCart();
                                string str_Limittab_TuanGouBuy_tab_Order_ShopingCart = "select sum(GoodIDCount) as CartCount  from tab_Order_ShopingCart where GoodType=2 and GoodTypeId=" + my_Model_tab_Order_ShopingCart.GoodTypeId + " and IsDeleted<>1 and GoodID=" + my_Model_tab_Order_ShopingCart.GoodID + " and UserID=" + my_Model_tab_Order_ShopingCart.UserID;
                                string strCartCount = MMMtab_Order_ShopingCart.SelectList(str_Limittab_TuanGouBuy_tab_Order_ShopingCart).Tables[0].Rows[0][0].ToString();
                                int intCartCount = 0;
                                int.TryParse(strCartCount, out intCartCount);
                                if (intCartCount > 0)
                                {
                                    // MMMtab_Order_ShopingCart.Add()
                                    ///购物车中已存在
                                    IFCanUpdateCart = false; ;
                                }
                            }
                        }
                    }


                    if (IFCanUpdateCart)///可以更新
                    {
                        #region  更新库存
                        int intOldGoodIDCount = ((my_Model_tab_Order_ShopingCart.GoodIDCount == null) ? 0 : Convert.ToInt32(my_Model_tab_Order_ShopingCart.GoodIDCount));
                        int intGoodID = ((my_Model_tab_Order_ShopingCart.GoodID == null) ? 0 : Convert.ToInt32(my_Model_tab_Order_ShopingCart.GoodID));
                        EggsoftWX.BLL.tab_Goods my_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        EggsoftWX.Model.tab_Goods my_Model_tab_Goods = my_tab_Goods.GetModel(intGoodID);

                        if ((pub_GoodCount <= my_Model_tab_Goods.MaxOrderNum) && (pub_GoodCount <= my_Model_tab_Goods.KuCunCount))
                        {
                            if (pub_GoodCount > intOldGoodIDCount)
                            {
                                my_tab_Goods.Update("KuCunCount=KuCunCount-" + (pub_GoodCount - intOldGoodIDCount), "id=" + intGoodID);
                            }
                            else
                            {
                                my_tab_Goods.Update("KuCunCount=KuCunCount+" + (intOldGoodIDCount - pub_GoodCount), "id=" + intGoodID);
                            }


                            if (pub_GoodCount - my_Model_tab_Goods.MinOrderNum > -1)
                            {
                                my_Model_tab_Order_ShopingCart.GoodIDCount = pub_GoodCount;
                                my_BLL_tab_Order_ShopingCart.Update(my_Model_tab_Order_ShopingCart);
                            }
                            else
                            {
                                if (my_Model_tab_Goods.MinOrderNum > 1)///非正常途径 削减  直接 减为0  
                                {
                                    my_tab_Goods.Update("KuCunCount=KuCunCount+" + (my_Model_tab_Goods.MinOrderNum), "id=" + intGoodID);
                                }
                                Eggsoft_Public_CL.ShoppingCart.ClearShoppingCartThisID(pub_Int_ID_ShopingCart);
                                my_BLL_tab_Order_ShopingCart.Delete(pub_Int_ID_ShopingCart);
                            }
                        }
                        #endregion
                    }
                }

                #endregion

                #region 重新生成所有的购物信息列表

                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(pub_Int_Session_CurUserID);
                int intHowToGetProduct = Convert.ToInt32(Model_tab_User.HowToGetProduct);

                //System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.DataTable dt_DataTable_ShopingCart = my_BLL_tab_Order_ShopingCart.GetList("UserID=" + pub_Int_Session_CurUserID + " and IsDeleted<>1").Tables[0];

                if (dt_DataTable_ShopingCart.Rows.Count == 0)
                {
                    strCartGoods = "&nbsp;&nbsp;暂无购物信息！";
                }
                else
                {

                    //解决 一个商品颜色 不同 首件运费的问题
                    System.Collections.Generic.List<int> myProductIDList = new System.Collections.Generic.List<int>();


                    EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
                    string strPub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                    if (dt_DataTable_ShopingCart.Rows.Count > 0)
                    {
                        strCartGoods += "	<script type=\"text/javascript\">\n";
                        strCartGoods += "var varintHowToGetProductDoOnlyAfterloadCart=" + intHowToGetProduct + "	</script>\n";
                    }
                    ///统计 购物车满多少钱免运费 。满多少件免运费。
                    ///
                    Decimal DecimalAllCartKg = 0;//统计满多少公斤。
                    int intAllCartGood = 0;//统计满多少件。
                    Decimal Decimal_AllMoney = 0;//统计满多少钱
                    Decimal Decimal_AllYunFei = 0;//统计满多少钱
                    EggsoftWX.BLL.tab_FreightTemplate BLL_tab_FreightTemplate = new EggsoftWX.BLL.tab_FreightTemplate();
                    //ArrayList ArrayListFreightTemplate_List = new ArrayList();///统计总件数  总运费
                    //ArrayList ArrayListFreightTemplate_HelperID_List = new ArrayList();///统计总件数  总运费
                    ArrayList allCartFullViewList = new ArrayList();
                    for (int inti = 0; inti < dt_DataTable_ShopingCart.Rows.Count; inti++)
                    {
                        String strID_ShopingCart = dt_DataTable_ShopingCart.Rows[inti]["ID"].ToString();
                        String strGoodID = dt_DataTable_ShopingCart.Rows[inti]["GoodID"].ToString();
                        String strGoodID_Count = dt_DataTable_ShopingCart.Rows[inti]["GoodIDCount"].ToString();
                        String strMultiBuyType = dt_DataTable_ShopingCart.Rows[inti]["MultiBuyType"].ToString();
                        String strVouchersNum_List = dt_DataTable_ShopingCart.Rows[inti]["VouchersNum_List"].ToString();
                        String strGoodType = dt_DataTable_ShopingCart.Rows[inti]["GoodType"].ToString();///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                        String strGoodTypeId = dt_DataTable_ShopingCart.Rows[inti]["GoodTypeId"].ToString();
                        String strGoodTypeIdBuyInfo = dt_DataTable_ShopingCart.Rows[inti]["GoodTypeIdBuyInfo"].ToString();

                        my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(Int32.Parse(strGoodID));
                        ///

                        String strBeans = dt_DataTable_ShopingCart.Rows[inti]["Beans"].ToString();
                        String strMoneyCredits = dt_DataTable_ShopingCart.Rows[inti]["MoneyCredits"].ToString();
                        String strMoneyWeBuy8Credits = dt_DataTable_ShopingCart.Rows[inti]["MoneyWeBuy8Credits"].ToString();
                        String strMoneyWealth = dt_DataTable_ShopingCart.Rows[inti]["WealthMoney"].ToString();

                        if (strBeans == "0") strBeans = "";//这里清空 有利于后面检查
                        if (strMoneyCredits == "0.00") strMoneyCredits = "";//这里清空 有利于后面检查
                        if (strMoneyWeBuy8Credits == "0.00") strMoneyWeBuy8Credits = "";//这里清空 有利于后面检查
                        if (strMoneyWealth == "0.00") strMoneyWealth = "";//这里清空 有利于后面检查
                        if (String.IsNullOrEmpty(strGoodTypeIdBuyInfo)) strGoodTypeIdBuyInfo = "0";

                        //if (my_BLL_tab_Goods.Exists(Convert.ToInt32(strGoodID)))
                        //{
                        int intGood = Convert.ToInt32(strGoodID);

                        my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(intGood);
                        string strGoodName = "";
                        if (strGoodType == "3")////众筹类的 取他的名字 GoodType商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                        {
                            EggsoftWX.BLL.tab_ZC_01Product_Support BLL_tab_ZC_01Product_Support = new EggsoftWX.BLL.tab_ZC_01Product_Support();
                            EggsoftWX.Model.tab_ZC_01Product_Support Model_tab_ZC_01Product_Support = BLL_tab_ZC_01Product_Support.GetModel(Int32.Parse(strGoodTypeIdBuyInfo));
                            strGoodName = GoodP.GetGoodType(Int32.Parse(strGoodType)) + Model_tab_ZC_01Product_Support.Name + my_Model_tab_Goods.Name;
                        }
                        else
                        {
                            strGoodName = GoodP.GetGoodType(Int32.Parse(strGoodType)) + my_Model_tab_Goods.Name;
                        }
                        string strGoodPrice = "";


                        bool boolmyProductIDListFirstDoFreight = true;
                        if (myProductIDList.Contains(intGood))
                        {
                            boolmyProductIDListFirstDoFreight = false;
                        }
                        else
                        {
                            myProductIDList.Add(intGood);
                        }
                        //Decimal dec_Good_Money = Eggsoft_Public_CL.ShoppingCart.CountCurPrice(strGoodID, strGoodID_Count, strMultiBuyType, strMoneyCredits, strVouchersNum_List, strMoneyWeBuy8Credits, strBeans, out strGoodPrice);
                        Decimal outDecimal_My_Freight = 0;//处理运费问题
                        string strShowYunFei = "";
                        string strMultiBuyTypeName = "";
                        bool boolCartSameGood = true;///处理购物车 同种商品 满足免运费的条件   购车车 才处理 不是购物车不处理 值是false
                        Eggsoft_Public_CL.AllcartYunFeiList myAllcartYunFeiList = new Eggsoft_Public_CL.AllcartYunFeiList();

                        ///本详细订单 用户需要支付的现金（已扣除购物券+现金） 
                        Decimal dec_Good_Money_Agent = Eggsoft_Public_CL.ShoppingCart.CountCur_Will_Pay_Price(pub_Int_Session_CurUserID, strGoodID, strGoodID_Count, strMultiBuyType, strMoneyCredits, strVouchersNum_List, strMoneyWeBuy8Credits, strMoneyWealth, strBeans, out strGoodPrice, out outDecimal_My_Freight, out strShowYunFei, boolmyProductIDListFirstDoFreight, out strMultiBuyTypeName, boolCartSameGood, out myAllcartYunFeiList, Int32.Parse(strGoodType), Int32.Parse(strGoodTypeId), strGoodTypeIdBuyInfo);
                        allCartFullViewList.Add(myAllcartYunFeiList);
                        ///商品 来源 类型  0 正常商品 1 微砍价订单 2团购订单 3 一元云购订单  4 自助收款  5会员充值 6运营中心
                        string strGoodLink = "";
                        if (strGoodType == "0")
                        {
                            strGoodLink = strPub_Agent_Path + "/product-" + strGoodID + ".aspx";
                        }
                        else if (strGoodType == "1")
                        {
                            strGoodLink = "/Huodong/WeiKanJia/default.html?kanjiaid=" + strGoodTypeId;
                        }
                        else if (strGoodType == "2")
                        {
                            strGoodLink = "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId;
                        }
                        else if (strGoodType == "3")
                        {
                            strGoodLink = "/addfunction/04zc_project/03zc.html?zcid=" + strGoodTypeId;
                        }
                        else if (strGoodType == "6")
                        {
                            strGoodLink = "/op-" + strGoodTypeId + "-" + strGoodTypeIdBuyInfo + ".aspx";
                        }
                        if (string.IsNullOrEmpty(strMultiBuyTypeName) == false) strMultiBuyTypeName = "(" + strMultiBuyTypeName + ")";
                        strCartGoods += "		<div style=\"display:block; clear:both;\" class=\"Cart_pro_list_name_EachGoods\" ms-repeat=\"items\">\n";
                        strCartGoods += "			<div class=\"pro_list_name left\">\n";
                        strCartGoods += "				<a style=\"margin-top: 0px;color:blue;\" title=\"" + strShowYunFei + "\" href=\"" + strGoodLink + "\">" + strGoodName + strMultiBuyTypeName + "</a>\n";
                        strCartGoods += "			</div>\n";
                        strCartGoods += "			<div class=\"pro_list_price left\">\n";
                        strCartGoods += "				￥" + strGoodPrice + "\n";
                        strCartGoods += "			</div>\n";
                        strCartGoods += "			<div id=\"ThisGoodID_CountCartID_" + strID_ShopingCart + "\" class=\"pro_list_count left\">\n";
                        strCartGoods += "				" + strGoodID_Count + "\n";
                        strCartGoods += "			</div>\n";
                        strCartGoods += "			<div id=\"ThisFullMoneyCartID_" + strID_ShopingCart + "\"  class=\"pro_list_sum left FullMoney\">\n";
                        strCartGoods += "				￥" + Eggsoft_Public_CL.Pub.getPubMoney(dec_Good_Money_Agent) + "\n";
                        strCartGoods += "			</div>\n";
                        strCartGoods += "			<div id=\"ThisoutDecimal_My_FreightCartID_" + strID_ShopingCart + "\" class=\"pro_list_sum left outDecimal_My_Freight\"  style=\"display:none;\">\n";
                        strCartGoods += "				￥" + Eggsoft_Public_CL.Pub.getPubMoney(dec_Good_Money_Agent - outDecimal_My_Freight) + "\n";
                        strCartGoods += "			</div>\n";
                        strCartGoods += "		</div>\n";

                        intAllCartGood += Int32.Parse(strGoodID_Count);//统计满多少件
                        Decimal_AllMoney += (dec_Good_Money_Agent - outDecimal_My_Freight);//统计满多少钱
                        Decimal_AllYunFei += outDecimal_My_Freight;//运费总计
                        DecimalAllCartKg += Convert.ToDecimal(my_Model_tab_Goods.kg) * Int32.Parse(strGoodID_Count);//统计满多少公斤
                        strCartGoods += "      <div class=\"YJJ_inputs_OneLine\" style=\"display:block; clear:both;height:25px;margin-bottom:16px;\">\n";
                        strCartGoods += "      <span style=\"display:inline-block; height:25px;margin-bottom:16px;\"  class=\"YJJ_inputs_Line_Left\">\n";
                        string strShowYunFeiValue = outDecimal_My_Freight > 0 ? ("含运费:￥" + Eggsoft_Public_CL.Pub.getPubMoney(outDecimal_My_Freight)) : "免运费";
                        strCartGoods += "   " + strShowYunFeiValue + "  </span>\n";

                        strCartGoods += "      <span style=\"display:inline-block; height:25px;margin-bottom:16px;\"  class=\"YJJ_inputs_Line\">\n";
                        strCartGoods += "          <span class=\"YJJ_Left_number\">数量:</span>\n";

                        if ((String.IsNullOrEmpty(strVouchersNum_List)) && (String.IsNullOrEmpty(strMoneyCredits)) && (String.IsNullOrEmpty(strMoneyWeBuy8Credits)) && (String.IsNullOrEmpty(strMoneyWealth)))////佣金之类的都没有使用过
                        {
                            strCartGoods += "          <input type=\"button\" value=\"-\" id=\"jian" + strID_ShopingCart + "\" onclick=\"CartJian(" + strID_ShopingCart + ")\" class=\"YJJ_inputs_left\">\n";
                        }
                        else
                        {///使用过的  关闭递减功能
                            strCartGoods += "          <input type=\"button\" value=\"-\" id=\"jian" + strID_ShopingCart + "\" style=\"color:#EBE2E2;\" class=\"YJJ_inputs_left\">\n";
                        }

                        strCartGoods += "          <input type=\"text\" readonly=\"readonly\" size=\"3\" id=\"valuesCart" + strID_ShopingCart + "\" class=\"YJJ_values_values\" value=\"" + strGoodID_Count + "\">\n";
                        strCartGoods += "          <input type=\"button\" value=\"+\" id=\"add" + strID_ShopingCart + "\" onclick=\"CartAdd(" + strID_ShopingCart + ")\" class=\"YJJ_inputs_Right\">\n";
                        strCartGoods += "      </span>\n";
                        strCartGoods += "   </div>\n";
                    }


                    //#endregion

                    #region 全场满运费
                    Decimal DecimalMaxMAXKgNoFright = 0;
                    Decimal DecimalMaxMAXMoneyNoFright = 0;
                    int intMaxMAXIntNoFright = 0;


                    Decimal DecimalAllFright = 0;
                    Decimal DecimalAllKg = 0;
                    Decimal DecimalAllMoney = 0;
                    int intAllIntN = 0;

                    for (int i = 0; i < allCartFullViewList.Count; i++)
                    {
                        Eggsoft_Public_CL.AllcartYunFeiList myEggsoft_Public_CL_AllcartYunFeiList = (Eggsoft_Public_CL.AllcartYunFeiList)allCartFullViewList[i];
                        if (myEggsoft_Public_CL_AllcartYunFeiList.FreightTempletID > 0)
                        {
                            if (myEggsoft_Public_CL_AllcartYunFeiList.MAXKgNoFright > DecimalMaxMAXKgNoFright) DecimalMaxMAXKgNoFright = myEggsoft_Public_CL_AllcartYunFeiList.MAXKgNoFright;
                            if (myEggsoft_Public_CL_AllcartYunFeiList.MAXMoneyNoFright > DecimalMaxMAXMoneyNoFright) DecimalMaxMAXMoneyNoFright = myEggsoft_Public_CL_AllcartYunFeiList.MAXMoneyNoFright;
                            if (myEggsoft_Public_CL_AllcartYunFeiList.MAXIntNoFright > intMaxMAXIntNoFright) intMaxMAXIntNoFright = myEggsoft_Public_CL_AllcartYunFeiList.MAXIntNoFright;
                        }
                        DecimalAllFright += myEggsoft_Public_CL_AllcartYunFeiList.DecimalAllFright;
                        DecimalAllKg += myEggsoft_Public_CL_AllcartYunFeiList.DecimalAllkg;
                        DecimalAllMoney += myEggsoft_Public_CL_AllcartYunFeiList.DecimalAllGoodPrice;
                        intAllIntN += myEggsoft_Public_CL_AllcartYunFeiList.GoodCount;
                    }
                    Decimal onlyGoodsPriceNoFright = DecimalAllMoney - DecimalAllFright;


                    Decimal argDecimal_My_Freight = 0; string strargYunFeiText = "";
                    Eggsoft_Public_CL.ShoppingCart.getYunFeiText_YunFeiText(DecimalMaxMAXKgNoFright, DecimalMaxMAXMoneyNoFright, intMaxMAXIntNoFright, DecimalAllKg, onlyGoodsPriceNoFright, intAllIntN, out argDecimal_My_Freight, out strargYunFeiText);
                    if (argDecimal_My_Freight > 99999999)///说明 没有满足免运费条件
                    {
                        argDecimal_My_Freight = DecimalAllFright;///没有满足包邮条件  就取实际的值
                    }
                    else if (argDecimal_My_Freight == 0)
                    {
                        argDecimal_My_Freight = 0;
                    }
                    string strIfShowKg = "";//统计满多少件
                    if (DecimalAllKg > 0)
                    {
                        strIfShowKg = DecimalAllCartKg > 1 ? "" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllKg) + "公斤," : "" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllKg * 1000) + "克,";
                    }

                    strCartGoods += "<div id=\"StaticCartselfFright\">共" + intAllIntN + "件," + strIfShowKg + "￥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllMoney) + "元,<span style=\"color:red\">运费￥" + Eggsoft_Public_CL.Pub.getPubMoney(argDecimal_My_Freight) + ",应支付总额￥" + Eggsoft_Public_CL.Pub.getPubMoney(onlyGoodsPriceNoFright + argDecimal_My_Freight) + "</span>" + strargYunFeiText + "</div>";
                    strCartGoods += "<div id=\"StaticCartselfNoFright\">共" + intAllIntN + "件," + strIfShowKg + "￥" + Eggsoft_Public_CL.Pub.getPubMoney(DecimalAllMoney) + "元,<span style=\"color:red\">运费￥0,应支付总额￥" + Eggsoft_Public_CL.Pub.getPubMoney(onlyGoodsPriceNoFright) + "</span>" + "</div>";
                    #endregion



                }
                #endregion

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "购物车计算");
            }
            finally
            {


            }
            return strCartGoods;
        }
    }


}