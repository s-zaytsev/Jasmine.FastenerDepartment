using Jasmine.FastenerDepartment.Application.Services.Print;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Print controller.
/// </summary>
[ApiController]
[Route("print")]
public class PrintController : ControllerBase
{
    private readonly IPrintService _printService;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="printService">Print service.</param>
    public PrintController(IPrintService printService)
    {
        _printService = printService;
    }

    /// <summary>
    /// Returns the list of products to print.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products to print.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Page<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsToPrintAsync(CancellationToken cancellationToken)
    {
        var products = await _printService.GetProductsToPrintAsync(cancellationToken);
        var dtos = products.Select(WebApiMapper.Map);

        return Ok(dtos);
    }

    /// <summary>
    /// Removes the product from the print list.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteFromListAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _printService.RemoveProductFromPrintListAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Removes the products from the print list.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAllFromListAsync(CancellationToken cancellationToken)
    {
        await _printService.RemoveAllProductsFromPrintListAsync(cancellationToken);
        return NoContent();
    }
}
