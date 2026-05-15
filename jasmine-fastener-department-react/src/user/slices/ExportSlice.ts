import {createSlice} from "@reduxjs/toolkit";
import type {ExportDocumentRequest, ExportPageState} from "../models/exportModels.ts";
import DocumentsApi from "../api/documentsApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import {downloadService} from "../../shared/services/downloadService.ts";

export const downloadDocument = createAsyncThunkWithErrorHandler(
    "export/downloadDocument",
    async (request: ExportDocumentRequest) => {
        const response = await DocumentsApi.downloadDocument(request);
        downloadService.downloadFile(response);
    }
);

const initialState: ExportPageState = {
    loading: false,
    success: undefined,
    error: undefined
};

const exportSlice = createSlice({
    name: "export",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(downloadDocument.pending, (state) => {
                state.loading = true;
            })
            .addCase(downloadDocument.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(downloadDocument.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    setSuccess
} = exportSlice.actions;

export default exportSlice.reducer;