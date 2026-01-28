using FluentValidation;
using ExamenProcomerBackend.Application.Extras.Commands;

namespace ExamenProcomerBackend.Application.Extras.Validators;

public sealed class ActualizarExtraCommandValidator : AbstractValidator<ActualizarExtraCommand>
{
    public ActualizarExtraCommandValidator()
    {
        RuleFor(x => x.IdExtra)
            .GreaterThan(0).WithMessage("El ID de extra debe ser mayor a 0.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es requerida.")
            .MaximumLength(150).WithMessage("La descripción no puede exceder 150 caracteres.");

        // Regla de Negocio: Cada extra tiene un costo diario fijo
        RuleFor(x => x.Costo)
            .GreaterThan(0).WithMessage("El costo debe ser mayor a 0.");
    }
}
