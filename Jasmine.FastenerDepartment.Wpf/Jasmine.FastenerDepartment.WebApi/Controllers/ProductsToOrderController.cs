using Jasmine.FastenerDepartment.Domain.ProductsToOrder.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.ProductsToOrder;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Products to order controller.
/// </summary>
[ApiController]
[Route("products-to-order")]
public class ProductsToOrderController : ControllerBase
{
    private readonly IProductsToOrderService _productsToOrderService;

    /// <summary>
    /// Creates the controller.
    /// </summary>
    /// <param name="productsToOrderService">Products to order service.</param>
    public ProductsToOrderController(IProductsToOrderService productsToOrderService)
    {
        _productsToOrderService = productsToOrderService;
    }

    /// <summary>
    /// Returns the list of products to order.
    /// </summary>
    /// <param name="queryDto">Products to order query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products to order.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductToOrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] ProductsToOrderQueryDto queryDto, CancellationToken cancellationToken)
    {
        var query = WebApiMapper.Map(queryDto);
        var products = await _productsToOrderService.GetAllAsync(query, cancellationToken);
        var dtos = products.Select(WebApiMapper.Map);

        return Ok(dtos);
    }
}
