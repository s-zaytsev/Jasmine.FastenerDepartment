using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Companies.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

internal class CompanyTypeDataConfiguration : IEntityTypeConfiguration<CompanyType>
{
    public void Configure(EntityTypeBuilder<CompanyType> builder)
    {
        builder.HasData(
            Create(
                CompanyTypeCode.IndividualEntrepreneur,
                new("Individual entrepreneur", "Индивидуальный предприниматель"))
            );
    }

    private CompanyType Create(
        CompanyTypeCode id,
        LocalizedString name)
    {
        return new CompanyType(id, name);
    }
}