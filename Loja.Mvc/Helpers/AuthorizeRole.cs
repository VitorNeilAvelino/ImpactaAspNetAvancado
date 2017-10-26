using System.Web.Mvc;
using Loja.Dominio;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace Loja.Mvc.Helpers
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params Perfil[] perfis)
        {
            foreach (var perfil in perfis)
            {
                Roles += perfil.ToString() + ",";
            }
        }
        
        //public Perfil Perfil { get; set; }

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    base.Roles = Perfil.ToString();

        //    base.OnAuthorization(filterContext);
        //}
    }
}