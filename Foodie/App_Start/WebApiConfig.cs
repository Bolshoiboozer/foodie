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
                name: "getRandomFood",
                routeTemplate: "api/{controller}/{action}/",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "searchFood",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "searchIngredient",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "calculateFood",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "userRegister",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "setAllergens",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "addFavorite",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "deleteFavorite",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "clickedRecipe",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "deleteFromList",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "postAnswers",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "setAlergens",
               routeTemplate: "api/{controller}/{action}/",
               defaults: new { id = RouteParameter.Optional }
           );
        }
    }
}
