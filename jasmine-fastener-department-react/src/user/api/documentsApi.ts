import api from "../../core/api.ts";
import type {ExportDocumentRequest} from "../models/exportModels.ts";
import type {ChangeTemplate} from "../models/templateModels.ts";

class DocumentsApi {
    downloadDocument(request: ExportDocumentRequest) {
        return api.get<Blob>(`/documents`, { params: request, responseType: 'blob' })
    }

    async getPreview(model: ChangeTemplate): Promise<string> {
        const response = await api.post<Blob>(`/documents/preview`, model,{
            responseType: 'blob',
        });

        const htmlText = await response.data.text();
        return htmlText;
    }
}

export default new DocumentsApi();