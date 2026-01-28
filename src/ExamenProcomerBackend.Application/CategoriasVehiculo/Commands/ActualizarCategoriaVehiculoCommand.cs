namespace ExamenProcomerBackend.Application.CategoriasVehiculo.Commands;

public sealed record ActualizarCategoriaVehiculoCommand(
    int IdCategoria,
    string Descripcion,
    string Codigo
);
