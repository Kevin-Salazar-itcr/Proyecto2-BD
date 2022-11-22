using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Departamento
    {
        public Departamento()
        {
            Ejecucions = new HashSet<Ejecucion>();
            Usuarios = new HashSet<Usuario>();
        }

        public short Id { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
