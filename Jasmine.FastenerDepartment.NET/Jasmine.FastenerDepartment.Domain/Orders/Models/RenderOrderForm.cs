using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Orders.Models;

/// <summary>
/// Render order form model.
/// </summary>
public class RenderOrderForm
{
    /// <summary>
    /// Template identifier.
    /// </summary>
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Template format code.
    /// </summary>
    public TemplateFormatCode FormatCode { get; set; }

    /// <summary>
    /// Company identifier.
    /// </summary>
    public Guid? CompanyId { get; set; }
}
