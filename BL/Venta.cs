using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

// LIBRERIA PARAMETERS ENTITY FRAMEWORK \\
using System.Data.Entity.Core.Objects;
namespace BL
{
    public class Venta
    {
        public static ML.Result AddSP(ML.Venta venta, List<object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string ventaItem = "VentaAdd";
                    using (SqlCommand cmd = new SqlCommand(ventaItem, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("IdCliente", SqlDbType.Int);
                        collection[0].Value = venta.Usuario.IdUsuario;

                        collection[1] = new SqlParameter("Total", SqlDbType.Decimal);
                        collection[1].Value = venta.Total;

                        collection[2] = new SqlParameter("IdMetodoPago", SqlDbType.TinyInt);
                        collection[2].Value = venta.MetodoPago.IdMetodoPago;

                        cmd.Parameters.AddRange(collection);
                        int RowsAffected = cmd.ExecuteNonQuery();

                        cmd.Connection.Close();

                        /////////////// OBTENER EL @@IDENTITY \\\\\\\\\\\\\\\
                        venta.IdVenta = (int)cmd.Parameters["IdVenta"].Value;


                        foreach (ML.VentaProducto ventaproducto in Objects)
                        {
                            ventaproducto.Venta = new ML.Venta();
                            ventaproducto.Venta.IdVenta = venta.IdVenta;

                            BL.VentaProducto.AddSP(ventaproducto);
                        }


                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Venta NO Insertada";
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
        public static ML.Result AddEF(ML.Venta venta, List<object> Objects)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    ///////////////////////////////////////// OTENER ID VENTA OUTPUT ENTITY FRAME WORK \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    //System.Data.Entity.Core.Objects.ObjectParameter IdVenta = new System.Data.Entity.Core.Objects.ObjectParameter("Idventa", typeof(Int32));
                    
                    ObjectParameter IdVenta = new ObjectParameter("Idventa", typeof(Int32));

                    var ventaItem = context.VentaAdd(IdVenta, venta.Usuario.IdUsuario, venta.Total, venta.MetodoPago.IdMetodoPago);
                    if (ventaItem != null)
                    {
                        venta.IdVenta = (int)IdVenta.Value;

                        ///////// OBTENER ID DE VENTA PARA TRABAJAR CON MI TICKET \\\\\\\\\
                        result.Object = venta ;

                        foreach (ML.VentaProducto ventaproducto in Objects)
                        {
                            ventaproducto.Venta = new ML.Venta();
                            ventaproducto.Venta.IdVenta = venta.IdVenta;

                            BL.VentaProducto.AddEF(ventaproducto);
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
