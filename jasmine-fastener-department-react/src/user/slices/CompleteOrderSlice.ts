import {createSlice} from "@reduxjs/toolkit";
import OrdersApi from "../api/ordersApi.ts";
import type {CompleteOrderProduct, CompleteOrderState} from "../models/orderModels.ts";
import ProductTypesApi from "../api/productTypesApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getOrder = createAsyncThunkWithErrorHandler(
    "completeOrder/getOrder",
    async (id: string) => {
        return OrdersApi.getOrder(id);
    }
);

export const completeOrder = createAsyncThunkWithErrorHandler(
    "completeOrder/completeOrder",
    async ({id, model}: any) => {
        return OrdersApi.completeOrder(id, model);
    }
);

export const getProductTypes = createAsyncThunkWithErrorHandler(
    "completeOrder/getProductTypes",
    async () => {
        return ProductTypesApi.getProductTypes();
    }
);

const initialState: CompleteOrderState = {
    model: {
        comment: '',
        products: []
    },
    order: undefined,
    productTypes: [],
    loading: false,
    success: undefined,
    error: undefined
};

const completeOrderSlice = createSlice({
    name: "completeOrder",
    initialState: initialState,
    reducers: {
        updateProducts: (state, action) => {
            state.model.products = action.payload;
        },
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
                state.model.products = payload.products.map(x => <CompleteOrderProduct>{
                    orderProductId: x.id,
                    productId: x.productId,
                    productName: x.productName,
                    price: x.price,
                    ordered: x.ordered,
                    productType: x.productType,
                    supplierProductNumber: x.supplierProductNumber,
                    fulfilled: {
                        value: x.ordered.value,
                        measurementUnitCode: x.ordered.measurementUnitCode,
                        specialMeasurementUnit: x.ordered.specialMeasurementUnit
                    },
                    addToPrint: true,
                    removeOrderStatus: true,
                    isFulfilled: false
                });
                state.loading = false;
            })
            .addCase(getOrder.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(completeOrder.pending, (state) => {
                state.loading = true;
            })
            .addCase(completeOrder.fulfilled, (state) => {
                state.success = 'Заказ завершен';
                state.loading = false;
            })
            .addCase(completeOrder.rejected, (state, action) => {
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
    }
});

export const {
    updateProducts,
    setSuccess
} = completeOrderSlice.actions;

export default completeOrderSlice.reducer;