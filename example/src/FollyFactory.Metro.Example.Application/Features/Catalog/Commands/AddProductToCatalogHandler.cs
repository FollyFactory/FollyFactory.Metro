using FollyFactory.Metro.Commands;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Commands;

public class AddProductToCatalogHandler : ICommandHandler<AddProductToCatalog>
{
    public Task<CommandResult> HandleAsync(AddProductToCatalog command, CancellationToken cancellationToken)
    {
        // do work here

        return Task.FromResult(new CommandResult(true));
    }
}