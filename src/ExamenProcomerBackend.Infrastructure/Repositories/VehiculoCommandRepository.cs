using Dapper;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Domain.Entities;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories.Sql;

namespace ExamenProcomerBackend.Infrastructure.Repositories;

public sealed class VehiculoCommandRepository : IVehiculoCommandRepository
{
    private readonly DapperContext _context;

    public VehiculoCommandRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> CrearAsync(Vehiculo vehiculo)
    {
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(VehiculoSql.Crear, vehiculo);
    }

    public async Task<bool> ActualizarAsync(Vehiculo vehiculo)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(VehiculoSql.Actualizar, vehiculo);
        return rows > 0;
    }

    public async Task<bool> EliminarAsync(int idVehiculo)
    {
        using var connection = _context.CreateConnection();
        var rows = await connection.ExecuteAsync(VehiculoSql.Eliminar, new { IdVehiculo = idVehiculo });
        return rows > 0;
    }
}
