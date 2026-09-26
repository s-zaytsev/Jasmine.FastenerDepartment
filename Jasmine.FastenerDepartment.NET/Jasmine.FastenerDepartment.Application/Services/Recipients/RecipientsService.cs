using Jasmine.FastenerDepartment.Domain.Common.Exceptions;
using Jasmine.FastenerDepartment.Domain.Recipients.Models;
using Jasmine.FastenerDepartment.Domain.Recipients.Repositories;
using Jasmine.FastenerDepartment.Domain.Recipients.Services;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;

namespace Jasmine.FastenerDepartment.Application.Services.Recipients;

internal class RecipientsService : IRecipientsService
{
    private readonly IRecipientsRepository _recipientsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecipientsService(
        IRecipientsRepository recipientsRepository,
        IUnitOfWork unitOfWork)
    {
        _recipientsRepository = recipientsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Recipient>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var recipients = await _recipientsRepository.GetAllAsync(cancellationToken);
        return recipients;
    }

    public async Task<Recipient> CreateAsync(ChangeRecipient model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var recipient = new Recipient(model.Name, model.Email);

        _recipientsRepository.Add(recipient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return recipient;
    }

    public async Task UpdateAsync(Guid id, ChangeRecipient model, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var recipient = await GetRecipientByIdASync(id, cancellationToken);
        recipient.ChangeName(model.Name);
        recipient.ChangeEmail(model.Email);

        _recipientsRepository.Update(recipient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task<Recipient> GetRecipientByIdASync(Guid id, CancellationToken cancellationToken)
    {
        var recipient = await _recipientsRepository.GetByIdAsync(id, cancellationToken);
        if (recipient == null)
        {
            throw new NotFoundException();
        }

        return recipient;
    }
}
