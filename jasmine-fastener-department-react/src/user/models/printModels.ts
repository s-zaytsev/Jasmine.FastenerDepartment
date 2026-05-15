import type {StateBase} from "../../shared/models/models.ts";
import type {Product} from "./productModel.ts";

export interface PrintPageState extends StateBase {
    products: ProductToPrint[];
}

export interface ProductToPrint {
    count: number;
    product: Product;
}

export interface ProductToPrintRow {
    id: number;
    products: ProductToPrintRowItem[];
    height: number;
    isSorted: boolean;
}

export interface ProductToPrintRowItem {
    id: number;
    product: Product;
}
