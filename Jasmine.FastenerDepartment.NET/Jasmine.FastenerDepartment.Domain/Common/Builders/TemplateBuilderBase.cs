using Jasmine.FastenerDepartment.Domain.Templates.Models;

namespace Jasmine.FastenerDepartment.Domain.Common.Builders;

/// <summary>
/// Template builder base.
/// </summary>
public abstract class TemplateBuilderBase<TContainer>
{
    /// <summary>
    /// Main container.
    /// </summary>
    protected TContainer MainContainer { get; set; }

    /// <summary>
    /// Builds a template.
    /// </summary>
    /// <returns>Template render result.</returns>
    public abstract TemplateRenderResult Build();

    /// <summary>
    /// Sets the main container.
    /// </summary>
    protected abstract void SetMainContainer();

    /// <summary>
    /// Converts the main container to the result.
    /// </summary>
    /// <returns>Template render result.</returns>
    protected abstract TemplateRenderResult ConvertMainContainerToResult();
}
