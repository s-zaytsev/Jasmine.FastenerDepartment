using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Companies.Models;

/// <summary>
/// Company type.
/// </summary>
public class CompanyType : EntityBase<CompanyTypeCode>
{
    /// <summary>
    /// Name.
    /// </summary>
    public LocalizedString Name { get; init; }

    private CompanyType() { }

    /// <summary>
    /// Creates company type.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="name">Name.</param>
    public CompanyType(
        CompanyTypeCode id,
        LocalizedString name)
    {
        Id = id;
        Name = name;
    }
}
