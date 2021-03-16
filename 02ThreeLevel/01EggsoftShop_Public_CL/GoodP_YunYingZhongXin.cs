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
    public class GoodP_YunYingZhongXin
    {
        public GoodP_YunYingZhongXin()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 检查每个自然月的单数
        /// </summary>
        /// <param name="intuserID">42487</param>
        /// <param name="ShopClientID">21</param>
        /// <returns> 返回一个 大于0的值 表示 已经 买了很多个的值 </returns>
        public void checkLimitBuyEveryMonth(int intuserID, int ShopClientID, int intGoodTypeType, out int intOutMaxCount, out int intNowCount)
        {

            intOutMaxCount = 0;/////
            intNowCount = 0;/////
            ///42487
            if(intGoodTypeType > 0)
            {
                EggsoftWX.BLL.b004_OperationGoods BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                EggsoftWX.Model.b004_OperationGoods Model_b004_OperationGoods = BLL_b004_OperationGoods.GetModel("ShopClient_ID=@ShopClient_ID and id=@id", ShopClientID, intGoodTypeType);

                if(Model_b004_OperationGoods != null)
                {
                    intOutMaxCount = Model_b004_OperationGoods.LimitBuyEveryMonth.toInt32();
                    if(intOutMaxCount > 0)
                    {
                        string strSQL = @"
select sum(OrderCount) from (SELECT tab_Order.UserID, tab_Order.PayStatus, tab_Order.PayDateTime, 
      tab_Orderdetails.OrderCount, tab_Orderdetails.GoodType, 
      tab_Orderdetails.GoodTypeId, tab_Orderdetails.GoodTypeIdBuyInfo, 
      tab_Orderdetails.GoodID
FROM tab_Orderdetails LEFT OUTER JOIN
      tab_Order ON 
      tab_Orderdetails.ShopClient_ID = tab_Order.ShopClient_ID AND 
      tab_Orderdetails.OrderID = tab_Order.ID
	  where tab_Orderdetails.ShopClient_ID=@ShopClient_ID and  tab_Order.ShopClient_ID=@ShopClient_ID  and tab_Order.userid=@UserID and tab_Orderdetails.GoodTypeIdBuyInfo=@GoodTypeIdBuyInfo  and tab_Order.PayStatus=1 and tab_Order.PayDateTime is not null and DATEDIFF(MONTH,tab_Order.PayDateTime,GETDATE())=0 and tab_Order.IsDeleted=0 
	  and tab_Order.UserID=@UserID) vvvv";
                        intNowCount = BLL_b004_OperationGoods.SelectList(strSQL, ShopClientID, intuserID, intGoodTypeType).Tables[0].Rows[0][0].toInt32();
                    }
                }
            }
        }

        /// <summary>
        /// 检查是否属于已经是 不受欢迎的用户  是的话 终止他们的资格
        /// </summary>
        public bool checkHaveQuitUser(int intuserID, int ShopClientID, int intGoodTypeType)
        {

            EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum BLL_b008_OpterationUserActiveReturnMoneyOrderNum = new EggsoftWX.BLL.b008_OpterationUserActiveReturnMoneyOrderNum();

            Boolean boolExsit = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.Exists("userid=@userid and ShopClient_ID=@ShopClient_ID and IsDeleted=0 and b004_OperationGoodsID=@b004_OperationGoodsID", intuserID, ShopClientID, intGoodTypeType);

            if(boolExsit)
            {
                string strSQLExsit = "select sum(ActiveOrderNum) from b008_OpterationUserActiveReturnMoneyOrderNum where userid=@userid and ShopClient_ID=@ShopClient_ID and IsDeleted=0 and b004_OperationGoodsID=@b004_OperationGoodsID";
                int intActiveOrderNum = BLL_b008_OpterationUserActiveReturnMoneyOrderNum.SelectList(strSQLExsit, intuserID, ShopClientID, intGoodTypeType).Tables[0].Rows[0][0].toInt32();
                //存在完全出局的情况 这种人 不能再买了;
                return intActiveOrderNum == 0;

            }
            else
            {
                return false;///可以买
            }



        }


        public static void tellShopClientID_UserPayMoney_ByWeiXin(Decimal DecimalAllMoney, String strUserID, int intGoodID, int intOperationCenterID, int intb004_OperationGoods, string OrderNum)
        {
            try
            {


                EggsoftWX.BLL.b002_OperationCenter my_BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter my_Model_b002_OperationCenter = my_BLL_b002_OperationCenter.GetModel(intOperationCenterID);


                string ShopClientID = my_Model_b002_OperationCenter.ShopClient_ID.ToString();
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));

                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "微店" + " 用户付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(intGoodID);

                int intUserID = Convert.ToInt32(strUserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if(intUserIDShopClientID != ShopClientID.toInt32()) return;///越界事情 直接返回


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);


                string strDescription = "亲，我们给你发信，是因为用户" + Pub.GetNickName(strUserID) + "付款通知 " + GoodP.GetGoodType(6) + my_Model_b002_OperationCenter.MasterName + myModel_tab_Goods.Name + " 引起！" + "\n";
                strDescription += "微店商品名称：" + myModel_tab_Goods.Name + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(DecimalAllMoney) + "\n";
                strDescription += "请登陆PC端商户后台进行检查运营情况、发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";

                string strClickURL = "";

                strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/op-" + intOperationCenterID + "-" + intb004_OperationGoods + ".aspx";

                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                WeiXinTuWens_ArrayList.Add(First);

                if(GoodP.GetShopClientAccptPowerList(Convert.ToInt32(ShopClientID), "WinXinPayedMoney"))
                {
                    string[] stringWeiXinRalationUserIDList = Pub.GetstringWeiXinRalationUserIDList(my_Model_tab_ShopClient.XML);
                    for(int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), intUserID, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if(exists)
                        {
                            if(boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]),
                                    intUserIDShopClientID, strClickURL,
                                    "用户" + Pub.GetNickName(intUserID.ToString()) + "运营中心付款通知",
                                    OrderNum,
                                    Pub.getPubMoney(Convert.ToDecimal(DecimalAllMoney)),
                                    strDescription.Replace("\n", ""));
                            }
                        }
                    }
                }

            }
            catch(Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "运营中心付款通知");
            }
            finally
            {

            }
        }

        public static void tellShopClientID_o2o_UserPayMoney_ByWeiXin(Decimal DecimalAllMoney, String strUserID, int intGoodID, int intOperationCenterID, int intb004_OperationGoods, string OrderNum)
        {
            try
            {

                EggsoftWX.BLL.b002_OperationCenter my_BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter my_Model_b002_OperationCenter = my_BLL_b002_OperationCenter.GetModel(intOperationCenterID);
                string ShopClientID = my_Model_b002_OperationCenter.ShopClient_ID.ToString();
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + ShopClientID);
                if(boolExsit == false) return;


                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "o2o微店" + " 运营中心用户付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(intGoodID);

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(Convert.ToInt32(strUserID), out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                string strDescription = "亲，我们给你发信，是因为运营中心用户" + Pub.GetNickName(strUserID) + "付款通知 " + GoodP.GetGoodType(6) + myModel_tab_Goods.Name + " 引起！" + "\n";
                strTitle += "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里" + "\n";
                strDescription += "微店运营中心订单名称：" + myModel_tab_Goods.Name + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(DecimalAllMoney) + "\n";
                string strClickURL = "";
                strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/op-" + intOperationCenterID + "-" + intb004_OperationGoods + ".aspx";
                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                WeiXinTuWens_ArrayList.Add(First);

                if(GoodP.GetShopClientAccptPowerList(Convert.ToInt32(ShopClientID), "WinXinPayedMoney"))
                {
                    EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for(int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(strUserID), WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if(exists)
                        {
                            if(boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]),
                                    Convert.ToInt32(ShopClientID), strClickURL,
                                    "用户" + Pub.GetNickName(strUserID) + "运营中心付款通知", OrderNum,
                                    Pub.getPubMoney(Convert.ToDecimal(DecimalAllMoney)),
                                    strDescription.Replace("\n", ""));
                            }
                        }
                    }
                }

            }
            catch(Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "运营中心付款通知");
            }
            finally
            {

            }
        }

        public static void tell_UserIGetMoneyPayMoney_ByWeiXin(Decimal DecimalAllMoney, String strUserID, int intGoodID, int intOperationCenterID, int intb004_OperationGoods, int intOrderID, int intGoodOrderCount, string OrderNum)
        {
            try
            {
                EggsoftWX.BLL.b002_OperationCenter my_BLL_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter my_Model_b002_OperationCenter = my_BLL_b002_OperationCenter.GetModel(intOperationCenterID);

                string ShopClientID = my_Model_b002_OperationCenter.ShopClient_ID.ToString();


                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
                my_Model_tab_User = my_BLL_tab_User.GetModel("id='" + strUserID + "'");

                EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                EggsoftWX.Model.tab_Order my_Model_tab_Order = new EggsoftWX.Model.tab_Order();
                my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderID);

                string strTitle = "亲，我们已收到您的运营中心订单，金额是：¥" + Pub.getPubMoney(DecimalAllMoney);
                ArrayList WeiXinTuWens_ArrayList = new ArrayList();


                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(intGoodID));
                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);
                int intShopClient_ID = Convert.ToInt32(ShopClientID);
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(intShopClient_ID.ToString(), "TempletPayMessage");///是否可以发模板消息
                int intUserID = Convert.ToInt32(strUserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if(intUserIDShopClientID != intShopClient_ID) return;///越界事情 直接返回
                string strDescription = "亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已付款通知 " + GoodP.GetGoodType(6) + "引起！会尽快安排发货。" + "\n ";
                strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                strDescription += "微店订单名称：" + my_Model_tab_Order.OrderName + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)) + "\n ";

                #region 财富返还
                EggsoftWX.BLL.b004_OperationGoods my_BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                EggsoftWX.Model.b004_OperationGoods my_Model_my_BLL_b004_OperationGoods = my_BLL_b004_OperationGoods.GetModel(intb004_OperationGoods);
                Decimal deReturnConsumerWealth = my_Model_my_BLL_b004_OperationGoods.ReturnConsumerWealth.toDecimal() * intGoodOrderCount * myModel_tab_Goods.PromotePrice.toDecimal();
                strDescription += "T+7后，您的财富" + Eggsoft_Public_CL.Pub.getBankPubMoney(deReturnConsumerWealth) + "会到账，并可得到财富返还资格" + "\n ";
                #endregion
                strDescription += DateTime.Now + "   \n ";



                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                //string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                string strErJiYuMing = "https://" + Model_tab_ShopClient.ErJiYuMing + "/op-" + intOperationCenterID + "-" + intb004_OperationGoods + ".aspx";



                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                WeiXinTuWens_ArrayList.Add(First);
                string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(strUserID), 0, WeiXinTuWens_ArrayList);
                string[] strCheckReSendList = { "45015", "45047" };
                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                if(exists)
                {

                    if(boolTempletPayMessage)
                    {
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(strUserID),
                            Convert.ToInt32(ShopClientID), strErJiYuMing,
                            "运营中心用户" + Pub.GetNickName(strUserID) + "付款通知",
                             OrderNum,
                            Pub.getPubMoney(Convert.ToDecimal(DecimalAllMoney)), strDescription.Replace("\n", ""));
                    }
                }
            }
            catch(Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "运营中心");
            }
            finally
            {

            }
        }

        public static void tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin(Decimal DecimalAllMoney, int intOrderNum, String strUserID, int GoodID, int intb004_OperationGoods, int intOperationCenterID)
        {
            try
            {
                EggsoftWX.BLL.b004_OperationGoods my_BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();



                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID).ToString(), "TempletPayMessage");///是否可以发模板消息

                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                System.Data.DataTable myDataTable = BLL_tab_Orderdetails.GetList("OrderID=" + intOrderNum + " and GoodType='6' and isdeleted<>1").Tables[0];
                for(int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string str_Orderdetails_ID = myDataTable.Rows[i]["ID"].ToString();
                    string strParentID = myDataTable.Rows[i]["ParentID"].ToString();
                    string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                    string strGoodPrice = myDataTable.Rows[i]["GoodPrice"].ToString();
                    string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();
                    string strGoodType = myDataTable.Rows[i]["GoodType"].ToString();
                    string strGoodTypeId = myDataTable.Rows[i]["GoodTypeId"].ToString();
                    string strGoodTypeIdBuyInfo = myDataTable.Rows[i]["GoodTypeIdBuyInfo"].ToString();

                    Decimal ALLDecimalMoney = decimal.Multiply(strGoodPrice.toDecimal(), decimal.Parse(strOrderCount));

                    int intShopClient_PartnerID = 0; //GoodP.GetShopClient_PartnerID_From_Good_ID(Int32.Parse(strGoodID));
                    int intShopClient_RecommendID = 0;// GoodP.GetShopClient_RecommendID_From_Good_ID(Int32.Parse(strGoodID));
                    Eggsoft_Public_CL.GoodP.GetShopClient_PartnerID_RecommendWeiXinID_From_Good_ID(Int32.Parse(strGoodID), out intShopClient_PartnerID, out intShopClient_RecommendID);



                    Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                    myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                    myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
                    myModel_MultiFenXiaoLevel.strGoodType = "0";
                    myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                    myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                    //int intParentID = 0;
                    //int.TryParse(strParentID, out intParentID);

                  
                    EggsoftWX.Model.b004_OperationGoods my_Model_BLL_b004_OperationGoods = my_BLL_b004_OperationGoods.GetModel(strGoodTypeIdBuyInfo.toInt32());

                    EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    string[] A_GoodID_From_Order_IDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                    EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(A_GoodID_From_Order_IDList[0]));
                    string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);
                    string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;

                    EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + strUserID + " and IsDeleted=0 and isnull(Empowered, 0) = 1");///有代理啊
                    string strAgent = "";
                    if(boolAgent)
                    {
                        strAgent = strUserID;
                    }
                    else if(strParentID.toInt32() > 0)
                    {
                        strAgent = strParentID;
                    }
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Int32.Parse(strUserID));
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                    string strErJiYuMing = "https://" + Model_tab_ShopClient.ErJiYuMing + "/op-" + strGoodTypeId + "-" + strGoodTypeIdBuyInfo + ".aspx";




                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                    int intShopClient_ID = Convert.ToInt32(my_Model_tab_Order.ShopClient_ID);

                    int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                    int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                    if(intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回

                    int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(strParentID.toInt32());
                    if(strParentID.toInt32() > 0)
                    {
                        Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyShareB.toDecimal());
                        DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);
                        if(DecimalMoney >= (Decimal)0.01)
                        {
                            string strTitle = "一级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们已经计算出您财富分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + myModel_tab_Goods.Name + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                            string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intShopClient_ID.ToString()) : "现金");
                            strTitle += strMoneyShow;


                            ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                            //实例化几个WeiXinTuWen类对象  



                            string strDescription = "一级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已财富付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "  \n";
                            strDescription += DateTime.Now + "\n";

                            ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                            WeiXinTuWens_ArrayList.Add(First);


                            string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(strParentID), 0, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {

                                if(boolTempletPayMessage)
                                {
                                    //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)

                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(strParentID), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID) + "财富付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                }
                            }
                        }
                    }
                    intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(myModel_MultiFenXiaoLevel.ManagerAgentParentID);
                    if(myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0)
                    {
                        Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyShareA.toDecimal());
                        DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);
                        if(DecimalMoney >= (Decimal)0.01)
                        {
                            string strTitle = "二级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们已经计算出您财富分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + myModel_tab_Goods.Name + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                            string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intShopClient_ID.ToString()) : "现金");
                            strTitle += strMoneyShow;


                            ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                            //实例化几个WeiXinTuWen类对象  



                            string strDescription = "二级" + (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已财富付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "    \n";
                            strDescription += DateTime.Now + "\n";

                            ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                            WeiXinTuWens_ArrayList.Add(First);


                            string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, 0, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {

                                if(boolTempletPayMessage)
                                {
                                    //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)

                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID.ToString()) + "财富付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "运营中心财富");
            }
            finally
            {

            }
        }


        public static void tell_YunYingZhongXin_ParentID_IGetMoneyPayMoney_ByWeiXin(Decimal DecimalAllMoney, int intOrderNum, String strUserID, int intGoodID, int intOperationCenterID, int intb004_OperationGoods)
        {
            try
            {

                EggsoftWX.BLL.b004_OperationGoods my_BLL_b004_OperationGoods = new EggsoftWX.BLL.b004_OperationGoods();
                EggsoftWX.BLL.b002_OperationCenter my_b002_OperationCenter = new EggsoftWX.BLL.b002_OperationCenter();
                EggsoftWX.Model.b002_OperationCenter my_Model_b002_OperationCenter = my_b002_OperationCenter.GetModel(intOperationCenterID);
                EggsoftWX.Model.b002_OperationCenter my_Parent_Model_b002_OperationCenter = my_b002_OperationCenter.GetModel(my_Model_b002_OperationCenter.ParentID.toInt32());



                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID).ToString(), "TempletPayMessage");///是否可以发模板消息

                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                System.Data.DataTable myDataTable = BLL_tab_Orderdetails.GetList("OrderID=" + intOrderNum + " and isdeleted<>1").Tables[0];
                for(int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string str_Orderdetails_ID = myDataTable.Rows[i]["ID"].ToString();
                    string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                    string strGoodPrice = myDataTable.Rows[i]["GoodPrice"].ToString();
                    string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();
                    string strGoodType = myDataTable.Rows[i]["GoodType"].ToString();
                    string strGoodTypeId = myDataTable.Rows[i]["GoodTypeId"].ToString();
                    string strGoodTypeIdBuyInfo = myDataTable.Rows[i]["GoodTypeIdBuyInfo"].ToString();
                    Decimal ALLDecimalMoney = decimal.Multiply(Convert.ToDecimal(strGoodPrice), decimal.Parse(strOrderCount));

                    EggsoftWX.Model.b004_OperationGoods my_Model_BLL_b004_OperationGoods = my_BLL_b004_OperationGoods.GetModel(strGoodTypeIdBuyInfo.toInt32());

                    EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    string[] A_GoodID_From_Order_IDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                    EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(A_GoodID_From_Order_IDList[0]));
                    string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);
                    string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;


                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Int32.Parse(strUserID));
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                    string strErJiYuMing = "https://" + Model_tab_ShopClient.ErJiYuMing + "/op-" + strGoodTypeId + "-" + strGoodTypeIdBuyInfo + ".aspx";



                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                    int intShopClient_ID = Convert.ToInt32(my_Model_tab_Order.ShopClient_ID);

                    int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                    int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                    if(intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回


                    if(my_Model_b002_OperationCenter != null && my_Model_b002_OperationCenter.RunningState.toBoolean() && (my_Model_BLL_b004_OperationGoods.ReturnMoneyOperationShareB > 0))
                    {
                        Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyOperationShareB.toDecimal());
                        DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);
                        if(DecimalMoney >= (Decimal)0.01)
                        {
                            string strTitle = "一级运营中心亲，我们已经计算出您财富分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + myModel_tab_Goods.Name + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                            ArrayList WeiXinTuWens_ArrayList = new ArrayList();



                            string strDescription = "一级运营中心亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已财富付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，运营所得将会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "  \n";
                            strDescription += DateTime.Now + "\n";

                            ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                            WeiXinTuWens_ArrayList.Add(First);

                            string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(my_Model_b002_OperationCenter.UserID.toInt32(), 0, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {

                                if(boolTempletPayMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(my_Model_b002_OperationCenter.UserID.toInt32(), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID) + "财富付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                }
                            }

                            #region 增加运营中心的订单消息通知
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = "已支付通知,待发货,待确认";
                            Model_b011_InfoAlertMessage.CreateBy = "运营中心已支付通知";
                            Model_b011_InfoAlertMessage.UpdateBy = "运营中心已支付通知";
                            Model_b011_InfoAlertMessage.UserID = my_Model_b002_OperationCenter.UserID.toInt32();
                            Model_b011_InfoAlertMessage.ShopClient_ID = my_Model_tab_Order.ShopClient_ID;
                            Model_b011_InfoAlertMessage.Type = "Info_myYunYingOrder";
                            Model_b011_InfoAlertMessage.TypeTableID = intOrderNum;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加运营中心的订单消息通知 


                        }
                    }
                    if(my_Parent_Model_b002_OperationCenter != null && my_Parent_Model_b002_OperationCenter.RunningState.toBoolean() && (my_Model_BLL_b004_OperationGoods.ReturnMoneyOperationShareA > 0))
                    {
                        Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, my_Model_BLL_b004_OperationGoods.ReturnMoneyOperationShareA.toDecimal());
                        DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);
                        if(DecimalMoney >= (Decimal)0.01)
                        {
                            string strTitle = "二级运营中心亲，我们已经计算出您财富分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + myModel_tab_Goods.Name + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                            ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                            //实例化几个WeiXinTuWen类对象  



                            string strDescription = "二级运营中心亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已财富付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "    \n";
                            strDescription += DateTime.Now + "\n";

                            ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                            WeiXinTuWens_ArrayList.Add(First);


                            string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(my_Parent_Model_b002_OperationCenter.UserID.toInt32(), 0, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {

                                if(boolTempletPayMessage)
                                {
                                    //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)

                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(my_Parent_Model_b002_OperationCenter.UserID.toInt32(), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID.ToString()) + "财富付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                }
                            }
                        }
                    }

                }
            }
            catch(Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "运营中心财富");
            }
            finally
            {

            }
        }


    }
}