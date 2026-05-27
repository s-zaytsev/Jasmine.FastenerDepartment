namespace Jasmine.FastenerDepartment.WebApi.Dtos.SettingsEntries;

/// <summary>
/// Company settings model.
/// </summary>
/// <param name="Title">Title.</param>
/// <param name="SubTitle">Sub-title.</param>
public record CompanySettingsDto(
    string Title,
    string SubTitle);
