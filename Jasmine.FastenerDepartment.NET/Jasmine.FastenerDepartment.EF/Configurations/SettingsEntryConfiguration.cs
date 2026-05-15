using Jasmine.FastenerDepartment.Domain.Settings.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class SettingsEntryConfiguration : IEntityTypeConfiguration<SettingsEntry>
{
    public void Configure(EntityTypeBuilder<SettingsEntry> builder)
    {
        builder.ToTable("Settings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnType("varchar(500)");
        builder.Property(x => x.Value).IsRequired();
    }
}