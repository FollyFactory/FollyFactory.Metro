using FollyFactory.Metro.Queries;
using FollyFactory.Metro.Validation;
using Shouldly;

#pragma warning disable CS8604 // Possible null reference argument.

namespace FollyFactory.Metro.UnitTests.Queries;

public class BasicValidationQueryHandlerDecoratorTests
{
    [Fact]
    public async Task Validate_WhenQueryIsValid_ReturnsSuccessful()
    {
        var query = new TestQuery1("Bill", 21);
        var handler = new TestQuery1Handler();
        var validator = new TestQuery1Validator();
        var decorator = new BasicValidationQueryHandlerDecorator<TestQuery1, string>(handler, validator);

        var result = await decorator.HandleAsync(query, CancellationToken.None);

        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Validate_WhenQueryNameIsNotValid_ReturnsFailure(string? name)
    {
        var query = new TestQuery1(name, 21);
        var handler = new TestQuery1Handler();
        var validator = new TestQuery1Validator();
        var decorator = new BasicValidationQueryHandlerDecorator<TestQuery1, string>(handler, validator);

        var result = await decorator.HandleAsync(query, CancellationToken.None);

        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeFalse();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Validate_WhenQueryNameIsNullOrEmpty_CorrectErrors(string? name)
    {
        var query = new TestQuery1(name, 21);
        var handler = new TestQuery1Handler();
        var validator = new TestQuery1Validator();
        var decorator = new BasicValidationQueryHandlerDecorator<TestQuery1, string>(handler, validator);

        var result = await decorator.HandleAsync(query, CancellationToken.None);
    
        result.ShouldNotBeNull();
        result.ValidationErrors.ShouldNotBeNull();
        result.ValidationErrors.Count.ShouldBe(1);
        result.ValidationErrors.ShouldContainKey("Name");
        result.ValidationErrors["Name"].ShouldNotBeNull();
        result.ValidationErrors["Name"].Length.ShouldBe(1);
        result.ValidationErrors["Name"][0].ShouldBe("Name is required");
    }
    
    [Fact]
    public async Task Validate_WhenQueryPropertyIsMultiInvalid_CorrectErrors()
    {
        var query = new TestQuery1("Multi", 21);
        var handler = new TestQuery1Handler();
        var validator = new TestQuery1Validator();
        var decorator = new BasicValidationQueryHandlerDecorator<TestQuery1, string>(handler, validator);

        var result = await decorator.HandleAsync(query, CancellationToken.None);
    
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
    public async Task Validate_WhenQueryMultiplePropertiesInvalid_CorrectErrors()
    {
        var query = new TestQuery1(string.Empty, 16);
        var handler = new TestQuery1Handler();
        var validator = new TestQuery1Validator();
        var decorator = new BasicValidationQueryHandlerDecorator<TestQuery1, string>(handler, validator);

        var result = await decorator.HandleAsync(query, CancellationToken.None);
    
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

    private record TestQuery1(string Name, int Age) : IQuery<string>;

    private class TestQuery1Validator : IQueryValidator<TestQuery1>
    {
        public Task<IValidationResult> ValidateQuery(TestQuery1 query, CancellationToken cancellationToken = default)
        {
            BasicValidationResult result = new();
            if (string.IsNullOrEmpty(query.Name))
            {
                result.AddError(nameof(query.Name), "Name is required");
            }

            if (query.Name == "Multi")
            {
                result.AddError(nameof(query.Name), "Multi error 1");
                result.AddError(nameof(query.Name), "Multi error 2");
            }

            if (query.Age < 18)
            {
                result.AddError(nameof(query.Age), "Must be 18 or older");
            }

            result.IsValid = result.Errors.Count == 0;
            return Task.FromResult<IValidationResult>(result);
        }
    }

    private class TestQuery1Handler : IQueryHandler<TestQuery1, string>
    {
        public Task<QueryResult<string?>> HandleAsync(TestQuery1 query, CancellationToken cancellationToken)
        {
            return Task.FromResult(new QueryResult<string?>(true, "Bingo"));
        }
    }
}

