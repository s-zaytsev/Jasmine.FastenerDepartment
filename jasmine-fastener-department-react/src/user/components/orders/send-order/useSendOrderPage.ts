import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {DownloadOrder, SendOrder, SendOrderState} from "../../../models/orderModels.ts";
import {useCallback, useEffect, useState} from "react";
import {
    getCompanies,
    getRecipients,
    getTemplates,
    renderOrderForm,
    selectCompany,
    selectFormat,
    selectRecipient,
    selectTemplate,
    setExtraFiles
} from "../../../slices/SendOrderSlice.ts";
import {TemplateFormatCode, TemplateTypeCode} from "../../../models/templateModels.ts";
import {useNavigate, useParams} from "react-router-dom";
import type {FileModel} from "../../../../shared/models/models.ts";
import {downloadOrder, sendOrder} from "../../../slices/OrdersSlice.ts";

const useSendOrderPage = () => {
    const state = useAppSelector<SendOrderState>(
        (state) => state.sendOrder
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const params = useParams();

    const [files, setFiles] = useState<{ id: string, file: File }[]>([]);

    const handleSelectTemplate = useCallback((id: string) => {
        dispatch(selectTemplate({id}));
    }, [dispatch]);

    const handleSelectCompany = useCallback((id: string) => {
        dispatch(selectCompany({id}));
    }, [dispatch]);

    const handleSelectRecipient = useCallback((id: string) => {
        dispatch(selectRecipient({id}));
    }, [dispatch]);

    const handleSelectFormat = useCallback((code: TemplateFormatCode) => {
        dispatch(selectFormat({code}));
    }, [dispatch]);

    const handleChangeExtraFiles = useCallback((files: File[]) => {
        const entries = files.map(file => (
            {id: crypto.randomUUID(), file}
        ));

        setFiles(() => entries);

        const extraFiles: FileModel[] = entries.map(({id, file}) => ({
            id,
            name: file.name,
            size: file.size,
            type: file.type,
            lastModified: file.lastModified,
        }));

        dispatch(setExtraFiles({files: extraFiles}));
    }, [dispatch])

    const handleSubmit = useCallback(() => {
        if (!state.selectedRecipient?.email ||
            !state.selectedTemplate?.id ||
            state.selectedFormats.length === 0)
            return;

        const model: SendOrder = {
            recipientContact: state.selectedRecipient.email,
            companyId: state.selectedCompany?.id,
            templateId: state.selectedTemplate?.id,
            formats: state.selectedFormats,
            attachments: files.map(x => x.file)
        };

        dispatch(sendOrder({id: params.id, model: model}));
    }, [
        dispatch,
        files,
        params.id,
        state.selectedCompany?.id,
        state.selectedFormats,
        state.selectedRecipient?.email,
        state.selectedTemplate?.id]);

    const handleDownload = useCallback(() => {
        if (!state.selectedTemplate?.id ||
            state.selectedFormats.length === 0)
            return;

        const model: DownloadOrder = {
            companyId: state.selectedCompany?.id,
            templateId: state.selectedTemplate?.id,
            formats: state.selectedFormats,
            attachments: files.map(x => x.file)
        };

        dispatch(downloadOrder({id: params.id, model: model}));
    }, [
        dispatch,
        files,
        params.id,
        state.selectedCompany?.id,
        state.selectedFormats,
        state.selectedTemplate?.id]);

    useEffect(() => {
        if (!params.id)
            navigate("/orders");

        dispatch(getCompanies())
        dispatch(getTemplates({typeCodes: [TemplateTypeCode.orderForm]}));
        dispatch(getRecipients());
    }, [dispatch]);

    useEffect(() => {
        if (!state.selectedTemplate?.id)
            return;

        dispatch(renderOrderForm({
            templateId: state.selectedTemplate?.id,
            orderId: params.id!,
            formatCode: TemplateFormatCode.html,
            companyId: state.selectedCompany?.id
        }))
    }, [dispatch, params.id, state.selectedCompany?.id, state.selectedTemplate?.id]);

    return {
        companies: state.companies,
        templates: state.templates,
        recipients: state.recipients,
        preview: state.preview,
        selectedTemplate: state.selectedTemplate,
        selectedCompany: state.selectedCompany,
        selectedRecipient: state.selectedRecipient,
        selectedFormats: state.selectedFormats,
        handleSelectTemplate,
        handleSelectCompany,
        handleSelectRecipient,
        handleSelectFormat,
        handleChangeExtraFiles,
        handleSubmit,
        handleDownload
    }
}

export default useSendOrderPage;