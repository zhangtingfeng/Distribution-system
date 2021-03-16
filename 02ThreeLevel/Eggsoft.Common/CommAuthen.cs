using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;


//============================================================================
// tai yi ge  co  官方支持：www.Eggsoft.com 
//
// 多媒体创作部 QQ:605662917
//============================================================================
namespace Eggsoft.Common
{
    public class CommAuthen
    {
        #region 管理员 _Admin
        public static void _Admin_SetAuthen(string UserID)
        {
            Session.Add("Eggsoft__Admin__Users", UserID);
            Eggsoft.Common.Cookie.Add("Eggsoft__Admin__Users", "AdminClient", UserID);

            bool boolppp = Session.Exists("Eggsoft__Admin__Users");
        }

        public static string _Admin_GetAminURL()
        {
            string strAdminURL = ConfigurationManager.AppSettings["AdminURL"];
            return strAdminURL;
        }

        public static string _ClientAdmin_GetAminURL()
        {
            string strClientAdminURL = ConfigurationManager.AppSettings["ClientAdminURL"];
            return strClientAdminURL;
        }


        public static string _Services_Get_Services_URL()
        {
            string strServicesURL = ConfigurationManager.AppSettings["ServicesURL"];
            return strServicesURL;
        }
        public static void _Admin_Check()
        {
            if (!Session.Exists("Eggsoft__Admin__Users"))
            {
                if (Eggsoft.Common.Cookie.Exists("Eggsoft__Admin__Users", "AdminClient"))
                {
                    Session.Add("Eggsoft__Admin__Users", Eggsoft.Common.Cookie.Read("Eggsoft__Admin__Users", "AdminClient"));

                }
                else
                {
                    HttpContext.Current.Response.Redirect(_Admin_GetAminURL() + "/_Admin/Login.aspx");
                }
            }
        }

        public static void _Admin_AdminLogout()
        {
            Session.Delete("Eggsoft__Admin__Users");
            Eggsoft.Common.Cookie.Delete("Eggsoft__Admin__Users", "AdminClient");
            HttpContext.Current.Response.Redirect(_Admin_GetAminURL() + "/_Admin/Login.aspx");
        }
        #endregion



        #region 管理员 ClientAdmin
        public static void Client_SetAuthenAdmin(string UserID)
        {
            Session.Add("webuy8_ClientAdmin_Users", UserID);

            //Eggsoft.Common.Cookie.Add("webuy8_ClientAdmin_Users", "ShoClient", UserID);
        }
        public static void Client_SetAuthenAdmin_ClientUserAccount(string ClientUserAccount)
        {
            Session.Add("webuy8_ClientAdmin_Users_ClientUserAccount", ClientUserAccount);
        }

        public static void Client_CheckAdmin()
        {
            if (!Session.Exists("webuy8_ClientAdmin_Users"))
            {
                //if (Eggsoft.Common.Cookie.Exists("webuy8_ClientAdmin_Users", "ShoClient"))
                //{
                //   string strShoClientID=Eggsoft.Common.Cookie.Read("webuy8_ClientAdmin_Users", "ShoClient");
                //   Session.Add("webuy8_ClientAdmin_Users", strShoClientID);
                //    Eggsoft.Common.Cookie.Delete("webuy8_ClientAdmin_Users", "ShoClient");
                //    Eggsoft.Common.Cookie.Add("webuy8_ClientAdmin_Users", "ShoClient", strShoClientID);
                //}
                //else
                //{
                HttpContext.Current.Response.Redirect(_ClientAdmin_GetAminURL() + "/ClientAdmin/Login.aspx?BackUrl=" + Application.httpFullUrl());
                //}
            }
        }

        public static void Client_AdminLogout()
        {
            Session.Delete("webuy8_ClientAdmin_Users");
            Eggsoft.Common.Cookie.Delete("webuy8_ClientAdmin_Users", "ShoClient");
            HttpContext.Current.Response.Redirect(_ClientAdmin_GetAminURL() + "/ClientAdmin/Login.aspx");
        }
        #endregion


        #region User管理员 UserAdmin
        public static void User_SetAuthenAdmin(string UserID)
        {
            Session.Add("Eggsoft__Users", UserID);
            Eggsoft.Common.Cookie.Add("Eggsoft__Users", "UserID", UserID);

        }

        public static void User_CheckAdmin()
        {
            if (!Session.Exists("Eggsoft__Users"))
            {
                if (Eggsoft.Common.Cookie.Exists("Eggsoft__Users", "UserID"))
                {
                    Session.Add("Eggsoft__Users", Eggsoft.Common.Cookie.Read("Eggsoft__Users", "UserID"));

                }
                else
                {
                    HttpContext.Current.Response.Redirect(_ClientAdmin_GetAminURL() + "/UserAdmin/Login.aspx");
                }
            }
        }

        public static void User_AdminLogout()
        {
            Session.Delete("Eggsoft__Users");
            Eggsoft.Common.Cookie.Delete("Eggsoft__Users", "UserID");

            HttpContext.Current.Response.Redirect(_ClientAdmin_GetAminURL() + "/UserAdmin/Login.aspx");
        }
        #endregion






    }
}
