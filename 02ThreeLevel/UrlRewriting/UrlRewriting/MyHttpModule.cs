namespace UrlRewriting
{
    using System;
    using System.Collections;
    using System.Web;

    public class MyHttpModule : IHttpModule
    {
        private void app_AuthorizeRequest(object sender, EventArgs e)
        {
       
            HttpApplication app = (HttpApplication) sender;
            //Eggsoft.Common.debug_Log.Call_WriteLog("app_AuthorizeRequest:" + "" + app.Request.Path);
            Rewrite(app.Request.Path, SiteUrls.GetSiteUrls().Urls, app);
            //if ((app.Request.Url.ToString().IndexOf("?") <= 0) && Rewrite(app.Request.Path, SiteUrls.GetSiteUrls().Urls, app))
            //{
            //}
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.AuthorizeRequest += new EventHandler(this.app_AuthorizeRequest);
        }

        public static bool Rewrite(string url, ArrayList urls, HttpApplication app)
        {
            ReWriteUnit.RewriteUrl(app.Context, SiteUrls.GetSiteUrls().Math(url, urls, app));
            return true;
        }

        public static bool TryCache(URLRewrite url, string query, HttpApplication app)
        {
            return false;
        }
    }
}

