import api from "../../core/api.ts";
import type {Template} from "../models/templateModels.ts";
import type {DownloadDocument, SendDocument} from "../models/exportModels.ts";

class ExportDocumentsApi {
    downloadDocument(model: DownloadDocument) {
        return api.post<Blob>(`/export-documents/download`, model,{ responseType: 'blob'})
    }

    sendDocument(model: SendDocument) {
        return api.post<void>(`/export-documents/send`, model)
    }

    async getDocumentsForExport(): Promise<Template[]> {
        const x = await api.get<Template[]>('export-documents/export-documents');
        return x.data;
    }
}

export default new ExportDocumentsApi();