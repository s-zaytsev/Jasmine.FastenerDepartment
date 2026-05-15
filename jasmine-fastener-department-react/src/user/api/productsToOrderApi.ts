import api from "../../core/api.ts";
import type {ProductsToOrderQuery, ProductToOrder} from "../models/productsToOrderModels.ts";

class ProductsToOrderApi {
    getProducts(query: ProductsToOrderQuery): Promise<ProductToOrder[]> {
        return api.get<ProductToOrder[]>(`/products-to-order`, {params: query})
            .then(x => x.data);
    }
}

export default new ProductsToOrderApi();