namespace Jasmine.FastenerDepartment.WebApi.Dtos.Recipients;

/// <summary>
/// Change recipient model.
/// </summary>
/// <param name="Name">Name.</param>
/// <param name="Email">Email.</param>
public record ChangeRecipientDto(string Name, string Email);
