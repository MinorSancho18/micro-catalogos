using FluentValidation;
using ExamenProcomerBackend.Application.Clientes.Commands;
using ExamenProcomerBackend.Application.Interfaces;

namespace ExamenProcomerBackend.Application.Clientes.Validators;

public sealed class ActualizarClienteCommandValidator : AbstractValidator<ActualizarClienteCommand>
{
    private readonly IClienteQueryRepository _queryRepository;

    public ActualizarClienteCommandValidator(IClienteQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;

        RuleFor(x => x.IdCliente)
            .GreaterThan(0).WithMessage("El ID de cliente debe ser mayor a 0.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(150).WithMessage("El nombre no puede exceder 150 caracteres.");

        // Regla de Negocio: El número de cédula es único en el sistema
        RuleFor(x => x)
            .MustAsync(async (command, cancellation) => 
                !await _queryRepository.ExisteCedulaAsync(command.NumeroCedula, command.IdCliente))
            .WithMessage("Ya existe un cliente con este número de cédula.");

        RuleFor(x => x.NumeroCedula)
            .NotEmpty().WithMessage("El número de cédula es requerido.")
            .MaximumLength(20).WithMessage("El número de cédula no puede exceder 20 caracteres.");
    }
}
