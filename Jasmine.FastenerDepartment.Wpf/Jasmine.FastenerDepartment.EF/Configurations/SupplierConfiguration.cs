using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasIndex(x => x.Id);

        builder
            .OwnsOne(x => x.Name, o =>
            {
                o.Property(x => x.Value).HasColumnName("Name").HasColumnType("varchar(500)").IsRequired();
                o.HasIndex(x => x.Value).IsUnique();
            });

        builder.Property(x => x.Address).HasColumnType("varchar(500)");

        builder
            .HasMany(x => x.Products)
            .WithOne(x => x.Supplier)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
