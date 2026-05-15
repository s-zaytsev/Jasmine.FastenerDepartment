using Jasmine.FastenerDepartment.Domain.Common.Expression;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;

namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Product.
/// </summary>
public class Product : AggregateRootBase<Guid>
{
    private List<ProductHistoryEntry> _historyEntries = [];
    private List<Supplier> _suppliers = [];

    /// <summary>
    /// Number.
    /// </summary>
    public ProductNumber Number { get; private set; }

    /// <summary>
    /// Name.
    /// </summary>
    public Name Name { get; private set; }

    /// <summary>
    /// Price.
    /// </summary>
    public Price Price { get; private set; }

    /// <summary>
    /// Product type identifier.
    /// </summary>
    public Guid? TypeId { get; private set; }

    /// <summary>
    /// Has hardware size.
    /// </summary>
    public bool HasHardwareSize { get; private set; }

    /// <summary>
    /// Is hardware size enabled.
    /// </summary>
    public bool IsHardwareSizeEnabled { get; private set; }

    /// <summary>
    /// Is needed to order.
    /// </summary>
    public bool IsNeededToOrder { get; private set; }

    /// <summary>
    /// Is needed to print.
    /// </summary>
    public bool IsNeededToPrint { get; private set; }

    /// <summary>
    /// Is deleted.
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    /// Product type.
    /// </summary>
    public ProductType Type { get; private set; }

    /// <summary>
    /// Price tag code.
    /// </summary>
    public PriceTagCode PriceTagCode { get; private set; }

    /// <summary>
    /// Measurement unit code.
    /// </summary>
    public MeasurementUnitCode MeasurementUnitCode { get; private set; }

    /// <summary>
    /// Price tag.
    /// </summary>
    public PriceTag PriceTag { get; private set; }

    /// <summary>
    /// Measurement unit.
    /// </summary>
    public MeasurementUnit MeasurementUnit { get; private set; }

    /// <summary>
    /// List of history entries.
    /// </summary>
    public IReadOnlyCollection<ProductHistoryEntry> HistoryEntries => _historyEntries.AsReadOnly();

    /// <summary>
    /// List of suppliers.
    /// </summary>
    public IReadOnlyCollection<Supplier> Suppliers => _suppliers.AsReadOnly();

    /// <summary>
    /// Creates product.
    /// </summary>
    private Product() { }

    public Product(
        int number,
        string name,
        decimal price,
        PriceTagCode priceTagCode,
        MeasurementUnitCode measurementUnitCode,
        Guid? typeId = null,
        bool isHardwareSizeEnabled = default,
        bool isNeededToOrder = default,
        bool isNeededToPrint = default)
    {
        Number = new(number);
        Name = new(name);
        Price = new(price);
        PriceTagCode = priceTagCode;
        TypeId = typeId;
        MeasurementUnitCode = measurementUnitCode;
        IsHardwareSizeEnabled = isHardwareSizeEnabled;
        IsNeededToOrder = isNeededToOrder;
        IsNeededToPrint = isNeededToPrint;

        AddInitHistoryEntry();
    }

    public Product(
        Guid id,
        DateTime createdDate,
        DateTime modifiedDate,
        int number,
        string name,
        decimal price,
        PriceTagCode priceTagCode,
        MeasurementUnitCode measurementUnitCode,
        bool isDeleted)
    {
        Id = id;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        Number = new(number);
        Name = new(name);
        Price = new(price);
        PriceTagCode = priceTagCode;
        MeasurementUnitCode = measurementUnitCode;
        IsDeleted = isDeleted;

        AddInitHistoryEntry(createdDate);
    }

    public Product(
        DateTime createdDate,
        DateTime modifiedDate,
        int number,
        string name,
        decimal price,
        PriceTagCode priceTagCode,
        MeasurementUnitCode measurementUnitCode,
        bool isHardwareSizeEnabled,
        bool isNeededToOrder,
        bool isDeleted)
    {
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        Number = new(number);
        Name = new(name);
        Price = new(price);
        PriceTagCode = priceTagCode;
        MeasurementUnitCode = measurementUnitCode;
        IsHardwareSizeEnabled = isHardwareSizeEnabled;
        IsNeededToOrder = isNeededToOrder;
        IsDeleted = isDeleted;

        AddInitHistoryEntry(createdDate);
    }

    /// <summary>
    /// Changes the product number.
    /// </summary>
    /// <param name="number">New number.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangeNumber(int number, DateTime? date = null)
    {
        if (number == Number.Value)
        {
            return;
        }

        AddHistoryEntry(ProductChangeReasonCode.ChangedNumber, Number.Value, number, date);
        Number.Value = number;
    }

    /// <summary>
    /// Changes the product name.
    /// </summary>
    /// <param name="name">New name.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangeName(string name, DateTime? date = null)
    {
        name = RegularExpressions.RemoveMultiplySpaces(name);
        if (name.Equals(Name.Value))
        {
            return;
        }

        AddHistoryEntry(ProductChangeReasonCode.ChangedName, Name.Value, name, date);
        Name.Value = name;
    }

    /// <summary>
    /// Changes the product price.
    /// </summary>
    /// <param name="price">New price.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangePrice(decimal price, DateTime? date = null)
    {
        if (price == Price.Value)
        {
            return;
        }

        AddHistoryEntry(ProductChangeReasonCode.ChangedPrice, Price.Value, price, date);
        Price.Value = price;
    }

    /// <summary>
    /// Changes price tag code.
    /// </summary>
    /// <param name="code">Price tag code.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangePriceTagCode(PriceTagCode code, DateTime? date = null)
    {
        if (code == PriceTagCode)
        {
            return;
        }

        AddHistoryEntry(ProductChangeReasonCode.ChangedPriceTagCode, (int)PriceTagCode, (int)code, date);
        PriceTagCode = code;
    }

    /// <summary>
    /// Changes product type.
    /// </summary>
    /// <param name="typeId">Product type.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangeType(Guid? typeId, DateTime? date = null)
    {
        if (typeId == TypeId)
        {
            return;
        }

        AddHistoryEntry(ProductChangeReasonCode.ChangedType, TypeId, typeId, date);
        TypeId = typeId;
    }

    /// <summary>
    /// Changes measurement unit code.
    /// </summary>
    /// <param name="measurementUnitCode">Measurement unit code.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangeMeasurementUnitCode(MeasurementUnitCode measurementUnitCode, DateTime? date = null)
    {
        if (measurementUnitCode == MeasurementUnitCode)
        {
            return;
        }

        AddHistoryEntry(ProductChangeReasonCode.ChangedMeasurementUnitCode, MeasurementUnitCode, measurementUnitCode);
        MeasurementUnitCode = measurementUnitCode;
    }

    /// <summary>
    /// Changes hardware size status.
    /// </summary>
    /// <param name="isHardwareSizeEnabled">Is hardware size enabled?</param>
    public void ChangeHardwareSizeStatus(bool isHardwareSizeEnabled)
    {
        IsHardwareSizeEnabled = isHardwareSizeEnabled;
    }

    /// <summary>
    /// Changes product print status.
    /// </summary>
    /// <param name="isNeededToPrint">Is needed to print.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangePrintStatus(bool isNeededToPrint, DateTime? date = null)
    {
        if (isNeededToPrint == IsNeededToPrint)
        {
            return;
        }

        IsNeededToPrint = isNeededToPrint;

        AddHistoryEntry(ProductChangeReasonCode.ChangedPrintStatus, !IsNeededToPrint, IsNeededToPrint, date);
    }

    /// <summary>
    /// Changes product order status.
    /// </summary>
    /// <param name="isNeededToOrder">Is needed to order.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangeOrderStatus(bool isNeededToOrder, DateTime? date = null)
    {
        if (isNeededToOrder == IsNeededToOrder)
        {
            return;
        }

        IsNeededToOrder = isNeededToOrder;

        AddHistoryEntry(ProductChangeReasonCode.ChangedOrderStatus, !IsNeededToOrder, IsNeededToOrder, date);
    }

    /// <summary>
    /// Changes the product delete status.
    /// </summary>
    /// <param name="isDelete">Is delete.</param>
    /// <param name="date">Modified date time.</param>
    public void ChangeDeletedStatus(bool isDelete, DateTime? date = null)
    {
        if (isDelete == IsDeleted)
        {
            return;
        }

        if (isDelete)
        {
            AddHistoryEntry(ProductChangeReasonCode.Deleted, false, true, date);
        }
        else
        {
            AddHistoryEntry(ProductChangeReasonCode.Recovered, true, false, date);
        }

        IsDeleted = isDelete;
    }

    private void AddInitHistoryEntry(DateTime? date = null)
    {
        AddHistoryEntry(ProductChangeReasonCode.Created, "", "", date);
    }

    /// <summary>
    /// TODO make this method as a private after migration.
    /// </summary>
    public ProductHistoryEntry AddHistoryEntry(
        ProductChangeReasonCode reasonCode,
        object oldValue,
        object newValue,
        DateTime? date = null)
    {
        _historyEntries ??= [];

        var historyEntry = new ProductHistoryEntry(
            reasonCode,
            oldValue?.ToString() ?? "",
            newValue?.ToString() ?? "",
            date ?? DateTime.UtcNow);

        _historyEntries.Add(historyEntry);
        return historyEntry;
    }
}