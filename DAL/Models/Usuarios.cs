using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Usuarios
    {
        [Key]
        public int idUser { get; set; }
        [Required(ErrorMessage = "El campo nombre de usuario es requerido")]
        public string UserName { get; set; }
        public string email { get; set; }
        [Required(ErrorMessage = "El campo contraseña es requerido")]
        public string pass { get; set; }
        public int roleId { get; set; }

        [ForeignKey("idUser")]
        public ICollection<UserInfo> UserInfos { get; set; }

        [ForeignKey("IdUsuario")]
        public ICollection<CarritoCompras> CarritoCompras { get; set; }

        [ForeignKey("IdVendedor")]
        public ICollection<Productos> Productos { get; set; }
    }
}
