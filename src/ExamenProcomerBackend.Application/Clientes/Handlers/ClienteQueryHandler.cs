using ExamenProcomerBackend.Application.Clientes.Queries;
using ExamenProcomerBackend.Application.Common;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Application.Clientes.DTOs;

namespace ExamenProcomerBackend.Application.Clientes.Handlers;

public sealed class ClienteQueryHandler
{
    private readonly IClienteQueryRepository _queryRepository;

    public ClienteQueryHandler(IClienteQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public async Task<OperationResult<IEnumerable<ClienteDto>>> HandleListarAsync(ListarClientesQuery query)
    {
        var clientes = await _queryRepository.ListarTodosAsync();
        return OperationResult<IEnumerable<ClienteDto>>.Ok(clientes);
    }

    public async Task<OperationResult<ClienteDto>> HandleObtenerPorIdAsync(ObtenerClientePorIdQuery query)
    {
        var cliente = await _queryRepository.ObtenerPorIdAsync(query.IdCliente);
        return cliente != null 
            ? OperationResult<ClienteDto>.Ok(cliente) 
            : OperationResult<ClienteDto>.Fail("Cliente no encontrado.");
    }

    public async Task<OperationResult<ClienteDto>> HandleObtenerPorCedulaAsync(ObtenerClientePorCedulaQuery query)
    {
        var cliente = await _queryRepository.ObtenerPorCedulaAsync(query.NumeroCedula);
        return cliente != null 
            ? OperationResult<ClienteDto>.Ok(cliente) 
            : OperationResult<ClienteDto>.Fail("Cliente no encontrado.");
    }
}
