using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Enums
    {

        public enum rol
        {
            [Description("Administrador")]
            admin = 1,
            [Description("Usuario")]
            user = 2,
            [Description("Vendedor")]
            vendedor = 3
        }

        public enum category
        {
            [Description("Tecnología")]
            tecnologia = 1,
            [Description("Entretenimiento")]
            entretenimiento = 2,
            [Description("Deportes")]
            deporte = 3,
            [Description("Alimentos")]
            alimento = 4,
            [Description("Mascotas")]
            mascota = 5,
            [Description("Ropa/Calzado")]
            ropa_calzado = 6,
            [Description("Joyería")]
            joyeria = 7,
            [Description("Jardinería")]
            jardineria = 8,
            [Description("Herramientas")]
            herramientas = 9,
            [Description("Accesorios")]
            accesorios = 10,
            [Description("Carteras/Bolsos")]
            carteras_bolso = 11
        }



    }
}
