import type {ProductMeasurementUnitCode} from "../../user/models/productModel.ts";

export interface QueryBase<TSortParameter> {
    pageNo: number;
    pageSize: number;
    search: string;
    sortBy?: TSortParameter;
    sortDesc?: boolean;
}

export interface Page<T> {
    items: T[];
    pageNo: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
}

export interface TableColumnDefinition {
    title?: string;
    parameter?: number;
    columnAlign?: string;
    width?: string;
}

export interface StateBase {
    loading: boolean;
    success?: string;
    error?: object;
}

export interface Checked<T> {
    value: T;
    isChecked: boolean;
}

export const numberEnumToArray = <TEnum extends number>(obj: object): TEnum[] => {
    const values = Object.keys(obj).map(x => Number(x)).filter(x => !isNaN(x)).map(x => x as TEnum);
    return values;
}

export const stringEnumToArray = <TEnum extends string>(obj: object): TEnum[] => {
    const values = Object.keys(obj);
    return values as TEnum[];
}

export interface Quantity {
    value: number;
    measurementUnitCode?: ProductMeasurementUnitCode | undefined;
    specialMeasurementUnit?: string;
}

export interface Filter<T> {
    id: T;
    title: string;
    count: number;
    isEnabled: boolean;
}

export interface SingleFilter {
    count: number;
    isEnabled: boolean;
}

export interface MultiFilter<T> {
    items: Filter<T>[];
}

export interface RangeFilter<T> {
    from: T;
    to: T;
    min: T;
    max: T;
}

export interface GroupOptions<T> {
    sortFn?: (a: T, b: T) => number;
    sortGroups?: boolean;
}

export interface StepperItem<T> {
    id: T;
    label: string;
}

export enum LanguageCode {
    en = "English",
    ru = "Russian"
}

export enum MessageType {
    email = 1
}