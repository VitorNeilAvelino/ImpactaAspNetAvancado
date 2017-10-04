using System.Web.Mvc;

namespace Loja.Mvc.Filtros
{
    public class LogErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                var controller = filterContext.RouteData.Values["controller"].ToString();
                var action = filterContext.RouteData.Values["action"].ToString();
                var loggerName = $"{controller}Controller.{action}";

                log4net.LogManager.GetLogger(loggerName).Error(string.Empty, filterContext.Exception);
            }
        }
    }
}