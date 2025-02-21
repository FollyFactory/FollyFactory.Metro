using FollyFactory.Metro.Commands;
using FollyFactory.Metro.Validation;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Commands;

public class AddProductToCatalogValidator : ICommandValidator<AddProductToCatalog>
{
    public Task<IValidationResult> ValidateCommand(AddProductToCatalog command)
    {
        throw new NotImplementedException();
    }
}