namespace DAL.Migrations
{
    using DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using static DAL.Models.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Models.ComprasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Models.ComprasContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //Create Roles
            var role = new List<Role>()
            {
                new Role{NombreRol = rol.admin},
                new Role{NombreRol = rol.user},
                new Role{NombreRol = rol.vendedor}
            };

            role.ForEach(r => context.roles.AddOrUpdate(rl => rl.NombreRol, r));
            context.SaveChanges();


            //Create User Admin
            var newUser = new Usuarios
            {
                UserName = "Admin",
                email = "admin@admin.com",
                pass = Helpers.GeneralHelper.EncriptarPassword("root"),
                roleId = (int)rol.admin
            };
            context.usuarios.Add(newUser);
            context.SaveChanges();

            //Create User Vendedor
            var newUser2 = new Usuarios
            {
                UserName = "vendedor",
                email = "vendedor@vendedor.com",
                pass = Helpers.GeneralHelper.EncriptarPassword("root"),
                roleId = (int)rol.vendedor
            };
            context.usuarios.Add(newUser2);
            context.SaveChanges();


            var prod = new List<Productos>()
            {
                new Productos
                {
                    nombreProducto = "Nintendo Switch",
                    description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vestibulum lorem ex, at molestie dolor fringilla id."+
                                  "Sed gravida ornare dignissim. Aliquam quis est elit. In hac habitasse platea dictumst. Duis sed lectus sapien",
                    category =  Enums.category.tecnologia,
                    photo = "https://laopinion.com/wp-content/uploads/sites/3/2020/09/71CZEg9K3AL._AC_SL1500_.jpg?quality=80&strip=all&w=1200",
                    cantidad = 2,
                    precio = 350.50
                },
                new Productos
                {
                    nombreProducto = "Telefono",
                    description = "Sed gravida ornare dignissim. Aliquam quis est elit. In hac habitasse platea dictumst. Duis sed lectus sapien",
                    category =  Enums.category.tecnologia,
                    photo = "https://i.blogs.es/575d4f/android/840_560.jpg",
                    cantidad = 5,
                    precio = 290.99,
                    IdVendedor = newUser2.idUser
                }
            };

            foreach (var item in prod)
            {
                context.Productos.AddOrUpdate(item);
                context.SaveChanges();
            }
            
        }
    }
}
