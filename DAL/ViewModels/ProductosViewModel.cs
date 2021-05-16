using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    class ProductosViewModel
    {
    }

    public class CategoriasDetalle
    {
        public string NombreCategoria { get; set; }
        public int Existencia { get; set; }
    }

    public class DetalleProductosCarrito
    {
        public string NombreProducto { get; set; }
        public string Fecha { get; set; }
        public double Precio { get; set; }
        public int IdProducto { get; set; }
        public int IdCarrito { get; set; }
    }
}
