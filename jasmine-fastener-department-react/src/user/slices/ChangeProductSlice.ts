import {createSlice} from "@reduxjs/toolkit";
import {type ChangeProductPageState, PriceTagCode, ProductMeasurementUnitCode} from "../models/productModel.ts";
import ProductsApi from "../api/productsApi.ts";
import SuppliersApi from "../api/suppliersApi.ts";
import ProductTypesApi from "../api/productTypesApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getProduct = createAsyncThunkWithErrorHandler(
    "changeProduct/getProduct",
    async (id: string) => {
        return ProductsApi.getProduct(id);
    }
);

export const save = createAsyncThunkWithErrorHandler(
    "changeProduct/save",
    async ({id, model}: any) => {
        return ProductsApi.changeProduct(id, model);
    }
);

export const getSuppliers = createAsyncThunkWithErrorHandler(
    "changeProduct/getSuppliers",
    async () => {
        return SuppliersApi.getSuppliers();
    }
);

export const getProductTypes = createAsyncThunkWithErrorHandler(
    "changeProduct/getProductTypes",
    async () => {
        return ProductTypesApi.getProductTypes();
    }
);

const initialState: ChangeProductPageState = {
    product: undefined,
    changeModel: {
        number: 0,
        name: '',
        price: 0,
        isHardwareSizeEnabled: true,
        measurementUnitCode: ProductMeasurementUnitCode.pieces,
        isNeededToOrder: false,
        isNeededToPrint: false,
        priceTagCode: PriceTagCode.m,
        supplierIds: []
    },
    loading: false,
    units: [],
    suppliers: [],
    productTypes: [],
    success: undefined,
    error: undefined
};

const changeProductSlice = createSlice({
    name: "changeProduct",
    initialState: initialState,
    reducers: {
        changeProduct: (state, action) => {
            state.changeModel = {
                ...action.payload,
                price: Number(action.payload.price)
            };
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getProduct.pending, (state) => {
                state.loading = true;
            })
            .addCase(getProduct.fulfilled, (state, {payload}) => {
                state.product = payload;
                state.changeModel = {
                    number: payload.number,
                    price: payload.price,
                    isHardwareSizeEnabled: payload.isHardwareSizeEnabled,
                    measurementUnitCode: payload.measurementUnitCode,
                    name: payload.name,
                    typeId: payload.type?.id,
                    priceTagCode: payload.priceTagCode,
                    isNeededToOrder: payload.isNeededToOrder,
                    isNeededToPrint: payload.isNeededToPrint,
                    supplierIds: payload.suppliers.map(x => x.id)
                }
                state.loading = false;
            })
            .addCase(getProduct.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(save.pending, (state) => {
                state.loading = true;
            })
            .addCase(save.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(save.rejected, (state, action) => {
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
                state.productTypes = payload.sort((a, b) => a.name.localeCompare(b.name));
                state.loading = false;
            })
            .addCase(getProductTypes.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    changeProduct,
    setSuccess
} = changeProductSlice.actions;

export default changeProductSlice.reducer;