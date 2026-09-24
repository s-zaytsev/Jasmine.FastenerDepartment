namespace Jasmine.FastenerDepartment.Domain.Templates.Models;

/// <summary>
/// Template render result.
/// </summary>
public class TemplateRenderResult
{
    /// <summary>
    /// Stream of content.
    /// </summary>
    public Stream Content { get; init; }

    /// <summary>
    /// Content type.
    /// </summary>
    public string ContentType { get; init; }

    /// <summary>
    /// File name.
    /// </summary>
    public string FileName { get; init; }

    /// <summary>
    /// Length of content.
    /// </summary>
    public long Length => Content?.Length ?? 0;

    /// <summary>
    /// Create a template render result.
    /// </summary>
    /// <param name="content">Stream of content.</param>
    /// <param name="contentType">Content type.</param>
    /// <param name="name">File name.</param>
    public TemplateRenderResult(
        Stream content,
        string contentType,
        string name)
    {
        Content = content;
        ContentType = contentType;
        FileName = name;
    }
}
