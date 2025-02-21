using FollyFactory.Metro.Example.Application.Features.Catalog.Views;
using FollyFactory.Metro.Queries;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Queries;

public record GetProductDetails(Guid ProductId) : IQuery<ProductDetailsView>;