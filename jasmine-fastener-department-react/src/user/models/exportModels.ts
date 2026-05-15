import type {StateBase} from "../../shared/models/models.ts";

export interface ExportDocumentRequest {
    documentType: DocumentType;
}

export enum DocumentType {
    word = 1
}

export interface ExportPageState extends StateBase {

}