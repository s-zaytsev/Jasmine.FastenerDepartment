using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Companies.Models;
using Jasmine.FastenerDepartment.Domain.Companies.Repositories;
using Jasmine.FastenerDepartment.Domain.Companies.Services;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

namespace Jasmine.FastenerDepartment.Application.Services.Companies;

internal class CompaniesService : ICompaniesService
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompaniesService(
        ICompaniesRepository companiesRepository,
        IUnitOfWork unitOfWork)
    {
        _companiesRepository = companiesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken = default)
    {
        var companies = await _companiesRepository.GetAllAsync(cancellationToken);
        return companies;
    }

    public async Task<Company> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var company = await GetCompanyByIdAsync(id, cancellationToken);
        return company;
    }

    public async Task<Company> CreateCompanyAsync(ChangeCompany model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var company = new Company(
            model.Title,
            model.FirstName,
            model.MiddleName,
            model.LastName,
            model.Email,
            model.City,
            model.Street,
            model.BuildingNumber,
            model.Inn,
            model.PhoneNumber);

        _companiesRepository.Add(company);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return company;
    }

    public async Task UpdateCompanyAsync(Guid id, ChangeCompany model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var company = await GetCompanyByIdAsync(id, cancellationToken);
        company.ChangeTitle(model.Title);
        company.ChangeFirstName(model.FirstName);
        company.ChangeMiddleName(model.MiddleName);
        company.ChangeLastName(model.LastName);
        company.ChangeEmail(model.Email);
        company.ChangeCity(model.City);
        company.ChangeStreet(model.Street);
        company.ChangeBuildingNumber(model.BuildingNumber);
        company.ChangeInn(model.Inn);
        company.ChangePhoneNumber(model.PhoneNumber);

        _companiesRepository.Update(company);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<Company> GetCompanyByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var company = await _companiesRepository.GetByIdAsync(id, cancellationToken);
        if (company == null) throw new NotFoundException();
        return company;
    }
}
