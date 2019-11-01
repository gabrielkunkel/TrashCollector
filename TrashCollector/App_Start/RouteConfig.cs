﻿using System.Web.Mvc;
using System.Web.Routing;

namespace TrashCollector
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "EditPickUp",
                url: "Customer/EditPickUp",
                defaults: new { controller = "Customer", action = "EditPickUp" }
            );

            routes.MapRoute(
                 name: "RegisterEmployee",
                 url: "Account/RegisterEmployee/",
                 defaults: new { controller = "Account", action = "RegisterEmployee" }
);

            routes.MapRoute(
                 name: "RegisterCustomer",
                 url: "Account/RegisterCustomer/",
                 defaults: new { controller = "Account", action = "RegisterCustomer" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
