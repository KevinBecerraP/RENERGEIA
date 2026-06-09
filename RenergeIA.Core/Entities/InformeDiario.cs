using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class InformeDiario : EntidadBase
{
    public int ProyectoId { get; set; }
    public DateTime Fecha { get; set; }
    public string NumeroCertificado { get; set; } = string.Empty;
    public string ResumenActividades { get; set; } = string.Empty;
    public int PersonalTotal { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public string CreadoPor { get; set; } = string.Empty;
    public bool Enviado { get; set; } = false;

    // Campos de flujo de aprobación
    public EstadoInforme Estado { get; set; } = EstadoInforme.Borrador;
    public string? RevisadoPor { get; set; }
    public DateTime? FechaRevision { get; set; }
    public string? MotivoRechazo { get; set; }

    // Control de versiones alfabético
    public string Version { get; set; } = "0.a";
    public string? ComentarioCambio { get; set; }
    public string? ComentariosGenerales { get; set; }
    public int? InformeDiarioAnteriorId { get; set; }

    public Proyecto Proyecto { get; set; } = null!;
    public InformeDiario? InformeDiarioAnterior { get; set; }
    public ICollection<RegistroAvanceDiario> RegistrosAvance { get; set; } = new List<RegistroAvanceDiario>();
    public ICollection<Fotografia> Fotografias { get; set; } = new List<Fotografia>();
    public ICollection<RegistroClima> RegistrosClima { get; set; } = new List<RegistroClima>();
}
