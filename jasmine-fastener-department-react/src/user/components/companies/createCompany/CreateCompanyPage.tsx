import Page from "../../../../shared/components/layout/Page.tsx";
import {Box} from "@mui/material";
import CompanyForm from "../shared/CompanyForm.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import useCreateCompanyPage from "./useCreateCompanyPage.ts";

const CreateCompanyPage = () => {
    const {
        model,
        loading,
        handleSubmit
    } = useCreateCompanyPage();

    return (
        <Page
            title={'Создание компании'}
            description={'Заполнение характеристик новой компании'}
            button={{
                label: 'Создать',
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

export default CreateCompanyPage;