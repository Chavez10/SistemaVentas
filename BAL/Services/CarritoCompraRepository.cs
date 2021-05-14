using BAL.IServices;
using DAL.Models;
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
