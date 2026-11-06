import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {type DownloadDocument, type ExportPageState, type SendDocument} from "../../models/exportModels.ts";
import {
    clearSelectedTemplate,
    downloadDocument,
    getDocuments,
    selectTemplate,
    sendDocument,
    setDownloadingTemplateId, setSendingTemplateId
} from "../../slices/ExportSlice.ts";
import {useEffect, useState} from "react";
import type {TemplateFormatCode} from "../../models/templateModels.ts";

const useExportPage = () => {

    const state = useAppSelector<ExportPageState>(
        (state) => state.export
    );

    const dispatch = useAppDispatch();

    const [isSendDialogOpen, setIsSendDialogOpen] = useState(false);

    const handleDownload = async (templateId: string, formatCode: TemplateFormatCode) => {
        try {
            dispatch(setDownloadingTemplateId({id: templateId}));

            const model: DownloadDocument = {
                templateId: templateId,
                formatCode: formatCode,
            }

            await dispatch(downloadDocument(model));
        } finally {
            dispatch(setDownloadingTemplateId({id: undefined}));
        }
    }

    const handleOpenSendDialog = (templateId: string, formatCode: TemplateFormatCode) => {
        dispatch(selectTemplate({id: templateId, code: formatCode}));
        setIsSendDialogOpen(true);
    }

    const handleCloseSendDialog = () => {
        dispatch(clearSelectedTemplate());
        setIsSendDialogOpen(false);
    }

    const handleSend = async (model: SendDocument) => {
        try {
            dispatch(setSendingTemplateId({id: model.templateId}));
            await dispatch(sendDocument(model));
        } finally {
            dispatch(setSendingTemplateId({id: undefined}));
        }
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
        sendingTemplateId: state.sendingTemplateId,
        downloadingTemplateId: state.downloadingTemplateId
    };
}

export default useExportPage;