using Owin;
using System.Web.Http;
using Identity.App_Start;

namespace Identity
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder app)
        {
            // Configura Idendity
            IdentityConfig.ConfigureOAuth(app);

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            RouteConfig.Register(config);
            app.UseWebApi(config);            
        }
    }
}
