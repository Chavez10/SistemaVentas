namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class model_userinfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "NombreUsuario", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "ApellidoUsuario", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "direccion", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "FechaNacimiento", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "telefono", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "documento", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "documento", c => c.String());
            AlterColumn("dbo.UserInfoes", "telefono", c => c.String());
            AlterColumn("dbo.UserInfoes", "FechaNacimiento", c => c.String());
            AlterColumn("dbo.UserInfoes", "direccion", c => c.String());
            AlterColumn("dbo.UserInfoes", "ApellidoUsuario", c => c.String());
            AlterColumn("dbo.UserInfoes", "NombreUsuario", c => c.String());
        }
    }
}
