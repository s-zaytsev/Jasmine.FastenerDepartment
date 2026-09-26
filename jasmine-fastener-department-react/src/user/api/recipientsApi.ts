import api from "../../core/api.ts";
import type {ChangeRecipient, Recipient} from "../models/recipientModels.ts";

class ProductTypesApi {
    getRecipients(): Promise<Recipient[]> {
        return api.get<Recipient[]>(`/recipients`)
            .then(x => x.data);
    }

    createRecipient(model: ChangeRecipient): Promise<void> {
        return api.post(`/recipients`, model)
            .then(x => x.data);
    }

    changeRecipient(id: string, model: ChangeRecipient): Promise<void> {
        return api.put(`/recipients/${id}`, model)
            .then(x => x.data);
    }
}

export default new ProductTypesApi();