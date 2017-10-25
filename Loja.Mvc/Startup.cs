using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Loja.Mvc.Startup))]

namespace Loja.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}