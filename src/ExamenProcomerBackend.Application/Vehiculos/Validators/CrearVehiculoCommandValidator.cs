using FluentValidation;
using ExamenProcomerBackend.Application.Vehiculos.Commands;
using ExamenProcomerBackend.Application.Interfaces;

namespace ExamenProcomerBackend.Application.Vehiculos.Validators;

public sealed class CrearVehiculoCommandValidator : AbstractValidator<CrearVehiculoCommand>
{
    private readonly ICategoriaVehiculoQueryRepository _categoriaRepository;

    public CrearVehiculoCommandValidator(ICategoriaVehiculoQueryRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;

        // Regla de Negocio: Todo vehículo debe pertenecer a una categoría válida
        RuleFor(x => x.IdCategoria)
            .GreaterThan(0).WithMessage("El ID de categoría debe ser mayor a 0.")
            .MustAsync(async (id, cancellation) => await _categoriaRepository.ObtenerPorIdAsync(id) != null)
            .WithMessage("La categoría especificada no existe.");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripción es requerida.")
            .MaximumLength(150).WithMessage("La descripción no puede exceder 150 caracteres.");

        // Regla de Negocio: El costo del vehículo representa la tarifa base diaria
        RuleFor(x => x.Costo)
            .GreaterThan(0).WithMessage("El costo debe ser mayor a 0.");
    }
}
