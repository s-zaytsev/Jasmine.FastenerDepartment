import Page from "../../../shared/components/layout/Page.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import useExportPage from "./useExportPage.ts";
import ExportDocumentGrid from "./ExportDocumentGrid.tsx";

const ExportPage = () => {

    const {
        templates,
        handleDownload,
        loading
    } = useExportPage();

    if (loading) {
        return <Loader/>;
    }

    return (
        <Page
            title={'Экспорт документов'}
            description={'Выбор необходимого формата для экспорта документа'}
        >
            <ExportDocumentGrid
                templates={templates}
                onDownload={handleDownload}
            />
        </Page>
    )
}

export default ExportPage;