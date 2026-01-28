namespace ExamenProcomerBackend.Application.Vehiculos.Commands;

public sealed record CrearVehiculoCommand(
    int IdCategoria,
    string Descripcion,
    decimal Costo
);
