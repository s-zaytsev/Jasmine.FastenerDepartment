import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {type ExportPageState} from "../../models/exportModels.ts";
import {downloadProductCatalog, getDocuments} from "../../slices/ExportSlice.ts";
import {useEffect} from "react";
import type {ProductCatalogRenderRequest, TemplateFormatCode} from "../../models/templateModels.ts";

const useExportPage = () => {

    const state = useAppSelector<ExportPageState>(
        (state) => state.export
    );

    const dispatch = useAppDispatch();

    const handleDownload = (templateId: string, formatCode: TemplateFormatCode) => {
        const request: ProductCatalogRenderRequest = {
            templateId: templateId,
            formatCode: formatCode,
        }

        dispatch(downloadProductCatalog(request));
    }

    useEffect(() => {
        dispatch(getDocuments());
    }, [dispatch]);

    return {
        templates: state.templates,
        handleDownload,
        loading: state.loading
    };
}

export default useExportPage;