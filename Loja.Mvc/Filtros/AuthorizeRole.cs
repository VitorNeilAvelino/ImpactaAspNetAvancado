using Loja.Dominio;
using System.Web.Mvc;

namespace Loja.Mvc.Filtros
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params Perfil[] perfis)
        {
            foreach (var perfil in perfis)
            {
                Roles += perfil + ",";
            }

            //Roles = string.Join(",", perfis.Select(p => Enum.GetName(p.GetType(), p)));
        }
    }
}