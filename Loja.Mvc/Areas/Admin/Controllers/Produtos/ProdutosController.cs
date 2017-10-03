using Loja.Mvc.Areas.Vendas.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Loja.Mvc.Areas.Admin.Controllers.Produtos
{
    public class ProdutosController : Controller
    {
        // GET: Admin/Produtos
        public ActionResult Index()
        {
            return View(new List<ProdutoViewModel> { new ProdutoViewModel() });
        }
    }
}