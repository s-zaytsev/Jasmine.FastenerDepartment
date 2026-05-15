using FluentAssertions;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.WebApi.Dtos.Suppliers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Jasmine.FastenerDepartment.WebApi.Tests;

[TestFixture]
internal class SuppliersControllerTests
{
    [SetUp]
    public async Task SetUpAsync()
    {
        await DataService.ClearAsync();
    }

    [TearDown]
    public async Task TearDownAsync()
    {
        await DataService.ClearAsync();
    }

    [Test]
    public async Task CanGetSuppliers()
    {
        var suppliers = new Supplier[]
        {
            new("Test 1", "Test address 1"),
            new("Test 2", "Test address 2")
        };

        await DataService.AddSuppliers(suppliers);

        var response = await Config.HttpClient.GetStringAsync($"/suppliers");

        var supplierList = JsonConvert.DeserializeObject<IEnumerable<SupplierDto>>(response);

        suppliers.Should().NotBeNull();
        suppliers.Should().HaveCount(2);
    }

    [Test]
    public async Task CanGetSupplier()
    {
        var suppliers = new Supplier[]
        {
            new("Test 1", "Test address 1"),
            new("Test 2", "Test address 2")
        };

        await DataService.AddSuppliers(suppliers);

        var response = await Config.HttpClient.GetStringAsync($"/suppliers/{suppliers.Last().Id}");

        var supplier = JsonConvert.DeserializeObject<SupplierDto>(response);

        supplier.Should().NotBeNull();
        supplier.Id.Should().Be(suppliers.Last().Id);
    }

    [Test]
    public async Task CanCreate()
    {
        var model = new ChangeSupplierModelDto("Created by test", "Test address");

        var json = DataService.GetJsonContent(model);

        var response = await Config.HttpClient.PostAsync($"/suppliers", json);
        response.EnsureSuccessStatusCode();

        var suppliersDb = await Config.CreateDbContext().Suppliers.ToListAsync();

        suppliersDb.Count.Should().Be(1);
    }

    [Test]
    public async Task CanUpdate()
    {
        var suppliers = new Supplier[]
        {
            new("Test 1", "Test address 1"),
            new("Test 2", "Test address 2")
        };

        await DataService.AddSuppliers(suppliers);

        var model = new ChangeSupplierModelDto("Updated by test", "Test address");

        var json = DataService.GetJsonContent(model);

        var response = await Config.HttpClient.PutAsync($"/suppliers/{suppliers.Last().Id}", json);
        response.EnsureSuccessStatusCode();

        var suppliersDb = await Config.CreateDbContext().Suppliers.ToListAsync();

        suppliersDb.First(x => x.Id == suppliers.Last().Id).Name.Value.Should().Be("Updated by test");
    }
}
