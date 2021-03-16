using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _05Client.eggsoft.cn.ClientAdmin._10tab_ShopClient
{
    /// <summary>
    /// ContactUsHandler 的摘要说明
    /// </summary>
    public class ContactUsHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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