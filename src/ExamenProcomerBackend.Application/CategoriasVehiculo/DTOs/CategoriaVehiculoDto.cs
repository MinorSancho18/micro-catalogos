namespace ExamenProcomerBackend.Application.CategoriasVehiculo.DTOs;

public sealed record CategoriaVehiculoDto(
    int IdCategoria,
    string Descripcion,
    string Codigo
);
