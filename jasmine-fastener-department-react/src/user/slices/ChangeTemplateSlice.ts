import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import {type ChangeTemplate, type ChangeTemplatePageState, TemplateTypeCode} from "../models/templateModels.ts";
import TemplatesApi from "../api/templatesApi.ts";
import DocumentsApi from "../api/documentsApi.ts";
import {createSlice} from "@reduxjs/toolkit";

export const getTemplate = createAsyncThunkWithErrorHandler(
    "changeTemplate/getTemplate",
    async (id: string) => {
        return TemplatesApi.getTemplate(id);
    }
);

export const changeTemplate = createAsyncThunkWithErrorHandler(
    "changeTemplate/changeTemplate",
    async ({id, model}: any) => {
        return TemplatesApi.changeTemplate(id, model);
    }
);

export const getTypes = createAsyncThunkWithErrorHandler(
    "changeTemplate/getTypes",
    async () => {
        return TemplatesApi.getTypes();
    }
);

export const getTableColumns = createAsyncThunkWithErrorHandler(
    "changeTemplate/getTableColumns",
    async () => {
        return TemplatesApi.getTableColumns();
    }
);

export const getPreview = createAsyncThunkWithErrorHandler(
    "changeTemplate/getPreview",
    async (model: ChangeTemplate) => {
        return DocumentsApi.getPreview(model);
    }
);

const initialState: ChangeTemplatePageState = {
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

const changeTemplateSlice = createSlice({
    name: "change-template",
    initialState: initialState,
    reducers: {
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getTemplate.pending, (state) => {
                state.loading = true;
            })
            .addCase(getTemplate.fulfilled, (state, {payload}) => {
                state.model = {typeCode: payload.type.id, content: payload.content, name: payload.name};
                state.loading = false;
            })
            .addCase(getTemplate.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeTemplate.pending, (state) => {
                state.loading = true;
            })
            .addCase(changeTemplate.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(changeTemplate.rejected, (state, action) => {
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
} = changeTemplateSlice.actions;

export default changeTemplateSlice.reducer;