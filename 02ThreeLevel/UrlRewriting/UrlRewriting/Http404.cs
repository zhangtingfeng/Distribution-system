namespace UrlRewriting
{
    using System;
    using System.Web;

    public class Http404 : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string s = context.Request.RawUrl.ToLower();
            context.Response.Write(s);
        }

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            int index = context.Request.RawUrl.IndexOf("//");
            index = context.Request.RawUrl.IndexOf("/", index + 2, (int) ((context.Request.RawUrl.Length - index) - 2));
            MyHttpModule.Rewrite(context.Request.RawUrl.Substring(index, context.Request.RawUrl.Length - index), SiteUrls.GetSiteUrls().Urls, context.ApplicationInstance);
        }

        public bool IsReusable
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        bool IHttpHandler.IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

