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
    }
}
