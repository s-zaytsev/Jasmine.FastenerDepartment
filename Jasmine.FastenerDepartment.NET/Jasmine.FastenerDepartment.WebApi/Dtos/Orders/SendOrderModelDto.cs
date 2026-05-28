using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Orders;

/// <summary>
/// Send order model.
/// </summary>
/// <param name="RecipientContact">Recipient contact.</param>
/// <param name="MessageType">Message type.</param>
public record SendOrderModelDto(string RecipientContact, MessageType MessageType);
