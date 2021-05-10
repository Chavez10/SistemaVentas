using BAL.IServices;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class RoleRepository : IRoleRepository, IDisposable
    {
        private readonly ComprasContext context;

        public RoleRepository(ComprasContext _contex)
        {
            context = _contex;
        }

        public IEnumerable<Role> GetRoles()
        {
            return context.roles.ToList();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        
    }
}
