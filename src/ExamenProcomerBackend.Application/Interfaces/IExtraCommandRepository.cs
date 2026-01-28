using ExamenProcomerBackend.Domain.Entities;

namespace ExamenProcomerBackend.Application.Interfaces;

public interface IExtraCommandRepository
{
    Task<int> CrearAsync(Extra extra);
    Task<bool> ActualizarAsync(Extra extra);
    Task<bool> EliminarAsync(int idExtra);
}
