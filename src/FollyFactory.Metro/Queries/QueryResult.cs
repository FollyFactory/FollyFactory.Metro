namespace FollyFactory.Metro.Queries;

public record QueryResult<TResult>(bool IsSuccessful, TResult? Result, Dictionary<string, string[]>? ValidationErrors = null)
{
    public bool NotFound { get; private init; } = false;
    
    public static QueryResult<TResult> Success(TResult result) => new(true, result);
    
    public static QueryResult<TResult> CreateNotFound() => new(true, default)
    {
        NotFound = true
    };
    
    public static QueryResult<TResult> Failure(Dictionary<string, string[]>? validationErrors = null) 
        => new(false, default)
        {
            ValidationErrors = validationErrors
        };
}