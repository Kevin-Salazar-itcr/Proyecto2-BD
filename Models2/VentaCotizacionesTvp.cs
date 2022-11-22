using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class VentaCotizacionesTvp
    {
        public string Cotizacion { get; set; } = null!;
        public string Oportunidad { get; set; } = null!;
        public string? CuentaAsociada { get; set; }
        public decimal? TotalCotizacion { get; set; }
        public decimal? TotalValorPresente { get; set; }
    }
}
