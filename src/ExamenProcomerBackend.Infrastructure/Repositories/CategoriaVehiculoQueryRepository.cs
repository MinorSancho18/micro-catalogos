using Dapper;
using ExamenProcomerBackend.Application.CategoriasVehiculo.DTOs;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories.Sql;

namespace ExamenProcomerBackend.Infrastructure.Repositories;

public sealed class CategoriaVehiculoQueryRepository : ICategoriaVehiculoQueryRepository
{
    private readonly DapperContext _context;

    public CategoriaVehiculoQueryRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoriaVehiculoDto>> ListarTodosAsync()
    {
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<CategoriaVehiculoDto>(CategoriaVehiculoSql.ListarTodos);
    }

    public async Task<CategoriaVehiculoDto?> ObtenerPorIdAsync(int idCategoria)
    {
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<CategoriaVehiculoDto>(
            CategoriaVehiculoSql.ObtenerPorId,
            new { IdCategoria = idCategoria });
    }

    public async Task<bool> ExisteCodigoAsync(string codigo, int? idCategoriaExcluir = null)
    {
        using var connection = _context.CreateConnection();
        var existe = await connection.ExecuteScalarAsync<int>(
            CategoriaVehiculoSql.ExisteCodigo,
            new { Codigo = codigo, IdCategoriaExcluir = idCategoriaExcluir });
        return existe == 1;
    }
}
