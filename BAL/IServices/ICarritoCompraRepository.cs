using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface ICarritoCompraRepository : IDisposable
    {
        Task<int> GetTotalbyUser(int IdUsuario);
        Task<bool> AgregarAlCarrito(int IdUser, int IdProducto);
        Task<List<DetalleProductosCarrito>> DetalleProductosCarrito(int IdUsuario);
        Task<bool> EliminarDelCarrito(int idCarrito);
    }
}
