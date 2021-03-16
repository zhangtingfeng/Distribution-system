using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web_Service
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("Reports");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //启用路由特性映射
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Track", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "VipCarUserInfoDetail",
                url: "VipCarUserInfo/Detail/{VipId}/{VipCarId}/{UserId}",
                defaults: new { controller = "VipCarUserInfo", action = "Detail" }
            );

            routes.MapRoute(
                name: "UserOrderViewByInfo",
                url: "UserOrder/ViewByInfo/{carNumber}",
                defaults: new { controller = "UserOrder", action = "ViewByInfo" }
            );

            routes.MapRoute(
                name: "PlayMoneyForUser",
                url: "Integral/PlayMoneyForUser/{UserId}/{Type}",
                defaults: new { controller = "Integral", action = "PlayMoneyForUser" }
            );

            routes.MapRoute(
                name: "GetUserLevel",
                url: "Cooperation/AccountDetail/{UserId}",
                defaults: new { controller = "AccountDetail", action = "GetUserLevel" }
            );
        }
    }
}
