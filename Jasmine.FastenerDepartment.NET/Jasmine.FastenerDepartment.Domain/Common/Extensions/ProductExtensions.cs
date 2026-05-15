using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Extensions;

/// <summary>
/// Product extensions.
/// </summary>
public static class ProductExtensions
{
    /// <summary>
    /// Returns changes.
    /// </summary>
    /// <param name="product">Main product.</param>
    /// <param name="comparedProduct">Compared product.</param>
    /// <returns>List of changes.</returns>
    public static ICollection<ProductChangesModel> GetChanges(this Product product, Product comparedProduct)
    {
        var changes = new List<ProductChangesModel>();

        if (product.Name != comparedProduct.Name)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedName, product.Name, comparedProduct.Name));
        }

        if (product.Price != comparedProduct.Price)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedPrice, product.Price, comparedProduct.Price));
        }

        if (product.PriceTagCode != comparedProduct.PriceTagCode)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedPriceTagCode, (int)product.PriceTagCode, (int)comparedProduct.PriceTagCode));
        }

        if (product.MeasurementUnitCode != comparedProduct.MeasurementUnitCode)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedMeasurementUnitCode, (int)product.MeasurementUnitCode, (int)comparedProduct.MeasurementUnitCode));
        }

        if (product.IsNeededToOrder != comparedProduct.IsNeededToOrder)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedOrderStatus, product.IsNeededToOrder, comparedProduct.IsNeededToOrder));
        }

        if (product.IsNeededToPrint != comparedProduct.IsNeededToPrint)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedPrintStatus, product.IsNeededToPrint, comparedProduct.IsNeededToPrint));
        }

        return changes;
    }

    /// <summary>
    /// Returns changes.
    /// </summary>
    /// <param name="product">Main product.</param>
    /// <param name="changeModel">Product's change model.</param>
    /// <returns>List of changes.</returns>
    public static ICollection<ProductChangesModel> GetChanges(this Product product, ChangeProductModel changeModel)
    {
        var changes = new List<ProductChangesModel>();

        if (product.Name.Value != changeModel.Name)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedName, product.Name, changeModel.Name));
        }

        if (product.Price.Value != changeModel.Price)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedPrice, product.Price, changeModel.Price));
        }

        if (product.PriceTagCode != changeModel.PriceTagCode)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedPriceTagCode, (int)product.PriceTagCode, (int)changeModel.PriceTagCode));
        }

        if (product.MeasurementUnitCode != changeModel.MeasurementUnitCode)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedMeasurementUnitCode, (int)product.MeasurementUnitCode, (int)changeModel.MeasurementUnitCode));
        }

        if (product.IsNeededToOrder != changeModel.IsNeededToOrder)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedOrderStatus, product.IsNeededToOrder, changeModel.IsNeededToOrder));
        }

        if (product.IsNeededToPrint != changeModel.IsNeededToPrint)
        {
            changes.Add(new(ProductChangeReasonCode.ChangedPrintStatus, product.IsNeededToPrint, changeModel.IsNeededToPrint));
        }

        return changes;
    }
}
