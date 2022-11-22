using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public short Id { get; set; }
        public string TipoRol { get; set; } = null!;

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
