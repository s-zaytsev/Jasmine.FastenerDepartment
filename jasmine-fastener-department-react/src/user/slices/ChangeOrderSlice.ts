import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import type {ChangeOrderProduct, ChangeOrderState} from "../models/orderModels.ts";
import OrdersApi from "../api/ordersApi.ts";
import {ProductMeasurementUnitCode} from "../models/productModel.ts";
import type {ProductsToOrderQuery, SupplierNumber} from "../models/productsToOrderModels.ts";
import ProductsToOrderApi from "../api/productsToOrderApi.ts";
import ProductTypesApi from "../api/productTypesApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getOrder = createAsyncThunkWithErrorHandler(
    "changeOrder/getOrder",
    async (id: string) => {
        return OrdersApi.getOrder(id);
    }
);

export const getProductsToOrder = createAsyncThunkWithErrorHandler(
    "changeOrder/getProductsToOrder",
    async (query: ProductsToOrderQuery) => {
        return ProductsToOrderApi.getProducts(query);
    }
);

export const changeOrder = createAsyncThunkWithErrorHandler(
    "changeOrder/changeOrder",
    async ({id, model}: any) => {
        return OrdersApi.changeOrder(id, model);
    }
);

export const getProductTypes = createAsyncThunkWithErrorHandler(
    "changeOrder/getProductTypes",
    async () => {
        return ProductTypesApi.getProductTypes();
    }
);


const initialState: ChangeOrderState = {
    model: {
        products: []
    },
    order: undefined,
    products: [],
    productTypes: [],
    loading: false,
    success: undefined,
    error: undefined
};

const changeOrderSlice = createSlice({
    name: "changeOrder",
    initialState: initialState,
    reducers: {
        addProduct: (state) => {
            state.model.products.push({
                productName: '',
                id: '',
                supplierProductNumber: '',
                ordered: {
                    value: 1,
                    measurementUnitCode: ProductMeasurementUnitCode.pieces,
                    specialMeasurementUnit: ''
                }
            })
        },
        addExistedProduct: (state, action) => {
            const product: ChangeOrderProduct = {
                productId: action.payload.product.id,
                productName: action.payload.product.name,
                productType: action.payload.product.type,
                supplierProductNumber: state.order?.supplier?.id != undefined ?
                    action.payload.supplierNumbers.find((x: SupplierNumber) => x.supplierId === state.order?.supplier?.id)?.number ?? '' :
                    '',
                ordered: {
                    value: 1,
                    specialMeasurementUnit: '',
                    measurementUnitCode: action.payload.product.measurementUnitCode
                }
            }
            state.model.products.push(product);
        },
        updateProducts: (state, action) => {
            state.model.products = action.payload;
        },
        deleteProduct: (state, action: PayloadAction<{ id?: string }>) => {
            state.model.products = state.model.products.filter(x => x.productId != action.payload.id);
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
                state.model = {
                    supplier: payload.supplier,
                    products: payload.products
                };
                state.loading = false;
            })
            .addCase(getOrder.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getProductsToOrder.pending, (state) => {
                state.loading = true;
            })
            .addCase(getProductsToOrder.fulfilled, (state, {payload}) => {
                state.products = payload;
                state.loading = false;
            })
            .addCase(getProductsToOrder.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeOrder.pending, (state) => {
                state.loading = true;
            })
            .addCase(changeOrder.fulfilled, (state) => {
                state.success = 'Данные заказа обновлены';
                state.loading = false;
            })
            .addCase(changeOrder.rejected, (state, action) => {
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
    addProduct,
    addExistedProduct,
    updateProducts,
    deleteProduct,
    setSuccess
} = changeOrderSlice.actions;

export default changeOrderSlice.reducer;