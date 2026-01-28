using FluentValidation;
using ExamenProcomerBackend.Application.Vehiculos.Commands;
using ExamenProcomerBackend.Application.Vehiculos.Queries;
using ExamenProcomerBackend.Application.Vehiculos.Handlers;

namespace ExamenProcomerBackend.API.Endpoints;

public static class VehiculosEndpoints
{
    public static void MapVehiculosEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vehiculos")
            .WithTags("Vehículos")
            .RequireAuthorization();

        group.MapGet("/", ListarVehiculos)
            .WithName("ListarVehiculos")
            .WithSummary("Listar todos los vehículos");

        group.MapGet("/{id:int}", ObtenerVehiculoPorId)
            .WithName("ObtenerVehiculoPorId")
            .WithSummary("Obtener un vehículo por ID");

        group.MapGet("/categoria/{idCategoria:int}", ListarVehiculosPorCategoria)
            .WithName("ListarVehiculosPorCategoria")
            .WithSummary("Listar vehículos por categoría");

        group.MapPost("/", CrearVehiculo)
            .WithName("CrearVehiculo")
            .WithSummary("Crear un nuevo vehículo");

        group.MapPut("/{id:int}", ActualizarVehiculo)
            .WithName("ActualizarVehiculo")
            .WithSummary("Actualizar un vehículo existente");

        group.MapDelete("/{id:int}", EliminarVehiculo)
            .WithName("EliminarVehiculo")
            .WithSummary("Eliminar un vehículo");
    }

    private static async Task<IResult> ListarVehiculos(VehiculoQueryHandler handler)
    {
        var result = await handler.HandleListarAsync(new ListarVehiculosQuery());
        return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ObtenerVehiculoPorId(int id, VehiculoQueryHandler handler)
    {
        var result = await handler.HandleObtenerPorIdAsync(new ObtenerVehiculoPorIdQuery(id));
        return result.Success ? Results.Ok(result.Data) : Results.NotFound(result.Error);
    }

    private static async Task<IResult> ListarVehiculosPorCategoria(int idCategoria, VehiculoQueryHandler handler)
    {
        var result = await handler.HandleListarPorCategoriaAsync(new ListarVehiculosPorCategoriaQuery(idCategoria));
        return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> CrearVehiculo(
        CrearVehiculoCommand command,
        IValidator<CrearVehiculoCommand> validator,
        VehiculoCommandHandler handler)
    {
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var result = await handler.HandleCrearAsync(command);
        return result.Success 
            ? Results.Created($"/api/vehiculos/{result.Data}", result.Data) 
            : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ActualizarVehiculo(
        int id,
        ActualizarVehiculoCommand command,
        IValidator<ActualizarVehiculoCommand> validator,
        VehiculoCommandHandler handler)
    {
        if (id != command.IdVehiculo)
            return Results.BadRequest("El ID de la URL no coincide con el ID del comando.");

        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var result = await handler.HandleActualizarAsync(command);
        return result.Success ? Results.NoContent() : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> EliminarVehiculo(int id, VehiculoCommandHandler handler)
    {
        var result = await handler.HandleEliminarAsync(new EliminarVehiculoCommand(id));
        return result.Success ? Results.NoContent() : Results.BadRequest(result.Error);
    }
}
