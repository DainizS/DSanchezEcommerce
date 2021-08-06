using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace PL
{
    public class Usuario
    {
        
        public static void LeerTxt()
        {
           string textFile = @"C:\Users\Alien7\Documents\Jose Dai Niz No Rai Sanchez Pizano\Documents\Registros.txt";
          
           var text = File.ReadAllLines(textFile);
           int count = 0;
           foreach (var line in text)
           {
               
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
               }
               count++;
              
           }
           
        }
    }
}
