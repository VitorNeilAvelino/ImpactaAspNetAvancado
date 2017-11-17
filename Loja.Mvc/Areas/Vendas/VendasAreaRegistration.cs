using System.Web.Http;
using System.Web.Mvc;

namespace Loja.Mvc.Areas.Vendas
{
    public class VendasAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Vendas";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());

            context.Routes.MapHttpRoute(
                name: "VendasDefaultApi",
                routeTemplate: "api/Vendas/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            context.MapRoute(
                "Vendas_default",
                "Vendas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}