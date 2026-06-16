import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import type {
    ChangeCompanySettings,
    ChangeEmailSettings,
    SettingsForm,
    SettingsPageState
} from "../../models/settingsModels.ts";
import {useCallback, useEffect, useState} from "react";
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
import {useForm} from "react-hook-form";

const useSettingsPage = () => {
    const state = useAppSelector<SettingsPageState>(
        (state) => state.settings
    );

    const [languageCode, setLanguageCode] = useState<LanguageCode>(LanguageCode.en);
    const dispatch = useAppDispatch();

    const forms = useForm({
        values: {
            companySettings: state.companySettings,
            emailSettings: state.emailSettings
        }
    });

    const handleChangeLanguage = useCallback((code: LanguageCode) => {
        languageService.setLanguage(code);
        setLanguageCode(code);
    }, []);

    const handleChangeCompanySettings = useCallback((data: ChangeCompanySettings) => {
        dispatch(updateCompanySettings(data));
    }, [dispatch]);

    const handleChangeEmailSettings = useCallback((data: ChangeEmailSettings) => {
        dispatch(updateEmailSettings(data));
    }, [dispatch]);

    const handleSubmit = useCallback((form: SettingsForm) => {
        dispatch(changeCompanySettings(form.companySettings));
        dispatch(changeEmailSettings(form.emailSettings));
    }, [dispatch]);

    useEffect(() => {
        setLanguageCode(languageService.getLanguage());

        dispatch(getCompanySettings());
        dispatch(getEmailSettings());
    }, [dispatch]);

    return {
        forms: forms,
        companySettings: state.companySettings,
        emailSettings: state.emailSettings,
        language: languageCode,
        handleChangeLanguage,
        handleChangeCompanySettings,
        handleChangeEmailSettings,
        handleSubmit
    }
}

export default useSettingsPage;