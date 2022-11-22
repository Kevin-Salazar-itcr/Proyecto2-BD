using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Etapa
    {
        public Etapa()
        {
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public string Etapa1 { get; set; } = null!;

        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
