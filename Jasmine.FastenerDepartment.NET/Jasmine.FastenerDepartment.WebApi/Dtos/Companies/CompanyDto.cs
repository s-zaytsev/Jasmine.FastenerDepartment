namespace Jasmine.FastenerDepartment.WebApi.Dtos.Companies;

/// <summary>
/// Company.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Title">Title.</param>
/// <param name="Type">Type.</param>
public record CompanyDto(
    Guid Id,
    string Title,
    string Type);
