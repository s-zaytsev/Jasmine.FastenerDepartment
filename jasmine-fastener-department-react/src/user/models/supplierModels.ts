import type {Page, QueryBase, StateBase} from "../../shared/models/models.ts";
import {type Product} from "./productModel.ts";

export interface SuppliersPageState extends StateBase {
    suppliers: ExtendedSupplier[];
    selectedSupplier?: ExtendedSupplier;
}

export interface SupplierProductsPageState extends StateBase {
    query: SupplierProductsQuery;
    page?: Page<SupplierProduct>;
    selectedProduct?: SupplierProduct;
}

export interface Supplier {
    id: string;
    name: string;
    address: string;
}

export interface ExtendedSupplier {
    id: string;
    name: string;
    address: string;
    productCount: number;
    activeOrderCount: number;
}

export interface SupplierProduct {
    id: string;
    number: string;
    product: Product;
}

export interface ChangeSupplierModel {
    name: string;
    address: string;
}

export interface ChangeSupplierProduct {
    number: string;
}

export interface SupplierProductsQuery extends QueryBase<SupplierProductsQueryParameter> {
    supplierId: string;
}

export enum SupplierProductsQueryParameter {
    productNumber = 1,
    name = 2,
    price = 3,
    priceTagCode = 4
}