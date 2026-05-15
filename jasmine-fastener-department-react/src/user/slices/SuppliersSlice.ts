import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import SuppliersApi from "../api/suppliersApi.ts";
import type {ChangeSupplierModel, SuppliersPageState} from "../models/supplierModels.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";

export const getExtendedSuppliers = createAsyncThunkWithErrorHandler(
    "suppliers/getExtendedSuppliers",
    async () => {
        return SuppliersApi.getExtendedSuppliers();
    }
);

export const createSupplier = createAsyncThunkWithErrorHandler(
    "suppliers/createSupplier",
    async (model: ChangeSupplierModel) => {
        return SuppliersApi.createSupplier(model);
    }
);

export const changeSupplier = createAsyncThunkWithErrorHandler(
    "suppliers/changeSupplier",
    async ({id, model}: any) => {
        return SuppliersApi.changeSupplier(id, model);
    }
);

const initialState: SuppliersPageState = {
    suppliers: [],
    selectedSupplier: undefined,
    loading: false,
    success: undefined,
    error: undefined
};

const suppliersSlice = createSlice({
    name: "suppliers",
    initialState: initialState,
    reducers: {
        selectSupplier: (state, action: PayloadAction<{ id: string }>) => {
            state.selectedSupplier = state.suppliers.find(x => x.id == action.payload.id);
        },
        clearSelectedSupplier: (state) => {
            state.selectedSupplier = undefined;
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getExtendedSuppliers.pending, (state) => {
                state.loading = true;
            })
            .addCase(getExtendedSuppliers.fulfilled, (state, {payload}) => {
                state.suppliers = payload.sort((a, b) => a.name.localeCompare(b.name));
                state.loading = false;
            })
            .addCase(getExtendedSuppliers.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(createSupplier.pending, (state) => {
                state.loading = true;
                state.error = undefined;
                state.success = undefined;
            })
            .addCase(createSupplier.fulfilled, (state) => {
                state.loading = false;
                state.success = "Поставщик добавлен";
                state.error = undefined;
            })
            .addCase(createSupplier.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
                state.success = undefined;
            });

        builder
            .addCase(changeSupplier.pending, (state) => {
                state.loading = true;
                state.error = undefined;
                state.success = undefined;
            })
            .addCase(changeSupplier.fulfilled, (state) => {
                state.loading = false;
                state.success = "Данные поставщика обновлены";
                state.error = undefined;
            })
            .addCase(changeSupplier.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
                state.success = undefined;
            });
    }
});

export const {
    selectSupplier,
    clearSelectedSupplier,
    setSuccess
} = suppliersSlice.actions;

export default suppliersSlice.reducer;