using Jasmine.FastenerDepartment.Domain.Settings.Models.Company;
using Jasmine.FastenerDepartment.Domain.Settings.Models.Emails;

namespace Jasmine.FastenerDepartment.Domain.Settings.Services;

/// <summary>
/// Settings service.
/// </summary>
public interface ISettingsEntriesService
{
    /// <summary>
    /// Returns company settings.
    /// </summary>
    /// <returns>Company settings.</returns>
    Task<CompanySettings> GetCompanySettingsAsync();

    /// <summary>
    /// Returns email settings.
    /// </summary>
    /// <returns>Email settings.</returns>
    Task<EmailSettings> GetEmailSettingsAsync();

    /// <summary>
    /// Changes company settings.
    /// </summary>
    /// <param name="settings">Change company settings model.</param>
    Task ChangeCompanySettingsAsync(ChangeCompanySettings settings);

    /// <summary>
    /// Changes email settings.
    /// </summary>
    /// <param name="settings">Change email settings model.</param>
    Task ChangeEmailSettingsAsync(ChangeEmailSettings settings);
}
