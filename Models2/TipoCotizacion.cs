using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class TipoCotizacion
    {
        public TipoCotizacion()
        {
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public short Id { get; set; }
        public string Tipo { get; set; } = null!;

        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
