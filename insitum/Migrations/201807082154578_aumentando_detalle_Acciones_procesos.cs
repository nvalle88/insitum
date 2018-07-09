namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aumentando_detalle_Acciones_procesos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accions", "Detalle", c => c.String(nullable: false));
            AlterColumn("dbo.Procesoes", "Detalle", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Procesoes", "Detalle", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Accions", "Detalle", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
