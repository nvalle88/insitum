namespace insitum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primera : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accions",
                c => new
                    {
                        IdAccion = c.Int(nullable: false, identity: true),
                        Detalle = c.String(nullable: false, maxLength: 1000),
                        FechaInicio = c.DateTime(nullable: false),
                        TiempoDias = c.Int(nullable: false),
                        IdProceso = c.Int(nullable: false),
                        IdTipoAccion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAccion)
                .ForeignKey("dbo.Procesoes", t => t.IdProceso, cascadeDelete: true)
                .ForeignKey("dbo.TipoAccions", t => t.IdTipoAccion, cascadeDelete: true)
                .Index(t => t.IdProceso)
                .Index(t => t.IdTipoAccion);
            
            CreateTable(
                "dbo.Procesoes",
                c => new
                    {
                        IdProceso = c.Int(nullable: false, identity: true),
                        Detalle = c.String(nullable: false, maxLength: 1000),
                        FechaInicio = c.DateTime(nullable: false),
                        NIP = c.String(nullable: false, maxLength: 20),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdProceso)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Identificacion = c.String(nullable: false, maxLength: 13),
                        Nombres = c.String(nullable: false, maxLength: 100),
                        Apellidos = c.String(nullable: false, maxLength: 100),
                        Direccion = c.String(nullable: false, maxLength: 500),
                        Correo = c.String(),
                        TelefonoContacto = c.String(nullable: false, maxLength: 20),
                        IdentificacionConyuge = c.String(nullable: false, maxLength: 13),
                        NombresConyuge = c.String(nullable: false, maxLength: 100),
                        ApellidosConyuge = c.String(nullable: false, maxLength: 100),
                        CorreoConyuge = c.String(),
                        TelefonoConyuge = c.String(nullable: false, maxLength: 20),
                        IdCiudad = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ciudads", t => t.IdCiudad, cascadeDelete: true)
                .Index(t => t.IdCiudad)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Ciudads",
                c => new
                    {
                        IdCiudad = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        IdProvincia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCiudad)
                .ForeignKey("dbo.Provincias", t => t.IdProvincia, cascadeDelete: true)
                .Index(t => t.IdProvincia);
            
            CreateTable(
                "dbo.Provincias",
                c => new
                    {
                        IdProvincia = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.IdProvincia);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TipoAccions",
                c => new
                    {
                        IdTipoAccion = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TiempoDias = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdTipoAccion);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Accions", "IdTipoAccion", "dbo.TipoAccions");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Procesoes", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ciudads", "IdProvincia", "dbo.Provincias");
            DropForeignKey("dbo.AspNetUsers", "IdCiudad", "dbo.Ciudads");
            DropForeignKey("dbo.Accions", "IdProceso", "dbo.Procesoes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Ciudads", new[] { "IdProvincia" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "IdCiudad" });
            DropIndex("dbo.Procesoes", new[] { "Id" });
            DropIndex("dbo.Accions", new[] { "IdTipoAccion" });
            DropIndex("dbo.Accions", new[] { "IdProceso" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TipoAccions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Provincias");
            DropTable("dbo.Ciudads");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Procesoes");
            DropTable("dbo.Accions");
        }
    }
}
