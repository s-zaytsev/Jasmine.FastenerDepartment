import api from "../../core/api.ts";
import type {ChangeProductType, ExtendedProductType, ProductType} from "../models/productTypeModels.ts";

class ProductTypesApi {
    getProductTypes(): Promise<ProductType[]> {
        return api.get<ProductType[]>(`/product-types`)
            .then(x => x.data);
    }

    getExtendedProductTypes(): Promise<ExtendedProductType[]> {
        return api.get<ExtendedProductType[]>(`/product-types/extended`)
            .then(x => x.data);
    }

    createProductType(model: ChangeProductType): Promise<void> {
        return api.post(`/product-types`, model)
            .then(x => x.data);
    }

    changeProductType(id: string, model: ChangeProductType): Promise<void> {
        return api.put(`/product-types/${id}`, model)
            .then(x => x.data);
    }
}

export default new ProductTypesApi();