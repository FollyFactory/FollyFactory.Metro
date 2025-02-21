using FollyFactory.Metro.Validation;

namespace FollyFactory.Metro.Commands;

public interface ICommandValidator<in TCommand>
{
    Task<IValidationResult> ValidateCommand(TCommand command);
}