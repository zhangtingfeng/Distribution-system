using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;


namespace Eggsoft_Public_CL
{
    /// <summary>
    ///EventKey 的摘要说明
    /// </summary>
    public class WX_EventKey
    {

        public static String Call_EventKey(string strXML, int intShopClientID = 0)
        {
            //
            //TODO: 在此处添加构造函数逻辑

            //
            WX_Model.WX_Model_EventKey myWX_Model = new WX_Model.WX_Model_EventKey();
            myWX_Model = myWX_Model.GetWX_Model_EventKey(strXML);


            String strCall_EventKey = myWX_Model.EventKey.ToLower();
            String strResponseText = "";

            if (strCall_EventKey.IndexOf("call_eventkey") != -1)
            {
                String[] Call_EventKeylist = strCall_EventKey.Split('#');
                strResponseText = Pub_DeMode.Get_Message_(strXML, Call_EventKeylist[1], Call_EventKeylist[2], true, intShopClientID);
            }

            //string str = myWX_Model.ToUserName;
            //String strReposne = "";
            return strResponseText;
        }
    }

}