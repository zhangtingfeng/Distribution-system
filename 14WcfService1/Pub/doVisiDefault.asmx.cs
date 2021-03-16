using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doVisiDefault 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doVisiDefault : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        #region 访问首页
        [WebMethod]
        public string doVisitDefaultAction()
        {
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;


                string strpInt_QueryString_ParentID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strpInt_QueryString_ParentID"]);
                int pInt_QueryString_ParentID = 0;
                int.TryParse(strpInt_QueryString_ParentID, out pInt_QueryString_ParentID);

                string strpub_Int_Session_CurUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strpub_Int_Session_CurUserID"]);
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);
                int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpub_Int_Session_CurUserID);
                int intParentID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpInt_QueryString_ParentID);///保证同源


                string strpub_Int_ShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strpub_Int_ShopClientID"]);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_Int_ShopClientID, out pub_Int_ShopClientID);

                if((pInt_QueryString_ParentID == 0) || ((intUserID_ShopClientID == pub_Int_ShopClientID) && (intParentID_ShopClientID == pub_Int_ShopClientID)))///保证同源
                {
                    sendSNSToShopClient_o2o_WeiXin(pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToShopClientWeiXin(pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToMyParentBonus_WeiXin(pub_Int_ShopClientID, pub_Int_Session_CurUserID, pInt_QueryString_ParentID);
                    Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);
                }
                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strpub_Int_Session_CurUserID=" + strpub_Int_Session_CurUserID + ",strpub_Int_ShopClientID=" + strpub_Int_ShopClientID + ",strpInt_QueryString_ParentID=" + strpInt_QueryString_ParentID, "临界区突破doVisiDefault");

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
            string str = "";
            str = "{\"ErrorCode\":0}";////表示ok            
            if(HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }

        private void sendSNSToShopClient_o2o_WeiXin(int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + pub_Int_ShopClientID);
                if(boolExsit == false) return;

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(pub_Int_Session_CurUserID, out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);

                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);


                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                string strTitle = Model_tab_ShopClient_O2O_ShopInfo.ShopName + "o2o店主亲,你店铺首页链接,正在被" + strUserNickName + "浏览," + "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里";

                if(Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "o2oLookGoods"))
                {
                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for(int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, strTitle);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if(exists)
                        {
                            if(boolTempletVisitMessage)
                            {
                                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("店铺首页浏览信息", "", strTitle, "");
                                WeiXinTuWens_ArrayList.Add(First);
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }
                    }
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
        }

        private void sendSNSToShopClientWeiXin(int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                string strTitle = "店主亲,你店铺首页链接,正在被" + strUserNickName + "浏览";

                if(Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
                {
                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                    for(int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, strTitle);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if(exists)
                        {
                            if(boolTempletVisitMessage)
                            {
                                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("店铺首页浏览信息", "", strTitle, "");
                                WeiXinTuWens_ArrayList.Add(First);
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                            }
                        }
                    }
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
        }
        private void sendSNSToMyParentBonus_WeiXin(int pub_Int_ShopClientID, int pub_Int_Session_CurUserID, int pInt_QueryString_ParentID)
        {
            try
            {
                if(Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
                {
                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息
                    Eggsoft.Common.debug_Log.Call_WriteLog("boolTempletVisitMessage=" + boolTempletVisitMessage);
                    //Decimal[] FenXiaoOrDailiList = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(pub_Int_ShopClientID,0);
                    EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel Model_b019_tab_ShopClient_MultiFenXiaoLevel = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Default_GetAgent_FenXiaoOrDailiList(pub_Int_ShopClientID, pub_Int_Session_CurUserID.toInt32(),0);
                    int intLength = (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGrandParentGet > 0).toInt32() + (Model_b019_tab_ShopClient_MultiFenXiaoLevel.FenxiaoGreatParentGet > 0).toInt32();



                    if (intLength > 0)///一级代理
                    {
                        int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(pInt_QueryString_ParentID);
                        if((pInt_QueryString_ParentID != 0) && (intIF_Agent_From_Database_ > 0) && (pub_Int_Session_CurUserID != pInt_QueryString_ParentID))//处理父亲的消息
                        //if (pInt_QueryString_ParentID != 0)//处理父亲的消息
                        {
                            //实例化几个WeiXinTuWen类对象  
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                            string strTitle = (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的一级分享链接,正在被" + strUserNickName + "浏览";
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pInt_QueryString_ParentID, pub_Int_Session_CurUserID, strTitle);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {
                                if(boolTempletVisitMessage)
                                {
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("店铺首页浏览信息", "", strTitle, "");
                                    WeiXinTuWens_ArrayList.Add(First);
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_ParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }
                        }
                    }
                    if(intLength > 1)///二级代理
                    {
                        int pInt_QueryString_GrandParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_QueryString_ParentID);
                        int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(pInt_QueryString_GrandParentID);

                        if(pInt_QueryString_GrandParentID != 0 && (intIF_Agent_From_Database_ > 0) && pub_Int_Session_CurUserID != pInt_QueryString_GrandParentID)//处理爷爷的消息
                        {
                            //实例化几个WeiXinTuWen类对象  
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                            string strTitle = (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的二级分享链接,正在被" + strUserNickName + "浏览";
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pInt_QueryString_GrandParentID, pub_Int_Session_CurUserID, strTitle);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {
                                if(boolTempletVisitMessage)
                                {
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("店铺首页浏览信息", "", strTitle, "");
                                    WeiXinTuWens_ArrayList.Add(First);
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_GrandParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }

                        }
                    }
                    if(intLength > 2)///三级代理
                    {
                        int pInt_QueryString_GrandParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_QueryString_ParentID);
                        int pInt_QueryString_GreatParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pInt_QueryString_GrandParentID);
                        int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(pInt_QueryString_GreatParentID);

                        if(pInt_QueryString_GreatParentID != 0 && (intIF_Agent_From_Database_ > 0) && pub_Int_Session_CurUserID != pInt_QueryString_GreatParentID)//处理太爷爷的消息
                        {
                            //实例化几个WeiXinTuWen类对象  
                            string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                            strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                            string strTitle = (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的三级分享链接,正在被" + strUserNickName + "浏览";
                            string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pInt_QueryString_GreatParentID, pub_Int_Session_CurUserID, strTitle);
                            string[] strCheckReSendList = { "45015", "45047" };
                            bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                            if(exists)
                            {
                                if(boolTempletVisitMessage)
                                {
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("店铺首页浏览信息", "", strTitle, "");
                                    WeiXinTuWens_ArrayList.Add(First);
                                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_GreatParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                }
                            }
                        }
                    }
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
        }
        #endregion
        /// <summary>
        /// 缺省的  分享商城的 回调 事件
        /// </summary>
        /// <returns></returns>

        #region 整个商城的分享事件的处理  包含申请提现 不包含商品页面的分享（单独处理）
        [WebMethod]
        public string doShareDefaultAction()
        {
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strUserID"]);
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strUserID, out pub_Int_Session_CurUserID);

                string strShopClientID = Eggsoft.Common.CommUtil.SafeFilter(context.QueryString["strShopClientID"]);
                int pub_Int_ShopClientID = 0;
                int.TryParse(strShopClientID, out pub_Int_ShopClientID);

                lock("201611130653" + strUserID)
                {
                    int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID);///保证同源
                    if((intUserID_ShopClientID == pub_Int_ShopClientID))///保证同源
                    {
                        Decimal Decimal_XianJin = 0;
                        Decimal Decimal_GouWuQuan = 0;

                        string strShareShopXianJin_EveryDay = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "ShareShopXianJin_EveryDay");///
                        string strShareShopGouWuQuan_EveryDay = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "ShareShopGouWuQuan_EveryDay");///

                        Decimal.TryParse(strShareShopXianJin_EveryDay, out Decimal_XianJin);
                        Decimal.TryParse(strShareShopGouWuQuan_EveryDay, out Decimal_GouWuQuan);

                        if((Decimal_XianJin > 0) || (Decimal_GouWuQuan > 0))
                        {
                            ////检查24小时内是否 有数据。。有的话取出Count_Visit 加-
                            ///没有  加数据 送积分
                            EggsoftWX.BLL.tab_User_ShopClient_History BLL_tab_User_ShopClient_History = new EggsoftWX.BLL.tab_User_ShopClient_History();

                            string strsql = "DateDiff(hh,CreatTime,GetDate())<24 and UserID=" + strUserID + " and ShopClientID=" + strShopClientID + " and Type_Visit='ShareTimeLine'";
                            bool bool24 = BLL_tab_User_ShopClient_History.Exists(strsql);
                            if(bool24)
                            {
                                EggsoftWX.Model.tab_User_ShopClient_History Model_tab_User_ShopClient_History = BLL_tab_User_ShopClient_History.GetModel(strsql);
                                Model_tab_User_ShopClient_History.Count_Visit = Model_tab_User_ShopClient_History.Count_Visit + 1;
                                Model_tab_User_ShopClient_History.UpdateTime = DateTime.Now;
                                BLL_tab_User_ShopClient_History.Update(Model_tab_User_ShopClient_History);
                            }
                            else
                            {
                                EggsoftWX.Model.tab_User_ShopClient_History Model_tab_User_ShopClient_History = new EggsoftWX.Model.tab_User_ShopClient_History();
                                Model_tab_User_ShopClient_History.ShopClientID = pub_Int_ShopClientID;
                                Model_tab_User_ShopClient_History.Type_Visit = "ShareTimeLine";//分享朋友圈
                                Model_tab_User_ShopClient_History.UserID = pub_Int_Session_CurUserID;
                                Model_tab_User_ShopClient_History.Count_Visit = 1;
                                BLL_tab_User_ShopClient_History.Add(Model_tab_User_ShopClient_History);


                                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                                string strmywebuyURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/mywebuy.aspx";
                                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                                if(Decimal_XianJin > 0)
                                {
                                    ///赠送一个钱
                                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 43;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Decimal_XianJin;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "分享商城送";
                                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
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

                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送现金通知", "", "分享商城送" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_XianJin) + "元现金,点击'我'查看", strmywebuyURL);
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    WeiXinTuWens_ArrayList.Add(First);


                                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pub_Int_ShopClientID, 0, "分享分享商城送" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_XianJin) + "元现金");
                                    string[] strCheckReSendList = { "45015", "45047" };
                                    bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                    if(exists)
                                    {
                                        if(boolTempletVisitMessage)
                                        {
                                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pub_Int_Session_CurUserID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                        }
                                    }

                                }
                                if(Decimal_GouWuQuan > 0)
                                {
                                    ///赠送购物券
                                    EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Decimal_GouWuQuan;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "分享商城送";
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = pub_Int_ShopClientID;
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


                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "通知", "", "分享商城送" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_GouWuQuan) + "元" + Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.ToString(), "VouchersShopName") + ",点击'我'查看", strmywebuyURL);
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    WeiXinTuWens_ArrayList.Add(First);

                                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pub_Int_Session_CurUserID, 0, "分享商城送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_GouWuQuan) + "元" + Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.ToString(), "VouchersShopName") + "");
                                    string[] strCheckReSendList = { "45015", "45047" };
                                    bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                    if(exists)
                                    {
                                        if(boolTempletVisitMessage)
                                        {
                                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pub_Int_Session_CurUserID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                    }
                }

            }
            catch(System.Threading.ThreadAbortException ettt)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ettt, "线程异常");
            }
            catch(Exception eee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eee);
            }
            string str = "";
            str = "{\"ErrorCode\":0}";////表示ok            
            if(HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";

        }
        #endregion
    }
}