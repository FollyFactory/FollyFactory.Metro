using FollyFactory.Metro.Example.Application.Features.Catalog.Views;
using FollyFactory.Metro.Queries;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Queries;
public record SearchProducts(string? KeyWords, int Page = 1, int PageSize = 25) : IQuery<PagedViewResult<ProductSearchView>>;