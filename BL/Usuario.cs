using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;


namespace BL
{
    public class Usuario
    {
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    string EPassword= Encrypt(usuario.Password);
                    var datime = usuario.FechaNacimiento.ToString("dd/MM/yyyy");
                    var date = datime.Split('/');
                    DateTime dt = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

                    var usuarioItem =Convert.ToString(context.UsuarioAdd(usuario.UserName, EPassword, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, dt, usuario.Sexo, usuario.Estatus, usuario.Rol.IdRol, usuario.Telefono,usuario.Imagen).FirstOrDefault());

                    if (usuarioItem == "Usuario Insertado")
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            if (usuarioItem.Length >= 49 && usuarioItem.Substring(0, 50) == "Violation of UNIQUE KEY constraint 'uniqueUserName'.")
                            {
                                result.Correct = false;
                                result.ErrorMessage = "Ya existe el Email del Usuario registrado";
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = usuarioItem;
                            }
                        }
                    
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
            
        }
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var usuarioItem = context.UsuarioGetAll( usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno).ToList();
                    result.Objects = new List<object>();
                    if (usuarioItem != null)
                    {
                        foreach (var usuarioItems in usuarioItem)
                        {
                            string DPassword = Decrypt(usuarioItems.Password);
                            ML.Usuario usuarios = new ML.Usuario();
                            usuarios.IdUsuario = usuarioItems.IdUsuario;
                            usuarios.UserName = usuarioItems.UserName;
                            usuarios.Password = DPassword;
                            usuarios.Nombre = usuarioItems.UsuarioNombre;
                            usuarios.ApellidoPaterno = usuarioItems.ApellidoPaterno;
                            usuarios.ApellidoMaterno = usuarioItems.ApellidoMaterno;
                            usuarios.FechaNacimiento = usuarioItems.FechaNacimiento.Value;
                            usuarios.Sexo = usuarioItems.Sexo;
                            usuarios.Estatus = usuarioItems.Estatus.Value;
                            //usuario.IdDireccion = usuarioItems.UserName;
                            usuarios.Rol = new ML.Rol();
                            usuarios.Rol.IdRol = usuarioItems.IdRol;
                            usuarios.Rol.Nombre = usuarioItems.RolNombre;
                            usuarios.Telefono = usuarioItems.Telefono;
                            usuarios.Imagen = usuarioItems.Imagen;
                            result.Objects.Add(usuarios);
                            
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }



            return result;
        }

        
        




   

        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var usuarioItem = context.UsuarioGetById(IdUsuario).SingleOrDefault();
                    if (usuarioItem != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        string DPassword = Decrypt(usuarioItem.Password);
                        usuario.IdUsuario = usuarioItem.IdUsuario;
                        usuario.UserName = usuarioItem.UserName;
                        usuario.Password = DPassword;
                        usuario.Nombre = usuarioItem.UsuarioNombre;
                        usuario.ApellidoPaterno = usuarioItem.ApellidoPaterno;
                        usuario.ApellidoMaterno = usuarioItem.ApellidoMaterno;
                        usuario.FechaNacimiento = usuarioItem.FechaNacimiento.Value;
                        usuario.Sexo = usuarioItem.Sexo;
                        usuario.Estatus = usuarioItem.Estatus.Value;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = usuarioItem.IdRol;
                        usuario.Rol.Nombre = usuarioItem.RolNombre;
                        usuario.Telefono = usuarioItem.Telefono;
                        usuario.Imagen = usuarioItem.Imagen;
                           

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }

            return result;
        }

        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    string EPassword = Encrypt(usuario.Password);
                    var datime = usuario.FechaNacimiento.ToString("dd/MM/yyyy");
                    var date = datime.Split('/');
                    DateTime dt = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
                    var usuarioItem = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, EPassword, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, dt, usuario.Sexo, usuario.Estatus, usuario.Rol.IdRol, usuario.Telefono,usuario.Imagen);
                    if (usuarioItem != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }



            return result;
        }

        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var usuarioItem = context.UsuarioDelete(IdUsuario);
                    if (usuarioItem != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }

            return result;
        }


        public static ML.Result UsuarioGetByIdRol(int IdRol)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var usuarioItem = context.UsuarioGetByIdRol(IdRol).ToList();
                    if (usuarioItem != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var usuarioItems in usuarioItem)
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.IdRol = usuarioItems.IdRol;
                            rol.Nombre = usuarioItems.RolNombre;
                            result.Objects.Add(rol);
                        }


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch(Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message; 
            }
            return result;
        }


        public static ML.Result UsuarioGetByUserName(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    string EPassword = Encrypt(usuario.Password);
                    var usuarioItem = context.UsuarioGetByUserName(usuario.UserName).FirstOrDefault();

                    if (usuarioItem != null)
                    {
                        ML.Usuario usuarioItems = new ML.Usuario();

                        string DPassword = Decrypt(usuarioItem.Password);
                        usuarioItems.UserName = usuarioItem.UserName;
                        usuarioItems.Password = DPassword;
                        usuarioItems.Rol = new ML.Rol();
                        usuarioItems.Rol.IdRol = usuarioItem.IdRol;
                        usuarioItems.Rol.Nombre = usuarioItem.RolNombre;

                        result.Object = usuarioItems;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch(Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message; 
            }
            return result;
        }














        static readonly string password = "P455W0rd";
        public static string Encrypt(string plainText)
        {
            if (plainText == null)
            {
                return null;
            }
            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

      
        public static string Decrypt(string encryptedText)
        {
            if (encryptedText == null)
            {
                return null;
            }
            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

    }
}
