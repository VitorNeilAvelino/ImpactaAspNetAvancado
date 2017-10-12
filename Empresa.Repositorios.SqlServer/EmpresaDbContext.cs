using Empresa.Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Repositorios.SqlServer
{
    public class EmpresaDbContext : IdentityDbContext<Usuario>
    {
        public EmpresaDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Deve vir primeiro.
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Usuario>().ToTable("Usuario");
            //modelBuilder.Entity<IdentityRole>().ToTable("Perfil");            
        }
    }
}