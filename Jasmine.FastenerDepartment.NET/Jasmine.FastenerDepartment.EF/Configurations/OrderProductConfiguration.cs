using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.ToTable("OrderProducts");

        builder.HasKey(x => x.Id);

        builder
            .OwnsOne(
                x => x.ProductName,
                o => o.Property(x => x.Value).HasColumnName("ProductName").IsRequired());

        builder
            .OwnsOne(x => x.Ordered, o =>
            {
                o.Property(x => x.Value).HasColumnName("OrderedQuantity");
                o.Property(x => x.MeasurementUnitCode).HasColumnName("OrderedMeasurementUnitId");
                o.Property(x => x.SpecialMeasurementUnit)
                    .HasColumnName("OrderedSpecialMeasurementUnit")
                    .HasColumnType("varchar(100)");

                o.HasOne(x => x.MeasurementUnit)
                    .WithMany()
                    .HasForeignKey(x => x.MeasurementUnitCode);
            });

        builder
           .OwnsOne(x => x.Fulfilled, o =>
           {
               o.Property(x => x.Value).HasColumnName("FulfilledQuantity");
               o.Property(x => x.MeasurementUnitCode).HasColumnName("FulfilledMeasurementUnitId");
               o.Property(x => x.SpecialMeasurementUnit)
                   .HasColumnName("FulfilledSpecialMeasurementUnit")
                   .HasColumnType("varchar(100)");

               o.HasOne(x => x.MeasurementUnit)
                    .WithMany()
                    .HasForeignKey(x => x.MeasurementUnitCode);
           });

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
