using FluentValidation;
using ExamenProcomerBackend.Application.Clientes.Commands;
using ExamenProcomerBackend.Application.Interfaces;

namespace ExamenProcomerBackend.Application.Clientes.Validators;

public sealed class CrearClienteCommandValidator : AbstractValidator<CrearClienteCommand>
{
    private readonly IClienteQueryRepository _queryRepository;

    public CrearClienteCommandValidator(IClienteQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MaximumLength(150).WithMessage("El nombre no puede exceder 150 caracteres.");

        // Regla de Negocio: El número de cédula es único en el sistema
        RuleFor(x => x.NumeroCedula)
            .NotEmpty().WithMessage("El número de cédula es requerido.")
            .MaximumLength(20).WithMessage("El número de cédula no puede exceder 20 caracteres.")
            .MustAsync(async (numeroCedula, cancellation) => !await _queryRepository.ExisteCedulaAsync(numeroCedula))
            .WithMessage("Ya existe un cliente con este número de cédula.");
    }
}
