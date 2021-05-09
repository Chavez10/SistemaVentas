using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IUserInfos : IDisposable
    {
        IEnumerable<UserInfo> GetUserInfos();
        IList<UserInfo> GetUserInfo(int? id);
    }
}
