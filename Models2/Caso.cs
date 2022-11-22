using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models2
{
    public partial class Caso
    {
        public string? IdCaso { get; set; }
        public string? Origen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? Prioridad { get; set; }
        public string? Asunto { get; set; }
        public string? Direccion { get; set; }
        public string? Descripcion { get; set; }
        public string? Propietario { get; set; }
        public string? NombreCuenta { get; set; }
        public string? Cotizacion { get; set; }
        public short? Contacto { get; set; }
        public short? Estado { get; set; }
        public short? TipoCaso { get; set; }

        public virtual Contacto? ContactoNavigation { get; set; }
        public virtual Cotizacione? CotizacionNavigation { get; set; }
        public virtual EstadoCaso? EstadoNavigation { get; set; }
        public virtual Cliente? NombreCuentaNavigation { get; set; }
        public virtual Usuario? PropietarioNavigation { get; set; }
        public virtual TipoCaso? TipoCasoNavigation { get; set; }
    }
}
