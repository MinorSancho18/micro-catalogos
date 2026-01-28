using FluentValidation;
using ExamenProcomerBackend.Application.Extras.Commands;

namespace ExamenProcomerBackend.Application.Extras.Validators;

public sealed class EliminarExtraCommandValidator : AbstractValidator<EliminarExtraCommand>
{
    public EliminarExtraCommandValidator()
    {
        RuleFor(x => x.IdExtra)
            .GreaterThan(0).WithMessage("El ID de extra debe ser mayor a 0.");
    }
}
