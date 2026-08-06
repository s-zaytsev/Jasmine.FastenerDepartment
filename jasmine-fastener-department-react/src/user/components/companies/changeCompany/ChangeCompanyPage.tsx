import useChangeCompanyPage from "./useChangeCompanyPage.ts";
import Page from "../../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import CompanyForm from "../shared/CompanyForm.tsx";
import Loader from "../../../../shared/components/Loader.tsx";

const ChangeCompanyPage = () => {
    const {
        model,
        loading,
        handleSubmit
    } = useChangeCompanyPage();

    return (
        <Page
            title={'Редактирование компании'}
            description={'Изменение характеристик существующей компании'}
            button={{
                label: 'Сохранить',
                type: "submit",
                formId: 'company-edit-form'
            }}
        >
            {loading && <Loader />}
            {!loading && <Box className={'flex justify-center w-[50%]'}>
                <CompanyForm model={model} onSubmit={handleSubmit}/>
            </Box>}
        </Page>
    );
}

export default ChangeCompanyPage;