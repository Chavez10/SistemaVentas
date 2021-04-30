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

                context.usuarios.Add(user_);
                //context.Entry(user_).State = System.Data.Entity.EntityState.Added;
                await context.SaveChangesAsync();

                var infos_ = new UserInfo
                {
                    NombreUsuario = modelo.NombreUsuario,
                    ApellidoUsuario = modelo.ApellidoUsuario,
                    FechaNacimiento = modelo.FechaNacimiento,
                    direccion = modelo.direccion,
                    idUser = user_.idUser
                };
                await context.SaveChangesAsync();

                sucess = true;
            }
            catch (Exception ex)
            {
                sucess = false;
            }

            return sucess;

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
