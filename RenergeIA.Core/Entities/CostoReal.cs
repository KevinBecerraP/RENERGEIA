namespace RenergeIA.Core.Entities;

public class CostoReal : EntidadBase
{
    public int PartidaId { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string TipoCosto { get; set; } = string.Empty;
    public decimal Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Monto => Cantidad * PrecioUnitario;
    public string NumeroFactura { get; set; } = string.Empty;
    public string Proveedor { get; set; } = string.Empty;
    public string RegistradoPor { get; set; } = string.Empty;

    public Partida Partida { get; set; } = null!;
}
