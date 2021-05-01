namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposRequeridos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuarios", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "pass", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "pass", c => c.String());
            AlterColumn("dbo.Usuarios", "UserName", c => c.String());
        }
    }
}
