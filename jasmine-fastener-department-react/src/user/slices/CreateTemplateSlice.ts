import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import TemplatesApi from "../api/templatesApi.ts";
import {type ChangeTemplate, type CreateTemplatePageState, TemplateTypeCode} from "../models/templateModels.ts";
import {createSlice} from "@reduxjs/toolkit";
import DocumentsApi from "../api/documentsApi.ts";

export const createTemplate = createAsyncThunkWithErrorHandler(
    "createTemplate/createTemplate",
    async (model: ChangeTemplate) => {
        return TemplatesApi.createTemplate(model);
    }
);

export const getTypes = createAsyncThunkWithErrorHandler(
    "createTemplate/getTypes",
    async () => {
        return TemplatesApi.getTypes();
    }
);

export const getTableColumns = createAsyncThunkWithErrorHandler(
    "createTemplate/getTableColumns",
    async () => {
        return TemplatesApi.getTableColumns();
    }
);

export const getPreview = createAsyncThunkWithErrorHandler(
    "createTemplate/getPreview",
    async (model: ChangeTemplate) => {
        return DocumentsApi.getPreview(model);
    }
);

const initialState: CreateTemplatePageState = {
    model: {
        name: '',
        typeCode: TemplateTypeCode.productCatalog,
        content: {
            groupByType: false,
            tableColumns: []
        }
    },
    types: [],
    columnGroups: [],
    preview: '',
    loading: false,
    success: undefined,
    error: undefined
};

const createTemplateSlice = createSlice({
    name: "create-template",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(createTemplate.pending, (state) => {
                state.loading = true;
            })
            .addCase(createTemplate.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(createTemplate.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getTypes.pending, (state) => {
                state.loading = true;
            })
            .addCase(getTypes.fulfilled, (state, {payload}) => {
                state.types = payload;
                state.loading = false;
            })
            .addCase(getTypes.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getTableColumns.pending, (state) => {
                state.loading = true;
            })
            .addCase(getTableColumns.fulfilled, (state, {payload}) => {
                state.columnGroups = payload;
                state.loading = false;
            })
            .addCase(getTableColumns.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getPreview.pending, (state) => {
                state.loading = true;
            })
            .addCase(getPreview.fulfilled, (state, {payload}) => {
                state.preview = payload;
                state.loading = false;
            })
            .addCase(getPreview.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    setSuccess
} = createTemplateSlice.actions;

export default createTemplateSlice.reducer;