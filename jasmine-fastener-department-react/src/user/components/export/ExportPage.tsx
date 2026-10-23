import Page from "../../../shared/components/layout/Page.tsx";
import Loader from "../../../shared/components/Loader.tsx";
import useExportPage from "./useExportPage.ts";
import ExportDocumentGrid from "./ExportDocumentGrid.tsx";
import SendExportDocumentDialog from "./dialogs/SendExportDocumentDialog.tsx";

const ExportPage = () => {

    const {
        templates,
        selectedTemplate,
        selectedFormatCode,
        isSendDialogOpen,
        handleDownload,
        handleOpenSendDialog,
        handleCloseSendDialog,
        handleSend,
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
                onSend={handleOpenSendDialog}
            />

            {selectedTemplate && selectedFormatCode && <SendExportDocumentDialog
                template={selectedTemplate!}
                formatCode={selectedFormatCode}
                open={isSendDialogOpen}
                onClose={handleCloseSendDialog}
                onSubmit={handleSend}
            />}
        </Page>
    )
}

export default ExportPage;