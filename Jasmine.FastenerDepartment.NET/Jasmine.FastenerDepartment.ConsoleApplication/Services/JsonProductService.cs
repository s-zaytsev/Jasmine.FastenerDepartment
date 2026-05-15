using Jasmine.FastenerDepartment.Domain.Common.Expression;
using Jasmine.FastenerDepartment.Domain.Common.Extensions;
using Jasmine.FastenerDepartment.Domain.HistoryEntries.Models;
using Jasmine.FastenerDepartment.Domain.MeasurementUnits.Models;
using Jasmine.FastenerDepartment.Domain.PriceTags.Models;
using Jasmine.FastenerDepartment.Domain.Products.Models;
using Jasmine.FastenerDepartment.EF;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Jasmine.FastenerDepartment.ConsoleApplication.Services;

public class JsonProductService : IJsonProductService
{
    private class JsonProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public bool HasHardwareSize { get; set; }
        public bool HardwareSizeEnabled { get; set; } = true;
        public bool IsNeededToOrder { get; set; }
        public bool IsDeleted { get; set; }
        public ProductPriceTag PriceTag { get; set; } = new();
        public DateTime? CreateUtcTime { get; set; }
        public DateTime? UpdateUtcTime { get; set; }
    }

    private class ProductPriceTag
    {
        public ProductPriceTagCode Code { get; set; } = ProductPriceTagCode.M;

        public string Name { get { return Enum.GetName(typeof(ProductPriceTagCode), Code); } }
    }

    private enum ProductPriceTagCode
    {
        S = 1,
        SM = 2,
        L = 3,
        M = 4,
        XL = 5
    }

    private class LogHistory
    {
        public string ProductNumber { get; set; }
        public ICollection<LogHistoryItem> Items { get; set; }
    }

    private class LogHistoryItem
    {
        public DateTime Date { get; set; }
        public string Value { get; set; }
    }

    private class LogHistoryItemReason
    {
        public ProductChangeReasonCode Code { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }


    private readonly ApplicationDbContext _context;

    public JsonProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ActualizeProductsFromJsonFileAsync(string filePath, string logsDirectoryPath)
    {
        var products = await GetProductsAsync(filePath);
        Console.WriteLine($"{products.Count()} were found.");

        CheckDoubledProducts(products);
        Console.WriteLine("Дубликаты не обнаружены.");

        var logs = await GetLogsAsync(logsDirectoryPath);

        Console.WriteLine("Добавляем историю к продуктам.");
        AddHistoryEntries(products, logs);

        Console.WriteLine("Записываем в базу данных.");
        await _context.AddRangeAsync(products);
        await _context.SaveChangesAsync();
        Console.WriteLine("Успешно.");
    }

    private async Task<IEnumerable<Product>> GetProductsAsync(string filePath)
    {
        var text = await File.ReadAllTextAsync(filePath);
        var products = JsonConvert.DeserializeObject<IEnumerable<JsonProduct>>(text);
        var productsToAdd = new List<Product>();

        var productsDb = await _context.Set<Product>().ToListAsync();

        foreach (var product in products)
        {
            if (productsDb.Any(x => x.Number.Value == int.Parse(product.Id)))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                product.Name = "Резерв";
            }

            var price = decimal.Parse(LogsExpressions.ProducPrice(product.Price));
            var unit = GetMeasurementUnitId(product.Price);

            var newProduct = new Product(
                product.CreateUtcTime ?? DateTime.Parse("2023-07-01").SetKindUtc(),
                product.UpdateUtcTime ?? DateTime.Parse("2023-07-01").SetKindUtc(),
                int.Parse(product.Id),
                product.Name,
                price,
                product.PriceTag.Code == ProductPriceTagCode.SM ? PriceTagCode.M : (PriceTagCode)product.PriceTag.Code,
                unit,
                product.HardwareSizeEnabled,
                product.IsNeededToOrder,
                product.IsDeleted);

            productsToAdd.Add(newProduct);
        }

        return productsToAdd;
    }

    private MeasurementUnitCode GetMeasurementUnitId(string line)
    {
        var unit = LogsExpressions.ProductMeasurementUnit(line);

        return unit switch
        {
            "" or "шт" => MeasurementUnitCode.Pieces,
            "метр" or "м" => MeasurementUnitCode.Meters,
            "кг" => MeasurementUnitCode.Kilograms,
            "упак" or "уп" or "упаков" => MeasurementUnitCode.Packages,
            "комп" or "компл" => MeasurementUnitCode.Sets,
            "лист" => MeasurementUnitCode.Lists,
            _ => throw new NotSupportedException($"Единица измерения {unit} не поддерживается"),
        };
    }

    private void CheckDoubledProducts(IEnumerable<Product> products)
    {
        var doubledProducts = products
            .GroupBy(x => x.Number)
            .Where(x => x.Count() > 1)
            .ToDictionary(g => g.Key, g => g);

        if (doubledProducts.Count > 0)
        {
            var message = string.Empty;

            message += "\nТовары с одинаковым артикулом\n";

            foreach (var product in doubledProducts)
            {
                message += $"\t{product.Key}\n";

                foreach (var item in product.Value)
                {
                    message += $"\t\t{item.Name}\t{(item.IsDeleted ? "Удалён" : "Не удалён")}\n";
                }
            }

            throw new InvalidOperationException(message);
        }
    }

    private async Task<ICollection<LogHistory>> GetLogsAsync(string directoryPath)
    {
        var files = Directory.GetFiles(directoryPath);
        Console.WriteLine($"Найдено {files.Length} файлов логов.");
        var logs = new Dictionary<string, HashSet<KeyValuePair<DateTime, string>>>();

        foreach (var file in files)
        {
            var date = file.Substring(file.Length - 14, 10);

            Console.WriteLine($"Читаем логи от {date}");

            var logsFromFile = new Dictionary<string, HashSet<KeyValuePair<DateTime, string>>>();

            if (IsOldFile(file))
            {
                logsFromFile = await GetLogsFromOldFileAsync(file);
            }
            else
            {
                logsFromFile = await GetLogsFromNewFileAsync(file);
            }

            if (!logsFromFile.Any())
            {
                continue;
            }

            foreach (var log in logsFromFile)
            {
                if (!logs.ContainsKey(log.Key))
                {
                    logs.Add(log.Key, log.Value);
                }
                else
                {
                    foreach (var pair in log.Value)
                    {
                        logs[log.Key].Add(pair);
                    }
                }
            }
        }

        var result = logs.Select(x => new LogHistory
        {
            ProductNumber = x.Key,
            Items = x.Value.Select(s => new LogHistoryItem
            {
                Date = s.Key,
                Value = RegularExpressions.RemoveMultiplySpaces(s.Value),
            })
            .ToList()
        }).ToList();

        return result;
    }

    private async Task<Dictionary<string, HashSet<KeyValuePair<DateTime, string>>>> GetLogsFromOldFileAsync(string filePath)
    {
        var isBlockOpen = false;
        var productNumber = string.Empty;
        var date = string.Empty;

        var logs = new Dictionary<string, HashSet<KeyValuePair<DateTime, string>>>();

        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            if (line.Contains("======"))
            {
                isBlockOpen = !isBlockOpen;

                if (line.StartsWith("===="))
                {
                    isBlockOpen = false;
                }

                if (!isBlockOpen)
                {
                    productNumber = string.Empty;
                }

                if (isBlockOpen)
                {
                    date = line[..23];
                }

                continue;
            }

            if (isBlockOpen)
            {
                if (LogsExpressions.IsStartOfProductInfo(line))
                {
                    productNumber = LogsExpressions.ProductNumber(line);
                }

                if (string.IsNullOrWhiteSpace(productNumber))
                {
                    continue;
                }

                var message = line.Replace(productNumber, "").Replace("\t", "").Trim();

                if (string.IsNullOrWhiteSpace(message))
                {
                    continue;
                }

                var pair = KeyValuePair.Create(DateTime.Parse(date).AddHours(-3).SetKindUtc(), char.ToUpper(message[0]) + message.Substring(1));
                if (!logs.ContainsKey(productNumber))
                {
                    logs.Add(productNumber, [pair]);
                }
                else
                {
                    logs[productNumber].Add(pair);
                }
            }
        }

        return logs;
    }

    private async Task<Dictionary<string, HashSet<KeyValuePair<DateTime, string>>>> GetLogsFromNewFileAsync(string filePath)
    {
        var isBlockOpen = false;
        var productNumber = string.Empty;
        var date = string.Empty;

        var logs = new Dictionary<string, HashSet<KeyValuePair<DateTime, string>>>();

        await foreach (var line in File.ReadLinesAsync(filePath))
        {
            if (line.Contains("======"))
            {
                isBlockOpen = !isBlockOpen;

                if (!isBlockOpen)
                {
                    productNumber = string.Empty;
                }

                continue;
            }

            if (!isBlockOpen)
            {
                if (DateTime.TryParse(line[..23], out var parseDate))
                {
                    date = parseDate.ToString();
                }
            }

            if (isBlockOpen)
            {
                if (line == "------------------------------")
                {
                    productNumber = string.Empty;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(productNumber))
                {
                    if (LogsExpressions.IsStartOfProductInfo(line))
                    {
                        productNumber = LogsExpressions.ProductNumber(line);
                    }
                }

                if (string.IsNullOrWhiteSpace(productNumber))
                {
                    continue;
                }

                var message = line.Replace(productNumber, "").Replace("\t", "").Trim();

                if (string.IsNullOrWhiteSpace(message))
                {
                    continue;
                }

                var pair = KeyValuePair.Create(DateTime.Parse(date).AddHours(-3).SetKindUtc(), char.ToUpper(message[0]) + message.Substring(1));
                if (!logs.ContainsKey(productNumber))
                {
                    logs.Add(productNumber, [pair]);
                }
                else
                {
                    logs[productNumber].Add(pair);
                }
            }
        }

        return logs;
    }

    private void AddHistoryEntries(IEnumerable<Product> products, ICollection<LogHistory> logs)
    {
        foreach (var log in logs)
        {
            var key = int.Parse(log.ProductNumber);
            var product = products.FirstOrDefault(x => x.Number.Value == key);
            if (product == null)
            {
                continue;
            }

            var historyEntries = new List<ProductHistoryEntry>();

            foreach (var item in log.Items)
            {
                if (product.Name.Value.Replace(" ", "") == item.Value.Replace(" ", ""))
                {
                    continue;
                }

                if (IsUselessLog(item.Value))
                {
                    continue;
                }

                var reason = GetReason(item, product);

                var entry = new ProductHistoryEntry(
                    reason.Code,
                    reason.OldValue?.ToString() ?? "",
                    reason.NewValue?.ToString() ?? "",
                    item.Date);

                historyEntries.Add(entry);
            }

            foreach (var entry in historyEntries)
            {
                product.AddHistoryEntry(entry.ChangeReasonCode, entry.OldValue, entry.NewValue, entry.CreatedDate);
            }
        }
    }

    private bool IsOldFile(string fileName)
    {
        var date = fileName.Substring(fileName.Length - 14, 10);

        if (DateTime.TryParse(date, out var result))
        {
            return result < DateTime.Parse("2025-05-07");
        }

        return false;
    }

    private bool IsUselessLog(string logMessage)
    {
        return logMessage.Contains("Изменен на клиенте") ||
            logMessage.Contains("Изменен на сервере") ||
            logMessage.Contains("Добавлено на клиент") ||
            logMessage.Contains("Добавлено на сервер") ||
            logMessage.Contains("------------------------------");
    }

    private LogHistoryItemReason GetReason(LogHistoryItem item, Product product)
    {
        var reason = new LogHistoryItemReason();

        if (item.Value.Contains("-->"))
        {
            var strings = item.Value.Split("-->");
            reason = ParseReason(strings[0].Trim(), strings[1].Trim());
        }
        else
        {
            reason = ParseReason(item.Value.Trim(), product);
        }

        return reason;
    }

    private LogHistoryItemReason ParseReason(string value, Product product)
    {
        var reason = new LogHistoryItemReason();

        if (value == "Добавлен в список заказа")
        {
            reason.Code = ProductChangeReasonCode.ChangedOrderStatus;
            reason.OldValue = false;
            reason.NewValue = true;

            return reason;
        }

        if (value == "Удалён из списка заказа")
        {
            reason.Code = ProductChangeReasonCode.ChangedOrderStatus;
            reason.OldValue = true;
            reason.NewValue = false;

            return reason;
        }

        reason.Code = ProductChangeReasonCode.ChangedName;
        reason.OldValue = value;
        reason.NewValue = product.Name;

        return reason;
    }

    private LogHistoryItemReason ParseReason(string left, string right)
    {
        if (TryParsePrice(left, right, out LogHistoryItemReason reason))
        {
            return reason;
        }

        if (TryParsePriceTagCode(left, right, out reason))
        {
            return reason;
        }

        reason = new()
        {
            Code = ProductChangeReasonCode.ChangedName,
            OldValue = left,
            NewValue = right
        };

        return reason;
    }

    private bool TryParsePrice(string left, string right, out LogHistoryItemReason result)
    {
        try
        {
            if (!(left + right).Contains("руб"))
            {
                result = null;
                return false;
            }

            var reason = new LogHistoryItemReason
            {
                Code = ProductChangeReasonCode.ChangedPrice,
                OldValue = decimal.Parse(LogsExpressions.ProducPrice(left)).ToString("0.00"),
                NewValue = decimal.Parse(LogsExpressions.ProducPrice(right)).ToString("0.00")
            };

            result = reason;
            return true;
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
    }

    private bool TryParsePriceTagCode(string left, string right, out LogHistoryItemReason result)
    {
        try
        {
            var reason = new LogHistoryItemReason
            {
                Code = ProductChangeReasonCode.ChangedPriceTagCode,
                OldValue = (int)Enum.Parse(typeof(ProductPriceTagCode), left),
                NewValue = (int)Enum.Parse(typeof(ProductPriceTagCode), right)
            };

            result = reason;
            return true;
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
    }
}
