import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import {createSlice} from "@reduxjs/toolkit";
import CompaniesApi from "../api/companiesApi.ts";
import type {CompaniesPageState} from "../models/companyModels.ts";

export const getCompanies = createAsyncThunkWithErrorHandler(
    "companies/getCompanies",
    async () => {
        return CompaniesApi.getCompanies();
    }
);

const initialState: CompaniesPageState = {
    companies: [],
    loading: false,
    success: undefined,
    error: undefined
};

const companiesSlice = createSlice({
    name: "companies",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getCompanies.pending, (state) => {
                state.loading = true;
            })
            .addCase(getCompanies.fulfilled, (state, {payload}) => {
                state.companies = payload.sort((a, b) => a.title.localeCompare(b.title));
                state.loading = false;
            })
            .addCase(getCompanies.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    setSuccess
} = companiesSlice.actions;

export default companiesSlice.reducer;