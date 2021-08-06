using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http;
using System.Configuration;

namespace PL_MVC.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Add()
        {
            return RedirectToAction("Form", "Usuario");
        }


        [HttpGet]
        public ActionResult Login()
        {
            ML.Usuario login = new ML.Usuario();
            return View(login);


        }

        public ActionResult IniciarSesion()
        {
            return View();
        }

        // entra cuando se utiliza un evento con <button>
        //UserName jose.dai@hotmail.com
        //pasword 123456
        //[HttpPost]
        //public ActionResult Login(ML.Usuario usuario)
        //{
        //    ML.Result result = BL.Usuario.UsuarioGetByUserName(usuario);

        //    ML.Usuario Usuario = new ML.Usuario();
        //    Usuario = ((ML.Usuario)result.Object);

        //    if (result.Correct)
        //    {
        //        if (usuario.UserName == Usuario.UserName)
        //        {
        //            if (usuario.Password == Usuario.Password)
        //            {

        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "contraseña incorrecta";
        //                return RedirectToAction("Modal");
        //            }

        //        }
        //        else
        //        {
        //            ViewBag.Message = "usuario incorrecto";
        //            return RedirectToAction("IniciarSesion");
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message("incorrecto");
        //        return RedirectToAction("Modal");
        //    }

        //}



        //CONSUMIENDO UN SERVICIO LOGIN
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();


            using (var client = new HttpClient())
            {
                string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
                client.BaseAddress = new Uri(WebDomain);


                var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/GetByUsername", usuario);
                postTask.Wait();

                var resultAPI = postTask.Result;
                if (resultAPI.IsSuccessStatusCode)
                {
                    var stream = resultAPI.Content;

                    var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    ML.Usuario resultItemUsuario = new ML.Usuario();
                    resultItemUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                    
                    //////////////////////////////////////////// TOKEN \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    using (var clientToken = new HttpClient())
                    {
                        string WebDomainToken = ConfigurationManager.AppSettings["WebDomain"];
                        clientToken.BaseAddress = new Uri(WebDomain);


                        var postTaskToken = clientToken.PostAsJsonAsync<ML.Usuario>("api/Usuario/Authenticate", resultItemUsuario);
                        postTaskToken.Wait();
                        var resultAPIToken = postTask.Result;
                        string token = postTaskToken.Result.ToString(); ;
                        if (resultAPIToken.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Message = postTask.Result.ToString();
                        }
                    }

                }
                else
                {
                    ViewBag.Message=resultAPI.StatusCode;

                }
                return PartialView("Modal");
            }
        }
















        // entra cuando se utiliza un evento de tipo Url.Action
        //[HttpPost]
        //public ActionResult login()
        //{
        //    ML.Usuario login = new ML.Usuario();
        //    return View(login);
        //}








    }
}




//[HttpPost]      
//       public ActionResult Login(ML.Usuario usuario)
//       {
//           ML.Result result = new ML.Result();


//           using (var client = new HttpClient())
//           {
//               string WebDomain = ConfigurationManager.AppSettings["WebDomain"];
//               client.BaseAddress = new Uri(WebDomain);


//               var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Login", usuario);
//               postTask.Wait();

//               var resultAPI = postTask.Result;
//               if (resultAPI.IsSuccessStatusCode)
//               {
//                   var stream = resultAPI.Content;

//                   var readTask = resultAPI.Content.ReadAsAsync<ML.Usuario>();
//                   readTask.Wait();

//                   ML.Usuario resultItem = new ML.Usuario();
//                   resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.ToString());

//                   if (usuario.UserName == resultItem.UserName)
//                   {
//                       if (usuario.Password == resultItem.Password)
//                       {

//                           return RedirectToAction("Index", "Home");
//                       }
//                       else
//                       {
//                           ViewBag.Message = "contraseña incorrecta";

//                       }

//                   }
//                   else
//                   {
//                       ViewBag.Message = "el usuari no se ha encontrado correctamente";

//                   }
//               }
//               else
//               {
//                   ViewBag.Message("incorrecto");

//               }
//               return RedirectToAction("Modal");
//           }
//       }