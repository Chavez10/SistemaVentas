namespace DAL.Migrations
{
    using DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
            var role = new List<Role>()
            {
                new Role{NombreRol = rol.admin},
                new Role{NombreRol = rol.user}
            };

            role.ForEach(r => context.roles.AddOrUpdate(rl => rl.NombreRol, r));
            context.SaveChanges();
        }
    }
}
