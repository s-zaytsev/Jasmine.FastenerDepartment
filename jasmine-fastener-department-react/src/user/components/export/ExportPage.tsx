import Page from "../../../shared/components/layout/Page.tsx";
import useExportPage from "./useExportPage.ts";
import ExportDocumentGrid from "./ExportDocumentGrid.tsx";
import SendExportDocumentDialog from "./dialogs/SendExportDocumentDialog.tsx";

const ExportPage = () => {

    const {
        templates,
        selectedTemplate,
        selectedFormatCode,
        sendingTemplateId,
        downloadingTemplateId,
        isSendDialogOpen,
        handleDownload,
        handleOpenSendDialog,
        handleCloseSendDialog,
        handleSend
    } = useExportPage();

    return (
        <Page
            title={'Экспорт документов'}
            description={'Выбор необходимого формата для экспорта документа'}
        >
            <ExportDocumentGrid
                templates={templates}
                sendingTemplateId={sendingTemplateId}
                downloadingTemplateId={downloadingTemplateId}
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