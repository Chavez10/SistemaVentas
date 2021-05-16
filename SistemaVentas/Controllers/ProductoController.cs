using BAL.IServices;
using BAL.Services;
using DAL.Helpers;
using DAL.Models;
using SistemaVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentas.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ComprasContext context;
        private IProductosRepository ProductoRepo;

        public ProductoController()
        {      
            this.ProductoRepo = new ProductosRepository(new ComprasContext());
            this.context = new ComprasContext();
        }

        public ProductoController(IProductosRepository productosRepository,ComprasContext context)
        {
            this.ProductoRepo = productosRepository;
            this.context = context;
        }

        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ObtenerProductos(DataTableJS request)
        {            
            var isAdmin = (int)Enums.rol.admin == (int)Session["Rol"];
            int idVendedor = int.Parse(Session["UserID"].ToString());
            var result = isAdmin ? ProductoRepo.ObtenerProductos(request) :
                         ProductoRepo.ObtenerProductos(request).Where(x => x.IdVendedor == idVendedor);

            var lista = result.Select(
                x => new
                {
                    nombre = x.nombreProducto,
                    categoria = GeneralHelper.GetDescriptionFromEnumValue(x.category),
                    existencia = x.cantidad,
                    precio = "$ "+x.precio,
                    idProducto = x.IdProducto
                }).ToList();

            return Json(new {
                draw = request.Draw,
                recordsTotal = result.Count(),
                recordsFiltered = result.Count(),
                data = lista
            });
        }

        public ActionResult ObtenerProductosIndex(DataTableJS request)
        {
            var result = ProductoRepo.ObtenerProductos(request);

            var lista = result.Select(
                x => new
                {
                    nombre = x.nombreProducto,
                    categoria = GeneralHelper.GetDescriptionFromEnumValue(x.category),
                    existencia = x.cantidad,
                    precio = "$ " + x.precio,
                    idProducto = x.IdProducto,
                    descripcion = x.description,
                    foto = x.photo,
                    IdVendedor = x.IdVendedor
                }).ToList();

            return Json(new
            {
                draw = request.Draw,
                recordsTotal = result.Count(),
                recordsFiltered = result.Count(),
                data = lista
            });
        }

        public ActionResult CreateOrUpdateProducto(int id=0)
        {
            if (id > 0)
            {
                var model = context.Productos.Find(id);
                ViewBag.IsEdit = true;
                return View(model);                
            }

            var model_ = new Productos();
            return View(model_);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateProducto(Productos model)
        {
            

            if (ModelState.IsValid)
            {
                var isEdit = model.IdProducto > 0 ? true : false;
                var result = await ProductoRepo.CreateOrUpdateProducto(model);
                ViewBag.Result = result;
                ViewBag.IsEdit = isEdit;
                return View(model);
            }

            return View(model);
        }

        public async Task<ActionResult> Categorias()
        {
            var lista = await ProductoRepo.getCategoriasDetalle();
            ViewBag.listaCat = lista;

            return View();
        }

        public async Task<ActionResult> DeleteProducto(int IdProducto)
        {
            var respuesta = await ProductoRepo.DeleteProductos(IdProducto);

            return Json(new { respuesta });
        }
        
        public ActionResult ListaProductos()
        {
            return View();
        }

    }
}