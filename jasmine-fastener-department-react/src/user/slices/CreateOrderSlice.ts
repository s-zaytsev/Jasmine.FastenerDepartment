import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import type {ChangeOrderProduct, CreateOrder, CreateOrderState} from "../models/orderModels.ts";
import OrdersApi from "../api/ordersApi.ts";
import SuppliersApi from "../api/suppliersApi.ts";
import ProductTypesApi from "../api/productTypesApi.ts";
import type {ProductsToOrderQuery, SupplierNumber} from "../models/productsToOrderModels.ts";
import ProductsToOrderApi from "../api/productsToOrderApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import type {Supplier} from "../models/supplierModels.ts";

export const getProductsToOrder = createAsyncThunkWithErrorHandler(
    "createOrder/getProductsToOrder",
    async (query: ProductsToOrderQuery) => {
        return ProductsToOrderApi.getProducts(query);
    }
);

export const createOrder = createAsyncThunkWithErrorHandler(
    "createOrder/createOrder",
    async (model: CreateOrder) => {
        return OrdersApi.createOrder(model);
    }
);

export const getSuppliers = createAsyncThunkWithErrorHandler(
    "createOrder/getSuppliers",
    async () => {
        return SuppliersApi.getSuppliers();
    }
);

export const getProductTypes = createAsyncThunkWithErrorHandler(
    "createOrder/getProductTypes",
    async () => {
        return ProductTypesApi.getProductTypes();
    }
);

const initialState: CreateOrderState = {
    model: {
        products: []
    },
    products: [],
    productsToOrder: [],
    suppliers: [],
    productTypes: [],
    loading: false,
    success: undefined,
    error: undefined
};

const createOrderSlice = createSlice({
    name: "createOrder",
    initialState: initialState,
    reducers: {
        addExistedProduct: (state, action) => {
            const product: ChangeOrderProduct = {
                productId: action.payload.product.id,
                productName: action.payload.product.name,
                productType: action.payload.product.type,
                supplierProductNumber: state.model.supplier?.id != undefined ?
                    action.payload.supplierNumbers.find((x: SupplierNumber) => x.supplierId === state.model.supplier?.id)?.number :
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
        changeSupplier: (state, action: PayloadAction<{ supplier?: Supplier }>) => {
            state.model.supplier = action.payload.supplier;
            state.model.products = [];
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getProductsToOrder.pending, (state) => {
                state.loading = true;
            })
            .addCase(getProductsToOrder.fulfilled, (state, {payload}) => {
                state.products = payload.sort((a, b) => a.product.name.localeCompare(b.product.name));
                state.loading = false;
            })
            .addCase(getProductsToOrder.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(createOrder.pending, (state) => {
                state.loading = true;
            })
            .addCase(createOrder.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(createOrder.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getSuppliers.pending, (state) => {
                state.loading = true;
            })
            .addCase(getSuppliers.fulfilled, (state, {payload}) => {
                state.suppliers = payload;
                state.loading = false;
            })
            .addCase(getSuppliers.rejected, (state, action) => {
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
    addExistedProduct,
    updateProducts,
    deleteProduct,
    changeSupplier,
    setSuccess
} = createOrderSlice.actions;

export default createOrderSlice.reducer;