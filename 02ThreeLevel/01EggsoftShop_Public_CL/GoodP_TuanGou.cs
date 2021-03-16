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
    public class GoodP_TuanGou
    {
        public GoodP_TuanGou()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public static void tellShopClientID_UserPayMoney_ByWeiXin_TuanGou(String strUserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(intGoodTypeId);


                string ShopClientID = my_Model_tab_TuanGou.ShopClientID.ToString();
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));

                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "微店" + " 用户付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_TuanGou.SourceGoodID));

                int intUserID = Convert.ToInt32(strUserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != my_Model_tab_TuanGou.ShopClientID) return;///越界事情 直接返回


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);


                string strDescription = "亲，我们给你发信，是因为用户" + Pub.GetNickName(strUserID) + "付款通知 " + GoodP.GetGoodType(2) + myModel_tab_Goods.Name + " 引起！" + "\n";
                strDescription += "微店团购号：" + intTuanGou_Number + "\n";
                strDescription += "微店商品名称：" + myModel_tab_Goods.Name + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)) + "\n";
                strDescription += "请登陆PC端商户后台进行检查组团情况、发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";

                string strClickURL = "";

                strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + intGoodTypeId + "&tuangouidnumber=" + intTuanGou_Number;


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
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), intUserIDShopClientID, strClickURL, "用户" + Pub.GetNickName(intUserID.ToString()) + "团购付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)), strDescription.Replace("\n", ""));
                                //Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), my_Model_tab_Order.UserID, WeiXinTuWens_ArrayList);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "团购");
            }
            finally
            {

            }
        }

        public static void tellShopClientID_o2o_UserPayMoney_ByWeiXin_TuanGou(String strUserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {

                EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(intGoodTypeId);
                string ShopClientID = my_Model_tab_TuanGou.ShopClientID.ToString();

                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));

                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + ShopClientID);
                if (boolExsit == false) return;


                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "o2o微店" + " 团购用户付款通知！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_TuanGou.SourceGoodID));

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(Convert.ToInt32(strUserID), out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                string strDescription = "亲，我们给你发信，是因为团购用户" + Pub.GetNickName(strUserID) + "付款通知 " + GoodP.GetGoodType(2) + myModel_tab_Goods.Name + " 引起！" + "\n";
                strTitle += "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里" + "\n";
                strDescription += "微店团购号：" + intTuanGou_Number + "\n";
                strDescription += "微店团购订单名称：" + myModel_tab_Goods.Name + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)) + "\n";
                //strDescription += "请登陆PC端商户后台进行发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";
                string strClickURL = "";

                strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + intGoodTypeId + "&tuangouidnumber=" + intTuanGou_Number;

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

                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(ShopClientID), strClickURL, "用户" + Pub.GetNickName(strUserID) + "团购付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)), strDescription.Replace("\n", ""));
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "团购");
            }
            finally
            {

            }
        }

        public static void tell_UserIGetMoneyPayMoney_ByWeiXin_TuanGou(String UserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(intGoodTypeId);
                string ShopClientID = my_Model_tab_TuanGou.ShopClientID.ToString();


                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
                my_Model_tab_User = my_BLL_tab_User.GetModel("id='" + UserID + "'");

                string strTitle = "亲，我们已收到您的团购订单，金额是：¥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice));
                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_TuanGou.SourceGoodID));


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                //EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                //EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                int intShopClient_ID = Convert.ToInt32(ShopClientID);
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(intShopClient_ID.ToString(), "TempletPayMessage");///是否可以发模板消息

                int intUserID = Convert.ToInt32(UserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != intShopClient_ID) return;///越界事情 直接返回


                string strDescription = "亲，我们给你发信，是" + Pub.GetNickName(UserID.ToString()) + "已付款通知 " + GoodP.GetGoodType(2) + "引起！会尽快安排发货。" + "\n ";
                strDescription += "微店团购订单号：" + intTuanGou_Number + "   \n";
                strDescription += DateTime.Now + "\n";

                int intParentID = 0;
                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                if (BLL_tab_Orderdetails.Exists("GoodType=2 and GoodTypeId=" + intGoodTypeId + " and GoodTypeIdBuyInfo='" + intTuanGou_Number + "'"))
                {
                    EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel("GoodType=2 and GoodTypeId=" + intGoodTypeId + " and GoodTypeIdBuyInfo='" + intTuanGou_Number + "'");
                    intParentID = ((Model_tab_Orderdetails.ParentID == null) ? 0 : Convert.ToInt32(Model_tab_Orderdetails.ParentID));
                }
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + UserID + "  and IsDeleted=0 and isnull(Empowered, 0) = 1");///有代理啊
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
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;

                strErJiYuMing = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + intGoodTypeId + "&tuangouidnumber=" + intTuanGou_Number + "&ParentID=" + strAgent;


                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                WeiXinTuWens_ArrayList.Add(First);


                string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(UserID), 0, WeiXinTuWens_ArrayList);

                string[] strCheckReSendList = { "45015", "45047" };
                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                if (exists)
                {

                    if (boolTempletPayMessage)
                    {
                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(UserID), Convert.ToInt32(ShopClientID), strErJiYuMing, "用户" + Pub.GetNickName(UserID.ToString()) + "团购付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)), strDescription.Replace("\n", ""));
                    }
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "团购");
            }
            finally
            {

            }
        }

        public static void tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin_TuanGou(int intOrderNum, String strUserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(intGoodTypeId);

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

                    Decimal ALLDecimalMoney = decimal.Multiply(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice), decimal.Parse(strOrderCount));
                    if (ALLDecimalMoney > 0)////有钱才有发送的意义
                    {
                        if (my_Model_tab_TuanGou.TuanZhangBonus_AgentGet == true)
                        { ///团长取得代理商利润
                            #region  团长取得代理商利润
                            Decimal TuanZhangAgentGet = Convert.ToDecimal(my_Model_tab_TuanGou.AgentPrice);
                            //int intParentID = 0;
                            //int.TryParse(strParentID, out intParentID);

                            //Decimal AgentGet = 0;
                            //Decimal ManagerAgentGet = 0;
                            //int ManagerAgentParentID = 0;
                            //Decimal ManagerGrandAgentGet = 0;
                            //int ManagerGrandAgentParentID = 0;
                            //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(Int32.Parse(strGoodID), intParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, "2", intGoodTypeId.ToString(), strGoodTypeIdBuyInfo);

                            Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                            myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                            myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
                            myModel_MultiFenXiaoLevel.strGoodType = "2";
                            myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                            myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                            myModel_MultiFenXiaoLevel.UserID = strUserID.toInt32();
                            Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);



                            EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                            EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                            EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                            string[] A_GoodID_From_Order_IDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                            EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(A_GoodID_From_Order_IDList[0]));
                            string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);
                            string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;
                            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(my_Model_tab_TuanGou.ShopClientID));
                            string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                            bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + strUserID+ " and IsDeleted=0");///有代理啊
                            string strAgent = "";
                            if (boolAgent)
                            {
                                strAgent = strUserID;
                            }
                            else if (myModel_MultiFenXiaoLevel.intParentID > 0)
                            {
                                strAgent = strParentID;
                            }

                            string strTuanZhangUserID = "0";
                            string strGetTuanZhangUserID = "select UserID from tab_TuanGou_Partner where TuanGouIDNumber=" + strGoodTypeIdBuyInfo + " and ParterRole=1 and ShopClientID=" + myModel_tab_Goods.ShopClient_ID;
                            System.Data.DataTable DataTableGetTuanZhangUserID = bll_tab_ShopClient_Agent_.SelectList(strGetTuanZhangUserID).Tables[0];
                            if (DataTableGetTuanZhangUserID.Rows.Count > 0)
                            {
                                strTuanZhangUserID = DataTableGetTuanZhangUserID.Rows[0]["UserID"].ToString();
                            }
                            if (strTuanZhangUserID != "0")
                            {
                                strErJiYuMing = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId + "&tuangouidnumber=" + strGoodTypeIdBuyInfo + "&parentid=" + strAgent;
                                string strTitle = "团长大人，我们已经计算出您团购分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的团购所得是：¥" + Pub.getPubMoney(TuanZhangAgentGet);
                                ArrayList WeiXinTuWens_ArrayList = new ArrayList();

                                string strDescription = "团长大人亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已团购付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                                strDescription += "亲，组团成功后后，所得将会自动转入您的余额。" + "\n";
                                strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "  \n";
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
                                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(strTuanZhangUserID), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID) + "团购付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 原三级分销代理商利润
                            //int intParentID = 0;
                            //int.TryParse(strParentID, out intParentID);

                            //Decimal AgentGet = 0;
                            //Decimal ManagerAgentGet = 0;
                            //int ManagerAgentParentID = 0;
                            //Decimal ManagerGrandAgentGet = 0;
                            //int ManagerGrandAgentParentID = 0;
                            //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(Int32.Parse(strGoodID), intParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, "2", intGoodTypeId.ToString(), strGoodTypeIdBuyInfo);

                            Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                            myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                            myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
                            myModel_MultiFenXiaoLevel.strGoodType = "2";
                            myModel_MultiFenXiaoLevel.strGoodTypeId = strGoodTypeId;
                            myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                            myModel_MultiFenXiaoLevel.UserID = strUserID.toInt32();
                            Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);

                            EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                            string[] A_GoodID_From_Order_IDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                            EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(A_GoodID_From_Order_IDList[0]));
                            string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);
                            string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;

                            EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                            bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + strUserID+ " and IsDeleted=0");///有代理啊
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
                            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Int32.Parse(strUserID));
                            EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                            EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                            string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;

                            strErJiYuMing = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId + "&tuangouidnumber=" + strGoodTypeIdBuyInfo + "&parentid=" + strAgent;




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
                                    string strTitle = "一级代理亲，我们已经计算出您团购分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                                    ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                                    //实例化几个WeiXinTuWen类对象  



                                    string strDescription = "一级代理亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已团购付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                                    strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
                                    strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "  \n";
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

                                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(strParentID), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID) + "团购付款通知", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
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
                                    string strTitle = "二级级代理亲，我们已经计算出您团购分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                                    ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                                    //实例化几个WeiXinTuWen类对象  



                                    string strDescription = "二级代理亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已团购付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                                    strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
                                    strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "    \n";
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

                                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID.ToString()) + "团购付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
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
                                    string strTitle = "三级代理亲，我们已经计算出您团购分享链接  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "产生的收益，您的分享所得是：¥" + Pub.getPubMoney(DecimalMoney);
                                    ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                                    //实例化几个WeiXinTuWen类对象  



                                    string strDescription = "三级代理亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已团购付款通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                                    strDescription += "亲，T+7后，代理所得将会自动转入您的余额。" + "\n";
                                    strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "   \n";
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

                                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "用户" + Pub.GetNickName(strUserID.ToString()) + "团购付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                                        }
                                    }
                                }
                            }
                            #endregion 原三级分销代理商利润
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "团购");
            }
            finally
            {

            }
        }



        /// <summary>
        /// 团购失效通知
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="intGoodTypeId"></param>
        /// <param name="intTuanGou_Number"></param>
        public static void tellShopClientID_UserPayMoney_ByWeiXin_TuanGou_DisEfficacy(String strUserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(intGoodTypeId);


                string ShopClientID = my_Model_tab_TuanGou.ShopClientID.ToString();
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));


                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_TuanGou.SourceGoodID));

                int intUserID = Convert.ToInt32(strUserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != my_Model_tab_TuanGou.ShopClientID) return;///越界事情 直接返回


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);


                string strDescription = "亲，我们给你发信，是因为用户" + Pub.GetNickName(strUserID) + "团购失效通知(客服将在1-2工作日内做退款处理) " + GoodP.GetGoodType(2) + myModel_tab_Goods.Name + " 引起！" + "\n";
                strDescription += "微店团购号：" + intTuanGou_Number + "\n";
                strDescription += "微店商品名称：" + myModel_tab_Goods.Name + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)) + "\n";
                strDescription += "请登陆PC端商户后台进行检查组团情况、发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";

                string strClickURL = "";

                strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + intGoodTypeId + "&tuangouidnumber=" + intTuanGou_Number;


                //ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                //WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(Convert.ToInt32(ShopClientID), "WinXinPayedMoney"))
                {
                    string[] stringWeiXinRalationUserIDList = Pub.GetstringWeiXinRalationUserIDList(my_Model_tab_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        //string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), intUserID, WeiXinTuWens_ArrayList);
                        //string[] strCheckReSendList = { "45015", "45047" };
                        //bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        //if (exists)
                        ////{

                        ////if (("45015" == strMessageImage))
                        //{
                        if (boolTempletPayMessage)
                        {
                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), intUserIDShopClientID, strClickURL, "(失效通知)" + "用户" + Pub.GetNickName(intUserID.ToString()) + "团购失效通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)), strDescription.Replace("\n", ""));
                            //Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), my_Model_tab_Order.UserID, WeiXinTuWens_ArrayList);
                        }
                        //}
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "团购");
            }
            finally
            {

            }
        }
        /// <summary>
        /// 团购失效通知
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="intGoodTypeId"></param>
        /// <param name="intTuanGou_Number"></param>
        public static void tellShopClientID_o2o_UserPayMoney_ByWeiXin_TuanGou_DisEfficacy(String strUserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {

                EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(intGoodTypeId);
                string ShopClientID = my_Model_tab_TuanGou.ShopClientID.ToString();

                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));

                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + ShopClientID);
                if (boolExsit == false) return;


                //ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                //string strTitle = tab_System_And_.getTab_System("CityName") + "o2o微店" + " 团购订单失效通知(客服将在1-2工作日内做退款处理)！";

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = new EggsoftWX.Model.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(my_Model_tab_Order.ID);
                myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_TuanGou.SourceGoodID));

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(Convert.ToInt32(strUserID), out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                string strDescription = "亲，我们给你发信，是因为团购用户" + Pub.GetNickName(strUserID) + "订单失效 " + GoodP.GetGoodType(2) + myModel_tab_Goods.Name + " 引起(客服将在1-2工作日内做退款处理)！" + "\n";
                //strTitle += "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里" + "\n";
                strDescription += "微店团购号：" + intTuanGou_Number + "\n";
                strDescription += "微店团购订单名称：" + myModel_tab_Goods.Name + "\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)) + "\n";
                //strDescription += "请登陆PC端商户后台进行发货处理！" + "\n";
                //strDescription += "用户微店号：" + my_Model_tab_Order.UserID + "。输入" + my_Model_tab_Order.UserID + "#文字内容，可与用户直接对话！" + DateTime.Now + "\n";
                string strClickURL = "";

                strClickURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + intGoodTypeId + "&tuangouidnumber=" + intTuanGou_Number;

                //string strClickURL = "http://" + my_Model_tab_ShopClient.ErJiYuMing + "/product-" + myModel_tab_Goods.ID + ".aspx";
                //ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                //WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(Convert.ToInt32(ShopClientID), "WinXinPayedMoney"))
                {
                    EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        //string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(strUserID), WeiXinTuWens_ArrayList);
                        //string[] strCheckReSendList = { "45015", "45047" };
                        //bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        //if (exists)
                        //{
                        //if (("45015" == strMessageImage))
                        //{
                        if (boolTempletPayMessage)
                        {
                            //SendTempletPayWinXinMessage(Int32 intToUserID, Int32 intShopClientID, string strClickURL, string strTitle, string stringOrderNum, string stringOrderNumTotalMoney, string strremark)

                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Convert.ToInt32(ShopClientID), strClickURL, "(失效通知)" + "用户" + Pub.GetNickName(strUserID) + "团购订单失效", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)), strDescription.Replace("\n", ""));
                        }
                        //}
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "团购");
            }
            finally
            {

            }
        }
        /// <summary>
        /// 团购失效通知
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="intGoodTypeId"></param>
        /// <param name="intTuanGou_Number"></param>
        public static void tell_UserIGetMoneyPayMoney_ByWeiXin_TuanGou_DisEfficacy(String UserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(intGoodTypeId);
                string ShopClientID = my_Model_tab_TuanGou.ShopClientID.ToString();


                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
                my_Model_tab_User = my_BLL_tab_User.GetModel("id='" + UserID + "'");

                //string strTitle = "亲，团购订单失效(客服将在1-2工作日内做退款处理)，金额是：¥" + Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice));
                //ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  

                EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                //string[] strGet_A_GoodIDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(my_Model_tab_TuanGou.SourceGoodID));


                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);

                //EggsoftWX.BLL.tab_Order my_BLL_tab_Order = new EggsoftWX.BLL.tab_Order();
                //EggsoftWX.Model.tab_Order my_Model_tab_Order = my_BLL_tab_Order.GetModel(intOrderNum);
                int intShopClient_ID = Convert.ToInt32(ShopClientID);
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(intShopClient_ID.ToString(), "TempletPayMessage");///是否可以发模板消息

                int intUserID = Convert.ToInt32(UserID);
                int intUserIDShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(intUserID.ToString());
                if (intUserIDShopClientID != intShopClient_ID) return;///越界事情 直接返回


                string strDescription = "亲，我们给你发信，是" + Pub.GetNickName(UserID.ToString()) + "已订单失效 " + GoodP.GetGoodType(2) + "引起！(客服将在1-2工作日内做退款处理)。" + "\n";
                strDescription += "微店团购订单号：" + intTuanGou_Number + "   " + "\n";
                strDescription += DateTime.Now + "  \n";

                int intParentID = 0;
                EggsoftWX.BLL.tab_Orderdetails BLL_tab_Orderdetails = new EggsoftWX.BLL.tab_Orderdetails();
                if (BLL_tab_Orderdetails.Exists("GoodType=2 and GoodTypeId=" + intGoodTypeId + " and GoodTypeIdBuyInfo='" + intTuanGou_Number + "'"))
                {
                    EggsoftWX.Model.tab_Orderdetails Model_tab_Orderdetails = BLL_tab_Orderdetails.GetModel("GoodType=2 and GoodTypeId=" + intGoodTypeId + " and GoodTypeIdBuyInfo='" + intTuanGou_Number + "'");
                    intParentID = ((Model_tab_Orderdetails.ParentID == null) ? 0 : Convert.ToInt32(Model_tab_Orderdetails.ParentID));
                }
                EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + UserID+ " and IsDeleted=0");///有代理啊
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
                EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;

                strErJiYuMing = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + intGoodTypeId + "&tuangouidnumber=" + intTuanGou_Number + "&ParentID=" + strAgent;


                //ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strErJiYuMing);
                //WeiXinTuWens_ArrayList.Add(First);


                //string strMessageImage = Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(UserID), 0, WeiXinTuWens_ArrayList);

                //string[] strCheckReSendList = { "45015", "45047" };
                //bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                //if (exists)
                //{

                if (boolTempletPayMessage)
                {
                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(UserID), Convert.ToInt32(ShopClientID), strErJiYuMing, "(失效通知)" + "用户" + Pub.GetNickName(UserID.ToString()) + "团购订单通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice)), strDescription.Replace("\n", ""));
                }
                //}
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "团购");
            }
            finally
            {

            }
        }
        /// <summary>
        /// 团购失效通知
        /// </summary>
        /// <param name="intOrderNum"></param>
        /// <param name="strUserID"></param>
        /// <param name="intGoodTypeId"></param>
        /// <param name="intTuanGou_Number"></param>
        public static void tell_User_ParentID_IGetMoneyPayMoney_ByWeiXin_TuanGou_DisEfficacy(int intOrderNum, String strUserID, int intGoodTypeId, int intTuanGou_Number)
        {
            try
            {
                EggsoftWX.BLL.tab_TuanGou my_BLL_tab_TuanGou = new EggsoftWX.BLL.tab_TuanGou();
                EggsoftWX.Model.tab_TuanGou my_Model_tab_TuanGou = my_BLL_tab_TuanGou.GetModel(intGoodTypeId);


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

                    Decimal ALLDecimalMoney = decimal.Multiply(Convert.ToDecimal(my_Model_tab_TuanGou.EachPeoplePrice), decimal.Parse(strOrderCount));


                    if (my_Model_tab_TuanGou.TuanZhangBonus_AgentGet == true)
                    { ///团长取得代理商利润

                    }
                    else
                    {
                        #region  原三级分销 代理商失效通知
                        Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                        myModel_MultiFenXiaoLevel.GoodID = strGoodID.toInt32();
                        myModel_MultiFenXiaoLevel.intParentID = strParentID.toInt32();
                        myModel_MultiFenXiaoLevel.strGoodType = "2";
                        myModel_MultiFenXiaoLevel.strGoodTypeId = intGoodTypeId.ToString();
                        myModel_MultiFenXiaoLevel.strGoodTypeIdBuyInfo = strGoodTypeIdBuyInfo;
                        myModel_MultiFenXiaoLevel.UserID = strUserID.toInt32();
                        Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);


                        //int intParentID = 0;
                        //int.TryParse(strParentID, out intParentID);

                        //Decimal AgentGet = 0;
                        //Decimal ManagerAgentGet = 0;
                        //int ManagerAgentParentID = 0;
                        //Decimal ManagerGrandAgentGet = 0;
                        //int ManagerGrandAgentParentID = 0;
                        //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(Int32.Parse(strGoodID), intParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, "2", intGoodTypeId.ToString(), strGoodTypeIdBuyInfo);


                        EggsoftWX.BLL.tab_Goods myBLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                        string[] A_GoodID_From_Order_IDList = GoodP.Get_A_GoodID_From_Order_ID(intOrderNum);
                        EggsoftWX.Model.tab_Goods myModel_tab_Goods = myBLL_tab_Goods.GetModel(Convert.ToInt32(A_GoodID_From_Order_IDList[0]));
                        string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(myModel_tab_Goods.Icon, 640);
                        string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUplaod() + GoodIcon;

                        EggsoftWX.BLL.tab_ShopClient_Agent_ bll_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                        bool boolAgent = bll_tab_ShopClient_Agent_.Exists("UserID=" + strUserID + " and IsDeleted=0 and isnull(Empowered, 0) = 1");///有代理啊
                        string strAgent = "";
                        if (boolAgent)
                        {
                            strAgent = strUserID;
                        }
                        else if (myModel_MultiFenXiaoLevel.intParentID > 0)
                        {
                            strAgent = strParentID;
                        }
                        EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                        EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(Int32.Parse(strUserID));
                        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(Convert.ToInt32(Model_tab_User.ShopClientID));

                        string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;

                        strErJiYuMing = "https://" + strErJiYuMing + "/addfunction/02pingtuan/03goods.html?tuangouid=" + strGoodTypeId + "&tuangouidnumber=" + strGoodTypeIdBuyInfo + "&parentid=" + strAgent;




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
                            string strTitle = "一级代理亲，订单失效通知(客服将在1-2工作日内做退款处理)  " + GoodP.GetGoodType(Int32.Parse(strGoodType)) + "您的原定分享所得是：¥" + Pub.getPubMoney(DecimalMoney);


                            string strDescription = "一级代理亲，我们给你发信，是" + Pub.GetNickName(strUserID.ToString()) + "已团购失效通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，订单成功后您的代理所得才会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "   \n";
                            strDescription += DateTime.Now + "\n";


                            if (boolTempletPayMessage)
                            {

                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(strParentID), Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "(失效通知)" + "用户" + Pub.GetNickName(strUserID) + "团购订单失效通知(客服将在1-2工作日内做退款处理)", my_Model_tab_Order.OrderNum, Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                            }
                        }
                        if ((myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0) && (myModel_MultiFenXiaoLevel.ManagerAgentGet > 0))
                        {
                            Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, myModel_MultiFenXiaoLevel.ManagerAgentGet);
                            DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);


                            string strDescription = "二级代理亲，订单失效通知(客服将在1-2工作日内做退款处理)，是" + Pub.GetNickName(strUserID.ToString()) + "已团购失效通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，订单成功后您的代理所得才会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "    \n";
                            strDescription += DateTime.Now + "\n";

                            if (boolTempletPayMessage)
                            {

                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "(失效通知)" + "用户" + Pub.GetNickName(strUserID.ToString()) + "团购付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                            }
                        }
                        if ((myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID > 0) && (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet > 0))
                        {
                            Decimal DecimalMoney = decimal.Multiply(ALLDecimalMoney, myModel_MultiFenXiaoLevel.ManagerGrandAgentGet);
                            DecimalMoney = decimal.Multiply(DecimalMoney, (decimal)0.01);

                            string strDescription = "三级代理亲，订单失效通知(客服将在1-2工作日内做退款处理)，是" + Pub.GetNickName(strUserID.ToString()) + "已团购失效通知引起！发送给您的朋友或者分享到朋友圈，就可以获得收入了。" + "\n";
                            strDescription += "亲，T+7后，订单成功后您的代理所得才会自动转入您的余额。" + "\n";
                            strDescription += "微店订单号：" + my_Model_tab_Order.OrderNum + "   \n";
                            strDescription += DateTime.Now + "\n";

                            if (boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, Convert.ToInt32(my_Model_tab_Order.ShopClient_ID), strErJiYuMing, "(失效通知)" + "用户" + Pub.GetNickName(strUserID.ToString()) + "团购付款通知", intTuanGou_Number.ToString(), Pub.getPubMoney(Convert.ToDecimal(my_Model_tab_Order.TotalMoney)), strDescription.Replace("\n", ""));
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "团购");
            }
            finally
            {

            }
        }


    }
}