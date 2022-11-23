using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class TopVentasVendedor
    {
        public string Vendedor { get; set; } = null!;
        public decimal? VentaTotal { get; set; }
    }
}
