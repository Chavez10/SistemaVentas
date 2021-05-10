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
    public interface IUsuarioRepository : IDisposable
    {
        IEnumerable<Usuarios> GetUsuarios();
        Usuarios GetUsuario(int? id);
        Task<bool> CreateNewUser(AgregarUsuario modelo);
        Task<bool> UserEmailExits(string Email);
        Task<bool> UserNameExits(string name);
        Task<bool> UserDocumentExits(string dui);
        IEnumerable<AgregarUsuario> GetUsuariosLists(DataTableJS request);
    }
}
