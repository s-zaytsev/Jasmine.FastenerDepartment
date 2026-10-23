namespace Jasmine.FastenerDepartment.Domain.Templates.Models.Content;

/// <summary>
/// Render request.
/// </summary>
public class RenderRequestBase
{

    /// <summary>
    /// Template identifier.
    /// </summary>
    public Guid TemplateId { get; init; }

    /// <summary>
    /// Format code.
    /// </summary>
    public TemplateFormatCode FormatCode { get; init; }

    /// <summary>
    /// Creates a request.
    /// </summary>
    /// <param name="templateId">Template identifier.</param>
    /// <param name="formatCode">Format code.</param>
    public RenderRequestBase(Guid templateId, TemplateFormatCode formatCode)
    {
        TemplateId = templateId;
        FormatCode = formatCode;
    }
}
