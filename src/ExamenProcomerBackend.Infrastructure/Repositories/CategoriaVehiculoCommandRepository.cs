using Dapper;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Domain.Entities;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories.Sql;

namespace ExamenProcomerBackend.Infrastructure.Repositories;

public sealed class CategoriaVehiculoCommandRepository : ICategoriaVehiculoCommandRepository
{
    private readonly DapperContext _context;

    public CategoriaVehiculoCommandRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> CrearAsync(CategoriaVehiculo categoria)
    {
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(CategoriaVehiculoSql.Crear, categoria);
    }

    public async Task<bool> ActualizarAsync(CategoriaVehiculo categoria)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(CategoriaVehiculoSql.Actualizar, categoria);
        return rows > 0;
    }

    public async Task<bool> EliminarAsync(int idCategoria)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(CategoriaVehiculoSql.Eliminar, new { IdCategoria = idCategoria });
        return rows > 0;
    }
}
