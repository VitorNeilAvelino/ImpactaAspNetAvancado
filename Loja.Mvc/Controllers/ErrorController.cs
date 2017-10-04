using System.Web.Mvc;

namespace Loja.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        //Se a view Error.cshtml for para dentro da pasta Error.
        public ActionResult Index()
        {
            return View("Error");
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}