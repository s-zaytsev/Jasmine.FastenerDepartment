import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import OrdersApi from "../api/ordersApi.ts";
import ProductTypesApi from "../api/productTypesApi.ts";
import type {OrderDetailsPageState} from "../models/orderModels.ts";
import {createSlice} from "@reduxjs/toolkit";
import {downloadService} from "../../shared/services/downloadService.ts";

export const getOrder = createAsyncThunkWithErrorHandler(
    "orderDetails/getOrder",
    async (id: string) => {
        return OrdersApi.getOrder(id);
    }
);

export const getProductTypes = createAsyncThunkWithErrorHandler(
    "orderDetails/getProductTypes",
    async () => {
        return ProductTypesApi.getProductTypes();
    }
);

export const downloadDocument = createAsyncThunkWithErrorHandler(
    "orderDetails/downloadDocument",
    async (id: string) => {
        const response = await OrdersApi.downloadDocument(id);
        downloadService.downloadFile(response);
    }
);

const initialState: OrderDetailsPageState = {
    order: undefined,
    productTypes: [],
    loading: false,
    success: undefined,
    error: undefined
};

const orderDetailsSlice = createSlice({
    name: "orderDetails",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getOrder.pending, (state) => {
                state.loading = true;
            })
            .addCase(getOrder.fulfilled, (state, {payload}) => {
                state.order = payload;
                state.loading = false;
            })
            .addCase(getOrder.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getProductTypes.pending, (state) => {
                state.loading = true;
            })
            .addCase(getProductTypes.fulfilled, (state, {payload}) => {
                state.productTypes = payload;
                state.loading = false;
            })
            .addCase(getProductTypes.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(downloadDocument.pending, (state) => {
                state.loading = true;
            })
            .addCase(downloadDocument.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(downloadDocument.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    setSuccess
} = orderDetailsSlice.actions;

export default orderDetailsSlice.reducer;