using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;

namespace _05Client.eggsoft.cn
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 600; //60 是1小时  600  10小时
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            foreach (string i in this.Request.Form)
            {
                if (i == "__VIEWSTATE") continue;
                this.goErr(this.Request.Form[i].ToString());
            }
            //遍历Get参数。
            foreach (string i in this.Request.QueryString)
            {
                this.goErr(this.Request.QueryString[i].ToString());
            }

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            // 在出现未处理的错误时运行的代码
            // 在出现未处理的错误时运行的代码        
            Exception ex = Server.GetLastError();
            if (ex == null) return;
            Int32 Int32getCode = ((HttpException)(ex)).GetHashCode();


            Exception objErr = Server.GetLastError().GetBaseException();
            string error = string.Empty;
            string errortime = string.Empty;
            string erroraddr = string.Empty;
            string errorinfo = string.Empty;
            string errorsource = string.Empty;
            string errortrace = string.Empty;

            error += "发生时间:" + System.DateTime.Now.ToString() + "<br>";
            errortime = "发生时间:" + System.DateTime.Now.ToString();

            error += "发生异常页: " + Request.Url.ToString() + "<br>";
            erroraddr = "发生异常页: " + Request.Url.ToString();

            error += "异常信息: " + objErr.Message + "<br>";
            errorinfo = "异常信息: " + objErr.Message;

            errorsource = "错误源:" + objErr.Source;
            errortrace = "堆栈信息:" + objErr.StackTrace;
            error += "--------------------------------------<br>";
            Application["error"] = error;

            //独占方式，因为文件只能由一个进程写入.
            System.IO.StreamWriter writer = null;
            try
            {
                lock (this)
                {
                    //文件不存在就创建,true表示追加

                    //writer = new System.IO.StreamWriter(file.FullName, true);

                    string ip = "用户IP:" + Request.UserHostAddress;

                    string log = " Int32getCode:" + Int32getCode + " errortime:" + errortime + " erroraddr:" + erroraddr + " ip:" + ip + " errorinfo:" + errorinfo + " errorsource:" + errorsource + "";
                    //writer.WriteLine(log);
                    Eggsoft.Common.debug_Log.Call_WriteLog(log);

                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();

            }
            //Response.Redirect("~/ErrorPage.aspx");
            System.Web.HttpContext.Current.Response.Redirect("/ClientAdmin/Login.aspx");

            //if (ex is HttpException)
            //{
            //    if ((Int32getCode == 404) || (Int32getCode == 500) || (Int32getCode == 300))//求的网页不存在(注意：410表示永久性，而404表示临时性)；
            //    {
            //        Server.Transfer("/ClientAdmin/Default.aspx", false);
            //    }
            //    else if ((Int32getCode == 410))
            //    {
            //        Server.Transfer("http://eggsoft.cn", false);
            //    }
            //    else if ((Int32getCode > 1000))
            //    {
            //       // Server.Transfer("/ClientAdmin/Default.aspx", false);
            //    }
            //}
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }


        private void goErr(string tm)
        {
            if (SqlFilter2(tm))
                this.Response.End();
        }


        public static bool SqlFilter2(string InText)
        {

            string word = @"select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and";
            //string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join";
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}