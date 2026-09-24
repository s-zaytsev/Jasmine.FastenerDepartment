import Page from "../../../shared/components/layout/Page.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import useTemplatesPage from "./useTemplatesPage.ts";
import TemplatesGrid from "./TemplatesGrid.tsx";

const TemplatesPage = () => {
    const {
        templates,
        handleNavigateToCreate,
        handleNavigateToChange,
        loading,
    } = useTemplatesPage();

    return (
        <Page
            title={'Шаблоны'}
            description={'Список доступных шаблонов'}
            button={{
                label: 'Создать',
                onClick: handleNavigateToCreate
            }}
        >
            {loading && <Loader/>}

            {!loading &&
                <TemplatesGrid
                    templates={templates}
                    onNavigateToChange={handleNavigateToChange}
                />
            }
        </Page>
    );
}

export default TemplatesPage;