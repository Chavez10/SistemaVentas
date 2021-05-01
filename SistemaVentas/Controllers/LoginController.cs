using DAL.Models;
using SistemaVentas.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaVentas.Controllers
{
    public class LoginController : Controller
    {
        private readonly ComprasContext context;

        public LoginController()
        {
            this.context = new ComprasContext();
        }
        public LoginController(ComprasContext context_)
        {
            this.context = context_;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                var contra = UserHelper.EncriptarPassword(usuarios.pass);
                var obj = context.usuarios.Where(x => x.UserName == usuarios.UserName
                            && x.pass == contra).FirstOrDefault();

                if(obj != null)
                {
                    Session["UserID"] = obj.idUser.ToString();
                    Session["UserName"] = obj.UserName.ToString();

                   return RedirectToAction("Index","Home");

                }
                else
                {
                    ModelState.AddModelError("InvalidCredentials", "El usuario o Contraseña son incorrectos");
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}