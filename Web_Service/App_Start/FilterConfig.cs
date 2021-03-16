﻿using InsureApi.Mvc;
using System.Web;
using System.Web.Mvc;

namespace Web_Service
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MvcLogFilter());
            //filters.Add(new MvcExceptionFilter());
            filters.Add(new MvcAuthorizeFilter());
        }
    }
}