using FollyFactory.Metro.Example.Application.Features.Catalog.Views;
using FollyFactory.Metro.Queries;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Queries;

public class SearchProductsHandler: IQueryHandler<SearchProducts, PagedViewResult<ProductSearchView>>
{
    public Task<QueryResult<PagedViewResult<ProductSearchView>?>> Handle(SearchProducts query, CancellationToken cancellationToken)
    {

        ProductSearchView[] products =
        [
            new() { ProductId = Guid.NewGuid(), Name = "Test Product 1", Sku = "TP1" },
            new() { ProductId = Guid.NewGuid(), Name = "Test Product 2", Sku = "TP2" },
        ];
        var paged = new PagedViewResult<ProductSearchView>(products, 2, query.Page, query.PageSize);

        return Task.FromResult(new QueryResult<PagedViewResult<ProductSearchView>?>(true, true, paged));
    }
}