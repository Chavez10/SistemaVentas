namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeloProducto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productos", "IdVendedor", c => c.Int());
            CreateIndex("dbo.Productos", "IdVendedor");
            AddForeignKey("dbo.Productos", "IdVendedor", "dbo.Usuarios", "idUser");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productos", "IdVendedor", "dbo.Usuarios");
            DropIndex("dbo.Productos", new[] { "IdVendedor" });
            DropColumn("dbo.Productos", "IdVendedor");
        }
    }
}
