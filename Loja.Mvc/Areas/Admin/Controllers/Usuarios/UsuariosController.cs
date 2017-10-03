using Loja.Mvc.Areas.Vendas.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Loja.Mvc.Areas.Admin.Controllers.Usuarios
{
    public class UsuariosController : Controller
    {
        // GET: Admin/Usuarios
        public ActionResult Index()
        {
            return View(new List<ProdutoViewModel> { new ProdutoViewModel() });
        }
    }
}