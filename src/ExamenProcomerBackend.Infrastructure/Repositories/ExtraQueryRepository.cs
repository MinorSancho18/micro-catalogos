using Dapper;
using ExamenProcomerBackend.Application.Extras.DTOs;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories.Sql;

namespace ExamenProcomerBackend.Infrastructure.Repositories;

public sealed class ExtraQueryRepository : IExtraQueryRepository
{
    private readonly DapperContext _context;

    public ExtraQueryRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExtraDto>> ListarTodosAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<ExtraDto>(ExtraSql.ListarTodos);
    }

    public async Task<ExtraDto?> ObtenerPorIdAsync(int idExtra)
    {
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ExtraDto>(
            ExtraSql.ObtenerPorId,
            new { IdExtra = idExtra });
    }
}
