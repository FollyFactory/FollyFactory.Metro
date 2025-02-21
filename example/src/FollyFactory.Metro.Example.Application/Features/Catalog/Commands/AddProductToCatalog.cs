using FollyFactory.Metro.Commands;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Commands;

public record AddProductToCatalog(Guid ProductId, string Sku, string Name, string Description) : ICommand;