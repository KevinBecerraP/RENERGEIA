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

    public Proyecto Proyecto { get; set; } = null!;
    public ICollection<RegistroAvanceDiario> RegistrosAvance { get; set; } = new List<RegistroAvanceDiario>();
    public ICollection<Fotografia> Fotografias { get; set; } = new List<Fotografia>();
}
