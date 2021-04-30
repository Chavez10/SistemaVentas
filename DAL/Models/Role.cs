using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Role
    {
        [Key]
        public int IdRol { get; set; }
        public rol NombreRol { get; set; }

        [ForeignKey("roleId")]
        public ICollection<Usuarios> Usuarios{ get; set; }
    }

    public enum rol
    {
        [Description("Administrador")]
        admin,
        [Description("Usuario")]
        user
    }
}
