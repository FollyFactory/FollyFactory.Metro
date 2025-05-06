namespace FollyFactory.Metro.Commands;

public record CommandResult(bool IsSuccessful, Dictionary<string, string[]>? ValidationErrors = null)
{
    public static CommandResult Success() => new(true);

    public static CommandResult Failure(Dictionary<string, string[]>? validationErrors = null) 
        => new(false, validationErrors);
}
