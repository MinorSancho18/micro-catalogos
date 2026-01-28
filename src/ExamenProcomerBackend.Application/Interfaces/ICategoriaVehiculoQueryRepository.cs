using ExamenProcomerBackend.Application.CategoriasVehiculo.DTOs;

namespace ExamenProcomerBackend.Application.Interfaces;

public interface ICategoriaVehiculoQueryRepository
{
    Task<IEnumerable<CategoriaVehiculoDto>> ListarTodosAsync();
    Task<CategoriaVehiculoDto?> ObtenerPorIdAsync(int idCategoria);
    Task<bool> ExisteCodigoAsync(string codigo, int? idCategoriaExcluir = null);
}
