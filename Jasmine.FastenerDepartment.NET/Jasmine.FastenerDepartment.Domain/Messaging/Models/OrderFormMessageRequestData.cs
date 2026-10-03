using Jasmine.FastenerDepartment.Domain.Common.Models;
using Jasmine.FastenerDepartment.Domain.Orders.Models;

namespace Jasmine.FastenerDepartment.Domain.Messaging.Models;

/// <summary>
/// Order form message request data.
/// </summary>
public class OrderFormMessageRequestData : MessageRequestData
{
    /// <summary>
    /// Order.
    /// </summary>
    public Order Order { get; init; }

    /// <summary>
    /// Whether the message has attachments.
    /// </summary>
    public bool HasAttachments { get; init; }

    /// <summary>
    /// Language code.
    /// </summary>
    public LanguageCode? LanguageCode { get; set; }

    /// <summary>
    /// Creates order form message request data.
    /// </summary>
    public OrderFormMessageRequestData()
    { }

    /// <summary>
    /// Creates order form message request data.
    /// </summary>
    /// <param name="order">Order.</param>
    /// <param name="hasAttachments">Whether the message has attachments.</param>
    /// <param name="languageCode">Language code.</param>
    public OrderFormMessageRequestData(
        Order order,
        bool hasAttachments,
        LanguageCode? languageCode)
    {
        Order = order;
        HasAttachments = hasAttachments;
        LanguageCode = languageCode;
    }
}
