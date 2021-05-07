namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Poducto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarritoCompras",
                c => new
                    {
                        IdCarrito = c.Int(nullable: false, identity: true),
                        IdUsuario = c.Int(nullable: false),
                        IdProducto = c.Int(nullable: false),
                        FechaAgregado = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdCarrito)
                .ForeignKey("dbo.Usuarios", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarritoCompras", "IdUsuario", "dbo.Usuarios");
            DropIndex("dbo.CarritoCompras", new[] { "IdUsuario" });
            DropTable("dbo.CarritoCompras");
        }
    }
}
