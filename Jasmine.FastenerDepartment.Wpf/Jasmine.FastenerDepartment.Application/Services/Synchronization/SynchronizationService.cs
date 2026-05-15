using Jasmine.FastenerDepartment.Application.Models.Synchronization;
using Jasmine.FastenerDepartment.Domain.Common.Extensions;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Repositories;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.Domain.Products.Repositories;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;
using System.Linq;

namespace Jasmine.FastenerDepartment.Application.Services.Synchronization;

/// <summary>
/// Synchronization service.
/// </summary>
public class SynchronizationService : ISynchronizationService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductHistoryRepository _productHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Creates service.
    /// </summary>
    /// <param name="productsRepository">Products repository.</param>
    /// <param name="productHistoryRepository">Products history repository.</param>
    /// <param name="unitOfWork">Unit of work.</param>
    public SynchronizationService(
        IProductsRepository productsRepository,
        IProductHistoryRepository productHistoryRepository,
        IUnitOfWork unitOfWork)
    {
        _productsRepository = productsRepository;
        _productHistoryRepository = productHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Synchronizes the products.
    /// </summary>
    /// <param name="request">Synchronization request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Synchronization response.</returns>
    public async Task<SynchronizationResponse> SynchronizeAsync(
        SynchronizationRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var response = new SynchronizationResponse()
        {
            ModifiedProducts = [],
            NewProducts = [],
            HistoryEntries = []
        };

        var historyEntriesByClientChanges = new List<SynchronizationHistoryEntry>();

        var products = await _productsRepository.GetAllWithHistoryAsync(cancellationToken);

        if (!request.LastSynchronizeUtcTime.HasValue)
        {
            response.NewProducts = [.. products.Select(ApplicationMapper.Map)];

            return response;
        }

        var newProductsForClient = products.Where(x => x.CreatedDate > request.LastSynchronizeUtcTime.Value.AddMinutes(-1));
        var newProductsForServer = request.Products.Where(x => !products.Any(a => a.Id == x.Id));

        //  await CheckProductNumbers(newProductsForServer);

        if (newProductsForServer.Any() == true)
        {
            var lastNumber = await _productsRepository.GetLastProductNumberAsync(cancellationToken);

            var mappedProducts = newProductsForServer
                .Select(x => new Product(
                    x.Id,
                    x.CreatedDate,
                    x.ModifiedDate,
                    ++lastNumber,
                    x.Name,
                    x.Price,
                    x.PriceTagCode,
                    x.MeasurementUnitCode,
                    x.IsDeleted))
                .ToList();

            _productsRepository.AddRange(mappedProducts);

            var historyEntries = mappedProducts.Select(x => new SynchronizationHistoryEntry
            {
                Date = DateTime.UtcNow,
                Items = new List<SynchronizationHistoryEntryItem>
                        {
                            new SynchronizationHistoryEntryItem
                            {
                                ProductId = x.Id,
                                ProductHistoryEntries =
                                [
                                    new(
                                        ProductChangeReasonCode.ChangedNumber,
                                        newProductsForServer.First(f => f.Id == x.Id).Number.ToString(),
                                        x.Number.Value.ToString(),
                                        DateTime.UtcNow
                                        )
                                ],
                            }
                        }
            });

            historyEntriesByClientChanges.AddRange(historyEntries);
        }

        if (newProductsForClient.Any() == true)
        {
            response.NewProducts = [.. newProductsForClient.Select(ApplicationMapper.Map)];
        }

        request.Products =
            request.Products.Where(x => !newProductsForServer.Contains(x));

        var modifiedProductsOnServer = products.Where(x => x.ModifiedDate > request.LastSynchronizeUtcTime.Value.SetKindUtc());

        foreach (var clientProduct in request.Products)
        {
            var product = products.FirstOrDefault(x => x.Id == clientProduct.Id);

            if (product != null)
            {
                if (product.ModifiedDate > clientProduct.ModifiedDate)
                {
                    response.ModifiedProducts.Add(ApplicationMapper.Map(product));
                }
                else
                {
             //       product.ChangeNumber(clientProduct.Number, clientProduct.ModifiedDate);
                    product.ChangeName(clientProduct.Name, clientProduct.ModifiedDate);
                    product.ChangePrice(clientProduct.Price, clientProduct.ModifiedDate);
                    product.ChangePriceTagCode(clientProduct.PriceTagCode, clientProduct.ModifiedDate);
            //        product.ChangePrintStatus(clientProduct.IsNeededToPrint, clientProduct.ModifiedDate);
                    product.ChangeOrderStatus(clientProduct.IsNeededToOrder, clientProduct.ModifiedDate);
                    product.ChangeDeletedStatus(clientProduct.IsDeleted, clientProduct.ModifiedDate);

                    var historyEntries = product
                        .HistoryEntries
                        .Where(x => x.CreatedDate == clientProduct.ModifiedDate)
                        .ToList();

                    var syncHistoryEntry = new SynchronizationHistoryEntry
                    {
                        Date = clientProduct.ModifiedDate,
                        Items = new List<SynchronizationHistoryEntryItem>
                        {
                            new SynchronizationHistoryEntryItem
                            {
                                ProductId = clientProduct.Id,
                                ProductHistoryEntries = historyEntries,
                            }
                        }
                    };

                    historyEntriesByClientChanges.Add(syncHistoryEntry);
                    _productsRepository.UpdateWithTime(product, clientProduct.ModifiedDate);
                }
            }
        }

        foreach (var serverProduct in modifiedProductsOnServer)
        {
            var clientProduct = request.Products.FirstOrDefault(x => x.Id == serverProduct.Id);
            if (clientProduct != null)
            {
                if (clientProduct.ModifiedDate < serverProduct.ModifiedDate)
                {
                    if (!response.ModifiedProducts.Any(x => x.Id == serverProduct.Id) &&
                        !response.NewProducts.Any(x => x.Id == serverProduct.Id))
                    {
                        response.ModifiedProducts.Add(ApplicationMapper.Map(serverProduct));
                    }
                }
            }
            else
            {
                var existedProductForServer = newProductsForServer.FirstOrDefault(x => x.Id == serverProduct.Id);

                if (existedProductForServer != null)
                {
                    continue;
                }

                var existedProductForClient = newProductsForClient.FirstOrDefault(x => x.Id == serverProduct.Id);

                if (existedProductForClient != null)
                {
                    continue;
                }

                response.ModifiedProducts.Add(ApplicationMapper.Map(serverProduct));
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var history = await GetHistoryAsync(request.LastSynchronizeUtcTime.Value.SetKindUtc(), cancellationToken);
        AddClientChangesToHistory(history, historyEntriesByClientChanges);

        response.HistoryEntries = history;

        return response;
    }

    private async Task CheckProductNumbers(IEnumerable<SynchronizationProduct> products)
    {
        foreach (var product in products)
        {
            var existingProduct = await _productsRepository.GetByNumberAsync(product.Number);
            if (existingProduct != null)
            {
                throw new InvalidOperationException($"Product with number {product.Number} is already existing.");
            }
        }
    }

    private async Task<List<SynchronizationHistoryEntry>> GetHistoryAsync(
        DateTime dateFrom, CancellationToken cancellationToken)
    {
        var historyEntries =
            await _productHistoryRepository.GetByDatesAsync(dateFrom, DateTime.UtcNow, cancellationToken);

        var groupedHistoryEntries = historyEntries
            .GroupBy(x => x.CreatedDate.Date)
            .OrderBy(x => x.Key)
            .Select(x => new SynchronizationHistoryEntry
            {
                Date = x.Key,
                Items =
                    x.GroupBy(x => x.ProductId)
                    .Select(s => new SynchronizationHistoryEntryItem
                    {
                        ProductId = s.Key,
                        ProductHistoryEntries = s.OrderBy(x => x.CreatedDate).ToList()
                    })
                    .ToList()
            });

        return [.. groupedHistoryEntries];
    }

    private static void AddClientChangesToHistory(
        List<SynchronizationHistoryEntry> historyEntries,
        List<SynchronizationHistoryEntry> clientHistoryEntries)
    {
        foreach (var entry in clientHistoryEntries)
        {
            var dayHistory = historyEntries.FirstOrDefault(x => x.Date.Date == entry.Date.Date);
            if (dayHistory != null)
            {
                foreach (var productHistory in dayHistory.Items)
                {
                    var productDayHistory =
                        dayHistory.Items.FirstOrDefault(x => x.ProductId == productHistory.ProductId);

                    if (productHistory != null)
                    {
                        productDayHistory.ProductHistoryEntries.ToList().AddRange(productHistory.ProductHistoryEntries);
                    }
                    else
                    {
                        dayHistory.Items.Add(productHistory);
                    }
                }
            }
            else
            {
                historyEntries.Add(entry);
            }
        }
    }
}
