using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//INTALAR EN PL_MVC PARA UTILIZAR HTTPCLIEN
//•	Json .NET
//ohttps://www.nuget.org/packages/Newtonsoft.Json/
//• Microsoft ASP.Net Web API 2.2 client Libraries
//ohttps://nugetmusthaves.com/Package/Microsoft.AspNet.WebApi.Client
//•	System.Net.Http
//ohttps://www.nuget.org/packages/System.Net.Http/
using System.Net.Http;
using System.Configuration;


namespace PL_MVC.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Result result = BL.Departamento.GetAllEF();

        //    if (result.Correct)
        //    {
        //        ML.Departamento departamento = new ML.Departamento();

               

        //        departamento.Departamentos = result.Objects;
                   
        //        return View(departamento);
        //    }
        //    else
        //    {
        //        ViewBag.Message = result.ErrorMessage;
        //        return PartialView("Modal");
        //    }
           
        //}
        // TODO: WEB API METODO
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Departamento resultDepartamento = new ML.Departamento();
            resultDepartamento.Departamentos = new List<object>();
           
                using (var client = new HttpClient())
                {

                    string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                    client.BaseAddress = new Uri(WebDomain);

                    var responseTask = client.GetAsync("Departamento/GetAll");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                     
                       
                            
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Departamento resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(resultItem.ToString());
                            resultDepartamento.Departamentos.Add(resultItemList);
                        }
                    
                      

                    }
                }
                return View(resultDepartamento);                      
        }
        [HttpPost]
        public ActionResult Add(ML.Departamento departamento)
        {

            using (var client = new HttpClient())
            {
                string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                client.BaseAddress = new Uri(WebDomain);
                
                //HTTP POST
                var postTask = client.PostAsJsonAsync<ML.Departamento>("Departamento/Add", departamento);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return PartialView("GetAll");
                }
                else
                {
                    ViewBag.Message = "el departamento no se ha agragado correctamente" ;
                    return View("Modal");
                }
            }

        }

        [HttpPut]
        public ActionResult Update(int IdDepartamento, ML.Departamento departamento)
        {

            using (var client = new HttpClient())
            {
                string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                client.BaseAddress = new Uri(WebDomain);

                //HTTP POST
                var postTask = client.PutAsJsonAsync<ML.Departamento>("Departamento/Update/"+IdDepartamento, departamento);
                 postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return PartialView("GetAll");
                }
                else
                {
                    ViewBag.Message = "el departamento no se ha agragado correctamente";
                    return View("Modal");
                }
            }
           
        }


        [HttpDelete]
        public ActionResult Delete(int IdDepartamento)
        {
           
            using (var client = new HttpClient())
            {
                string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                client.BaseAddress = new Uri(WebDomain);

                //HTTP POST
                var postTask = client.DeleteAsync("Departamento/Delete/" + IdDepartamento);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "se ha eliminado correctamete el departamento";
                    //ejecuta en metodo del cotrolador
                    //RedirectToAction("GetAll")
                    return  PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "no se ha eliminado correctamete el departamento";
                    return PartialView("Modal");
                }
            }

            
           
        }

        [HttpGet]
        public ActionResult Form(int? IdDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();


            ML.Result resultArea = BL.Area.GetAll();

            departamento.Area = new ML.Area();
            departamento.Area.Areas = resultArea.Objects;
            
            if(IdDepartamento == null)
            {
                return View(departamento);
            }
            else
            {
                ML.Result result = BL.Departamento.GetById(IdDepartamento.Value);
                if (result.Correct)
                {

                    departamento = ((ML.Departamento)result.Object);
                    departamento.Area.Areas = resultArea.Objects;
                    return View(departamento);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                    
                }
            }
        }
        //[HttpGet]
        //public ActionResult Delete(int IdDepartamento)
        //{
        //    ML.Result result = BL.Departamento.DeleteEF(IdDepartamento);

        //    if (result.Correct)
        //    {
        //        return RedirectToAction("GetAll");
        //    }
        //    else
        //    {
        //        ViewBag.Message = result.ErrorMessage;
        //        return PartialView("Modal");
        //    }

        //}

        // POST: Departamento
        [HttpPost]
        public ActionResult Form(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            if (departamento.IdDepartamento == 0)
            {
                Add(departamento);
                //result = BL.Departamento.AddEF(departamento);
                ViewBag.Message = "se ha agregado correctamente el departamento";
            }
            else
            {
                Update(departamento.IdDepartamento,departamento);
                //result = BL.Departamento.Update(departamento);
                ViewBag.Message = "se ha actualizado correctamete el departamento";
            }

            //if (!result.Correct)
            //{
            //    ViewBag.Message = "el departamento no se ha agragado correctamente" + result.ErrorMessage;
            //}
            //return PartialView("modal");
            return PartialView("Modal");
        }

    }
}