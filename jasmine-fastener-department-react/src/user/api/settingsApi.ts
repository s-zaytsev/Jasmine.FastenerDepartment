import api from "../../core/api.ts";
import type {
    ChangeCompanySettings,
    ChangeEmailSettings,
    CompanySettings,
    EmailSettings
} from "../models/settingsModels.ts";

class SettingsApi {
    getCompanySettings(): Promise<CompanySettings> {
        return api.get<Promise<CompanySettings>>(`/settings/company`)
            .then(x => x.data);
    }

    changeCompanySettings(model: ChangeCompanySettings): Promise<void> {
        return api.put<Promise<void>>(`/settings/company`, model)
            .then(x => x.data);
    }

    getEmailSettings(): Promise<EmailSettings> {
        return api.get<Promise<EmailSettings>>(`/settings/email`)
            .then(x => x.data);
    }

    changeEmailSettings(model: ChangeEmailSettings): Promise<void> {
        return api.put<Promise<void>>(`/settings/email`, model)
            .then(x => x.data);
    }
}

export default new SettingsApi();