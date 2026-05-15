using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Jasmine.FastenerDepartment.Domain.Settings.Models;
using Jasmine.FastenerDepartment.Domain.Suppliers.Models;
using Jasmine.FastenerDepartment.EF;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Jasmine.FastenerDepartment.WebApi.Tests;

public static class DataService
{
    public static async Task AddProducts(params Product[] products)
    {
        Config.DbContext.AddRange(products);
        await Config.DbContext.SaveChangesAsync();
    }

    public static async Task AddSuppliers(params Supplier[] suppliers)
    {
        Config.DbContext.AddRange(suppliers);
        await Config.DbContext.SaveChangesAsync();
    }

    public static async Task AddOrders(params Order[] orders)
    {
        Config.DbContext.AddRange(orders);
        await Config.DbContext.SaveChangesAsync();
    }

    public static async Task AddProductTypes(params ProductType[] productTypes)
    {
        Config.DbContext.AddRange(productTypes);
        await Config.DbContext.SaveChangesAsync();
    }

    public static async Task ClearAsync()
    {
        var context = Config.CreateDbContext();
        ClearAsync<Order>(context);
        ClearAsync<Product>(context);
        ClearAsync<ProductHistoryEntry>(context);
        ClearAsync<SettingsEntry>(context);
        ClearAsync<Supplier>(context);
        ClearAsync<ProductType>(context);

        await context.SaveChangesAsync();
    }

    public static void ClearAsync<T>(ApplicationDbContext context) where T : class
    {
        context.Set<T>().RemoveRange(context.Set<T>());
    }

    public static HttpContent GetJsonContent(object obj)
    {
        var jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        var json = JsonConvert.SerializeObject(obj, jsonSettings);

        return GetContent("application/json", json);
    }

    public static HttpContent GetContent(string contentType, string contentValue)
    {
        var content = new StringContent(contentValue);
        content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

        return content;
    }
}
