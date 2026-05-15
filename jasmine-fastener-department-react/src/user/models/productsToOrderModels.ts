import type {Product} from "./productModel.ts";

export interface ProductToOrder {
    product: Product;
    supplierNumbers: SupplierNumber[];
}

export interface SupplierNumber {
    supplierId: string;
    number: string;
}

export interface ProductsToOrderQuery {
    supplierId?: string;
    search?: string;
    onlyToOrder?: boolean;
}