using Jasmine.FastenerDepartment.Domain.Companies.Services;
using Jasmine.FastenerDepartment.WebApi.Dtos.Companies;
using Microsoft.AspNetCore.Mvc;

namespace Jasmine.FastenerDepartment.WebApi.Controllers;

/// <summary>
/// Companies controller.
/// </summary>
[ApiController]
[Route("companies")]
public class CompaniesController : ControllerBase
{
    private readonly ICompaniesService _companiesService;
    private readonly WebApiMapper _mapper;

    /// <summary>
    /// Creates controller.
    /// </summary>
    public CompaniesController(
        ICompaniesService companiesService,
        WebApiMapper mapper)
    {
        _companiesService = companiesService;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns companies list.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Companies list.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        var companies = await _companiesService.GetCompaniesAsync(cancellationToken);
        var dtos = companies.Select(_mapper.Map);

        return Ok(dtos);
    }

    /// <summary>
    /// Returns a company by identifier.
    /// </summary>
    /// <param name="id">Company identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Company.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CompanyDetailsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompanyAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var company = await _companiesService.GetCompanyAsync(id, cancellationToken);
        var dto = _mapper.MapDetails(company);

        return Ok(dto);
    }

    /// <summary>
    /// Create a company.
    /// </summary>
    /// <param name="modelDto">Change company model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Company identifier.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCompanyAsync(
        [FromBody] ChangeCompanyDto modelDto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(modelDto);
        var company = await _companiesService.CreateCompanyAsync(model, cancellationToken);

        return Ok(company.Id);
    }

    /// <summary>
    /// Updates a company.
    /// </summary>
    /// <param name="id">Company identifier.</param>
    /// <param name="modelDto">Change company model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateCompanyAsync(
        [FromRoute] Guid id, [FromBody] ChangeCompanyDto modelDto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map(modelDto);
        await _companiesService.UpdateCompanyAsync(id, model, cancellationToken);

        return NoContent();
    }
}
