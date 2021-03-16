using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using _03WAWapShop_Oliver.Status;

namespace _03WAWapShop_Oliver
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            ///Quartz定时调度CronTrigger时间配置格式说明与实例
            //开启定时任务调度
            //JobHelper.StartJob<MyJob>("0 0/1 * * * ?");//每隔一分钟执行一次任务
            JobHelper.StartJob<MyJobDoYunYingZhongXin28EveryDay>("0 20 9  * * ?");//每天9点触发

            //JobHelper.StartJob<MyJobDoYunYingZhongXin5MinEveryDay>("0 0/1 * * * ?");//每隔5分钟执行一次任务
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
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
                lock(this)
                {
                    string ip = "用户IP:" + Request.UserHostAddress;
                    string line = "-----------------------------------------------------";

                    string log = "<p style='font-size:9pt;'><br>" + line + "<br><font color=red>" + errortime + "&nbsp;&nbsp;" + erroraddr + "</font><br><font color=green>" + "<br/>" + ip + errorinfo + "<br>" + errorsource + "<br>" + errortrace.Replace("\r\n", "<br>") + "</font></p>";
                    Eggsoft.Common.debug_Log.Call_WriteLog(log);
                }
            }
            finally
            {
                if(writer != null)
                    writer.Close();

            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            //下面的代码是关键，可解决IIS应用程序池自动回收的问题 

            Thread.Sleep(1000);

            //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start 

            string url = "http://oliver.eggsoft.cn/Status/WebForm1.aspx";
            //url = HttpContext.Current.Request.Url.Host.ToString()+ "/Status/WebForm1.aspx";
            Eggsoft.Common.debug_Log.Call_WriteLog("这里设置你的web地址", "Application_End");
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流 
        }
    }
}