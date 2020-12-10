using System.Web.Http;
using Owin;

namespace LinkApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultWebApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
            appBuilder.UseWebApi(config);
        }
    }
}