using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var rolItem = context.RolGetAll().ToList();
                    result.Objects = new List<object>();
                    if (rolItem != null)
                    {
                        foreach (var rolItems in rolItem)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = rolItems.IdRol;
                            rol.Nombre = rolItems.Nombre;

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



    }
}
