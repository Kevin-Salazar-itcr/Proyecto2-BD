using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class ProductosXcotizacion
    {
        public string CodigoProducto { get; set; } = null!;
        public string NumeroCotizacion { get; set; } = null!;
        public short Cantidad { get; set; }
        public decimal PrecioNegociado { get; set; }

        public virtual Producto CodigoProductoNavigation { get; set; } = null!;
        public virtual Cotizacione NumeroCotizacionNavigation { get; set; } = null!;
    }
}
