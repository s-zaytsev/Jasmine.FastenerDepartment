import Page from "../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import LanguageSettingsCard from "./LanguageSettingsCard.tsx";
import CompanySettingsCard from "./CompanySettingsCard.tsx";
import EmailSettingsCard from "./EmailSettingsCard.tsx";
import useSettingsPage from "./useSettingsPage.ts";
import {FormProvider} from "react-hook-form";

const SettingsPage = () => {
    const {
        forms,
        language,
        handleChangeLanguage,
        handleSubmit
    } = useSettingsPage();

    return (
        <Page
            title={'Настройки'}
            description={'Изменение настроек приложения'}
            button={{
                label: 'Сохранить',
                type: 'submit',
                formId: 'settings-edit-form'
            }}
        >
            <Box className={'flex flex-col justify-center w-full items-center gap-[1rem]'}>
                <FormProvider {...forms}>
                    <form id={'settings-edit-form'} className={'w-[60%]'} onSubmit={forms.handleSubmit(handleSubmit)}>
                        <Box className={'flex gap-[1rem] w-full'}>
                            <Box className={'w-full flex flex-col'}>
                                <CompanySettingsCard/>
                                <LanguageSettingsCard currentLanguage={language} onChange={handleChangeLanguage}/>
                            </Box>

                            <Box className={'w-full'}>
                                <EmailSettingsCard/>
                            </Box>
                        </Box>

                        <button type="submit" style={{display: 'none'}}/>
                    </form>
                </FormProvider>
            </Box>
        </Page>
    )
}

export default SettingsPage;