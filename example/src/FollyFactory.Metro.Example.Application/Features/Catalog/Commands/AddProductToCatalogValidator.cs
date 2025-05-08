using FollyFactory.Metro.Commands;
using FollyFactory.Metro.Validation;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Commands;

public class AddProductToCatalogValidator : ICommandValidator<AddProductToCatalog>
{
    public Task<IValidationResult> ValidateCommand(AddProductToCatalog command, CancellationToken cancellationToken)
    {
        var errors = new List<ValidationError>();

        if(string.IsNullOrEmpty(command.Sku))
            errors.Add(ValidationError.Create(nameof(command.Sku), "SKU is required"));
        
        if (string.IsNullOrEmpty(command.Name))
            errors.Add(ValidationError.Create(nameof(command.Name), "Name is required"));

        return Task.FromResult<IValidationResult>(errors.Count != 0 
            ? BasicValidationResult.Invalid(errors) 
            : BasicValidationResult.Valid());
    }
}