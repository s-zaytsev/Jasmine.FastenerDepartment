using Jasmine.FastenerDepartment.Domain.Recipients.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

internal class RecipientConfiguration : IEntityTypeConfiguration<Recipient>
{
    public void Configure(EntityTypeBuilder<Recipient> builder)
    {
        builder.ToTable("Recipients");

        builder.HasIndex(x => x.Id);

        builder.OwnsOne(
            x => x.Name,
            o => o.Property(x => x.Value).HasColumnName("Title").IsRequired());

        builder.OwnsOne(
            x => x.Email,
            o => o.Property(x => x.Value).HasColumnName("Email").IsRequired());
    }
}
