using ExamenProcomerBackend.Application.Clientes.DTOs;

namespace ExamenProcomerBackend.Application.Interfaces;

public interface IClienteQueryRepository
{
    Task<IEnumerable<ClienteDto>> ListarTodosAsync();
    Task<ClienteDto?> ObtenerPorIdAsync(int idCliente);
    Task<ClienteDto?> ObtenerPorCedulaAsync(string numeroCedula);
    Task<bool> ExisteCedulaAsync(string numeroCedula, int? idClienteExcluir = null);
}
