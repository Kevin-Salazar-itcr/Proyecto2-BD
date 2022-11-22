using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class VistaVentasCotXdepto
    {
        public string? Nombre { get; set; }
        public string NumeroCotizacion { get; set; } = null!;
        public decimal? Venta { get; set; }
    }
}
