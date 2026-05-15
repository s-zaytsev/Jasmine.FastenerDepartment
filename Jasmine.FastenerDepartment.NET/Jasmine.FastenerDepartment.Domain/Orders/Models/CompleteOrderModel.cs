namespace Jasmine.FastenerDepartment.Domain.Products.Models;

/// <summary>
/// Complete order model.
/// </summary>
public class CompleteOrderModel
{
    /// <summary>
    /// Comment.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// List of products.
    /// </summary>
    public ICollection<CompleteOrderProduct> Products { get; set; }
}
