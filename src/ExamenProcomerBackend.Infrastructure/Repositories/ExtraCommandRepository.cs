using Dapper;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Domain.Entities;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories.Sql;

namespace ExamenProcomerBackend.Infrastructure.Repositories;

public sealed class ExtraCommandRepository : IExtraCommandRepository
{
    private readonly DapperContext _context;

    public ExtraCommandRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> CrearAsync(Extra extra)
    {
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(ExtraSql.Crear, extra);
    }

    public async Task<bool> ActualizarAsync(Extra extra)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(ExtraSql.Actualizar, extra);
        return rows > 0;
    }

    public async Task<bool> EliminarAsync(int idExtra)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(ExtraSql.Eliminar, new { IdExtra = idExtra });
        return rows > 0;
    }
}
