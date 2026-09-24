import {createSlice} from "@reduxjs/toolkit";
import type {TemplatesPageState} from "../models/templateModels.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import TemplatesApi from "../api/templatesApi.ts";

export const getTemplates = createAsyncThunkWithErrorHandler(
    "templates/getTemplates",
    async () => {
        return TemplatesApi.getTemplates();
    }
);

const initialState: TemplatesPageState = {
    templates: [],
    loading: false,
    success: undefined,
    error: undefined
};

const templatesSlice = createSlice({
    name: "templates",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getTemplates.pending, (state) => {
                state.loading = true;
            })
            .addCase(getTemplates.fulfilled, (state, {payload}) => {
                state.templates = payload;
                state.loading = false;
            })
            .addCase(getTemplates.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    setSuccess
} = templatesSlice.actions;

export default templatesSlice.reducer;