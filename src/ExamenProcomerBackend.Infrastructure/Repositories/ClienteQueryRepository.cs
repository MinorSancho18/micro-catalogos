using Dapper;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Application.Clientes.DTOs;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories.Sql;

namespace ExamenProcomerBackend.Infrastructure.Repositories;

internal sealed class ClienteQueryRepository : IClienteQueryRepository
{
    private readonly DapperContext _context;

    public ClienteQueryRepository(DapperContext context) => _context = context;

    public async Task<IEnumerable<ClienteDto>> ListarTodosAsync()
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<ClienteDto>(ClienteSql.GetAll);
    }

    public async Task<ClienteDto?> ObtenerPorIdAsync(int idCliente)
    {
        using var conn = _context.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<ClienteDto>(ClienteSql.GetById, new { IdCliente = idCliente });
    }

    public async Task<ClienteDto?> ObtenerPorCedulaAsync(string numeroCedula)
    {
        using var conn = _context.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<ClienteDto>(ClienteSql.GetByCedula, new { NumeroCedula = numeroCedula });
    }

    public async Task<bool> ExisteCedulaAsync(string numeroCedula, int? idClienteExcluir = null)
    {
        using var conn = _context.CreateConnection();
        var result = await conn.ExecuteScalarAsync<int>(ClienteSql.ExistsCedula, new 
        { 
            NumeroCedula = numeroCedula, 
            IdClienteExcluir = idClienteExcluir 
        });
        return result == 1;
    }
}
