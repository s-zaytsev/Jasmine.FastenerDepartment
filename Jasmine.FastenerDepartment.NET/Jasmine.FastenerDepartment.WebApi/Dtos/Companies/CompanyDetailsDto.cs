namespace Jasmine.FastenerDepartment.WebApi.Dtos.Companies;

/// <summary>
/// Company details.
/// </summary>
/// <param name="Id">Identifier.</param>
/// <param name="Title">Title.</param>
/// <param name="FirstName">First name.</param>
/// <param name="MiddleName">Middle name.</param>
/// <param name="LastName">Last name.</param>
/// <param name="Email">Email.</param>
/// <param name="City">City.</param>
/// <param name="Street">Street.</param>
/// <param name="BuildingNumber">Building number.</param>
/// <param name="Inn">Individual identification number (INN).</param>
/// <param name="PhoneNumber">Phone number.</param>
public record CompanyDetailsDto(
    Guid Id,
    string Title,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string City,
    string Street,
    string BuildingNumber,
    string Inn,
    string PhoneNumber);
