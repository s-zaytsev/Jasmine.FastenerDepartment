using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.Configurations;

class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable("ProductTypes");

        builder.HasKey(x => x.Id);

        builder
           .OwnsOne(x => x.Name, o =>
           {
               o.Property(x => x.Value).HasColumnName("Name").HasColumnType("varchar(500)").IsRequired();
               o.HasIndex(x => x.Value).IsUnique();
           });
    }
}
