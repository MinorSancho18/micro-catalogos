using FluentValidation;
using ExamenProcomerBackend.Application.Clientes.Commands;
using ExamenProcomerBackend.Application.Clientes.Queries;
using ExamenProcomerBackend.Application.Clientes.Handlers;

namespace ExamenProcomerBackend.API.Endpoints;

public static class ClientesEndpoints
{
    public static void MapClientesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clientes")
            .WithTags("Clientes")
            .RequireAuthorization();

        group.MapGet("/", ListarClientes)
            .WithName("ListarClientes")
            .WithSummary("Listar todos los clientes");

        group.MapGet("/{id:int}", ObtenerClientePorId)
            .WithName("ObtenerClientePorId")
            .WithSummary("Obtener un cliente por ID");

        group.MapGet("/cedula/{numeroCedula}", ObtenerClientePorCedula)
            .WithName("ObtenerClientePorCedula")
            .WithSummary("Obtener un cliente por número de cédula");

        group.MapPost("/", CrearCliente)
            .WithName("CrearCliente")
            .WithSummary("Crear un nuevo cliente");

        group.MapPut("/{id:int}", ActualizarCliente)
            .WithName("ActualizarCliente")
            .WithSummary("Actualizar un cliente existente");

        group.MapDelete("/{id:int}", EliminarCliente)
            .WithName("EliminarCliente")
            .WithSummary("Eliminar un cliente");
    }

    private static async Task<IResult> ListarClientes(ClienteQueryHandler handler)
    {
        var result = await handler.HandleListarAsync(new ListarClientesQuery());
        return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ObtenerClientePorId(int id, ClienteQueryHandler handler)
    {
        var result = await handler.HandleObtenerPorIdAsync(new ObtenerClientePorIdQuery(id));
        return result.Success ? Results.Ok(result.Data) : Results.NotFound(result.Error);
    }

    private static async Task<IResult> ObtenerClientePorCedula(string numeroCedula, ClienteQueryHandler handler)
    {
        var result = await handler.HandleObtenerPorCedulaAsync(new ObtenerClientePorCedulaQuery(numeroCedula));
        return result.Success ? Results.Ok(result.Data) : Results.NotFound(result.Error);
    }

    private static async Task<IResult> CrearCliente(
        CrearClienteCommand command,
        IValidator<CrearClienteCommand> validator,
        ClienteCommandHandler handler)
    {
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var result = await handler.HandleCrearAsync(command);
        return result.Success 
            ? Results.Created($"/api/clientes/{result.Data}", result.Data) 
            : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ActualizarCliente(
        int id,
        ActualizarClienteCommand command,
        IValidator<ActualizarClienteCommand> validator,
        ClienteCommandHandler handler)
    {
        if (id != command.IdCliente)
            return Results.BadRequest("El ID del cliente no coincide.");

        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var result = await handler.HandleActualizarAsync(command);
        return result.Success ? Results.Ok() : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> EliminarCliente(int id, ClienteCommandHandler handler)
    {
        var result = await handler.HandleEliminarAsync(new EliminarClienteCommand(id));
        return result.Success ? Results.NoContent() : Results.BadRequest(result.Error);
    }
}
