using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http;
using System.Configuration;

///////// TODO: libreria para trabajar con varbinary \\\\\\\\\\\\\\\\
using System.IO;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Result result = BL.Producto.GetAllEF();


        //    if (result.Correct)
        //    {
        //        ML.Producto producto = new ML.Producto();
        //        producto.Productos = result.Objects;


        //        return View(producto);

        //    }
        //    else
        //    {
        //        ViewBag.Message = result.ErrorMessage;

        //        return PartialView("Modal");
        //    }

        //}


        // CONSUMIR UN SERVICIO GET ALL
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto resultProducto = new ML.Producto();
            resultProducto.Productos = new List<object>();

            using (var client = new HttpClient())
            {

                string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                client.BaseAddress = new Uri(WebDomain);

                var responseTask = client.GetAsync("Producto/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();



                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Producto resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultItem.ToString());
                        resultProducto.Productos.Add(resultItemList);
                    }



                }
            }
            return View(resultProducto);
        }
        // CONSUMIR UN SERVICIO ADD
        [HttpPost]
        public ActionResult Add(ML.Producto producto)
        {

            using (var client = new HttpClient())
            {
                string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                client.BaseAddress = new Uri(WebDomain);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ML.Producto>("Producto/Add", producto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return PartialView("GetAll");
                }
                else
                {
                    ViewBag.Message = "el producto no se ha agregado correctamente ";
                    return PartialView("Modal");
                }
            }

         
        }
        // CONSUMIR UN SERVICIO UPDATE
        [HttpPut]
        public ActionResult Update(int IdProducto, ML.Producto producto)
        {

            using (var client = new HttpClient())
            {
                string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                client.BaseAddress = new Uri(WebDomain);

                //HTTP POST
                var postTask = client.PutAsJsonAsync<ML.Producto>("Producto/Update/" + IdProducto, producto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return PartialView("GetAll");
                }
                else
                {
                    ViewBag.Message = "el producto no se ha actualizado correctamente ";
                    return PartialView("Modal");
                }
            }

        }
        // CONSUMIR UN SERVICIO DELETE
        [HttpDelete]
        public ActionResult Delete(int IdProducto)
        {

            using (var client = new HttpClient())
            {
                string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                client.BaseAddress = new Uri(WebDomain);

                //HTTP POST
                var postTask = client.DeleteAsync("Producto/Delete/" + IdProducto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "se ha eliminado correctamete el Producto";
                    //ejecuta en metodo del cotrolador
                    //RedirectToAction("GetAll")
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "el producto no se ha eliminado correctamente ";
                    return PartialView("Modal");
                }
            }



        }

        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            
            ////////////////// TODO:  DROPDOWN LIST DATOS IDS\\\\\\\\\\\\\\\\\\\\\\
            ML.Result resultProductos = BL.Producto.GetAllEF();
            ML.Result resultDepartamentos = BL.Departamento.GetAllEF();
            ML.Result resultProveedores = BL.Proveedor.GetAll();
            ML.Result resultAreas = BL.Area.GetAll();

            producto.Departamento = new ML.Departamento();
            producto.Departamento.Departamentos  = resultDepartamentos.Objects;

            producto.Departamento.Area = new ML.Area();
            producto.Departamento.Area.Areas = resultAreas.Objects;

            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.Proveedores = resultProveedores.Objects;
           
            if (IdProducto == null) // ADD
            {
                return View(producto);
            }
            else // UPDATE
            {
                ML.Result result = BL.Producto.GetByIdEFMVC(IdProducto.Value);

                if (result.Correct)
                {

                    producto = ((ML.Producto)result.Object);

                    producto.Departamento.Departamentos = resultDepartamentos.Objects;
                    
                    /////////////////// TODO: AREA DE DEPARTAMENTO DENTRO DEL INNER JOIN DE PRODUCTOS \\\\\\\\\\\\\\\\\\\\\
                   
                    producto.Departamento.Area.Areas = resultAreas.Objects;

                    producto.Proveedor.Proveedores = resultProveedores.Objects;


                    return View(producto);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }


            }
        }

     
        ///////////////////////////////////////////////////////////////////////
        // TODO: OBTENER IMAGEN DE LA VISTAY COVERTIRLA A BASE64
        public byte[] converttobytes(HttpPostedFileBase imagen)
        {
            byte[] data = null;

            BinaryReader  reader = new BinaryReader(imagen.InputStream);
            data = reader.ReadBytes((int)imagen.ContentLength);

            return data;
        }
        
        //////////////////////////////////////////////////////////////////////
      
        //[HttpGet]
        //public ActionResult Delete(int IdProducto)
        //{
        //    ML.Result result = BL.Producto.DeleteEFMVC(IdProducto);

        //    if (result.Correct)
        //    {
        //        return RedirectToAction("GetAll");
        //    }
        //    else
        //    {
        //        ViewBag.Message = result.ErrorMessage;
        //        return PartialView("modal");
        //    }

        //}

        // POST: Producto
        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            //ML.Result result = new ML.Result();

            // TODO: AGREGAR IMAGEN DEL PRODUCTO BASE64
            HttpPostedFileBase file = Request.Files["ImagenData"];
            if (file.ContentLength > 0)
            {
                producto.Imagen = converttobytes(file);
            }
            else
            {
                //producto.Imagen = file;
            }
            /////////////////////////////////////////////////////

            if (producto.IdProducto == 0)
            {
                //result = BL.Producto.AddEF(producto);
                Add(producto);
                ViewBag.Message = "el producto se ha agrego correctamente ";
            }
            else
            {                    
                //result = BL.Producto.UpdateEF(producto);
                Update(producto.IdProducto,producto);
                ViewBag.Message = "el producto se ha actualizado correctamente ";
            }
            //if (!result.Correct)
            //{
            //    ViewBag.Message = "el producto no se ha agrego correctamente " + result.ErrorMessage;
            //}
            return PartialView("Modal");
        }

       
        //////////////////  TODO: DROP DOWN LIST ON CASCADE  \\\\\\\\\\\\\\\\\\\\\\
        //////////////   DE AREAS CON DEPARTAMENTOS EN PRODUCTO \\\\\\\\\\\\\\\\\\\    
        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Departamento.DepartamentoGetByIdArea(IdArea);

            List<SelectListItem> opciones = new List<SelectListItem>();
            opciones.Add(new SelectListItem { Text = "-- seleccione una opccion--", Value = "0" });
            
            if(result.Objects != null)
            {

                foreach (ML.Departamento departamento in result.Objects)
                {
               
                opciones.Add(new SelectListItem { Text =  departamento.Nombre.ToString(), Value = departamento.IdDepartamento.ToString() });
                }
            }

            return Json(new SelectList(opciones, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

    }
}