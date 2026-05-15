import type {ChangeProduct, Product, ProductPageFilters, ProductsQuery} from "../models/productModel.ts";
import type {Page} from "../../shared/models/models.ts";
import api from "../../core/api.ts";

class ProductsApi {
    getFilters(query: ProductsQuery): Promise<ProductPageFilters> {
        return api.get<ProductPageFilters>(`/products/page-filters`, {params: query})
            .then(x => x.data);
    }

    getProducts(query: ProductsQuery): Promise<Page<Product>> {
        return api.get<Page<Product>>(`/products`, {params: query})
            .then(x => x.data);
    }

    getProduct(id: string): Promise<Product> {
        return api.get<Product>(`/products/${id}`)
            .then(x => x.data);
    }

    getLastId(): Promise<number> {
        return api.get<number>(`/products/last-number`)
            .then(x => x.data);
    }

    changePrintStatus(id: string): Promise<void> {
        return api.post(`/products/${id}/print`, null)
            .then(x => x.data);
    }

    changeOrderStatus(id: string): Promise<void> {
        return api.post(`/products/${id}/order`, null)
            .then(x => x.data);
    }

    changeProduct(id: string, model: ChangeProduct): Promise<void> {
        return api.put(`/products/${id}`, model)
            .then(x => x.data);
    }

    createProduct(model: ChangeProduct): Promise<void> {
        return api.post(`/products`, model)
            .then(x => x.data);
    }
}

export default new ProductsApi();