using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Loja.Mvc.App_Start.Startup))]

namespace Loja.Mvc.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}