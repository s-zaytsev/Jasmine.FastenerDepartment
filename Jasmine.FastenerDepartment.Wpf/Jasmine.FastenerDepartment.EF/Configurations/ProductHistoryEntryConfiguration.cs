using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class ProductHistoryEntryConfiguration : IEntityTypeConfiguration<ProductHistoryEntry>
{
    public void Configure(EntityTypeBuilder<ProductHistoryEntry> builder)
    {
        builder.ToTable("ProductHistoryEntries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ChangeReasonCode).HasColumnName("ChangeReasonId");

        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.HistoryEntries)
            .HasForeignKey(x => x.ProductId);

        builder
            .HasOne(x => x.Reason)
            .WithMany()
            .HasForeignKey(x => x.ChangeReasonCode)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
