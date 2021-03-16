#region Usings
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web.Security;
#endregion

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class Session
    {
        static public string getQueryString(string Key)
        {
            String strgetQueryString = System.Web.HttpContext.Current.Request.QueryString[Key];
            if (strgetQueryString == null) strgetQueryString = "";
            return strgetQueryString;
        }

        static public void Add(string Key, string Value)
        {
            // Create an HttpOnly cookie.  
            // HttpCookie myHttpOnlyCookie = new HttpCookie(Key, Value);

            // Setting the HttpOnly value to true, makes  
            // this cookie accessible only to ASP.NET.  

            //myHttpOnlyCookie.HttpOnly = true;
            //myHttpOnlyCookie.Name = Key;
            //Response.AppendCookie(myHttpOnlyCookie);  
            //HttpContext.Current.Response.AppendCookie(myHttpOnlyCookie);


            System.Web.HttpContext.Current.Session[Key] = Value;
        }

        static public bool Exists(string Key)
        {
            try
            {
                bool b = true;
                if ((System.Web.HttpContext.Current.Session == null) || System.Web.HttpContext.Current.Session[Key] == null || System.Web.HttpContext.Current.Session[Key].ToString() == "")
                {
                    b = false;
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(Key + " " + ex.Message);
            }
        }

        static public void Delete(string Key)
        {
            System.Web.HttpContext.Current.Session[Key] = "";
        }

        static public string Read(string Key)
        {
            if (Exists(Key))
            {
                return System.Web.HttpContext.Current.Session[Key].ToString();
            }
            else
            {
                return "";
            }
        }

        static public string ReadAll()
        {
            string ret = "";
            for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
            {
                ret += "Key:" + HttpContext.Current.Session.Keys[i] + "|";
            }
            return ret;
        }
        static public void SessionStatus()
        {
            if (HttpContext.Current.Session["User_Name"] == null)
            {

                Eggsoft.Common.JsUtil.ShowMsgNew("登陆超时，请重新登录", "/");
            }
            return;
        }

    }


    public class Cookie
    {

        static public void Add(string strRoot, string strKey, string strValue)
        {
            System.Web.HttpCookie cookie;//初使化并设置Cookie的名称
            cookie = System.Web.HttpContext.Current.Request.Cookies[strRoot];
            if (cookie != null)
            {
                if (Exists(strRoot, strKey))
                {
                    Delete(strRoot, strKey);
                }
            }
            else
            {
                cookie = new System.Web.HttpCookie(strRoot);//初使化并设置Cookie的名称
            }

            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(3, 12, 0, 0, 0);//过期时间为365天 1分钟
            cookie.Expires = dt.Add(ts);//设置过期时间
            cookie.Values.Add(strKey, strValue);
            System.Web.HttpContext.Current.Response.AppendCookie(cookie);
        }

        static public bool Exists(string strRoot, string strKey)
        {
            try
            {
                bool b = true;


                //获取客户端的Cookie对象
                HttpCookie cok = System.Web.HttpContext.Current.Request.Cookies[strRoot];

                if (cok != null)
                {
                    if (cok[strKey] == null || cok[strKey] == "")
                    {
                        b = false;
                    }
                }
                else
                {
                    b = false;
                }



                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(strKey + " " + ex.Message);
            }
        }

        static public void Delete(string strRoot, string strKey)
        {
            System.Web.HttpCookie cok = System.Web.HttpContext.Current.Request.Cookies[strRoot];
            if (cok != null)
            {

                cok.Values.Remove(strKey);//移除键值为userid的值
                //}
                //else
                //{
                //    TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                //    cok.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
                //}
                System.Web.HttpContext.Current.Response.AppendCookie(cok);
            }
        }

        static public string Read(string strRoot, string strKey)
        {

            string strRead = "";

            if ((System.Web.HttpContext.Current != null) && (System.Web.HttpContext.Current.Request.Cookies[strRoot] != null))
            {
                strRead = HttpContext.Current.Request.Cookies[strRoot][strKey];//输出全部的值
            }
            return strRead;
        }


    }
}
