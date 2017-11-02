using System;
using System.Web;
using System.Web.Mvc;
using Loja.Mvc.Helpers;

namespace Loja.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DefinirLinguagemPadrao();
            return View();
        }

        private void DefinirLinguagemPadrao()
        {
            if (Request.Cookies["linguagemSelecionada"] != null) return;

            var linguagem = CulturaHelper.LinguagemPadrao;

            if (Request.UserLanguages != null && Request.UserLanguages[0] != string.Empty)
            {
                linguagem = Request.UserLanguages[0];
            }

            var linguagemSelecionadaCookie = new HttpCookie("linguagemSelecionada", linguagem);

            linguagemSelecionadaCookie.Expires = DateTime.MaxValue;

            Response.Cookies.Add(linguagemSelecionadaCookie);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DefinirLinguagem(string linguagem)
        {
            Response.Cookies["linguagemSelecionada"].Value = linguagem;

            return RedirectToAction("Index");
        }
    }
}