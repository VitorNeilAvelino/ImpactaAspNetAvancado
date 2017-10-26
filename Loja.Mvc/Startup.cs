using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Loja.Mvc.Startup))]

namespace Loja.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}