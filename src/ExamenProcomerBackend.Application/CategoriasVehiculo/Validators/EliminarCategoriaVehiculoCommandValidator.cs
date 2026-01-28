using FluentValidation;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Commands;

namespace ExamenProcomerBackend.Application.CategoriasVehiculo.Validators;

public sealed class EliminarCategoriaVehiculoCommandValidator : AbstractValidator<EliminarCategoriaVehiculoCommand>
{
    public EliminarCategoriaVehiculoCommandValidator()
    {
        RuleFor(x => x.IdCategoria)
            .GreaterThan(0).WithMessage("El ID de categoría debe ser mayor a 0.");
    }
}
