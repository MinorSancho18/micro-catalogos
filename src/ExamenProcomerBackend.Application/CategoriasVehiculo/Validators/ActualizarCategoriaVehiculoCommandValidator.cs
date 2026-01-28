using FluentValidation;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Commands;
using ExamenProcomerBackend.Application.Interfaces;

namespace ExamenProcomerBackend.Application.CategoriasVehiculo.Validators;

public sealed class ActualizarCategoriaVehiculoCommandValidator : AbstractValidator<ActualizarCategoriaVehiculoCommand>
{
    private readonly ICategoriaVehiculoQueryRepository _queryRepository;

    public ActualizarCategoriaVehiculoCommandValidator(ICategoriaVehiculoQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;

        RuleFor(x => x.IdCategoria)
            .GreaterThan(0).WithMessage("El ID de categoría debe ser mayor a 0.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es requerida.")
            .MaximumLength(100).WithMessage("La descripción no puede exceder 100 caracteres.");

        // Regla de Negocio: Cada categoría debe tener un código único de 3 caracteres
        RuleFor(x => x.Codigo)
            .NotEmpty().WithMessage("El código es requerido.")
            .Length(3).WithMessage("El código debe tener exactamente 3 caracteres.")
            .MustAsync(async (command, codigo, cancellation) => 
                !await _queryRepository.ExisteCodigoAsync(codigo, command.IdCategoria))
            .WithMessage("El código ya existe.");
    }
}
