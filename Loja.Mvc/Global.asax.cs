using System;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Loja.Mvc.Helpers;

namespace Loja.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //_Logger.Error(ex);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var cultura = new CulturaHelper();

            Thread.CurrentThread.CurrentUICulture = cultura.CultureInfo;
            Thread.CurrentThread.CurrentCulture = cultura.CultureInfo;
        }
    }
}