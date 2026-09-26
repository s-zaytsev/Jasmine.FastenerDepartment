namespace Jasmine.FastenerDepartment.WebApi.Dtos.Recipients;

/// <summary>
/// Recipient.
/// </summary>
/// <param name="Id">Recipient identifier.</param>
/// <param name="Name">Name.</param>
/// <param name="Email">Email.</param>
public record RecipientDto(Guid Id, string Name, string Email);
