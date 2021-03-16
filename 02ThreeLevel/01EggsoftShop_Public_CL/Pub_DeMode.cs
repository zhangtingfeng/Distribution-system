using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Data;



namespace Eggsoft_Public_CL
{
    /// <summary>
    ///Pub 的摘要说明
    /// </summary>
    public class Pub_DeMode
    {
        public Pub_DeMode()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }




        public static string Get_GuideSubscribePageFromWeiXinD_ShopClientID_()
        {

            string strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = "";

            string CacheKey = "Get_GuideSubscribePageFromWeiXinD_ShopClientID_";
            object objType = Eggsoft.Common.DataCache.GetCache(CacheKey);
            if (objType == null)
            {
                try
                {
                    string strShopClientID = Eggsoft_Public_CL.Pub_Agent.GetShopClientID_ErJiYuMing();

                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + strShopClientID);

                    strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = Model_tab_ShopClient_EngineerMode.GuideSubscribePageFromWeiXinD;
                }
                catch (Exception Exceptione)
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                }
                finally
                {

                }
            }
            else
            {
                strGet_GuideSubscribePageFromWeiXinD_ShopClientID_ = (string)objType;
            }

            return strGet_GuideSubscribePageFromWeiXinD_ShopClientID_;
        }

        public static int Get_ShopClientID_From_UserID_(int intUserID)
        {

            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel(intUserID);
            if (Model_tab_User != null)
            {
                return Convert.ToInt32(Model_tab_User.ShopClientID);
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 微现场
        /// </summary>
        /// <returns></returns>
        public static String doWeiXinResponseText(string strOpenID, int intintShopClientID)
        {
            string strdoShakeToday = "";

            EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong BLL_tab_ShopClient_XianChangHuoDong = new EggsoftWX.BLL.tab_ShopClient_XianChangHuoDong();


            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel("OpenID='" + strOpenID + "'");
            if (Model_tab_User != null)
            {
                String strWhereActivityState = "ShopClientID=" + intintShopClientID + " and ActivityState=1";
                bool boolActivityState = BLL_tab_ShopClient_XianChangHuoDong.Exists(strWhereActivityState);

                if (boolActivityState)
                {
                    EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intintShopClientID);

                    string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                    //strErJiYuMing = "http://" + strErJiYuMing + strAgent + "/product-" + myModel_tab_Goods.ID + ".aspx";
                    string strURL = "https://" + strErJiYuMing + "/addfunction/01shake_parter/01shake_parter.aspx";




                    EggsoftWX.Model.tab_ShopClient_XianChangHuoDong Model_tab_ShopClient_XianChangHuoDong = BLL_tab_ShopClient_XianChangHuoDong.GetModel(strWhereActivityState);
                    string strdoTextToday = Model_tab_ShopClient_XianChangHuoDong.ActivityName + "的微现场活动正在进行,赶快点击这里";
                    strdoShakeToday += "<a href=\"" + strURL + "\">" + strdoTextToday + "</a>";

                }
                else
                {
                    strdoShakeToday += "暂无活动,请听从现场协调";
                }
            }
            return strdoShakeToday;
        }

        /// <summary>
        /// 今日签到  
        /// </summary>
        /// <returns></returns>
        public static String doSignWorkingToday(string strOpenID, int intintShopClientID)
        {
            string strdoSignWorkingToday = "";

            EggsoftWX.BLL.tab_ShopClient_SignWorkingEveryDay BLL_tab_ShopClient_SignWorkingEveryDay = new EggsoftWX.BLL.tab_ShopClient_SignWorkingEveryDay();


            EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
            EggsoftWX.Model.tab_User Model_tab_User = BLL_tab_User.GetModel("OpenID='" + strOpenID + "'");
            if (Model_tab_User != null)
            {
                bool boolSignedToday = BLL_tab_ShopClient_SignWorkingEveryDay.Exists("UserID=" + Model_tab_User.ID + " and DateDiff(dd,SignTime,getdate())=0");

                if (boolSignedToday)
                {
                    strdoSignWorkingToday = "你今天已经签到过了";
                }
                else
                {
                    string strLock = "Lock201602140418" + intintShopClientID.ToString() + Model_tab_User.ID.ToString();
                    lock (strLock)
                    {
                        EggsoftWX.Model.tab_ShopClient_SignWorkingEveryDay Model_tab_ShopClient_SignWorkingEveryDay = new EggsoftWX.Model.tab_ShopClient_SignWorkingEveryDay();

                        Model_tab_ShopClient_SignWorkingEveryDay.ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(Model_tab_User.ID.ToString());

                        string strSignWorkingEveryDay_Money = Eggsoft_Public_CL.Pub.stringShowPower(intintShopClientID.ToString(), "SignWorkingEveryDay_Money");///
                        string strSignWorkingEveryDay_GouWuQuan = Eggsoft_Public_CL.Pub.stringShowPower(intintShopClientID.ToString(), "SignWorkingEveryDay_GouWuQuan");///
                        Decimal DecimalSignWorkingEveryDay_Money = 0;
                        Decimal DecimalSignWorkingEveryDay_GouWuQuan = 0;

                        Decimal.TryParse(strSignWorkingEveryDay_Money, out DecimalSignWorkingEveryDay_Money);
                        Decimal.TryParse(strSignWorkingEveryDay_GouWuQuan, out DecimalSignWorkingEveryDay_GouWuQuan);
                        if (DecimalSignWorkingEveryDay_Money > 0)
                        {
                            Model_tab_ShopClient_SignWorkingEveryDay.SendMoney = DecimalSignWorkingEveryDay_Money;

                            ///每日签到一个钱
                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = DecimalSignWorkingEveryDay_Money;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "每日签到赠送现金" + DecimalSignWorkingEveryDay_Money + "元";
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 44;
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intintShopClientID;
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
                            strdoSignWorkingToday += "现金增加" + DecimalSignWorkingEveryDay_Money + "元";
                        }
                        if (DecimalSignWorkingEveryDay_GouWuQuan > 0)
                        {
                            Model_tab_ShopClient_SignWorkingEveryDay.SendGouWuQuan = DecimalSignWorkingEveryDay_GouWuQuan;

                            ///赠送购物券
                            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = DecimalSignWorkingEveryDay_GouWuQuan;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "每日签到赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intintShopClientID.ToString()) + DecimalSignWorkingEveryDay_GouWuQuan + "元";// Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享商品" + my_Model_tab_Goods.Name + "送";
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Model_tab_User.ID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intintShopClientID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                            int intTableID=BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

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

                            strdoSignWorkingToday += Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(intintShopClientID.ToString()) + "增加" + DecimalSignWorkingEveryDay_GouWuQuan + "元";
                        }
                        EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(intintShopClientID);

                        string strErJiYuMing = Model_tab_ShopClient.ErJiYuMing;
                        //strErJiYuMing = "http://" + strErJiYuMing + strAgent + "/product-" + myModel_tab_Goods.ID + ".aspx";
                        string strURL = "https://" + strErJiYuMing + "/mywebuy.aspx";


                        strdoSignWorkingToday += ",<a href=\"" + strURL + "\">点击进入会员中心</a>";


                        Model_tab_ShopClient_SignWorkingEveryDay.SignTime = DateTime.Now;
                        Model_tab_ShopClient_SignWorkingEveryDay.NickName = Model_tab_User.NickName;
                        Model_tab_ShopClient_SignWorkingEveryDay.ShopUserID = Model_tab_User.ShopUserID;
                        Model_tab_ShopClient_SignWorkingEveryDay.UserID = Model_tab_User.ID;

                        BLL_tab_ShopClient_SignWorkingEveryDay.Add(Model_tab_ShopClient_SignWorkingEveryDay);
                    }




                }
            }




            return strdoSignWorkingToday;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strXML"></param>
        /// <param name="strType"></param>
        /// <param name="strResourceID"></param>
        /// <param name="IfClickEvent">click 事件  不做多客服消息转发处理</param>
        /// <param name="intShopClientID"></param>
        /// <returns></returns>
        public static String Get_Message_(string strXML, string strType, string strResourceID, bool IfClickEvent, int intShopClientID = 0)
        {
            WX_Model.WX_Model_EventKey myWX_Model = new WX_Model.WX_Model_EventKey();
            myWX_Model = myWX_Model.GetWX_Model_EventKey(strXML);
            String strResponseText = "";

            Eggsoft.Common.debug_Log.Call_WriteLog("strXML=  strType=" + strXML + "  ," + strType);

            if (strType == "31")//如果公众号处于开发模式，需要在接收到用户发送的消息时，返回一个MsgType为transfer_customer_service的消息，微信服务器在收到这条消息时，会把这次发送的消息转到多客服系统。用户被客服接入以后，客服关闭会话以前，处于会话过程中，用户发送的消息均会被直接转发至客服系统。
            {

                strResponseText += "<xml>";
                strResponseText += "<ToUserName><![CDATA[" + myWX_Model.FromUserName + "]]></ToUserName>";
                strResponseText += "<FromUserName><![CDATA[" + myWX_Model.ToUserName + "]]></FromUserName>";
                strResponseText += "<CreateTime>" + Pub_DeMode.ConvertDateTimeInt(System.DateTime.Now) + "</CreateTime>";

                if ((IfClickEvent == false) && Eggsoft_Public_CL.Pub.boolShowPower(intShopClientID.ToString(), "weixinMultiDuoKeFu"))///微信多客服
                {
                    strResponseText += "<MsgType><![CDATA[transfer_customer_service]]></MsgType>";
                }
                else
                {
                    strResponseText += "<MsgType><![CDATA[text]]></MsgType>";
                }
                strResponseText += "<Content><![CDATA[免费代理微店，0投资0风险，不用囤货，不用发货，真正一键微店，公司帮你一切搞定。]]></Content>";
                strResponseText += "</xml>";


            }
            else if (strType == "1")
            {

                string strDescription = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource().GetList("Text", "ID=" + strResourceID).Tables[0].Rows[0][0].ToString();
                strDescription = HttpContext.Current.Server.HtmlDecode(strDescription);

                strResponseText += "<xml>";
                strResponseText += "<ToUserName><![CDATA[" + myWX_Model.FromUserName + "]]></ToUserName>";
                strResponseText += "<FromUserName><![CDATA[" + myWX_Model.ToUserName + "]]></FromUserName>";
                strResponseText += "<CreateTime>" + Pub_DeMode.ConvertDateTimeInt(System.DateTime.Now) + "</CreateTime>";

                if (strDescription.IndexOf("{####QianDaoEveryDay####}") != -1)
                {
                    strResponseText += "<MsgType><![CDATA[text]]></MsgType>";
                    string strdoSignWorkingToday = Eggsoft_Public_CL.Pub_DeMode.doSignWorkingToday(myWX_Model.FromUserName, intShopClientID);
                    strDescription = strDescription.Replace("{####QianDaoEveryDay####}", strdoSignWorkingToday);
                    strResponseText += "<Content><![CDATA[" + strDescription + "]]></Content>";
                }
                else if (strDescription.IndexOf("{####WeiXianChangShake####}") != -1)
                {
                    strResponseText += "<MsgType><![CDATA[text]]></MsgType>";
                    string strdoXianChangHuoDong = Eggsoft_Public_CL.Pub_DeMode.doWeiXinResponseText(myWX_Model.FromUserName, intShopClientID);
                    strDescription = strDescription.Replace("{####WeiXianChangShake####}", strdoXianChangHuoDong);
                    strResponseText += "<Content><![CDATA[" + strDescription + "]]></Content>";
                }
                else if ((IfClickEvent == false) && Eggsoft_Public_CL.Pub.boolShowPower(intShopClientID.ToString(), "weixinMultiDuoKeFu"))///微信多客服{
                {
                    strResponseText += "<MsgType><![CDATA[transfer_customer_service]]></MsgType>";
                    strResponseText += "<Content><![CDATA[" + strDescription + "]]></Content>";
                }
                else
                {
                    strResponseText += "<MsgType><![CDATA[text]]></MsgType>";
                    strResponseText += "<Content><![CDATA[" + strDescription + "]]></Content>";
                }
                ///
                strResponseText += "</xml>";

                Eggsoft.Common.debug_Log.Call_WriteLog("strDescription=" + strDescription);

            }
            else if (strType == "2")////
            {


                String strTextContent = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource().GetList("Text", "ID=" + strResourceID).Tables[0].Rows[0][0].ToString();
                Eggsoft.Common.debug_Log.Call_WriteLog("strTextContent=" + strTextContent);

                String strTextAll = HttpContext.Current.Server.HtmlDecode(strTextContent);
                Eggsoft.Common.debug_Log.Call_WriteLog("strTextAll=" + strTextAll);

                string[] strTitleAndContentList = strTextAll.Split(new char[5] { '#', '@', '#', '$', '#' }, StringSplitOptions.RemoveEmptyEntries);

                EggsoftWX.Model.tab_ShopClient_System_XML_Resource mytab_ShopClient_System_XML_Resource = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource().GetModel(Convert.ToInt32(strResourceID));

                strResponseText = "<xml>";
                strResponseText = strResponseText + "<ToUserName><![CDATA[" + myWX_Model.FromUserName + "]]></ToUserName>";
                strResponseText = strResponseText + "<FromUserName><![CDATA[" + myWX_Model.ToUserName + "]]></FromUserName>";
                strResponseText = strResponseText + "<CreateTime>" + Pub_DeMode.ConvertDateTimeInt(System.DateTime.Now) + "</CreateTime>";
                if ((IfClickEvent == false) && Eggsoft_Public_CL.Pub.boolShowPower(intShopClientID.ToString(), "weixinMultiDuoKeFu"))///微信多客服
                {
                    strResponseText += "<MsgType><![CDATA[transfer_customer_service]]></MsgType>";
                }
                else
                {
                    strResponseText = strResponseText + "<MsgType><![CDATA[news]]></MsgType>";
                }
                strResponseText = strResponseText + "<ArticleCount>1</ArticleCount>";
                strResponseText = strResponseText + "<Articles>";

                strResponseText = strResponseText + "<item>";   ///+ "mytab_ShopClient_System_XML_Resource.LinkURL.Length="
                strResponseText = strResponseText + "<Title><![CDATA[" + HttpContext.Current.Server.HtmlDecode(strTitleAndContentList[0]) + "]]></Title> ";
                strResponseText = strResponseText + "<Description><![CDATA[" + HttpContext.Current.Server.HtmlDecode(strTitleAndContentList[1]) + "]]></Description>";
                strResponseText = strResponseText + "<PicUrl><![CDATA[" + mytab_ShopClient_System_XML_Resource.Pic + "]]></PicUrl>";
                if (mytab_ShopClient_System_XML_Resource.LinkURL.Length > 0)
                {
                    strResponseText = strResponseText + "<Url><![CDATA[" + mytab_ShopClient_System_XML_Resource.LinkURL + "]]></Url>";
                }
                else
                {
                    strResponseText = strResponseText + "<Url><![CDATA[#]]></Url>";
                }

                strResponseText = strResponseText + "</item>";

                strResponseText = strResponseText + "</Articles>";
                strResponseText = strResponseText + "</xml> ";


                //WXdbMsghelp.addUser_Chat_InfoToMDF(Int32.Parse(Pub.GetUserIDFromOpenID(myWX_Model.ToUserName)), 0, "平台回复", "news", strResponseText, myWX_Model.FromUserName, myWX_Model.ToUserName);

            }
            else if (strType == "3")
            {
                List<EachTuWen> argEachTuWenList = new List<EachTuWen>();

                int intcountHowManyTuWen = countHowManyTuWen(strResourceID, out argEachTuWenList);

                strResponseText = "<xml>";
                strResponseText = strResponseText + "<ToUserName><![CDATA[" + myWX_Model.FromUserName + "]]></ToUserName>";
                strResponseText = strResponseText + "<FromUserName><![CDATA[" + myWX_Model.ToUserName + "]]></FromUserName>";
                strResponseText = strResponseText + "<CreateTime>" + Pub_DeMode.ConvertDateTimeInt(System.DateTime.Now) + "</CreateTime>";
                if ((IfClickEvent == false) && Eggsoft_Public_CL.Pub.boolShowPower(intShopClientID.ToString(), "weixinMultiDuoKeFu"))///微信多客服
                {
                    strResponseText += "<MsgType><![CDATA[transfer_customer_service]]></MsgType>";
                }
                else
                {
                    strResponseText = strResponseText + "<MsgType><![CDATA[news]]></MsgType>";
                }
                strResponseText = strResponseText + "<ArticleCount>" + intcountHowManyTuWen + "</ArticleCount>";
                strResponseText = strResponseText + "<Articles>";



                for (int i = 0; i < intcountHowManyTuWen; i++)
                {
                    strResponseText = strResponseText + "<item>";
                    strResponseText = strResponseText + "<Title><![CDATA[" + argEachTuWenList[i].Title + "]]></Title> ";
                    //strResponseText = strResponseText + "<Description><![CDATA[" + "9999999999" + "]]></Description>";
                    strResponseText = strResponseText + "<PicUrl><![CDATA[" + argEachTuWenList[i].PicURL + "]]></PicUrl>";
                    strResponseText = strResponseText + "<Url><![CDATA[" + argEachTuWenList[i].LinkURL + "]]></Url>";
                    strResponseText = strResponseText + "</item>";
                }




                strResponseText = strResponseText + "</Articles>";
                strResponseText = strResponseText + "</xml> ";



            }
            

            return strResponseText;
        }


        public class EachTuWen
        {
            public string Title;
            public string PicURL;
            public string LinkURL;

            public EachTuWen(string n, string c, string y)
            {
                Title = n;
                PicURL = c;
                LinkURL = y;
            }
        }

        public static int countHowManyTuWen(string strResourceID, out List<EachTuWen> argEachTuWenList, int shopClientID = 0)
        {
            int intall = 1;//当前的是一个；

            System.Data.DataRow myDataTable = null;

            EggsoftWX.BLL.tab_ShopClient_System_XML_Resource my_tab_ShopClient_System_XML_Resource = new EggsoftWX.BLL.tab_ShopClient_System_XML_Resource();
            myDataTable = my_tab_ShopClient_System_XML_Resource.GetList("Text,Pic,LinkURL", "ID=" + strResourceID).Tables[0].Rows[0];
            bool boolThisCurID = true;


            argEachTuWenList = new List<EachTuWen>();
            argEachTuWenList.Add(new EachTuWen(myDataTable[0].ToString(), myDataTable[1].ToString(), myDataTable[2].ToString()));
            //out参数规定，参数在方法体内必须被初始化。


            while (boolThisCurID)
            {
                boolThisCurID = my_tab_ShopClient_System_XML_Resource.Exists("ParentID=" + strResourceID);
                if (boolThisCurID)
                {
                    myDataTable = my_tab_ShopClient_System_XML_Resource.GetList("Text,Pic,LinkURL", "ParentID=" + strResourceID).Tables[0].Rows[0];
                    argEachTuWenList.Add(new EachTuWen(myDataTable[0].ToString(), myDataTable[1].ToString(), myDataTable[2].ToString()));
                    intall++;
                    strResourceID = my_tab_ShopClient_System_XML_Resource.GetList("ID", "ParentID=" + strResourceID).Tables[0].Rows[0][0].ToString();

                }

            }



            return intall;
        }


        /// <summary>
        /// boolDoNewGetACCESS_TOKEN  是否强制刷新
        /// </summary>
        /// <param name="intShopClientID"></param>
        /// <param name="boolDoNewGetACCESS_TOKEN"></param>
        /// <returns></returns>


        public static String Button_MakeMenu_Get_ACCESS_TOKEN(int intShopClientID, bool boolDoNewGetACCESS_TOKEN = false)
        {
            string strACCESS_TOKEN = "";
            if (intShopClientID == 0) return "";
            try
            {            
                DateTime oldDateTime, NewDateTime = DateTime.Now;    
                strACCESS_TOKEN = Marker_tab_System_WeiXin.ReadTextNeedTime("token", intShopClientID, out oldDateTime);
                //access_token是公众号的全局唯一票据，公众号调用各接口时都需使用access_token。正常情况下access_token有效期为7200秒，重复获取将导致上次获取的access_token失效。
                TimeSpan ts = NewDateTime - oldDateTime;
                if (boolDoNewGetACCESS_TOKEN) { strACCESS_TOKEN = ""; }//强制刷新
                if ((ts.TotalSeconds > 7200) || strACCESS_TOKEN == "")  //没读到
                {                  
                    String strURL = "";
                    string strWeiXinAppId = "";
                    string strWeiXinAppSecret = "";

                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                    Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);

                    if (Model_tab_ShopClient_EngineerMode != null)
                    {
                        strWeiXinAppId = Model_tab_ShopClient_EngineerMode.WeiXinAppId;
                        strWeiXinAppSecret = Model_tab_ShopClient_EngineerMode.WeiXinAppSecret;
                    }

                    //Eggsoft.Common.debug_Log.Call_WriteLog("Button_MakeMenu_Get_ACCESS_TOKEN3=");


                    strURL = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + strWeiXinAppId + "&secret=" + strWeiXinAppSecret;


                    HttpWebRequest webRequest2 = (HttpWebRequest)WebRequest.Create(strURL);
                    webRequest2.ContentType = "text/html; charset=UTF-8";
                    webRequest2.Method = "GET";
                    webRequest2.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                    webRequest2.ContentType = "application/x-www-form-urlencoded";
                    HttpWebResponse response2 = (HttpWebResponse)webRequest2.GetResponse();

                    //Eggsoft.Common.debug_Log.Call_WriteLog("Button_MakeMenu_Get_ACCESS_TOKEN4=");

                    StreamReader sr2 = new StreamReader(response2.GetResponseStream(), Encoding.Default);
                    string text2 = sr2.ReadToEnd();
                    sr2.Close();

                    if (string.IsNullOrEmpty(text2)) Eggsoft.Common.debug_Log.Call_WriteLog("Button_MakeMenu_Get_ACCESS_TOKEN=" + intShopClientID, "Get_ACCESS_TOKEN", "程序报错");

                    //在这里对Access_token 赋值  
                    Access_token token = new Access_token();
                    token = JsonHelper.JsonDeserialize<Access_token>(text2);
                    // Eggsoft.Common.debug_Log.Call_WriteLog("Button_MakeMenu_Get_ACCESS_TOKEN5=");

                    if (token != null)
                    {
                        strACCESS_TOKEN = token.access_token;
                     
                        if (Model_tab_ShopClient_EngineerMode != null)
                        {
                            Model_tab_ShopClient_EngineerMode.ACCESS_TOKEN_WeiXin = strACCESS_TOKEN;
                            Model_tab_ShopClient_EngineerMode.Get_ACCESS_TOKEN_Time_WeiXin = DateTime.Now;
                            BLL_tab_ShopClient_EngineerMode.Update(Model_tab_ShopClient_EngineerMode);
                        }
                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "Get_ACCESS_TOKEN");
            }
            finally
            {

            }
            return strACCESS_TOKEN;
        }



        public static String Button_MakeMenu_Get_GetTicket(int intShopClientID, bool boolDoNewGetGetTicket = false)
        {
            //string strCheck_SocialPlatform = Pub_SocialPlatform.Check_SocialPlatform();

            DateTime oldDateTime, NewDateTime = DateTime.Now;

            string strGetTicket = Marker_tab_System_WeiXin.ReadTextNeedTime("ticket", intShopClientID, out oldDateTime);

            TimeSpan ts = NewDateTime - oldDateTime;
            if (boolDoNewGetGetTicket) { strGetTicket = ""; }//强制刷新
            if ((ts.TotalSeconds > 7200) || strGetTicket == "")  //没读到
            {
                String strURL = "";


                string Button_MakeMenu_Get_ACCESS_TOKEN = Pub_DeMode.Button_MakeMenu_Get_ACCESS_TOKEN(intShopClientID, boolDoNewGetGetTicket);

                strURL = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + Button_MakeMenu_Get_ACCESS_TOKEN + "&type=jsapi";


                HttpWebRequest webRequest2 = (HttpWebRequest)WebRequest.Create(strURL);
                webRequest2.ContentType = "text/html; charset=UTF-8";
                webRequest2.Method = "GET";
                webRequest2.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:2.0.1) Gecko/20100101 Firefox/4.0.1";
                webRequest2.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse response2 = (HttpWebResponse)webRequest2.GetResponse();


                StreamReader sr2 = new StreamReader(response2.GetResponseStream(), Encoding.Default);
                string text2 = sr2.ReadToEnd();
                sr2.Close();

                //在这里对Access_token 赋值  
                GetTicket GetTicket = new GetTicket();
                GetTicket = JsonHelper.JsonDeserialize<GetTicket>(text2);


                strGetTicket = GetTicket.ticket;
                if (strGetTicket != null)
                {

                    EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();
                    EggsoftWX.Model.tab_ShopClient_EngineerMode Model_tab_ShopClient_EngineerMode = new EggsoftWX.Model.tab_ShopClient_EngineerMode();
                    Model_tab_ShopClient_EngineerMode = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);

                    if (Model_tab_ShopClient_EngineerMode != null)
                    {
                        Model_tab_ShopClient_EngineerMode.GetTicket_WeiXin = strGetTicket;
                        Model_tab_ShopClient_EngineerMode.Get_Ticket_Time_WeiXin = DateTime.Now;
                        BLL_tab_ShopClient_EngineerMode.Update(Model_tab_ShopClient_EngineerMode);
                    }



                    //Marker_tab_System_WeiXin.setUpdateText("GetTicket" + "_" + strCheck_SocialPlatform, strGetTicket);
                }
            }
            return strGetTicket;
        }


        public static string HttpWebRequest_WebRequest_Post_JSON(string strURL, string strJSON)
        {
            WebRequest httpWebRequest = WebRequest.Create(strURL);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());

            streamWriter.Write(strJSON);
            streamWriter.Flush();
            streamWriter.Close();

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());
            string resultstring = streamReader.ReadToEnd();
            streamReader.Close();
            //Eggsoft.Common.JsUtil.ShowMsg(resultstring);
            return resultstring;

        }

        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

    }

}
