using ExamenProcomerBackend.Application.Vehiculos.Commands;
using ExamenProcomerBackend.Application.Common;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Domain.Entities;

namespace ExamenProcomerBackend.Application.Vehiculos.Handlers;

public sealed class VehiculoCommandHandler
{
    private readonly IVehiculoCommandRepository _commandRepository;
    private readonly IVehiculoQueryRepository _queryRepository;
    private readonly ICategoriaVehiculoQueryRepository _categoriaRepository;

    public VehiculoCommandHandler(
        IVehiculoCommandRepository commandRepository,
        IVehiculoQueryRepository queryRepository,
        ICategoriaVehiculoQueryRepository categoriaRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<OperationResult<int>> HandleCrearAsync(CrearVehiculoCommand command)
    {
        var vehiculo = new Vehiculo
        {
            IdCategoria = command.IdCategoria,
            Descripcion = command.Descripcion,
            Costo = command.Costo
        };

        var id = await _commandRepository.CrearAsync(vehiculo);
        return OperationResult<int>.Ok(id);
    }

    public async Task<OperationResult> HandleActualizarAsync(ActualizarVehiculoCommand command)
    {
        var vehiculoExistente = await _queryRepository.ObtenerPorIdAsync(command.IdVehiculo);
        if (vehiculoExistente == null)
            return OperationResult.Fail("El vehículo no existe.");

        var vehiculo = new Vehiculo
        {
            IdVehiculo = command.IdVehiculo,
            IdCategoria = command.IdCategoria,
            Descripcion = command.Descripcion,
            Costo = command.Costo
        };

        var actualizado = await _commandRepository.ActualizarAsync(vehiculo);
        return actualizado ? OperationResult.Ok() : OperationResult.Fail("No se pudo actualizar el vehículo.");
    }

    public async Task<OperationResult> HandleEliminarAsync(EliminarVehiculoCommand command)
    {
        var vehiculoExistente = await _queryRepository.ObtenerPorIdAsync(command.IdVehiculo);
        if (vehiculoExistente == null)
            return OperationResult.Fail("El vehículo no existe.");

        var eliminado = await _commandRepository.EliminarAsync(command.IdVehiculo);
        return eliminado ? OperationResult.Ok() : OperationResult.Fail("No se pudo eliminar el vehículo.");
    }
}
