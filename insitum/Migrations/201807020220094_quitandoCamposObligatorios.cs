namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitandoCamposObligatorios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Identificacion", c => c.String(maxLength: 13));
            AlterColumn("dbo.AspNetUsers", "Nombres", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Apellidos", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Direccion", c => c.String(maxLength: 500));
            AlterColumn("dbo.AspNetUsers", "TelefonoContacto", c => c.String(maxLength: 20));
            AlterColumn("dbo.AspNetUsers", "IdentificacionConyuge", c => c.String(maxLength: 13));
            AlterColumn("dbo.AspNetUsers", "NombresConyuge", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "ApellidosConyuge", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "TelefonoConyuge", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "TelefonoConyuge", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.AspNetUsers", "ApellidosConyuge", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "NombresConyuge", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "IdentificacionConyuge", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.AspNetUsers", "TelefonoContacto", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.AspNetUsers", "Direccion", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.AspNetUsers", "Apellidos", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Nombres", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "Identificacion", c => c.String(nullable: false, maxLength: 13));
        }
    }
}
