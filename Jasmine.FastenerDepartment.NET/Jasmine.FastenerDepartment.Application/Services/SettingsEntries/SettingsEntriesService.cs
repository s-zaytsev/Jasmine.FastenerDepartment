using Jasmine.FastenerDepartment.Domain.Settings.Models.Company;
using Jasmine.FastenerDepartment.Domain.Settings.Models.Emails;
using Jasmine.FastenerDepartment.Domain.Settings.Models.Keys;
using Jasmine.FastenerDepartment.Domain.Settings.Repositories;
using Jasmine.FastenerDepartment.Domain.Settings.Services;
using Jasmine.FastenerDepartment.EF.Repositories.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace Jasmine.FastenerDepartment.Application.Services.SettingsEntries;

internal class SettingsEntriesService : ISettingsEntriesService
{
    private readonly ISettingsEntriesRepository _settingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public SettingsEntriesService(
        ISettingsEntriesRepository settingRepository,
        IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        _settingRepository = settingRepository;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<CompanySettings> GetCompanySettingsAsync()
    {
        var companySettings = await _settingRepository.GetBySectionNameAsync(CompanySettingsKeys.SECTION_NAME);
        return new()
        {
            Title = companySettings[CompanySettingsKeys.TITLE_KEY].Value,
            SubTitle = companySettings[CompanySettingsKeys.SUBTITLE_KEY].Value,
        };
    }

    public async Task<EmailSettings> GetEmailSettingsAsync()
    {
        var emailSettings = await _settingRepository.GetBySectionNameAsync(EmailSettingsKeys.SECTION_NAME);
        
        var port = 0;
        if (int.TryParse(emailSettings[EmailSettingsKeys.SMTP_PORT_KEY].Value, out var parsedPort))
        {
            port = parsedPort;
        }

        return new()
        {
            SmtpUrl = emailSettings[EmailSettingsKeys.SMTP_URL_KEY].Value,
            SmtpPort = port,
            UserName = emailSettings[EmailSettingsKeys.USER_NAME_KEY].Value,
            Password = emailSettings[EmailSettingsKeys.PASSWORD_KEY].Value,
            DisplayName = emailSettings[EmailSettingsKeys.DISPLAY_NAME_KEY].Value
        };
    }

    public async Task ChangeCompanySettingsAsync(ChangeCompanySettings settings)
    {
        var companySettings = await _settingRepository.GetBySectionNameAsync(CompanySettingsKeys.SECTION_NAME);

        companySettings[CompanySettingsKeys.TITLE_KEY].ChangeValue(settings.Title);
        companySettings[CompanySettingsKeys.SUBTITLE_KEY].ChangeValue(settings.SubTitle);

        foreach (var pair in companySettings)
        {
            _settingRepository.Change(pair.Value);
        }

        await _unitOfWork.SaveChangesAsync();
        ReloadConfiguration();
    }

    public async Task ChangeEmailSettingsAsync(ChangeEmailSettings settings)
    {
        var emailSettings = await _settingRepository.GetBySectionNameAsync(EmailSettingsKeys.SECTION_NAME);

        emailSettings[EmailSettingsKeys.SMTP_URL_KEY].ChangeValue(settings.SmtpUrl);
        emailSettings[EmailSettingsKeys.SMTP_PORT_KEY].ChangeValue(settings.SmtpPort.ToString());
        emailSettings[EmailSettingsKeys.USER_NAME_KEY].ChangeValue(settings.UserName);
        emailSettings[EmailSettingsKeys.PASSWORD_KEY].ChangeValue(settings.Password);
        emailSettings[EmailSettingsKeys.DISPLAY_NAME_KEY].ChangeValue(settings.DisplayName);

        foreach (var pair in emailSettings)
        {
            _settingRepository.Change(pair.Value);
        }

        await _unitOfWork.SaveChangesAsync();
        ReloadConfiguration();
    }

    private void ReloadConfiguration()
    {
        if (_configuration is IConfigurationRoot root)
            root.Reload();
    }
}
