using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Companies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(
            x => x.Title,
            o => o.Property(x => x.Value).HasColumnName("Title").IsRequired());

        builder.OwnsOne(
            x => x.Email,
            o => o.Property(x => x.Value).HasColumnName("Email").IsRequired());

        builder.OwnsOne(
            x => x.PhoneNumber,
            o => o.Property(x => x.Value).HasColumnName("PhoneNumber").IsRequired());

        builder.OwnsOne(
            x => x.Inn,
            o =>
            {
                o.Property(x => x.Value).HasColumnName("Inn").IsRequired();
                o.HasIndex(x => x.Value).IsUnique();
            });

        builder.Property(x => x.TypeCode).HasColumnName("TypeId");

        builder
            .HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeCode)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
