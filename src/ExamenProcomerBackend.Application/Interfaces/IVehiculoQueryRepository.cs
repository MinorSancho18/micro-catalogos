using ExamenProcomerBackend.Application.Vehiculos.DTOs;

namespace ExamenProcomerBackend.Application.Interfaces;

public interface IVehiculoQueryRepository
{
    Task<IEnumerable<VehiculoDto>> ListarTodosAsync();
    Task<VehiculoDto?> ObtenerPorIdAsync(int idVehiculo);
    Task<IEnumerable<VehiculoDto>> ListarPorCategoriaAsync(int idCategoria);
    Task<bool> ExisteVehiculoPorCategoriaAsync(int idCategoria);
}
