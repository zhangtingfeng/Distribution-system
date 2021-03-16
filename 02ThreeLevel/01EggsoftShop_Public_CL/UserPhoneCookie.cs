using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Eggsoft_Public_CL
{
    /// <summary>
    ///UserPhoneCookie 的摘要说明
    /// </summary>
    public class UserPhoneCookie
    {
        public static String getUserPhoneCookie()
        {
            string strloginName = "";

            HttpCookie myHttpCookie = System.Web.HttpContext.Current.Request.Cookies["UserPhoneCookie"];
            if (myHttpCookie != null)
            {
                strloginName = myHttpCookie.Value.ToString();
            }
            else
            {
                strloginName = MakeIPTime7ReplacePhone();
                AddToUserPhoneCookie(strloginName);
            }
            return strloginName;
        }


        /// <summary> 
        ///clearUserPhoneCookie 
        /// </summary> 
        /// <param></param> 
        /// <param></param> 
        public static void ClearUserPhoneCookie()
        {

            if (HttpContext.Current.Request.Cookies["UserPhoneCookie"] != null)
            {
                HttpContext.Current.Response.Cookies["UserPhoneCookie"].Expires = DateTime.Now.AddDays(-1);//将这个Cookie过期掉.
            }
        }


        /// <summary> 
        ///产生一个和用户IP相关的时间相关的电话号码 并以7开头  代替 电话号码
        /// </summary> 
        /// <param></param> 
        /// <param></param> 
        public static string MakeIPTime7ReplacePhone()//UserIntergentMark 和手机一一对应  客户端微店码  存于cookies  客户端使用Des 加密
        {
            string strIP = IpHelper.IpHelper.GetWebClientIp();


            string strAddTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string strMakeIPTime7ReplacePhone = "01" + "-" + strIP + "-" + strAddTime;

            IpHelper.IpDetail myIpDetail = IpHelper.IpHelper.Get(strIP, null);
            strMakeIPTime7ReplacePhone = Eggsoft.Common.DESCrypt.Crypt(strMakeIPTime7ReplacePhone);
            return strMakeIPTime7ReplacePhone;
        }



        /// <summary> 
        ///加入UserPhoneCookie
        /// </summary> 
        /// <param></param> 
        /// <param></param> 
        public static void AddToUserPhoneCookie(String strPhone)
        {

            if (HttpContext.Current.Request.Cookies["UserPhoneCookie"] == null)
            {
                HttpCookie oCookie = new HttpCookie("UserPhoneCookie");
                //Set Cookie to expire in 3 
                oCookie.Expires = DateTime.Now.AddYears(3);
                oCookie.Value = strPhone;

                HttpContext.Current.Response.Cookies.Add(oCookie);
            }
            //如果cookie已经存在 
            else
            {

            }
        }

        public static void UpdateUserPhoneCookie(String strPhone)
        {

            if (HttpContext.Current.Request.Cookies["UserPhoneCookie"] != null)
            {
                HttpCookie oCookie = (HttpCookie)HttpContext.Current.Request.Cookies["UserPhoneCookie"];
                oCookie.Expires = DateTime.Now.AddYears(3);
                HttpContext.Current.Response.Cookies["UserPhoneCookie"].Value = strPhone;
            }
        }

    }

}