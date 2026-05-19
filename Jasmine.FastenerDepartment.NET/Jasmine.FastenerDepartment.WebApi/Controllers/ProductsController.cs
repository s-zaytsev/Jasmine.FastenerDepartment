using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Products;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Products controller.
/// </summary>
[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="productsService">Product service.</param>
    /// <param name="mapper">Mapper.</param>
    public ProductsController(
        IProductsService productsService,
        WebApiMapper mapper)
    {
        _productsService = productsService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns product page filters.
    /// </summary>
    /// <param name="queryDto">Products query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product page filters.</returns>
    [HttpGet("page-filters")]
    [ProducesResponseType(typeof(ProductFiltersDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageFiltersAsync(
        [FromQuery] ProductsQueryDto queryDto, CancellationToken cancellationToken)
    {
        var query = _mapper.Map(queryDto);

        var productFilters = await _productsService.GetPageFiltersAsync(query, cancellationToken);
        var dto = _mapper.Map(productFilters);

        return Ok(dto);
    }

    /// <summary>
    /// Returns the products page.
    /// </summary>
    /// <param name="queryDto">Products query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Products page.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Page<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPageAsync([FromQuery] ProductsQueryDto queryDto, CancellationToken cancellationToken)
    {
        var query = _mapper.Map(queryDto);

        var page = await _productsService.GetPageAsync(query, cancellationToken);
        var pageDto = _mapper.Map(page);

        return Ok(pageDto);
    }

    /// <summary>
    /// Returns the product.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var product = await _productsService.GetAsync(id, cancellationToken);
        var dto = _mapper.Map(product);

        return Ok(dto);
    }

    /// <summary>
    /// Returns the last product number.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The last product number.</returns>
    [HttpGet("last-number")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLastIdAsync(CancellationToken cancellationToken)
    {
        var number = await _productsService.GetLastProductNumberAsync(cancellationToken);
        return Ok(number);
    }

    /// <summary>
    /// Creates the product.
    /// </summary>
    /// <param name="modelDto">Change product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product identifier.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] ChangeProductModelDto modelDto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(modelDto);
        var product = await _productsService.AddAsync(model, cancellationToken);

        return Ok(product.Id);
    }

    /// <summary>
    /// Changes the product.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <param name="modelDto">Change product model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeAsync(
        [FromRoute] Guid id, [FromBody] ChangeProductModelDto modelDto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(modelDto);
        await _productsService.UpdateAsync(id, model, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Changes the product print status.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("{id}/print")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangePrintStatusAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _productsService.ChangePrintStatusAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Changes the product order status.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPost("{id}/order")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangeOrderStatusAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _productsService.ChangeOrderStatusAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes the product.
    /// </summary>
    /// <param name="id">Product identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _productsService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
