using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Foodie
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API yapılandırması ve hizmetleri

            // Web API yolları
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "rndFood",
                routeTemplate: "api/{controller}/{action}/",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
