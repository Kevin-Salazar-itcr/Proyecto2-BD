using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoCRM.Models
{
    public partial class crmContext : DbContext
    {
        public crmContext()
        {
        }

        public crmContext(DbContextOptions<crmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividads { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Contacto> Contactos { get; set; } = null!;
        public virtual DbSet<CotizacionDenegadum> CotizacionDenegada { get; set; } = null!;
        public virtual DbSet<Cotizacione> Cotizaciones { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Ejecucion> Ejecucions { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Etapa> Etapas { get; set; } = null!;
        public virtual DbSet<FamiliaProducto> FamiliaProductos { get; set; } = null!;
        public virtual DbSet<Inflacion> Inflacions { get; set; } = null!;
        public virtual DbSet<Monedum> Moneda { get; set; } = null!;
        public virtual DbSet<Probabilidad> Probabilidads { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductosXcotizacion> ProductosXcotizacions { get; set; } = null!;
        public virtual DbSet<Rivale> Rivales { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Sector> Sectors { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;
        public virtual DbSet<TipoContacto> TipoContactos { get; set; } = null!;
        public virtual DbSet<TipoCotizacion> TipoCotizacions { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<ValorPresenteCotizacione> ValorPresenteCotizaciones { get; set; } = null!;
        public virtual DbSet<Zona> Zonas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=crm;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.ToTable("actividad");

                entity.HasIndex(e => e.Id, "UQ__activida__3213E83E265CC858")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("fechaInicio");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__actividad__aseso__403A8C7D");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.NombreCuenta)
                    .HasName("PK__cliente__7E5CF2B8690CE421");

                entity.ToTable("cliente");

                entity.Property(e => e.NombreCuenta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cuenta");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.Celular)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.ContactoPrincipal)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("contacto_principal");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Idmoneda).HasColumnName("IDmoneda");

                entity.Property(e => e.Idsector).HasColumnName("IDsector");

                entity.Property(e => e.Idzona).HasColumnName("IDzona");

                entity.Property(e => e.Sitio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sitio");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente__asesor__4AB81AF0");

                entity.HasOne(d => d.IdmonedaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Idmoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente__IDmoned__4D94879B");

                entity.HasOne(d => d.IdsectorNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Idsector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente__IDsecto__4CA06362");

                entity.HasOne(d => d.IdzonaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Idzona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente__IDzona__4BAC3F29");
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.IdContacto)
                    .HasName("PK__contacto__4B1329C755D47589");

                entity.ToTable("contacto");

                entity.Property(e => e.IdContacto)
                    .ValueGeneratedNever()
                    .HasColumnName("idContacto");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cliente");

                entity.Property(e => e.Correo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Motivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("motivo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sector).HasColumnName("sector");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoContacto).HasColumnName("tipoContacto");

                entity.Property(e => e.Zona).HasColumnName("zona");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__asesor__5629CD9C");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__client__534D60F1");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__estado__5812160E");

                entity.HasOne(d => d.SectorNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Sector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__sector__5535A963");

                entity.HasOne(d => d.TipoContactoNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.TipoContacto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__tipoCo__571DF1D5");

                entity.HasOne(d => d.ZonaNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Zona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__zona__5441852A");

                entity.HasMany(d => d.Actividads)
                    .WithMany(p => p.Contactos)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActividadesXcontacto",
                        l => l.HasOne<Actividad>().WithMany().HasForeignKey("Actividad").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__activ__5BE2A6F2"),
                        r => r.HasOne<Contacto>().WithMany().HasForeignKey("Contacto").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__conta__5AEE82B9"),
                        j =>
                        {
                            j.HasKey("Contacto", "Actividad").HasName("PK__activida__387C6263D37F2F33");

                            j.ToTable("actividadesXcontacto");

                            j.IndexerProperty<short>("Contacto").HasColumnName("contacto");

                            j.IndexerProperty<short>("Actividad").HasColumnName("actividad");
                        });

                entity.HasMany(d => d.Tareas)
                    .WithMany(p => p.Contactos)
                    .UsingEntity<Dictionary<string, object>>(
                        "TareaXcontacto",
                        l => l.HasOne<Tarea>().WithMany().HasForeignKey("Tarea").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXcon__tarea__5FB337D6"),
                        r => r.HasOne<Contacto>().WithMany().HasForeignKey("Contacto").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXcon__conta__5EBF139D"),
                        j =>
                        {
                            j.HasKey("Contacto", "Tarea").HasName("PK__tareaXco__80667CCD3B210DC9");

                            j.ToTable("tareaXcontacto");

                            j.IndexerProperty<short>("Contacto").HasColumnName("contacto");

                            j.IndexerProperty<short>("Tarea").HasColumnName("tarea");
                        });
            });

            modelBuilder.Entity<CotizacionDenegadum>(entity =>
            {
                entity.ToTable("cotizacionDenegada");

                entity.HasIndex(e => e.Id, "UQ__cotizaci__3213E83EAC042FDF")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Razon)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("razon");
            });

            modelBuilder.Entity<Cotizacione>(entity =>
            {
                entity.HasKey(e => e.NumeroCotizacion)
                    .HasName("PK__cotizaci__2B77500B637C933A");

                entity.ToTable("cotizaciones");

                entity.Property(e => e.NumeroCotizacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numeroCotizacion");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.ContactoAsociado).HasColumnName("contactoAsociado");

                entity.Property(e => e.ContraQuien)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("contraQuien");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Etapa).HasColumnName("etapa");

                entity.Property(e => e.Factur)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("factur");

                entity.Property(e => e.FechaCierra)
                    .HasColumnType("date")
                    .HasColumnName("fechaCierra");

                entity.Property(e => e.FechaCotizacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaCotizacion");

                entity.Property(e => e.Moneda).HasColumnName("moneda");

                entity.Property(e => e.NombreCuenta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombreCuenta");

                entity.Property(e => e.NombreOportunidad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombreOportunidad");

                entity.Property(e => e.OrdenCompra)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ordenCompra");

                entity.Property(e => e.Probabilidad).HasColumnName("probabilidad");

                entity.Property(e => e.RazonDenegacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("razonDenegacion");

                entity.Property(e => e.Sector).HasColumnName("sector");

                entity.Property(e => e.Tipo).HasColumnName("tipo");

                entity.Property(e => e.Zona).HasColumnName("zona");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__aseso__778AC167");

                entity.HasOne(d => d.ContactoAsociadoNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.ContactoAsociado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__conta__76969D2E");

                entity.HasOne(d => d.ContraQuienNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.ContraQuien)
                    .HasConstraintName("FK__cotizacio__contr__7D439ABD");

                entity.HasOne(d => d.EtapaNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Etapa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__etapa__797309D9");

                entity.HasOne(d => d.MonedaNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Moneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__moned__75A278F5");

                entity.HasOne(d => d.NombreCuentaNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.NombreCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__nombr__787EE5A0");

                entity.HasOne(d => d.ProbabilidadNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Probabilidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__proba__7A672E12");

                entity.HasOne(d => d.RazonDenegacionNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.RazonDenegacion)
                    .HasConstraintName("FK__cotizacio__razon__7C4F7684");

                entity.HasOne(d => d.SectorNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Sector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__secto__74AE54BC");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacion__tipo__7B5B524B");

                entity.HasOne(d => d.ZonaNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Zona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacion__zona__73BA3083");

                entity.HasMany(d => d.ActividadCotizacions)
                    .WithMany(p => p.NumeroCotizacions)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActividadXcotizacion",
                        l => l.HasOne<Actividad>().WithMany().HasForeignKey("ActividadCotizacion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__activ__08B54D69"),
                        r => r.HasOne<Cotizacione>().WithMany().HasForeignKey("NumeroCotizacion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__numer__07C12930"),
                        j =>
                        {
                            j.HasKey("NumeroCotizacion", "ActividadCotizacion").HasName("PK__activida__E60993CC090570E0");

                            j.ToTable("actividadXcotizacion");

                            j.IndexerProperty<string>("NumeroCotizacion").HasMaxLength(20).IsUnicode(false).HasColumnName("numero_cotizacion");

                            j.IndexerProperty<short>("ActividadCotizacion").HasColumnName("actividad_cotizacion");
                        });

                entity.HasMany(d => d.TareaCotizacions)
                    .WithMany(p => p.NumeroCotizacions)
                    .UsingEntity<Dictionary<string, object>>(
                        "TareaXcotizacion",
                        l => l.HasOne<Tarea>().WithMany().HasForeignKey("TareaCotizacion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXcot__tarea__04E4BC85"),
                        r => r.HasOne<Cotizacione>().WithMany().HasForeignKey("NumeroCotizacion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXcot__numer__03F0984C"),
                        j =>
                        {
                            j.HasKey("NumeroCotizacion", "TareaCotizacion").HasName("PK__tareaXco__78BCABADE8EAAF31");

                            j.ToTable("tareaXcotizacion");

                            j.IndexerProperty<string>("NumeroCotizacion").HasMaxLength(20).IsUnicode(false).HasColumnName("numero_cotizacion");

                            j.IndexerProperty<short>("TareaCotizacion").HasColumnName("tarea_cotizacion");
                        });
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("departamento");

                entity.HasIndex(e => e.Id, "UQ__departam__3213E83EAA263B5C")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Ejecucion>(entity =>
            {
                entity.HasKey(e => e.Idejecucion)
                    .HasName("PK__ejecucio__7F8C9D195CC5120D");

                entity.ToTable("ejecucion");

                entity.HasIndex(e => e.Idejecucion, "UQ__ejecucio__7F8C9D1805C5FDB7")
                    .IsUnique();

                entity.Property(e => e.Idejecucion)
                    .ValueGeneratedNever()
                    .HasColumnName("IDejecucion");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.Departamento).HasColumnName("departamento");

                entity.Property(e => e.FechaCierra)
                    .HasColumnType("date")
                    .HasColumnName("fecha_cierra");

                entity.Property(e => e.FechaEjecucion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_ejecucion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreCuenta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cuenta");

                entity.Property(e => e.NumeroCotizacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numero_cotizacion");

                entity.Property(e => e.Propietario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("propietario");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Ejecucions)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ejecucion__aseso__0D7A0286");

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Ejecucions)
                    .HasForeignKey(d => d.Departamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ejecucion__depar__0F624AF8");

                entity.HasOne(d => d.NombreCuentaNavigation)
                    .WithMany(p => p.Ejecucions)
                    .HasForeignKey(d => d.NombreCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ejecucion__nombr__0E6E26BF");

                entity.HasOne(d => d.NumeroCotizacionNavigation)
                    .WithMany(p => p.Ejecucions)
                    .HasForeignKey(d => d.NumeroCotizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ejecucion__numer__0C85DE4D");

                entity.HasMany(d => d.Actividads)
                    .WithMany(p => p.Ejecucions)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActividadesXejecucion",
                        l => l.HasOne<Actividad>().WithMany().HasForeignKey("Actividad").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__activ__1332DBDC"),
                        r => r.HasOne<Ejecucion>().WithMany().HasForeignKey("Ejecucion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__ejecu__123EB7A3"),
                        j =>
                        {
                            j.HasKey("Ejecucion", "Actividad").HasName("PK__activida__410CFF91489A1598");

                            j.ToTable("actividadesXejecucion");

                            j.IndexerProperty<short>("Ejecucion").HasColumnName("ejecucion");

                            j.IndexerProperty<short>("Actividad").HasColumnName("actividad");
                        });

                entity.HasMany(d => d.Tareas)
                    .WithMany(p => p.Ejecucions)
                    .UsingEntity<Dictionary<string, object>>(
                        "TareaXejecucion",
                        l => l.HasOne<Tarea>().WithMany().HasForeignKey("Tarea").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXeje__tarea__17036CC0"),
                        r => r.HasOne<Ejecucion>().WithMany().HasForeignKey("Ejecucion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXeje__ejecu__160F4887"),
                        j =>
                        {
                            j.HasKey("Ejecucion", "Tarea").HasName("PK__tareaXej__F916E13F1AC025E6");

                            j.ToTable("tareaXejecucion");

                            j.IndexerProperty<short>("Ejecucion").HasColumnName("ejecucion");

                            j.IndexerProperty<short>("Tarea").HasColumnName("tarea");
                        });
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("estado");

                entity.HasIndex(e => e.Id, "UQ__estado__3213E83E9914FBFB")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Estado1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");
            });

            modelBuilder.Entity<Etapa>(entity =>
            {
                entity.ToTable("etapa");

                entity.HasIndex(e => e.Id, "UQ__etapa__3213E83E5F671D87")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Etapa1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("etapa");
            });

            modelBuilder.Entity<FamiliaProducto>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__familia___40F9A2070B2C9CD2");

                entity.ToTable("familia_producto");

                entity.HasIndex(e => e.Codigo, "UQ__familia___40F9A206FF82BBB0")
                    .IsUnique();

                entity.Property(e => e.Codigo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Inflacion>(entity =>
            {
                entity.ToTable("inflacion");

                entity.HasIndex(e => e.Id, "UQ__inflacio__3213E83EB6866E5E")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");
            });

            modelBuilder.Entity<Monedum>(entity =>
            {
                entity.ToTable("moneda");

                entity.HasIndex(e => e.Id, "UQ__moneda__3213E83E89F936FA")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMoneda)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Probabilidad>(entity =>
            {
                entity.ToTable("probabilidad");

                entity.HasIndex(e => e.Id, "UQ__probabil__3213E83EDC48358C")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Etapa).HasColumnName("etapa");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__producto__40F9A207B8576517");

                entity.ToTable("producto");

                entity.HasIndex(e => e.Codigo, "UQ__producto__40F9A2061C18AA9E")
                    .IsUnique();

                entity.Property(e => e.Codigo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CodigoFamilia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codigo_familia");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.CodigoFamiliaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CodigoFamilia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__producto__codigo__34C8D9D1");
            });

            modelBuilder.Entity<ProductosXcotizacion>(entity =>
            {
                entity.HasKey(e => new { e.CodigoProducto, e.NumeroCotizacion })
                    .HasName("PK__producto__E9AA2349E9769A59");

                entity.ToTable("productosXcotizacion");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.NumeroCotizacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("numero_cotizacion");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.PrecioNegociado)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("precioNegociado");

                entity.HasOne(d => d.CodigoProductoNavigation)
                    .WithMany(p => p.ProductosXcotizacions)
                    .HasForeignKey(d => d.CodigoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__productos__codig__00200768");

                entity.HasOne(d => d.NumeroCotizacionNavigation)
                    .WithMany(p => p.ProductosXcotizacions)
                    .HasForeignKey(d => d.NumeroCotizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__productos__numer__01142BA1");
            });

            modelBuilder.Entity<Rivale>(entity =>
            {
                entity.ToTable("rivales");

                entity.HasIndex(e => e.Id, "UQ__rivales__3213E83EAA94EBF7")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol");

                entity.HasIndex(e => e.Id, "UQ__rol__3213E83E56DD08AB")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TipoRol)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tipoRol");
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("sector");

                entity.HasIndex(e => e.Id, "UQ__sector__3213E83E9941759F")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Sector1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sector");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.ToTable("tarea");

                entity.HasIndex(e => e.Id, "UQ__tarea__3213E83E07DFBF54")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaCreacion");

                entity.Property(e => e.FechaFinalizacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaFinalizacion");

                entity.Property(e => e.Informacion)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("informacion");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tarea__asesor__46E78A0C");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tarea__estado__47DBAE45");
            });

            modelBuilder.Entity<TipoContacto>(entity =>
            {
                entity.ToTable("tipoContacto");

                entity.HasIndex(e => e.Id, "UQ__tipoCont__3213E83EB80E8298")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<TipoCotizacion>(entity =>
            {
                entity.ToTable("tipoCotizacion");

                entity.HasIndex(e => e.Id, "UQ__tipoCoti__3213E83EB3141647")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK__usuario__415B7BE43C6B2878");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.Cedula, "UQ__usuario__415B7BE5A888EF0E")
                    .IsUnique();

                entity.Property(e => e.Cedula)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("apellido1");

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("apellido2");

                entity.Property(e => e.Clave)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Departamento).HasColumnName("departamento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre_usuario");

                entity.Property(e => e.Rol).HasColumnName("rol");

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Departamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__usuario__departa__3C69FB99");

                entity.HasOne(d => d.RolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Rol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__usuario__rol__3B75D760");
            });

            modelBuilder.Entity<ValorPresenteCotizacione>(entity =>
            {
                entity.HasKey(e => e.IdCotizacion)
                    .HasName("PK__ValorPre__D931C39BBC29D0E6");

                entity.Property(e => e.IdCotizacion)
                    .ValueGeneratedNever()
                    .HasColumnName("idCotizacion");

                entity.Property(e => e.AnioCotizacion).HasColumnName("anioCotizacion");

                entity.Property(e => e.ContactoAsociado).HasColumnName("contactoAsociado");

                entity.Property(e => e.NombreCuenta)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombreCuenta");

                entity.Property(e => e.NombreOportunidad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombreOportunidad");

                entity.Property(e => e.TotalCotizacion)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("totalCotizacion");

                entity.Property(e => e.TotalValorPresente)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("totalValorPresente");

                entity.HasOne(d => d.ContactoAsociadoNavigation)
                    .WithMany(p => p.ValorPresenteCotizaciones)
                    .HasForeignKey(d => d.ContactoAsociado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ValorPres__conta__19DFD96B");

                entity.HasOne(d => d.NombreCuentaNavigation)
                    .WithMany(p => p.ValorPresenteCotizaciones)
                    .HasForeignKey(d => d.NombreCuenta)
                    .HasConstraintName("FK__ValorPres__nombr__1AD3FDA4");
            });

            modelBuilder.Entity<Zona>(entity =>
            {
                entity.ToTable("zona");

                entity.HasIndex(e => e.Id, "UQ__zona__3213E83E2F3BA572")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Zona1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("zona");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
