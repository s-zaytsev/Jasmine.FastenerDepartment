import type {StateBase} from "../../shared/models/models.ts";
import type {ProductHistoryEntry} from "./productModel.ts";
import type {ProductType} from "./productTypeModels.ts";

export interface HistoryPageState extends StateBase {
    items?: DailyHistory[];
    productTypes: ProductType[];
}

export interface DailyHistory {
    date: Date;
    historyEntries: ProductHistoryEntry[];
}