using Jasmine.FastenerDepartment.Documents.Export.Models;

namespace Jasmine.FastenerDepartment.Documents.Export.Services;

internal interface IDocumentsServiceFactory
{
    IDocumentsService GetService(DocumentType type);
}
