using Loja.Mvc.Helpers;
using Loja.Repositorios.SqlServer.EF;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Loja.Dominio;
using Loja.Mvc.Filtros;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Loja.Mvc.Areas.Vendas.Controllers
{
    //[AuthorizeRole(Perfil.Master)]
    [AuthorizeRole(Perfil.Leiloeiro, Perfil.Comprador)]
    public class LeiloesController : Controller
    {
        private readonly LojaDbContext _db = new LojaDbContext();

        public ActionResult Index()
        {
            return View(Mapeamento.Mapear(_db.Produtos.Where(p => p.EmLeilao).ToList()));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var identity = (ClaimsIdentity)User.Identity;

            if (!identity.HasClaim(Modulo.Leilao.ToString(), Acao.Detalhar.ToString()))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var produto = _db.Produtos.Find(id);

            if (produto == null)

            {
                return HttpNotFound();
            }

            return View(Mapeamento.Mapear(produto));
        }
    }
}