using System.Data.Entity.ModelConfiguration;
using Loja.Dominio;

namespace Loja.Repositorios.SqlServer.EF.ModelConfiguration
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            Ignore(u => u.Nome);
        }
    }
}