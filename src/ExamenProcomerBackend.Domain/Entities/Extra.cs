namespace ExamenProcomerBackend.Domain.Entities;

public sealed class Extra
{
    public int IdExtra { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
}
