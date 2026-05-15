using Jasmine.FastenerDepartment.Domain.ProductTypes.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.ProductTypes;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Product types controller.
/// </summary>
[ApiController]
[Route("product-types")]
public class ProductTypesController : ControllerBase
{
    private readonly IProductTypesService _productTypesService;

    /// <summary>
    /// Creates the controller.
    /// </summary>
    /// <param name="productTypesService"></param>
    public ProductTypesController(IProductTypesService productTypesService)
    {
        _productTypesService = productTypesService;
    }

    /// <summary>
    /// Returns the collection of product types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of product types.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductTypesAsync(CancellationToken cancellationToken)
    {
        var types = await _productTypesService.GetAllAsync(cancellationToken);
        var dtos = types.Select(WebApiMapper.Map);

        return Ok(dtos);
    }

    /// <summary>
    /// Returns the collection of extended product types.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of extended product types.</returns>
    [HttpGet("extended")]
    [ProducesResponseType(typeof(IEnumerable<ExtendedProductTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExtendedProductTypesAsync(CancellationToken cancellationToken)
    {
        var types = await _productTypesService.GetAllExtendedProductTypesAsync(cancellationToken);
        var dtos = types.Select(WebApiMapper.Map);

        return Ok(dtos);
    }

    /// <summary>
    /// Creates a new product type.
    /// </summary>
    /// <param name="dto">Change product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product type identifier.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateProductTypeAsync(
        ChangeProductTypeDto dto, CancellationToken cancellationToken)
    {
        var model = WebApiMapper.Map(dto);

        var type = await _productTypesService.CreateAsync(model, cancellationToken);

        return Ok(type.Id);
    }

    /// <summary>
    /// Updates the product type.
    /// </summary>
    /// <param name="id">Product type identifier.</param>
    /// <param name="dto">Change product type model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateProductTypeAsync(
       Guid id, ChangeProductTypeDto dto, CancellationToken cancellationToken)
    {
        var model = WebApiMapper.Map(dto);

        await _productTypesService.UpdateAsync(id, model, cancellationToken);

        return NoContent();
    }
}
