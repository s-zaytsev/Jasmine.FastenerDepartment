import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import type {ChangeCompanySettings, ChangeEmailSettings, SettingsPageState} from "../../models/settingsModels.ts";
import {useEffect, useState} from "react";
import {
    changeCompanySettings,
    changeEmailSettings,
    getCompanySettings,
    getEmailSettings,
    updateCompanySettings,
    updateEmailSettings
} from "../../slices/SettingsSlice.ts";
import {LanguageCode} from "../../../shared/models/models.ts";
import languageService from "../../../shared/services/languageService.ts";

const useSettingsPage = () => {
    const state = useAppSelector<SettingsPageState>(
        (state) => state.settings
    );

    const [languageCode, setLanguageCode] = useState<LanguageCode>(LanguageCode.en);
    const dispatch = useAppDispatch();

    useEffect(() => {
        setLanguageCode(languageService.getLanguage());

        dispatch(getCompanySettings());
        dispatch(getEmailSettings());
    }, []);

    const handleChangeLanguage = (code: LanguageCode) => {
        languageService.setLanguage(code);
        setLanguageCode(code);
    }

    const handleChangeCompanySettings = (data: ChangeCompanySettings) => {
        dispatch(updateCompanySettings(data));
    }

    const handleChangeEmailSettings = (data: ChangeEmailSettings) => {
        dispatch(updateEmailSettings(data));
    }

    const handleSubmit = () => {
        dispatch(changeCompanySettings(state.companySettings));
        dispatch(changeEmailSettings(state.emailSettings));
    }

    return {
        companySettings: state.companySettings,
        emailSettings: state.emailSettings,
        language: languageCode,
        changeLanguage: handleChangeLanguage,
        changeCompanySettings: handleChangeCompanySettings,
        changeEmailSettings: handleChangeEmailSettings,
        submit: handleSubmit
    }
}

export default useSettingsPage;