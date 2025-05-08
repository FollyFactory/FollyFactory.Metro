using FollyFactory.Metro.Validation;

namespace FollyFactory.Metro.Queries;

public interface IQueryValidator<in TQuery>
{
    Task<IValidationResult> ValidateQuery(TQuery command, CancellationToken cancellationToken = default);
}