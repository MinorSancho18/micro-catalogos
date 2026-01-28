using ExamenProcomerBackend.Domain.Entities;

namespace ExamenProcomerBackend.Application.Interfaces;

public interface IClienteCommandRepository
{
    Task<int> CrearAsync(Cliente cliente);
    Task<bool> ActualizarAsync(Cliente cliente);
    Task<bool> EliminarAsync(int idCliente);
}
