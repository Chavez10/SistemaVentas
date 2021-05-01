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
    public class LoginRepository : ILoginRepository, IDisposable
    {
        private readonly ComprasContext context;
        
        public LoginRepository(ComprasContext context)
        {
            this.context = context;
        }

        public async Task<Usuarios> IniciarSesion(string nombre,string pass)
        {
            return await context.usuarios.Where(x => (x.UserName == nombre || x.email == nombre)
                           && x.pass == pass).FirstOrDefaultAsync();
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
