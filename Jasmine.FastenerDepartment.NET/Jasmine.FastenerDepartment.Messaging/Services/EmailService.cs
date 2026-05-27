using Jasmine.FastenerDepartment.Domain.Settings.Models.Emails;
using Jasmine.FastenerDepartment.Messaging.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Jasmine.FastenerDepartment.Messaging.Services;

internal class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptionsSnapshot<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<string> SendAsync(MessageRequest request)
    {
        using var client = CreateSmtpClient();
        var message = CreateMessage(request);

 //       await client.SendMailAsync(message);

        return request.RecipientContact;
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

    private MailMessage CreateMessage(MessageRequest request)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_settings.UserName, _settings.DisplayName),
            Subject = request.Title,
            Body = request.Content,
            IsBodyHtml = true
        };

        mailMessage.To.Add(request.RecipientContact);

        return mailMessage;
    }
}
