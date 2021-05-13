using BAL.IServices;
using BAL.Services;
using DAL.Helpers;
using DAL.Models;
using DAL.ViewModels;
using SistemaVentas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentas.Controllers
{
    public class UserController : Controller
    {

        private IUsuarioRepository usuarioRepository;
        private IUserInfos userInfos;
        private IRoleRepository roleRepository;
        private ComprasContext db_ = new ComprasContext();

        public UserController()
        {
            this.usuarioRepository = new UsuarioRepository(new ComprasContext());
            this.userInfos = new UserInfosRepository(new ComprasContext());
            this.roleRepository = new RoleRepository(new ComprasContext());
        }

        public UserController(IUsuarioRepository usuarioRepository, IUserInfos userInfos, IRoleRepository role)
        {
            this.usuarioRepository = usuarioRepository;
            this.userInfos = userInfos;
            this.roleRepository = role;
        }

        // GET: User
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult UsersList()
        {
            return View();
        }

        public async Task<ActionResult> GetUserList(DataTableJS request)
        {            

            var result = usuarioRepository.GetUsuariosLists(request);

            return Json(new
                {
                    draw = request.Draw,
                    recordsTotal = result.Count(),
                    recordsFiltered = result.Count(),
                    data = result
                });
        }

        public ActionResult UserEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }
            ActualizarUsuario modelo = new ActualizarUsuario();

            using (ComprasContext db = new ComprasContext())
            {
                var usu = db.usuarios.Find(id);
                var info = db.UserInfo.Find(id);

                modelo.Nombre = info.NombreUsuario;
                modelo.Apellido = info.ApellidoUsuario;
                modelo.direccion = info.direccion;
                modelo.documento = info.documento;
                modelo.telefono = info.telefono;
                modelo.FechaNacimiento = info.FechaNacimiento;

                modelo.idUser = usu.idUser;
                modelo.UserName = usu.UserName;
                modelo.email = usu.email;
                modelo.pass = usu.pass;
                modelo.rol = usu.roleId;
                
            }
            var roles = (from r in db_.roles select new {
                id = r.IdRol,
                rol = r.NombreRol
            });

            
            return View(modelo);
        }


        [HttpPost]
        public async Task<ActionResult> UserEdit(FormCollection fr, ActualizarUsuario modelo)
        {
            bool exito = false;
            var rol = fr["IdRol"];
            int idRol = int.Parse(rol);
            modelo.rol = idRol;
            if (ModelState.IsValid)
            {
                modelo.pass = GeneralHelper.EncriptarPassword(modelo.pass);
                var data = await usuarioRepository.EditUser(modelo);
                exito = data;

                if (exito == true)
                    return RedirectToAction("UsersList", "User");
            }
            var roles = (from r in db_.roles
                         select new
                         {
                             id = r.IdRol,
                             rol = r.NombreRol
                         });
            var selectRol = (from s in db_.usuarios
                             where s.idUser == idRol
                             select s.roleId);

            ViewBag.IdRol = new SelectList(roles, "id", "rol", selectRol);
            return View(modelo);
        }

        public ActionResult CreateOrUpdateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateUser(AgregarUsuario modelo)
        {
            bool exito = false;

            if (ModelState.IsValid)
            {
                var EmailExits = await usuarioRepository.UserEmailExits(modelo.email);
                var UserNExits = await usuarioRepository.UserNameExits(modelo.UserName);
                var DocuExist = await usuarioRepository.UserDocumentExits(modelo.documento);
                if (!EmailExits && !UserNExits && !DocuExist)
                {
                    modelo.pass = GeneralHelper.EncriptarPassword(modelo.pass);
                    var data = await usuarioRepository.CreateNewUser(modelo);
                    exito = data;
                }
                
                if(EmailExits)
                    ViewBag.EmailExits = true;
                if (UserNExits)
                    ViewBag.UserNExits = true;
                if (DocuExist)
                    ViewBag.DocuExist = true;


                if (exito == true)
                    return RedirectToAction("Login", "Login");
            }
            
            return View(modelo);

        }
    }
}