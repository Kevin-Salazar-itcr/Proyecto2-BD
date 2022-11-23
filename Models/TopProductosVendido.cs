using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class TopProductosVendido
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? VecesVendido { get; set; }
    }
}
