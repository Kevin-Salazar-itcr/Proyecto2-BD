using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Rivale
    {
        public Rivale()
        {
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public string Id { get; set; } = null!;
        public string? Nombre { get; set; }

        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
