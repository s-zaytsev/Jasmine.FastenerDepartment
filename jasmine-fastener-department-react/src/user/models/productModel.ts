import type {MultiFilter, Page, QueryBase, RangeFilter, SingleFilter, StateBase} from "../../shared/models/models.ts";
import type {Supplier} from "./supplierModels.ts";
import type {ProductType} from "./productTypeModels.ts";

export interface Product {
    id: string;
    number: number;
    name: string;
    price: number;
    type?: ProductType;
    measurementUnitCode: ProductMeasurementUnitCode;
    isNeededToPrint: boolean;
    isNeededToOrder: boolean;
    isHardwareSizeEnabled: boolean;
    priceTagCode: PriceTagCode;
    historyEntries: ProductHistoryEntry[];
    suppliers: Supplier[];
}

export interface ProductHistoryEntry {
    id: string;
    createdDate: string;
    productId: string;
    productNumber: number;
    changeReasonCode: ProductChangeReasonCode;
    oldValue: object;
    newValue: object;
}

export enum PriceTagCode {
    s = 1,
    l = 3,
    m = 4,
    xl = 5
}

export enum ProductMeasurementUnitCode {
    pieces = 1,
    meters = 2,
    kilograms = 3,
    packages = 4,
    sets = 5,
    lists = 6
}

export enum ProductChangeReasonCode {
    created = 1,
    changedNumber = 2,
    changedName = 3,
    changedPrice = 4,
    changedPriceTagCode = 5,
    changedMeasurementUnitCode = 6,
    changedIsNeededToOrder = 7,
    changedIsNeededToPrint = 8,
    deleted = 9,
    recovered = 10,
    changedType = 11
}

export interface MeasurementUnit {
    id: string;
    shortName: string;
    name: string;
}

export interface ChangeProduct {
    number: number;
    name: string;
    price: number;
    typeId?: string;
    isHardwareSizeEnabled: boolean;
    isNeededToPrint: boolean;
    isNeededToOrder: boolean;
    priceTagCode: PriceTagCode;
    measurementUnitCode: ProductMeasurementUnitCode;
    supplierIds: string[];
}

export interface ProductsQuery extends QueryBase<ProductsQueryParameter> {
    includeDeleted?: boolean;
    priceTags?: PriceTagCode[];
    types?: string[];
    suppliers?: string[];
    priceFrom?: number;
    priceTo?: number;
    onlyToPrint?: boolean;
    onlyToOrder?: boolean;
}

export interface ProductPageFilters {
    priceTags?: MultiFilter<PriceTagCode>;
    priceRange?: RangeFilter<number>;
    types?: MultiFilter<string>;
    suppliers?: MultiFilter<string>;
    onlyToPrint?: SingleFilter;
    onlyToOrder?: SingleFilter;
}

export enum ProductsQueryParameter {
    productNumber = 1,
    name = 2,
    price = 3,
    priceTagCode = 4,
    createdDate = 5,
    modifiedDate = 6,
    type = 7
}

export interface ProductsPageState extends StateBase {
    query: ProductsQuery;
    filters: ProductPageFilters;
    page?: Page<Product>;
    productTypes: ProductType[];
}

export interface ChangeProductPageState extends StateBase {
    product?: Product;
    changeModel: ChangeProduct;
    units: MeasurementUnit[];
    suppliers: Supplier[];
    productTypes: ProductType[];
}

export interface CreateProductPageState extends StateBase {
    changeModel: ChangeProduct;
    lastId: string;
    units: MeasurementUnit[];
    suppliers: Supplier[];
    productTypes: ProductType[];
}


export const shortMeasurementUnitName = (code: ProductMeasurementUnitCode) => {
    switch (code) {
        case ProductMeasurementUnitCode.meters:
            return 'м';
        case ProductMeasurementUnitCode.kilograms:
            return 'кг';
        case ProductMeasurementUnitCode.packages:
            return 'уп';
        case ProductMeasurementUnitCode.sets:
            return 'компл';
        case ProductMeasurementUnitCode.lists:
            return 'л';
        case ProductMeasurementUnitCode.pieces:
            return 'шт';
        default:
            return '??';
    }
}

export const longMeasurementUnitName = (code: ProductMeasurementUnitCode) => {
    switch (code) {
        case ProductMeasurementUnitCode.meters:
            return 'Метры';
        case ProductMeasurementUnitCode.kilograms:
            return 'Килограммы';
        case ProductMeasurementUnitCode.packages:
            return 'Упаковки';
        case ProductMeasurementUnitCode.sets:
            return 'Комплекты';
        case ProductMeasurementUnitCode.lists:
            return 'Листы';
        case ProductMeasurementUnitCode.pieces:
            return 'Штуки';
        default:
            return '??';
    }
}