using ExamenProcomerBackend.Domain.Entities;

namespace ExamenProcomerBackend.Application.Interfaces;

public interface IVehiculoCommandRepository
{
    Task<int> CrearAsync(Vehiculo vehiculo);
    Task<bool> ActualizarAsync(Vehiculo vehiculo);
    Task<bool> EliminarAsync(int idVehiculo);
}
