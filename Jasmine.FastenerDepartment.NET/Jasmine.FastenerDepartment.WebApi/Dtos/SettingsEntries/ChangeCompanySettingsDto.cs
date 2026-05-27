namespace Jasmine.FastenerDepartment.WebApi.Dtos.SettingsEntries;

/// <summary>
/// Change company settings model.
/// </summary>
/// <param name="Title">Title.</param>
/// <param name="SubTitle">Sub-title.</param>
public record ChangeCompanySettingsDto(
    string Title,
    string SubTitle);
