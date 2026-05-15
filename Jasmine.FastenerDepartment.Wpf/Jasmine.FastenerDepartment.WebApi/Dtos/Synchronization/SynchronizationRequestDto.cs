namespace Jasmine.FastenerDepartment.WebApi.Dtos.Synchronization;

/// <summary>
/// Synchronization request.
/// </summary>
/// <param name="LastSynchronizeUtcTime">Last synchronization UTC time.</param>
/// <param name="Products">List of products.</param>
public record SynchronizationRequestDto(
    DateTime? LastSynchronizeUtcTime,
    IEnumerable<SynchronizationProductDto> Products);
