using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Products;

/// <summary>
/// Product change reason.
/// </summary>
/// <param name="Code">Code.</param>
/// <param name="Description">Description.</param>
public record ProductChangeReasonDto(
    ProductChangeReasonCode Code,
    string Description);
