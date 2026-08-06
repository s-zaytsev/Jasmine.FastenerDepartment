using Jasmine.FastenerDepartment.Domain.Common.Models;

namespace Jasmine.FastenerDepartment.Domain.Companies.Models;

/// <summary>
/// Company.
/// </summary>
public class Company : AggregateRootBase<Guid>
{
    /// <summary>
    /// Title.
    /// </summary>
    public Name Title { get; private set; }

    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Middle name.
    /// </summary>
    public string MiddleName { get; private set; }

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// City.
    /// </summary>
    public string City { get; private set; }

    /// <summary>
    /// Street.
    /// </summary>
    public string Street { get; private set; }

    /// <summary>
    /// Building number.
    /// </summary>
    public string BuildingNumber { get; private set; }

    /// <summary>
    /// Individual identification number (INN). 
    /// </summary>
    public Inn Inn { get; private set; }

    /// <summary>
    /// Company type code.
    /// </summary>
    public CompanyTypeCode TypeCode { get; private set; }

    /// <summary>
    /// Company type.
    /// </summary>
    public CompanyType Type { get; private set; }

    /// <summary>
    /// Phone number.
    /// </summary>
    public PhoneNumber PhoneNumber { get; private set; }

    /// <summary>
    /// Email.
    /// </summary>
    public Email Email { get; private set; }

    private Company() { }

    /// <summary>
    /// Creates company.
    /// </summary>
    public Company(
        string title,
        string firstName,
        string middleName,
        string lastName,
        string email,
        string city,
        string street,
        string buildingNumber,
        string inn,
        string phoneNumber)
    {
        Title = new(title);
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = new(email);
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
        Inn = new(inn);
        PhoneNumber = new(phoneNumber);
        TypeCode = CompanyTypeCode.IndividualEntrepreneur;
    }

    /// <summary>
    /// Changes title.
    /// </summary>
    /// <param name="title">Title.</param>
    public void ChangeTitle(string title)
    {
        Title = new(title);
    }

    /// <summary>
    /// Changes first name.
    /// </summary>
    /// <param name="firstName">First name.</param>
    public void ChangeFirstName(string firstName) 
    {
        FirstName = firstName;
    }

    /// <summary>
    /// Changes middle name.
    /// </summary>
    /// <param name="middleName">Middle name.</param>
    public void ChangeMiddleName(string middleName) 
    {
        MiddleName = middleName;
    }

    /// <summary>
    /// Changes last name.
    /// </summary>
    /// <param name="lastName">Last name.</param>
    public void ChangeLastName(string lastName) 
    {
        LastName = lastName;
    }

    /// <summary>
    /// Changes email.
    /// </summary>
    /// <param name="email">Email.</param>
    public void ChangeEmail(string email) 
    {
        Email = new(email);
    }

    /// <summary>
    /// Changes city.
    /// </summary>
    /// <param name="city"></param>
    public void ChangeCity(string city) 
    {
        City = city;
    }

    /// <summary>
    /// Changes street.
    /// </summary>
    /// <param name="street">Street.</param>
    public void ChangeStreet(string street) 
    {
        Street = street;
    }

    /// <summary>
    /// Changes building number.
    /// </summary>
    /// <param name="buildingNumber"></param>
    public void ChangeBuildingNumber(string buildingNumber) 
    {
        BuildingNumber = buildingNumber;
    }

    /// <summary>
    /// Changes INN.
    /// </summary>
    /// <param name="inn">INN.</param>
    public void ChangeInn(string inn) 
    {
        Inn = new(inn);
    }

    /// <summary>
    /// Changes phone number.
    /// </summary>
    /// <param name="phoneNumber">Phone number.</param>
    public void ChangePhoneNumber(string phoneNumber) 
    {
        PhoneNumber = new(phoneNumber);
    }
}
