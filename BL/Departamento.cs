using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;




namespace BL
{
    public class Departamento
    {
        public static ML.Result Add(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string departamentoItem = "INSERT INTO [Departamento] ([Nombre],[IdArea])  VALUES (@Nombre,@IdArea)";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = departamentoItem;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = departamento.Nombre;

                        collection[1] = new SqlParameter("IdArea", SqlDbType.Int);
                        collection[1].Value = departamento.Area.IdArea;

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
                            result.ErrorMessage = "no se agrado correctamente el departamento";
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
        public static ML.Result AddSP(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string departamentoItem = "DepartamentoAdd";

                    using(SqlCommand cmd = new SqlCommand(departamentoItem,context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("Nombre",SqlDbType.VarChar);
                        collection[0].Value = departamento.Nombre;

                        collection[1] = new SqlParameter("IdArea", SqlDbType.Int);
                        collection[1].Value = departamento.Area.IdArea;

                        cmd.Parameters.AddRange(collection);
                        

                        int RowsAffected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "no se a agregado correctamente el departamento";
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
        public static ML.Result AddEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var departamentoItem = context.DepartamentoAdd(departamento.Nombre,departamento.Area.IdArea);
                    if (departamentoItem != null)
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


        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string departamentoItem = "SELECT [IdDepartamento],[IdArea],[Nombre] FROM [Departamento]";
                    using (SqlCommand cmd =new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = departamentoItem;
                        cmd.Connection.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataTable ProductoDT = new DataTable();
                        da.Fill(ProductoDT);

                        if (ProductoDT.Rows.Count >= 1)
                        {
                           result.Objects = new List<object>();

                            foreach(DataRow row in ProductoDT.Rows)
                            {
                                ML.Departamento departamento = new ML.Departamento();
                                departamento.IdDepartamento =int.Parse(row[0].ToString());
                                departamento.Nombre = row[1].ToString();

                                departamento.Area = new ML.Area();
                                departamento.Area.IdArea = int.Parse(row[2].ToString());

                                result.Objects.Add(departamento);

                            }

                            result.Correct = true;
                        }

                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "no hay registros de departamentos";
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
        public static ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string departamentoItem = "DepartamentoGetall";
                    using (SqlCommand cmd = new SqlCommand(departamentoItem, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable departamentoDT = new DataTable();
                        da.Fill(departamentoDT);

                        if (departamentoDT.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in departamentoDT.Rows)
                            {
                                ML.Departamento departamento = new ML.Departamento();

                                departamento.IdDepartamento =int.Parse(row[0].ToString());
                                departamento.Nombre = row[1].ToString();

                                departamento.Area = new ML.Area();
                                departamento.Area.IdArea = int.Parse(row[2].ToString());

                                result.Objects.Add(departamento);


                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "no se encontro registros";
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
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var departamentoItem = context.DepartamentoGetAll().ToList();

                    if (departamentoItem != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var departamentoItems in departamentoItem)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = departamentoItems.IdDepartamento;
                            departamento.Nombre = departamentoItems.DepartamentoNombre;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = departamentoItems.IdArea.Value;

                            departamento.Area.Nombre = departamentoItems.AreaNombre;

                            result.Objects.Add(departamento);
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


        



        public static ML.Result GetById(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var departamentoItem = context.DepartamentoGetById(IdDepartamento).FirstOrDefault();

                    if (departamentoItem != null)
                    {
                        ML.Departamento departamento = new ML.Departamento();

                        departamento.IdDepartamento = departamentoItem.IdDepartamento;
                        departamento.Nombre = departamentoItem.DepartamentoNombre;

                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = departamentoItem.IdArea.Value;
                        departamento.Area.Nombre = departamentoItem.AreaNombre;

                        result.Object = departamento;
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

        public static ML.Result DepartamentoGetByIdArea(int IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var areaItem = context.DepartamentoGetByIdArea(IdArea).ToList();
                    if (areaItem != null)
                    {

                        result.Objects = new List<object>();

                        foreach (var departamentoItems in areaItem)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = departamentoItems.IdDepartamento;
                            departamento.Nombre = departamentoItems.DepartamentoNombre;
                            result.Objects.Add(departamento);
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

        public static ML.Result Update(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string departamentoItem = "UPDATE [Departamento] SET [Nombre] = @Nombre,[IdArea] = @IdArea WHERE IdDepartamento = @IdDepartamento";
                    using (SqlCommand cmd =new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = departamentoItem;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[0].Value = departamento.IdDepartamento;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = departamento.Nombre;

                        collection[2] = new SqlParameter("IdArea", SqlDbType.Int);
                        collection[2].Value = departamento.Area.IdArea;

                        cmd.Parameters.AddRange(collection);
                                                
                        int RowsAffected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            Console.WriteLine("no se a actualizado correctamente");
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
        public static ML.Result UpdateSP(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string departamentoItem = "DepartamentoUpdate";
                    using (SqlCommand cmd = new SqlCommand(departamentoItem, contex))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[0].Value = departamento.IdDepartamento;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = departamento.Area;

                        collection[2] = new SqlParameter("IdArea", SqlDbType.Int);
                        collection[2].Value = departamento.Area.IdArea;
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
                            result.ErrorMessage = "no se actualizo correctamente el departamento";

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

        public static ML.Result UpdateEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var dpartamentoItem = context.DepartamentoUpdate(departamento.IdDepartamento, departamento.Nombre, departamento.Area.IdArea);
                    if (dpartamentoItem != null)
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


        public static ML.Result Delete(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string departamentoItem = "DELETE FROM [Departamento] WHERE IdDepartamento = @IdDepartamento";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = departamentoItem;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[0].Value = departamento.IdDepartamento;

                        cmd.Parameters.AddRange(collection);
                        

                        int RowsAffected = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            Console.WriteLine("el departamento no se a borrado correctamente");
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
        public static ML.Result DeleteSP(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string departamentoItem = "DepartamentoDelete";
                    using(SqlCommand cmd = new SqlCommand(departamentoItem,context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[0].Value = departamento.IdDepartamento;

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
                            result.ErrorMessage = "no se borro correctamente el registro";
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

        public static ML.Result DeleteEF(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var departamentoitem = context.DepartamentoDelete(IdDepartamento);
                    
                    if (departamentoitem != null)
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

        

    }
}
