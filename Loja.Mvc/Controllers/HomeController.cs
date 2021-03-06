﻿using System.Web.Mvc;
using Loja.Mvc.Helpers;

namespace Loja.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {         
            return View();
        }

        public ActionResult DefinirLinguagem(string linguagem)
        {
            Response.Cookies[Cookie.LinguagemSelecionada.ToString()].Value = linguagem;

            return Redirect(Request.UrlReferrer.ToString());
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
    }
}