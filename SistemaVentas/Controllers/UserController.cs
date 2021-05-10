using BAL.IServices;
using BAL.Services;
using DAL.Helpers;
using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
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

            var UserInfo = userInfos.GetUserInfos();
            var UsersList = usuarioRepository.GetUsuarios();
            ViewBag.lista = UsersList.ToList();
            ViewBag.listaInfo = UserInfo.ToList();
            return View();
        }

        public ActionResult UserEdit(int? id)
        {
            if(id == null || id == 0)
            {
                return HttpNotFound();
            }
            Usuarios usuario = usuarioRepository.GetUsuario(id);
            UserInfo userInfo = userInfos.GetUserInfo(id);
            AgregarUsuario modelos = new AgregarUsuario();
            
            if(usuario == null && userInfo == null)
            {
                return HttpNotFound();
            }

            modelos.usuario = usuario;
            modelos.userInfo = userInfo;
            //IEnumerable<SelectListItem> items = roleRepository.GetRoles().Select(r => new SelectListItem
            //{
            //    Value = r.IdRol.ToString(),
            //    Text = r.NombreRol.ToString(),
            //    Selected = usuario.roleId.Equals(id)
            //});

            return View(modelos);
        }


        [HttpPost]
        public async Task<ActionResult> UserEdit(AgregarUsuario modelo)
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
                    var data = await usuarioRepository.EditUser(modelo);
                    exito = data;
                }
                if (EmailExits)
                    ViewBag.EmailExits = true;
                if (UserNExits)
                    ViewBag.UserNExits = true;
                if (DocuExist)
                    ViewBag.DocuExist = true;

                if (exito == true)
                    return RedirectToAction("UsersList", "User");
            }

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