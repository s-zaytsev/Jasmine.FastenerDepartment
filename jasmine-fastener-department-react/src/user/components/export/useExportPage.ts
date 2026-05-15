import {useAppDispatch} from "../../../shared/hooks/sharedHooks.ts";
import {DocumentType, type ExportDocumentRequest} from "../../models/exportModels.ts";
import {downloadDocument} from "../../slices/ExportSlice.ts";

const useExportPage = () => {

    const dispatch = useAppDispatch();

    const handleDownload = (type: DocumentType) => {
        const request: ExportDocumentRequest = {
            documentType: type,
        }
        dispatch(downloadDocument(request));
    }

    return {
        handleDownload
    };
}

export default useExportPage;