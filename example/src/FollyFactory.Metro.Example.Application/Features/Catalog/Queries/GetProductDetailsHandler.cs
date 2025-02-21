using FollyFactory.Metro.Example.Application.Features.Catalog.Views;
using FollyFactory.Metro.Queries;

namespace FollyFactory.Metro.Example.Application.Features.Catalog.Queries;

public record GetProductDetailsHandler : IQueryHandler<GetProductDetails, ProductDetailsView>
{
    public Task<QueryResult<ProductDetailsView?>> Handle(GetProductDetails query, CancellationToken cancellationToken)
    {
        return Task.FromResult(new QueryResult<ProductDetailsView?>(true, false, new ProductDetailsView
        {
            ProductId = query.ProductId,
            Sku = "SKU-123",
            Name = "Product Name",
            Description = "Product Description"
        }));
    }
}