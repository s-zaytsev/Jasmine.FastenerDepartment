import type {StateBase} from "../../shared/models/models.ts";
import {type Template, TemplateFormatCode} from "./templateModels.ts";

export interface ExportPageState extends StateBase {
    templates: Template[];
    selectedTemplate?: Template;
    selectedFormatCode?: TemplateFormatCode;
}

export interface DownloadDocument {
    templateId: string;
    formatCode: TemplateFormatCode;
}

export interface SendDocument {
    templateId: string;
    formatCode: TemplateFormatCode;
    recipientContact: string;
}