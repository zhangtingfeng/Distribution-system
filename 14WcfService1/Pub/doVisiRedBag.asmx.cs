using Eggsoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _14WcfS.Pub
{
    /// <summary>
    /// doVisiRedBag 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class doVisiRedBag : System.Web.Services.WebService
    {


        [WebMethod]
        public string doVisiRedBagAction()
        {
            try
            {
                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                string strthisshowid = context.QueryString["strthisshowid"];
                int pthisshowid = 0;
                int.TryParse(strthisshowid, out pthisshowid);

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

                if((pInt_QueryString_ParentID == 0) || ((intUserID_ShopClientID == pub_Int_ShopClientID) && (intParentID_ShopClientID == pub_Int_ShopClientID)))///保证同源
                {
                    EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                    EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers From_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(pthisshowid);
                    if(From_Model_tab_RedWallet_Money_Credits != null)
                    {
                        Int32 intMoney_Credits_Vouchers = From_Model_tab_RedWallet_Money_Credits.Type_Or_Money_Credits_Vouchers.toInt32();

                        sendSNSToShopClientWeiXin(intMoney_Credits_Vouchers, pthisshowid, pub_Int_ShopClientID, pub_Int_Session_CurUserID, pInt_QueryString_ParentID);
                        sendSNSToShopCliento2oWeiXin(intMoney_Credits_Vouchers, pthisshowid, pub_Int_ShopClientID, pub_Int_Session_CurUserID, pInt_QueryString_ParentID);
                        sendSNSToMyParentBonus_WeiXin(intMoney_Credits_Vouchers, pthisshowid, pub_Int_ShopClientID, pub_Int_Session_CurUserID, pInt_QueryString_ParentID);

                        Eggsoft_Public_CL.Pub_FenXiao.Write_This_Record_ShopClient(pub_Int_ShopClientID, pInt_QueryString_ParentID, pub_Int_Session_CurUserID);
                    }
                    else
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog("pthisshowid=" + pthisshowid, "doVisiRedBag分销红包", "程序报错记录原因");
                    }
                }
                else
                {
                    Eggsoft.Common.debug_Log.Call_WriteLog("strpub_Int_Session_CurUserID=" + strpub_Int_Session_CurUserID + ",strpub_Int_ShopClientID=" + strpub_Int_ShopClientID + ",strpInt_QueryString_ParentID=" + strpInt_QueryString_ParentID, "临界区突破doVisiRedBag");

                }
            }

            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "doVisiRedBag分销红包");
            }
            finally
            {

            }
            return "1";

        }
        private void sendSNSToShopCliento2oWeiXin(Int32 intMoney_Credits_Vouchers, int pthisshowid, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID, int pInt_QueryString_ParentID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo BLL_tab_ShopClient_O2O_ShopInfo = new EggsoftWX.BLL.tab_ShopClient_O2O_ShopInfo();
                bool boolExsit = BLL_tab_ShopClient_O2O_ShopInfo.Exists("ShopClientID=" + pub_Int_ShopClientID);
                if(boolExsit == false) return;


                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


                string strTitle = "";
                string strDes = "";

                EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers IGet_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(pthisshowid);
                Decimal myDecimal = 0;
                bool boolIhaveGet = Bll_tab_RedWallet_Money_Credits.Exists("PID=" + pthisshowid + " and UserID=" + pub_Int_Session_CurUserID);
                if(boolIhaveGet)
                {
                    IGet_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel("PID=" + pthisshowid + " and UserID=" + pub_Int_Session_CurUserID);
                    if(IGet_Model_tab_RedWallet_Money_Credits != null)
                    {
                        myDecimal = IGet_Model_tab_RedWallet_Money_Credits.Money.toDecimal();
                    }

                }
                if(myDecimal <= 0) return;////还发送什么消息

                int outintShopo2oID = 0;
                double outDecimalDistance = 0;
                string strUserBaiDuAdress = "";
                Eggsoft_Public_CL.ClassP.getNearestShop_IDFrom_UserID(pub_Int_Session_CurUserID, out outintShopo2oID, out outDecimalDistance, out strUserBaiDuAdress);
                EggsoftWX.Model.tab_ShopClient_O2O_ShopInfo Model_tab_ShopClient_O2O_ShopInfo = BLL_tab_ShopClient_O2O_ShopInfo.GetModel(outintShopo2oID);

                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);
                string strIMGUrl = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Model_ShopClient.UpLoadPath + "/QRCodeImage/MredWallet" + (2 - intMoney_Credits_Vouchers) + "_Share.jpg";

                if(intMoney_Credits_Vouchers == 1)
                {
                    strTitle += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "抢现金红包，抢到" + Eggsoft_Public_CL.Pub.getPubMoney(myDecimal) + "元";
                    strDes = Model_tab_ShopClient_O2O_ShopInfo.ShopName + "o2o店主亲," + "微店现金红包" + "由" + Eggsoft_Public_CL.Pub.GetNickName(pInt_QueryString_ParentID.ToString()) + "发放";
                }

                else if(intMoney_Credits_Vouchers == 2)
                {
                    strTitle += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "抢购物红包,抢到" + Eggsoft_Public_CL.Pub.getPubMoney(myDecimal) + "元";
                    strDes = Model_tab_ShopClient_O2O_ShopInfo.ShopName + "o2o店主亲," + "微店购物红包" + "由" + Eggsoft_Public_CL.Pub.GetNickName(pInt_QueryString_ParentID.ToString()) + "发放";
                }
                strTitle += "," + "目前o2o位置" + strUserBaiDuAdress + ",距离:" + Eggsoft_Public_CL.Pub.getPubMoney(Convert.ToDecimal(outDecimalDistance)) + "公里";



                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  

                //string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                strDescription += my_Model_tab_Goods.ShortInfo + "。";

                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                #region 加签名  
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Modeltab_User = BLL_tab_User.GetModel(IGet_Model_tab_RedWallet_Money_Credits.UserID.toInt32());
                string strUserIDSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2(Modeltab_User.SafeCode);
                string strSign = Eggsoft.Common.DESCrypt.hex_md5_8(pthisshowid.toString() + strUserIDSafeCode);
                #endregion 加签名


                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/midsmf-" + pthisshowid.ToString() + ".aspx?sign=" + strSign;
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strIMGUrl, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);

                if(Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "o2oLookGoods"))
                {

                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalation_o2o_UserIDList(Model_tab_ShopClient_O2O_ShopInfo.XML);
                    for(int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);
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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "doVisiRedBag分销红包");
            }
            finally
            {

            }
        }


        private void sendSNSToShopClientWeiXin(Int32 intMoney_Credits_Vouchers, int pthisshowid, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID, int pInt_QueryString_ParentID)
        {
            //return;
            try
            {
                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


                string strTitle = "";
                string strFirstImageFullName = "";
                string strDes = "";

                EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers IGet_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(pthisshowid);
                Decimal myDecimal = 0;
                bool boolIhaveGet = Bll_tab_RedWallet_Money_Credits.Exists("PID=" + pthisshowid + " and UserID=" + pub_Int_Session_CurUserID);
                if(boolIhaveGet)
                {
                    IGet_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel("PID=" + pthisshowid + " and UserID=" + pub_Int_Session_CurUserID);
                    if(IGet_Model_tab_RedWallet_Money_Credits != null)
                    {
                        myDecimal = IGet_Model_tab_RedWallet_Money_Credits.Money.toDecimal();
                    }

                }
                if(myDecimal <= 0) return;////还发送什么消息

                if(intMoney_Credits_Vouchers == 1)
                {
                    strTitle += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "抢现金红包，抢到" + Eggsoft_Public_CL.Pub.getPubMoney(myDecimal) + "元";
                    strFirstImageFullName = "http://qiniu.eggsoft.cn/walletWeBuy1.png";
                    strDes = "微店现金红包" + "由" + Eggsoft_Public_CL.Pub.GetNickName(pInt_QueryString_ParentID.ToString()) + "发放";
                }

                else if(intMoney_Credits_Vouchers == 2)
                {
                    strTitle += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "抢购物红包,抢到" + Eggsoft_Public_CL.Pub.getPubMoney(myDecimal) + "元";
                    strFirstImageFullName = "http://qiniu.eggsoft.cn/walletWeBuy0.png";
                    strDes = "微店购物红包" + "由" + Eggsoft_Public_CL.Pub.GetNickName(pInt_QueryString_ParentID.ToString()) + "发放";
                }



                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  

                //string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                strDescription += my_Model_tab_Goods.ShortInfo + "。";

                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息

                #region 加签名  
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Modeltab_User = BLL_tab_User.GetModel(IGet_Model_tab_RedWallet_Money_Credits.UserID.toInt32());
                string strUserIDSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2(Modeltab_User.SafeCode);
                string strSign = Eggsoft.Common.DESCrypt.hex_md5_8(pthisshowid.toString() + strUserIDSafeCode);
                #endregion 加签名


                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/midsmf-" + pthisshowid.ToString() + ".aspx?sign=" + strSign;
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strFirstImageFullName, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);

                if(Eggsoft_Public_CL.GoodP.GetShopClientAccptPowerList(pub_Int_ShopClientID, "WinXinLookGoods"))
                {
                    string[] stringWeiXinRalationUserIDList = Eggsoft_Public_CL.Pub.GetstringWeiXinRalationUserIDList(Model_ShopClient.XML);
                    for(int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {
                        string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(Int32.Parse(stringWeiXinRalationUserIDList[k]), pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);

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
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "doVisiRedBag分销红包");
            }
            finally
            {

            }
        }



        private void sendSNSToMyParentBonus_WeiXin(Int32 intMoney_Credits_Vouchers, int pthisshowid, int pub_Int_ShopClientID, int pub_Int_Session_CurUserID, int pInt_QueryString_ParentID)
        {
            try
            {
                EggsoftWX.BLL.tab_Goods my_BLL_tab_Goods = new EggsoftWX.BLL.tab_Goods();
                EggsoftWX.Model.tab_Goods my_Model_tab_Goods = new EggsoftWX.Model.tab_Goods();


                string strTitle = "";
                //string strFirstImageFullName = "";
                string strDes = "";

                EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers Bll_tab_RedWallet_Money_Credits = new EggsoftWX.BLL.tab_RedWallet_Money_Credits_Vouchers();
                EggsoftWX.Model.tab_RedWallet_Money_Credits_Vouchers IGet_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel(pthisshowid);
                Decimal myDecimal = 0;
                bool boolIhaveGet = Bll_tab_RedWallet_Money_Credits.Exists("PID=" + pthisshowid + " and UserID=" + pub_Int_Session_CurUserID);
                if(boolIhaveGet)
                {
                    IGet_Model_tab_RedWallet_Money_Credits = Bll_tab_RedWallet_Money_Credits.GetModel("PID=" + pthisshowid + " and UserID=" + pub_Int_Session_CurUserID);
                    if(IGet_Model_tab_RedWallet_Money_Credits != null)
                    {
                        myDecimal = IGet_Model_tab_RedWallet_Money_Credits.Money.toDecimal();
                    }
                }
                if(myDecimal <= 0) return;////还发送什么消息
                EggsoftWX.BLL.tab_ShopClient BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_tab_ShopClient = BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);
                string strIMGUrl = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Model_tab_ShopClient.UpLoadPath + "/QRCodeImage/MredWallet" + (2 - intMoney_Credits_Vouchers) + "_Share.jpg";

                if(intMoney_Credits_Vouchers == 1)
                {
                    strTitle += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "抢现金红包，抢到" + Eggsoft_Public_CL.Pub.getPubMoney(myDecimal) + "元";
                    strDes = "微店现金红包" + "由" + Eggsoft_Public_CL.Pub.GetNickName(pInt_QueryString_ParentID.ToString()) + "发放";
                }

                else if(intMoney_Credits_Vouchers == 2)
                {
                    strTitle += Eggsoft_Public_CL.Pub.GetNickName(pub_Int_Session_CurUserID.ToString()) + "抢购物红包,抢到" + Eggsoft_Public_CL.Pub.getPubMoney(myDecimal) + "元";
                    strDes = "微店购物红包" + "由" + Eggsoft_Public_CL.Pub.GetNickName(pInt_QueryString_ParentID.ToString()) + "发放";
                }



                EggsoftWX.BLL.tab_ShopClient bll_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient Model_ShopClient = bll_ShopClient.GetModel(pub_Int_ShopClientID);

                System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                //实例化几个WeiXinTuWen类对象  

                //string strImage = Eggsoft_Public_CL.Pub.GetAppConfiugUpLoadResourceURL() + Eggsoft_Public_CL.GoodP.APPCODE_OnlyGetMyFileName_Image_Force(my_Model_tab_Goods.Icon, 640);

                string strDescription = "";
                strDescription += my_Model_tab_Goods.ShortInfo + "。";

                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(pub_Int_Session_CurUserID);

                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(pub_Int_ShopClientID);

                #region 加签名  
                EggsoftWX.BLL.tab_User BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User Modeltab_User = BLL_tab_User.GetModel(IGet_Model_tab_RedWallet_Money_Credits.UserID.toInt32());
                string strUserIDSafeCode = Eggsoft.Common.DESCrypt.hex_md5_2(Modeltab_User.SafeCode);
                string strSign = Eggsoft.Common.DESCrypt.hex_md5_8(pthisshowid.toString() + strUserIDSafeCode);
                #endregion 加签名

                string strURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + "/midsmf-" + pthisshowid.ToString() + ".aspx?sign=" + strSign;
                Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen(strTitle, strIMGUrl, strDescription, strURL);
                WeiXinTuWens_ArrayList.Add(First);

                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(pub_Int_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息
                string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessageImage(pInt_QueryString_ParentID, pub_Int_Session_CurUserID, WeiXinTuWens_ArrayList);

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
            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "doVisiRedBag分销红包");
            }
            finally
            {

            }
        }

        /// <summary>
        /// 兑换 事件
        /// </summary>       
        /// <returns></returns>
        [WebMethod]
        public String _ChangeMoney()
        {
            try
            {

                var response = HttpContext.Current.Response;
                var context = HttpContext.Current.Request;

                String strUserID = context.QueryString["strUserID"];
                String strShopClientID = context.QueryString["strShopClientID"];
                String GouWuQuan2XianJInEtcID = context.QueryString["GouWuQuan2XianJInEtcID"];

                //Eggsoft.Common.debug_Log.Call_WriteLog("strUserID=" + strUserID + " strShopClientID=" + strShopClientID + " GouWuQuan2XianJInEtcID=" + GouWuQuan2XianJInEtcID + "", "_ChangeMoney");

                int intErrorCode = 1;
                string strReturn = "";
                lock("201601132026" + strUserID)
                {
                    #region 保证同源
                    try
                    {
                        int intUserID_ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID);///保证同源
                        if(intUserID_ShopClientID.ToString() == strShopClientID)
                        {
                            Decimal myCountMoney_Vouchers = 0;
                            Eggsoft_Public_CL.Pub_FenXiao.Do_CountyuEArgMoney_Vouchers(Int32.Parse(strUserID), out myCountMoney_Vouchers);
                            Decimal WillChangeDecimal = myCountMoney_Vouchers;

                            EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc BLL_tab_GouWuQuan2XianJInEtc = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc();
                            EggsoftWX.Model.tab_GouWuQuan2XianJInEtc Model_tab_GouWuQuan2XianJInEtc = BLL_tab_GouWuQuan2XianJInEtc.GetModel(Int32.Parse(GouWuQuan2XianJInEtcID));

                            if(Model_tab_GouWuQuan2XianJInEtc.UserGouWuQuan > WillChangeDecimal)
                            {
                                intErrorCode = -2;///余额不足 无权兑现
                            }
                            else
                            {
                                EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc_User my_BLL_tab_GouWuQuan2XianJInEtc_User = new EggsoftWX.BLL.tab_GouWuQuan2XianJInEtc_User();
                                EggsoftWX.Model.tab_GouWuQuan2XianJInEtc_User my_Model_tab_GouWuQuan2XianJInEtc_User = new EggsoftWX.Model.tab_GouWuQuan2XianJInEtc_User();
                                my_Model_tab_GouWuQuan2XianJInEtc_User.ID_GouWuQuan2XianJInEtc = Int32.Parse(GouWuQuan2XianJInEtcID);
                                my_Model_tab_GouWuQuan2XianJInEtc_User.ShopClientID = intUserID_ShopClientID;
                                my_Model_tab_GouWuQuan2XianJInEtc_User.UserID = Int32.Parse(strUserID);
                                my_Model_tab_GouWuQuan2XianJInEtc_User.updatetime = DateTime.Now;


                                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = new EggsoftWX.Model.tab_ShopClient();
                                my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(intUserID_ShopClientID);
                                string Pub_Agent_Path = Eggsoft_Public_CL.Pub_Agent.Pub_Agent_Path(Int32.Parse(strUserID));

                                string strmywebuyURL = "https://" + my_Model_tab_ShopClient.ErJiYuMing + Pub_Agent_Path + "/mywebuy.aspx";
                                bool boolTempletVisitMessage = Eggsoft_Public_CL.Pub.boolShowPower(intUserID_ShopClientID.ToString(), "TempletVisitMessage");///是否可以发模板消息


                                if((Model_tab_GouWuQuan2XianJInEtc.ChangeAuto == "Auto") && (Model_tab_GouWuQuan2XianJInEtc.ChangeDestination == 1))
                                {
                                    intErrorCode = 2;///兑现成功
                                    my_Model_tab_GouWuQuan2XianJInEtc_User.ISpassed = 1;
                                    my_BLL_tab_GouWuQuan2XianJInEtc_User.Add(my_Model_tab_GouWuQuan2XianJInEtc_User);
                                    #region change
                                    EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge BLL_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.BLL.tab_TotalCredits_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge Model_tab_TotalCredits_Consume_Or_Recharge = new EggsoftWX.Model.tab_TotalCredits_Consume_Or_Recharge();
                                    Model_tab_TotalCredits_Consume_Or_Recharge.Bool_ConsumeOrRecharge = true;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeMoney = (Decimal)Model_tab_GouWuQuan2XianJInEtc.XianJinMoney;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeTypeOrRecharge = "兑现" + Eggsoft_Public_CL.Pub.GetstringShowPower_ShopName(strShopClientID) + Model_tab_GouWuQuan2XianJInEtc.UserGouWuQuan + "元";
                                    Model_tab_TotalCredits_Consume_Or_Recharge.UserID = Int32.Parse(strUserID);
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ConsumeOrRechargeType = 81;
                                    Model_tab_TotalCredits_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
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

                                    EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge BLL_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.BLL.tab_Total_Vouchers_Consume_Or_Recharge();
                                    EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge Model_tab_Total_Vouchers_Consume_Or_Recharge = new EggsoftWX.Model.tab_Total_Vouchers_Consume_Or_Recharge();
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.Bool_ConsumeOrRecharge = false;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeOrRecharge_Vouchers = (Decimal)Model_tab_GouWuQuan2XianJInEtc.XianJinMoney; ;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge = "自动兑现现金" + (Decimal)Model_tab_GouWuQuan2XianJInEtc.XianJinMoney + "元";
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID = Int32.Parse(strUserID); ;
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID = strShopClientID.toInt32();
                                    Model_tab_Total_Vouchers_Consume_Or_Recharge.UpdateTime = DateTime.Now;
                                    intTableID=BLL_tab_Total_Vouchers_Consume_Or_Recharge.Add(Model_tab_Total_Vouchers_Consume_Or_Recharge);
                                    #region 增加购物券未处理信息                            
                                    Model_b011_InfoAlertMessage = new EggsoftWX.Model.b011_InfoAlertMessage();
                                    Model_b011_InfoAlertMessage.InfoTip = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                    Model_b011_InfoAlertMessage.CreateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                    Model_b011_InfoAlertMessage.UpdateBy = Model_tab_Total_Vouchers_Consume_Or_Recharge.ConsumeTypeOrRecharge;
                                    Model_b011_InfoAlertMessage.UserID = Model_tab_Total_Vouchers_Consume_Or_Recharge.UserID;
                                    Model_b011_InfoAlertMessage.ShopClient_ID = Model_tab_Total_Vouchers_Consume_Or_Recharge.ShopClient_ID;
                                    Model_b011_InfoAlertMessage.Type = "Info_GouWuHongBao";
                                    Model_b011_InfoAlertMessage.TypeTableID = intTableID;
                                    bll_b011_InfoAlertMessage.Add(Model_b011_InfoAlertMessage);
                                    #endregion 增加购物券未处理信息  


                                    Eggsoft_Public_CL.ClassP.WeiXinTuWen First = new Eggsoft_Public_CL.ClassP.WeiXinTuWen("兑换现金通知", "", "自动兑现现金" + (Decimal)Model_tab_GouWuQuan2XianJInEtc.XianJinMoney + "元现金,点击'我'查看", strmywebuyURL);
                                    System.Collections.ArrayList WeiXinTuWens_ArrayList = new System.Collections.ArrayList();
                                    WeiXinTuWens_ArrayList.Add(First);

                                    string strMessageImage = Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTextWinXinMessage(intUserID_ShopClientID, 0, "自动兑现现金" + (Decimal)Model_tab_GouWuQuan2XianJInEtc.XianJinMoney + "元现金");
                                    string[] strCheckReSendList = { "45015", "45047" };
                                    bool exists = ((System.Collections.IList)strCheckReSendList).Contains(strMessageImage);
                                    if(exists)
                                    {
                                        if(boolTempletVisitMessage)
                                        {
                                            Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletWinXinMessage(Int32.Parse(strUserID), intUserID_ShopClientID, WeiXinTuWens_ArrayList);
                                        }
                                    }
                                    #endregion
                                    ///
                                }
                                else if(Model_tab_GouWuQuan2XianJInEtc.ChangeAuto == "Hand")
                                {
                                    EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                                    EggsoftWX.Model.tab_User my_Model_tab_User = my_BLL_tab_User.GetModel(Int32.Parse(strUserID));
                                    if(my_Model_tab_User.Subscribe == false)
                                    {
                                        intErrorCode = -3;///没有关注
                                    }
                                    {
                                        intErrorCode = 3;///申请成功
                                        my_Model_tab_GouWuQuan2XianJInEtc_User.ISpassed = 0;
                                        my_BLL_tab_GouWuQuan2XianJInEtc_User.Add(my_Model_tab_GouWuQuan2XianJInEtc_User);////等待 审批
                                    }
                                }
                            }

                            strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\"}";

                        }

                    }
                    catch(Exception Exceptione)
                    {
                        Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
                        intErrorCode = -1;
                        strReturn = "{\"ErrorCode\":\"" + intErrorCode + "\"}";
                    }
                    finally
                    {

                    }
                    #endregion
                }

                if(HttpContext.Current.Request["jsonp"] != null)//这里是执行是否需要返回JSONP字符串的唯一标识
                {
                    HttpRequest Request = HttpContext.Current.Request;
                    HttpResponse Response = HttpContext.Current.Response;
                    Response.Charset = "utf-8";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    Response.ContentType = ("application/json;charset=UTF-8");
                    string callback = Request["jsonp"];
                    Response.Write(callback + "(" + strReturn + ")");
                    Response.End();//结束后续的操作，直接返回所需要的字符串
                }

            }

            catch(Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione, "doVisiRedBag分销红包");
            }
            finally
            {

            }
            return "";
        }

    }
}
