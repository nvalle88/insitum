namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregandofechaFin_DetalleFin_Estado_Acciones2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accions", "FechaFin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accions", "FechaFin", c => c.DateTime());
        }
    }
}
