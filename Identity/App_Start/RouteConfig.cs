using Identity.Filters;
using System.Web.Http;

namespace Identity.App_Start
{
    public static class RouteConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new CustomAuthenticationAttribute());
        }        
    }
}
