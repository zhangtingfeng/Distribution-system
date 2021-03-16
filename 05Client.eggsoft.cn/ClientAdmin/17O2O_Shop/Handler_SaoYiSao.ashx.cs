using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _05Client.eggsoft.cn.ClientAdmin._17O2O_Shop
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
            String strO2oID = context.Request["O2oID"];
            String strPubStringUserListID_ForCheck_ = context.Request["pubStringUserListID_ForCheck_"];
            if (String.IsNullOrEmpty(strO2oID)) return;

            String String_userIDList = Eggsoft_Public_CL.Pub.Get_WeiXinRalationUserID_o2o_List_ID_FromDateBase(strO2oID);

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