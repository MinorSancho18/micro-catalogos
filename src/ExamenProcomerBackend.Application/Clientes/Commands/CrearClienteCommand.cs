namespace ExamenProcomerBackend.Application.Clientes.Commands;

public sealed record CrearClienteCommand(
    string Nombre,
    string NumeroCedula);
