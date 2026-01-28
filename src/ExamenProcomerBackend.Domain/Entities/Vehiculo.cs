namespace ExamenProcomerBackend.Domain.Entities;

public sealed class Vehiculo
{
    public int IdVehiculo { get; set; }
    public int IdCategoria { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
}
