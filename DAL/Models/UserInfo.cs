using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserInfo
    {
       [Key]
       public int IdUserInformation { get; set; }
        [Required(ErrorMessage = "Campo Nombre es requerido")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Campo Apellido es requerido")]
        public string ApellidoUsuario { get; set; }
        [Required(ErrorMessage = "Campo Dirección es requerido")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Campo Fecha de Nacimiento es requerido")]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Campo Telefono es requerido")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Campo Documento es requerido")]
        public string documento { get; set; }
        [Required]
        public int idUser { get; set; }       
    }
}
