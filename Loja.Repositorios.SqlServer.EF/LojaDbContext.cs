using Loja.Dominio;
using Loja.Repositorios.SqlServer.EF.Migrations;
using Loja.Repositorios.SqlServer.EF.ModelConfiguration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Loja.Repositorios.SqlServer.EF
{
    public class LojaDbContext : IdentityDbContext<Usuario>//DbContext
    {
        public LojaDbContext() : base("name=lojaConnectionString")
        {
            //pág. 191 - estratégias de inicialização.
            //Database.SetInitializer(new LojaDbInitializer());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<LojaDbContext, Configuration>());
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        
        // Já é realizado na herança acima.
        //public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Deve vir primeiro, senão as tabelas do Identity não são criadas.
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<IdentityRole>().ToTable("Perfil");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsuarioPerfis");
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsuarioLogins");
            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsuarioPermissoes");

            // Apenas para .NET Core
            //modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("PerfilPermissoes");
            //modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsuarioTokens");

            modelBuilder.Configurations.Add(new ProdutoConfiguration());
            modelBuilder.Configurations.Add(new ProdutoImagemConfiguration());
            modelBuilder.Configurations.Add(new CategoriaConfiguration());
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
        }
    }
}