using Jasmine.FastenerDepartment.Domain.Settings.Models;
using Jasmine.FastenerDepartment.Domain.Settings.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Repositories.SettingsEntries;

internal class SettingsEntriesRepository :
    EntitiesRepositoryBase<string, SettingsEntry>, ISettingsEntriesRepository
{
    public SettingsEntriesRepository(ApplicationDbContext context)
        : base(context)
    { }

    public async Task<IDictionary<string, SettingsEntry>> GetBySectionNameAsync(string sectionName)
    {
        return await GetQuery()
            .Where(x => x.Id.StartsWith(sectionName))
            .ToDictionaryAsync(x => x.Id, x => x);
    }

    public void Change(SettingsEntry entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Context.Update(entity);
    }
}
