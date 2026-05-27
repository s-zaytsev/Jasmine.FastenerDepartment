namespace Jasmine.FastenerDepartment.Domain.Settings.Models.Emails;

/// <summary>
/// Change email settings model.
/// </summary>
public class ChangeEmailSettings
{
    /// <summary>
    /// Simple mail transfer protocol URL.
    /// </summary>
    public string SmtpUrl { get; set; }

    /// <summary>
    /// Simple mail transfer protocol port.
    /// </summary>
    public int SmtpPort { get; set; }

    /// <summary>
    /// User name.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Display name.
    /// </summary>
    public string DisplayName { get; set; }
}
