import Page from "../../../shared/components/layout/Page.tsx";
import CompaniesGrid from "./CompaniesGrid.tsx";
import useCompaniesPage from "./useCompaniesPage.ts";
import Loader from "../../../shared/components/Loader.tsx";

const CompaniesPage = () => {
    const {
        companies,
        loading,
        handleCreateCompany,
        handleChangeCompany
    } = useCompaniesPage();

    return(
        <Page
            title={'Компании'}
            description={'Список доступных компаний'}
            button={{
                label: 'Создать',
                onClick: handleCreateCompany
            }}
        >
            {loading && <Loader />}

            {!loading &&
                <CompaniesGrid
                    companies={companies}
                    onEdit={handleChangeCompany}/>
            }
        </Page>
    );
}

export default CompaniesPage;