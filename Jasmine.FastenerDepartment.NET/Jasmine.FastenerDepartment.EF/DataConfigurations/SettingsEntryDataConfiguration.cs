using Jasmine.FastenerDepartment.Domain.Settings.Models;
using Jasmine.FastenerDepartment.Domain.Settings.Models.Keys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

internal class SettingsEntryDataConfiguration : IEntityTypeConfiguration<SettingsEntry>
{
    public void Configure(EntityTypeBuilder<SettingsEntry> builder)
    {
        builder.HasData(
            Create(CompanySettingsKeys.TITLE_KEY, "", "Company's title"),
            Create(CompanySettingsKeys.SUBTITLE_KEY, "", "Company sub-title"),

            Create(EmailSettingsKeys.SMTP_URL_KEY, "", "Simple mail transfer protocol URL"),
            Create(EmailSettingsKeys.SMTP_PORT_KEY, "", "Simple mail transfer protocol port"),
            Create(EmailSettingsKeys.USER_NAME_KEY, "", "User name of an account"),
            Create(EmailSettingsKeys.PASSWORD_KEY, "", "Password of account for external services"),
            Create(EmailSettingsKeys.DISPLAY_NAME_KEY, "", "Name which will be shown instead of email address"));
    }

    private SettingsEntry Create(string id, string value, string description = null)
    {
        return new SettingsEntry(id, value, description);
    }
}
