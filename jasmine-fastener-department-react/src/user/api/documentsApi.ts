import api from "../../core/api.ts";
import type {ExportDocumentRequest} from "../models/exportModels.ts";

class DocumentsApi {
    downloadDocument(request: ExportDocumentRequest) {
        return api.get<Blob>(`/documents`, { params: request, responseType: 'blob' })
    }
}

export default new DocumentsApi();