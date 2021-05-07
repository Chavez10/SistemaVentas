namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        IdProducto = c.Int(nullable: false, identity: true),
                        nombreProducto = c.String(),
                        description = c.String(),
                        category = c.Int(nullable: false),
                        photo = c.String(),
                        cantidad = c.Int(nullable: false),
                        precio = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdProducto);
            
            CreateIndex("dbo.CarritoCompras", "IdProducto");
            AddForeignKey("dbo.CarritoCompras", "IdProducto", "dbo.Productos", "IdProducto", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarritoCompras", "IdProducto", "dbo.Productos");
            DropIndex("dbo.CarritoCompras", new[] { "IdProducto" });
            DropTable("dbo.Productos");
        }
    }
}
