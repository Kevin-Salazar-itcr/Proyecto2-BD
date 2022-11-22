using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class TotalTareasYactividade
    {
        public string NumeroCotizacion { get; set; } = null!;
        public string NombreOportunidad { get; set; } = null!;
        public int? TotalTareasYActividades { get; set; }
    }
}
