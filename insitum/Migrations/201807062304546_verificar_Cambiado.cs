namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class verificar_Cambiado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Procesoes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Procesoes", "Discriminator");
        }
    }
}
