using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class TopVentasCliente
    {
        public string NombreCuenta { get; set; } = null!;
        public decimal? VentaTotal { get; set; }
    }
}
