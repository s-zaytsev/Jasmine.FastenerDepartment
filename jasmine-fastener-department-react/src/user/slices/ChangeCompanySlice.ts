import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import CompaniesApi from "../api/companiesApi.ts";
import type {ChangeCompanyPageState} from "../models/companyModels.ts";
import {createSlice} from "@reduxjs/toolkit";

export const getCompany = createAsyncThunkWithErrorHandler(
    "changeCompany/getCompany",
    async (id: string) => {
        return CompaniesApi.getCompany(id);
    }
);

export const changeCompany = createAsyncThunkWithErrorHandler(
    "changeCompany/changeCompany",
    async ({id, model}: any) => {
        return CompaniesApi.changeCompany(id, model);
    }
);

const initialState: ChangeCompanyPageState = {
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

const changeCompanySlice = createSlice({
    name: "changeCompany",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getCompany.pending, (state) => {
                state.loading = true;
            })
            .addCase(getCompany.fulfilled, (state, {payload}) => {
                state.model = {
                    title: payload.title,
                    firstName: payload.firstName,
                    middleName: payload.middleName,
                    lastName: payload.lastName,
                    email: payload.email,
                    city: payload.city,
                    street: payload.street,
                    buildingNumber: payload.buildingNumber,
                    inn: payload.inn,
                    phoneNumber: payload.phoneNumber
                };
                state.loading = false;
            })
            .addCase(getCompany.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeCompany.pending, (state) => {
                state.loading = true;
                state.error = undefined;
                state.success = undefined;
            })
            .addCase(changeCompany.fulfilled, (state) => {
                state.loading = false;
                state.success = "Данные компании обновлены";
                state.error = undefined;
            })
            .addCase(changeCompany.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
                state.success = undefined;
            });
    }
});

export const {
    setSuccess
} = changeCompanySlice.actions;

export default changeCompanySlice.reducer;