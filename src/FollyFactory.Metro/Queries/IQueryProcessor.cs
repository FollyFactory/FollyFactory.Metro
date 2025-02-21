namespace FollyFactory.Metro.Queries;

public interface IQueryProcessor
{
    Task<QueryResult<TResult?>> Process<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}