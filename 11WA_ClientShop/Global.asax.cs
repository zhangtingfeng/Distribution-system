using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using _11WA_ClientShop;

namespace _11WA_ClientShop
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码

        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
            // 在出现未处理的错误时运行的代码
            // 在出现未处理的错误时运行的代码         
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
            // Server.ClearError();//清除异常，其他地方不再捕获此异常。
            Application["error"] = error;

            //独占方式，因为文件只能由一个进程写入.
            System.IO.StreamWriter writer = null;
            try
            {
                lock (this)
                {


                    string ip = "用户IP:" + Request.UserHostAddress;
                    string line = "-----------------------------------------------------";

                    string log = "<p style='font-size:9pt;'><br>" + line + "<br><font color=red>" + errortime + "&nbsp;&nbsp;" + erroraddr + "</font><br><font color=green>" + "<br/>" + ip + errorinfo + "<br>" + errorsource + "<br>" + errortrace.Replace("\r\n", "<br>") + "</font></p>";
                    //writer.WriteLine(log);
                    Eggsoft.Common.debug_Log.Call_WriteLog(log, "Application_Error", "程序报错");

                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();

            }


            Server.ClearError();

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码
            // 在新会话启动时运行的代码 
            Session.Timeout = 600; //60 是1小时  600  10小时
        }




        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //遍历Post参数，隐藏域除外
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

    }
}
