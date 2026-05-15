using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class OrderHistoryEntryConfiguration : IEntityTypeConfiguration<OrderHistoryEntry>
{
    public void Configure(EntityTypeBuilder<OrderHistoryEntry> builder)
    {
        builder.ToTable("OrderHistoryEntries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderStatusCode).HasColumnName("OrderStatusId");

        builder
            .HasOne<OrderStatus>()
            .WithMany()
            .HasForeignKey(x => x.OrderStatusCode)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
