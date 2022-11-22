using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class TipoContacto
    {
        public TipoContacto()
        {
            Contactos = new HashSet<Contacto>();
        }

        public short Id { get; set; }
        public string Tipo { get; set; } = null!;

        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}
