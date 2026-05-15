using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StatusCode).HasColumnName("StatusId");
        
        builder
            .HasOne(x => x.Status)
            .WithMany()
            .HasForeignKey(x => x.StatusCode)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(x => x.Products)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.HistoryEntries)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.Number).IsUnique();
    }
}
