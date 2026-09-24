using Jasmine.FastenerDepartment.Domain.Common.Builders;
using Jasmine.FastenerDepartment.Domain.Common.Expression;
using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models;
using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;

namespace Jasmine.FastenerDepartment.Domain.Templates.Builders;

/// <summary>
/// Order form template content builder.
/// </summary>
/// <typeparam name="TContainer">Type of main container.</typeparam>
/// <typeparam name="TTable">Type of table.</typeparam>
/// <typeparam name="TRow">Type of table row.</typeparam>
/// <typeparam name="TRowCell">Type of table row cell.</typeparam>
public abstract class OrderFormTemplateContentBuilder<TContainer, TTable, TRow, TRowCell>
    : TemplateBuilderBase<TContainer>
{
    private readonly OrderFormTemplateRenderData _data;

    private readonly Dictionary<OrderFormTemplateContentTableColumnCode, int> _fixedWidthPercent = new()
    {
        { OrderFormTemplateContentTableColumnCode.SupplierProductNumber, 15 },
        { OrderFormTemplateContentTableColumnCode.ProductSize, 15 },
        { OrderFormTemplateContentTableColumnCode.Amount, 10 },
    };

    private readonly LocalizedString _companyLabel = new("Company", "Компания");
    private readonly LocalizedString _fullNameLabel = new("Full name", "ФИО");
    private readonly LocalizedString _emailLabel = new("Email", "Электронная почта");
    private readonly LocalizedString _cityLabel = new("City", "Город");
    private readonly LocalizedString _addressLabel = new("Address", "Адрес");
    private readonly LocalizedString _innLabel = new("INN", "ИНН");
    private readonly LocalizedString _phoneNumberLabel = new("Phone number", "Номер телефона");

    private int _unfixedWidthPercent = 100;

    /// <summary>
    /// Creates builder.
    /// </summary>
    protected OrderFormTemplateContentBuilder(
        OrderFormTemplateRenderData data)
    {
        _data = data;

        foreach (var code in _data.Content.TableColumnCodes)
        {
            if (_fixedWidthPercent.TryGetValue(code, out var percent))
                _unfixedWidthPercent -= percent;
        }
    }

    /// <summary>
    /// Builds a content.
    /// </summary>
    /// <returns>Template render result.</returns>
    public override TemplateRenderResult Build()
    {
        SetMainContainer();
        AppendCompanyData();
        AppendProducts();

        var result = ConvertMainContainerToResult();
        return result;
    }

    /// <summary>
    /// Appends a company name line.
    /// </summary>
    /// <param name="line">Company name line.</param>
    protected abstract void AppendCompanyNameLine(string line);

    /// <summary>
    /// Appends a full name line.
    /// </summary>
    /// <param name="line">Full name line.</param>
    protected abstract void AppendFullNameLine(string line);

    /// <summary>
    /// Appends an email line.
    /// </summary>
    /// <param name="line">Email line.</param>
    protected abstract void AppendEmailLine(string line);

    /// <summary>
    /// Appends a city line.
    /// </summary>
    /// <param name="line">City line.</param>
    protected abstract void AppendCityLine(string line);

    /// <summary>
    /// Appends an address line.
    /// </summary>
    /// <param name="line">Address line.</param>
    protected abstract void AppendAddressLine(string line);

    /// <summary>
    /// Appends an INN line.
    /// </summary>
    /// <param name="line">INN line.</param>
    protected abstract void AppendInnLine(string line);

    /// <summary>
    /// Appends a phone number line.
    /// </summary>
    /// <param name="line">Phone number line.</param>
    protected abstract void AppendPhoneNumberLine(string line);

    /// <summary>
    /// Creates table.
    /// </summary>
    /// <returns>Table.</returns>
    protected abstract TTable CreateTable();

    /// <summary>
    /// Appends table to result.
    /// </summary>
    /// <param name="table">Table.</param>
    protected abstract void AppendTable(TTable table);

    /// <summary>
    /// Appends table group name.
    /// </summary>
    /// <param name="name">Name of group.</param>
    protected abstract void AppendTableGroupName(string name);

    /// <summary>
    /// Creates table row.
    /// </summary>
    /// <returns>Table row.</returns>
    protected abstract TRow CreateTableRow();

    /// <summary>
    /// Appends table row to table.
    /// </summary>
    /// <param name="table">Table.</param>
    /// <param name="row">Table row.</param>
    protected abstract void AppendTableRow(TTable table, TRow row);

    /// <summary>
    /// Creates table row cell.
    /// </summary>
    /// <param name="text">Cell text.</param>
    /// <param name="widthPercent">Width percent.</param>
    /// <returns>Table row cell.</returns>
    protected abstract TRowCell CreateTableRowCell(string text, string widthPercent);

    /// <summary>
    /// Appends table row cell to table row.
    /// </summary>
    /// <param name="row">Table row.</param>
    /// <param name="cell">Table row cell.</param>
    protected abstract void AppendTableRowCell(TRow row, TRowCell cell);

    private void AppendCompanyData()
    {
        if (!_data.Content.HasCompanyData || _data.Company == null)
            return;

        var languageCode = _data.LanguageCode;

        AppendCompanyNameLine($"{_companyLabel.GetText(languageCode)}: {_data.Company.Title.Value}");
        AppendFullNameLine(
            $"{_fullNameLabel.GetText(languageCode)}:" +
            $" {string.Join(" ", _data.Company.LastName, _data.Company.FirstName, _data.Company.MiddleName)}");
        AppendEmailLine($"{_emailLabel.GetText(languageCode)}: {_data.Company.Email.Value}");
        AppendCityLine($"{_cityLabel.GetText(languageCode)}: {_data.Company.City}");
        AppendAddressLine($"{_addressLabel.GetText(languageCode)}:" +
            $" {string.Join(", ", _data.Company.Street, _data.Company.BuildingNumber)}");
        AppendInnLine($"{_innLabel.GetText(languageCode)}: {_data.Company.Inn.Value}");
        AppendPhoneNumberLine($"{_phoneNumberLabel.GetText(languageCode)}: {_data.Company.PhoneNumber.Value}");
    }

    private void AppendProducts()
    {
        if (!_data.Content.GroupByType)
            AppendUngroupedTable(_data.Order, _data.LanguageCode);
        else
            AppendGroupedTables(_data.Order, _data.LanguageCode);
    }

    private void AppendUngroupedTable(
        Order order,
        LanguageCode? languageCode)
    {
        var table = CreateTable();
        foreach (var product in order.Products)
        {
            var row = CreateTableRow(product, languageCode);
            AppendTableRow(table, row);
        }

        AppendTable(table);
    }

    private void AppendGroupedTables(
        Order order,
        LanguageCode? languageCode)
    {
        var groupedByType = order.Products
            .GroupBy(x => x.Product.Type?.Name.Value ?? "Others")
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var pair in groupedByType)
        {
            AppendTableGroupName(pair.Key);

            var table = CreateTable();
            foreach (var product in pair.Value)
            {
                var row = CreateTableRow(product, languageCode);
                AppendTableRow(table, row);
            }

            AppendTable(table);
        }
    }

    private TRow CreateTableRow(OrderProduct product, LanguageCode? languageCode)
    {
        var row = CreateTableRow();

        foreach (var code in _data.Content.TableColumnCodes)
        {
            var widthPercent = _fixedWidthPercent.TryGetValue(code, out var width) ? width : _unfixedWidthPercent;
            var cell = GetTableRowCell(code, product, widthPercent.ToString(), languageCode);
            AppendTableRowCell(row, cell);
        }

        return row;
    }

    private TRowCell GetTableRowCell(
        OrderFormTemplateContentTableColumnCode code,
        OrderProduct product,
        string width,
        LanguageCode? languageCode)
    {
        return code switch
        {
            OrderFormTemplateContentTableColumnCode.SupplierProductNumber =>
                CreateTableRowCell(product.SupplierProductNumber, width),

            OrderFormTemplateContentTableColumnCode.ProductName =>
                CreateTableRowCell(product.ProductName.Value, width),

            OrderFormTemplateContentTableColumnCode.ProductNameWithoutSize =>
                CreateTableRowCell(
                    RegularExpressions.ContainsHardwareSize(product.ProductName.Value) ?
                        product.ProductName.Value.Replace(RegularExpressions.GetHardwareSize(product.ProductName.Value), "") :
                        product.ProductName.Value, width),

            OrderFormTemplateContentTableColumnCode.ProductSize => CreateTableRowCell(
                    RegularExpressions.ContainsHardwareSize(product.ProductName.Value) ?
                        RegularExpressions.GetHardwareSize(product.ProductName.Value) :
                        "", width),

            OrderFormTemplateContentTableColumnCode.Amount =>
                CreateTableRowCell(
                    $"{product.Ordered.Value} {product.Ordered.MeasurementUnit.ShortName.GetText(languageCode)}",
                    width),

            _ => throw new NotSupportedException(),
        };
    }
}
