using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web.Security;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class DotCookie
    {
        /// <summary>
        /// 创建Cookies
        /// </summary>
        /// <param name="strValue">Cookie 键值</param>
        /// <param name="strDay">Cookie 天数</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.setCookie("主键","键值","天数");</code>
        static public void Add(string strName, string strValue, int strMinute)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(DESCrypt.Crypt(strName));
                Cookie.Expires = DateTime.Now.AddMinutes(strMinute);
                Cookie.Value = DESCrypt.Crypt(strValue);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.getCookie("主键");</code>
        static public string Read(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[DESCrypt.Crypt(strName)];
            if (Cookie != null)
            {
                return DESCrypt.DeCrypt(Cookie.Value);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.delCookie("主键");</code>
        static public void Delete(string strName)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(DESCrypt.Crypt(strName));
                Cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static public bool Exists(string strName)
        { 
            bool b=false;
            if (Read(strName) != null && Read(strName) != "")
            {
                b = true;
            }
            return b;
        }
    }
}
