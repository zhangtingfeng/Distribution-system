using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _05Client.eggsoft.cn.ClientAdmin._14System_WeiXin
{
    /// <summary>
    /// Handler_SaoYiSao 的摘要说明
    /// </summary>
    public class Handler_SaoYiSao : IHttpHandler
    {
        private const String strOpenIDSessionName = "OpenIDSession";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            String strResult = "0";
            String strShopClientID = context.Request["ID"];
            String strPubStringUserListID_ForCheck_ = context.Request["pubStringUserListID_ForCheck_"];
            if (String.IsNullOrEmpty(strShopClientID)) return;

            String String_userIDList = Eggsoft_Public_CL.Pub.Get_WeiXinRalationUserIDList_ID_FromDateBase(strShopClientID);

            if (strPubStringUserListID_ForCheck_ != String_userIDList)
            {
                strResult = "1";
            }
            else
            {
                strResult = "0";
            }
            string json = strResult;
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}