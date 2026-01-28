using FluentValidation;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Commands;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Queries;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Handlers;

namespace ExamenProcomerBackend.API.Endpoints;

public static class CategoriasVehiculoEndpoints
{
    public static void MapCategoriasVehiculoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categorias-vehiculo")
            .WithTags("Categorías de Vehículos")
            .RequireAuthorization();

        group.MapGet("/", ListarCategorias)
            .WithName("ListarCategoriasVehiculo")
            .WithSummary("Listar todas las categorías de vehículos");

        group.MapGet("/{id:int}", ObtenerCategoriaPorId)
            .WithName("ObtenerCategoriaVehiculoPorId")
            .WithSummary("Obtener una categoría de vehículo por ID");

        group.MapPost("/", CrearCategoria)
            .WithName("CrearCategoriaVehiculo")
            .WithSummary("Crear una nueva categoría de vehículo");

        group.MapPut("/{id:int}", ActualizarCategoria)
            .WithName("ActualizarCategoriaVehiculo")
            .WithSummary("Actualizar una categoría de vehículo existente");

        group.MapDelete("/{id:int}", EliminarCategoria)
            .WithName("EliminarCategoriaVehiculo")
            .WithSummary("Eliminar una categoría de vehículo");
    }

    private static async Task<IResult> ListarCategorias(CategoriaVehiculoQueryHandler handler)
    {
        var result = await handler.HandleListarAsync(new ListarCategoriasVehiculoQuery());
        return result.Success ? Results.Ok(result.Data) : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ObtenerCategoriaPorId(int id, CategoriaVehiculoQueryHandler handler)
    {
        var result = await handler.HandleObtenerPorIdAsync(new ObtenerCategoriaVehiculoPorIdQuery(id));
        return result.Success ? Results.Ok(result.Data) : Results.NotFound(result.Error);
    }

    private static async Task<IResult> CrearCategoria(
        CrearCategoriaVehiculoCommand command,
        IValidator<CrearCategoriaVehiculoCommand> validator,
        CategoriaVehiculoCommandHandler handler)
    {
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var result = await handler.HandleCrearAsync(command);
        return result.Success 
            ? Results.Created($"/api/categorias-vehiculo/{result.Data}", result.Data) 
            : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ActualizarCategoria(
        int id,
        ActualizarCategoriaVehiculoCommand command,
        IValidator<ActualizarCategoriaVehiculoCommand> validator,
        CategoriaVehiculoCommandHandler handler)
    {
        if (id != command.IdCategoria)
            return Results.BadRequest("El ID de la URL no coincide con el ID del comando.");

        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.Errors);

        var result = await handler.HandleActualizarAsync(command);
        return result.Success ? Results.NoContent() : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> EliminarCategoria(int id, CategoriaVehiculoCommandHandler handler)
    {
        var result = await handler.HandleEliminarAsync(new EliminarCategoriaVehiculoCommand(id));
        return result.Success ? Results.NoContent() : Results.BadRequest(result.Error);
    }
}
