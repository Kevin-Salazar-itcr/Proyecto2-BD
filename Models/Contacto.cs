using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models
{
    public partial class Contacto
    {
        public Contacto()
        {
            Cotizaciones = new HashSet<Cotizacione>();
            ValorPresenteCotizaciones = new HashSet<ValorPresenteCotizacione>();
            Actividads = new HashSet<Actividad>();
            Tareas = new HashSet<Tarea>();
        }

        public short IdContacto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Motivo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public short Zona { get; set; }
        public short Sector { get; set; }
        public string Asesor { get; set; } = null!;
        public short TipoContacto { get; set; }
        public short Estado { get; set; }

        public virtual Usuario AsesorNavigation { get; set; } = null!;
        public virtual Cliente ClienteNavigation { get; set; } = null!;
        public virtual Estado EstadoNavigation { get; set; } = null!;
        public virtual Sector SectorNavigation { get; set; } = null!;
        public virtual TipoContacto TipoContactoNavigation { get; set; } = null!;
        public virtual Zona ZonaNavigation { get; set; } = null!;
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
        public virtual ICollection<ValorPresenteCotizacione> ValorPresenteCotizaciones { get; set; }

        public virtual ICollection<Actividad> Actividads { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
