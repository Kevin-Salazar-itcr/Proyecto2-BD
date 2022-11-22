using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Cotizacione
    {
        public Cotizacione()
        {
            Ejecucions = new HashSet<Ejecucion>();
            ProductosXcotizacions = new HashSet<ProductosXcotizacion>();
            ActividadCotizacions = new HashSet<Actividad>();
            TareaCotizacions = new HashSet<Tarea>();
        }

        public string NumeroCotizacion { get; set; } = null!;
        public string NombreOportunidad { get; set; } = null!;
        public DateTime FechaCotizacion { get; set; }
        public DateTime FechaCierra { get; set; }
        public string OrdenCompra { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Factur { get; set; } = null!;
        public short Zona { get; set; }
        public short Sector { get; set; }
        public short Moneda { get; set; }
        public short ContactoAsociado { get; set; }
        public string Asesor { get; set; } = null!;
        public string NombreCuenta { get; set; } = null!;
        public short Etapa { get; set; }
        public short Probabilidad { get; set; }
        public short Tipo { get; set; }
        public string? RazonDenegacion { get; set; }
        public string? ContraQuien { get; set; }

        public virtual Usuario AsesorNavigation { get; set; } = null!;
        public virtual Contacto ContactoAsociadoNavigation { get; set; } = null!;
        public virtual Rivale? ContraQuienNavigation { get; set; }
        public virtual Etapa EtapaNavigation { get; set; } = null!;
        public virtual Monedum MonedaNavigation { get; set; } = null!;
        public virtual Cliente NombreCuentaNavigation { get; set; } = null!;
        public virtual Probabilidad ProbabilidadNavigation { get; set; } = null!;
        public virtual CotizacionDenegadum? RazonDenegacionNavigation { get; set; }
        public virtual Sector SectorNavigation { get; set; } = null!;
        public virtual TipoCotizacion TipoNavigation { get; set; } = null!;
        public virtual Zona ZonaNavigation { get; set; } = null!;
        public virtual ICollection<Ejecucion> Ejecucions { get; set; }
        public virtual ICollection<ProductosXcotizacion> ProductosXcotizacions { get; set; }

        public virtual ICollection<Actividad> ActividadCotizacions { get; set; }
        public virtual ICollection<Tarea> TareaCotizacions { get; set; }
    }
}
