using FluentValidation;
using ExamenProcomerBackend.Application.Extras.Commands;
using ExamenProcomerBackend.Application.Extras.Queries;
using ExamenProcomerBackend.Application.Extras.Handlers;

namespace ExamenProcomerBackend.API.Endpoints;

public static class ExtrasEndpoints
{
    public static void MapExtrasEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/extras")
            .WithTags("Extras")
            .RequireAuthorization();

        group.MapGet("/", ListarExtras)
            .WithName("ListarExtras")
            .WithSummary("Listar todos los extras");

        group.MapGet("/{id:int}", ObtenerExtraPorId)
            .WithName("ObtenerExtraPorId")
            .WithSummary("Obtener un extra por ID");

        group.MapPost("/", CrearExtra)
            .WithName("CrearExtra")
            .WithSummary("Crear un nuevo extra");

        group.MapPut("/{id:int}", ActualizarExtra)
            .WithName("ActualizarExtra")
            .WithSummary("Actualizar un extra existente");

        group.MapDelete("/{id:int}", EliminarExtra)
            .WithName("EliminarExtra")
            .WithSummary("Eliminar un extra");
    }

    private static async Task<IResult> ListarExtras(ExtraQueryHandler handler)
    {
        var result = await handler.HandleListarAsync(new ListarExtrasQuery());
        return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ObtenerExtraPorId(int id, ExtraQueryHandler handler)
    {
        var result = await handler.HandleObtenerPorIdAsync(new ObtenerExtraPorIdQuery(id));
        return result.Success ? Results.Ok(result.Data) : Results.NotFound(result.Error);
    }

    private static async Task<IResult> CrearExtra(
        CrearExtraCommand command,
        IValidator<CrearExtraCommand> validator,
        ExtraCommandHandler handler)
    {
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var result = await handler.HandleCrearAsync(command);
        return result.Success 
            ? Results.Created($"/api/extras/{result.Data}", result.Data) 
            : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ActualizarExtra(
        int id,
        ActualizarExtraCommand command,
        IValidator<ActualizarExtraCommand> validator,
        ExtraCommandHandler handler)
    {
        if (id != command.IdExtra)
            return Results.BadRequest("El ID de la URL no coincide con el ID del comando.");

        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var result = await handler.HandleActualizarAsync(command);
        return result.Success ? Results.NoContent() : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> EliminarExtra(int id, ExtraCommandHandler handler)
    {
        var result = await handler.HandleEliminarAsync(new EliminarExtraCommand(id));
        return result.Success ? Results.NoContent() : Results.BadRequest(result.Error);
    }
}
