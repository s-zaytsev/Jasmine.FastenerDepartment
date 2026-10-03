using Jasmine.FastenerDepartment.Domain.Settings.Models.Emails;
using Jasmine.FastenerDepartment.Messaging.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Jasmine.FastenerDepartment.Messaging.MessageProviders;

internal class EmailsProvider : IMessageTypeProvider
{
    private readonly EmailSettings _settings;

    public EmailsProvider(IOptionsSnapshot<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendAsync(SendModel model, CancellationToken cancellationToken)
    {
        using var client = CreateSmtpClient();
        var message = CreateMessage(model);

        await client.SendMailAsync(message, cancellationToken);
    }

    private SmtpClient CreateSmtpClient()
    {
        var client = new SmtpClient(_settings.SmtpUrl)
        {
            Port = _settings.SmtpPort,
            Credentials = new NetworkCredential(_settings.UserName, _settings.Password),
            EnableSsl = true
        };

        return client;
    }

    private MailMessage CreateMessage(SendModel model)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_settings.UserName, _settings.DisplayName),
            Subject = model.Title,
            Body = model.Content,
            IsBodyHtml = true
        };

        mailMessage.To.Add(model.RecipientContact);

        foreach (var file in model.Attachments)
        {
            mailMessage.Attachments.Add(new(file.Content, file.Name, file.ContentType));
        }

        return mailMessage;
    }
}
