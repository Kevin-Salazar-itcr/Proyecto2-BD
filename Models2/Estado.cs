using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Estado
    {
        public Estado()
        {
            Contactos = new HashSet<Contacto>();
            Tareas = new HashSet<Tarea>();
        }

        public short Id { get; set; }
        public string Estado1 { get; set; } = null!;

        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
