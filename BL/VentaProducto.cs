using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;


namespace BL
{
    public class VentaProducto
    {
        public static ML.Result AddSP(ML.VentaProducto ventaproducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string ventaproductoItem = "VentaProductoAdd";
                    using (SqlCommand cmd = new SqlCommand(ventaproductoItem, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("IdVenta", SqlDbType.Int);
                        collection[0].Value = ventaproducto.Venta.IdVenta;

                        collection[1] = new SqlParameter("Cantidad", SqlDbType.Int);
                        collection[1].Value = ventaproducto.Cantidad;

                        collection[2] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[2].Value = ventaproducto.Producto.IdProducto;

                        cmd.Parameters.AddRange(collection);

                        int RowsAffected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Venta Producto NO Insertado";
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
        public static ML.Result AddEF(ML.VentaProducto ventaproducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var ventaproductoItem = context.VentaProductoAdd(ventaproducto.Venta.IdVenta, ventaproducto.Cantidad, ventaproducto.Producto.IdProducto);

                    if (ventaproductoItem != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = true;
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


        public static ML.Result VentaProductoGetByIdVentaEF(int IdVenta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {

                    var ventaproductoitem = context.VentaProductoGetByIdVenta(IdVenta).ToList();

                    if(ventaproductoitem != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var ventaproductoItems in ventaproductoitem)
                        {
                            ML.VentaProducto ventaproducto = new ML.VentaProducto();
                            ventaproducto.Cantidad = ventaproductoItems.Cantidad.Value;

                            ventaproducto.Producto = new ML.Producto();

                            ventaproducto.Producto.IdProducto = ventaproductoItems.IdProducto;
                            ventaproducto.Producto.Nombre = ventaproductoItems.ProductoNombre;
                            ventaproducto.Producto.Descripcion = ventaproductoItems.Descripcion;
                            ventaproducto.Producto.Imagen = ventaproductoItems.Imagen;
                            ventaproducto.Producto.PrecioUnitario = ventaproductoItems.PrecioUnitario.Value;

                            ventaproducto.Venta = new ML.Venta();

                            ventaproducto.Venta.IdVenta = ventaproductoItems.IdVenta;

                            ventaproducto.Venta.MetodoPago = new ML.MetodoPago();
                            ventaproducto.Venta.MetodoPago.IdMetodoPago = ventaproductoItems.IdMetodoPago.Value;
                            ventaproducto.Venta.Total = ventaproductoItems.Total.Value;
                            ventaproducto.Venta.MetodoPago.Metodo = ventaproductoItems.Metodo;

                            ventaproducto.Venta.Usuario = new ML.Usuario();
                            ventaproducto.Venta.Usuario.IdUsuario = ventaproductoItems.IdUsuario;
                            ventaproducto.Venta.Usuario.Nombre = ventaproductoItems.UsuarioNombre;
                            ventaproducto.Venta.Usuario.ApellidoPaterno = ventaproductoItems.ApellidoPaterno;
                            ventaproducto.Venta.Usuario.ApellidoMaterno = ventaproductoItems.ApellidoMaterno;
                            

                            result.Objects.Add(ventaproducto);
                            
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
    }
}
