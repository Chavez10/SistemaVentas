using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ComprasContext : DbContext
    {
        public DbSet<Role> roles { get; set; }
        public DbSet<Usuarios> usuarios { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
