using InsureApi.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web_Service.Common.Config;

namespace Web_Service
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
             AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["SessionTimeout"] = 600;
            GlobalConfiguration.Configuration.Formatters.Add(new MultipartDataMediaFormatter.FormMultipartEncodedMediaTypeFormatter());
        }
    }
}
