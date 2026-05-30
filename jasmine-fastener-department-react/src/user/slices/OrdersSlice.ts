import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import OrdersApi from "../api/ordersApi.ts";
import {type OrderPageState, type OrdersQuery, OrdersQueryParameter} from "../models/orderModels.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getCompletedOrdersPage = createAsyncThunkWithErrorHandler(
    "orders/getCompletedOrdersPage",
    async (query: OrdersQuery) => {
        return OrdersApi.getOrdersPage(query);
    }
);

export const getActiveOrdersPage = createAsyncThunkWithErrorHandler(
    "orders/getActiveOrdersPage",
    async (query: OrdersQuery) => {
        return OrdersApi.getOrdersPage(query);
    }
);

export const sendOrder = createAsyncThunkWithErrorHandler(
    "orders/sendOrder",
    async ({id, model}: any) => {
        return OrdersApi.sendOrder(id, model);
    }
);

export const cancelOrder = createAsyncThunkWithErrorHandler(
    "orders/cancelOrder",
    async ({id, model}: any) => {
        return OrdersApi.cancelOrder(id, model);
    }
);

const initialState: OrderPageState = {
    completedOrdersQuery: {
        pageNo: 1,
        pageSize: 10,
        search: '',
        sortDesc: true,
        sortBy: OrdersQueryParameter.createdDate,
        onlyCompleted: true,
        onlyActive: false
    },
    activeOrdersQuery: {
        pageNo: 1,
        pageSize: 100,
        search: '',
        sortDesc: true,
        sortBy: OrdersQueryParameter.createdDate,
        onlyActive: true,
        onlyCompleted: false
    },
    completedOrders: undefined,
    activeOrders: undefined,
    selectedOrder: undefined,
    loading: false,
    success: undefined,
    error: undefined
};

const ordersSlice = createSlice({
    name: "orders",
    initialState: initialState,
    reducers: {
        selectOrder: (state, action: PayloadAction<{ id: string }>) => {
            state.selectedOrder = state.activeOrders?.items.find(x => x.id == action.payload.id);
        },
        clearSelectedOrder: (state) => {
            state.selectedOrder = undefined;
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getCompletedOrdersPage.pending, (state) => {
                state.loading = true;
            })
            .addCase(getCompletedOrdersPage.fulfilled, (state, {payload}) => {
                state.completedOrders = payload;
                state.loading = false;
            })
            .addCase(getCompletedOrdersPage.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getActiveOrdersPage.pending, (state) => {
                state.loading = true;
            })
            .addCase(getActiveOrdersPage.fulfilled, (state, {payload}) => {
                state.activeOrders = payload;
                state.loading = false;
            })
            .addCase(getActiveOrdersPage.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(sendOrder.pending, (state) => {
                state.loading = true;
            })
            .addCase(sendOrder.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(sendOrder.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(cancelOrder.pending, (state) => {
                state.loading = true;
            })
            .addCase(cancelOrder.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(cancelOrder.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    selectOrder,
    clearSelectedOrder,
    setSuccess
} = ordersSlice.actions;

export default ordersSlice.reducer;