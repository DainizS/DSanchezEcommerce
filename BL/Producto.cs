using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BL
{
    public class Producto
    {
        public static ML.Result AddSP(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string productoItem = "ProductoAdd";

                    using (SqlCommand cmd = new SqlCommand(productoItem, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[6];
                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = producto.Nombre;

                        collection[1] = new SqlParameter("PrecioUnitario", SqlDbType.Decimal);
                        collection[1].Value = producto.PrecioUnitario;

                        collection[2] = new SqlParameter("Stock", SqlDbType.Int);
                        collection[2].Value = producto.Stock;

                        collection[3] = new SqlParameter("IdProveedor", SqlDbType.Int);
                        collection[3].Value = producto.Proveedor.IdProveedor;

                        collection[4] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[4].Value = producto.Departamento.IdDepartamento;

                        collection[5] = new SqlParameter("Descripcion", SqlDbType.VarChar);
                        collection[5].Value = producto.Descripcion;

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
                            result.ErrorMessage = "no se ha agregado corectamente el producto";
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
        public static ML.Result AddEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var productoItem = context.ProductoAdd(producto.Nombre,producto.PrecioUnitario,producto.Stock,producto.Proveedor.IdProveedor,producto.Departamento.IdDepartamento,producto.Descripcion,producto.Imagen);
                    if (productoItem > 0)
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

        public static ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string productoItem = "ProductoGetAll";
                    using (SqlCommand cmd = new SqlCommand(productoItem, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable productoDT = new DataTable();
                        da.Fill(productoDT);


                        if (productoDT.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in productoDT.Rows)
                            {
                                ML.Producto producto = new ML.Producto();

                                producto.IdProducto = int.Parse(row[0].ToString());
                                producto.Nombre = row[1].ToString();
                                producto.PrecioUnitario =decimal.Parse( row[2].ToString());
                                producto.Stock = int.Parse(row[3].ToString());

                                producto.Proveedor = new ML.Proveedor();
                                producto.Proveedor.IdProveedor = int.Parse(row[4].ToString());

                                producto.Departamento = new ML.Departamento();
                                producto.Departamento.IdDepartamento = int.Parse(row[5].ToString());

                                producto.Descripcion = row[6].ToString();

                                result.Objects.Add(producto);

                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "no se encontraron registros";
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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var productoItem = context.ProductoGetAll().ToList();

                    if (productoItem != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var productoItems in productoItem)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = productoItems.IdProducto;
                            producto.Nombre = productoItems.ProductoNombre;
                            producto.PrecioUnitario = productoItems.PrecioUnitario.Value;
                            producto.Stock = productoItems.Stock.Value;

                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = productoItems.IdProveedor;
                            producto.Proveedor.Nombre = productoItems.ProveedorNombre;
                            producto.Proveedor.Telefono = productoItems.Telefono;

                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = productoItems.IdDepartamento;
                            producto.Departamento.Nombre = productoItems.DepartamentoNombre;

                            producto.Departamento.Area= new ML.Area();
                            producto.Departamento.Area.IdArea = productoItems.IdArea.Value;
                            producto.Departamento.Area.Nombre = productoItems.AreaNombre;

                            producto.Descripcion = productoItems.Descripcion;
                            producto.Imagen = productoItems.Imagen;
                            
                            
                            result.Objects.Add(producto);  

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

        
        public static ML.Result GetByIdSP(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string productoItem = "ProductoGetById";
                    using (SqlCommand cmd = new SqlCommand(productoItem,context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdProducto",SqlDbType.Int);
                        collection[0].Value = producto.IdProducto;

                        cmd.Parameters.AddRange(collection);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable productoDT = new DataTable();
                        da.Fill(productoDT);


                        if (productoDT.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            DataRow Row = productoDT.Rows[0];
                            producto.IdProducto = int.Parse(Row[0].ToString());
                            producto.Nombre = Row[1].ToString();
                            producto.PrecioUnitario = decimal.Parse(Row[2].ToString());
                            producto.Stock = int.Parse(Row[3].ToString());

                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = int.Parse(Row[4].ToString());

                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = int.Parse(Row[5].ToString());

                            producto.Descripcion = Row[6].ToString();

                            result.Object = producto;


                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "no se encontro el registro"+ producto.IdProducto;
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
        public static ML.Result GetByIdEF(ML.Producto productoId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var productoItem = context.ProductoGetById(productoId.IdProducto).FirstOrDefault();

                    if (productoItem != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = productoItem.IdProducto;
                        producto.Nombre = productoItem.ProductoNombre;
                        producto.PrecioUnitario = productoItem.PrecioUnitario.Value;
                        producto.Stock = productoItem.Stock.Value;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = productoItem.IdProveedor;
                        producto.Proveedor.Nombre = productoItem.ProveedorNombre;
                        

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = productoItem.IdDepartamento;
                        producto.Departamento.Nombre = productoItem.DepartamentoNombre;

                        producto.Descripcion = productoItem.Descripcion;

                        result.Object = producto;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct  = false;
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

        public static ML.Result GetByIdEFMVC(int productoId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var productoItem = context.ProductoGetById(productoId).FirstOrDefault();

                    if (productoItem != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = productoItem.IdProducto;
                        producto.Nombre = productoItem.ProductoNombre;
                        producto.PrecioUnitario = productoItem.PrecioUnitario.Value;
                        producto.Stock = productoItem.Stock.Value;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = productoItem.IdProveedor;
                        producto.Proveedor.Nombre = productoItem.ProveedorNombre;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = productoItem.IdDepartamento;
                        producto.Departamento.Nombre = productoItem.DepartamentoNombre;

                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = productoItem.IdArea.Value;
                        producto.Departamento.Area.Nombre = productoItem.AreaNombre;

                        producto.Descripcion = productoItem.Descripcion;
                        producto.Imagen = productoItem.Imagen;

                     

                        result.Object = producto;
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


        public static ML.Result UpdateSP(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {

                    string productoItem = "ProductoUpdate";

                    using (SqlCommand cmd = new SqlCommand(productoItem, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[7];
                        collection[0] = new SqlParameter("IdProducto", SqlDbType.VarChar);
                        collection[0].Value = producto.IdProducto;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = producto.Nombre;

                        collection[2] = new SqlParameter("PrecioUnitario", SqlDbType.Decimal);
                        collection[2].Value = producto.PrecioUnitario;

                        collection[3] = new SqlParameter("Stock", SqlDbType.Int);
                        collection[3].Value = producto.Stock;

                        collection[4] = new SqlParameter("IdProveedor", SqlDbType.Int);
                        collection[4].Value = producto.Proveedor.IdProveedor;

                        collection[5] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[5].Value = producto.Departamento.IdDepartamento;

                        collection[6] = new SqlParameter("Descripcion", SqlDbType.VarChar);
                        collection[6].Value = producto.Descripcion;

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
                            result.ErrorMessage = "no se a agragado correctamente el producto";
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
        public static ML.Result UpdateEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var productoItem = context.ProductoUpdate(producto.IdProducto,producto.Nombre,producto.PrecioUnitario,producto.Stock,producto.Proveedor.IdProveedor,producto.Departamento.IdDepartamento,producto.Descripcion,producto.Imagen);

                    if (productoItem != null)
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
        

        public static ML.Result DeleteSP(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string productoItem = "ProductoDelete";

                    using (SqlCommand cmd = new SqlCommand(productoItem, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[0].Value = producto.IdProducto;

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
                            result.ErrorMessage = "no se elimino coreectamente el producto";
                        }

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
        public static ML.Result DeleteEF(ML.Producto productoId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var productoItem = context.ProductoDelete(productoId.IdProducto);

                    if (productoItem != null)
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
                result.Correct = true;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }

        public static ML.Result DeleteEFMVC(int? productoId)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var productoItem = context.ProductoDelete(productoId);

                    if (productoItem != null)
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
                result.Correct = true;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }
    }
}
