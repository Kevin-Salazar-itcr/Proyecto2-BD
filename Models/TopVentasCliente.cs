using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class TopVentasCliente
    {
        public string NombreCuenta { get; set; } = null!;
        public decimal? VentaTotal { get; set; }
    }
}
