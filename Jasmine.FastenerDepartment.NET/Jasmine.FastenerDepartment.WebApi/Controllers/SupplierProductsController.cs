using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Products;
using Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Supplier products controller.
/// </summary>
[ApiController]
[Route("supplier-products")]
public class SupplierProductsController : ControllerBase
{
    private readonly ISupplierProductsService _supplierProductsService;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="supplierProductsService">Supplier products service.</param>
    public SupplierProductsController(ISupplierProductsService supplierProductsService)
    {
        _supplierProductsService = supplierProductsService;
    }

    /// <summary>
    /// Returns the supplier products page.
    /// </summary>
    /// <param name="queryDto">Supplier products query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Supplier products page.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Page<SupplierProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync(
        [FromQuery] SupplierProductsQueryDto queryDto, CancellationToken cancellationToken)
    {
        var query = WebApiMapper.Map(queryDto);
        var page = await _supplierProductsService.GetPageAsync(query, cancellationToken);

        var dto = WebApiMapper.Map(page);

        return Ok(dto);
    }

    /// <summary>
    /// Changes the supplier product.
    /// </summary>
    /// <param name="id">Supplier product identifier.</param>
    /// <param name="dto">Change supplier product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeAsync(
        [FromRoute] Guid id, [FromBody] ChangeSupplierProductModelDto dto, CancellationToken cancellationToken)
    {
        var model = WebApiMapper.Map(dto);

        await _supplierProductsService.ChangeAsync(id, model, cancellationToken);

        return NoContent();
    }
}
