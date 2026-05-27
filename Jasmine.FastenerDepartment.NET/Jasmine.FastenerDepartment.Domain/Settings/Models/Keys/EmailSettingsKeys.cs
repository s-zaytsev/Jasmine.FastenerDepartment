namespace Jasmine.FastenerDepartment.Domain.Settings.Models.Keys;

/// <summary>
/// Email settings keys.
/// </summary>
public static class EmailSettingsKeys
{
    /// <summary>
    /// Section name.
    /// </summary>
    public const string SECTION_NAME = "Emails";

    /// <summary>
    /// Simple mail transfer protocol URL key.
    /// </summary>
    public const string SMTP_URL_KEY = $"{SECTION_NAME}:SmtpUrl";

    /// <summary>
    /// Simple mail transfer protocol port key.
    /// </summary>
    public const string SMTP_PORT_KEY = $"{SECTION_NAME}:SmtpPort";

    /// <summary>
    /// User name key.
    /// </summary>
    public const string USER_NAME_KEY = $"{SECTION_NAME}:UserName";

    /// <summary>
    /// Password key.
    /// </summary>
    public const string PASSWORD_KEY = $"{SECTION_NAME}:Password";

    /// <summary>
    /// Display name key.
    /// </summary>
    public const string DISPLAY_NAME_KEY = $"{SECTION_NAME}:DisplayName";
}
