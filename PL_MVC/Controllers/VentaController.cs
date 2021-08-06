using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PL_MVC.Controllers
{
    public class VentaController : Controller
    {
        // GET: Venta

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();

            //Session.Remove("Venta");
            if (result.Correct)
            {
                ML.Producto producto = new ML.Producto();
                producto.Productos = result.Objects;


                //Session["Venta"] = result.Objects;
                return View(producto);

            }
            else
            {
                ViewBag.Message = result.ErrorMessage;

                return PartialView("Modal");
            }

        }

        [HttpGet]
        public ActionResult AddCarrito(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            //Session.Remove("Carrito");
            ////// TODO: COMPROBAR SI MI SESSION ESTA VACIA O NO 
            if (Session["Carrito"] == null)
            {


                ML.VentaProducto ventaproducto = new ML.VentaProducto();
                ventaproducto.Cantidad = 1;

                ML.Result resultProducto = BL.Producto.GetByIdEFMVC(IdProducto);
                if (resultProducto != null)
                {

                    ventaproducto.Producto = (ML.Producto)resultProducto.Object;
                    result.Objects.Add(ventaproducto);

                    //////// EMPAQUETAR MI LISTA DE OBJETOS \\\\\\\\\\\\
                    Session["Carrito"] = result.Objects;


                    //ML.Producto producto = new ML.Producto();
                    //producto.Productos = (List<object>)Session["venta"];


                    //return View("GetAll");
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }
                return RedirectToAction("Carrito");

            }

            else
            {
                ///// DESEMPAQUETAR MI LISTA DE OFJETOS DE LA SECCION \\\\\\\\\

                result.Objects = new List<object>();
                result.Objects = (List<object>)Session["Carrito"];

                result.Correct = false;

                // TODO: COMPROVAR SI SE ESTA SELECCIONANDO EL MISMO PRODUCTO O NO 
                foreach (ML.VentaProducto VProducto in result.Objects)
                {


                    if (IdProducto == VProducto.Producto.IdProducto)
                    {

                        result.Correct = true;

                    }

                }


                if (result.Correct)//encontraste el producto
                {

                    foreach (ML.VentaProducto VentaProducto in result.Objects)
                    {

                        if (IdProducto == VentaProducto.Producto.IdProducto)
                        {
                            if (VentaProducto.Cantidad > VentaProducto.Producto.Stock)
                            {
                                
                                VentaProducto.Cantidad = VentaProducto.Cantidad + 1;
                            }
                            VentaProducto.Cantidad = VentaProducto.Producto.Stock;
                            /////// otra forma de hacerlo 
                            //VentaProducto.Cantidad += 1;
                        }


                    }
                }
                else //NO encontraste el producto
                {
                    ML.VentaProducto ventaproducto = new ML.VentaProducto();
                    ML.Result resultProducto = BL.Producto.GetByIdEFMVC(IdProducto);
                    if (resultProducto != null)
                    {

                        ventaproducto.Producto = (ML.Producto)resultProducto.Object;
                        ventaproducto.Cantidad = 1;
                        result.Objects.Add(ventaproducto);

                        //////// EMPAQUETAR MI LISTA DE OBJETOS \\\\\\\\\\\\
                        Session["Carrito"] = result.Objects;


                        //return View("GetAll");
                    }
                    else
                    {
                        ViewBag.Message = result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }

            }

            return RedirectToAction("Carrito");


        }


        public ActionResult Carrito()
        {
            ML.Result result = new ML.Result();
            result.Objects = (List<object>)Session["Carrito"];
            return View(result);
        }

        public ActionResult Incrementar(int IdProducto)
        {

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            result.Correct = false;

            // TODO: COMPROVAR SI SE ESTA SELECCIONANDO EL MISMO PRODUCTO O NO 
            // remove Add investi
            foreach (ML.VentaProducto VProducto in result.Objects)
            {


                if (IdProducto == VProducto.Producto.IdProducto)
                {

                    result.Correct = true;

                }

            }


            if (result.Correct)//encontraste el producto
            {

                foreach (ML.VentaProducto VentaProducto in result.Objects)
                {
                    if (IdProducto == VentaProducto.Producto.IdProducto)
                    {
                        VentaProducto.Cantidad = VentaProducto.Cantidad + 1;
                        /////// otra forma de hacerlo 
                        //VentaProducto.Cantidad += 1;
                    }


                }
            }
            return RedirectToAction("Carrito");
        }

        public ActionResult Decrementar(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            result.Correct = false;

            // TODO: COMPROVAR SI SE ESTA SELECCIONANDO EL MISMO PRODUCTO O NO 
            // remove Add investigar
            foreach (ML.VentaProducto VProducto in result.Objects)
            {


                if (IdProducto == VProducto.Producto.IdProducto)
                {

                    result.Correct = true;

                }

            }


            if (result.Correct)//encontraste el producto
            {

                foreach (ML.VentaProducto VentaProducto in result.Objects)
                {
                    if (VentaProducto.Cantidad > VentaProducto.Producto.Stock)
                    {
                        if (IdProducto == VentaProducto.Producto.IdProducto)
                        {
                            VentaProducto.Cantidad = VentaProducto.Cantidad - 2;
                            /////// otra forma de hacerlo 
                            //VentaProducto.Cantidad += 1;
                        }
                    }
                    else
                    {
                        if (IdProducto == VentaProducto.Producto.IdProducto)
                        {
                            VentaProducto.Cantidad = VentaProducto.Cantidad - 1;

                        }
                    }



                }
            }
            return RedirectToAction("Carrito");

        }

        public ActionResult Eliminar(int IdProducto)
        {
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            result.Correct = false;
            int idex = 0;
            // TODO: COMPROVAR SI SE ESTA SELECCIONANDO EL MISMO PRODUCTO O NO 
            foreach (ML.VentaProducto VProducto in result.Objects)
            {


                if (IdProducto == VProducto.Producto.IdProducto)
                {

                    idex = result.Objects.IndexOf(VProducto);



                }

            } result.Objects.RemoveAt(idex);


            return RedirectToAction("Carrito");
        }
        public ActionResult ProcesarCompra()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            decimal total = 0;

            ML.Venta venta = new ML.Venta();

            foreach (ML.VentaProducto Vproducto in result.Objects)
            {

                total += Vproducto.Cantidad * Vproducto.Producto.PrecioUnitario;

                
                venta.Total = total;

                

            }
            
            venta.MetodoPago = new ML.MetodoPago();
            venta.MetodoPago.IdMetodoPago = 1;

            venta.Usuario = new ML.Usuario();
            venta.Usuario.IdUsuario = 1;


            
            result = BL.Venta.AddEF(venta, result.Objects);
            if (result.Correct)
            {
                venta = ((ML.Venta)result.Object);
                
                ViewBag.Message = " la compra se realizo correctamente";
                Session.Remove("Carrito");
                return RedirectToAction("GetByIdVenta", "VentaProducto", new { IdVenta = venta.IdVenta });
                
            
            }
            else
            {
                ViewBag.Message = " Error no se pudo ejecutar correctamente la compra";
                return PartialView("Aprobacion");
            }


        }
    }
}