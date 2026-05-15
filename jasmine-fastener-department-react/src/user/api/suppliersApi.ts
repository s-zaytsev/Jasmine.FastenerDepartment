import api from "../../core/api.ts";
import type {ChangeSupplierModel, ExtendedSupplier, Supplier} from "../models/supplierModels.ts";

class SuppliersApi {
    getSuppliers(): Promise<Supplier[]> {
        return api.get<Supplier[]>(`/suppliers`)
            .then(x => x.data);
    }

    getExtendedSuppliers(): Promise<ExtendedSupplier[]> {
        return api.get<ExtendedSupplier[]>(`/suppliers/extended`)
            .then(x => x.data);
    }

    getSupplier(id: string): Promise<Supplier> {
        return api.get<Supplier>(`/suppliers/${id}`)
            .then(x => x.data);
    }

    changeSupplier(id: string, model: ChangeSupplierModel): Promise<void> {
        return api.put(`/suppliers/${id}`, model)
            .then(x => x.data);
    }

    createSupplier(model: ChangeSupplierModel): Promise<void> {
        return api.post(`/suppliers`, model)
            .then(x => x.data);
    }
}

export default new SuppliersApi();