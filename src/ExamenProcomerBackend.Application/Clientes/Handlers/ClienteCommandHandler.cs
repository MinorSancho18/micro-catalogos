using ExamenProcomerBackend.Application.Clientes.Commands;
using ExamenProcomerBackend.Application.Common;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Domain.Entities;

namespace ExamenProcomerBackend.Application.Clientes.Handlers;

public sealed class ClienteCommandHandler
{
    private readonly IClienteCommandRepository _commandRepository;
    private readonly IClienteQueryRepository _queryRepository;

    public ClienteCommandHandler(
        IClienteCommandRepository commandRepository,
        IClienteQueryRepository queryRepository)
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
    }

    public async Task<OperationResult<int>> HandleCrearAsync(CrearClienteCommand command)
    {
        // Regla de Negocio: El número de cédula es único en el sistema (validado en validator)
        var cliente = new Cliente
        {
            Nombre = command.Nombre,
            NumeroCedula = command.NumeroCedula
        };

        var id = await _commandRepository.CrearAsync(cliente);
        
        // TODO: Emitir evento ClienteCreado según reglas de negocio
        
        return OperationResult<int>.Ok(id);
    }

    public async Task<OperationResult> HandleActualizarAsync(ActualizarClienteCommand command)
    {
        var clienteExistente = await _queryRepository.ObtenerPorIdAsync(command.IdCliente);
        if (clienteExistente == null)
            return OperationResult.Fail("El cliente no existe.");

        var cliente = new Cliente
        {
            IdCliente = command.IdCliente,
            Nombre = command.Nombre,
            NumeroCedula = command.NumeroCedula
        };

        var actualizado = await _commandRepository.ActualizarAsync(cliente);
        return actualizado ? OperationResult.Ok() : OperationResult.Fail("No se pudo actualizar el cliente.");
    }

    public async Task<OperationResult> HandleEliminarAsync(EliminarClienteCommand command)
    {
        var clienteExistente = await _queryRepository.ObtenerPorIdAsync(command.IdCliente);
        if (clienteExistente == null)
            return OperationResult.Fail("El cliente no existe.");

        // Regla de Negocio: El microservicio NO valida si el cliente tiene contratos activos o deuda
        // Esa validación es responsabilidad de otro microservicio
        var eliminado = await _commandRepository.EliminarAsync(command.IdCliente);
        return eliminado ? OperationResult.Ok() : OperationResult.Fail("No se pudo eliminar el cliente.");
    }
}
