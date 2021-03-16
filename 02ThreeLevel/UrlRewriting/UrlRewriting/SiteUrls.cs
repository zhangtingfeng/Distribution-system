namespace UrlRewriting
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Caching;
    using System.Xml;
    using System.Configuration;

    public class SiteUrls
    {
        private NameValueCollection _Paths;
        private ArrayList _Urls;
        private string SiteUrlsFile = HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["SiteUrls"]);

        public SiteUrls()
        {
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath == "/")
            {
                applicationPath = string.Empty;
            }
            this.Urls = new ArrayList();
            this.Paths = new NameValueCollection();
            this.Paths.Add("root", applicationPath);
            XmlDocument document = new XmlDocument();
            document.Load(this.SiteUrlsFile);
            foreach (XmlNode node2 in document.SelectSingleNode("RewriterConfig").ChildNodes)
            {
                if (node2.Name == "RewriterRule")
                {
                    XmlElement element = (XmlElement) node2;
                    string innerText = node2.SelectSingleNode("LookFor").InnerText;
                    string sendTo = node2.SelectSingleNode("SendTo").InnerText;
                    if ((innerText != "") && (sendTo != ""))
                    {
                        this.Urls.Add(new URLRewrite(innerText, sendTo));
                    }
                }
            }
        }

        public static SiteUrls GetSiteUrls()
        {
          
            string key = "SiteUrls";
            SiteUrls urls = HttpContext.Current.Cache[key] as SiteUrls;
            if (urls == null)
            {
                urls = new SiteUrls();
                HttpContext.Current.Cache.Insert(key, urls, new CacheDependency(urls.SiteUrlsFile), DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, null);
            }
            return urls;
        }

        public URLRewrite Math(string url, ArrayList urls)
        {
            foreach (URLRewrite rewrite in urls)
            {
                if (Regex.IsMatch(url, rewrite.LookFor, RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    return rewrite;
                }
            }
            return null;
        }

        public string Math(string url, ArrayList urls, HttpApplication app)
        {
            foreach (URLRewrite rewrite in urls)
            {
                Regex regex = new Regex("^" + ReWriteUnit.ResolveUrl(app.Context.Request.ApplicationPath, rewrite.LookFor) + "$", RegexOptions.IgnoreCase);
                if (regex.IsMatch(url))
                {
                    return ReWriteUnit.ResolveUrl(app.Context.Request.ApplicationPath, regex.Replace(url, rewrite.SendTo));
                }
            }
            return url;
        }

        public NameValueCollection Paths
        {
            get
            {
                return this._Paths;
            }
            set
            {
                this._Paths = value;
            }
        }

        public ArrayList Urls
        {
            get
            {
                return this._Urls;
            }
            set
            {
                this._Urls = value;
            }
        }
    }
}

