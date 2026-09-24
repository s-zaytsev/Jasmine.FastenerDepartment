namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content.OrderForm;

/// <summary>
/// Order form render request.
/// </summary>
public class OrderFormRenderRequest
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Format code.
    /// </summary>
    public TemplateFormatCode FormatCode { get; set; }

    /// <summary>
    /// Order identifier.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Company identifier.
    /// </summary>
    public Guid? CompanyId { get; set; }
}
