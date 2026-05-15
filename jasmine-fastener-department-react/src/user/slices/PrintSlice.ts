import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import PrintApi from "../api/printApi.ts";
import type {PrintPageState, ProductToPrint} from "../models/printModels.ts";
import {localStorageService} from "../../shared/services/localStorageService.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getProductsToPrint = createAsyncThunkWithErrorHandler(
    "print/getProductsToPrint",
    async () => {
        return PrintApi.getProductsToPrint();
    }
);

export const deleteProductFromList = createAsyncThunkWithErrorHandler(
    "print/deleteProduct",
    async (productId: string) => {
        return PrintApi.delete(productId);
    }
);

export const deleteAllProductsFromList = createAsyncThunkWithErrorHandler(
    "print/deleteAllProducts",
    async () => {
        return PrintApi.deleteAll();
    }
);

const initialState: PrintPageState = {
    products: [],
    error: undefined,
    loading: false,
    success: undefined
};

const printSlice = createSlice({
    name: 'print',
    initialState,
    reducers: {
        incrementCount: (state, action: PayloadAction<{ id: string }>) => {
            const {id} = action.payload;
            const item = state.products.find((item) => item.product.id === id);
            if (item) {
                const currentCount = localStorageService.getItemCount(item.product.id);
                const newCount = +currentCount + 1;
                localStorageService.setItemCount(item.product.id, newCount);
                item.count = newCount;
            }
        },
        decrementCount: (state, action: PayloadAction<{ id: string }>) => {
            const {id} = action.payload;
            const item = state.products.find((item) => item.product.id === id);
            if (item && localStorageService.getItemCount(item.product.id) > 1) {
                const currentCount = localStorageService.getItemCount(item.product.id);
                const newCount = +currentCount - 1;
                localStorageService.setItemCount(item.product.id, newCount);
                item.count = newCount;
            }
        },
        changeCount: (state, action: PayloadAction<{ id: string, count: number }>) => {
            const {id, count} = action.payload;
            const item = state.products.find((item) => item.product.id === id);
            if (item) {
                localStorageService.setItemCount(item.product.id, count);
                item.count = count;
            }
        },
        removeProduct: (state, action: PayloadAction<{ id: string }>) => {
            const {id} = action.payload;
            state.products = state.products.filter(x => x.product.id != id);
            localStorage.removeItem(id);
        },
        clear: (state) => {
            state.products = [];
        },
    },
    extraReducers: builder => {
        builder
            .addCase(getProductsToPrint.pending, (state) => {
                state.loading = true;
            })
            .addCase(getProductsToPrint.fulfilled, (state, {payload}) => {
                state.products = payload.sort((a, b) => a.name.localeCompare(b.name))
                    .map(x => <ProductToPrint>{
                        product: x,
                        count: localStorageService.getItemCount(x.id)
                    });
                state.loading = false;
            })
            .addCase(getProductsToPrint.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(deleteProductFromList.pending, () => { })
            .addCase(deleteProductFromList.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(deleteProductFromList.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(deleteAllProductsFromList.pending, (state) => {
                state.loading = true;
            })
            .addCase(deleteAllProductsFromList.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(deleteAllProductsFromList.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    incrementCount,
    decrementCount,
    changeCount,
    removeProduct,
    clear
} = printSlice.actions;
export default printSlice.reducer;