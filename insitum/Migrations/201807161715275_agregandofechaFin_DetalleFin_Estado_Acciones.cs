namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregandofechaFin_DetalleFin_Estado_Acciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accions", "FechaFin", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accions", "DetalleFin", c => c.String(nullable: false));
            AddColumn("dbo.Accions", "Estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accions", "Estado");
            DropColumn("dbo.Accions", "DetalleFin");
            DropColumn("dbo.Accions", "FechaFin");
        }
    }
}
