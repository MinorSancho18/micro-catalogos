namespace ExamenProcomerBackend.Domain.Entities;

public sealed class Cliente
{
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string NumeroCedula { get; set; } = string.Empty;
}
