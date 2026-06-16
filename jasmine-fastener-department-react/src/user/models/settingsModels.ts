import type {StateBase} from "../../shared/models/models.ts";

export interface SettingsPageState extends StateBase {
    companySettings: ChangeCompanySettings;
    emailSettings: ChangeEmailSettings;
}

export interface CompanySettings {
    title: string;
    subTitle: string;
}

export interface ChangeCompanySettings {
    title: string;
    subTitle: string;
}

export interface EmailSettings {
    smtpUrl: string;
    smtpPort: number;
    userName: string;
    password: string;
    displayName: string;
}

export interface ChangeEmailSettings {
    smtpUrl: string;
    smtpPort: number;
    userName: string;
    password: string;
    displayName: string;
}

export interface SettingsForm {
    companySettings: ChangeCompanySettings;
    emailSettings: EmailSettings;
}