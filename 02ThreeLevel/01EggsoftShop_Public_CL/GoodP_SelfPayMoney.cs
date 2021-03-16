using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;


namespace Eggsoft_Public_CL
{
    /// <summary>
    ///ClassP 的摘要说明
    /// </summary>
    public class GoodP_SelfPayMoney
    {
        public GoodP_SelfPayMoney()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static void tellShopClientID_UserPayMoney_ByWeiXin_SelfPayMoney(String strOrderNum, String strPayGoodPrice, String strUserID)
        {
            try
            {


                string ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID).ToString();
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(ShopClientID, "TempletPayMessage");///是否可以发模板消息
                EggsoftWX.BLL.tab_ShopClient my_BLL_tab_ShopClient = new EggsoftWX.BLL.tab_ShopClient();
                EggsoftWX.Model.tab_ShopClient my_Model_tab_ShopClient = my_BLL_tab_ShopClient.GetModel(Convert.ToInt32(ShopClientID));

                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                //实例化几个WeiXinTuWen类对象  
                string strTitle = tab_System_And_.getTab_System("CityName") + "微店" + " 用户付款通知！";
                string strImage = "";

                string strDescription = "亲，我们给你发信，是因为用户" + Pub.GetNickName(strUserID) + "付款通知 " + GoodP.GetGoodType(1) + " 自助付款引起！" + "\n";
                strDescription += "微店商品名称：自助付款\n";
                strDescription += "支付金额：" + "￥" + Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice)) + "\n";

                string strClickURL = "";
                strClickURL = "";

                ClassP.WeiXinTuWen First = new ClassP.WeiXinTuWen(strTitle, strImage, strDescription, strClickURL);
                WeiXinTuWens_ArrayList.Add(First);

                if (GoodP.GetShopClientAccptPowerList(Convert.ToInt32(ShopClientID), "WinXinPayedMoney"))
                {
                    string[] stringWeiXinRalationUserIDList = Pub.GetstringWeiXinRalationUserIDList(my_Model_tab_ShopClient.XML);
                    for (int k = 0; k < stringWeiXinRalationUserIDList.Length; k++)
                    {

                        {
                            if (boolTempletPayMessage)
                            {
                                Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(stringWeiXinRalationUserIDList[k]), Int32.Parse(ShopClientID), strClickURL, "用户" + Pub.GetNickName(strUserID) + "自助付款通知", strOrderNum, Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice)), strDescription.Replace("\n", ""));
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "自助付款");
            }
            finally
            {

            }
        }

        public static void tell_UserIGetMoneyPayMoney_ByWeiXin_SelfPayMoney(String strOrderNum, String strPayGoodPrice, String strUserID)
        {
            try
            {
                string ShopClientID = Eggsoft_Public_CL.Pub.GetShopClientIDFromUserID(strUserID).ToString();

                EggsoftWX.BLL.tab_User my_BLL_tab_User = new EggsoftWX.BLL.tab_User();
                EggsoftWX.Model.tab_User my_Model_tab_User = new EggsoftWX.Model.tab_User();
                my_Model_tab_User = my_BLL_tab_User.GetModel("id='" + strUserID + "'");

                string strTitle = "亲，我们已收到您的自助付款订单，金额是：¥" + Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice));
                ArrayList WeiXinTuWens_ArrayList = new ArrayList();
                string strDescription = "亲，我们给你发信，是" + Pub.GetNickName(strUserID) + "已付款通知 " + GoodP.GetGoodType(2) + "引起！" + "\n";
                strDescription += "微店订单号：" + strOrderNum + "\n";
                strDescription += DateTime.Now + "\n";


                int intShopClient_ID = Convert.ToInt32(ShopClientID);
                bool boolTempletPayMessage = Eggsoft_Public_CL.Pub.boolShowPower(intShopClient_ID.ToString(), "TempletPayMessage");///是否可以发模板消息
                if (boolTempletPayMessage)
                {
                    Eggsoft_Public_CL.Pub_GetOpenID_And_.SendTempletPayWinXinMessage(Int32.Parse(strUserID), Convert.ToInt32(ShopClientID), "", "用户" + Pub.GetNickName(strUserID) + "自助付款通知", strOrderNum, Pub.getPubMoney(Convert.ToDecimal(strPayGoodPrice)), strDescription.Replace("\n",""));
                }
            }
            catch (Exception e)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(e, "自助付款");
            }
            finally
            {

            }
        }
    }
}