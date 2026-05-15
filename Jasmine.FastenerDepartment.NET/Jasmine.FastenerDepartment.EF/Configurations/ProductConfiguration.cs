using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MeasurementUnitCode).HasColumnName("MeasurementUnitId");
        builder.Property(x => x.PriceTagCode).HasColumnName("PriceTagId");

        builder.OwnsOne(x => x.Number, o =>
        {
            o.Property(x => x.Value).HasColumnName("Number").IsRequired();
            o.HasIndex(x => x.Value).IsUnique();
        });

        builder.OwnsOne(x => x.Name, o =>
        {
            o.Property(x => x.Value).HasColumnName("Name").HasColumnType("varchar(500)").IsRequired();
            o.HasIndex(x => x.Value);
        });

        builder.OwnsOne(
            x => x.Price,
            o => o.Property(x => x.Value).HasColumnName("Price").HasColumnType("decimal(18,2)").IsRequired());

        builder
            .HasOne(x => x.PriceTag)
            .WithMany()
            .HasForeignKey(x => x.PriceTagCode)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.MeasurementUnit)
            .WithMany()
            .HasForeignKey(x => x.MeasurementUnitCode)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Type)
            .WithMany()
            .HasForeignKey(x => x.TypeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(x => x.HistoryEntries)
            .WithOne()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Suppliers)
            .WithMany()
            .UsingEntity<SupplierProduct>();
    }
}
