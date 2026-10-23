import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import type {DownloadDocument, ExportPageState, SendDocument} from "../models/exportModels.ts";
import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import {downloadService} from "../../shared/services/downloadService.ts";
import ExportDocumentsApi from "../api/exportDocumentsApi.ts";
import type {TemplateFormatCode} from "../models/templateModels.ts";

export const getDocuments = createAsyncThunkWithErrorHandler(
    'export/getDocuments',
    async () => {
        return ExportDocumentsApi.getDocumentsForExport();
    }
)

export const downloadDocument = createAsyncThunkWithErrorHandler(
    "export/downloadDocument",
    async (model: DownloadDocument) => {
        const response = await ExportDocumentsApi.downloadDocument(model);
        downloadService.downloadFile(response);
    }
);

export const sendDocument = createAsyncThunkWithErrorHandler(
    "export/sendDocument",
    async (model: SendDocument) => {
        await ExportDocumentsApi.sendDocument(model);
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
        selectTemplate: (state, action: PayloadAction<{ id: string, code: TemplateFormatCode }>) => {
            state.selectedTemplate = state.templates.find(x => x.id == action.payload.id);
            state.selectedFormatCode = action.payload.code;
        },
        clearSelectedTemplate: (state) => {
            state.selectedTemplate = undefined;
            state.selectedFormatCode = undefined;
        },
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

        builder
            .addCase(sendDocument.pending, (state) => {
                state.loading = true;
            })
            .addCase(sendDocument.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(sendDocument.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    selectTemplate,
    clearSelectedTemplate,
    setSuccess
} = exportSlice.actions;

export default exportSlice.reducer;