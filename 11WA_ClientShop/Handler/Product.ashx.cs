using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _11WA_ClientShop.Handler
{
    /// <summary>
    /// Product 的摘要说明
    /// </summary>
    public class Product : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string strGoodID = context.Request.QueryString["strGoodID"];
                //
                //string strContext=
                int pIntGoodID = 0;
                int.TryParse(strGoodID, out pIntGoodID);

                string strVisitUserListImgeAndName = Eggsoft_Public_CL.GoodP_MakeHtml.VisitUserListImgeAndName(pIntGoodID);

                context.Response.Write(strVisitUserListImgeAndName);
            }
            catch (Exception Exceptione)
            {
                Eggsoft.Common.debug_Log.Call_WriteLog(Exceptione);
            }
            finally
            {

            }
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