using System;
using System.Collections.Generic;
////using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Text;
namespace Eggsoft.Common
{
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.Expires = 0;
            context.Response.Clear();
            context.Response.ContentType = "image|jpg,image|png,image|gif,image|bmp,image|ico";

            if (context.Request.UrlReferrer.Host == "localhost")
            {

                context.Response.WriteFile(context.Request.PhysicalPath);
                context.Response.End();
            }
            else
            {

                context.Response.WriteFile(context.Request.PhysicalApplicationPath + "error.jpg");
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
       
    }
}
