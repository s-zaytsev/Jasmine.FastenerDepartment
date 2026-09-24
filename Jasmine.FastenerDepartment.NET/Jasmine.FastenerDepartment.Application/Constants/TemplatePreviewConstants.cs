using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Companies.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.ProductTypes.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.ProductCatalog;

namespace Jasmine.FastenerDepartment.Application.Constants;

internal static class TemplatePreviewConstants
{
    private static readonly LocalizedString _title = new("Company name", "Название компании");
    private static readonly LocalizedString _firstName = new("First", "Имя");
    private static readonly LocalizedString _middleName = new("Middle", "Отчество");
    private static readonly LocalizedString _lastName = new("Last", "Фамилия");
    private static readonly LocalizedString _city = new("City name", "Название города");
    private static readonly LocalizedString _street = new("Street name", "Название улицы");

    private static readonly MeasurementUnit _measurementUnit = new(
        MeasurementUnitCode.Pieces,
        new("pcs", "шт"),
        new("Pieces", "Штуки"));

    public static ProductCatalogTemplateRenderData GetCatalogData(
        TemplateContent content, LanguageCode? languageCode)
    {
        var products = GetProducts(languageCode);
        return new ProductCatalogTemplateRenderData(
            content,
            products,
            languageCode);
    }

    public static OrderFormTemplateRenderData GetOrderReuqestData(
        TemplateContent content, LanguageCode? languageCode)
    {
        var company = GetCompany(languageCode);
        var products = GetProducts(languageCode);

        var order = new Order(222, null,
        [
            new(products[0], new(11, _measurementUnit), "1-11111"),
            new(products[1], new(22, _measurementUnit), "22-2222"),
            new(products[2], new(33, _measurementUnit), "333-333")
        ]);

        return new OrderFormTemplateRenderData(
            company,
            order,
            content,
            languageCode);
    }

    private static List<Product> GetProducts(LanguageCode? languageCode)
    {
        return
        [
            new(
                10000001,
                new LocalizedString("Product", "Товар").GetText(languageCode),
                111,
                _measurementUnit, GetProductType(1, languageCode)),
            
            new(
                10000002,
                new LocalizedString("Product with size 13mm", "Товар с размером 13мм").GetText(languageCode),
                222,
                _measurementUnit, GetProductType(2, languageCode)),

            new(
                10000003,
                new LocalizedString("Product without size", "Товар без размера").GetText(languageCode),
                333,
                _measurementUnit, GetProductType(1, languageCode)),
        ];
    }

    private static Company GetCompany(LanguageCode? languageCode)
    {
        return new Company(
            _title.GetText(languageCode),
            _firstName.GetText(languageCode),
            _middleName.GetText(languageCode),
            _lastName.GetText(languageCode),
            "email@test.org",
            _city.GetText(languageCode),
            _street.GetText(languageCode),
            "22",
            "111111111111",
            "11111111111");
    }

    private static ProductType GetProductType(int typeNumber, LanguageCode? languageCode)
    {
        return typeNumber switch
        {
            1 => new ProductType(new LocalizedString("Type 1", "Тип 1").GetText(languageCode)),
            2 => new ProductType(new LocalizedString("Type 2", "Тип 2").GetText(languageCode)),
            _ => new ProductType(new LocalizedString("Type 1", "Тип 1").GetText(languageCode)),
        };
    }
}
