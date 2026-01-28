using ExamenProcomerBackend.Application.Extras.DTOs;

namespace ExamenProcomerBackend.Application.Interfaces;

public interface IExtraQueryRepository
{
    Task<IEnumerable<ExtraDto>> ListarTodosAsync();
    Task<ExtraDto?> ObtenerPorIdAsync(int idExtra);
}
