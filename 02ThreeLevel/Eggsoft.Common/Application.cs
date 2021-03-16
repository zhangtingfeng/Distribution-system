using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Net;

//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{


    /// <summary>
    /// 全局类，当前域名，物理路径
    /// </summary>
    public class Application
    {
        /// <summary>
        /// 【1】获取 完整url （协议名+域名+站点名+文件名+参数）
        /// url= http://www.jb51.net/aaa/bbb.aspx?id=5&name=kelli
        /// </summary>
        public static string httpFullUrl()
        {
            string url = "";
            try
            {
                url = HttpContext.Current.Request.Url.ToString();
            }
            catch {
            }
            return url;
        }

        /// <summary>
        /// 【1】获取 完整url （协议名+域名+站点名+文件名+参数）
        /// url= http://www.jb51.net/aaa/bbb.aspx?id=5&name=kelli
        /// </summary>
        public static string httpFullUrl_BeforeUrlRewriting()
        {
            string url =getwebHttp()+ HttpContext.Current.Request.RawUrl.ToString();
            return url.ToLower();
            
        }


        /// <summary>
        /// 获得url 域名部分路径, http://localhost:8675
        /// </summary>
        public static string getwebHttp()
        {
            string strAbsoluteUri = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            string strAbsolutePath = HttpContext.Current.Request.Url.AbsolutePath.ToString();
            int pos = strAbsoluteUri.IndexOf(strAbsolutePath);
            string strhttpPath = strAbsoluteUri.Substring(0, pos);//从指定的字符位置开始取指定长度 
            return strhttpPath;
        }

        /// <summary>
        /// 获得url 参数 ?id=5&name=kelli
        /// </summary>
        public static string RequestUrlQuery()
        {
            string url = HttpContext.Current.Request.Url.Query;
            return url;
            //url= ?id=5&name=kelli
        }
        /// <summary>
        /// 获得url 域名部分路径, http://localhost/www_dalian/fangyuan.asp 的部分如：http://localhost
        /// </summary>
        public static string httpUrl
        {
            get
            {
                return "http://" + System.Web.HttpContext.Current.Request.Url.Host.ToString();
            }
        }


        /// <summary>
        /// 获得url路径, http://localhost/www_dalian/fangyuan.asp 的app部分如：http://localhost/www_dalian
        /// </summary>
        public static string AppUrl
        {
            get
            {
                return "http://" + System.Web.HttpContext.Current.Request.Url.Host.ToString() + AppSiteName;
            }
        }

        /// <summary>
        /// 获得 http://localhost/www_dalian/fangyuan.asp 的 部分如：www_dalian
        /// </summary>
        public static string AppSiteName
        {
            get
            {
                string SiteAddress = "";
                SiteAddress = System.Web.HttpContext.Current.Request.ApplicationPath.ToString();
                if (System.Web.HttpContext.Current.Request.ApplicationPath.ToString() == "/")
                {
                    SiteAddress = "";
                }
                else
                {
                    SiteAddress = System.Web.HttpContext.Current.Request.ApplicationPath.ToString();
                }
                return SiteAddress.ToString();
            }
        }

        /// <summary>
        /// 获得 http://localhost/www_dalian/fangyuan.asp 的物理路径 如：E:/SinvanProject/CMSHouseDalian
        /// </summary>
        public static string AppMapPath
        {
            get
            {
                string ApplicationPath = System.Web.HttpContext.Current.Server.MapPath("~/");
                if (ApplicationPath.EndsWith("//") == true)
                {
                    ApplicationPath = ApplicationPath.Remove(ApplicationPath.Length - 1);
                }
                return ApplicationPath;
            }
        }


    }
}




//asp.net获取当前网址url

//设当前页完整地址是：http://www.jb51.net/aaa/bbb.aspx?id=5&name=kelli 
//"http://"是协议名 
//"www.jb51.net"是域名 
//"aaa"是站点名 
//"bbb.aspx"是页面名（文件名） 
//"id=5&name=kelli"是参数 
//【1】获取 完整url （协议名+域名+站点名+文件名+参数）

//代码如下:

//string url=Request.Url.ToString(); 
//url= http://www.jb51.net/aaa/bbb.aspx?id=5&name=kelli

//【2】获取 站点名+页面名+参数：

//代码如下:

//string url=Request.RawUrl; 
//(或 string url=Request.Url.PathAndQuery;) 
//url= /aaa/bbb.aspx?id=5&name=kelli

//【3】获取 站点名+页面名：

// 代码如下:

//string url=HttpContext.Current.Request.Url.AbsolutePath; 
//(或 string url= HttpContext.Current.Request.Path;) 
//url= aaa/bbb.aspx

//【4】获取 域名：

//代码如下:

//string url=HttpContext.Current.Request.Url.Host; 
//url= www.jb51.net

//【5】获取 参数：

//代码如下:

//string url= HttpContext.Current.Request.Url.Query; 
//url= ?id=5&name=kelli



// 代码如下:

//Request.RawUrl：获取客户端请求的URL信息（不包括主机和端口）------>/Default2.aspx 
//Request.ApplicationPath：获取服务器上ASP.NET应用程序的虚拟路径。------>/ 
//Request.CurrentExecutionFilePath：获取当前请求的虚拟路径。------>/Default2.aspx 
//Request.Path：获取当前请求的虚拟路径。------>/Default2.aspx 
//Request.PathInfo：取具有URL扩展名的资源的附加路径信息------> 
//Request.PhysicalPath：获取与请求的URL相对应的物理文件系统路径。------>E:\temp\Default2.aspx 
//Request.Url.LocalPath：------>/Default2.aspx 
//Request.Url.AbsoluteUri：------>http://localhost:8080/Default2.aspx 
//Request.Url.AbsolutePath：---------------------------->/Default2.aspx