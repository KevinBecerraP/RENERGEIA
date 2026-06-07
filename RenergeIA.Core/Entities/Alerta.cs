using RenergeIA.Core.Enums;

namespace RenergeIA.Core.Entities;

public class Alerta : EntidadBase
{
    public int ProyectoId { get; set; }
    public CategoriaAlerta Categoria { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public string Severidad { get; set; } = "Media";
    public bool EsLeida { get; set; } = false;
    public string DestinatarioId { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;

    public Proyecto Proyecto { get; set; } = null!;
}
