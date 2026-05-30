import api from "../../core/api.ts";
import type {Page} from "../../shared/models/models.ts";
import type {
    CancelOrder,
    ChangeOrder,
    CompleteOrder,
    CreateOrder,
    Order,
    OrdersQuery,
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
        return api.post(`/orders/${id}/send`, model)
    }

    cancelOrder(id: string, model: CancelOrder): Promise<void> {
        return api.post(`/orders/${id}/cancel`, model)
    }

    downloadDocument(id: string) {
        return api.get<Blob>(`/orders/document/${id}`, { responseType: 'blob' })
    }
}

export default new OrdersApi();