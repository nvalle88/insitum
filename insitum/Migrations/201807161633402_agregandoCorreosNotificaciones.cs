namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregandoCorreosNotificaciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CorreoNotificacion_1", c => c.String());
            AddColumn("dbo.AspNetUsers", "CorreoNotificacion_2", c => c.String());
            AddColumn("dbo.AspNetUsers", "CorreoNotificacion_3", c => c.String());
            AddColumn("dbo.AspNetUsers", "CorreoNotificacion_4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CorreoNotificacion_4");
            DropColumn("dbo.AspNetUsers", "CorreoNotificacion_3");
            DropColumn("dbo.AspNetUsers", "CorreoNotificacion_2");
            DropColumn("dbo.AspNetUsers", "CorreoNotificacion_1");
        }
    }
}
