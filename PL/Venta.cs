using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Venta
    {

        //public static void Add()
        //{
        //    ML.Venta venta = new ML.Venta();

        //    Console.WriteLine("1) COMPRAR UN PRODUCTO");
        //    Console.WriteLine("2) SALIR");
        //    Console.WriteLine("\n");

        //    int opc = int.Parse(Console.ReadLine());

        //    ML.Result result = new ML.Result();
        //    result.Objects = new List<object>();

        //    venta.Total = 0;
        //    while (opc == 1)
        //    {
        //        Console.WriteLine("PRODUCTOS EXISTENTES");
        //        PL.Producto.GetAll();


        //        Console.WriteLine("Ingresa tu ID de Cliente");
        //        venta.Cliente = new ML.Cliente();
        //        venta.Cliente.IdCliente = int.Parse(Console.ReadLine());

        //        Console.WriteLine("\n METODOS DE PAGO");
        //        Console.WriteLine("1) Efectivo");
        //        Console.WriteLine("2) Tarjeta");
        //        Console.WriteLine("3) Transferencia");
        //        Console.WriteLine("\n Ingresa el ID de Metodo de Pago");
        //        venta.MetodoPago = new ML.MetodoPago();
        //        venta.MetodoPago.IdMetodoPago = byte.Parse(Console.ReadLine());


        //        Console.WriteLine("Ingresa el ID del Producto que desees comprar: ");
        //        ML.VentaProducto ventaproducto = new ML.VentaProducto();
        //        ventaproducto.Producto = new ML.Producto();
        //        ventaproducto.Producto.IdProducto = int.Parse(Console.ReadLine());

        //        Console.WriteLine("Ingrese la cantidad a comprar");
        //        ventaproducto.Cantidad = int.Parse(Console.ReadLine());

        //        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //        ML.Producto productoCostoId = new ML.Producto();
        //        productoCostoId = ventaproducto.Producto;

        //        ML.Result resultProducto = BL.Producto.GetByIdSP(productoCostoId);

        //        ML.Producto producto = new ML.Producto();
        //        producto = ((ML.Producto)resultProducto.Object);
        //        venta.Total = venta.Total + (producto.PrecioUnitario * ventaproducto.Cantidad);

        //        Console.WriteLine(" Total " + venta.Total);
        //        /////////////////////////////////////////////////////////////////////////////////////////////////////             

        //        result.Objects.Add(ventaproducto);

        //        Console.WriteLine("\n");
        //        Console.WriteLine("1) REALIZAR OTRA COMPRA");
        //        Console.WriteLine("0) SALIR");
        //        opc = int.Parse(Console.ReadLine());
        //    }

        //    if (opc == 0)
        //    {
        //        result = BL.Venta.AddSP(venta, result.Objects);

        //        if (result.Correct)
        //        {
        //            Console.WriteLine("Venta insertada");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Venta NO insertada " + result.ErrorMessage);
        //        }
        //    }


        //}
        //public static void AddEF()
        //{
        //    ML.Venta venta = new ML.Venta();

        //    Console.WriteLine("1) COMPRAR UN PRODUCTO");
        //    Console.WriteLine("2) SALIR");
        //    Console.WriteLine("\n");

        //    int opc = int.Parse(Console.ReadLine());

        //    ML.Result result = new ML.Result();
        //    result.Objects = new List<object>();

        //    venta.Total = 0;
        //    while (opc == 1)
        //    {
        //        Console.WriteLine("PRODUCTOS EXISTENTES");
        //        PL.Producto.GetAll();


        //        Console.WriteLine("Ingresa tu ID de Cliente");
        //        venta.Cliente = new ML.Cliente();
        //        venta.Cliente.IdCliente = int.Parse(Console.ReadLine());

        //        Console.WriteLine("\n METODOS DE PAGO");
        //        Console.WriteLine("1) Efectivo");
        //        Console.WriteLine("2) Tarjeta");
        //        Console.WriteLine("3) Transferencia");
        //        Console.WriteLine("\n Ingresa el ID de Metodo de Pago");
        //        venta.MetodoPago = new ML.MetodoPago();
        //        venta.MetodoPago.IdMetodoPago = byte.Parse(Console.ReadLine());


        //        Console.WriteLine("Ingresa el ID del Producto que desees comprar: ");
        //        ML.VentaProducto ventaproducto = new ML.VentaProducto();
        //        ventaproducto.Producto = new ML.Producto();
        //        ventaproducto.Producto.IdProducto = int.Parse(Console.ReadLine());

        //        Console.WriteLine("Ingrese la cantidad a comprar");
        //        ventaproducto.Cantidad = int.Parse(Console.ReadLine());

        //        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //        ML.Producto productoCostoId = new ML.Producto();
        //        productoCostoId = ventaproducto.Producto;

        //        ML.Result resultProducto = BL.Producto.GetByIdSP(productoCostoId);

        //        ML.Producto producto = new ML.Producto();
        //        producto = ((ML.Producto)resultProducto.Object);
        //        venta.Total = venta.Total + (producto.PrecioUnitario * ventaproducto.Cantidad);

        //        Console.WriteLine(" Total " + venta.Total);
        //        /////////////////////////////////////////////////////////////////////////////////////////////////////             

        //        result.Objects.Add(ventaproducto);

        //        Console.WriteLine("\n");
        //        Console.WriteLine("1) REALIZAR OTRA COMPRA");
        //        Console.WriteLine("0) SALIR");
        //        opc = int.Parse(Console.ReadLine());
        //    }

        //    if (opc == 0)
        //    {
        //        result = BL.Venta.AddSP(venta, result.Objects);

        //        if (result.Correct)
        //        {
        //            Console.WriteLine("Venta insertada");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Venta NO insertada " + result.ErrorMessage);
        //        }
        //    }


        //}

    }
}


//Console.WriteLine("Ingrese el Id de la materia que desee comprar");

//                ML.Materia materia = new ML.Materia();
//                materia.IdMateria = int.Parse(Console.ReadLine());

//                Console.WriteLine("Ingrese la cantidad de la materia que desee comprar");

//                ML.VentaMateria ventaMateria = new ML.VentaMateria();
//                ventaMateria.Cantidad = int.Parse(Console.ReadLine());
//                ventaMateria.Materia = materia;

//                result.Objects.Add(ventaMateria);

//                //Calcular Total

//                ML.Result resultMateria = BL.Materia.GetById(materia.IdMateria);

//                materia.Costo = ((ML.Materia)resultMateria.Object).Costo;

//                venta.Total = venta.Total + (materia.Costo * ventaMateria.Cantidad);