namespace FollyFactory.Metro.Commands;

public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    private readonly ICommandValidator<TCommand>? _validator;
    private readonly ICommandHandler<TCommand> _decoratee;

    public ValidationCommandHandlerDecorator(ICommandHandler<TCommand> decoratee, ICommandValidator<TCommand>? validator = null)
    {
        _validator = validator;
        _decoratee = decoratee;
    }

    public async Task<CommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        //IValidationResult results = new();
        //if (_validator != null)
        //{
        //    results = await _validator.ValidateCommand(command);
        //}

        //if (results.IsValid)
        //{
        // return await _decoratee.HandleAsync(command, cancellationToken);
        //}
        //else
        //{
        //    throw new CommandValidationException(results);
        //}

        return await _decoratee.HandleAsync(command, cancellationToken);
    }
}