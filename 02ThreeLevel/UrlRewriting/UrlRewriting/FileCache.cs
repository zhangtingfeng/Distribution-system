namespace UrlRewriting
{
    using System;
    using System.Collections;
    using System.Web;

    public class FileCache
    {
        public static readonly Hashtable CacheList = Hashtable.Synchronized(new Hashtable());

        public static string ApplicationPath
        {
            get
            {
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (applicationPath == "/")
                {
                    applicationPath = string.Empty;
                }
                return applicationPath;
            }
        }
    }
}

