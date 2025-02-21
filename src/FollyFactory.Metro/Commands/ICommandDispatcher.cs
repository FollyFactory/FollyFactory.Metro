namespace FollyFactory.Metro.Commands;

public interface ICommandDispatcher
{
    Task<CommandResult> Dispatch(ICommand command, CancellationToken cancellationToken = default);
}