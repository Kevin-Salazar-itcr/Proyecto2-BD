﻿using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Contactos = new HashSet<Contacto>();
            Cotizaciones = new HashSet<Cotizacione>();
            Ejecucions = new HashSet<Ejecucion>();
        }

        public string NombreCuenta { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Sitio { get; set; } = null!;
        public string ContactoPrincipal { get; set; } = null!;
        public string Asesor { get; set; } = null!;
        public short Idzona { get; set; }
        public short Idmoneda { get; set; }

        public virtual Usuario AsesorNavigation { get; set; } = null!;
        public virtual Monedum IdmonedaNavigation { get; set; } = null!;
        public virtual ZonaSector IdzonaNavigation { get; set; } = null!;
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
    }
}