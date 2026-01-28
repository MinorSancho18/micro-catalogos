using FluentValidation;
using FluentValidation.Results;
using System.Threading;

namespace ExamenProcomerBackend.Application.Tests;

public class FakeValidator<T> : AbstractValidator<T>
{
    private readonly ValidationResult _result;
    public FakeValidator() => _result = new ValidationResult();
    public FakeValidator(ValidationResult result) => _result = result;
    public override ValidationResult Validate(ValidationContext<T> context) => _result;
    public override Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = default) => Task.FromResult(_result);
}
 
