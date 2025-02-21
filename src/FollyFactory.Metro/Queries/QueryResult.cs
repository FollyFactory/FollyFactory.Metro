namespace FollyFactory.Metro.Queries;

public record QueryResult<TResult>(bool IsSuccessfull, bool NotFound, TResult? Result);