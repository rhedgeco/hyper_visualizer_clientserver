using System.Web.Http;
using System.Web.Http.Routing;
using Owin;
using UnityEngine;

namespace LinkApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(config);
        }
    }
}