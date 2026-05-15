using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.Domain.Tests;

[TestFixture]
class ProductTests
{
    [Test]
    public void CreateObject_CorrectProduct_Success()
    {
        var product = new Product(10000001, "Test", 22, PriceTagCode.S, MeasurementUnitCode.Pieces);

        product.Should().NotBeNull();
        product.HistoryEntries.Should().HaveCount(1);
        product.HistoryEntries.First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Created);
    }

    [Test]
    public void ChangeNumber_Number_Success()
    {
        var product = new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, 10000002, "Test", 22, PriceTagCode.S, MeasurementUnitCode.Pieces, false);
        product.ChangeNumber(10000033);

        var historyEntries = product.HistoryEntries.OrderBy(x => x.CreatedDate);

        product.Number.Value.Should().Be(10000033);
        product.HistoryEntries.Should().HaveCount(2);
        historyEntries.First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Created);
        historyEntries.Last().ChangeReasonCode.Should().Be(ProductChangeReasonCode.ChangedNumber);
        historyEntries.Last().OldValue.Should().Be("10000002");
        historyEntries.Last().NewValue.Should().Be("10000033");
    }

    [Test]
    public void ChangeName_Name_Success()
    {
        var product = new Product(10000001, "Test", 22, PriceTagCode.S, MeasurementUnitCode.Pieces);
        product.ChangeName("New test");

        var historyEntries = product.HistoryEntries.OrderBy(x => x.CreatedDate);

        product.Name.Value.Should().Be("New test");
        product.HistoryEntries.Should().HaveCount(2);
        historyEntries.First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Created);
        historyEntries.Last().ChangeReasonCode.Should().Be(ProductChangeReasonCode.ChangedName);
        historyEntries.Last().OldValue.Should().Be("Test");
        historyEntries.Last().NewValue.Should().Be("New test");
    }

    [Test]
    public void ChangePrice_Price_Success()
    {
        var product = new Product(10000001, "Test", 22, PriceTagCode.S, MeasurementUnitCode.Pieces);
        product.ChangePrice(33);

        var historyEntries = product.HistoryEntries.OrderBy(x => x.CreatedDate);

        product.Price.Value.Should().Be(33);
        product.HistoryEntries.Should().HaveCount(2);
        historyEntries.First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Created);
        historyEntries.Last().ChangeReasonCode.Should().Be(ProductChangeReasonCode.ChangedPrice);
        historyEntries.Last().OldValue.Should().Be("22");
        historyEntries.Last().NewValue.Should().Be("33");
    }

    [Test]
    public void ChangeOrderStatus_OrderStatus_Success()
    {
        var product = new Product(10000001, "Test", 22, PriceTagCode.S, MeasurementUnitCode.Pieces);
        product.ChangeOrderStatus(true);

        var historyEntries = product.HistoryEntries.OrderBy(x => x.CreatedDate);

        product.IsNeededToOrder.Should().Be(true);
        product.HistoryEntries.Should().HaveCount(2);
        historyEntries.First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Created);
        historyEntries.Last().ChangeReasonCode.Should().Be(ProductChangeReasonCode.ChangedOrderStatus);
        historyEntries.Last().OldValue.Should().Be("False");
        historyEntries.Last().NewValue.Should().Be("True");
    }

    [Test]
    public void ChangePrintStatus_PrintStatus_Success()
    {
        var product = new Product(10000001, "Test", 22, PriceTagCode.S, MeasurementUnitCode.Pieces);
        product.ChangePrintStatus(true);

        var historyEntries = product.HistoryEntries.OrderBy(x => x.CreatedDate);

        product.IsNeededToPrint.Should().Be(true);
        product.HistoryEntries.Should().HaveCount(2);
        historyEntries.First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Created);
        historyEntries.Last().ChangeReasonCode.Should().Be(ProductChangeReasonCode.ChangedPrintStatus);
        historyEntries.Last().OldValue.Should().Be("False");
        historyEntries.Last().NewValue.Should().Be("True");
    }

    [Test]
    public void ChangeDeleteStatus_DeleteStatus_True_Success()
    {
        var product = new Product(10000001, "Test", 22, PriceTagCode.S, MeasurementUnitCode.Pieces);
        product.ChangeDeletedStatus(true);

        var historyEntries = product.HistoryEntries.OrderBy(x => x.CreatedDate);

        product.IsDeleted.Should().Be(true);
        product.HistoryEntries.Should().HaveCount(2);
        historyEntries.First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Created);
        historyEntries.Last().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Deleted);
        historyEntries.Last().OldValue.Should().Be("False");
        historyEntries.Last().NewValue.Should().Be("True");
    }

    [Test]
    public void ChangeDeleteStatus_DeleteStatus_False_Success()
    {
        var product = new Product(10000001, "Test", 22, PriceTagCode.S, MeasurementUnitCode.Pieces);
        product.ChangeDeletedStatus(true);
        product.ChangeDeletedStatus(false);

        var historyEntries = product.HistoryEntries.OrderBy(x => x.CreatedDate);

        product.IsDeleted.Should().Be(false);
        product.HistoryEntries.Should().HaveCount(3);
        historyEntries.First().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Created);
        historyEntries.Last().ChangeReasonCode.Should().Be(ProductChangeReasonCode.Recovered);
        historyEntries.Last().OldValue.Should().Be("True");
        historyEntries.Last().NewValue.Should().Be("False");
    }
}
