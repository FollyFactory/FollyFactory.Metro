using FollyFactory.Metro.Validation;

namespace FollyFactory.Metro.Queries;

public class BasicValidationQueryHandlerDecorator<TQuery, TResult> :IQueryHandler<TQuery, TResult> 
    where TQuery : IQuery<TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _decoratedQueryHandler;
    private readonly IQueryValidator<TQuery>? _validator;

    public BasicValidationQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decoratedQueryHandler, IQueryValidator<TQuery>? validator)
    {
        _decoratedQueryHandler = decoratedQueryHandler;
        _validator = validator;
    }
    
    public async Task<QueryResult<TResult?>> HandleAsync(TQuery query, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await _decoratedQueryHandler.HandleAsync(query, cancellationToken);
        }
        
        IValidationResult validationResult = await _validator.ValidateQuery(query, cancellationToken);
        if (validationResult.IsValid)
        {
            return await _decoratedQueryHandler.HandleAsync(query, cancellationToken);
        }

        Dictionary<string, string[]> validationErrors = new();
        foreach (ValidationError error in validationResult.Errors)
        {
            validationErrors.TryAdd(error.PropertyName, error.ErrorMessages.ToArray());
        }
        
        var queryResult = QueryResult<TResult?>.Failure(validationErrors);
        return queryResult;
    }
}