using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Ejecucion
    {
        public Ejecucion()
        {
            Actividads = new HashSet<Actividad>();
            Tareas = new HashSet<Tarea>();
        }

        public short Idejecucion { get; set; }
        public string Propietario { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public DateTime FechaEjecucion { get; set; }
        public DateTime FechaCierra { get; set; }
        public string NumeroCotizacion { get; set; } = null!;
        public string Asesor { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public short Departamento { get; set; }

        public virtual Usuario AsesorNavigation { get; set; } = null!;
        public virtual Departamento DepartamentoNavigation { get; set; } = null!;
        public virtual Cliente NombreCuentaNavigation { get; set; } = null!;
        public virtual Cotizacione NumeroCotizacionNavigation { get; set; } = null!;

        public virtual ICollection<Actividad> Actividads { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
