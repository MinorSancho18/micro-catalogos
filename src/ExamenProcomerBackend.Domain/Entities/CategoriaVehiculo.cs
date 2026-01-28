namespace ExamenProcomerBackend.Domain.Entities;

public sealed class CategoriaVehiculo
{
    public int IdCategoria { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
}
