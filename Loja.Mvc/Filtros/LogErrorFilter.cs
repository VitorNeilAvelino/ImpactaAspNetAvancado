using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Filtros
{
    public class LogErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //HttpContext.Current.Session["Exception.StackTrace"] = filterContext.Exception.StackTrace;
            Debug.WriteLine(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}