namespace ExamenProcomerBackend.Application.Vehiculos.DTOs;

public sealed record VehiculoDto(
    int IdVehiculo,
    int IdCategoria,
    string Descripcion,
    decimal Costo,
    string? CategoriaDescripcion = null,
    string? CategoriaCodigo = null
);
