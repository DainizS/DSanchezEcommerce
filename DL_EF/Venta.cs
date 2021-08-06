//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL_EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venta()
        {
            this.VentaProducto = new HashSet<VentaProducto>();
        }
    
        public int IdVenta { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<byte> IdMetodoPago { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<int> IdUsuario { get; set; }
    
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaProducto> VentaProducto { get; set; }
    }
}
