using FluentValidation;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Commands;
using ExamenProcomerBackend.Application.Interfaces;

namespace ExamenProcomerBackend.Application.CategoriasVehiculo.Validators;

public sealed class CrearCategoriaVehiculoCommandValidator : AbstractValidator<CrearCategoriaVehiculoCommand>
{
    private readonly ICategoriaVehiculoQueryRepository _queryRepository;

    public CrearCategoriaVehiculoCommandValidator(ICategoriaVehiculoQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es requerida.")
            .MaximumLength(100).WithMessage("La descripción no puede exceder 100 caracteres.");

        // Regla de Negocio: Cada categoría debe tener un código único de 3 caracteres
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El código es requerido.")
            .Length(3).WithMessage("El código debe tener exactamente 3 caracteres.")
            .MustAsync(async (codigo, cancellation) => !await _queryRepository.ExisteCodigoAsync(codigo))
            .WithMessage("El código ya existe.");
    }
}
