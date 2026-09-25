import api from "../../core/api.ts";
import type {ChangeTemplate, ProductCatalogRenderRequest, Template} from "../models/templateModels.ts";

class DocumentsApi {
    downloadProductCatalogDocument(request: ProductCatalogRenderRequest) {
        return api.post<Blob>(`/documents/product-catalog`, request,{ responseType: 'blob'})
    }

    async getDocumentsForExport(): Promise<Template[]> {
        const x = await api.get<Template[]>('documents/export-documents');
        return x.data;
    }

    async getPreview(model: ChangeTemplate): Promise<string> {
        const response = await api.post<Blob>(`/documents/preview`, model, {
            responseType: 'blob',
        });

        const htmlText = await response.data.text();
        return htmlText;
    }
}

export default new DocumentsApi();