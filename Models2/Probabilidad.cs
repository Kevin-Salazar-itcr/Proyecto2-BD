using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Probabilidad
    {
        public Probabilidad()
        {
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public double Etapa { get; set; }

        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
