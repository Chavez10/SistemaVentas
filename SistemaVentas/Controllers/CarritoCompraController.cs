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
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetProductosByUser()
        {
            var UserId = Convert.ToInt32(Session["UserID"]);
            var Result = await carritoCompraRepository.GetTotalbyUser(UserId);

            return Json(Result,JsonRequestBehavior.AllowGet);
        }
    }
}