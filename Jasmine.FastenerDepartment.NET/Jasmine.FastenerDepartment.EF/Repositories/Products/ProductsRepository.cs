using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;
using Jasmine.FastenerDepartment.EF.Extensions;
using Jasmine.FastenerDepartment.EF.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jasmine.FastenerDepartment.EF.Repositories.Products;

/// <summary>
/// Products repository.
/// </summary>
internal class ProductsRepository : RepositoryBase<Guid, Product>, IProductsRepository
{
    private enum PredicateCode
    {
        PriceRange,
        Suppliers,
        PriceTags,
        Types,
        OnlyToPrint,
        OnlyToOrder
    }

    /// <summary>
    /// Creates repository.
    /// </summary>
    /// <param name="context">Context.</param>
    public ProductsRepository(ApplicationDbContext context)
        : base(context)
    { }

    /// <summary>
    /// Returns the list of products with history.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products.</returns>
    public async Task<IEnumerable<Product>> GetAllWithHistoryAsync(CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .Include(x => x.HistoryEntries)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Returns product by number.
    /// </summary>
    /// <param name="number">Product number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product.</returns>
    public async Task<Product> GetByNumberAsync(int number, CancellationToken cancellationToken = default)
    {
        return await GetQuery().FirstOrDefaultAsync(x => x.Number.Value == number, cancellationToken);
    }

    /// <summary>
    /// Returns the product by identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product.</returns>
    public override async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .Include(x => x.HistoryEntries).ThenInclude(x => x.Reason)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>
    /// Returns last product number.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Last product number.</returns>
    public async Task<int> GetLastProductNumberAsync(CancellationToken cancellationToken = default)
    {
        var product = await GetQuery().OrderByDescending(x => x.Number.Value).FirstOrDefaultAsync(cancellationToken);
        return product?.Number.Value ?? 10000000;
    }

    /// <summary>
    /// Returns the list of products to print.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products to print.</returns>
    public async Task<IEnumerable<Product>> GetProductsToPrintAsync(CancellationToken cancellationToken = default)
    {
        return await GetQuery().Where(x => x.IsNeededToPrint).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Returns the list of products by identifiers.
    /// </summary>
    /// <param name="ids">Identifiers.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of products by identifiers.</returns>
    public async Task<ICollection<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await GetQuery()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductFilters> GetPageFiltersAsync(ProductsQuery query, CancellationToken cancellationToken)
    {
        var priceRange = await GetPriceRangeFilterAsync(query, cancellationToken);
        var types = await GetTypeFiltersAsync(query, cancellationToken);
        var priceTags = await GetPriceTagFiltersAsync(query, cancellationToken);
        var suppliers = await GetSupplierFiltersAsync(query, cancellationToken);
        var onlyToPrint = await GetOnlyToPrintFilterAsync(query, cancellationToken);
        var onlyToOrder = await GetOnlyToOrderFilterAsync(query, cancellationToken);

        var filters = new ProductFilters
        {
            PriceRange = priceRange,
            Types = new MultiFilter<Guid?> { Items = types },
            PriceTags = new MultiFilter<PriceTagCode> { Items = priceTags },
            Suppliers = new MultiFilter<Guid?> { Items = suppliers },
            OnlyToPrint = onlyToPrint,
            OnlyToOrder = onlyToOrder
        };
        return filters;
    }

    protected override IEnumerable<Expression<Func<Product, bool>>> GetFilterings<T, K>(
        IQueryable<Product> dbQuery, T query)
    {
        var productQuery = query as ProductsQuery;

        var predicates = new List<Expression<Func<Product, bool>>>();

        var basePredicates = GetBasicPredicates(productQuery);
        var extraPredicates = GetExtraPredicates(productQuery);

        predicates.AddRange(basePredicates);
        predicates.AddRange(extraPredicates);

        return predicates;
    }

    protected override Expression<Func<Product, object>> GetSorting<E>(E parameter)
    {
        return parameter switch
        {
            ProductsQueryParameter.ProductNumber => x => x.Number.Value,
            ProductsQueryParameter.Name => x => x.Name.Value,
            ProductsQueryParameter.Price => x => x.Price.Value,
            ProductsQueryParameter.PriceTag => x => x.PriceTagCode,
            ProductsQueryParameter.Type => x => x.Type.Name.Value,
            _ => x => x.Id,
        };
    }

    protected override IQueryable<Product> GetQuery()
    {
        return base.GetQuery()
            .Include(x => x.PriceTag)
            .Include(x => x.MeasurementUnit)
            .Include(x => x.Suppliers)
            .Include(x => x.Type);
    }

    private async Task<RangeFilter<decimal>> GetPriceRangeFilterAsync(
        ProductsQuery query, CancellationToken cancellationToken)
    {
        var dbQueryFullRange = GetQuery();
        var dbQueryCurrentRange = GetQuery(query);

        var priceFullRange = await dbQueryFullRange
            .Select(x => new
            {
                Min = dbQueryFullRange.Min(y => y.Price.Value),
                Max = dbQueryFullRange.Max(y => y.Price.Value)
            })
            .FirstOrDefaultAsync(cancellationToken);

        var priceCurrentRange = await dbQueryCurrentRange
            .Select(x => new
            {
                Min = dbQueryCurrentRange.Min(y => y.Price.Value),
                Max = dbQueryCurrentRange.Max(y => y.Price.Value)
            })
            .FirstOrDefaultAsync(cancellationToken);

        var filter = new RangeFilter<decimal>
        {
            Min = priceFullRange?.Min ?? default,
            Max = priceFullRange?.Max ?? default,
            From = query.PriceFrom ?? priceCurrentRange?.Min ?? default,
            To = query.PriceTo ?? priceCurrentRange?.Max ?? default
        };

        return filter;
    }

    private async Task<ICollection<Filter<Guid?>>> GetTypeFiltersAsync(
        ProductsQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = GetQuery(query, PredicateCode.Types);

        var filters = await GetQuery()
            .GroupBy(x => x.TypeId)
            .Select(type => new Filter<Guid?>
            {
                Id = type.Key,
                Title = type.FirstOrDefault().Type.Name.Value ?? string.Empty,
                Count = dbQuery.Count(x => x.TypeId == type.Key)
            })
            .ToListAsync(cancellationToken);

        filters.ForEach(x => x.IsEnabled = query.Types?.Contains(x.Id) == true);
        filters = [.. filters.OrderBy(x => x.Title)];

        return filters;
    }

    private async Task<ICollection<Filter<PriceTagCode>>> GetPriceTagFiltersAsync(
        ProductsQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = GetQuery(query, PredicateCode.PriceTags);

        var filters = await GetQuery()
            .GroupBy(x => x.PriceTagCode)
            .Select(type => new Filter<PriceTagCode>
            {
                Id = type.Key,
                Title = type.FirstOrDefault().PriceTag.Name.GetText(query.LanguageCode) ?? string.Empty,
                Count = dbQuery.Count(x => x.PriceTagCode == type.Key)
            })
            .ToListAsync(cancellationToken);

        filters.ForEach(x => x.IsEnabled = query.PriceTags?.Contains(x.Id) == true);
        filters = [.. filters.OrderBy(x => x.Title)];

        return filters;
    }

    private async Task<ICollection<Filter<Guid?>>> GetSupplierFiltersAsync(
        ProductsQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = GetQuery(query, PredicateCode.Suppliers);

        var filters = await dbQuery
            .SelectMany(p => p.Suppliers.DefaultIfEmpty())
            .GroupBy(s => s == null ? (Guid?)null : s.Id)
            .Select(g => new Filter<Guid?>
            {
                Id = g.Key,
                Title = g.Key == null
                    ? string.Empty
                    : g.First().Name.Value,
                Count = g.Count(),
                IsEnabled = g.Key != null && query.Suppliers.Contains(g.Key.Value) == true
            })
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);

        return filters;
    }

    private async Task<SingleFilter> GetOnlyToPrintFilterAsync(
        ProductsQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = GetQuery(query, PredicateCode.OnlyToPrint);

        var filter = new SingleFilter
        {
            Count = await dbQuery.Where(x => x.IsNeededToPrint).CountAsync(cancellationToken),
            IsEnabled = query.OnlyToPrint
        };

        return filter;
    }

    private async Task<SingleFilter> GetOnlyToOrderFilterAsync(
        ProductsQuery query, CancellationToken cancellationToken)
    {
        var dbQuery = GetQuery(query, PredicateCode.OnlyToOrder);

        var filter = new SingleFilter
        {
            Count = await dbQuery.Where(x => x.IsNeededToOrder).CountAsync(cancellationToken),
            IsEnabled = query.OnlyToOrder
        };

        return filter;
    }

    private IQueryable<Product> GetQuery(ProductsQuery query, PredicateCode? skip = null)
    {
        var dbQuery = GetQuery();

        var predicates = new List<Expression<Func<Product, bool>>>();
        predicates.AddRange(GetBasicPredicates(query));
        predicates.AddRange(GetExtraPredicates(query, skip));

        if (predicates.Any())
        {
            var predicate = predicates.Aggregate((x, y) => x.And(y));
            dbQuery = dbQuery.Where(predicate);
        }

        return dbQuery;
    }

    private IEnumerable<Expression<Func<Product, bool>>> GetBasicPredicates(ProductsQuery query)
    {
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            yield return x =>
                x.Name.Value.ToLower().Contains(query.Search.ToLower()) ||
                x.Number.Value.ToString().Contains(query.Search);
        }
    }

    private IEnumerable<Expression<Func<Product, bool>>> GetExtraPredicates(
        ProductsQuery query, PredicateCode? skip = null)
    {
        if (skip != PredicateCode.PriceRange)
        {
            if (query.PriceFrom.HasValue)
            {
                yield return x => x.Price.Value >= query.PriceFrom;
            }

            if (query.PriceTo.HasValue)
            {
                yield return x => x.Price.Value <= query.PriceTo;
            }
        }

        if (query.Suppliers?.Count > 0 && skip != PredicateCode.Suppliers)
        {
            if (query.Suppliers.Contains(null))
            {
                yield return x => x.Suppliers.Any(a => query.Suppliers.Contains(a.Id)) || !x.Suppliers.Any();
            }
            else
            {
                yield return x => x.Suppliers.Any(a => query.Suppliers.Contains(a.Id));
            }
        }

        if (query.Types?.Count > 0 && skip != PredicateCode.Types)
        {
            yield return x => query.Types.Contains(x.TypeId);
        }

        if (query.PriceTags?.Count > 0 && skip != PredicateCode.PriceTags)
        {
            yield return x => query.PriceTags.Contains(x.PriceTagCode);
        }

        if (query.OnlyToPrint && skip != PredicateCode.OnlyToPrint)
        {
            yield return x => x.IsNeededToPrint;
        }

        if (query.OnlyToOrder && skip != PredicateCode.OnlyToOrder)
        {
            yield return x => x.IsNeededToOrder;
        }
    }
}
