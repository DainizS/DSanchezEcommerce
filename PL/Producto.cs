using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Producto
    {
        public static void Add()
        {
            ML.Producto producto = new ML.Producto();

            Console.WriteLine("ingrese Nombre del producto");
            producto.Nombre = Console.ReadLine();

            Console.WriteLine("ingrese Precio Unitario del producto");
            producto.PrecioUnitario =decimal.Parse( Console.ReadLine());

            Console.WriteLine("ingrese Stock del producto");
            producto.Stock =int.Parse( Console.ReadLine());

            Console.WriteLine("ingrese Proveedor del producto");
            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.IdProveedor =int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese id Departamento del producto");
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento =int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese Descipccion del producto");
            producto.Descripcion = Console.ReadLine();

            ML.Result result = BL.Producto.AddSP(producto);
            if (result.Correct)
            {
                Console.WriteLine("se a agregado corectamente");
            }
            else
            {
                Console.WriteLine("no se a agregado error:" + result.ErrorMessage);
            }


        }
        public static void AddEF()
        {
            ML.Producto producto = new ML.Producto();

            Console.WriteLine("ingrese Nombre del producto");
            producto.Nombre = Console.ReadLine();

            Console.WriteLine("ingrese Precio Unitario del producto");
            producto.PrecioUnitario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("ingrese Stock del producto");
            producto.Stock = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese Proveedor del producto");
            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.IdProveedor = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese id Departamento del producto");
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese Descipccion del producto");
            producto.Descripcion = Console.ReadLine();

            ServiceReferenceProducto.ProductoClient obj = new ServiceReferenceProducto.ProductoClient();
            var result = obj.Add(producto);

            //ML.Result result = BL.Producto.AddEF(producto);

            

            if (result.Correct)
            {
                Console.WriteLine("se a agregado corectamente");
            }
            else
            {
                Console.WriteLine("no se a agregado error:" + result.ErrorMessage);
            }


        }

        public static void GetAll()
        {
            ML.Result result = BL.Producto.GetAllSP();

            if (result.Correct)
            {
                foreach (ML.Producto producto in result.Objects)
                {
                    Console.WriteLine("IdProducto: " + producto.IdProducto);
                    Console.WriteLine("Nombre: " + producto.Nombre);
                    Console.WriteLine("PrecioUnitario: " + producto.PrecioUnitario);
                    Console.WriteLine("Stock: " + producto.Stock);
                    Console.WriteLine("Proveedor: " + producto.Proveedor.IdProveedor);
                    Console.WriteLine("Departamento: " + producto.Departamento.IdDepartamento);
                    Console.WriteLine("Descripcion: " + producto.Descripcion);
                    Console.WriteLine();
                }

            }
        }
        public static void GetAllEF()
        {
            ServiceReferenceProducto.ProductoClient obj = new ServiceReferenceProducto.ProductoClient();
            var result = obj.GetAll();

            //ML.Result result = BL.Producto.GetAllEF();

            if (result.Correct)
            {
                foreach (ML.Producto producto in result.Objects)
                {
                    Console.WriteLine("IdProducto: " + producto.IdProducto);
                    Console.WriteLine("Nombre: " + producto.Nombre);
                    Console.WriteLine("PrecioUnitario: " + producto.PrecioUnitario);
                    Console.WriteLine("Stock: " + producto.Stock);
                    Console.WriteLine("Proveedor: " + producto.Proveedor.IdProveedor);
                    Console.WriteLine("Departamento: " + producto.Departamento.IdDepartamento);
                    Console.WriteLine("Descripcion: " + producto.Descripcion);
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine("error " + result.ErrorMessage);
            }
        }

        public static void GetById()
        {
            Console.WriteLine("ingrese id del producto a buscar");
            ML.Producto productoId = new ML.Producto();
            productoId.IdProducto = int.Parse(Console.ReadLine());

            ML.Result result = BL.Producto.GetByIdSP(productoId);


            if (result.Correct)
            {
                ML.Producto producto = new ML.Producto();

                
                producto = ((ML.Producto)result.Object);

                Console.WriteLine("IdProducto: " + producto.IdProducto);
                Console.WriteLine("Nombre: " + producto.Nombre);
                Console.WriteLine("PrecioUnitario: " + producto.PrecioUnitario);
                Console.WriteLine("Stock: " + producto.Stock);
                Console.WriteLine("IdProveedor: " + producto.Proveedor.IdProveedor);
                Console.WriteLine("IdDepartamento: " + producto.Departamento.IdDepartamento);
                Console.WriteLine("Descripcion: " + producto.Descripcion);

            }
            else
            {
                Console.WriteLine("no se encontro el registro error " + result.ErrorMessage);
            }
        }
        public static void GetByIdEF()
        {
            Console.WriteLine("ingrese id del producto a buscar");
            
            int IdProducto = int.Parse(Console.ReadLine());

            ServiceReferenceProducto.ProductoClient obj = new ServiceReferenceProducto.ProductoClient();
            var result = obj.GetById(IdProducto);
            //ML.Result result = BL.Producto.GetByIdEF(productoId);


            if (result.Correct)
            {
                ML.Producto producto = new ML.Producto();


                producto = ((ML.Producto)result.Object);

                Console.WriteLine("IdProducto: " + producto.IdProducto);
                Console.WriteLine("Nombre: " + producto.Nombre);
                Console.WriteLine("PrecioUnitario: " + producto.PrecioUnitario);
                Console.WriteLine("Stock: " + producto.Stock);
                Console.WriteLine("IdProveedor: " + producto.Proveedor.IdProveedor);
                Console.WriteLine("IdDepartamento: " + producto.Departamento.IdDepartamento);
                Console.WriteLine("Descripcion: " + producto.Descripcion);

            }
            else
            {
                Console.WriteLine("no se encontro el registro error " + result.ErrorMessage);
            }
        }

        public static void Update()
        {
            ML.Producto producto = new ML.Producto();

            Console.WriteLine("ingrese id producto a actualizar");
            producto.IdProducto = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese nombre producto a actualizar");
            producto.Nombre = Console.ReadLine();

            Console.WriteLine("ingrese PrecioUnitario producto a actualizar");
            producto.PrecioUnitario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("ingrese Stock producto a actualizar");
            producto.Stock = int.Parse(Console.ReadLine());
            
            Console.WriteLine("ingrese id proveedor del producto a actualizar");
            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.IdProveedor = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese id departamento del producto a actualizar");
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese descripccion del producto a actualizar");
            producto.Descripcion = Console.ReadLine();


            ML.Result result = BL.Producto.UpdateSP(producto);

            if(result.Correct)
            {
                Console.WriteLine("se a actualizado correctamente el producto");
            }
            else
            {
                Console.WriteLine("no se a actualizado correctamente el producto"+ result.ErrorMessage);
            }
        }
        public static void UpdateEF()
        {
            ML.Producto producto = new ML.Producto();

            Console.WriteLine("ingrese id producto a actualizar");
            producto.IdProducto = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese nombre producto a actualizar");
            producto.Nombre = Console.ReadLine();

            Console.WriteLine("ingrese PrecioUnitario producto a actualizar");
            producto.PrecioUnitario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("ingrese Stock producto a actualizar");
            producto.Stock = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese id proveedor del producto a actualizar");
            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.IdProveedor = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese id departamento del producto a actualizar");
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("ingrese descripccion del producto a actualizar");
            producto.Descripcion = Console.ReadLine();

            ServiceReferenceProducto.ProductoClient obj = new ServiceReferenceProducto.ProductoClient();
            var result = obj.Update(producto);
            //ML.Result result = BL.Producto.UpdateEF(producto);

            if (result.Correct)
            {
                Console.WriteLine("se a actualizado correctamente el producto");
            }
            else
            {
                Console.WriteLine("no se a actualizado correctamente el producto" + result.ErrorMessage);
            }
        }

        public static void Delete()
        {
            ML.Producto producto = new ML.Producto();

            Console.WriteLine("ingrese id del producto a eliminar");
            producto.IdProducto = int.Parse(Console.ReadLine());

            ML.Result result = BL.Producto.DeleteSP(producto);

            if (result.Correct)
            {
                Console.WriteLine("se a aliminado correctamente");
            }
            else
            {
                Console.WriteLine("no se a aeliminado error"+result.ErrorMessage);
            }
        }
        public static void DeleteEF()
        {
           

            Console.WriteLine("ingrese id del producto a eliminar");
            int IdProducto = int.Parse(Console.ReadLine());


            ServiceReferenceProducto.ProductoClient obj = new ServiceReferenceProducto.ProductoClient();
            var result = obj.Delete(IdProducto);
            //ML.Result result = BL.Producto.DeleteEF(producto);

            if (result.Correct)
            {
                Console.WriteLine("se a aliminado correctamente");
            }
            else
            {
                Console.WriteLine("no se a aeliminado error" + result.ErrorMessage);
            }
        }
    }
}
