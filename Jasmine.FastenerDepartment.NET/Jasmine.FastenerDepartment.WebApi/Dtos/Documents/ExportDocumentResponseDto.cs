namespace Jasmine.FastenerDepartment.WebApi.Dtos.Documents;

/// <summary>
/// Export document response.
/// </summary>
/// <param name="Name">Document name.</param>
/// <param name="Stream">Document stream.</param>
public record ExportDocumentResponseDto(
    string Name,
    Stream Stream);
