namespace RenergeIA.Core.Entities;

public class Partida : EntidadBase
{
    public int ProyectoId { get; set; }
    public int? ActividadWBSId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Unidad { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal CantidadPresupuestada { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal MontoPresupuestado => CantidadPresupuestada * PrecioUnitario;

    public Proyecto Proyecto { get; set; } = null!;
    public ActividadWBS? ActividadWBS { get; set; }
    public ICollection<CostoReal> CostosReales { get; set; } = new List<CostoReal>();
}
