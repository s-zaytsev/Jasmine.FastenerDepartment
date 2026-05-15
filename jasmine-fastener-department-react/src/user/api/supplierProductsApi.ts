import api from "../../core/api.ts";
import type {Page} from "../../shared/models/models.ts";
import type {ChangeSupplierProduct, SupplierProduct, SupplierProductsQuery} from "../models/supplierModels.ts";

class SupplierProductsApi {
    getPage(query: SupplierProductsQuery): Promise<Page<SupplierProduct>> {
        return api.get<Page<SupplierProduct>>(`/supplier-products`, {params: query})
            .then(x => x.data);
    }

    change(id: string, model: ChangeSupplierProduct): Promise<void> {
        return api.put<Promise<void>>(`/supplier-products/${id}`, model)
            .then(x => x.data);
    }
}

export default new SupplierProductsApi();