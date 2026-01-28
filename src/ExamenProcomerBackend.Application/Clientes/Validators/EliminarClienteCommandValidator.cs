using FluentValidation;
using ExamenProcomerBackend.Application.Clientes.Commands;

namespace ExamenProcomerBackend.Application.Clientes.Validators;

public sealed class EliminarClienteCommandValidator : AbstractValidator<EliminarClienteCommand>
{
    public EliminarClienteCommandValidator()
    {
        RuleFor(x => x.IdCliente)
            .GreaterThan(0).WithMessage("El ID de cliente debe ser mayor a 0.");
    }
}
