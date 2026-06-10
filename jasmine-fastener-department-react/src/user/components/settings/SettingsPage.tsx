import Page from "../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import LanguageSettingsCard from "./LanguageSettingsCard.tsx";
import CompanySettingsCard from "./CompanySettingsCard.tsx";
import EmailSettingsCard from "./EmailSettingsCard.tsx";
import useSettingsPage from "./useSettingsPage.ts";

const SettingsPage = () => {
    const {
        companySettings,
        emailSettings,
        language,
        handleChangeLanguage,
        handleChangeCompanySettings,
        handleChangeEmailSettings,
        handleSubmit
    } = useSettingsPage();

    return (
        <Page
            title={'Настройки'}
            description={'Изменение настроек приложения'}
            button={{
                label: 'Сохранить',
                onClick: handleSubmit
            }}>
            <Box className={'flex flex-col justify-center w-full items-center gap-[1rem]'}>
                <Box className={'flex gap-[1rem] w-[70%]'}>
                    <Box className={'w-full flex flex-col'}>
                        <CompanySettingsCard settings={companySettings} onChange={handleChangeCompanySettings}/>
                        <LanguageSettingsCard currentLanguage={language} onChange={handleChangeLanguage}/>
                    </Box>

                    <Box className={'w-full'}>
                        <EmailSettingsCard settings={emailSettings} onChange={handleChangeEmailSettings}/>
                    </Box>
                </Box>
            </Box>
        </Page>
    )
}

export default SettingsPage;