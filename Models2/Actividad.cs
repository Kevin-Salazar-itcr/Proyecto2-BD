using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Actividad
    {
        public Actividad()
        {
            Contactos = new HashSet<Contacto>();
            Ejecucions = new HashSet<Ejecucion>();
            NumeroCotizacions = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Asesor { get; set; } = null!;

        public virtual Usuario AsesorNavigation { get; set; } = null!;

        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
        public virtual ICollection<Cotizacione> NumeroCotizacions { get; set; }
    }
}
