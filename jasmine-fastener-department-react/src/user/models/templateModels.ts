import type {StateBase} from "../../shared/models/models.ts";

export interface TemplatesPageState extends StateBase {
    templates: Template[];
}

export interface CreateTemplatePageState extends StateBase {
    model: ChangeTemplate;
    types: TemplateType[];
    columnGroups: TemplateContentTableColumnGroup[];
    preview: string;
}

export interface ChangeTemplatePageState extends StateBase {
    model: ChangeTemplate;
    types: TemplateType[];
    columnGroups: TemplateContentTableColumnGroup[];
    preview: string;
}

export interface Template {
    id: string;
    name: string;
    type: TemplateType;
    content: TemplateContent;
}

export interface TemplateType {
    id: TemplateTypeCode;
    name: string;
}

export enum TemplateTypeCode {
    productCatalog = 1,
    orderForm = 2
}

export type TemplateContent = ProductCatalogTemplateContent | OrderFormTemplateContent;

export interface ProductCatalogTemplateContent {
    groupByType: boolean;
    tableColumns: TemplateContentTableColumn[];
}

export enum ProductCatalogTemplateContentTableColumnCode {
    number = 1,
    name = 2,
    type = 3,
    price = 4
}

export interface OrderFormTemplateContent {
    hasCompanyData: boolean;
    groupByType: boolean;
    tableColumns: TemplateContentTableColumn[];
}

export enum OrderFormTemplateContentTableColumnCode {
    supplierProductNumber = 1,
    productName = 2,
    productNameWithoutSize = 3,
    productSize = 4,
    amount = 5
}

export interface ChangeTemplate {
    name: string;
    typeCode: TemplateTypeCode;
    content: TemplateContent;
}

export interface TemplateContentTableColumnGroup {
    typeCode: TemplateTypeCode;
    columns: TemplateContentTableColumn[];
}

export type ContentTableColumn = ProductCatalogTemplateContentTableColumnCode | OrderFormTemplateContentTableColumnCode;

export interface TemplateContentTableColumn {
    code: ContentTableColumn;
    name: string;
}

