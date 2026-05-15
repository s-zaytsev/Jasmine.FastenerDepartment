import type {Page, Quantity, QueryBase, StateBase} from "../../shared/models/models.ts";
import type {Supplier} from "./supplierModels.ts";
import type {ProductType} from "./productTypeModels.ts";
import type {ProductToOrder} from "./productsToOrderModels.ts";

export interface OrderPageState extends StateBase {
    completedOrdersQuery: OrdersQuery;
    completedOrders?: Page<Order>;
    activeOrdersQuery: OrdersQuery;
    activeOrders?: Page<Order>;
    selectedOrder?: Order;
}

export interface CreateOrderState extends StateBase {
    model: OrderStepperModel;
    products: ProductToOrder[];
    productsToOrder: ProductToOrder[];
    suppliers: Supplier[];
    productTypes: ProductType[];
}

export interface ChangeOrderState extends StateBase {
    order?: Order;
    model: OrderStepperModel;
    products: ProductToOrder[];
    productTypes: ProductType[];
}

export interface CompleteOrderState extends StateBase {
    order?: Order;
    completeOrderModel: CompleteOrder;
    productTypes: ProductType[];
}

export interface FulfilledOrderState extends StateBase {
    order?: Order;
    productTypes: ProductType[];
}

export interface CancelledOrderState extends StateBase {
    order?: Order;
    productTypes: ProductType[];
}

export interface OrderDetailsPageState extends StateBase {
    order?: Order;
    productTypes: ProductType[];
}

export interface OrdersQuery extends QueryBase<OrdersQueryParameter> {
    onlyCompleted: boolean;
    onlyActive: boolean;
}

export enum OrdersQueryParameter {
    createdDate = 1,
    modifiedDate = 2,
    closedDate = 3,
    status = 4
}

export interface Order {
    id: string;
    createdDate: string;
    number: number;
    statusCode: OrderStatusCode;
    supplier?: Supplier;
    products: OrderProduct[];
    historyEntries: OrderHistoryEntry[];
}

export interface OrderProduct {
    id?: string;
    productId?: string;
    productName: string;
    price: number;
    productType?: ProductType;
    ordered: Quantity;
    fulfilled?: Quantity;
    supplierProductNumber: string;
    isFulfilled: boolean;
}

export interface OrderHistoryEntry {
    id: string;
    createdDate: string;
    statusCode: OrderStatusCode;
    message: string;
}

export interface OrderStepperModel {
    supplier?: Supplier;
    products: ChangeOrderProduct[];
}

export interface CreateOrder {
    supplierId?: string;
    products: ChangeOrderProduct[];
}

export interface ChangeOrder {
    products: ChangeOrderProduct[];
}

export interface ChangeOrderProduct {
    id?: string;
    productId?: string;
    productName: string;
    productType?: ProductType;
    ordered: Quantity;
    supplierProductNumber: string;
}

export enum OrderStatusCode {
    created = 1,
    sent = 2,
    fulfilled = 3,
    cancelled = 4
}

export interface CompleteOrder {
    comment: string;
    products: CompleteOrderProduct[];
}

export interface CancelOrder {
    comment: string;
}

export interface CompleteOrderProduct {
    orderProductId?: string;
    productId?: string;
    productName: string;
    price: number;
    productType?: ProductType;
    ordered: Quantity;
    fulfilled: Quantity;
    isFulfilled: boolean;
    addToPrint: boolean;
    removeOrderStatus: boolean;
    supplierProductNumber: string;
}

export interface ChangeOrderForm {
    products: ChangeOrderProduct[];
}

export interface CompleteOrderForm {
    products: CompleteOrderProduct[];
}

export enum OrderStepperStep {
    suppliers = 1,
    products = 2,
    amount = 3,
    confirm = 4
}