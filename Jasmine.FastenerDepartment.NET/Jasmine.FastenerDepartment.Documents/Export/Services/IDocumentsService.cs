using Jasmine.FastenerDepartment.Domain.Products.Models;

namespace Jasmine.FastenerDepartment.Documents.Export.Services;

internal interface IDocumentsService
{
    Task<Stream> GetStreamAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default);
}
