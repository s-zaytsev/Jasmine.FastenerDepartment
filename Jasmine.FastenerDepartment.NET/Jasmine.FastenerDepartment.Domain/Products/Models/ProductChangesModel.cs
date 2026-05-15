namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Product changes model.
/// </summary>
/// <param name="Code">Product change reason code.</param>
/// <param name="OldValue">Old value.</param>
/// <param name="NewValue">New value.</param>
public record struct ProductChangesModel(
    ProductChangeReasonCode Code,
    object OldValue,
    object NewValue);
