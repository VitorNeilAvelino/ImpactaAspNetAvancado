using Microsoft.AspNetCore.Mvc;
using Empresa.Repositorios.SqlServer;
using System.Linq;
using Empresa.Dominio;

namespace Empresa.Mvc.Controllers
{
    public class ContatosController : Controller
    {
        EmpresaDbContext _db;

        public ContatosController(EmpresaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {            
            return View(_db.Contatos.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contato contato)
        {
            if (!ModelState.IsValid)
            {
                return View(contato);
            }

            _db.Contatos.Add(contato);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}