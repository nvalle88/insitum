namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregandofechaFin_DetalleFin_Estado_Acciones1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accions", "FechaFin", c => c.DateTime());
            AlterColumn("dbo.Accions", "DetalleFin", c => c.String());
            AlterColumn("dbo.Accions", "Estado", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accions", "Estado", c => c.Int(nullable: true));
            AlterColumn("dbo.Accions", "DetalleFin", c => c.String(nullable: true));
            AlterColumn("dbo.Accions", "FechaFin", c => c.DateTime(nullable: true));
        }
    }
}
