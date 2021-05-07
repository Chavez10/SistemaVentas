using BAL.IServices;
using BAL.Services;
using DAL.Helpers;
using DAL.Models;
using SistemaVentas.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaVentas.Controllers
{
    public class LoginController : Controller
    {
        private readonly ComprasContext context;
        private ILoginRepository loginRepository;

        public LoginController()
        {
            this.context = new ComprasContext();
            this.loginRepository = new LoginRepository(context);
        }
        public LoginController(ComprasContext context_,ILoginRepository _loginRepository)
        {
            this.context = context_;
            this.loginRepository = _loginRepository;
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
        public async Task<ActionResult> Login(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                var contra = GeneralHelper.EncriptarPassword(usuarios.pass);
                var obj = await loginRepository.IniciarSesion(usuarios.UserName,contra);

                if(obj != null)
                {
                    Session["UserID"] = obj.idUser.ToString();
                    Session["UserName"] = obj.UserName.ToString();

                   return RedirectToAction("Index","Home");

                }
                else
                {
                    ViewBag.ErrorUser = true;
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