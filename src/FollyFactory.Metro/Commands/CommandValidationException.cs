using FollyFactory.Metro.Validation;

namespace FollyFactory.Metro.Commands;

public class CommandValidationException : ApplicationValidationException
{
    public CommandValidationException(string message) : base(message)
    {
    }

    public CommandValidationException(IValidationResult results) : base(results)
    {
    }
}