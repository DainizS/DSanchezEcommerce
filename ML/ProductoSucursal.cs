using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ProductoSucursal
    {
        public int IdPraductoSucursal { get; set; }
        public ML.Producto Producto { get; set; }
        public ML.Sucursal Sucursal { get; set; }

        public List<object> Productos { get; set; }

    }
}
