using Jasmine.FastenerDepartment.Domain.Common.Repositories;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;

namespace Jasmine.FastenerDepartment.Domain.PriceTags.Repositories;

/// <summary>
/// Price tags repository. 
/// </summary>
public interface IPriceTagsRepository : IEntitiesRepository<PriceTagCode, PriceTag>
{}
