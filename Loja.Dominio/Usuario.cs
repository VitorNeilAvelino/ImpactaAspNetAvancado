using Microsoft.AspNet.Identity.EntityFramework;

namespace Loja.Dominio
{
    public class Usuario : IdentityUser
    {
        public string Nome
        {
            get { return base.UserName; }
            set { base.UserName = value; }
        }

        public string Cpf { get; set; }
    }
}