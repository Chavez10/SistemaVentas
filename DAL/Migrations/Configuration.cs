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


            //Create User
            var newUser = new Usuarios
            {
                UserName = "Admin",
                email = "admin@admin.com",
                pass = Helpers.GeneralHelper.EncriptarPassword("root"),
                roleId = (int)rol.admin
            };
        }
    }
}
