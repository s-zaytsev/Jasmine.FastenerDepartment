import type {StateBase} from "../../shared/models/models.ts";

export interface ProductTypesState extends StateBase {
    productTypes: ExtendedProductType[];
    selectedProductType?: ExtendedProductType;
}

export  interface ProductType {
    id: string;
    name: string;
}

export interface ExtendedProductType {
    id: string;
    name: string;
    productCount: number;
}

export interface ChangeProductType {
    name: string;
}