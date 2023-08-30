using System.Web.Http;

namespace GetEmpStatus
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Comment out or remove the following lines to disable Help Page
            //config.EnableHelpPage();
            //config.EnableHelpPage(WebApiConfigExtensions.DefaultArea);
        }
    }
}
