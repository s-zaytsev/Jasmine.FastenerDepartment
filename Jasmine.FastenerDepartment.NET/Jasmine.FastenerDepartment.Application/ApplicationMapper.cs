using Jasmine.FastenerDepartment.Application.Models.Synchronization;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;

namespace Jasmine.FastenerDepartment.Application;

internal static class ApplicationMapper
{
    internal static SynchronizationProduct Map(Product product)
    {
        return new SynchronizationProduct
        {
            Id = product.Id,
            CreatedDate = product.CreatedDate,
            ModifiedDate = product.ModifiedDate,
            Number = product.Number.Value,
            Name = product.Name.Value,
            Price = product.Price.Value,
            IsDeleted = product.IsDeleted,
            IsNeededToOrder = product.IsNeededToOrder,
            IsNeededToPrint = product.IsNeededToPrint,
            MeasurementUnitCode = product.MeasurementUnitCode,
            PriceTagCode = product.PriceTagCode,
        };
    }

    internal static Supplier Map(ChangeSupplierModel model)
    {
        return new Supplier(model.Name, model.Address);
    }
}
