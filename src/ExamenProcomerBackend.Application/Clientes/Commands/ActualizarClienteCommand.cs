namespace ExamenProcomerBackend.Application.Clientes.Commands;

public sealed record ActualizarClienteCommand(
    int IdCliente,
    string Nombre,
    string NumeroCedula);
