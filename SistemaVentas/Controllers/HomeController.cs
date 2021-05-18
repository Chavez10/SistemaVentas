using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ComprasContext context;

        public HomeController()
        {
            this.context = new ComprasContext();
        }
        public HomeController(ComprasContext context_)
        {
            this.context = context_;
        }

        [HttpGet]
        [Route("loaderio-dc70124d933a9aab224cb80cc614a27a")]
        public HttpResponseMessage GetLoaderIoVerification()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent("loaderio-dc70124d933a9aab224cb80cc614a27a", Encoding.UTF8, "text/plain");
            return response;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult IndexAdmin()
        {
            ViewBag.Usuario = context.usuarios.Count();
            ViewBag.Producto = context.Productos.Count();
            return View();
        }

        public ActionResult IndexVendedor()
        {
            var userId = Convert.ToInt32(Session["UserID"]);

            ViewBag.Producto = context.Productos.Where(x=>x.IdVendedor == userId).Count();
            return View();
        }
    }
}