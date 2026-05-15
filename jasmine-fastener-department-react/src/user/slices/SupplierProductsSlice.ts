import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import {
    type SupplierProductsPageState,
    type SupplierProductsQuery,
    SupplierProductsQueryParameter
} from "../models/supplierModels.ts";
import SupplierProductsApi from "../api/supplierProductsApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getPage = createAsyncThunkWithErrorHandler(
    "supplierProducts/getPage",
    async (query: SupplierProductsQuery) => {
        return SupplierProductsApi.getPage(query);
    }
);

export const changeSupplierProduct = createAsyncThunkWithErrorHandler(
    "supplierProducts/change",
    async ({id, model}: any) => {
        return SupplierProductsApi.change(id, model);
    }
);

const initialState: SupplierProductsPageState = {
    query: {
        supplierId: '',
        search: '',
        sortBy: SupplierProductsQueryParameter.productNumber,
        sortDesc: false,
        pageNo: 1,
        pageSize: 10
    },
    page: undefined,
    selectedProduct: undefined,
    loading: false,
    success: undefined,
    error: undefined
};

const supplierProductsSlice = createSlice({
    name: "supplierProducts",
    initialState: initialState,
    reducers: {
        selectProduct: (state, action: PayloadAction<{ id: string }>) => {
            state.selectedProduct = state.page?.items.find(x => x.id == action.payload.id);
        },
        changeQuery: (state, action) => {
            state.query = action.payload;
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getPage.pending, (state) => {
                state.loading = true;
            })
            .addCase(getPage.fulfilled, (state, {payload}) => {
                state.page = payload;
                state.loading = false;
            })
            .addCase(getPage.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeSupplierProduct.pending, (state) => {
                state.loading = true;
            })
            .addCase(changeSupplierProduct.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(changeSupplierProduct.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    selectProduct,
    changeQuery,
    setSuccess
} = supplierProductsSlice.actions;

export default supplierProductsSlice.reducer;