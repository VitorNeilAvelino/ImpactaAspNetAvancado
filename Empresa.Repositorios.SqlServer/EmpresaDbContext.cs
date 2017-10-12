using Empresa.Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Repositorios.SqlServer
{
    public class EmpresaDbContext : DbContext //: IdentityDbContext<Usuario>
    {
        public EmpresaDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Contato> Contatos { get; set; }
        //public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Deve vir primeiro, senão as tabelas do Identity não são criadas.
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contato>().ToTable("Contato");//;
          //  modelBuilder.Entity<Usuario>().ToTable("Usuario")
                //.Property(u => u.Senha)
                //.HasColumnType("BINARY(64)");
                //.Ignore(u => u.Senha);
            
            //modelBuilder.Entity<IdentityRole>().ToTable("Perfil");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsuarioPerfis");
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsuarioLogins");
            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsuarioPermissoes");
            //modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("PerfilPermissoes");
            //modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsuarioTokens");
        }
    }
}