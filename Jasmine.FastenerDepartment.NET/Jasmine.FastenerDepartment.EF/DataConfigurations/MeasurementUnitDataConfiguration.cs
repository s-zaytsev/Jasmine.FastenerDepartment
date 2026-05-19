using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jasmine.FastenerDepartment.EF.DataConfigurations;

class MeasurementUnitDataConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.HasData(
            Create(MeasurementUnitCode.Pieces, new("pcs", "шт"), new("Pieces", "Штуки")),
            Create(MeasurementUnitCode.Meters, new("m", "м"), new("Meters", "Метры")),
            Create(MeasurementUnitCode.Kilograms, new("kg", "кг"), new("Kilograms", "Килограммы")),
            Create(MeasurementUnitCode.Packages, new("pack", "уп"), new("Packages", "Упаковки")),
            Create(MeasurementUnitCode.Sets, new("sets", "компл"), new("Sets", "Комплекты")),
            Create(MeasurementUnitCode.Lists, new("l", "л"), new("Lists", "Листы")));
    }

    private MeasurementUnit Create(
        MeasurementUnitCode id,
        LocalizedString shortName,
        LocalizedString name)
    {
        return new MeasurementUnit(id, shortName, name);
    }
}
