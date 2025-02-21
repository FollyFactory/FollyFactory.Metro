using FollyFactory.Metro.Example.Application.Features.Catalog.Queries;
using FollyFactory.Metro.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FollyFactory.Metro.Example.Api.Features.Catalog.ProductList;

[Route("catalog/products")]
[ApiController]
[ApiExplorerSettings(GroupName = "Catalog Products")]
public class ProductListController : ControllerBase
{
    private readonly IQueryProcessor _queryProcessor;

    public ProductListController(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListProducts([FromQuery] string? terms, CancellationToken cancellationToken)
    {
        var query = new SearchProducts(terms);
        var queryResult = await _queryProcessor.Process(query, cancellationToken);
        
        return Ok(queryResult.Result);
    }
}
