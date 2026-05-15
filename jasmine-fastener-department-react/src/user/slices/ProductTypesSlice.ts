import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import ProductTypesApi from "../api/productTypesApi.ts";
import type {ChangeProductType, ProductTypesState} from "../models/productTypeModels.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getExtendedProductTypes = createAsyncThunkWithErrorHandler(
    "productTypes/getExtendedProductTypes",
    async () => {
        return ProductTypesApi.getExtendedProductTypes();
    }
);

export const createProductType = createAsyncThunkWithErrorHandler(
    "productTypes/createProductType",
    async (model: ChangeProductType) => {
        return ProductTypesApi.createProductType(model);
    }
);

export const changeProductType = createAsyncThunkWithErrorHandler(
    "productTypes/changeProductType",
    async ({id, model}: any) => {
        return ProductTypesApi.changeProductType(id, model);
    }
);

const initialState: ProductTypesState = {
    productTypes: [],
    selectedProductType: undefined,
    loading: false,
    success: undefined,
    error: undefined
};

const productTypesSlice = createSlice({
    name: "productTypes",
    initialState: initialState,
    reducers: {
        selectProductType: (state, action: PayloadAction<{ id: string }>) => {
            state.selectedProductType = state.productTypes.find(x => x.id == action.payload.id);
        },
        clearSelectedProductType: (state) => {
            state.selectedProductType = undefined;
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getExtendedProductTypes.pending, (state) => {
                state.loading = true;
            })
            .addCase(getExtendedProductTypes.fulfilled, (state, {payload}) => {
                state.productTypes = payload.sort((a, b) => a.name.localeCompare(b.name));
                state.loading = false;
            })
            .addCase(getExtendedProductTypes.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(createProductType.pending, (state) => {
                state.loading = true;
            })
            .addCase(createProductType.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(createProductType.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeProductType.pending, (state) => {
                state.loading = true;
            })
            .addCase(changeProductType.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(changeProductType.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    selectProductType,
    clearSelectedProductType,
    setSuccess
} = productTypesSlice.actions;

export default productTypesSlice.reducer;