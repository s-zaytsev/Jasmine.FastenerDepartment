using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Common.Exceptions.Codes;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

/// <summary>
/// Order product.
/// </summary>
public class OrderProduct : EntityBase<Guid>
{
    /// <summary>
    /// Order identifier.
    /// </summary>
    public Guid OrderId { get; init; }

    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid? ProductId { get; private set; }

    /// <summary>
    /// Product name.
    /// </summary>
    public Name ProductName { get; private set; }

    /// <summary>
    /// Product type identifier.
    /// </summary>
    public Guid? ProductTypeId { get; private set; }

    /// <summary>
    /// Ordered quantity.
    /// </summary>
    public Quantity Ordered { get; private set; }

    /// <summary>
    /// Fulfilled quantity.
    /// </summary>
    public Quantity Fulfilled { get; private set; }

    /// <summary>
    /// Is fulfilled.
    /// </summary>
    public bool IsFulfilled { get; private set; }

    /// <summary>
    /// Supplier product number.
    /// </summary>
    public string SupplierProductNumber { get; private set; }

    /// <summary>
    /// Product.
    /// </summary>
    public Product Product { get; private set; }

    private OrderProduct() { }

    /// <summary>
    /// Creates the order product.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="productName">Product name.</param>
    /// <param name="productTypeId">Product type identifier.</param>
    /// <param name="ordered">Ordered quantity.</param>
    /// <param name="supplierProductNumber">Supplier product number.</param>
    public OrderProduct(
        Guid? productId,
        string productName,
        Guid? productTypeId,
        Quantity ordered,
        string supplierProductNumber)
    {
        ProductId = productId;
        ProductName = new(productName);
        ProductTypeId = productTypeId;
        Ordered = ordered;
        SupplierProductNumber = supplierProductNumber;
    }

    /// <summary>
    /// Creates the order product.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    /// <param name="quantity">Quantity.</param>
    public OrderProduct(
        Guid productId,
        Quantity quantity)
    {
        ProductId = productId;
        Ordered = quantity;
    }

    /// <summary>
    /// Creates the order product.
    /// </summary>
    /// <param name="productName">Product name.</param>
    /// <param name="quantity">Quantity.</param>
    public OrderProduct(
        string productName,
        Quantity quantity)
    {
        ProductName = new(productName);
        Ordered = quantity;
    }

    /// <summary>
    /// Changes the product name.
    /// </summary>
    /// <param name="name">Name.</param>
    public void ChangeName(string name)
    {
        if (ProductId.HasValue)
        {
            DomainGuard.ThrowOrderException(OrderExceptionCode.ProductNameCannotBeChanged);
        }

        ProductName.Value = name;
    }

    /// <summary>
    /// Changes the product type.
    /// </summary>
    /// <param name="typeId">Product type identifier.</param>
    public void ChangeProductType(Guid? typeId)
    {
        if (ProductId.HasValue)
        {
            DomainGuard.ThrowOrderException(OrderExceptionCode.ProductTypeCannotBeChanged);
        }

        ProductTypeId = typeId;
    }

    /// <summary>
    /// Changes supplier product number.
    /// </summary>
    /// <param name="supplierProductNumber">Supplier product number.</param>
    public void ChangeSupplierProductNumber(string supplierProductNumber)
    {
        //if (ProductId.HasValue)
        //{
        //    DomainGuard.ThrowOrderException(OrderExceptionCode.SupplierProductNumberCannotBeChanged);
        //}

        SupplierProductNumber = supplierProductNumber;
    }

    /// <summary>
    /// Changes the product ordered quantity. 
    /// </summary>
    /// <param name="quantity">Quantity.</param>
    public void ChangeOrderedQuantity(Quantity quantity)
    {
        ArgumentNullException.ThrowIfNull(quantity);

        Ordered = quantity;
    }

    /// <summary>
    /// Changes the product fulfilled quantity. 
    /// </summary>
    /// <param name="quantity">Quantity.</param>
    public void ChangeFulfilledQuantity(Quantity quantity)
    {
        ArgumentNullException.ThrowIfNull(quantity);

        Fulfilled = quantity;
    }

    /// <summary>
    /// Sets the status as fulfilled.
    /// </summary>
    public void SetFulfilledStatus()
    {
        IsFulfilled = true;
    }

    /// <summary>
    /// Sets product identifier.
    /// </summary>
    /// <param name="productId">Product identifier.</param>
    public void SetProductId(Guid productId)
    {
        if (ProductId.HasValue)
        {
            throw new InvalidOperationException();
        }

        ProductId = productId;
    }
}
