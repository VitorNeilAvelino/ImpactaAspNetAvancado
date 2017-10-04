using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Loja.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //_Logger.Error(ex);
        }
    }
}