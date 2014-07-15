using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FossLock.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProductFeatureRoute",
                url: "Product/{productId}/Feature/{action}/{featureId}",
                defaults: new
                {
                    controller = "ProductFeature",
                    productId = UrlParameter.Optional,
                    action = "Index",
                    featureId = UrlParameter.Optional
                },
                constraints: new
                {
                    productId = @"^\d+$"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
