using FollyFactory.Metro.Commands;
using Shouldly;

namespace FollyFactory.Metro.UnitTests.Commands;

public class ValidationCommandHandlerDecoratorTests
{
    [Fact]
    public async Task Handle_Returns_Result()
    {
        var testCommand = new TestCommand1("Bill");
        var testHandler = new TestCommand1Handler();
        var decorator = new ValidationCommandHandlerDecorator<TestCommand1>(testHandler);

        var result = await decorator.HandleAsync(testCommand, CancellationToken.None);

        result.ShouldNotBeNull();
    }
}

internal record TestCommand1(string Name) : ICommand;

internal class TestCommand1Handler : ICommandHandler<TestCommand1>
{
    public Task<CommandResult> HandleAsync(TestCommand1 command, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CommandResult(true));
    }
}