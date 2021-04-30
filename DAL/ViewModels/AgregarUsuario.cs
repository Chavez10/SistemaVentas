using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class AgregarUsuario
    {
        public string UserName { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string direccion { get; set; }
        public string FechaNacimiento { get; set; }
        public string telefono { get; set; }
        public string documento { get; set; }
    }
}
