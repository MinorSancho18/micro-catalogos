using ExamenProcomerBackend.Application.Vehiculos.DTOs;
using ExamenProcomerBackend.Application.Vehiculos.Queries;
using ExamenProcomerBackend.Application.Common;
using ExamenProcomerBackend.Application.Interfaces;

namespace ExamenProcomerBackend.Application.Vehiculos.Handlers;

public sealed class VehiculoQueryHandler
{
    private readonly IVehiculoQueryRepository _queryRepository;

    public VehiculoQueryHandler(IVehiculoQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public async Task<OperationResult<IEnumerable<VehiculoDto>>> HandleListarAsync(ListarVehiculosQuery query)
    {
        var vehiculos = await _queryRepository.ListarTodosAsync();
        return OperationResult<IEnumerable<VehiculoDto>>.Ok(vehiculos);
    }

    public async Task<OperationResult<VehiculoDto>> HandleObtenerPorIdAsync(ObtenerVehiculoPorIdQuery query)
    {
        var vehiculo = await _queryRepository.ObtenerPorIdAsync(query.IdVehiculo);
        if (vehiculo == null)
            return OperationResult<VehiculoDto>.Fail("El vehículo no existe.");

        return OperationResult<VehiculoDto>.Ok(vehiculo);
    }

    public async Task<OperationResult<IEnumerable<VehiculoDto>>> HandleListarPorCategoriaAsync(ListarVehiculosPorCategoriaQuery query)
    {
        var vehiculos = await _queryRepository.ListarPorCategoriaAsync(query.IdCategoria);
        return OperationResult<IEnumerable<VehiculoDto>>.Ok(vehiculos);
    }
}
