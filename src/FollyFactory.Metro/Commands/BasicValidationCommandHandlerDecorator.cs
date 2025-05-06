using FollyFactory.Metro.Validation;

namespace FollyFactory.Metro.Commands;

public class BasicValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    private readonly ICommandValidator<TCommand>? _validator;
    private readonly ICommandHandler<TCommand> _decoratedCommandHandler;

    public BasicValidationCommandHandlerDecorator(ICommandHandler<TCommand> decoratedCommandHandler, ICommandValidator<TCommand>? validator = null)
    {
        _validator = validator;
        _decoratedCommandHandler = decoratedCommandHandler;
    }

    public async Task<CommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        if (_validator == null)
        {
            return await _decoratedCommandHandler.HandleAsync(command, cancellationToken);
        }

        IValidationResult validationResult = await _validator.ValidateCommand(command, cancellationToken);
        if (validationResult.IsValid)
        {
            return await _decoratedCommandHandler.HandleAsync(command, cancellationToken);
        }

        Dictionary<string, string[]> validationErrors = new();
        foreach (ValidationError error in validationResult.Errors)
        {
            validationErrors.TryAdd(error.PropertyName, error.ErrorMessages.ToArray());
        }
        var commandResult = CommandResult.Failure(validationErrors);
        return commandResult;
    }
}
