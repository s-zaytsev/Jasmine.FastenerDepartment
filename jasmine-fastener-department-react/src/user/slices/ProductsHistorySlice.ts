import {createSlice} from "@reduxjs/toolkit";
import HistoryApi from "../api/historyApi.ts";
import type {HistoryPageState} from "../models/historyModels.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import ProductTypesApi from "../api/productTypesApi.ts";

export const getDailyHistory = createAsyncThunkWithErrorHandler(
    "history/getHistory",
    async () => {
        return HistoryApi.getHistory();
    }
);

export const getProductTypes = createAsyncThunkWithErrorHandler(
    "history/getProductTypes",
    async () => {
        return ProductTypesApi.getProductTypes();
    }
);

const initialState: HistoryPageState = {
    items: undefined,
    productTypes: [],
    loading: false,
    success: undefined,
    error: undefined
};

const historySlice = createSlice({
    name: "products",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getDailyHistory.pending, (state) => {
                state.loading = true;
            })
            .addCase(getDailyHistory.fulfilled, (state, {payload}) => {
                state.items = payload;
                state.loading = false;
            })
            .addCase(getDailyHistory.rejected, (state, action) => {
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
    setSuccess
} = historySlice.actions;

export default historySlice.reducer;