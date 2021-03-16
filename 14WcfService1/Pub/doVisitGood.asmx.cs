using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doVisitGood 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doVisitGood : System.Web.Services.WebService
    {

        /// <summary>
        /// 访问商品
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string doShareGoodAction(string strStringJson)
        {
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strIntGoodID = context.QueryString["strIntGoodID"];
                int pIntGoodID = 0;
                int.TryParse(strIntGoodID, out pIntGoodID);


                string strUserID = context.QueryString["strUserID"];
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strUserID, out pub_Int_Session_CurUserID);

                string strShopClientID = context.QueryString["strShopClientID"];
                int pub_Int_ShopClientID = 0;
                int.TryParse(strShopClientID, out pub_Int_ShopClientID);


                lock ("201611130654" + strUserID)
                {
                    int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID);///保证同源
                    int intGoodID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(pIntGoodID);///保证同源
                    if ((intUserID_ShopClientID == intGoodID_ShopClientID) && (pub_Int_ShopClientID == intGoodID_ShopClientID))///保证同源
                    {
                        Decimal Decimal_XianJin = 0;
                        Decimal Decimal_GouWuQuan = 0;

                        string strDecimal_XianJin = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "ShareGoodXianJin_EveryDay");
                        string strDecimal_GouWuQuan = Eggsoft_Public_CL.Pub.stringShowPower(strShopClientID, "ShareGoodGouWuQuan_EveryDay");

                        Decimal.TryParse(strDecimal_XianJin, out Decimal_XianJin);
                        Decimal.TryParse(strDecimal_GouWuQuan, out Decimal_GouWuQuan);

                        if ((Decimal_XianJin > 0) || (Decimal_GouWuQuan > 0))
                        {
                            ////检查24小时内是否 有数据。。有的话取出Count_Visit 加-
                            ///没有  加数据 送积分
                            EggsoftWX.BLL.tab_User_Good_History BLL_tab_User_Good_History = new EggsoftWX.BLL.tab_User_Good_History();

                            string strsql = "DateDiff(hh,CreatTime,GetDate())<24 and UserID=" + strUserID + " and GoodID=" + strIntGoodID + " and Type_Visit='ShareTimeLine'";
                            bool bool24 = BLL_tab_User_Good_History.Exists(strsql);
                            if (bool24)
                            {
                                EggsoftWX.Model.tab_User_Good_History Model_tab_User_Good_History = BLL_tab_User_Good_History.GetModel(strsql);
                                Model_tab_User_Good_History.Count_Visit = Model_tab_User_Good_History.Count_Visit + 1;
                                Model_tab_User_Good_History.UpdateTime = DateTime.Now;
                                BLL_tab_User_Good_History.Update(Model_tab_User_Good_History);
                            }
                            else
                            {
                                EggsoftWX.Model.tab_User_Good_History Model_tab_User_Good_History = new EggsoftWX.Model.tab_User_Good_History();
                                Model_tab_User_Good_History.GoodID = pIntGoodID;
                                Model_tab_User_Good_History.Type_Visit = "ShareTimeLine";//分享朋友圈
                                Model_tab_User_Good_History.UserID = pub_Int_Session_CurUserID;
                                Model_tab_User_Good_History.Count_Visit = 1;
                                BLL_tab_User_Good_History.Add(Model_tab_User_Good_History);

                                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
                                my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);
                                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                                string strmywebuyURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/mywebuy.aspx";
                                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                                if (Decimal_XianJin > 0)
                                {
                                    ///赠送一个钱
                                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 40;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = Decimal_XianJin;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "分享商品" + my_Model_tab_Goods.Name + "送";
                                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = intGoodID_ShopClientID;
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

                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送现金通知", "", "分享商品" + my_Model_tab_Goods.Name + "送" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_XianJin) + "元现金,点击'我'查看", strmywebuyURL);
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    WeiXinTuWens_ArrayList.Add(First);


                                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pub_Int_ShopClientID, 0, "分享商品" + my_Model_tab_Goods.Name + "送" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_XianJin) + "元现金");
                                    string[] strCheckReSendList = { "45015", "45047" };
                                    bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                    if (exists)
                                    {
                                        if (boolTempletVisitMessage)
                                        {
                                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pub_Int_Session_CurUserID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                        }
                                    }

                                }
                                if (Decimal_GouWuQuan > 0)
                                {
                                    ///赠送购物券
                                    EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = Decimal_GouWuQuan;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "分享商品" + my_Model_tab_Goods.Name + "送";
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = pub_Int_Session_CurUserID;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = intGoodID_ShopClientID;
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

                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "通知", "", "分享商品" + my_Model_tab_Goods.Name + "送" + Eggsoft_Public_CL.Pub.getPubMoney(Decimal_GouWuQuan) + "元" + Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.ToString(), "VouchersShopName") + ",点击'我'查看", strmywebuyURL);
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    WeiXinTuWens_ArrayList.Add(First);

                                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(pub_Int_Session_CurUserID, 0, "分享商品" + my_Model_tab_Goods.Name + "送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "元" + Eggsoft_Public_CL.Pub.stringShowPower(pub_Int_ShopClientID.ToString(), "VouchersShopName") + "");
                                    string[] strCheckReSendList = { "45015", "45047" };
                                    bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                    if (exists)
                                    {
                                        if (boolTempletVisitMessage)
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
                        //Eggsoft.Common.debug_Log.Call_WriteLog("strpub_Int_Session_CurUserID=" + strpub_Int_Session_CurUserID + ",strpub_Int_ShopClientID=" + strpub_Int_ShopClientID + ",strpInt_QueryString_ParentID=" + strpInt_QueryString_ParentID + ",strIntGoodID=" + strIntGoodID, "临界区突破doVisitGood");

                    }
                }

            }
            catch (Exception eee)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(eee, "浏览商品");
            }
            string str = "";
            str = "{\"ErrorCode\":0}";////表示ok            
            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }

        /// <summary>
        /// 访问事件
        /// </summary>
        /// <returns></returns>

        [WebMethod]
        public string doVisitGoodAction()
        {
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strIntGoodID = context.QueryString["strIntGoodID"];
                int pIntGoodID = 0;
                int.TryParse(strIntGoodID, out pIntGoodID);

                string strpInt_QueryString_ParentID = context.QueryString["strpInt_QueryString_ParentID"];
                int pInt_QueryString_ParentID = 0;
                int.TryParse(strpInt_QueryString_ParentID, out pInt_QueryString_ParentID);

                string strpub_Int_Session_CurUserID = context.QueryString["strpub_Int_Session_CurUserID"];
                int pub_Int_Session_CurUserID = 0;
                int.TryParse(strpub_Int_Session_CurUserID, out pub_Int_Session_CurUserID);

                string strpub_Int_ShopClientID = context.QueryString["strpub_Int_ShopClientID"];
                int pub_Int_ShopClientID = 0;
                int.TryParse(strpub_Int_ShopClientID, out pub_Int_ShopClientID);

                int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpub_Int_Session_CurUserID);///保证同源
                int intParentID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strpInt_QueryString_ParentID);///保证同源
                int intGoodID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromGoodID(pIntGoodID);///保证同源
                if (
                    (pInt_QueryString_ParentID == 0) ||
                    (
                    (intUserID_ShopClientID == pub_Int_ShopClientID)
                    && (intParentID_ShopClientID == pub_Int_ShopClientID)
                    && (intGoodID_ShopClientID == pub_Int_ShopClientID)
                    )
                    )///保证同源
                {
                    sendSNSToShopClientWeiXin(pIntGoodID, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToo2oWeiXin(pIntGoodID, pub_Int_ShopClientID, pub_Int_Session_CurUserID);
                    sendSNSToMyParentBonus_WeiXin(pIntGoodID, pub_Int_ShopClientID, pub_Int_Session_CurUserID, pInt_QueryString_ParentID);
                    sendSNSToUserBonus_WeiXin(pIntGoodID, pub_Int_ShopClientID, pub_Int_Session_CurUserID);



                    Write_This_Record(pIntGoodID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID, pub_Int_ShopClientID);
                    Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);
                }
                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strpub_Int_Session_CurUserID=" + strpub_Int_Session_CurUserID + ",strpub_Int_ShopClientID=" + strpub_Int_ShopClientID + ",strpInt_QueryString_ParentID=" + strpInt_QueryString_ParentID + ",strIntGoodID=" + strIntGoodID, "临界区突破doVisitGood");

                }
            }

            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "浏览商品");
            }
            finally
            {

            }
            string str = "";
            str = "{\"ErrorCode\":0}";////表示ok

            if (HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
            {
                HttpRequest Request = HttpContext.Current.Request;
                HttpResponse Response = HttpContext.Current.Response;
                string callback = Request["jsonp"];
                Response.Write(callback + "(" + str + ")");
                Response.End();//结束后续的操作，直接返回所需要的字符串
            }
            return "1";
        }

        private void sendSNSToShopClientWeiXin(int pIntGoodID, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

                my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "正在浏览" + my_Model_tab_Goods.Name;

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                strDescription += my_Model_tab_Goods.ShortInfo + "。";
                if (my_Model_tab_Goods.Send_Vouchers_IfBuy > 0)
                {
                    strDescription += "购买赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + Convert.ToDecimal(my_Model_tab_Goods.Send_Vouchers_IfBuy) + "元。";
                }
                if (my_Model_tab_Goods.Send_Money_IfBuy > 0)
                {
                    strDescription += "购买赠送现金" + Convert.ToDecimal(my_Model_tab_Goods.Send_Money_IfBuy) + "元。";
                }

                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);


                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/product-" + pIntGoodID.ToString() + ".aspx";
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);


                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
                {
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
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

        private void sendSNSToo2oWeiXin(int pIntGoodID, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + pub_Int_ShopClientID);
                if (boolExsit == false) return;

                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();

                my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "正在浏览" + my_Model_tab_Goods.Name;

                string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                strDescription += my_Model_tab_Goods.ShortInfo + "。";
                if (my_Model_tab_Goods.Send_Vouchers_IfBuy > 0)
                {
                    strDescription += "购买赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + Convert.ToDecimal(my_Model_tab_Goods.Send_Vouchers_IfBuy) + "元。";
                }
                if (my_Model_tab_Goods.Send_Money_IfBuy > 0)
                {
                    strDescription += "购买赠送现金" + Convert.ToDecimal(my_Model_tab_Goods.Send_Money_IfBuy) + "元。";
                }


                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(pub_Int_Session_CurUserID, out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);
                strTitle += "," + "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里";
                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);


                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/product-" + pIntGoodID.ToString() + ".aspx";
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);

                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "o2oLookGoods"))
                {
                    EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);
                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
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
        /// 可能会骚扰用户 停下来吧
        /// </summary>
        /// <param name="pIntGoodID"></param>
        /// <param name="pub_Int_ShopClientID"></param>
        /// <param name="pub_Int_Session_CurUserID"></param>
        private void sendSNSToUserBonus_WeiXin(int pIntGoodID, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID)
        {
            //EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
            //EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
            //my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);

            //System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
            //string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
            //strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
            //string strTitle = strUserNickName + ",亲,你正在浏览“" + my_Model_tab_Goods.Name + "”";

            //string strDescription = "";
            //strDescription += my_Model_tab_Goods.ShortInfo + "。";
            //if (my_Model_tab_Goods.Send_Vouchers_IfBuy > 0)
            //{
            //    strDescription += "购买赠送购物券" + Convert.ToDecimal(my_Model_tab_Goods.Send_Vouchers_IfBuy) + "元。";
            //}
            //if (my_Model_tab_Goods.Send_Money_IfBuy > 0)
            //{
            //    strDescription += "购买赠送现金" + Convert.ToDecimal(my_Model_tab_Goods.Send_Money_IfBuy) + "元。";
            //}
            //System.Collections.ArrayList WeiXinTuWens_ArrayListTemplet = new System.Collections.ArrayList();
            //Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTemplet = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strURL);
            //WeiXinTuWens_ArrayListTemplet.Add(FirstTemplet);

            //System.Collections.ArrayList WeiXinTuWens_ArrayListTuWen = new System.Collections.ArrayList();
            //String strTuwen = strDescription;

            //strTuwen += "代理微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "。";
            //Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTuWen = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strTuwen, strURL);
            //WeiXinTuWens_ArrayListTuWen.Add(FirstTuWen);

            //string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pInt_QueryString_ParentID, pub_Int_Session_CurUserID, WeiXinTuWens_ArrayListTuWen);
            //string[] strCheckReSendList = { "45015", "45047" };
            //bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
            //if (exists)
            //{
            //    if (boolTempletVisitMessage)
            //    {
            //        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_ParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayListTemplet);
            //    }
            //}
        }

        private void sendSNSToMyParentBonus_WeiXin(int pIntGoodID, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID, int pInt_QueryString_ParentID)
        {
            try
            {
                ///1 上级 是代理商 我是代理商 启用 代理商分级结算
                //2 上级 是代理商  我不是代理商 启动 代理商对零售商结算模式
                //3 上级是分销商 启用 
                ///1 判断上级代理的身份 如果 是分销商或者最低级的天使代理  启用 分销模式 
                ///2 如果是代理  启用 省级代理判断模式
                ///
                if (Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
                {
                    if (pInt_QueryString_ParentID == 0) return;
                    EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                    EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
                    my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);

                    EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                    EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                    my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                    string GoodIcon = Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);
                    string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + GoodIcon;
                    string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);
                    string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/product-" + pIntGoodID.ToString() + ".aspx";

                    EggsoftWX.BLL.tab_ShopClient_Agent_ BLL_tab_ShopClient_Agent_ = new EggsoftWX.BLL.tab_ShopClient_Agent_();
                    if ((pInt_QueryString_ParentID == pub_Int_Session_CurUserID) && (BLL_tab_ShopClient_Agent_.Exists("UserID=" + pInt_QueryString_ParentID + " and OnlyIsAngel=1 and IsDeleted=0")))
                    {
                        pInt_QueryString_ParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pub_Int_Session_CurUserID);///天使的父亲不要是自己。天使本人不享受分销提成
                    }
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent__ParentID = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pInt_QueryString_ParentID + " and Empowered=1 and IsDeleted=0");
                    EggsoftWX.Model.tab_ShopClient_Agent_ Model_tab_ShopClient_Agent__UserID = BLL_tab_ShopClient_Agent_.GetModel("UserID=" + pub_Int_Session_CurUserID + " and IsDeleted=0 and (Empowered=1 or OnlyIsAngel=1)");


                    //if (Model_tab_ShopClient_Agent__UserID != null && Model_tab_ShopClient_Agent__UserID.AgentLevelSelect > 0) return;/////是代理商浏览 什么也不干  直接退出
                    EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo BLL_tab_ShopClient_Agent_Level_ProductInfo = new EggsoftWX.BLL.tab_ShopClient_Agent_Level_ProductInfo();


                    bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                    //1 上级 是代理商 我是代理商 启用 代理商分级结算
                    #region 用户没有支付现金  什么也不干
                    ///用户需要支付的现金 包含已有现金  不包含购物券  计算代理所得的钱  从订单详细表中
                    Decimal DecimalUserPayMoney = my_Model_tab_Goods.PromotePrice.toDecimal();
                    #endregion 用户没有支付现金  什么也不干

                    //if (boolIfDaiLi == false)
                    //{
                    #region 是分销模式 并融入代理
                    int intParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(pub_Int_Session_CurUserID);
                    int intGrandID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intParentID);
                    int intGreatParentID = Eggsoft_Public_CL.Pub_Agent.GetGrandParentID_Agent_From_Database_(intGrandID);

                    //Decimal AgentGet = 0;
                    //Decimal ManagerAgentGet = 0;
                    //int ManagerAgentParentID = 0;
                    //Decimal ManagerGrandAgentGet = 0;
                    //int ManagerGrandAgentParentID = 0;
                    //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(pIntGoodID, pInt_QueryString_ParentID, out AgentGet, out ManagerAgentGet, out ManagerAgentParentID, out ManagerGrandAgentGet, out ManagerGrandAgentParentID, "0", "0", "0");
                    Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel myModel_MultiFenXiaoLevel = new Eggsoft_Public_CL.GoodP_ShopClient_MultiFenXiaoLevel();
                    myModel_MultiFenXiaoLevel.UserID = pub_Int_Session_CurUserID;
                    myModel_MultiFenXiaoLevel.GoodID = pIntGoodID;
                    Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_Percent(myModel_MultiFenXiaoLevel);
                    DecimalUserPayMoney = myModel_MultiFenXiaoLevel.DecimalGoodPrice;
                    #region  各级代理自己的价格
                    //Decimal myAdvancePriceProductPricepParentUserd_ID = 0;
                    //Decimal myAdvanceProductPricepManagerAgentParentID = 0;
                    //Decimal myAdvanceProductPriceManagerGrandAgentParentID = 0;
                    //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_AdvanceAgentProduct(pIntGoodID, intParentID, intGrandID, intGreatParentID, out myAdvancePriceProductPricepParentUserd_ID, out myAdvanceProductPricepManagerAgentParentID, out myAdvanceProductPriceManagerGrandAgentParentID);

                    //my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(pIntGoodID);
                    //Decimal DecimalUserPayMoney = my_Model_tab_Goods.PromotePrice.toDecimal();
                    //string strOrderCount = "1";
                    //string strGoodPrice = DecimalUserPayMoney.toString();
                    //Decimal? AdvanceParentAgentGet = DecimalUserPayMoney - myAdvancePriceProductPricepParentUserd_ID * strOrderCount.toInt32(); if (AdvanceParentAgentGet < 0) AdvanceParentAgentGet = 0;
                    //if (myAdvancePriceProductPricepParentUserd_ID == 0) AdvanceParentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelParentTrueGet = (AdvanceParentAgentGet > (myModel_MultiFenXiaoLevel.AgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceParentAgentGet : (myModel_MultiFenXiaoLevel.AgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得


                    //Decimal? AdvanceManagerAgentAgentGet = DecimalUserPayMoney - myAdvanceProductPricepManagerAgentParentID * strOrderCount.toInt32() - LevelParentTrueGet; if (AdvanceManagerAgentAgentGet < 0) AdvanceManagerAgentAgentGet = 0;
                    //if (myAdvanceProductPricepManagerAgentParentID == 0) AdvanceManagerAgentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelManagerParentTrueGet = (AdvanceManagerAgentAgentGet > (myModel_MultiFenXiaoLevel.ManagerAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceManagerAgentAgentGet : (myModel_MultiFenXiaoLevel.ManagerAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得

                    //Decimal? AdvanceManagerGrandAgentParentIDGet = DecimalUserPayMoney - myAdvanceProductPriceManagerGrandAgentParentID * strOrderCount.toInt32() - LevelManagerParentTrueGet - LevelParentTrueGet; if (AdvanceManagerGrandAgentParentIDGet < 0) AdvanceManagerGrandAgentParentIDGet = 0;
                    //if (myAdvanceProductPriceManagerGrandAgentParentID == 0) AdvanceManagerGrandAgentParentIDGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelGrandParentTrueGet = (AdvanceManagerGrandAgentParentIDGet > (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()) ? AdvanceManagerGrandAgentParentIDGet : (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * strGoodPrice.toDecimal() * (Decimal)0.01 * strOrderCount.toDecimal()));////可能作为分销商的取得

                    #region  代理的 由于用户可能是购物券付款 要计算实际购物所得    支付现金太少 就不给了
                    //if (DecimalUserPayMoney < AdvanceParentAgentGet) AdvanceParentAgentGet = 0;
                    //if ((DecimalUserPayMoney - AdvanceParentAgentGet) < AdvanceManagerAgentAgentGet) AdvanceManagerAgentAgentGet = 0;
                    //if ((DecimalUserPayMoney - AdvanceParentAgentGet - AdvanceManagerAgentAgentGet) < AdvanceManagerGrandAgentParentIDGet) AdvanceManagerGrandAgentParentIDGet = 0;
                    #endregion   由于用户可能是购物券付款 要计算实际购物所得
                    #region 重新计算一下
                    Decimal? AgentGetDis = myModel_MultiFenXiaoLevel.AgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? ManagerAgentDis = myModel_MultiFenXiaoLevel.ManagerAgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    Decimal? ManagerGrandAgentParentDis = myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * DecimalUserPayMoney * (Decimal)0.01;
                    #endregion 重新计算一下

                    #endregion  各级代理自己的价格

                    //#region  各级代理自己的价格
                    //Decimal myAdvancePriceProductPricepParentUserd_ID = 0;
                    //Decimal myAdvanceProductPricepManagerAgentParentID = 0;
                    //Decimal myAdvanceProductPriceManagerGrandAgentParentID = 0;
                    //Eggsoft_Public_CL.GoodP.Get_Agent_Product_Money_AdvanceAgentProduct(pIntGoodID, pInt_QueryString_ParentID, myModel_MultiFenXiaoLevel.ManagerAgentParentID, myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, out myAdvancePriceProductPricepParentUserd_ID, out myAdvanceProductPricepManagerAgentParentID, out myAdvanceProductPriceManagerGrandAgentParentID);

                    //Decimal? AdvanceParentAgentGet = my_Model_tab_Goods.PromotePrice - myAdvancePriceProductPricepParentUserd_ID; if(AdvanceParentAgentGet < 0) AdvanceParentAgentGet = 0;
                    //if(myAdvancePriceProductPricepParentUserd_ID == 0) AdvanceParentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelParentTrueGet = (AdvanceParentAgentGet > (myModel_MultiFenXiaoLevel.AgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01) ? AdvanceParentAgentGet : (myModel_MultiFenXiaoLevel.AgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01));////可能作为分销商的取得


                    //Decimal? AdvanceManagerAgentAgentGet = my_Model_tab_Goods.PromotePrice - myAdvanceProductPricepManagerAgentParentID - LevelParentTrueGet; if(AdvanceManagerAgentAgentGet < 0) AdvanceManagerAgentAgentGet = 0;
                    //if(myAdvanceProductPricepManagerAgentParentID == 0) AdvanceManagerAgentAgentGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelManagerParentTrueGet = (AdvanceManagerAgentAgentGet > (myModel_MultiFenXiaoLevel.ManagerAgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01) ? AdvanceManagerAgentAgentGet : (myModel_MultiFenXiaoLevel.ManagerAgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01));////可能作为分销商的取得

                    //Decimal? AdvanceManagerGrandAgentParentIDGet = my_Model_tab_Goods.PromotePrice - myAdvanceProductPriceManagerGrandAgentParentID - LevelParentTrueGet - LevelManagerParentTrueGet; if(AdvanceManagerGrandAgentParentIDGet < 0) AdvanceManagerGrandAgentParentIDGet = 0;
                    //if(myAdvanceProductPriceManagerGrandAgentParentID == 0) AdvanceManagerGrandAgentParentIDGet = 0;//父亲不是代理 就不要拿代理差价
                    //Decimal? LevelGrandParentTrueGet = (AdvanceManagerGrandAgentParentIDGet > (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01) ? AdvanceManagerGrandAgentParentIDGet : (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet * my_Model_tab_Goods.PromotePrice.toDecimal() * (Decimal)0.01));////可能作为分销商的取得


                    //#endregion  各级代理自己的价格
                    int intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(pInt_QueryString_ParentID);
                    if (pub_Int_Session_CurUserID != pInt_QueryString_ParentID && (pInt_QueryString_ParentID != 0) && (myModel_MultiFenXiaoLevel.AgentGet > 0) && (intIF_Agent_From_Database_ > 0))//处理父亲的消息
                    {
                        System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                        string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                        strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                        string strTitle = (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的一级分享链接“" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "浏览";
                        string strShortInfoDescription = "";
                        strShortInfoDescription += my_Model_tab_Goods.ShortInfo + "。";




                        System.Collections.ArrayList WeiXinTuWens_ArrayListTemplet = new System.Collections.ArrayList();
                        Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTemplet = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strShortInfoDescription, strURL);
                        WeiXinTuWens_ArrayListTemplet.Add(FirstTemplet);

                        string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) : "现金");
                        System.Collections.ArrayList WeiXinTuWens_ArrayListTuWen = new System.Collections.ArrayList();
                        String strTuwen = strShortInfoDescription + "代理亲。如果购买你一级分享的商品，将会获得所代理的商品的";
                        //if (myAdvancePriceProductPricepParentUserd_ID > 0)
                        //{
                        strTuwen += Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(AgentGetDis)) + "元" + strMoneyShow + "部分喔！" + "。";
                        //}
                        //else
                        //{
                        //    strTuwen += "百分比" + myModel_MultiFenXiaoLevel.AgentGet + " % " + strMoneyShow + "部分喔！" + "。";
                        //}
                        strTuwen += "代理微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "。";
                        Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTuWen = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strTuwen, strURL);
                        WeiXinTuWens_ArrayListTuWen.Add(FirstTuWen);

                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pInt_QueryString_ParentID, pub_Int_Session_CurUserID, WeiXinTuWens_ArrayListTuWen);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(pInt_QueryString_ParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayListTemplet);
                            }
                        }
                    }
                    intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(myModel_MultiFenXiaoLevel.ManagerAgentParentID);
                    if (pub_Int_Session_CurUserID != myModel_MultiFenXiaoLevel.ManagerAgentParentID && intIF_Agent_From_Database_ > 0 && (pInt_QueryString_ParentID != 0) && (myModel_MultiFenXiaoLevel.ManagerAgentGet > 0 ) && (myModel_MultiFenXiaoLevel.ManagerAgentParentID > 0)) //处理上级代理人的消息
                    {

                        System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                        string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                        strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());

                        string strTitle = (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的二级分享链接“" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "浏览";

                        string strShortInfoDescription = "";
                        strShortInfoDescription += my_Model_tab_Goods.ShortInfo + "。";

                        System.Collections.ArrayList WeiXinTuWens_ArrayListTemplet = new System.Collections.ArrayList();
                        Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTemplet = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strShortInfoDescription, strURL);
                        WeiXinTuWens_ArrayListTemplet.Add(FirstTemplet);
                        string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) : "现金");



                        System.Collections.ArrayList WeiXinTuWens_ArrayListTuWen = new System.Collections.ArrayList();
                        String strTuwen = strShortInfoDescription + "代理亲。如果有人购买你二级分享的商品，将会获得二级代理的商品的";
                        //if(myAdvanceProductPricepManagerAgentParentID > 0)
                        //{
                        strTuwen += "" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(ManagerAgentDis)) + " 元" + strMoneyShow + "部分喔！" + "。";
                        //}
                        //else
                        //{
                        //strTuwen += "百分比" + myModel_MultiFenXiaoLevel.ManagerAgentGet + " %" + strMoneyShow + "部分喔！" + "。";
                        //}
                        strTuwen += "代理微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "。";
                        Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTuWen = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strTuwen, strURL);
                        WeiXinTuWens_ArrayListTuWen.Add(FirstTuWen);

                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, pub_Int_Session_CurUserID, WeiXinTuWens_ArrayListTuWen);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(myModel_MultiFenXiaoLevel.ManagerAgentParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayListTemplet);
                            }
                        }

                    }

                    intIF_Agent_From_Database_ = Eggsoft_Public_CL.Pub_Agent.IF_Agent_From_Database_(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID);
                    if (pub_Int_Session_CurUserID != myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID && intIF_Agent_From_Database_ > 0 && (pInt_QueryString_ParentID != 0) && (myModel_MultiFenXiaoLevel.ManagerGrandAgentGet > 0 ) && (myModel_MultiFenXiaoLevel.ManagerAgentParentID != 0) && (myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID > 0)) //处理上上级代理人的消息
                    {
                        System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                        string strUserNickName = Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString());
                        strUserNickName += " 微店号:" + Eggsoft_Public_CL.Pub.GetMyShopUserID(pub_Int_Session_CurUserID.ToString());
                        string strTitle = (intIF_Agent_From_Database_ == 3 ? "天使" : "代理") + "亲,你的三级分享链接“" + my_Model_tab_Goods.Name + "”,正在被" + strUserNickName + "浏览";
                        string strShortInfoDescription = "";
                        strShortInfoDescription += my_Model_tab_Goods.ShortInfo + "。";

                        System.Collections.ArrayList WeiXinTuWens_ArrayListTemplet = new System.Collections.ArrayList();
                        Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTemplet = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strShortInfoDescription, strURL);
                        WeiXinTuWens_ArrayListTemplet.Add(FirstTemplet);
                        string strMoneyShow = (intIF_Agent_From_Database_ == 3 ? Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) : "现金");

                        System.Collections.ArrayList WeiXinTuWens_ArrayListTuWen = new System.Collections.ArrayList();
                        String strTuwen = strShortInfoDescription + "代理亲。如果有人购买你三级分享的商品，将会获得三级代理的商品的";
                        //if(myAdvanceProductPriceManagerGrandAgentParentID > 0)
                        //{
                        strTuwen += "" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(ManagerGrandAgentParentDis)) + "元" + strMoneyShow + "部分喔！" + "。";
                        //}
                        //else
                        //{
                        //    strTuwen += "百分比" + myModel_MultiFenXiaoLevel.ManagerGrandAgentGet + "%" + strMoneyShow + "部分喔！" + "。";
                        //}
                        strTuwen += "代理微店，真正一键微店。0投资0风险，不用囤货，不用发货，公司帮你一切搞定。！" + "。";
                        Eggsoft_Public_CL.ClassP.WeiXinTuWen FirstTuWen = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strImage, strTuwen, strURL);
                        WeiXinTuWens_ArrayListTuWen.Add(FirstTuWen);


                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, pub_Int_Session_CurUserID, WeiXinTuWens_ArrayListTuWen);
                        string[] strCheckReSendList = { "45015", "45047" };
                        bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                        if (exists)
                        {
                            if (boolTempletVisitMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(myModel_MultiFenXiaoLevel.ManagerGrandAgentParentID, pub_Int_ShopClientID, WeiXinTuWens_ArrayListTemplet);
                            }
                        }
                    }

                    #endregion 是分销模式
                    //}
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



        private void Write_This_Record(int intGoodID, int intParentID, int intUserID, int pub_Int_ShopClientID)
        {
            bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

            //String strPriParentID = Request.QueryString["ParentID"];
            try
            {
                if (intParentID == intUserID) intParentID = 0;///是否浏览自己的网页  在自己的店里买东西的问题啊

                EggsoftWX.BLL.tab_User_Good_History my_BLL_tab_User_Good_History = new EggsoftWX.BLL.tab_User_Good_History();
                EggsoftWX.Model.tab_User_Good_History my_Model_tab_User_Good_History = new EggsoftWX.Model.tab_User_Good_History();

                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();
                my_Model_tab_Goods = my_BLL_tab_Goods.GetModel(intGoodID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(intUserID);

                string strmywebuyURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/mywebuy.aspx";



                String strmy_BLL_tab_User_Good_History = "UserID=" + intUserID + " and GoodID=" + intGoodID + "" + " and Parent_UserID=" + intParentID;
                if (my_BLL_tab_User_Good_History.Exists(strmy_BLL_tab_User_Good_History))//重复访问  
                {
                    my_Model_tab_User_Good_History = my_BLL_tab_User_Good_History.GetModel(strmy_BLL_tab_User_Good_History);
                    my_Model_tab_User_Good_History.Count_Visit = my_Model_tab_User_Good_History.Count_Visit + 1;
                    my_Model_tab_User_Good_History.UpdateTime = DateTime.Now;
                    my_BLL_tab_User_Good_History.Update(my_Model_tab_User_Good_History);
                }
                else
                {


                    //写入分享数据
                    my_Model_tab_User_Good_History.UserID = intUserID;
                    my_Model_tab_User_Good_History.Parent_UserID = intParentID;

                    my_Model_tab_User_Good_History.GoodID = intGoodID;
                    my_Model_tab_User_Good_History.UpdateTime = DateTime.Now;
                    my_Model_tab_User_Good_History.Count_Visit = 1;
                    my_Model_tab_User_Good_History.Type_Visit = "Visit";
                    my_BLL_tab_User_Good_History.Add(my_Model_tab_User_Good_History);

                    if ((intParentID > 0))
                    {
                        EggsoftWX.BLL.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_bll = new EggsoftWX.BLL.tab_ShopClient_ShopPar();
                        EggsoftWX.Model.tab_ShopClient_ShopPar tab_ShopClient_ShopPar_Model = tab_ShopClient_ShopPar_bll.GetModel("ShopClientID=" + pub_Int_ShopClientID);
                        if (tab_ShopClient_ShopPar_Model != null)
                        {
                            if (Decimal.Round(tab_ShopClient_ShopPar_Model.GoodShareGiveMoney.toDecimal(), 2) > 0)
                            {
                                ///赠送一个钱
                                EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 20;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = tab_ShopClient_ShopPar_Model.GoodShareGiveMoney;
                                Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享商品" + my_Model_tab_Goods.Name + "送";
                                Model_tab_TotalCredits_Consume_Or_Recharge.UserID = intParentID;
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

                                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送现金通知", "", Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享商品" + my_Model_tab_Goods.Name + "送" + Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.GoodShareGiveMoney.toDecimal()) + "元现金,点击'我'查看", strmywebuyURL);
                                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                WeiXinTuWens_ArrayList.Add(First);


                                string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享商品" + my_Model_tab_Goods.Name + "送" + Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.GoodShareGiveMoney.toDecimal()) + "元现金");
                                string[] strCheckReSendList = { "45015", "45047" };
                                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                if (exists)
                                {
                                    if (boolTempletVisitMessage)
                                    {
                                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(intUserID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                    }
                                }

                            }
                            if (Decimal.Round(tab_ShopClient_ShopPar_Model.GoodShareGiveVouchers.toDecimal(), 2) > 0)
                            {
                                ///赠送购物券
                                EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = tab_ShopClient_ShopPar_Model.GoodShareGiveVouchers;
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享商品" + my_Model_tab_Goods.Name + "送";
                                Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = intParentID;
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

                                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("赠送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "通知", "", Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享商品" + my_Model_tab_Goods.Name + "送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "" + Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.GoodShareGiveVouchers.toDecimal()) + "元,点击'我'查看", strmywebuyURL);
                                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                WeiXinTuWens_ArrayList.Add(First);

                                string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID, 0, Eggsoft_Public_CL.Pub.GetNickName(intUserID.ToString()) + "分享商品" + my_Model_tab_Goods.Name + "送" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(pub_Int_ShopClientID.ToString()) + "" + Eggsoft_Public_CL.Pub.getPubMoney(tab_ShopClient_ShopPar_Model.GoodShareGiveVouchers.toDecimal()) + "元");
                                string[] strCheckReSendList = { "45015", "45047" };
                                bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                if (exists)
                                {
                                    if (boolTempletVisitMessage)
                                    {
                                        Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(intUserID, pub_Int_ShopClientID, WeiXinTuWens_ArrayList);
                                    }
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
