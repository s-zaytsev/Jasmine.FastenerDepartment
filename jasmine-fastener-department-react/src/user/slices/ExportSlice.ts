import {createSlice} from "@reduxjs/toolkit";
import type {ExportPageState} from "../models/exportModels.ts";
import DocumentsApi from "../api/documentsApi.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import {downloadService} from "../../shared/services/downloadService.ts";
import type {ProductCatalogRenderRequest} from "../models/templateModels.ts";

export const getDocuments = createAsyncThunkWithErrorHandler(
    'export/getDocuments',
    async () => {
        return DocumentsApi.getDocumentsForExport();
    }
)

export const downloadProductCatalog = createAsyncThunkWithErrorHandler(
    "export/downloadDocument",
    async (request: ProductCatalogRenderRequest) => {
        const response = await DocumentsApi.downloadProductCatalogDocument(request);
        downloadService.downloadFile(response);
    }
);

const initialState: ExportPageState = {
    templates: [],
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
            .addCase(getDocuments.pending, (state) => {
                state.loading = true;
            })
            .addCase(getDocuments.fulfilled, (state, {payload}) => {
                state.templates = payload;
                state.loading = false;
            })
            .addCase(getDocuments.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(downloadProductCatalog.pending, (state) => {
                state.loading = true;
            })
            .addCase(downloadProductCatalog.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(downloadProductCatalog.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    setSuccess
} = exportSlice.actions;

export default exportSlice.reducer;