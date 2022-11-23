using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class VistaVentasCotXdepto
    {
        public string? Nombre { get; set; }
        public string NumeroCotizacion { get; set; } = null!;
        public decimal? Venta { get; set; }
    }
}
