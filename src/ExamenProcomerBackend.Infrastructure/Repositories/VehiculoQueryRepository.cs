using Dapper;
using ExamenProcomerBackend.Application.Vehiculos.DTOs;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories.Sql;

namespace ExamenProcomerBackend.Infrastructure.Repositories;

public sealed class VehiculoQueryRepository : IVehiculoQueryRepository
{
    private readonly DapperContext _context;

    public VehiculoQueryRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VehiculoDto>> ListarTodosAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<VehiculoDto>(VehiculoSql.ListarTodos);
    }

    public async Task<VehiculoDto?> ObtenerPorIdAsync(int idVehiculo)
    {
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<VehiculoDto>(
            VehiculoSql.ObtenerPorId,
            new { IdVehiculo = idVehiculo });
    }

    public async Task<IEnumerable<VehiculoDto>> ListarPorCategoriaAsync(int idCategoria)
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<VehiculoDto>(
            VehiculoSql.ListarPorCategoria,
            new { IdCategoria = idCategoria });
    }

    public async Task<bool> ExisteVehiculoPorCategoriaAsync(int idCategoria)
    {
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteScalarAsync<int>(
            VehiculoSql.ExisteVehiculoPorCategoria,
            new { IdCategoria = idCategoria });
        return result == 1;
    }
}
