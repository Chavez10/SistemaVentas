using BAL.IServices;
using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserInfosRepository : IUserInfos, IDisposable
    {

        private readonly ComprasContext context;

        public UserInfosRepository(ComprasContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<UserInfo> GetUserInfos()
        {
            return context.UserInfo.ToList();
        }

        public UserInfo GetUserInfo(int? id)
        {
            UserInfo usuario = context.UserInfo.Find(id);
            return usuario;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        
    }
}
