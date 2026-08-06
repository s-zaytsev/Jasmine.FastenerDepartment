import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import CompaniesApi from "../api/companiesApi.ts";
import type {ChangeCompany, CreateCompanyPageState} from "../models/companyModels.ts";
import {createSlice} from "@reduxjs/toolkit";

export const createCompany = createAsyncThunkWithErrorHandler(
    "createCompany/createCompany",
    async (model: ChangeCompany) => {
        return CompaniesApi.createCompany(model);
    }
);

const initialState: CreateCompanyPageState = {
    model: {
        title: '',
        firstName: '',
        middleName: '',
        lastName: '',
        email: '',
        city: '',
        street: '',
        buildingNumber: '',
        inn: '',
        phoneNumber: ''
    },
    loading: false,
    success: undefined,
    error: undefined
};

const createCompanySlice = createSlice({
    name: "createCompany",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(createCompany.pending, (state) => {
                state.loading = true;
                state.error = undefined;
                state.success = undefined;
            })
            .addCase(createCompany.fulfilled, (state) => {
                state.loading = false;
                state.success = "Компания успешно создана";
                state.error = undefined;
            })
            .addCase(createCompany.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
                state.success = undefined;
            });
    }
});

export const {
    setSuccess
} = createCompanySlice.actions;

export default createCompanySlice.reducer;