using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Departamento
    {
        public static void Add()
        {
         
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            Console.WriteLine("Ingrese Nombre del departamento");
            departamento.Nombre = Console.ReadLine();

            Console.WriteLine("Ingres Id area");
            departamento.Area.IdArea = int.Parse(Console.ReadLine());


            ML.Result result = BL.Departamento.AddSP(departamento);

            if (result.Correct)
            {
                Console.WriteLine("se a agragado correctamente el departamento");

            }
            else
            {
                Console.WriteLine("no se a agragado correctamente el departamento"+ result.ErrorMessage);
            }


        }
        public static void AddEF()
        {

            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            Console.WriteLine("Ingrese Nombre del departamento");
            departamento.Nombre = Console.ReadLine();

            Console.WriteLine("Ingres Id area");
            departamento.Area.IdArea = int.Parse(Console.ReadLine());

            // consumir un servicio

            ServiceReferenceDepartamento.DepartamentoClient obj =new ServiceReferenceDepartamento.DepartamentoClient();
            var result = obj.Add(departamento);


            //ML.Result result = BL.Departamento.AddSP(departamento);

            if (result.Correct)
            {
                Console.WriteLine("se a agragado correctamente el departamento");

            }
            else
            {
                Console.WriteLine("no se a agragado correctamente el departamento" + result.ErrorMessage);
            }


        }

        public static void GetAll()
        {
            ML.Result result = BL.Departamento.GetAllSP();

            if (result.Correct)
            {
                foreach (ML.Departamento departamento in result.Objects)
                {
                    Console.WriteLine("Id Departamento: " + departamento.IdDepartamento);
                    Console.WriteLine("Nombre: " + departamento.Nombre);
                    Console.WriteLine("Id Area: " + departamento.Area.IdArea);
                    Console.WriteLine();
                }
            }

        }
        public static void GetallEF()
        {
            ServiceReferenceDepartamento.DepartamentoClient obj = new ServiceReferenceDepartamento.DepartamentoClient();
            var result = obj.GetAll();
            //ML.Result result = BL.Departamento.GetAllEF();

            if (result.Correct)
            {
                foreach (ML.Departamento departamento in result.Objects)
                {
                    Console.WriteLine("Id Departamento: " + departamento.IdDepartamento);
                    Console.WriteLine("Nombre: " + departamento.Nombre);
                    Console.WriteLine("Id Area: " + departamento.Area.IdArea);
                    Console.WriteLine();
                }
            }

        }

        public static void Update()
        {
            ML.Departamento deparatamento = new ML.Departamento();
            deparatamento.Area = new ML.Area();
            Console.WriteLine("ingrese id departamento");
            deparatamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese nuevo Nombre");
            deparatamento.Nombre = Console.ReadLine();

            Console.WriteLine("ingrese nueva id area");
            deparatamento.Area.IdArea = int.Parse(Console.ReadLine());

            ML.Result result = BL.Departamento.UpdateSP(deparatamento);

            if (result.Correct)
            {
                Console.WriteLine("se a actualizado correctamente el departamento");
            }
            else
            {
                Console.WriteLine("no se a agregado correctamente el departamento" + result.ErrorMessage);
            }

        }
        public static void UpdateEF()
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();
            Console.WriteLine("ingrese id departamento");
            departamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese nuevo Nombre");
            departamento.Nombre = Console.ReadLine();

            Console.WriteLine("ingrese nueva id area");
            departamento.Area.IdArea = int.Parse(Console.ReadLine());

            ServiceReferenceDepartamento.DepartamentoClient obj = new ServiceReferenceDepartamento.DepartamentoClient();
            var result = obj.Update(departamento);
            //ML.Result result = BL.Departamento.UpdateSP(deparatamento);

            if (result.Correct)
            {
                Console.WriteLine("se a actualizado correctamente el departamento");
            }
            else
            {
                Console.WriteLine("no se a agregado correctamente el departamento" + result.ErrorMessage);
            }

        }


        public static void Delete()
        {
            ML.Departamento departamento = new ML.Departamento();
            
            Console.WriteLine("ingrese id del departamento a eliminar");
            departamento.IdDepartamento = int.Parse(Console.ReadLine());

            ML.Result result = BL.Departamento.Delete(departamento);

            if (result.Correct)
            {
                Console.WriteLine("se a eliminado correctamente el departamento");
            }
            else
            {
                Console.WriteLine("no se a eliminado correctamente"+result.ErrorMessage);
            }

        }
        public static void DeleteEF()
        {
            ML.Departamento departamento = new ML.Departamento();

            Console.WriteLine("ingrese id del departamento a eliminar");
            int IdDepartamento = int.Parse(Console.ReadLine());

            ServiceReferenceDepartamento.DepartamentoClient obj = new ServiceReferenceDepartamento.DepartamentoClient();
            var result = obj.Delete(IdDepartamento);

            //ML.Result result = BL.Departamento.Delete(departamento);

            if (result.Correct)
            {
                Console.WriteLine("se a eliminado correctamente el departamento");
            }
            else
            {
                Console.WriteLine("no se a eliminado correctamente" + result.ErrorMessage);
            }

        }
    }
}
