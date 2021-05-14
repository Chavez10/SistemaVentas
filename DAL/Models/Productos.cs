using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.Models.Enums;

namespace DAL.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        public string nombreProducto { get; set; }
        public string description { get; set; }
        public category category { get; set; }
        public string photo { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public  int? IdVendedor { get; set; }
        [ForeignKey("IdProducto")]
        public ICollection<CarritoCompras> CarritoCompras { get; set; }
    }
}
