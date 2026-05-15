import api from "../../core/api.ts";
import type {DailyHistory} from "../models/historyModels.ts";

class HistoryApi {
    getHistory(): Promise<DailyHistory[]> {
        return api.get<DailyHistory[]>(`/product-history`)
            .then(x => x.data);
    }
}

export default new HistoryApi();