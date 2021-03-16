using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doGuidePage1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doGuidePage1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string doGuidePageAction()
        {
            var response = HttpContext.Current.Response;
            var context = HttpContext.Current.Request;
            string str = "";

            try
            {
                string strThisShowID = context.QueryString["strThisShowID"];
                int pInt_ThisShowID = 0;
                int.TryParse(strThisShowID, out pInt_ThisShowID);

                string strpInt_QueryString_ParentID = context.QueryString["strpInt_QueryString_ParentID"];
                int pInt_QueryString_ParentID = 0;
                int.TryParse(strpInt_QueryString_ParentID, out pInt_QueryString_ParentID);

                string strpub_Int_Session_CurUserID = context.QueryString["strpub_Int_Session_CurUserID"];
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);

                string strpub_Int_ShopClientID = context.QueryString["strpub_Int_ShopClientID"];
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_Int_ShopClientID, out pub_Int_ShopClientID);

                int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpub_Int_Session_CurUserID);
                int intParentID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpInt_QueryString_ParentID);///保证同源

                if((pInt_QueryString_ParentID == 0) || ((intUserID_ShopClientID == pub_Int_ShopClientID) && (intParentID_ShopClientID == pub_Int_ShopClientID)))///保证同源
                {
                    sendSNSToShopClientWeiXin(pInt_ThisShowID, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToShopCliento2oWeiXin(pInt_ThisShowID, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToMyParentBonus_WeiXin(pInt_ThisShowID, pub_Int_ShopClientID, pub_Int_Session_CurUserID, pInt_QueryString_ParentID);
                    Write_This_Record(pInt_ThisShowID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);


                    str = "{\"ErrorCode\":\"" + 1 + "\"}";
                    if(HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                    {
                        HttpRequest Request = HttpContext.Current.Request;
                        HttpResponse Response = HttpContext.Current.Response;
                        string callback = Request["jsonp"];
                        Response.Write(callback + "(" + str + ")");
                        Response.End();//结束后续的操作，直接返回所需要的字符串
                    }


                }
                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strpub_Int_Session_CurUserID=" + strpub_Int_Session_CurUserID + ",strpub_Int_ShopClientID=" + strpub_Int_ShopClientID + ",strpInt_QueryString_ParentID=" + strpInt_QueryString_ParentID, "临界区突破doGuidePage");

                    str = "";
                }
            }
            catch(System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
            return str;
        }


        private void sendSNSToMyParentBonus_WeiXin(int pInt_ThisShowID, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID, int pInt_QueryString_ParentID)
        {
            try
            {
                if(Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
                {
                    if(pub_Int_Session_CurUserID == pInt_QueryString_ParentID) pInt_QueryString_ParentID = 0;
                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息


                    if((pInt_QueryString_ParentID != 0) && (Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(pInt_QueryString_ParentID) > 0) && pub_Int_Session_CurUserID != pInt_QueryString_ParentID)//处理父亲的消息
                    {
                        EggsoftWX.BLL.tab_ShopClient_GuidePages INC_User_Menu_bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
                        EggsoftWX.Model.tab_ShopClient_GuidePages INC_User_Menu_Model = new EggsoftWX.Model.tab_ShopClient_GuidePages();
                        INC_User_Menu_Model = INC_User_Menu_bll.GetModel(pInt_ThisShowID);
                        String pub_str_FirstImageFull = Eggsoft.Common.Image.GetFirstHtmlImageUrl(INC_User_Menu_Model.MenuText);
                        String pub_strMenuContent = Server.HtmlDecode(INC_User_Menu_Model.MenuText);///供微信分析使用
                        String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));

                        EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                        EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                        my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                        string pub_strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/guidepage-" + pInt_ThisShowID + ".aspx";

                        //实例化几个WeiXinTuWen类对象  
                        string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                        strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                        string strTitle = "代理亲," + strUserNickName + "浏览" + Eggsoft.Common.CommUtil.getShortText(INC_User_Menu_Model.MenuName, 80);


                        System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                        Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, pub_str_FirstImageFull, Eggsoft.Common.CommUtil.getShortText(pub_strDESFull, 80), pub_strURL);
                        WeiXinTuWens_ArrayList.Add(First);

                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pInt_QueryString_ParentID, 0, WeiXinTuWens_ArrayList);

                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if(exists)
                        {
                            if(boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_ParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }
                    }
                }
            }
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }
        private void sendSNSToShopClientWeiXin(int pInt_ThisShowID, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_ShopClient_GuidePages INC_User_Menu_bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
                EggsoftWX.Model.tab_ShopClient_GuidePages INC_User_Menu_Model = new EggsoftWX.Model.tab_ShopClient_GuidePages();
                INC_User_Menu_Model = INC_User_Menu_bll.GetModel(pInt_ThisShowID);
                String pub_str_FirstImageFull = Eggsoft.Common.Image.GetFirstHtmlImageUrl(INC_User_Menu_Model.MenuText);
                String pub_strMenuContent = Server.HtmlDecode(INC_User_Menu_Model.MenuText);///供微信分析使用
                String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                string pub_strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/guidepage-" + pInt_ThisShowID + ".aspx";

                // string pub_strURL = Eggsoft.Common.Application.AppUrl + "/guidepage-" + pInt_ThisShowID + ".aspx";



                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                string strTitle = "店主亲," + strUserNickName + "浏览" + Eggsoft.Common.CommUtil.getShortText(INC_User_Menu_Model.MenuName, 80);

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, pub_str_FirstImageFull, Eggsoft.Common.CommUtil.getShortText(pub_strDESFull, 80), pub_strURL);
                WeiXinTuWens_ArrayList.Add(First);


                if(Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
                {
                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                    for(int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), 0, WeiXinTuWens_ArrayList);

                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if(exists)
                        {
                            if(boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }

                    }
                }
            }
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
        }

        private void sendSNSToShopCliento2oWeiXin(int pInt_ThisShowID, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + pub_Int_ShopClientID);
                if(boolExsit == false) return;

                EggsoftWX.BLL.tab_ShopClient_GuidePages INC_User_Menu_bll = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
                EggsoftWX.Model.tab_ShopClient_GuidePages INC_User_Menu_Model = new EggsoftWX.Model.tab_ShopClient_GuidePages();
                INC_User_Menu_Model = INC_User_Menu_bll.GetModel(pInt_ThisShowID);
                String pub_str_FirstImageFull = Eggsoft.Common.Image.GetFirstHtmlImageUrl(INC_User_Menu_Model.MenuText);
                String pub_strMenuContent = Server.HtmlDecode(INC_User_Menu_Model.MenuText);///供微信分析使用
                String pub_strDESFull = Eggsoft.Common.CommUtil.GetMainContent(Eggsoft.Common.CommUtil.NoHTML(pub_strMenuContent));

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                string pub_strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/guidepage-" + pInt_ThisShowID + ".aspx";


                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(pub_Int_Session_CurUserID, out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);
                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);
                string strTitle = Model_tab_ShopClient_O2O_ShopInfo.ShopName + "o2o店主亲," + strUserNickName + "浏览" + Eggsoft.Common.CommUtil.getShortText(INC_User_Menu_Model.MenuName, 80);
                strTitle += "," + "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里";

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, pub_str_FirstImageFull, Eggsoft.Common.CommUtil.getShortText(pub_strDESFull, 80), pub_strURL);
                WeiXinTuWens_ArrayList.Add(First);


                if(Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "o2oLookGoods"))
                {
                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息


                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for(int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), 0, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if(exists)
                        {
                            if(boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }
                    }
                }
            }
            catch(System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "给店主发送o2o消息", "线程异常");
            }
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog("int pInt_ThisShowID=" + pInt_ThisShowID + ", int pub_Int_ShopClientID=" + pub_Int_ShopClientID + ", int pub_Int_Session_CurUserID=" + pub_Int_Session_CurUserID + "", "给店主发送o2o消息", "程序报错");
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "给店主发送o2o消息", "程序报错");
            }
            finally
            {

            }
        }

        /// <summary>
        /// 每个有效转发咨询奖励现金
        /// </summary>
        /// <param name="pInt_ThisShowID"></param>
        /// <param name="intParentID"></param>
        /// <param name="intUserID"></param>
        /// <param name="pub_Int_ShopClientID"></param>
        private void Write_This_Record(int pInt_ThisShowID, int intParentID, int intUserID, int pub_Int_ShopClientID)
        {
            bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息
            try
            {
                if(intParentID == intUserID) intParentID = 0;///是否浏览自己的网页  在自己的店里买东西的问题啊

                EggsoftWX.BLL.tab_User_GuidePages_History my_BLL_tab_User_GuidePages_History = new EggsoftWX.BLL.tab_User_GuidePages_History();
                EggsoftWX.Model.tab_User_GuidePages_History my_Model_tab_User_GuidePages_History = new EggsoftWX.Model.tab_User_GuidePages_History();

                EggsoftWX.BLL.tab_ShopClient_GuidePages my_BLL_tab_ShopClient_GuidePages = new EggsoftWX.BLL.tab_ShopClient_GuidePages();
                EggsoftWX.Model.tab_ShopClient_GuidePages my_Model_tab_ShopClient_GuidePages = new EggsoftWX.Model.tab_ShopClient_GuidePages();
                my_Model_tab_ShopClient_GuidePages = my_BLL_tab_ShopClient_GuidePages.GetModel(pInt_ThisShowID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(intUserID);

                string strmywebuyURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/mywebuy.aspx";



                String strmy_BLL_tab_User_GuidePages_History = "UserID=" + intUserID + " and GuidePagesID=" + pInt_ThisShowID + "" + " and Parent_UserID=" + intParentID;
                if(my_BLL_tab_User_GuidePages_History.Exists(strmy_BLL_tab_User_GuidePages_History))//重复访问  
                {
                    my_Model_tab_User_GuidePages_History = my_BLL_tab_User_GuidePages_History.GetModel(strmy_BLL_tab_User_GuidePages_History);
                    my_Model_tab_User_GuidePages_History.Count_Visit = my_Model_tab_User_GuidePages_History.Count_Visit + 1;
                    my_Model_tab_User_GuidePages_History.UpdateTime = DateTime.Now;
                    my_BLL_tab_User_GuidePages_History.Update(my_Model_tab_User_GuidePages_History);
                }
                else
                {


                    //写入分享数据
                    my_Model_tab_User_GuidePages_History.UserID = intUserID;
                    my_Model_tab_User_GuidePages_History.Parent_UserID = intParentID;

                    my_Model_tab_User_GuidePages_History.GuidePagesID = pInt_ThisShowID;
                    my_Model_tab_User_GuidePages_History.UpdateTime = DateTime.Now;
                    my_Model_tab_User_GuidePages_History.Count_Visit = 1;
                    my_Model_tab_User_GuidePages_History.Type_Visit = "Visit";
                    my_BLL_tab_User_GuidePages_History.Add(my_Model_tab_User_GuidePages_History);

                    if((intParentID > 0))
                    {
                        string strBonusMoney_ShareGuidePages = Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.ToString(), "BonusMoney_ShareGuidePages");//////每个有效转发咨询奖励现金
                        string strBonusGouWuQuan_ShareGuidePages = Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.ToString(), "BonusGouWuQuan_ShareGuidePages");///每个有效转发咨询奖励购物券



                        if(strBonusMoney_ShareGuidePages.toDecimal() > 0)
                        {
                            ///赠送一个钱
                            EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                            Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 50;//50表示咨询访问收入
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = strBonusMoney_ShareGuidePages.toDecimal();
                            Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享咨询" + my_Model_tab_ShopClient_GuidePages.MenuName + "送";
                            Model_tab_TotalCredits_Consume_Or_Recharge.UserID = intParentID;
                            Model_tab_TotalCredits_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                            Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
                            int intTableID = BLL_tab_TotalCredits_Consume_Or_Recharge.Add(Model_tab_TotalCredits_Consume_Or_Recharge);

                            #region 增加账户余额未处理信息
                            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
                            EggsoftWX.Model.b011_InfoAlertMessage Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                            Model_b011_InfoAlertMessage.InfoTip = Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                            Model_b011_InfoAlertMessage.CreateBy = "分享咨询";
                            Model_b011_InfoAlertMessage.UpdateBy = "分享咨询";
                            Model_b011_InfoAlertMessage.UserID = Model_tab_TotalCredits_Consume_Or_Recharge.UserID;
                            Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID;
                            Model_b011_InfoAlertMessage.Type = "Info_ZhangHuYuE";
                            Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                            bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                            #endregion 增加账户余额未处理信息 

                            Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送现金通知", "", Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享咨询" + my_Model_tab_ShopClient_GuidePages.MenuName + "送" + Eggsoft_Public_CL.Pub.getPubMoney(strBonusMoney_ShareGuidePages.toDecimal()) + "元现金,点击'我'查看", strmywebuyURL);
                            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                            WeiXinTuWens_ArrayList.Add(First);


                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享咨询" + my_Model_tab_ShopClient_GuidePages.MenuName + "送" + Eggsoft_Public_CL.Pub.getPubMoney(strBonusMoney_ShareGuidePages.toDecimal()) + "元现金");
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {
                                if(boolTempletVisitMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(intUserID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }

                        }
                        if(strBonusGouWuQuan_ShareGuidePages.toDecimal() > 0)
                        {
                            ///赠送购物券
                            EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                            EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = strBonusGouWuQuan_ShareGuidePages.toDecimal();
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享咨询" + my_Model_tab_ShopClient_GuidePages.MenuName + "送";
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = intParentID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
                            Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                            BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);

                            Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "通知", "", Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享咨询" + my_Model_tab_ShopClient_GuidePages.MenuName + "送" + Eggsoft_Public_CL.Pub.getPubMoney(strBonusGouWuQuan_ShareGuidePages.toDecimal()) + "元购物券,点击'我'查看", strmywebuyURL);
                            System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                            WeiXinTuWens_ArrayList.Add(First);

                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享咨询" + my_Model_tab_ShopClient_GuidePages.MenuName + "送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "元" + Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.ToString(), "VouchersShopName") + "");
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {
                                if(boolTempletVisitMessage)
                                {
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(intUserID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }

                        }


                    }
                }
            }
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "分享咨询");
            }
            finally
            {

            }
        }

    }
}
