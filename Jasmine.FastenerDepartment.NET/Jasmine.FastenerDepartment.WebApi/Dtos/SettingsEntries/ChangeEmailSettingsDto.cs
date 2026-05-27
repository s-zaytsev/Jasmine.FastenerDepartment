namespace Jasmine.FastenerDepartment.WebApi.Dtos.SettingsEntries;

/// <summary>
/// Change email settings model.
/// </summary>
/// <param name="SmtpUrl">Simple mail transfer protocol URL.</param>
/// <param name="SmtpPort">Simple mail transfer protocol port.</param>
/// <param name="UserName">User name.</param>
/// <param name="Password">Password.</param>
/// <param name="DisplayName">Display name.</param>
public record ChangeEmailSettingsDto(
    string SmtpUrl,
    int SmtpPort,
    string UserName,
    string Password,
    string DisplayName
);
