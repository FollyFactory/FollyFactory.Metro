using FollyFactory.Metro.Queries;

namespace FollyFactory.Metro.DependencyInjection.Microsoft;

public class MicrosoftQueryProcessor : IQueryProcessor
{
    private readonly IServiceProvider _serviceProvider;

    public MicrosoftQueryProcessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<QueryResult<TResult?>> Process<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var wrapperType = typeof(QueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

        var handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
        {
            throw new Exception($"Handler for {query.GetType().Name} not found");
        }

        if (Activator.CreateInstance(wrapperType, handler) is QueryHandler<TResult> wrappedHandler)
        {
            return await wrappedHandler.Handle(query, cancellationToken);
        }

        throw new Exception("Handler creation error");
    }



    private abstract class QueryHandler<TResult>
    {
        public abstract Task<QueryResult<TResult?>> Handle(IQuery<TResult> query, CancellationToken cancellationToken);
    }


    private class QueryHandler<TQuery, TResult> : QueryHandler<TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _handler;

        public QueryHandler(IQueryHandler<TQuery, TResult> handler)
        {
            _handler = handler;
        }

        public override Task<QueryResult<TResult?>> Handle(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            return _handler.Handle((TQuery)query, cancellationToken);
        }
    }
}