namespace Jasmine.FastenerDepartment.ConsoleApplication.Services;

public interface IJsonProductService
{
    Task ActualizeProductsFromJsonFileAsync(string filePath, string logsDirectoryPath);
}
