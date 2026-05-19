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
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates the controller.
    /// </summary>
    /// <param name="productsToOrderService">Products to order service.</param>
    /// <param name="mapper">Mapper.</param>
    public ProductsToOrderController(
        IProductsToOrderService productsToOrderService,
        WebApiMapper mapper)
    {
        _productsToOrderService = productsToOrderService;
        _mapper = mapper;
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
        var query = _mapper.Map(queryDto);
        var products = await _productsToOrderService.GetAllAsync(query, cancellationToken);
        var dtos = products.Select(_mapper.Map);

        return Ok(dtos);
    }
}
