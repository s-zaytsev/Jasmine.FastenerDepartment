using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Settings.Models;

/// <summary>
/// Settings entry.
/// </summary>
public class SettingsEntry : EntityBase<string>
{
    /// <summary>
    /// Value.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; init; }

    private SettingsEntry() { }

    /// <summary>
    /// Creates the settings entry.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="value">Value.</param>
    /// <param name="description">Description.</param>
    public SettingsEntry(
        string id,
        string value,
        string description)
    {
        Id = id;
        Value = value;
        Description = description;
    }

    /// <summary>
    /// Changes value. 
    /// </summary>
    /// <param name="value">Value.</param>
    public void ChangeValue(string value)
    {
        Value = value;
    }
}