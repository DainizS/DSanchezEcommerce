using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
                {
                    var areaItem = context.AreaGetAll().ToList();
                    if (areaItem != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var areaItems in areaItem)
                        {
                            ML.Area area = new ML.Area();
                            area.IdArea = areaItems.IdArea;
                            area.Nombre = areaItems.Nombre;
                            result.Objects.Add(area);
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

        //public static ML.Result GetById(int IdArea)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL_EF.DSanchezEcommerceEntities context = new DL_EF.DSanchezEcommerceEntities())
        //        {
        //            var areaItem = context.DepartamentoGetByIdArea(IdArea).FirstOrDefault();
        //            if (areaItem != null)
        //            {
        //                ML.Area area = new ML.Area();

        //                area.IdArea = areaItem.IdArea;
        //                area.Nombre = areaItem.AreaNombre;

        //                result.Object = area;

        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = Ex.Message;
        //    }

        //    return result;
        //}

    }
}
