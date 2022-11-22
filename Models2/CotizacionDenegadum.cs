using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class CotizacionDenegadum
    {
        public CotizacionDenegadum()
        {
            Cotizaciones = new HashSet<Cotizacione>();
        }

        public string Id { get; set; } = null!;
        public string? Razon { get; set; }

        public virtual ICollection<Cotizacione> Cotizaciones { get; set; }
    }
}
