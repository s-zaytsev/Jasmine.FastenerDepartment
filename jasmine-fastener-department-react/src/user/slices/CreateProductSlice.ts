import {createSlice} from "@reduxjs/toolkit";
import {
    type ChangeProduct,
    type CreateProductPageState,
    PriceTagCode,
    ProductMeasurementUnitCode
} from "../models/productModel.ts";
import ProductsApi from "../api/productsApi.ts";
import SuppliersApi from "../api/suppliersApi.ts";
import ProductTypesApi from "../api/productTypesApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getLastId = createAsyncThunkWithErrorHandler(
    "createProduct/getLastId",
    async () => {
        return ProductsApi.getLastId();
    }
);

export const saveProduct = createAsyncThunkWithErrorHandler(
    "createProduct/saveProduct",
    async (model: ChangeProduct) => {
        return ProductsApi.createProduct(model);
    }
);

export const getSuppliers = createAsyncThunkWithErrorHandler(
    "createProduct/getSuppliers",
    async () => {
        return SuppliersApi.getSuppliers();
    }
);

export const getProductTypes = createAsyncThunkWithErrorHandler(
    "createProduct/getProductTypes",
    async () => {
        return ProductTypesApi.getProductTypes();
    }
);

const initialState: CreateProductPageState = {
    model: {
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
    lastId: '',
    units: [],
    suppliers: [],
    productTypes: [],
    loading: false,
    success: undefined,
    error: undefined
};

const createProductSlice = createSlice({
    name: "createProduct",
    initialState: initialState,
    reducers: {
        changeProduct: (state, action) => {
            state.model = {
                ...action.payload,
                price: Number(action.payload.price)
            };
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        },
        setError: (state, action) => {
            state.error = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getLastId.pending, (state) => {
                state.loading = true;
            })
            .addCase(getLastId.fulfilled, (state, {payload}) => {
                state.model.number = payload + 1;
                state.loading = false;
            })
            .addCase(getLastId.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(saveProduct.pending, (state) => {
                state.loading = true;
                state.error = undefined;
                state.success = undefined;
            })
            .addCase(saveProduct.fulfilled, (state) => {
                state.loading = false;
                state.success = "Продукт успешно создан";
                state.model = {
                    number: 0,
                    name: '',
                    price: 0,
                    isHardwareSizeEnabled: true,
                    measurementUnitCode: ProductMeasurementUnitCode.pieces,
                    isNeededToOrder: false,
                    isNeededToPrint: false,
                    priceTagCode: PriceTagCode.m,
                    supplierIds: []
                }
                state.error = undefined;
            })
            .addCase(saveProduct.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
                state.success = undefined;
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
    setSuccess,
    setError
} = createProductSlice.actions;

export default createProductSlice.reducer;