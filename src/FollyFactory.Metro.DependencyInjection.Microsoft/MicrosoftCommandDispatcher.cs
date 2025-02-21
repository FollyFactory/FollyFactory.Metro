using FollyFactory.Metro.Commands;
using System.Diagnostics;

namespace FollyFactory.Metro.DependencyInjection.Microsoft;

public class MicrosoftCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public MicrosoftCommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<CommandResult> Dispatch(ICommand command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        var wrapperType = typeof(CommandHandler<>).MakeGenericType(command.GetType());

        var handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
        {
            throw new Exception($"Handler for {command.GetType().Name} not found");
        }

        if (Activator.CreateInstance(wrapperType, handler) is CommandHandler wrappedHandler)
        {
            return await wrappedHandler.Handle(command, cancellationToken);
        }
        else
        {
            throw new Exception("Handler creation error");
        }
    }

    private abstract class CommandHandler
    {
        public abstract Task<CommandResult> Handle(ICommand command, CancellationToken cancellationToken);
    }

    private class CommandHandler<T> : CommandHandler
        where T : ICommand
    {
        private readonly ICommandHandler<T> _handler;

        public CommandHandler(ICommandHandler<T> handler)
        {
            _handler = handler;
        }

        [DebuggerStepThrough]
        public override Task<CommandResult> Handle(ICommand command, CancellationToken cancellationToken)
        {
            return _handler.HandleAsync((T)command, cancellationToken);
        }
    }
}