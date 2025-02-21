namespace FollyFactory.Metro.Queries;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<QueryResult<TResult?>> Handle(TQuery query, CancellationToken cancellationToken);
}