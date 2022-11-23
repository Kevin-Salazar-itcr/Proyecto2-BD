using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class DiferEnciaDiasCot
    {
        public string NumeroCotizacion { get; set; } = null!;
        public string NombreOportunidad { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public int? DíasDeDiferencía { get; set; }
    }
}
