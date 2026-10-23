import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {type DownloadDocument, type ExportPageState, type SendDocument} from "../../models/exportModels.ts";
import {
    clearSelectedTemplate,
    downloadDocument,
    getDocuments,
    selectTemplate,
    sendDocument
} from "../../slices/ExportSlice.ts";
import {useEffect, useState} from "react";
import type {TemplateFormatCode} from "../../models/templateModels.ts";

const useExportPage = () => {

    const state = useAppSelector<ExportPageState>(
        (state) => state.export
    );

    const dispatch = useAppDispatch();

    const [isSendDialogOpen, setIsSendDialogOpen] = useState(false);

    const handleDownload = (templateId: string, formatCode: TemplateFormatCode) => {
        const model: DownloadDocument = {
            templateId: templateId,
            formatCode: formatCode,
        }

        dispatch(downloadDocument(model));
    }

    const handleOpenSendDialog = (templateId: string, formatCode: TemplateFormatCode) => {
        dispatch(selectTemplate({id: templateId, code: formatCode}));
        setIsSendDialogOpen(true);
    }

    const handleCloseSendDialog = () => {
        dispatch(clearSelectedTemplate());
        setIsSendDialogOpen(false);
    }

    const handleSend = (model: SendDocument) => {
        dispatch(sendDocument(model));
    }

    useEffect(() => {
        dispatch(getDocuments());
    }, [dispatch]);

    return {
        templates: state.templates,
        handleDownload,
        handleOpenSendDialog,
        handleSend,
        handleCloseSendDialog,
        loading: state.loading,
        isSendDialogOpen,
        selectedTemplate: state.selectedTemplate,
        selectedFormatCode: state.selectedFormatCode,
    };
}

export default useExportPage;