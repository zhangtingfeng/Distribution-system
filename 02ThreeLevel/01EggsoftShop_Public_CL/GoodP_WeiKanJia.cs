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
    public class GoodP_WeiKanJia
    {
        public GoodP_WeiKanJia()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static void tellShopClientID_UserPayMoney_ByWeiXin_WeiKanJia(String strPayGoodPrice, String strUserID, int intGoodTypeId, int intMaster_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();


                string ShopClientID = my_Model_tab_WeiKanJia.ShopClientID.ToString();
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));

                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "微店" + " 用户付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_WeiKanJia.GoodID));

                int intUserID = Convert.ToInt32(strUserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != my_Model_tab_WeiKanJia.ShopClientID) return;///越界事情 直接返回


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);


                string strDescription = "亲，我们给你发信，是因为用户" + Pub.GetNickName(strUserID) + "付款通知 " + GoodP.GetGoodType(1) + myModel_tab_Goods.Name + " 引起！" + "\n";
                strDescription += "微店微砍价号：" + intMaster_Number + "\n";
                strDescription += "微店商品名称：" + myModel_tab_Goods.Name + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice)) + "\n";
                strDescription += "请登陆PC端商户后台进行检查组团情况、发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";

                string strClickURL = "";

                strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=?TuanGouID=" + intGoodTypeId;


                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(Convert.ToInt32(ShopClientID), "WinXinPayedMoney"))
                {
                    string[] stringWeiXinRalationUserIDList = Pub.GetstringWeiXinRalationUserIDList(my_Model_tab_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), intUserID, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        //{

                        //if (("45015" == strMessageImage))
                        {
                            if (boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), intUserIDShopClientID, strClickURL, "用户" + Pub.GetNickName(intUserID.ToString()) + "微砍价付款通知", intMaster_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice)), strDescription.Replace("\n", ""));
                                //Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), my_Model_tab_Order.UserID, WeiXinTuWens_ArrayList);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "微砍价");
            }
            finally
            {

            }
        }

        public static void tellShopClientID_o2o_UserPayMoney_ByWeiXin_WeiKanJia(String strPayGoodPrice, String strUserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {

                EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();
                string ShopClientID = my_Model_tab_WeiKanJia.ShopClientID.ToString();

                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));

                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + ShopClientID);
                if (boolExsit == false) return;


                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "o2o微店" + " 微砍价用户付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_WeiKanJia.GoodID));

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(Convert.ToInt32(strUserID), out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                string strDescription = "亲，我们给你发信，是因为微砍价用户" + Pub.GetNickName(strUserID) + "付款通知 " + GoodP.GetGoodType(1) + myModel_tab_Goods.Name + " 引起！" + "\n";
                strTitle += "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里" + "\n";
                strDescription += "微店微砍价号：" + intTuanGou_Number + "\n";
                strDescription += "微店微砍价订单名称：" + myModel_tab_Goods.Name + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice)) + "\n";
                //strDescription += "请登陆PC端商户后台进行发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";
                string strClickURL = "";

                strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=?TuanGouID=" + intGoodTypeId;

                //string strClickURL = "http://" + my_Model_tab_ShopClient.ErJiYuMing + "/product-" + myModel_tab_Goods.ID + ".aspx";
                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(Convert.ToInt32(ShopClientID), "WinXinPayedMoney"))
                {
                    EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(strUserID), WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            //if (("45015" == strMessageImage))
                            //{
                            if (boolTempletPayMessage)
                            {
                                //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)

                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(ShopClientID), strClickURL, "用户" + Pub.GetNickName(strUserID) + "微砍价付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice)), strDescription.Replace("\n", ""));
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "微砍价");
            }
            finally
            {

            }
        }

        public static void tell_UserIGetMoneyPayMoney_ByWeiXin_WeiKanJia(String strPayGoodPrice, String UserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();
                string ShopClientID = my_Model_tab_WeiKanJia.ShopClientID.ToString();


                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
                my_Model_tab_User = my_BLL_tab_User.GetModel("id='" + UserID + "'");

                string strTitle = "亲，我们已收到您的微砍价订单，金额是：¥" + Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice));
                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_WeiKanJia.GoodID));


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                //EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                //EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                int intShopClient_ID = Convert.ToInt32(ShopClientID);
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(intShopClient_ID.ToString(), "TempletPayMessage");///是否可以发模板消息

                int intUserID = Convert.ToInt32(UserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != intShopClient_ID) return;///越界事情 直接返回


                string strDescription = "亲，我们给你发信，是" + Pub.GetNickName(UserID.ToString()) + "已付款通知 " + GoodP.GetGoodType(1) + "引起！会尽快安排发货。" + "\n ";
                strDescription += "微店微砍价订单号：" + intTuanGou_Number + "\n ";
                strDescription += DateTime.Now + "\n";

                int intParentID = 0;
                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                if (BLL_tab_Orderdetails.Exists("GoodType=2 and GoodTypeId=" + intGoodTypeId + " and GoodTypeIdBuyInfo='" + intTuanGou_Number + "'"))
                {
                    EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel("GoodType=2 and GoodTypeId=" + intGoodTypeId + " and GoodTypeIdBuyInfo='" + intTuanGou_Number + "'");
                    intParentID = ((Model_tab_Orderdetails.ParentID == null) ? 0 : Convert.ToInt32(Model_tab_Orderdetails.ParentID));
                }
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + UserID + " and IsDeleted=0 and isnull(Empowered, 0) = 1");///有代理啊
                string strAgent = "";
                if (boolAgent)
                {
                    strAgent = UserID;
                }
                else if (intParentID > 0)
                {
                    strAgent = intParentID.ToString();
                }
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(UserID);
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;

                strErJiYuMing = "https://" + strErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=?TuanGouID=" + intGoodTypeId;


                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                WeiXinTuWens_ArrayList.Add(First);


                string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(UserID), 0, WeiXinTuWens_ArrayList);

                string[] strCheckReSendList = { "45015", "45047" };
                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                if (exists)
                {

                    if (boolTempletPayMessage)
                    {
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(UserID), Convert.ToInt32(ShopClientID), strErJiYuMing, "用户" + Pub.GetNickName(UserID.ToString()) + "微砍价付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice)), strDescription.Replace("\n", ""));
                    }
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "微砍价");
            }
            finally
            {

            }
        }

        public static void tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin_WeiKanJia(String strPayGoodPrice, int intOrderNum, String strUserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_WeiKanJia my_BLL_tab_WeiKanJia = new EggsoftWX.BLL.tab_WeiKanJia();
                EggsoftWX.Model.tab_WeiKanJia my_Model_tab_WeiKanJia = new EggsoftWX.Model.tab_WeiKanJia();


                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID).ToString(), "TempletPayMessage");///是否可以发模板消息

                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                System.Data.DataTable myDataTable = BLL_tab_Orderdetails.GetList("OrderID=" + intOrderNum + " and isdeleted<>1").Tables[0];
                for (int i = 0; i < myDataTable.Rows.Count; i++)
                {
                    string str_Orderdetails_ID = myDataTable.Rows[i]["ID"].ToString();
                    string strParentID = myDataTable.Rows[i]["ParentID"].ToString();
                    string strGoodID = myDataTable.Rows[i]["GoodID"].ToString();
                    string strGoodPrice = myDataTable.Rows[i]["GoodPrice"].ToString();
                    string strOrderCount = myDataTable.Rows[i]["OrderCount"].ToString();
                    string strGoodType = myDataTable.Rows[i]["GoodType"].ToString();
                    string strGoodTypeId = myDataTable.Rows[i]["GoodTypeId"].ToString();
                    string strGoodTypeIdBuyInfo = myDataTable.Rows[i]["GoodTypeIdBuyInfo"].ToString();

                    Decimal ALLDecimalMoney = decimal.Multiply(Convert.ToDecimal(strPayGoodPrice), decimal.Parse(strOrderCount));



                    int intShopClient_PartnerID = 0; //GoodP.GetShopClient_PartnerID_From_Good_ID(Int32.Parse(strGoodID));
                    int intShopClient_RecommendID = 0;// GoodP.GetShopClient_RecommendID_From_Good_ID(Int32.Parse(strGoodID));
                    Eggsoft_Public_CL.GoodP.GetShopClient_PartnerID_RecommendWeiXinID_From_Good_ID(Int32.Parse(strGoodID), out intShopClient_PartnerID, out intShopClient_RecommendID);



                    Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                    myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                    myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
                    myModel_MultiFenXiaoLevel.strGoodType = strGoodType;
                    myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                    myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;

                    //string strUserID = myDataTable.Rows[i]["UserID"].ToString();
                    //int intParentID = 0;
                    //int.TryParse(strParentID, out intParentID);

                    //Decimal AgentGet = 0;
                    //Decimal ManagerAgentGet = 0;
                    //int ManagerAgentParentID = 0;
                    //Decimal ManagerGrandAgentGet = 0;
                    //int ManagerGrandAgentParentID = 0;
                    //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(Int32.Parse(strGoodID), intParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, "1", intGoodTypeId.ToString(), strGoodTypeIdBuyInfo);


                    EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(GoodP.Get_A_GoodID_From_Order_ID(intOrderNum)[0]));
                    string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);
                    string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;

                    EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + strUserID + " and IsDeleted=0 and isnull(Empowered, 0) = 1");///有代理啊
                    string strAgent = "";
                    if (boolAgent)
                    {
                        strAgent = strUserID;
                    }
                    else if (strParentID.toInt32() > 0)
                    {
                        strAgent = strParentID;
                    }
                    EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                    EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(strUserID);
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                    string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;

                    strErJiYuMing = "https://" + strErJiYuMing + "/Huodong/WeiKanJia/default.html?kanjiaid=" + intGoodTypeId;




                    EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                    EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                    int intShopClient_ID = Convert.ToInt32(my_Model_tab_Order.ShopClient_ID);

                    int intUserID = Convert.ToInt32(my_Model_tab_Order.UserID);
                    int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                    if (intUserIDShopClientID != my_Model_tab_Order.ShopClient_ID) return;///越界事情 直接返回


                    if ((myModel_MultiFenXiaoLevel.intParentID > 0) && (myModel_MultiFenXiaoLevel.AgentGet > 0))
                    {
                        Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, myModel_MultiFenXiaoLevel.AgentGet);
                        DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);
                        if (DecimalMoney >= (Decimal)0.01)
                        {
                            string strTitle = "一级代理亲，我们已经计算出您微砍价分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                            ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                            //实例化几个WeiXinTuWen类对象  



                            string strDescription = "一级代理亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已微砍价付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
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
                                    //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)

                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(strParentID), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID) + "微砍价付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                }
                            }

                        }
                        //Pub_GetOpenID_And_.SendTextWinXinMessageImage(0, 0, WeiXinTuWens_ArrayList, "代理分享链接付款消息");
                    }
                    if ((myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0) && (myModel_MultiFenXiaoLevel.ManagerAgentGet > 0))
                    {
                        Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, myModel_MultiFenXiaoLevel.ManagerAgentGet);
                        DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);
                        if (DecimalMoney >= (Decimal)0.01)
                        {
                            string strTitle = "二级级代理亲，我们已经计算出您微砍价分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                            ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                            //实例化几个WeiXinTuWen类对象  



                            string strDescription = "二级代理亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已微砍价付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                            strDescription += DateTime.Now + "\n";

                            ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                            WeiXinTuWens_ArrayList.Add(First);


                            string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, 0, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {

                                if (boolTempletPayMessage)
                                {
                                    //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)

                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID.ToString()) + "微砍价付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                }
                            }
                        }
                    }
                    if ((myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID > 0) && (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet > 0))
                    {
                        Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, myModel_MultiFenXiaoLevel.ManagerGrandAgentGet);
                        DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);
                        if (DecimalMoney >= (Decimal)0.01)
                        {
                            string strTitle = "三级代理亲，我们已经计算出您微砍价分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                            ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                            //实例化几个WeiXinTuWen类对象  



                            string strDescription = "三级代理亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已微砍价付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "\n";
                            strDescription += DateTime.Now + "\n";

                            ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                            WeiXinTuWens_ArrayList.Add(First);


                            string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, 0, WeiXinTuWens_ArrayList);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {

                                if (boolTempletPayMessage)
                                {
                                    //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID.ToString()) + "微砍价付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "微砍价");
            }
            finally
            {

            }
        }


    }
}