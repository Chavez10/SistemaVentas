using BAL.IServices;
using DAL.Helpers;
using DAL.Models;
using DAL.ViewModels;
using SistemaVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.Models.Enums;

namespace BAL.Services
{
    public class ProductosRepository : IProductosRepository, IDisposable
    {
        private readonly ComprasContext context;

        public ProductosRepository(ComprasContext context)
        {
            this.context = context;
        }

        public IEnumerable<Productos> GetProductos()
        {
            return context.Productos.ToList();
        }

        public async Task<bool> CreateOrUpdateProducto(Productos model)
        {
            var success = false;
            try
            {

                if(model.IdProducto > 0)
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Added;
                }

                await context.SaveChangesAsync();
                success = true;
            }
            catch{ }

            return success;
        }

        public async Task<List<CategoriasDetalle>> getCategoriasDetalle()
        {
            var lista = new List<CategoriasDetalle>();

            foreach(var cat in Enum.GetValues(typeof(category)))
            {
                var nombre = GeneralHelper.GetDescriptionFromEnumValue((category)cat);
                var cantidad = context.Productos.Where(x => x.category == (category)cat).Count();

                lista.Add(new CategoriasDetalle { NombreCategoria = nombre, Existencia = cantidad });
            }

            return lista;
        }

        public IEnumerable<Productos> ObtenerProductos(DataTableJS request)
        {
            var productos = context.Productos.ToList();

            if (!string.IsNullOrEmpty(request.Search.Value))
            {
                var texto = request.Search.Value.ToLower();

                productos = productos.Where(x=>x.nombreProducto.ToLower().Contains(texto)).ToList();
            }

            var result = productos.OrderByDescending(c => c.nombreProducto).Skip(request.Start).Take(request.Length).ToList();

            return result;
        }

        public async Task<bool> DeleteProductos(int Id)
        {
            bool success = false;
            try
            {
                var model = context.Productos.Find(Id);
                context.Productos.Remove(model);
                await context.SaveChangesAsync();

                success = true;
            }
            catch { }

            return success;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
