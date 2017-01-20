using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PresentConnection.Internship7.Iot.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "manufacturers-list",
                url: "manufacturers",
                defaults: new { controller = "Manufacturer", action = "List" }
            );

            routes.MapRoute(
                name: "manufacturers-create",
                url: "manufacturers/new",
                defaults: new { controller = "Manufacturer", action = "Create" }
            );

            routes.MapRoute(
                name: "manufacturer-edit",
                url: "manufacturers/{id}",
                defaults: new { controller = "Manufacturer", action = "Update" }
            );

            routes.MapRoute(
                name: "manufacturer-delete",
                url: "manufacturers/{id}/delete",
                defaults: new { controller = "Manufacturer", action = "Delete" }
            );

            routes.MapRoute(
                name: "devices-list",
                url: "devices",
                defaults: new { controller = "Device", action = "List" }
            );

            routes.MapRoute(
                name: "devices-create",
                url: "devices/new",
                defaults: new { controller = "Device", action = "Create" }
            );

            routes.MapRoute(
                name: "device-edit",
                url: "devices/{id}",
                defaults: new { controller = "Device", action = "Update" }
            );

            routes.MapRoute(
                name: "device-delete",
                url: "devices/{id}/delete",
                defaults: new { controller = "Device", action = "Delete" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
