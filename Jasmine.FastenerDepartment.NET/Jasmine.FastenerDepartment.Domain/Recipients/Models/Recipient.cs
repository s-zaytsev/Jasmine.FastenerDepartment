using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Recipients.Models;

/// <summary>
/// Recipient.
/// </summary>
public class Recipient : AggregateRootBase<Guid>
{
    /// <summary>
    /// Recipient name.
    /// </summary>
    public Name Name { get; private set; }

    /// <summary>
    /// Recipient email.
    /// </summary>
    public Email Email { get; private set; }

    private Recipient() { }

    /// <summary>
    /// Creates a recipient.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="email">Email.</param>
    public Recipient(string name, string email)
    {
        Name = new(name);
        Email = new(email);
    }

    /// <summary>
    /// Changes a recipient name.
    /// </summary>
    /// <param name="name">Name.</param>
    public void ChangeName(string name)
    {
        Name = new(name);
    }

    /// <summary>
    /// Changes a recipient email.
    /// </summary>
    /// <param name="email">Email.</param>
    public void ChangeEmail(string email)
    {
        Email = new(email);
    }
}
