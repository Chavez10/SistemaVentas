using BAL.IServices;
using BAL.Services;
using DAL.Models;
using DAL.ViewModels;
using SistemaVentas.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentas.Controllers
{
    public class UserController : Controller
    {

        private IUsuarioRepository usuarioRepository;

        public UserController()
        {
            this.usuarioRepository = new UsuarioRepository(new ComprasContext());
        }

        public UserController(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrUpdateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateOrUpdateUser(AgregarUsuario modelo)
        {
            bool exito = false;

            if (ModelState.IsValid)
            {
                modelo.pass = UserHelper.EncriptarPassword(modelo.pass);
                var data = usuarioRepository.CreateNewUser(modelo);
                exito = data.Result;
            }

            if (exito == true)
                ViewBag.Correcto = true;

            return View(modelo);

        }
    }
}