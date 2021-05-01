using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface ILoginRepository : IDisposable
    {
        Task<Usuarios> IniciarSesion(string nombre, string pass);
    }
}
