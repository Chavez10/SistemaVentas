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
    public class UsuarioRepository : IUsuarioRepository, IDisposable
    {

        private readonly ComprasContext context;

        public UsuarioRepository(ComprasContext context)
        {
            this.context = context;
        }

        public IEnumerable<Usuarios> GetUsuarios()
        {
            return context.usuarios.ToList();
        }

        public async Task<bool> UserEmailExits(string Email)
        {
            return await context.usuarios.Where(x => x.email.ToLower().Equals(Email.ToLower())).AnyAsync();
        }
        public async Task<bool> UserNameExits(string name)
        {
            return await context.usuarios.Where(x => x.UserName.ToLower().Equals(name.ToLower())).AnyAsync();
        }
        public async Task<bool> UserDocumentExits(string dui)
        {
            return await context.UserInfo.Where(x => x.documento.ToLower().Equals(dui.ToLower())).AnyAsync();
        }

        public async Task<bool> CreateNewUser(AgregarUsuario modelo)
        {
            bool sucess;
            try
            {
                var user_ = new Usuarios()
                {
                    UserName = modelo.UserName,
                    email = modelo.email,
                    pass = modelo.pass,
                    roleId = context.roles.Where(x => x.NombreRol == rol.user).Select(x => x.IdRol).FirstOrDefault()
                };

                var us_ = await CreateUser(user_);                

                var infos_ = new UserInfo
                {
                    NombreUsuario = modelo.Nombre,
                    ApellidoUsuario = modelo.Apellido,
                    FechaNacimiento = modelo.FechaNacimiento,
                    direccion = modelo.direccion,
                    documento = modelo.documento,
                    telefono = modelo.telefono,
                    idUser = us_.idUser
                };

                var in_ = await CreateUserInfo(infos_);

                if (in_.IdUserInformation > 0 && us_.idUser > 0)
                    sucess = true;
                else
                    sucess = false;
            }
            catch (Exception ex)
            {
                sucess = false;
            }

            return sucess;

        }

        private async Task<Usuarios> CreateUser(Usuarios model)
        {
            context.usuarios.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        private async Task<UserInfo> CreateUserInfo(UserInfo model)
        {
            context.UserInfo.Add(model);
            await context.SaveChangesAsync();

            return model;
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
