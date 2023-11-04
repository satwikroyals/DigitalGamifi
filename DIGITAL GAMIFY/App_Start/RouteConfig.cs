using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIGITAL_GAMIFY
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();


            routes.MapRoute(
               name: "AdminLogin",
               url: "Admin/{action}",
               defaults: new { controller = "Admin", action = "Login" },
               namespaces: new string[] { "DIGITAL_GAMIFY.Controllers" }   //Add Namespaces to avoid Areas controller names.
           );

            routes.MapRoute(
             name: "BusinessLogin",
             url: "Business/{action}",
             defaults: new { controller = "Business", action = "Login" },
             namespaces: new string[] { "DIGITAL_GAMIFY.Controllers" }   //Add Namespaces to avoid Areas controller names.
         );


            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

          );
        }
    }
}