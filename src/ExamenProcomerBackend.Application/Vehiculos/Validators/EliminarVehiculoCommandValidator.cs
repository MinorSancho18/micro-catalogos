using FluentValidation;
using ExamenProcomerBackend.Application.Vehiculos.Commands;

namespace ExamenProcomerBackend.Application.Vehiculos.Validators;

public sealed class EliminarVehiculoCommandValidator : AbstractValidator<EliminarVehiculoCommand>
{
    public EliminarVehiculoCommandValidator()
    {
        RuleFor(x => x.IdVehiculo)
            .GreaterThan(0).WithMessage("El ID de vehículo debe ser mayor a 0.");
    }
}
