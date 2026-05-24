using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Templates.Builders;

internal abstract class TemplateBuilderBase
{
    protected abstract string Title { get; set; }
    protected abstract string Body { get; set; }
    protected LanguageCode? LanguageCode { get; private set; }

    public TemplateBuilderBase SetLanguage(LanguageCode? code)
    {
        LanguageCode = code;
        return this;
    }

    public abstract string Build();

    internal abstract TemplateBuilderBase AddBody();

}
