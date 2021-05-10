using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class AgregarUsuario
    {
        public int idUser { get; set; }

        [Required(ErrorMessage ="Campo Usuario es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Campo Email es requerido")]
        public string email { get; set; }
        [Required(ErrorMessage = "Campo Contraseña es requerido")]
        public string pass { get; set; }
        [Required(ErrorMessage = "Campo Nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo Apellido es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Campo Dirección es requerido")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Campo Fecha de Nacimiento es requerido")]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Campo Telefono es requerido")]
        public string telefono { get; set; }
        [Required(ErrorMessage = "Campo Documento es requerido")]
        public string documento { get; set; }
        [Required (ErrorMessage = "El Campo Confirmar Contraseña es requerido")]
        [Compare("pass", ErrorMessage = "Las contraseñas no coinciden")]
        public string confirmPass { get; set; }
        public int? idUserInfo { get; set; }
        public int? roleId { get; set; }
        public string roleString { get; set; }
    }
}
