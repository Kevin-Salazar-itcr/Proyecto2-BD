using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class ValorPresenteCotizacione
    {
        public short IdCotizacion { get; set; }
        public short ContactoAsociado { get; set; }
        public string NombreOportunidad { get; set; } = null!;
        public short? AnioCotizacion { get; set; }
        public string? NombreCuenta { get; set; }
        public decimal? TotalCotizacion { get; set; }
        public decimal? TotalValorPresente { get; set; }

        public virtual Contacto ContactoAsociadoNavigation { get; set; } = null!;
        public virtual Cliente? NombreCuentaNavigation { get; set; }
    }
}
