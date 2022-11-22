using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Producto
    {
        public Producto()
        {
            ProductosXcotizacions = new HashSet<ProductosXcotizacion>();
        }

        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public short Activo { get; set; }
        public string CodigoFamilia { get; set; } = null!;

        public virtual FamiliaProducto CodigoFamiliaNavigation { get; set; } = null!;
        public virtual ICollection<ProductosXcotizacion> ProductosXcotizacions { get; set; }
    }
}
