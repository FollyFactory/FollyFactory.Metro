using FollyFactory.Metro.Example.Application.Features.Catalog.Queries;
using FollyFactory.Metro.Example.Application.Features.Catalog.Views;
using FollyFactory.Metro.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FollyFactory.Metro.Example.Api.Features.Catalog.ProductDetails;

[Route("catalog/products")]
[ApiController]
[ApiExplorerSettings(GroupName = "Catalog Products")]
public class ProductDetailsController : ControllerBase
{
    internal const string RouteName = "GetProductById";

    private readonly IQueryProcessor _queryProcessor;

    public ProductDetailsController(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet("{productId:guid}", Name = RouteName)]
    [ProducesResponseType(typeof(ProductDetailsView), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(Guid productId, CancellationToken cancellationToken)
    {
        var query = new GetProductDetails(productId);
        var queryResult = await _queryProcessor.Process(query, cancellationToken);

        if (queryResult.NotFound)
        {
            return NotFound();
        }

        return Ok(queryResult.Result);
    }
}
