import api from "../../core/api.ts";
import type {Page} from "../../shared/models/models.ts";
import type {
    CancelOrder,
    ChangeOrder,
    CompleteOrder,
    CreateOrder,
    DownloadOrder,
    Order,
    OrdersQuery,
    RenderOrderForm,
    SendOrder
} from "../models/orderModels.ts";

class OrdersApi {
    getOrdersPage(query: OrdersQuery): Promise<Page<Order>> {
        return api.get<Page<Order>>(`/orders`, {params: query})
            .then(x => x.data);
    }

    getOrder(id: string): Promise<Order> {
        return api.get<Order>(`/orders/${id}`)
            .then(x => x.data);
    }

    createOrder(model: CreateOrder): Promise<void> {
        return api.post(`/orders`, model)
            .then(x => x.data);
    }

    changeOrder(id: string, model: ChangeOrder): Promise<void> {
        return api.put(`/orders/${id}`, model)
    }

    completeOrder(id: string, model: CompleteOrder): Promise<void> {
        return api.post(`/orders/${id}/complete`, model)
    }

    sendOrder(id: string, model: SendOrder): Promise<void> {
        const body = new FormData();
        body.append('recipientContact', model.recipientContact);
        body.append('templateId', model.templateId);

        for (const format of model.formats) {
            body.append('formats', format.toString());
        }

        if (model.companyId) {
            body.append('companyId', model.companyId);
        }

        if (model.attachments) {
            for (const attachment of model.attachments) {
                body.append('attachments', attachment);
            }
        }

        return api.post(`/orders/${id}/send`, body)
    }

    async renderOrderForm(id: string, model: RenderOrderForm): Promise<string> {
        const response = await api.post<Blob>(`/orders/${id}/render`, model, {
            responseType: 'blob',
        });

        const text = await response.data.text();
        return text;
    }

    downloadOrder(id: string, model: DownloadOrder) {
        const body = new FormData();
        body.append('templateId', model.templateId);

        for (const format of model.formats) {
            body.append('formats', format.toString());
        }

        if (model.companyId) {
            body.append('companyId', model.companyId);
        }

        if (model.attachments) {
            for (const attachment of model.attachments) {
                body.append('attachments', attachment);
            }
        }

        return api.post<Blob>(`/orders/${id}/download`, body, {responseType: 'blob'})
    }

    cancelOrder(id: string, model: CancelOrder): Promise<void> {
        return api.post(`/orders/${id}/cancel`, model)
    }

    downloadDocument(id: string) {
        return api.get<Blob>(`/orders/document/${id}`, {responseType: 'blob'})
    }
}

export default new OrdersApi();