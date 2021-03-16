using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace Eggsoft_Public_CL
{
    /// <summary>
    ///Subscribe 的摘要说明
    /// </summary>
    public class WX_Subscribe
    {

        public static string Call_Subscribe(int intShopClientID, string strXML)
        {

          


            String[] strSubscribeList = null;

           
            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();

            EggsoftWX.Model.tab_ShopClient_EngineerMode mu = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);
            string strmu = mu.Subscribe;
            if (String.IsNullOrEmpty(strmu) == false)
            {
                strSubscribeList = mu.Subscribe.Split(',');
            }
          

            String strResponseText = "";
            if (strSubscribeList != null)///关注 是空的
            {
                if (strSubscribeList.Length > 0)
                {
                    string strType = strSubscribeList[0];
                    string strResourceID = strSubscribeList[1];
                    strResponseText = Pub_DeMode.Get_Message_(strXML, strType, strResourceID,false, intShopClientID);
                }
            }

            return strResponseText;
        }

        public static string Call_KeyAnswer_Default(int intShopClientID, string strXML)
        {

         



            String[] strSubscribeList = null;

            EggsoftWX.BLL.tab_ShopClient_EngineerMode BLL_tab_ShopClient_EngineerMode = new EggsoftWX.BLL.tab_ShopClient_EngineerMode();

            EggsoftWX.Model.tab_ShopClient_EngineerMode mu = BLL_tab_ShopClient_EngineerMode.GetModel("ShopClientID=" + intShopClientID);
            string strmu = mu.KeyAnswer_Default;
            if (String.IsNullOrEmpty(strmu) == false)
            {
                strSubscribeList = strmu.Split(',');
            }
          
            String strResponseText = "";
            if (strSubscribeList != null)
            {
                if (strSubscribeList.Length > 0)
                {
                    string strType = strSubscribeList[0];
                    string strResourceID = strSubscribeList[1];
                    strResponseText = Pub_DeMode.Get_Message_(strXML, strType, strResourceID, false, intShopClientID);
                }
                else
                {
                    strResponseText = Pub_DeMode.Get_Message_(strXML, "31", "0", false, intShopClientID);
                }
            }
            else
            {
                strResponseText = Pub_DeMode.Get_Message_(strXML, "31", "0", false,intShopClientID);
            }

            return strResponseText;
        }


    }

}