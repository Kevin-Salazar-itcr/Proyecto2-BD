﻿using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class ConsultaCierreEjecucione
    {
        public short Idejecucion { get; set; }
        public string Nombre { get; set; } = null!;
        public string NumeroCotizacion { get; set; } = null!;
    }
}