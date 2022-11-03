using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class Tarea
    {
        public Tarea()
        {
            Contactos = new HashSet<Contacto>();
            Ejecucions = new HashSet<Ejecucion>();
            NumeroCotizacions = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Informacion { get; set; } = null!;
        public string Asesor { get; set; } = null!;
        public short Estado { get; set; }

        public virtual Usuario AsesorNavigation { get; set; } = null!;
        public virtual Estado EstadoNavigation { get; set; } = null!;

        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
        public virtual ICollection<Cotizacione> NumeroCotizacions { get; set; }
    }
}
