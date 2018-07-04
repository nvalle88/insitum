namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CREANDO_CAMPO_ESTADO_USUARIO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Estado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Estado");
        }
    }
}
