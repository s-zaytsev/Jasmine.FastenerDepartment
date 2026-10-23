using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Templates.Emails.Constants;

namespace Jasmine.FastenerDepartment.Templates.Emails.Builders;

internal class ProductCatalogEmailTemplateBuilder : EmailTemplateBuilderBase
{
    private readonly IEnumerable<Product> _products;

    public ProductCatalogEmailTemplateBuilder(
        IEnumerable<Product> products,
        LanguageCode? languageCode)
        : base(languageCode)
    {
        _products = products;
    }

    protected override void SetTitle()
    {
        Title = $"{ProductCatalogConstants.PRODUCT_CATALOG.GetText(LanguageCode)}";
    }

    protected override void SetBody()
    {
        var header = GetHeader();
        var messageText = GetMessageText();
        var categoryList = GetCategoryList();
        Body = string.Join("\n", header, messageText, categoryList);
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
              </tr>
            </tbody>
            </table>
           </div>";
    }

    private string GetMessageText()
    {
        return $@"
        <div style=""width: 100%; text-align: left; margin: 30px 0"">
          <div>
            <p style=""
                font-family: sans-serif;
                margin-top: 0px;
                font-family: sans-serif;
                font-size: 14px;
                color: #6B7280"">
                {ProductCatalogConstants.MESSAGE_TEXT.GetText(LanguageCode)}        
            </p>
          </div>
        </div>";
    }

    private string GetCategoryList()
    {
        var others = CommonConstants.OTHERS.GetText(LanguageCode);

        var groupedProducts = _products
            .GroupBy(x => x.Type?.Name.Value)
            .ToDictionary(
                x => x.Key ?? others,
                x => x.Count())
            .OrderBy(x => (x.Key == others, x.Key))
            .ToDictionary();

        var desktopTable = GetDesktopTable(groupedProducts);
        var mobileTable = GetMobileTable(groupedProducts);

        return string.Join('\n', desktopTable, mobileTable);
    }

    private string GetDesktopTable(Dictionary<string, int> categories)
    {
        return $@"
            <table
                class=""desktop-only""
                width=""100%""
                border=""0""
                cellpadding=""0""
                style=""
                    width: 100%;
                    margin-top: 10px;
                    border-collapse: collapse;"">

              <thead>
                <tr style=""box-sizing: border-box; height: 35px"">
                <td style=""
                  width: 550px;
                  font-family: sans-serif;
                  font-size: 14px;
                  color: #6B7280;
                  padding: 0 5px;"">
                      {OrderFormConstants.NAME.GetText(LanguageCode)}
                </td>
                <td style=""
                  width: 150px;
                  font-family: sans-serif;
                  font-size: 14px;
                  text-align: center;
                  color: #6B7280;
                  padding: 0 5px;"">
                      {OrderFormConstants.AMOUNT.GetText(LanguageCode)}
                </td>
              </tr>
              </thead>
              <tbody>
                {string.Join("\n", categories.Select(GetDesktopTableRow))}
              </tbody>
            </table>";
    }

    private string GetMobileTable(Dictionary<string, int> categories)
    {
        return $@"
            <table
                class=""mobile-only""
                width=""100%""
                border=""0""
                cellpadding=""0""
                style=""
                    border-collapse: collapse;
                    display: none;
                    max-height: 0;
                    overflow: hidden;
                    visibility: hidden;"">

              <thead>
                <tr style=""box-sizing: border-box; height: 35px"">
                <td style=""
                  width: 550px;
                  font-family: sans-serif;
                  font-size: 14px;
                  color: #6B7280;
                  padding: 0 5px;"">
                      {OrderFormConstants.NAME.GetText(LanguageCode)}
                </td>
                <td style=""
                  width: 150px;
                  font-family: sans-serif;
                  font-size: 14px;
                  text-align: center;
                  color: #6B7280;
                  padding: 0 5px;"">
                      {OrderFormConstants.AMOUNT.GetText(LanguageCode)}
                </td>
              </tr>
              </thead>
              <tbody>
                {string.Join("\n", categories.Select(GetMobileTableRow))}
              </tbody>
            </table>";
    }

    private string GetDesktopTableRow(KeyValuePair<string, int> pair)
    {
        return $@"
              <tr style=""
                background-color: #FFFFFF;
                box-sizing: border-box;
                height: 35px"">
                
                <td style=""
                  font-family: sans-serif;
                  font-size: 16px;
                  border-bottom: 4px solid #F5F7FA;
                  padding: 0 5px;"">
                      {pair.Key}
                </td>
                <td style=""
                  font-family: sans-serif;
                  font-size: 16px;
                  color: #6B7280;
                  border-bottom: 4px solid #F5F7FA;
                  text-align: center;
                  padding: 0 5px;"">
                      {pair.Value} {CommonConstants.GetPositionText(pair.Value).GetText(LanguageCode)}
                </td>
              </tr>";
    }

    private string GetMobileTableRow(KeyValuePair<string, int> pair)
    {
        return $@"
              <tr style=""
                background-color: #FFFFFF;
                box-sizing: border-box;
                height: 35px"">
                
                 <td style=""
                  font-family: sans-serif;
                  font-size: 12px;
                  border-bottom: 2px solid #F5F7FA;
                  padding: 0 5px;"">
                      {pair.Key}
                </td>

                <td style=""
                  font-family: sans-serif;
                  font-size: 12px;
                  border-bottom: 2px solid #F5F7FA;
                  color: #6B7280;
                  text-align: center;
                  padding: 0 5px;"">
                      {pair.Value} {CommonConstants.GetPositionText(pair.Value).GetText(LanguageCode)}
                </td>
              </tr>";
    }
}
