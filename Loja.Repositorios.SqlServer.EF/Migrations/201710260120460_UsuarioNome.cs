namespace Loja.Repositorios.SqlServer.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioNome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Nome", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Nome");
        }
    }
}
