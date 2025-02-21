using FollyFactory.Metro.Commands;
using FollyFactory.Metro.Example.Api.Features.Catalog.ProductDetails;
using FollyFactory.Metro.Example.Application.Features.Catalog.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FollyFactory.Metro.Example.Api.Features.Catalog.AddProduct;

[Route("catalog/products")]
[ApiController]
[ApiExplorerSettings(GroupName = "Catalog Products")]
public class AddProductController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AddProductController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AddProductRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Guid newProductId = Guid.NewGuid();
        var command = new AddProductToCatalog(newProductId, request.Sku, request.Name, request.Description);

        await _commandDispatcher.Dispatch(command, cancellationToken);

        return CreatedAtAction(ProductDetailsController.RouteName, new { id = command.ProductId }, command);
    }
}