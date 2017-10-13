using Empresa.Mvc.ViewModels;
using Empresa.Repositorios.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace Empresa.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmpresaDbContext _db;
        private readonly IDataProtector _protectorProvider;

        public HomeController(EmpresaDbContext db, IDataProtectionProvider protectionProvider)
        {
            _db = db;
            _protectorProvider = protectionProvider.CreateProtector(GetType().GetTypeInfo().Assembly.GetName().Name);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Login()
        {
            //HTML da view na página 193.
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var contato = _db.Contatos.SingleOrDefault(c => c.Email == login.Email
                            && _protectorProvider.Unprotect(c.Senha) == login.Senha);

            if (contato == null)
            {
                // Case sensitive para senha, não para email.
                ModelState.AddModelError(string.Empty, "Email/Senha não encontrados.");

                return View(login);
            }

            // 1. Install-Package Microsoft.AspNetCore.Authentication.Cookies
            // 2. Startup.Configure

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, contato.Nome),
                        new Claim(ClaimTypes.Email, contato.Email),
                        new Claim(ClaimTypes.Role, "Vendedor"),
                        new Claim(ClaimTypes.Role, "Consultor")//,
                        //new Claim("Permissao", "CriarNovoContato")
                    };

            var identidade = new ClaimsIdentity(claims, "EmpresaCookieAuthentication");
            var principal = new ClaimsPrincipal(identidade);

            HttpContext.Authentication.SignInAsync("EmpresaCookieAuthentication", principal);

            return RedirectToAction("Index", "Contatos");            
        }

        public IActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync("EmpresaCookieAuthentication");
                        
            return RedirectToAction("Index");
        }
    }
}