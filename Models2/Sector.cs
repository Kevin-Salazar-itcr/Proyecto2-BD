using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Sector
    {
        public Sector()
        {
            Clientes = new HashSet<Cliente>();
            Contactos = new HashSet<Contacto>();
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public string? Sector1 { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
