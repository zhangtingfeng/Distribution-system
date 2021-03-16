using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// MultiButton_agent 的摘要说明
    /// </summary>
    public class MultiButton_agent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strBody = "";

            try
            {
                string strUserID = Eggsoft.Common.CommUtil.SafeFilter(context.Request.QueryString["strUserID"]);
                string strpage = Eggsoft.Common.CommUtil.SafeFilter(context.Request.Form["page"]);
                string strpagesize = Eggsoft.Common.CommUtil.SafeFilter(context.Request.Form["pagesize"]);
                string strLevelShow = Eggsoft.Common.CommUtil.SafeFilter(context.Request.QueryString["LevelShow"]);

                //
                //string strContext=
                int pIntUserID = 0;
                int.TryParse(strUserID, out pIntUserID);

                int pIntShowpages = 1;
                int.TryParse(strpage, out pIntShowpages);

                int pIntLevelShow = 1;
                int.TryParse(strLevelShow, out pIntLevelShow);

                strBody = Eggsoft_Public_CL.Pub_Agent.GetMySon_AgentMoneyLoadingByPage(pIntUserID, pIntShowpages, pIntLevelShow);//intAgentLevelNum可选参数  标志显示多少级别

            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {


            }

            //string strList = "{\"list\":[";
            //for (int i = 0; i < 8; i++)
            //{
            //    strList += "{\"id\":\"" + i.ToString() + "\",\"name\":\"<table><tr><td></td><td></td></tr><tr><td>46346345634</td></tr></table>\",\"time\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\"}";
            //    if (i != 7) strList += ",";

            //}
            //strList += "]}";

            context.Response.Write(strBody);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}