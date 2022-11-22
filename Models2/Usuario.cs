using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Usuario
    {
        public Usuario()
        {
            Actividads = new HashSet<Actividad>();
            Clientes = new HashSet<Cliente>();
            Contactos = new HashSet<Contacto>();
            Cotizaciones = new HashSet<Cotizacione>();
            Ejecucions = new HashSet<Ejecucion>();
            Tareas = new HashSet<Tarea>();
        }

        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido1 { get; set; } = null!;
        public string Apellido2 { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public short Rol { get; set; }
        public short Departamento { get; set; }

        public virtual Departamento DepartamentoNavigation { get; set; } = null!;
        public virtual Rol RolNavigation { get; set; } = null!;
        public virtual ICollection<Actividad> Actividads { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
