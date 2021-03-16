using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Quartz;

namespace _03WAWapShop_Oliver.Status
{
    public class MyJobDoYunYingZhongXin28EveryDay : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            #region 每天运营中心加权分红
            Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红开始执行4", "每天更新");
            Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay mmPub_Default_DoYunYingZhongXin28EveryDay = new Eggsoft_Public_CL.Pub_Default_DoYunYingZhongXin28EveryDay(21);
            Eggsoft.Common.debug_Log.Call_WriteLog("每天运营中心加权分红执行完毕4", "每天更新");
            //string strFilePath = "~/File/21doYunYinZhongXin28Action.txt";
            //Response.Write(Eggsoft.Common.FileFolder.ReadFile((strFilePath)));

            //string strFilePath = "~/File/21do" + DateTime.Now.ToString("yyyyMMdd") + "YunYinZhongXin28Action.txt";
            //Response.Write(Eggsoft.Common.FileFolder.ReadFile((strFilePath)));
            #endregion 每天运营中心加权分红


            Eggsoft.Common.debug_Log.Call_WriteLog(DateTime.Now.ToString(), "MyJobDoYunYingZhongXin28EveryDay", DateTime.Now.ToShortTimeString());

        }



    }

    public class MyJobDoYunYingZhongXin5MinEveryDay : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            #region 每天5分钟
            try
            {
                String str=System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
                string url = HttpContext.Current.Request.Url.Host;
                WebRequest request = WebRequest.Create("~/DoTask_Services.aspx");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));

                reader.ReadToEnd();

                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch (Exception ex)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(ex, "每5Min执行");
            }
            #endregion 每天5分钟



        }



    }
}