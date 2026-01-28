using Dapper;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Domain.Entities;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories.Sql;

namespace ExamenProcomerBackend.Infrastructure.Repositories;

internal sealed class ClienteCommandRepository : IClienteCommandRepository
{
    private readonly DapperContext _context;

    public ClienteCommandRepository(DapperContext context) => _context = context;

    public async Task<int> CrearAsync(Cliente cliente)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(ClienteSql.Insert, cliente);
    }

    public async Task<bool> ActualizarAsync(Cliente cliente)
    {
        using var conn = _context.CreateConnection();
        var rowsAffected = await conn.ExecuteAsync(ClienteSql.Update, cliente);
        return rowsAffected > 0;
    }

    public async Task<bool> EliminarAsync(int idCliente)
    {
        using var conn = _context.CreateConnection();
        var rowsAffected = await conn.ExecuteAsync(ClienteSql.Delete, new { IdCliente = idCliente });
        return rowsAffected > 0;
    }
}
