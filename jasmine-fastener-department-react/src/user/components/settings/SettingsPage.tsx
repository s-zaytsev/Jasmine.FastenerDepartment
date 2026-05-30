import Page from "../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import LanguageSettingsCard from "./LanguageSettingsCard.tsx";
import CompanySettingsCard from "./CompanySettingsCard.tsx";
import EmailSettingsCard from "./EmailSettingsCard.tsx";
import useSettingsPage from "./useSettingsPage.ts";
import FilledButton from "../../../shared/components/buttons/FilledButton.tsx";

const SettingsPage = () => {
    const {
        companySettings,
        emailSettings,
        language,
        changeLanguage,
        changeCompanySettings,
        changeEmailSettings,
        submit
    } = useSettingsPage();

    return (
        <Page>
            <Box className={'flex flex-col justify-center w-full items-center gap-[1rem]'}>
                <Box className={'flex gap-[1rem] w-[70%]'}>
                    <Box className={'w-full flex flex-col'}>
                        <CompanySettingsCard settings={companySettings} onChange={changeCompanySettings}/>
                        <LanguageSettingsCard currentLanguage={language} onChange={changeLanguage}/>
                    </Box>

                    <Box className={'w-full'}>
                        <EmailSettingsCard settings={emailSettings} onChange={changeEmailSettings}/>
                    </Box>
                </Box>
                <Box className={'flex justify-end w-[70%]'}>
                    <FilledButton variant={'contained'} onClick={submit}>Сохранить</FilledButton>
                </Box>
            </Box>
        </Page>
    )
}

export default SettingsPage;