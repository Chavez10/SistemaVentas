using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CarritoCompras
    {
        [Key]
        public int IdCarrito { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public DateTime FechaAgregado { get; set; }
    }
}
