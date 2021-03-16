using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doVisitGames 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doVisitGames : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string dodoVisitGameAction()
        {

            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;


                string strpGameInfo = context.QueryString["strpGameInfo"];
                strpGameInfo = System.Web.HttpUtility.UrlDecode(strpGameInfo);

                string strpInt_QueryString_ParentID = context.QueryString["strpInt_QueryString_ParentID"];
                int pInt_QueryString_ParentID = 0;
                int.TryParse(strpInt_QueryString_ParentID, out pInt_QueryString_ParentID);

                string strpub_Int_Session_CurUserID = context.QueryString["strpub_Int_Session_CurUserID"];
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);
                int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpub_Int_Session_CurUserID);
                int intParentID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpInt_QueryString_ParentID);///保证同源

                string strpub_Int_ShopClientID = context.QueryString["strpub_Int_ShopClientID"];
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_Int_ShopClientID, out pub_Int_ShopClientID);

                if ((pInt_QueryString_ParentID == 0) || ((intUserID_ShopClientID == pub_Int_ShopClientID) && (intParentID_ShopClientID == pub_Int_ShopClientID)))///保证同源
                {
                    sendSNSToShopClient_o2o_WeiXin(strpGameInfo, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToShopClientWeiXin(strpGameInfo, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToMyParentBonus_WeiXin(strpGameInfo, pub_Int_ShopClientID, pub_Int_Session_CurUserID, pInt_QueryString_ParentID);
                    Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);
                }
                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strpub_Int_Session_CurUserID=" + strpub_Int_Session_CurUserID + ",strpub_Int_ShopClientID=" + strpub_Int_ShopClientID + ",strpInt_QueryString_ParentID=" + strpInt_QueryString_ParentID, "临界区突破doVisiDefault");

                }
            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "浏览通用页面");
            }
            finally
            {

            }
            return "1";
        }

        private void sendSNSToShopClient_o2o_WeiXin(string strpGameInfo, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + pub_Int_ShopClientID);
                if (boolExsit == false) return;

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(pub_Int_Session_CurUserID, out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);


                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                string strTitle = Model_tab_ShopClient_O2O_ShopInfo.ShopName + "o2o店主亲," + strpGameInfo;
                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "GameLook"))
                {
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, strTitle);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("打开游戏消息", "", strTitle, "");
                                WeiXinTuWens_ArrayList.Add(First);
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);

                            }
                        }
                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "浏览商品");
            }
            finally
            {

            }
        }

        private void sendSNSToShopClientWeiXin(string strpGameInfo, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                string strTitle = "店主亲," + strpGameInfo;
                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "GameLook"))
                {
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, strTitle);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("打开游戏消息", "", strTitle, "");
                                WeiXinTuWens_ArrayList.Add(First);
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);

                            }
                        }

                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "浏览商品");
            }
            finally
            {

            }
        }
        private void sendSNSToMyParentBonus_WeiXin(string strpGameInfo, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID, int pInt_QueryString_ParentID)
        {
            try
            {
                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "GameLook"))
                {
                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息
                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(pub_Int_ShopClientID, pub_Int_Session_CurUserID, 0);
                    int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();


                    //Decimal[] FenXiaoOrDailiList = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(pub_Int_ShopClientID,0);
                    if (intLength > 0)///一级代理
                    {
                        if ((pInt_QueryString_ParentID != 0) && (Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(pInt_QueryString_ParentID)>0))//处理父亲的消息
                        {
                            //实例化几个WeiXinTuWen类对象  
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                            string strTitle = "代理亲,一级分享链接" + strpGameInfo;
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pInt_QueryString_ParentID, pub_Int_Session_CurUserID, strTitle);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {
                                if (boolTempletVisitMessage)
                                {
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("打开游戏消息", "", strTitle, "");
                                    WeiXinTuWens_ArrayList.Add(First);
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_ParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);

                                }
                            }
                        }
                    }
                    else if (intLength > 1)///二级代理
                    {
                        int pInt_QueryString_GrandParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_QueryString_ParentID);

                        if (pInt_QueryString_GrandParentID != 0)//处理爷爷的消息
                        {
                            //实例化几个WeiXinTuWen类对象  
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                            string strTitle = "代理亲,二级分享链接" + strpGameInfo;
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pInt_QueryString_GrandParentID, pub_Int_Session_CurUserID, strTitle);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {
                                if (boolTempletVisitMessage)
                                {
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("打开游戏消息", "", strTitle, "");
                                    WeiXinTuWens_ArrayList.Add(First);
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_GrandParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);

                                }
                            }
                        }
                    }
                    else if (intLength > 2)///三级代理
                    {
                        int pInt_QueryString_GrandParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_QueryString_ParentID);
                        int pInt_QueryString_GreatParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_QueryString_GrandParentID);

                        if (pInt_QueryString_GreatParentID != 0)//处理太爷爷的消息
                        {
                            //实例化几个WeiXinTuWen类对象  
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                            string strTitle = "代理亲,三级分享链接" + strpGameInfo;
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pInt_QueryString_GreatParentID, pub_Int_Session_CurUserID, strTitle);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if (exists)
                            {
                                if (boolTempletVisitMessage)
                                {
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("打开游戏消息", "", strTitle, "");
                                    WeiXinTuWens_ArrayList.Add(First);
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_GreatParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);

                                }
                            }

                        }
                    }
                }
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "浏览商品");
            }
            finally
            {

            }

        }


    }
}
