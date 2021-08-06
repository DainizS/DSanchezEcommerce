using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(" MENU ECOMMERCE");
            Console.WriteLine("1.- MENU PRODUCTO");
            Console.WriteLine("2.- MENU VENTA");
            Console.WriteLine("3.- MENU DEPARTAMENTO");
            Console.WriteLine("4.- TXT");
            Console.WriteLine(" SELECCIONE MENU");
            int OpcionMenu = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (OpcionMenu)
            {
                case 1:
                    Console.WriteLine(" MENU PRODUCTO");
                    Console.WriteLine("1.- AGREGAR PRODUCTO");
                    Console.WriteLine("2.- VER PRODUCTOS");
                    Console.WriteLine("3.- VER PRODUCTO ID");
                    Console.WriteLine("4.- ACTUALIZAR PRODUCTO");
                    Console.WriteLine("5.- BORRAR PRODUCTO");
                    Console.WriteLine(" SELECCIONE OPCION");
                    int OpcionProducto = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (OpcionProducto)
                    {
                        case 1:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- AGREGAR PRODUCTO SQL");
                            Console.WriteLine("2.- AGREGAR PRODUCTO ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionProductoAddEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionProductoAddEF)
                            {
                                case 1:
                                    PL.Producto.Add();
                                    break;

                                case 2:
                                    PL.Producto.AddEF();
                                    break;
                            }



                            break;
                        case 2:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- VER PRODUCTOS SQL");
                            Console.WriteLine("2.- VER PRODUCTOS ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionProductoGetAllEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionProductoGetAllEF)
                            {
                                case 1:
                                    PL.Producto.GetAll();
                                    break;

                                case 2:
                                    PL.Producto.GetAllEF();
                                    break;
                            }

                            break;
                        case 3:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- VER PRODUCTO ID SQL");
                            Console.WriteLine("2.- VER PRODUCTO ID ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionProductoGetByIdEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionProductoGetByIdEF)
                            {
                                case 1:
                                    PL.Producto.GetById();
                                    break;

                                case 2:
                                    PL.Producto.GetByIdEF();
                                    break;
                            }

                            break;
                        case 4:

                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- ACTUALIZAR PRODUCTO SQL");
                            Console.WriteLine("2.- ACTUALIZAR PRODUCTO ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionProductoUpdateeEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionProductoUpdateeEF)
                            {
                                case 1:
                                    PL.Producto.Update();
                                    break;

                                case 2:
                                    PL.Producto.UpdateEF();
                                    break;
                            }

                            break;
                        case 5:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- BORRAR PRODUCTO SQL");
                            Console.WriteLine("2.- BORRAR PRODUCTO ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionProductoDeleteEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionProductoDeleteEF)
                            {
                                case 1:
                                    PL.Producto.Delete();
                                    break;

                                case 2:
                                    PL.Producto.DeleteEF();
                                    break;
                            }

                            break;
                   
                        default:

                            break;
                    }
                    break;



                case 2:
                    Console.WriteLine(" MENU VENTA");
                    Console.WriteLine("1.- AGREGAR VENTA");
                    Console.WriteLine(" SELECCIONE OPCION");
                    int OpcionVenta = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (OpcionVenta)
                    {
                        case 1:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- AGREGAR VENTA SQL");
                            Console.WriteLine("2.- AGREGAR VENTA ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionProductoDeleteEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionProductoDeleteEF)
                            {
                                case 1:
                                    PL.Producto.Delete();
                                    break;

                                case 2:
                                    PL.Producto.DeleteEF();
                                    break;
                            }
                            break;

                        default:

                            break;
                    }
                    break;




                case 3:
                    Console.WriteLine(" MENU DEPARTAMENTO");
                    Console.WriteLine("1.- AGREGAR DEPARTAMENTO");
                    Console.WriteLine("2.- VER DEPARTAMENTO");
                    Console.WriteLine("3.- ACTUALIZAR DEPARTAMENTO");
                    Console.WriteLine("4.- BORRAR DEPARTAMENTO");
                    Console.WriteLine(" SELECCIONE OPCION");
                    int OpcionDepartamento = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (OpcionDepartamento)
                    {
                        case 1:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- AGREGAR DEPARTAMENTO SQL");
                            Console.WriteLine("2.- AGREGAR DEPARTAMENTO ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionDepartamentoAddEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionDepartamentoAddEF)
                            {
                                case 1:
                                    PL.Departamento.Add();
                                    break;

                                case 2:
                                    PL.Departamento.AddEF();
                                    break;
                            }

                            break;
                        case 2:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- VER DEPARTAMENTO SQL");
                            Console.WriteLine("2.- VER DEPARTAMENTO ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionDepartamentoGetAllEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionDepartamentoGetAllEF)
                            {
                                case 1:
                                    PL.Departamento.GetAll();
                                    break;

                                case 2:
                                    PL.Departamento.GetallEF();
                                    break;
                            }
                            break;
                        case 3:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- ACTUALIZAR DEPARTAMENTO SQL");
                            Console.WriteLine("2.- ACTUALIZAR DEPARTAMENTO ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionDepartamentoUpdateEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionDepartamentoUpdateEF)
                            {
                                case 1:
                                    PL.Departamento.Update();
                                    break;

                                case 2:
                                    PL.Departamento.UpdateEF();
                                    break;
                            }
                            break;
                        case 4:
                            Console.WriteLine(" MODO DE REALIZACION ");
                            Console.WriteLine("1.- BORRAR DEPARTAMENTO SQL");
                            Console.WriteLine("2.- BORRAR DEPARTAMENTO ENTITY FRAMEWORK");
                            Console.WriteLine(" SELECCIONE OPCION");
                            int OpcionDepartamentoDeleteEF = int.Parse(Console.ReadLine());
                            Console.Clear();
                            switch (OpcionDepartamentoDeleteEF)
                            {
                                case 1:
                                    PL.Departamento.Delete();
                                    break;

                                case 2:
                                    PL.Departamento.DeleteEF();
                                    break;
                            }
                            break;

                        default:

                            break;
                    }

                    break;
                case 4:
                    PL.Usuario.LeerTxt();
                    break;
            }




            Console.ReadLine();
        }
    }
}
