using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.Settings.Models;

namespace Jasmine.FastenerDepartment.Domain.Settings.Repositories;

/// <summary>
/// Settings entries repository.
/// </summary>
public interface ISettingsEntriesRepository : IEntitiesRepository<string, SettingsEntry>
{
    /// <summary>
    /// Returns settings entries by section name.
    /// </summary>
    /// <param name="sectionName">Section name.</param>
    /// <returns>Settings entries by section name.</returns>
    Task<IDictionary<string, SettingsEntry>> GetBySectionNameAsync(string sectionName);

    /// <summary>
    /// Changes settings entry.
    /// </summary>
    /// <param name="entry">Settings entry.</param>
    void Change(SettingsEntry entry);
}
