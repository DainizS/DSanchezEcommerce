using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;

            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {

                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("Modal");
            }

        }


        // aparatado de busqueda  

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno; ;
            usuario.ApellidoMaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno; ;
            ML.Result result = new ML.Result();
            result = BL.Usuario.GetAll(usuario);

            usuario.Usuarios = result.Objects;
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            ML.Result resultRoles = BL.Rol.GetAllEF();

            usuario.Rol = new ML.Rol();
            usuario.Rol.Roles = resultRoles.Objects;


            if (IdUsuario == null) // Agraga 
            {
                return View(usuario);
            }
            else
            {
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);

                if (result.Correct)
                {
                    usuario = ((ML.Usuario)result.Object);
                    usuario.Rol.Roles = resultRoles.Objects;

                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }
            }

        }


        [HttpPost]

        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.AddEF(usuario);
                ViewBag.Message = "el usuario se ha agregado correctamente";
            }
            else
            {
                result = BL.Usuario.UpdateEF(usuario);
                ViewBag.Message = "el usuario se ha agregado correctamente";
            }

            if (!result.Correct)
            {
                ViewBag.Message = "el usuario no se agrego correctamente";
            }
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult UpdateStatus(int IdUsuario, bool Estatus)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Correct)
            {

                ML.Usuario usuario = new ML.Usuario();
                usuario = ((ML.Usuario)result.Object);
                ////////// aperador ternario \\\\\\\\\\\\
                usuario.Estatus = (usuario.Estatus ? false : true);

                ML.Result resultUpdateEstatus = BL.Usuario.UpdateEF(usuario);

                return RedirectToAction("GetAll");
            }
            else
            {
                return View("Modal");
            }

        }
        public JsonResult GetRol(int IdRol)
        {
            var result = BL.Usuario.UsuarioGetByIdRol(IdRol);

            List<SelectListItem> opciones = new List<SelectListItem>();

            opciones.Add(new SelectListItem { Text = "-- seleccione una opccion--", Value = "0" });

            if (result.Objects != null)
            {

                foreach (ML.Rol rol in result.Objects)
                {

                    opciones.Add(new SelectListItem { Text = rol.Nombre.ToString(), Value = rol.IdRol.ToString() });
                }
            }

            return Json(new SelectList(opciones, "Value", "Text", JsonRequestBehavior.AllowGet));
        }



        // aparatado de carga masiva desde txt


        [HttpPost]
        public ActionResult Cargar()
        {
            HttpPostedFileBase file = Request.Files["txtdata"];
            int count = 0;
            if (file.ContentLength > 0)
            {


                BinaryReader b = new BinaryReader(file.InputStream);
                var binData = b.ReadBytes(file.ContentLength);
                string read = System.Text.Encoding.UTF8.GetString(binData);
                var csvData = read.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (var line in csvData)
                {
                    ML.Result resul = new ML.Result();
                    string[] datos = line.Split('|');
                    if (count != 0)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.UserName = datos[3];
                        usuario.Password = (usuario.Password == null) ? "" : usuario.Password;
                        usuario.Nombre = datos[0].ToString();
                        usuario.ApellidoPaterno = datos[1];
                        usuario.ApellidoMaterno = datos[2];
                        var fechadata = datos[4].ToString(); //yyyy-mm-dd

                        string[] separator = { "\t\t", "/", " " };
                        var date = fechadata.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        int dia = int.Parse(date[2]);
                        int mes = int.Parse(date[1]);
                        int año = int.Parse(date[0]);
                        DateTime dt = new DateTime(año, mes, dia);
                        usuario.FechaNacimiento = dt;
                        usuario.Sexo = (usuario.Sexo == null) ? "M" : usuario.Sexo;
                        usuario.Telefono = (usuario.Telefono == null) ? "" : usuario.Telefono;

                        //resul = BL.Usuario.AddEF(usuario);
                    }
                    count++;
                }



            }
            return View();
        }

        [HttpGet]
        public FileContentResult XML(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);

            ML.Usuario usuario = ((ML.Usuario)result.Object);


            string fileName = "file.xml";

            var XML = new System.Xml.Serialization.XmlSerializer(typeof(ML.Usuario));
            var subReq = usuario;
            var xml = "";
            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    XML.Serialize(writer, subReq);
                    xml = sww.ToString(); // Your XML

                    byte[] contenido = System.Text.Encoding.ASCII.GetBytes(xml);

                    return File(contenido, "application/xml", fileName);
                }
            }
            


        }
        [HttpGet]
        public FileContentResult TXT(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.GetByIdEF(IdUsuario);

            ML.Usuario usuario = ((ML.Usuario)result.Object);
            //StreamWriter sw = System.IO.File.AppendText(@"C:\Users\Alien7\Documents\Jose Dai Niz No Rai Sanchez Pizano\Documents\file.txt");

            StreamWriter sw = System.IO.File.AppendText(@"C:\Users\Alien7\Documents\Jose Dai Niz No Rai Sanchez Pizano\Documents\example.txt");
            sw.WriteLine("IdUsuario: " + usuario.IdUsuario);
            sw.WriteLine("UserName: " + usuario.UserName);
            sw.WriteLine("Password: " + usuario.Password);
            sw.WriteLine("Nombre: " + usuario.Nombre);
            sw.WriteLine("ApellidoPaterno: " + usuario.ApellidoPaterno);
            sw.WriteLine("ApellidoMaterno: " + usuario.ApellidoMaterno);
            sw.WriteLine("FechaNacimiento: " + usuario.FechaNacimiento);
            sw.WriteLine("Sexo: " + usuario.Sexo);
            sw.WriteLine("Estatus: " + usuario.Estatus);
            usuario.Rol = new ML.Rol();
            sw.WriteLine("IdRol: " + usuario.Rol.IdRol);
            sw.WriteLine("Nombre: " + usuario.Rol.Nombre);
            sw.WriteLine("Telefono: " + usuario.Telefono);
            sw.WriteLine("Imagen: " + usuario.Imagen);



            string contenidos = "IdUsuario: " + usuario.IdUsuario +
                "\r\nUserName: " + usuario.UserName +
                "\r\nPassword: " + usuario.Password +
                "\r\nNombre: " + usuario.Nombre +
                "\r\nApellidoPaterno: " + usuario.ApellidoPaterno +
                "\r\nApellidoMaterno: " + usuario.ApellidoMaterno +
                "\r\nFechaNacimiento: " + usuario.FechaNacimiento +
                "\r\nSexo: " + usuario.Sexo +
                "\r\nEstatus: " + usuario.Estatus +
                "\r\nIdRol: " + usuario.Rol.IdRol +
                "\r\nNombreRol: " + usuario.Rol.Nombre +
                "\r\nTelefono: " + usuario.Telefono +
                "\r\nImagen: " + usuario.Imagen
                ;
            byte[] contenido = System.Text.Encoding.ASCII.GetBytes(contenidos);
            return File(contenido, "application/text", "example1.txt");










        }

    }
}