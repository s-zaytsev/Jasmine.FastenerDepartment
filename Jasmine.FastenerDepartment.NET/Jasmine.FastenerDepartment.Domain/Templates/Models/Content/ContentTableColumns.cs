using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

/// <summary>
/// Content table columns.
/// </summary>
public static class ContentTableColumns
{
    private readonly static IReadOnlyDictionary<Enum, LocalizedString> _productCatalogTableColumns =
        new Dictionary<Enum, LocalizedString>()
    {
        { ProductCatalogTemplateContentTableColumnCode.Number, new("Number", "Артикул") },
        { ProductCatalogTemplateContentTableColumnCode.Name, new("Name", "Наименование") },
        { ProductCatalogTemplateContentTableColumnCode.Type, new("Type", "Тип") },
        { ProductCatalogTemplateContentTableColumnCode.Price, new("Price", "Цена") },
    };

    private readonly static IReadOnlyDictionary<Enum, LocalizedString> _orderFormTableColumns =
        new Dictionary<Enum, LocalizedString>()
    {
        { OrderFormTemplateContentTableColumnCode.SupplierProductNumber, new("Number", "Артикул") },
        { OrderFormTemplateContentTableColumnCode.ProductName, new("Name", "Наименование") },
        { OrderFormTemplateContentTableColumnCode.ProductNameWithoutSize, new("Name without size", "Наименование без размера") },
        { OrderFormTemplateContentTableColumnCode.ProductSize, new("Size", "Размер") },
        { OrderFormTemplateContentTableColumnCode.Amount, new("Amount", "Количество") },
    };

    /// <summary>
    /// Content table columns.
    /// </summary>
    public readonly static IReadOnlyDictionary<TemplateTypeCode, IReadOnlyDictionary<Enum, LocalizedString>> Columns =
        new Dictionary<TemplateTypeCode, IReadOnlyDictionary<Enum, LocalizedString>>
    {
        { TemplateTypeCode.ProductCatalog, _productCatalogTableColumns },
        { TemplateTypeCode.OrderForm, _orderFormTableColumns }
    };
}
