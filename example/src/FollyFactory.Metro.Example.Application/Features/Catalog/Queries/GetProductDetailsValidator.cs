using FollyFactory.Metro.Queries;
using FollyFactory.Metro.Validation;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Queries;

public class GetProductDetailsValidator : IQueryValidator<GetProductDetails>
{
    public Task<IValidationResult> ValidateQuery(GetProductDetails query, CancellationToken cancellationToken = default)
    {
        var errors = new List<ValidationError>();

        if(query.ProductId == Guid.Empty)
            errors.Add(ValidationError.Create(nameof(query.ProductId), "ID is required"));

        return Task.FromResult<IValidationResult>(errors.Count != 0 
            ? BasicValidationResult.Invalid(errors) 
            : BasicValidationResult.Valid());
    }
}