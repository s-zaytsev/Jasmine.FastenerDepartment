import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import {type ProductsPageState, type ProductsQuery, ProductsQueryParameter} from "../models/productModel.ts";
import ProductsApi from "../api/productsApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import ProductTypesApi from "../api/productTypesApi.ts";

export const getPageFilters = createAsyncThunkWithErrorHandler(
    "products/getPageFilters",
    async (query: ProductsQuery) => {
        return ProductsApi.getFilters(query);
    }
);

export const getProducts = createAsyncThunkWithErrorHandler(
    "products/getProducts",
    async (query: ProductsQuery) => {
        return ProductsApi.getProducts(query);
    }
);

export const changePrintStatus = createAsyncThunkWithErrorHandler(
    "products/changePrintStatus",
    async (id: string) => {
        return ProductsApi.changePrintStatus(id);
    }
);

export const changeOrderStatus = createAsyncThunkWithErrorHandler(
    "products/changeOrderStatus",
    async (id: string) => {
        return ProductsApi.changeOrderStatus(id);
    }
);

export const getProductTypes = createAsyncThunkWithErrorHandler(
    "products/getProductTypes",
    async () => {
        return ProductTypesApi.getProductTypes();
    }
);

const initialState: ProductsPageState = {
    query: {
        search: '',
        sortBy: ProductsQueryParameter.productNumber,
        sortDesc: false,
        pageNo: 1,
        pageSize: 10,
        includeDeleted: false
    },
    filters: {},
    page: undefined,
    productTypes: [],
    loading: false,
    success: undefined,
    error: undefined
};

const productsSlice = createSlice({
    name: "products",
    initialState: initialState,
    reducers: {
        changeQuery: (state, action) => {
            state.query = action.payload;
        },
        changeNeededPrintStatus: (state, action: PayloadAction<{ id: string }>) => {
            const {id} = action.payload;
            const item = state.page?.items.find((item) => item.id === id);
            if (item) item.isNeededToPrint = !item.isNeededToPrint;
        },
        changeNeededOrderStatus: (state, action: PayloadAction<{ id: string }>) => {
            const {id} = action.payload;
            const item = state.page?.items.find((item) => item.id === id);
            if (item) item.isNeededToOrder = !item.isNeededToOrder;
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getPageFilters.pending, (state) => {
                state.loading = true;
            })
            .addCase(getPageFilters.fulfilled, (state, {payload}) => {
                state.filters = payload;
                state.loading = false;
            })
            .addCase(getPageFilters.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getProducts.pending, (state) => {
                state.loading = true;
            })
            .addCase(getProducts.fulfilled, (state, {payload}) => {
                state.page = payload;
                state.loading = false;
            })
            .addCase(getProducts.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changePrintStatus.pending, (state) => {
                state.loading = true;
            })
            .addCase(changePrintStatus.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(changePrintStatus.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeOrderStatus.pending, (state) => {
                state.loading = true;
            })
            .addCase(changeOrderStatus.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(changeOrderStatus.rejected, (state, action) => {
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
    changeQuery,
    changeNeededPrintStatus,
    changeNeededOrderStatus,
    setSuccess
} = productsSlice.actions;

export default productsSlice.reducer;