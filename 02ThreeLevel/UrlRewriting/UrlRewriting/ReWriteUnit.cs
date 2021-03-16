namespace UrlRewriting
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web;

    public class ReWriteUnit
    {
        internal static string ResolveUrl(string appPath, string url)
        {
            if ((url.Length == 0) || (url[0] != '~'))
            {
                return url;
            }
            if (url.Length == 1)
            {
                return appPath;
            }
            if ((url[1] == '/') || (url[1] == '\\'))
            {
                if (appPath.Length > 1)
                {
                    return (appPath + "/" + url.Substring(2));
                }
                return ("/" + url.Substring(2));
            }
            if (appPath.Length > 1)
            {
                return (appPath + "/" + url.Substring(1));
            }
            return (appPath + url.Substring(1));
        }

        internal static void RewriteUrl(HttpContext context, string sendToUrl)
        {
            string str;
            string str2;
            RewriteUrl(context, sendToUrl, out str, out str2);
        }

        //ztf  检查是否有重复
        public static string UrlQueryStringDelete(string sendToUrl)
        {
            string strQueryString = String.Empty;

            int intpos = sendToUrl.IndexOf("?");
            string strPre = sendToUrl.Substring(0, intpos);
            string strNext = sendToUrl.Remove(0, intpos+1);
            String[] strSplit = strNext.Split('&');

            string strReturn = "";
            string strCheck = "";

            for (int i = 0; i < strSplit.Length; i++)
            { 
                string[] streach= strSplit[i].Split('=');


                if (strCheck.IndexOf(streach[0].ToLower()) == -1)
                {
                    if (i != 0)
                    {
                        strCheck += "&";
                        strReturn += "&";
                    }
                    strCheck += streach[0].ToLower() + "=" + streach[1];
                    strReturn += streach[0] + "=" + streach[1];

                    
                }            
            }
            return strPre+"?"+strReturn;
        }

        //ztf 


        internal static void RewriteUrl(HttpContext context, string sendToUrl, out string sendToUrlLessQString, out string filePath)
        {
            if (context.Request.QueryString.Count > 0)
            {
                if (sendToUrl.IndexOf('?') != -1)
                {
                    sendToUrl = sendToUrl + "&" + context.Request.QueryString.ToString();
                    sendToUrl=UrlQueryStringDelete(sendToUrl);
                }
                else
                {
                    sendToUrl = sendToUrl + "?" + context.Request.QueryString.ToString();
                }
            }
            string queryString = string.Empty;
            sendToUrlLessQString = sendToUrl;
            if (sendToUrl.IndexOf('?') > 0)
            {
                sendToUrlLessQString = sendToUrl.Substring(0, sendToUrl.IndexOf('?'));
                queryString = sendToUrl.Substring(sendToUrl.IndexOf('?') + 1);
            }
            filePath = string.Empty;
            filePath = context.Server.MapPath(sendToUrlLessQString);
            context.RewritePath(sendToUrlLessQString, string.Empty, queryString);
        }
    }
}

