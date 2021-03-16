using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using _06ThirdplatForm.eggsoft.cn;

namespace _06ThirdplatForm.eggsoft.cn
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
                    // 写入日志
                    //string year = DateTime.Now.Year.ToString();
                    //string month = DateTime.Now.Month.ToString();
                    //string path = string.Empty;
                    //string filename = DateTime.Now.Day.ToString() + ".html";
                    //path = Server.MapPath("~/ErrorLog/") + year + "/" + month;
                    //如果目录不存在则创建
                    //if (!System.IO.Directory.Exists(path))
                    //{
                    //    System.IO.Directory.CreateDirectory(path);
                    //}
                    //System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);


                    //文件不存在就创建,true表示追加

                    //writer = new System.IO.StreamWriter(file.FullName, true);

                    string ip = "用户IP:" + Request.UserHostAddress;
                    string line = "-----------------------------------------------------";

                    string log = "<p style='font-size:9pt;'><br>" + line + "<br><font color=red>" + errortime + "&nbsp;&nbsp;" + erroraddr + "</font><br><font color=green>" + "<br/>" + ip + errorinfo + "<br>" + errorsource + "<br>" + errortrace.Replace("\r\n", "<br>") + "</font></p>";
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
        

        }


        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码
            Session.Timeout = 1200; //60 是1小时  1200  20小时
        }
    }
}
