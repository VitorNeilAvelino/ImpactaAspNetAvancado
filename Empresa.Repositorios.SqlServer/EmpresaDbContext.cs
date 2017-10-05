using Empresa.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Repositorios.SqlServer
{
    public class EmpresaDbContext : DbContext
    {
        public EmpresaDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }
    }
}