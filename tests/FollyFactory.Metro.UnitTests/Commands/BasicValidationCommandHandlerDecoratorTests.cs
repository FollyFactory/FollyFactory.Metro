using FollyFactory.Metro.Commands;
using FollyFactory.Metro.Validation;
using Shouldly;

#pragma warning disable CS8604 // Possible null reference argument.

namespace FollyFactory.Metro.UnitTests.Commands;

public class BasicValidationCommandHandlerDecoratorTests
{
    [Fact]
    public async Task Validate_WhenCommandIsValid_ReturnsSuccessful()
    {
        var testCommand = new TestCommand1("Bill", 21);
        var testHandler = new TestCommand1Handler();
        var testCommandValidator = new TestCommand1Validator();
        var decorator = new BasicValidationCommandHandlerDecorator<TestCommand1>(testHandler, testCommandValidator);

        var result = await decorator.HandleAsync(testCommand, CancellationToken.None);

        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Validate_WhenCommandNameIsNotValid_ReturnsFailure(string? name)
    {
        var testCommand = new TestCommand1(name, 34);
        var testHandler = new TestCommand1Handler();
        var testCommandValidator = new TestCommand1Validator();
        var decorator = new BasicValidationCommandHandlerDecorator<TestCommand1>(testHandler, testCommandValidator);

        var result = await decorator.HandleAsync(testCommand, CancellationToken.None);

        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Validate_WhenCommandNameIsNullOrEmpty_CorrectErrors(string? name)
    {
        var testCommand = new TestCommand1(name, 69);
        var testHandler = new TestCommand1Handler();
        var testCommandValidator = new TestCommand1Validator();
        var decorator = new BasicValidationCommandHandlerDecorator<TestCommand1>(testHandler, testCommandValidator);

        var result = await decorator.HandleAsync(testCommand, CancellationToken.None);

        result.ShouldNotBeNull();
        result.ValidationErrors.ShouldNotBeNull();
        result.ValidationErrors.Count.ShouldBe(1);
        result.ValidationErrors.ShouldContainKey("Name");
        result.ValidationErrors["Name"].ShouldNotBeNull();
        result.ValidationErrors["Name"].Length.ShouldBe(1);
        result.ValidationErrors["Name"][0].ShouldBe("Name is required");
    }

    [Fact]
    public async Task Validate_WhenCommandPropertyIsMultiInvalid_CorrectErrors()
    {
        var testCommand = new TestCommand1("Multi", 42);
        var testHandler = new TestCommand1Handler();
        var testCommandValidator = new TestCommand1Validator();
        var decorator = new BasicValidationCommandHandlerDecorator<TestCommand1>(testHandler, testCommandValidator);

        var result = await decorator.HandleAsync(testCommand, CancellationToken.None);

        result.ShouldNotBeNull();
        result.ValidationErrors.ShouldNotBeNull();
        result.ValidationErrors.Count.ShouldBe(1);
        result.ValidationErrors.ShouldContainKey("Name");
        result.ValidationErrors["Name"].ShouldNotBeNull();
        result.ValidationErrors["Name"].Length.ShouldBe(2);
        result.ValidationErrors["Name"][0].ShouldBe("Multi error 1");
        result.ValidationErrors["Name"][1].ShouldBe("Multi error 2");
    }
    
    [Fact]
    public async Task Validate_WhenCommandMultiplePropertiesInvalid_CorrectErrors()
    {
        var testCommand = new TestCommand1(string.Empty, 16);
        var testHandler = new TestCommand1Handler();
        var testCommandValidator = new TestCommand1Validator();
        var decorator = new BasicValidationCommandHandlerDecorator<TestCommand1>(testHandler, testCommandValidator);

        var result = await decorator.HandleAsync(testCommand, CancellationToken.None);

        result.ShouldNotBeNull();
        result.ValidationErrors.ShouldNotBeNull();
        result.ValidationErrors.Count.ShouldBe(2);
        result.ValidationErrors.ShouldContainKey("Name");
        result.ValidationErrors["Name"].ShouldNotBeNull();
        result.ValidationErrors["Age"].ShouldNotBeNull();
        
        result.ValidationErrors["Name"].Length.ShouldBe(1);
        result.ValidationErrors["Name"][0].ShouldBe("Name is required");
        result.ValidationErrors["Age"].Length.ShouldBe(1);
        result.ValidationErrors["Age"][0].ShouldBe("Must be 18 or older");
    }
}

internal record TestCommand1(string Name, int Age) : ICommand;

internal class TestCommand1Validator : ICommandValidator<TestCommand1>
{
    public Task<IValidationResult> ValidateCommand(TestCommand1 command, CancellationToken cancellationToken)
    {
        BasicValidationResult result = new();
        if (string.IsNullOrEmpty(command.Name))
        {
            result.AddError(nameof(command.Name), "Name is required");
        }

        if (command.Name == "Multi")
        {
            result.AddError(nameof(command.Name), "Multi error 1");
            result.AddError(nameof(command.Name), "Multi error 2");
        }

        if (command.Age < 18)
        {
            result.AddError(nameof(command.Age), "Must be 18 or older");
        }

        result.IsValid = result.Errors.Count == 0;
        return Task.FromResult<IValidationResult>(result);
    }
}

internal class TestCommand1Handler : ICommandHandler<TestCommand1>
{
    public Task<CommandResult> HandleAsync(TestCommand1 command, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CommandResult(true));
    }
}