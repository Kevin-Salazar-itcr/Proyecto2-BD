using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Zona
    {
        public Zona()
        {
            Clientes = new HashSet<Cliente>();
            Contactos = new HashSet<Contacto>();
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public string? Zona1 { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
