namespace ExamenProcomerBackend.Application.Vehiculos.Commands;

public sealed record ActualizarVehiculoCommand(
    int IdVehiculo,
    int IdCategoria,
    string Descripcion,
    decimal Costo
);
