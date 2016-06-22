using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mvcSample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*As stated earlier, you can keep both the “Convention-based Routing” as well as the “[attribute] routing” under the same web app, 
             * if so then keep the following note in mind.   MapMvcAttributeRoutes() have to call before the Convention-based Routing.*/
            routes.MapMvcAttributeRoutes(); 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

         
        }
    }
}
