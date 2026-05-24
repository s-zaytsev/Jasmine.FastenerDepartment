using Jasmine.FastenerDepartment.Domain.Orders.Models;
using Jasmine.FastenerDepartment.Templates.Constants;

namespace Jasmine.FastenerDepartment.Templates.Builders.Html;

internal class HtmlOrderRequestTemplateBuilder : HtmlTemplateBuilderBase
{
    private readonly Order _order;

    protected override string Title { get; set; }
    protected override string Body { get; set; }

    /// <summary>
    /// Creates template.
    /// </summary>
    public HtmlOrderRequestTemplateBuilder(Order order)
    {
        _order = order;
    }

    internal override TemplateBuilderBase AddBody()
    {
        var header = GetHeader();
        var title = GetTitle();
        var productList = GetProductList();

        Title = $"{OrderConstants.ORDER.GetText(LanguageCode)} #{_order.Number}";
        Body = string.Join("\n", header, title, productList);
        return this;
    }

    private string GetHeader()
    {
        return @$" 
            <div>
              <table style=""width: 100%"">
	            <tbody style=""vertical-align: baseline"">
              <tr>
                <td>
                  <div>
                    <p style=""
                        font-family: sans-serif;
                        font-size: 28px;
                        margin-bottom: 0px;
                        color: #7C3AED"">
                            Жасмин
                    </p>
                    <p style=""
                        font-family: sans-serif;
                        margin-top: 0px;
                        font-family: sans-serif;
                        color: #6B7280"">
                            Отдел крепежа
                    </p>
                  </div>
                </td>
                <td>
                  <div style=""text-align: right;"">
                    <p style=""
                        font-family: sans-serif;
                        margin-bottom: 0px;
                        font-size: 16px;
                        color: #6B7280"">
                            {OrderConstants.ORDER.GetText(LanguageCode)} #{_order.GetNumberAsText()}
                    </p>
                    <p style=""
                        margin-top: 0px;
                        font-family: sans-serif;
                        color: #6B7280"">
                            {OrderConstants.FROM.GetText(LanguageCode)} {_order.CreatedDate:dd.MM.yyyy}
                    </p>
                  </div>
                </td>
              </tr>
            </tbody>
            </table>
           </div>";
    }

    private string GetTitle()
    {
        return $@"
        <div style=""width: 100%; text-align: center; margin: 30px 0"">
          <div>
            <p style=""
                font-family: sans-serif;
                font-size: 36px;
                font-weight: 500;
                margin-bottom: 5px;
                color: #7C3AED"">
                    {_order.Supplier?.Name.Value}
            </p>
            <p style=""
                font-family: sans-serif;
                margin-top: 0px;
                font-family: sans-serif;
                color: #6B7280"">
                    {_order.Supplier?.Address}
            </p>
          </div>
        </div>";
    }

    private string GetProductList()
    {
        var groupedProducts = _order.Products
            .GroupBy(x => x.Product.Type.Name.Value)
            .OrderBy(x => x.Key)
            .ToDictionary(
                x => x.Key ?? "others",
                x => x.OrderBy(x => x.ProductName.Value).ToList());

        return string.Join("\n", groupedProducts.Select(x => GetSection(x.Key, x.Value)));
    }

    private string GetSection(string name, List<OrderProduct> products)
    {
        var header = GetSectionHeader(name, products.Count);
        var table = GetSectionTable(products);

        return $@"
          <div style=""width: 100%; margin-top: 30px"">
                {header}
                {table}    
          </div>";
    }

    private string GetSectionHeader(string name, int count)
    {
        return $@"
            <div style=""width: 100%;"">
              <table>
                <tr>
                  <td>
                    <div style=""
                        width: 4px;
                        background-color: #7C3AED;
                        border-radius: 4px"">
                            &nbsp;
                    </div>
                  </td>
                  <td>
                    <p style=""
                        font-family: sans-serif;
                        margin: 0 5px"">
                            {name}
                    </p>
                  </td>
                  <td>
                    <div style=""
                        background-color: #FFFFFF;
                        padding: 2px 5px;
                        font-family: sans-serif;
                        font-size: 12px"">
                            {count} {OrderConstants.GetPositionText(count).GetText(LanguageCode)}
                    </div>
                  </td>
                </tr>
              </table>
            </div>";
    }

    private string GetSectionTable(List<OrderProduct> products)
    {
        return $@"
          <div>
            <table style=""width: 100%; margin-top: 10px"">
              <thead>
                <tr style=""box-sizing: border-box; height: 35px"">
                  <td style=""
                    width: 20%;
                    font-family: sans-serif;
                    font-size: 14px;
                    color: #6B7280;
                    padding: 0 5px;"">
                        {OrderConstants.NUMBER.GetText(LanguageCode)}
                  </td>
                  <td style=""
                    width: 20%;
                    font-family: sans-serif;
                    font-size: 14px;
                    color: #6B7280;
                    padding: 0 5px;"">
                        {OrderConstants.NAME.GetText(LanguageCode)}
                  </td>
                  <td style=""
                    width: 20%;
                    font-family: sans-serif;
                    font-size: 14px;
                    color: #6B7280;
                    padding: 0 5px;"">
                        {OrderConstants.AMOUNT.GetText(LanguageCode)}
                  </td>
                </tr>
              </thead>
              <tbody>
                {string.Join("\n", products.Select(GetTableRow))}
              </tbody>
            </table>
          </div>";
    }

    private string GetTableRow(OrderProduct product)
    {
        return $@"
            <tr style=""background-color: #FFFFFF; box-sizing: border-box; height: 35px"">
              <td style=""
                font-family: sans-serif;
                font-size: 14px;
                padding: 0 5px;"">
                    {product.SupplierProductNumber}
              </td>
              <td style=""
                font-family: sans-serif;
                font-size: 14px;
                padding: 0 5px;"">
                    {product.ProductName.Value}
              </td>
              <td style=""
                font-family: sans-serif;
                font-size: 14px;
                padding: 0 5px;"">
                    {product.Ordered.Value} {product.Ordered.MeasurementUnit.ShortName.GetText(LanguageCode)}
              </td>
            </tr>
        ";
    }
}
