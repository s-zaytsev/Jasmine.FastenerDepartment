namespace Jasmine.FastenerDepartment.Domain.Common.Models;

/// <summary>
/// File model.
/// </summary>
public class FileModel
{
    /// <summary>
    /// File name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Content type.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Stream of content.
    /// </summary>
    public Stream Content { get; set; }
}
