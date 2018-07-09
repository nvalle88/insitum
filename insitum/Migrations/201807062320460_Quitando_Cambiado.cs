namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Quitando_Cambiado : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Procesoes", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Procesoes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
