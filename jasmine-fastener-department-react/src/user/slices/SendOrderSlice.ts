import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import RecipientsApi from "../api/recipientsApi.ts";
import type {SendOrderState} from "../models/orderModels.ts";
import TemplatesApi from "../api/templatesApi.ts";
import {type OrderFormRenderRequest, TemplateFormatCode, type TemplatesQuery} from "../models/templateModels.ts";
import CompaniesApi from "../api/companiesApi.ts";
import DocumentsApi from "../api/documentsApi.ts";
import type {FileModel} from "../../shared/models/models.ts";

export const getCompanies = createAsyncThunkWithErrorHandler(
    "sendOrder/getCompanies",
    async () => {
        return CompaniesApi.getCompanies();
    }
);

export const getTemplates = createAsyncThunkWithErrorHandler(
    "sendOrder/getTemplates",
    async (query: TemplatesQuery) => {
        return TemplatesApi.getTemplates(query);
    }
);

export const getRecipients = createAsyncThunkWithErrorHandler(
    "sendOrder/getRecipients",
    async () => {
        return RecipientsApi.getRecipients();
    }
);

export const renderOrderForm = createAsyncThunkWithErrorHandler(
    "sendOrder/renderOrderForm",
    async (request: OrderFormRenderRequest) => {
        return DocumentsApi.renderOrderForm(request);
    }
);

const initialState: SendOrderState = {
    companies: [],
    templates: [],
    recipients: [],
    preview: '',
    selectedFormats: [],
    extraFiles: [],
    loading: false,
    success: undefined,
    error: undefined
};

const sendOrderSlice = createSlice({
    name: "sendOrder",
    initialState: initialState,
    reducers: {
        selectTemplate: (state, action: PayloadAction<{ id: string }>) => {
            state.selectedTemplate = state.templates.find(x => x.id == action.payload.id);
        },
        selectCompany: (state, action: PayloadAction<{ id: string }>) => {
            state.selectedCompany = state.companies.find(x => x.id == action.payload.id);
        },
        selectRecipient: (state, action: PayloadAction<{ id: string }>) => {
            state.selectedRecipient = state.recipients.find(x => x.id == action.payload.id);
        },
        selectFormat: (state, action: PayloadAction<{ code: TemplateFormatCode }>) => {
            if (state.selectedFormats.find(x => x == action.payload.code)) {
                state.selectedFormats = state.selectedFormats.filter(x => x !== action.payload.code);
            } else
                state.selectedFormats = [...state.selectedFormats, action.payload.code];
        },
        clearSelectedRecipient: (state) => {
            state.selectedRecipient = undefined;
        },
        setExtraFiles: (state, action: PayloadAction<{ files: FileModel[] }>) => {
            state.extraFiles = action.payload.files;
        },
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

        builder
            .addCase(getRecipients.pending, (state) => {
                state.loading = true;
            })
            .addCase(getRecipients.fulfilled, (state, {payload}) => {
                state.recipients = payload.sort((a, b) => a.name.localeCompare(b.name));
                state.loading = false;
            })
            .addCase(getRecipients.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

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

        builder
            .addCase(renderOrderForm.pending, (state) => {
                state.loading = true;
            })
            .addCase(renderOrderForm.fulfilled, (state, {payload}) => {
                state.preview = payload;
                state.loading = false;
            })
            .addCase(renderOrderForm.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    selectTemplate,
    selectCompany,
    selectRecipient,
    selectFormat,
    clearSelectedRecipient,
    setExtraFiles,
    setSuccess
} = sendOrderSlice.actions;

export default sendOrderSlice.reducer;