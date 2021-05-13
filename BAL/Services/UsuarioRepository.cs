using BAL.IServices;
using DAL.Helpers;
using DAL.Models;
using DAL.ViewModels;
using SistemaVentas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.Models.Enums;

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

        public List<RolesDetalle> getRolesDetalles()
        {
            var lista = new List<RolesDetalle>();

            foreach (var role in Enum.GetValues(typeof(rol)))
            {
                var nombre = GeneralHelper.GetDescriptionFromEnumValue((rol)role);
                var total = context.usuarios.Where(x=>x.roleId == (int)role).Count();

                lista.Add(new RolesDetalle { rolName = nombre, totalUsers = total });
            }

            return lista;
        }

        public IEnumerable<AgregarUsuario> GetUsuariosLists(DataTableJS request)
        {
            var UserInfo = context.UserInfo.ToList();
            var usuario = context.usuarios.ToList();


            var lista = usuario.Select(x => new AgregarUsuario {
                idUser = x.idUser,
                Apellido = UserInfo.Where(y=>y.idUser == x.idUser).Select(y=>y.ApellidoUsuario).FirstOrDefault(),
                direccion = UserInfo.Where(y => y.idUser == x.idUser).Select(y => y.direccion).FirstOrDefault(),
                documento = UserInfo.Where(y => y.idUser == x.idUser).Select(y => y.documento).FirstOrDefault(),
                email = x.email,
                FechaNacimiento = UserInfo.Where(y => y.idUser == x.idUser).Select(y => y.FechaNacimiento).FirstOrDefault(),
                idUserInfo = UserInfo.Where(y => y.idUser == x.idUser).Select(y => y.IdUserInformation).FirstOrDefault(),
                Nombre = UserInfo.Where(y => y.idUser == x.idUser).Select(y => y.NombreUsuario).FirstOrDefault(),
                pass = x.pass,
                telefono = UserInfo.Where(y => y.idUser == x.idUser).Select(y => y.telefono).FirstOrDefault(),
                UserName = x.UserName,
                roleId = x.roleId,
                roleString = GeneralHelper.GetDescriptionFromEnumValue(x.roleId == (int)Enums.rol.admin ? Enums.rol.admin : x.roleId == (int)Enums.rol.user ? Enums.rol.user : Enums.rol.vendedor)
            });

            //var lista = UserInfo.Select(x => new AgregarUsuario
            //{
            //    idUser = x.IdUserInformation,
            //    Apellido = x.ApellidoUsuario,
            //    direccion = x.direccion,
            //    documento = x.documento,
            //    email = UsersList.Where(y => y.idUser == x.idUser).Select(y => y.email).FirstOrDefault(),
            //    FechaNacimiento = x.FechaNacimiento,
            //    idUserInfo = x.IdUserInformation,
            //    Nombre = x.NombreUsuario,
            //    pass = UsersList.Where(y => y.idUser == x.idUser).Select(y => y.pass).FirstOrDefault(),
            //    telefono = x.telefono,
            //    UserName = UsersList.Where(y => y.idUser == x.idUser).Select(y => y.UserName).FirstOrDefault(),
            //    roleId = UsersList.Where(y => y.idUser == x.idUser).Select(y => y.roleId).FirstOrDefault()
            //});

            if (!string.IsNullOrEmpty(request.Search.Value))
            {
                var texto = request.Search.Value.ToLower();
                lista = lista.Where(x => (!string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToLower().Contains(texto)) || (!string.IsNullOrEmpty(x.Apellido) && x.Apellido.ToLower().Contains(texto))
                                    || (!string.IsNullOrEmpty(x.UserName) && x.UserName.ToLower().Contains(texto))
                                    || (!string.IsNullOrEmpty(x.email) && x.email.ToLower().Contains(texto)));
            }

            var result = lista.OrderByDescending(c => c.Nombre).Skip(request.Start).Take(request.Length).ToList();

            return result;
        }

        public Usuarios GetUsuario(int? id)
        {
            Usuarios usuario = context.usuarios.Find(id);
            return usuario;
        }

        public async Task<bool> UserDelete(int? id)
        {
            try
            {
                UserInfo info = context.UserInfo.Find(id);
                Usuarios usu = context.usuarios.Find(id);

                context.UserInfo.Remove(info);
                await context.SaveChangesAsync();

                context.usuarios.Remove(usu);
                await context.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
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
                    roleId = (int)rol.user
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

        public async Task<bool> EditUser(ActualizarUsuario modelo)
        {
            bool success;

            try
            {
                var usu_ = context.usuarios.Find(modelo.idUser);
                usu_.UserName = modelo.UserName;
                usu_.email = modelo.email;
                usu_.pass = modelo.pass;
                usu_.roleId = modelo.rol;

                var _us = await EditUsu(usu_);

                var info = context.UserInfo.Find(modelo.idUser);
                info.NombreUsuario = modelo.Nombre;
                info.ApellidoUsuario = modelo.Apellido;
                info.direccion = modelo.direccion;
                info.documento = modelo.documento;
                info.FechaNacimiento = modelo.FechaNacimiento;
                info.telefono = modelo.telefono;

                var _info = await EditInfo(info);

                if (_info.IdUserInformation > 0 && _us.idUser > 0)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {

                success = false;
            }

            return success;
        }

        private async Task<Usuarios> CreateUser(Usuarios model)
        {
            context.usuarios.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        private async Task<Usuarios> EditUsu(Usuarios usu)
        {
            context.Entry(usu).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return usu;
        }

        private async Task<UserInfo> EditInfo(UserInfo info)
        {
            context.Entry(info).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return info;
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
