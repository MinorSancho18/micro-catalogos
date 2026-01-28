using ExamenProcomerBackend.Domain.Entities;

namespace ExamenProcomerBackend.Application.Interfaces;

public interface ICategoriaVehiculoCommandRepository
{
    Task<int> CrearAsync(CategoriaVehiculo categoria);
    Task<bool> ActualizarAsync(CategoriaVehiculo categoria);
    Task<bool> EliminarAsync(int idCategoria);
}
