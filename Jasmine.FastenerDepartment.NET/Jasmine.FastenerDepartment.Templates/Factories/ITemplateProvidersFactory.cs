using Jasmine.FastenerDepartment.Templates.Models;
using Jasmine.FastenerDepartment.Templates.Providers;

namespace Jasmine.FastenerDepartment.Templates.Factories;

internal interface ITemplateProvidersFactory
{
    ITemplateProvider GetProvider(TemplateType type);
}
