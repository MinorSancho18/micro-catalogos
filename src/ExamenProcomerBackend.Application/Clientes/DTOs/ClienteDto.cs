namespace ExamenProcomerBackend.Application.Clientes.DTOs;

public sealed class ClienteDto
{
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string NumeroCedula { get; set; } = string.Empty;
}
