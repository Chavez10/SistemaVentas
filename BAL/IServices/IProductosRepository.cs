using DAL.Models;
using DAL.ViewModels;
using SistemaVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IProductosRepository : IDisposable
    {
        IEnumerable<Productos> GetProductos();
        IEnumerable<Productos> ObtenerProductos(DataTableJS request);
        Task<bool> CreateOrUpdateProducto(Productos model);
        Task<List<CategoriasDetalle>> getCategoriasDetalle();
        Task<bool> DeleteProductos(int Id);

    }
}
