using Jasmine.FastenerDepartment.Domain.Suppliers.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Suppliers controller.
/// </summary>
[ApiController]
[Route("suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly ISuppliersService _suppliersService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    /// <param name="suppliersService">Supplier service.</param>
    /// <param name="mapper">Mapper.</param>
    public SuppliersController(
        ISuppliersService suppliersService,
        WebApiMapper mapper)
    {
        _suppliersService = suppliersService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns the list of suppliers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of suppliers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SupplierDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var suppliers = await _suppliersService.GetAllAsync(cancellationToken);
        var dtos = suppliers.Select(_mapper.Map);

        return Ok(dtos);
    }

    /// <summary>
    /// Returns the list of extended suppliers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of extended suppliers.</returns>
    [HttpGet("extended")]
    [ProducesResponseType(typeof(IEnumerable<ExtendedSupplierDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllExtendedSuppliersAsync(CancellationToken cancellationToken)
    {
        var suppliers = await _suppliersService.GetAllExtendedSuppliersAsync(cancellationToken);
        var dtos = suppliers.Select(_mapper.Map);

        return Ok(dtos);
    }

    /// <summary>
    /// Returns the supplier.
    /// </summary>
    /// <param name="id">Supplier identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Supplier.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SupplierDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var supplier = await _suppliersService.GetAsync(id, cancellationToken);
        var dto = _mapper.Map(supplier);

        return Ok(dto);
    }

    /// <summary>
    /// Creates a new supplier.
    /// </summary>
    /// <param name="dto">Change supplier model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Supplier identifier.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] ChangeSupplierModelDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        var supplier = await _suppliersService.AddAsync(model, cancellationToken);

        return Ok(supplier.Id);
    }

    /// <summary>
    /// Updates the supplier.
    /// </summary>
    /// <param name="id">Supplier identifier.</param>
    /// <param name="dto">Change supplier model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id, [FromBody] ChangeSupplierModelDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(dto);
        await _suppliersService.UpdateAsync(id, model, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes the supplier.
    /// </summary>
    /// <param name="id">Supplier identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _suppliersService.RemoveSupplierAsync(id, cancellationToken);
        return NoContent();
    }
}
