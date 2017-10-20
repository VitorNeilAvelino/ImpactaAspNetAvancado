using Microsoft.AspNetCore.Mvc;
using Empresa.Repositorios.SqlServer;
using System.Linq;
using Empresa.Dominio;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace Empresa.Mvc.Controllers
{
    public class ContatosController : Controller
    {
        private readonly EmpresaDbContext _db;
        private readonly IDataProtector _protectorProvider;

        public ContatosController(EmpresaDbContext db, IDataProtectionProvider protectionProvider, 
            IConfiguration configuracao)
        {
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(configuracao.GetSection("ChaveCriptografia").Value);
        }

        public IActionResult Index()
        {
            return View(_db.Contatos.ToList());
        }

        public IActionResult Create()
        {
            //var podeCriar = User.HasClaim("Contato", "Criar");

            //if (!podeCriar)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

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

            contato.Senha = _protectorProvider.Protect(contato.Senha);

            _db.Contatos.Add(contato);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}