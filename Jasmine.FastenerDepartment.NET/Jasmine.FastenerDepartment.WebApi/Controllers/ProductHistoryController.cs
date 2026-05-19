using Jasmine.FastenerDepartment.Domain.Products.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.HistoryEntries;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Product history controller.
/// </summary>
[ApiController]
[Route("product-history")]
public class ProductHistoryController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="productsService">Products service.</param>
    /// <param name="mapper">Mapper.</param>
    public ProductHistoryController(
        IProductsService productsService,
        WebApiMapper mapper)
    {
        _productsService = productsService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns the daily history.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Daily history.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DailyHistoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsHistoryAsync(CancellationToken cancellationToken)
    {
        var history = await _productsService.GetDailyHistoryAsync(cancellationToken);
        var dtos = history
            .OrderByDescending(x => x.Date)
            .Select(x => new DailyHistoryDto(x.Date, x.HistoryEntries.Select(_mapper.Map)));

        return Ok(dtos);
    }
}
