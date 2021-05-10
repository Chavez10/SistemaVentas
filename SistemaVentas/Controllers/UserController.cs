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

        public UserController()
        {
            this.usuarioRepository = new UsuarioRepository(new ComprasContext());
            this.userInfos = new UserInfosRepository(new ComprasContext());
        }

        public UserController(IUsuarioRepository usuarioRepository, IUserInfos userInfos)
        {
            this.usuarioRepository = usuarioRepository;
            this.userInfos = userInfos;
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
            if(id == null || id == 0)
            {
                return HttpNotFound();
            }
            var usuario = usuarioRepository.GetUsuario(id);
            
            if(usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
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