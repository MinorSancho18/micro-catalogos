using ExamenProcomerBackend.Application.CategoriasVehiculo.Commands;
using ExamenProcomerBackend.Application.Common;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Domain.Entities;

namespace ExamenProcomerBackend.Application.CategoriasVehiculo.Handlers;

public sealed class CategoriaVehiculoCommandHandler
{
    private readonly ICategoriaVehiculoCommandRepository _commandRepository;
    private readonly ICategoriaVehiculoQueryRepository _queryRepository;
    private readonly IVehiculoQueryRepository _vehiculoQueryRepository;

    public CategoriaVehiculoCommandHandler(
        ICategoriaVehiculoCommandRepository commandRepository,
        ICategoriaVehiculoQueryRepository queryRepository,
        IVehiculoQueryRepository vehiculoQueryRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
        _vehiculoQueryRepository = vehiculoQueryRepository;
    }

    public async Task<OperationResult<int>> HandleCrearAsync(CrearCategoriaVehiculoCommand command)
    {
        // Normalizar código a mayúsculas para consistencia
        var categoria = new CategoriaVehiculo
        {
            Descripcion = command.Descripcion,
            Codigo = command.Codigo.ToUpper()
        };

        var id = await _commandRepository.CrearAsync(categoria);
        return OperationResult<int>.Ok(id);
    }

    public async Task<OperationResult> HandleActualizarAsync(ActualizarCategoriaVehiculoCommand command)
    {
        var categoriaExistente = await _queryRepository.ObtenerPorIdAsync(command.IdCategoria);
        if (categoriaExistente == null)
            return OperationResult.Fail("La categoría no existe.");

        // Normalizar código a mayúsculas para consistencia
        var categoria = new CategoriaVehiculo
        {
            IdCategoria = command.IdCategoria,
            Descripcion = command.Descripcion,
            Codigo = command.Codigo.ToUpper()
        };

        var actualizado = await _commandRepository.ActualizarAsync(categoria);
        return actualizado ? OperationResult.Ok() : OperationResult.Fail("No se pudo actualizar la categoría.");
    }

    public async Task<OperationResult> HandleEliminarAsync(EliminarCategoriaVehiculoCommand command)
    {
        var categoriaExistente = await _queryRepository.ObtenerPorIdAsync(command.IdCategoria);
        if (categoriaExistente == null)
            return OperationResult.Fail("La categoría no existe.");

        // Regla de Negocio: No se permite eliminar una categoría si existen vehículos asociados
        var tieneVehiculos = await _vehiculoQueryRepository.ExisteVehiculoPorCategoriaAsync(command.IdCategoria);
        if (tieneVehiculos)
            return OperationResult.Fail("No se puede eliminar la categoría porque tiene vehículos asociados.");

        var eliminado = await _commandRepository.EliminarAsync(command.IdCategoria);
        return eliminado ? OperationResult.Ok() : OperationResult.Fail("No se pudo eliminar la categoría.");
    }
}
