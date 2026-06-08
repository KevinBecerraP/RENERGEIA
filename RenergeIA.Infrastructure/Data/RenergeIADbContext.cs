using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RenergeIA.Core.Entities;
using RenergeIA.Infrastructure.Identity;

namespace RenergeIA.Infrastructure.Data;

public class RenergeIADbContext : IdentityDbContext<ApplicationUser>
{
    public RenergeIADbContext(DbContextOptions<RenergeIADbContext> options) : base(options) { }

    public DbSet<Proyecto> Proyectos => Set<Proyecto>();
    public DbSet<ActividadWBS> ActividadesWBS => Set<ActividadWBS>();
    public DbSet<RegistroAvanceDiario> RegistrosAvanceDiario => Set<RegistroAvanceDiario>();
    public DbSet<InformeDiario> InformesDiarios => Set<InformeDiario>();
    public DbSet<Fotografia> Fotografias => Set<Fotografia>();
    public DbSet<Documento> Documentos => Set<Documento>();
    public DbSet<VersionDocumento> VersionesDocumento => Set<VersionDocumento>();
    public DbSet<Partida> Partidas => Set<Partida>();
    public DbSet<CostoReal> CostosReales => Set<CostoReal>();
    public DbSet<NoConformidad> NoConformidades => Set<NoConformidad>();
    public DbSet<AccionCorrectiva> AccionesCorrectivas => Set<AccionCorrectiva>();
    public DbSet<Restriccion> Restricciones => Set<Restriccion>();
    public DbSet<PersonalProyecto> PersonalProyecto => Set<PersonalProyecto>();
    public DbSet<DocumentoPersona> DocumentosPersona => Set<DocumentoPersona>();
    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<RegistroHorometro> RegistrosHorometro => Set<RegistroHorometro>();
    public DbSet<Mantenimiento> Mantenimientos => Set<Mantenimiento>();
    public DbSet<Alerta> Alertas => Set<Alerta>();
    public DbSet<RegistroClima> RegistrosClima => Set<RegistroClima>();
    public DbSet<RegistroAvancePersonal> RegistrosAvancePersonal => Set<RegistroAvancePersonal>();
    public DbSet<RegistroAvanceEquipo> RegistrosAvanceEquipo => Set<RegistroAvanceEquipo>();
    public DbSet<RegistroAvanceRestriccion> RegistrosAvanceRestriccion => Set<RegistroAvanceRestriccion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Proyecto
        modelBuilder.Entity<Proyecto>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Codigo).IsRequired().HasMaxLength(20);
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
            e.Property(p => p.Cliente).IsRequired().HasMaxLength(200);
            e.Property(p => p.Pais).HasMaxLength(100);
            e.Property(p => p.PresupuestoContractual).HasColumnType("decimal(18,2)");
            e.Property(p => p.CapacidadKWp).HasColumnType("decimal(10,2)");
            e.HasIndex(p => p.Codigo).IsUnique();
        });

        // RegistroAvanceDiario — tres FK llegan a Proyectos por distintos caminos,
        // SQL Server no permite múltiples CASCADE, se usa Restrict en las secundarias
        modelBuilder.Entity<RegistroAvanceDiario>(e =>
        {
            e.Property(r => r.PorcentajeAvance).HasColumnType("decimal(5,2)");
            e.Property(r => r.HorasTrabajadas).HasColumnType("decimal(6,2)");
            e.Property(r => r.CantidadEjecutadaDia).HasColumnType("decimal(18,4)");
            e.Property(r => r.AvanceEsperado).HasColumnType("decimal(5,2)");
            e.Property(r => r.AvanceAcumulado).HasColumnType("decimal(5,2)");
            e.Property(r => r.Desviacion).HasColumnType("decimal(5,2)");
            e.Property(r => r.HorasAfectadasClima).HasColumnType("decimal(6,2)");
            e.HasOne(r => r.ActividadWBS)
             .WithMany(a => a.RegistrosAvance)
             .HasForeignKey(r => r.ActividadWBSId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(r => r.InformeDiario)
             .WithMany(i => i.RegistrosAvance)
             .HasForeignKey(r => r.InformeDiarioId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // ActividadWBS — auto-referencia (padre-hijo)
        modelBuilder.Entity<ActividadWBS>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.CodigoWBS).IsRequired().HasMaxLength(50);
            e.Property(a => a.Nombre).IsRequired().HasMaxLength(300);
            e.Property(a => a.AvancePlanificado).HasColumnType("decimal(5,2)");
            e.Property(a => a.AvanceReal).HasColumnType("decimal(5,2)");
            e.HasOne(a => a.ActividadPadre)
             .WithMany(a => a.SubActividades)
             .HasForeignKey(a => a.ActividadPadreId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // Partida — MontoPresupuestado es calculado, no se guarda en BD
        modelBuilder.Entity<Partida>(e =>
        {
            e.Property(p => p.PrecioUnitario).HasColumnType("decimal(18,2)");
            e.Property(p => p.CantidadPresupuestada).HasColumnType("decimal(18,4)");
            e.Ignore(p => p.MontoPresupuestado);
        });

        // CostoReal — Monto es calculado, no se guarda en BD
        modelBuilder.Entity<CostoReal>(e =>
        {
            e.Property(c => c.PrecioUnitario).HasColumnType("decimal(18,2)");
            e.Property(c => c.Cantidad).HasColumnType("decimal(18,4)");
            e.Ignore(c => c.Monto);
        });

        // Mantenimiento
        modelBuilder.Entity<Mantenimiento>(e =>
        {
            e.Property(m => m.Costo).HasColumnType("decimal(18,2)");
        });

        // RegistroHorometro
        modelBuilder.Entity<RegistroHorometro>(e =>
        {
            e.Property(r => r.LecturaHorometro).HasColumnType("decimal(10,2)");
            e.Property(r => r.HorasTrabajadas).HasColumnType("decimal(10,2)");
        });

        // RegistroClima
        modelBuilder.Entity<RegistroClima>(e =>
        {
            e.Property(r => r.TemperaturaMaxima).HasColumnType("decimal(5,2)");
            e.Property(r => r.TemperaturaMinima).HasColumnType("decimal(5,2)");
            e.Property(r => r.HumedadRelativa).HasColumnType("decimal(5,2)");
            e.Property(r => r.VelocidadViento).HasColumnType("decimal(7,2)");
            e.Property(r => r.PrecipitacionMm).HasColumnType("decimal(7,2)");
            e.Property(r => r.HorasDisponiblesTrabajar).HasColumnType("decimal(4,1)");
        });

        // Fotografia
        modelBuilder.Entity<Fotografia>(e =>
        {
            e.Property(f => f.Latitud).HasColumnType("decimal(10,7)");
            e.Property(f => f.Longitud).HasColumnType("decimal(10,7)");
        });

        // InformeDiario — auto-referencia para versionado
        modelBuilder.Entity<InformeDiario>(e =>
        {
            e.HasOne(i => i.InformeDiarioAnterior)
             .WithMany()
             .HasForeignKey(i => i.InformeDiarioAnteriorId)
             .OnDelete(DeleteBehavior.NoAction);
        });

        // RegistroAvancePersonal — tabla intermedia M:N
        modelBuilder.Entity<RegistroAvancePersonal>(e =>
        {
            e.Property(r => r.HorasTrabajadas).HasColumnType("decimal(6,2)");
            e.HasOne(r => r.RegistroAvanceDiario)
             .WithMany()
             .HasForeignKey(r => r.RegistroAvanceDiarioId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(r => r.PersonalProyecto)
             .WithMany()
             .HasForeignKey(r => r.PersonalProyectoId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // RegistroAvanceEquipo — tabla intermedia M:N
        modelBuilder.Entity<RegistroAvanceEquipo>(e =>
        {
            e.Property(r => r.HorasUtilizadas).HasColumnType("decimal(6,2)");
            e.HasOne(r => r.RegistroAvanceDiario)
             .WithMany()
             .HasForeignKey(r => r.RegistroAvanceDiarioId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(r => r.Equipo)
             .WithMany()
             .HasForeignKey(r => r.EquipoId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // RegistroAvanceRestriccion — tabla intermedia M:N
        modelBuilder.Entity<RegistroAvanceRestriccion>(e =>
        {
            e.HasOne(r => r.RegistroAvanceDiario)
             .WithMany()
             .HasForeignKey(r => r.RegistroAvanceDiarioId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(r => r.Restriccion)
             .WithMany()
             .HasForeignKey(r => r.RestriccionId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
