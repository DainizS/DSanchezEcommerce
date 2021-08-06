using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class VentaProductoController : Controller
    {
        // GET: VentaProducto
        [HttpGet]
        public ActionResult GetByIdVenta(int IdVenta)
        {
            ML.Result result = BL.VentaProducto.VentaProductoGetByIdVentaEF(IdVenta);

            if (result.Correct)
            {
                ML.VentaProducto VentaProducto = new ML.VentaProducto();
                VentaProducto.VentaProductos = result.Objects;

                return View(VentaProducto);
                
            }
            else
            {
                return PartialView("Modal");
            }
            

        }
    }
}