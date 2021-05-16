using BAL.IServices;
using BAL.Services;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentas.Controllers
{
    public class CarritoCompraController : Controller
    {
        private ICarritoCompraRepository carritoCompraRepository;

        public CarritoCompraController()
        {
            this.carritoCompraRepository = new CarritoCompraRepository(new ComprasContext());
        }

        public CarritoCompraController(ICarritoCompraRepository carritoCompra)
        {
            this.carritoCompraRepository = carritoCompra;
        }

        // GET: CarritoCompra
        public async  Task<ActionResult> Index(bool isExito=false)
        {
            if (isExito) { ViewBag.Exito = true; }
                
            var UserId = Convert.ToInt32(Session["UserID"]);
            var lista = await carritoCompraRepository.DetalleProductosCarrito(UserId);

            return View(lista);
        }

        public async Task<ActionResult> GetProductosByUser()
        {
            var UserId = Convert.ToInt32(Session["UserID"]);
            var Result = await carritoCompraRepository.GetTotalbyUser(UserId);

            return Json(Result,JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AgregarAlCarrito(int IdProducto)
        {
            var UserId = Convert.ToInt32(Session["UserID"]);
            var respuesta = await carritoCompraRepository.AgregarAlCarrito(UserId,IdProducto);

            return Json(new { respuesta });
        }

        public async Task<ActionResult> EliminarCarrito(int IdCarrito)
        {
            var respuesta = await carritoCompraRepository.EliminarDelCarrito(IdCarrito);

            return Json(new { respuesta });
        }

    }
}