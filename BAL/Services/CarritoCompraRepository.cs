using BAL.IServices;
using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class CarritoCompraRepository : ICarritoCompraRepository, IDisposable
    {
        private readonly ComprasContext context;

        public CarritoCompraRepository(ComprasContext context)
        {
            this.context = context;
        }

        public async Task<int> GetTotalbyUser(int IdUsuario)
        {
            return await context.CarritoCompras.Where(x => x.IdUsuario == IdUsuario).CountAsync();
        }

        public async Task<List<DetalleProductosCarrito>> DetalleProductosCarrito(int IdUsuario)
        {
            List<DetalleProductosCarrito> ListaDetalle = new List<DetalleProductosCarrito>();

            var model = context.CarritoCompras.Where(x => x.IdUsuario == IdUsuario).ToList();
            
            foreach(var d in model)
            {
                var item = new DetalleProductosCarrito() {
                    IdProducto = d.IdProducto,
                    Fecha = d.FechaAgregado.ToShortDateString(),
                    NombreProducto = context.Productos.Where(y=>y.IdProducto == d.IdProducto).FirstOrDefault().nombreProducto,
                    Precio = context.Productos.Where(y => y.IdProducto == d.IdProducto).FirstOrDefault().precio,
                    IdCarrito = d.IdCarrito
                };

                ListaDetalle.Add(item);
            }

            return ListaDetalle;
        }

        public async Task<bool> AgregarAlCarrito(int IdUser,int IdProducto)
        {
            bool success = false;
            try
            {
                var model = new CarritoCompras()
                {
                    IdProducto = IdProducto,
                    IdUsuario = IdUser,
                    FechaAgregado = DateTime.Now
                };

                context.Entry(model).State = EntityState.Added;
                await context.SaveChangesAsync();

                success = true;
            }
            catch
            {

            }

            return success;
        }

        public async Task<bool> EliminarDelCarrito(int idCarrito)
        {
            bool success = false;

            try
            {
                var model = context.CarritoCompras.Find(idCarrito);

                context.Entry(model).State = EntityState.Deleted;
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
