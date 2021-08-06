using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var proveedorItem = context.ProveedorGetAll().ToList();

                    if (proveedorItem != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var proveedorItems in proveedorItem)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = proveedorItems.IdProveedor;
                            proveedor.Nombre = proveedorItems.Nombre;
                            proveedor.Telefono = proveedorItems.Telefono;

                            result.Objects.Add(proveedor);

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
                result.Correct = true;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }



    }
}
