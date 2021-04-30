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
       public string NombreUsuario { get; set; }
       public string ApellidoUsuario { get; set; }
       public string direccion { get; set; }
       public string FechaNacimiento { get; set; }
       public string telefono { get; set; }
       public string documento { get; set; }
       public int idUser { get; set; }       
    }
}
