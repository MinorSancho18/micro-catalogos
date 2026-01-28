namespace ExamenProcomerBackend.Application.CategoriasVehiculo.Commands;

public sealed record CrearCategoriaVehiculoCommand(
    string Descripcion,
    string Codigo
);
