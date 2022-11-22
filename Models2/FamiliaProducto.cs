using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class FamiliaProducto
    {
        public FamiliaProducto()
        {
            Productos = new HashSet<Producto>();
        }

        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
