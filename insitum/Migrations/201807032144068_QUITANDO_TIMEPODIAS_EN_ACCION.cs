namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QUITANDO_TIMEPODIAS_EN_ACCION : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accions", "TiempoDias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accions", "TiempoDias", c => c.Int(nullable: false));
        }
    }
}
