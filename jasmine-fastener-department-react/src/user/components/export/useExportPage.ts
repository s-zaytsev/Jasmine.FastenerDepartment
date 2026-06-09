import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {DocumentType, type ExportDocumentRequest, type ExportPageState} from "../../models/exportModels.ts";
import {downloadDocument} from "../../slices/ExportSlice.ts";

const useExportPage = () => {

    const state = useAppSelector<ExportPageState>(
        (state) => state.export
    );

    const dispatch = useAppDispatch();

    const handleDownload = (type: DocumentType) => {
        const request: ExportDocumentRequest = {
            documentType: type,
        }
        dispatch(downloadDocument(request));
    }

    return {
        handleDownload,
        loading: state.loading
    };
}

export default useExportPage;