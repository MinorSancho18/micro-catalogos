using ExamenProcomerBackend.Application.Extras.Commands;
using ExamenProcomerBackend.Application.Common;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Domain.Entities;

namespace ExamenProcomerBackend.Application.Extras.Handlers;

public sealed class ExtraCommandHandler
{
    private readonly IExtraCommandRepository _commandRepository;
    private readonly IExtraQueryRepository _queryRepository;

    public ExtraCommandHandler(
        IExtraCommandRepository commandRepository,
        IExtraQueryRepository queryRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
    }

    public async Task<OperationResult<int>> HandleCrearAsync(CrearExtraCommand command)
    {
        var extra = new Extra
        {
            Descripcion = command.Descripcion,
            Costo = command.Costo
        };

        var id = await _commandRepository.CrearAsync(extra);
        return OperationResult<int>.Ok(id);
    }

    public async Task<OperationResult> HandleActualizarAsync(ActualizarExtraCommand command)
    {
        var extraExistente = await _queryRepository.ObtenerPorIdAsync(command.IdExtra);
        if (extraExistente == null)
            return OperationResult.Fail("El extra no existe.");

        var extra = new Extra
        {
            IdExtra = command.IdExtra,
            Descripcion = command.Descripcion,
            Costo = command.Costo
        };

        var actualizado = await _commandRepository.ActualizarAsync(extra);
        return actualizado ? OperationResult.Ok() : OperationResult.Fail("No se pudo actualizar el extra.");
    }

    public async Task<OperationResult> HandleEliminarAsync(EliminarExtraCommand command)
    {
        var extraExistente = await _queryRepository.ObtenerPorIdAsync(command.IdExtra);
        if (extraExistente == null)
            return OperationResult.Fail("El extra no existe.");

        var eliminado = await _commandRepository.EliminarAsync(command.IdExtra);
        return eliminado ? OperationResult.Ok() : OperationResult.Fail("No se pudo eliminar el extra.");
    }
}
