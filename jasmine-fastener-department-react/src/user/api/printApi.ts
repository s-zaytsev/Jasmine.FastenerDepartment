import api from "../../core/api.ts";
import type {Product} from "../models/productModel.ts";

class PrintApi {
    getProductsToPrint(): Promise<Product[]> {
        return api.get<Product[]>(`/print`)
            .then(x => x.data);
    }

    delete(productId: string) {
        return api.delete(`/print/${productId}`).then(x => x.data);
    }

    deleteAll() {
        return api.delete(`/print`).then(x => x.data);
    }
}

export default new PrintApi();