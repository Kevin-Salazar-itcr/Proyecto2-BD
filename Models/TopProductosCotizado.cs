using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class TopProductosCotizado
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int? VecesCotizado { get; set; }
    }
}
