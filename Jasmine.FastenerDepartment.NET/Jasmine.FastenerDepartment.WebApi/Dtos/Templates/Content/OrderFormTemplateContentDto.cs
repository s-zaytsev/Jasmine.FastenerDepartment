using Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;

namespace Jasmine.FastenerDepartment.WebApi.Dtos.Templates.Content;

/// <summary>
/// Order form template content.
/// </summary>
/// <param name="HasCompanyData">whether the content contains company data.</param>
/// <param name="GroupByType">Whether the content groups product by type.</param>
/// <param name="TableColumns">The collection of table column.</param>
public record OrderFormTemplateContentDto(
    bool HasCompanyData,
    bool GroupByType,
    IEnumerable<TemplateContentTableColumnDto> TableColumns)
    : TemplateContentDto;