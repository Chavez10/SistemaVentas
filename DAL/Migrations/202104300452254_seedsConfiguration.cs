namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedsConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        IdRol = c.Int(nullable: false, identity: true),
                        NombreRol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdRol);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        idUser = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        email = c.String(),
                        pass = c.String(),
                        roleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idUser)
                .ForeignKey("dbo.Roles", t => t.roleId, cascadeDelete: true)
                .Index(t => t.roleId);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        IdUserInformation = c.Int(nullable: false, identity: true),
                        NombreUsuario = c.String(),
                        ApellidoUsuario = c.String(),
                        direccion = c.String(),
                        FechaNacimiento = c.String(),
                        telefono = c.String(),
                        documento = c.String(),
                        idUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdUserInformation)
                .ForeignKey("dbo.Usuarios", t => t.idUser, cascadeDelete: true)
                .Index(t => t.idUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "roleId", "dbo.Roles");
            DropForeignKey("dbo.UserInfoes", "idUser", "dbo.Usuarios");
            DropIndex("dbo.UserInfoes", new[] { "idUser" });
            DropIndex("dbo.Usuarios", new[] { "roleId" });
            DropTable("dbo.UserInfoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Roles");
        }
    }
}
