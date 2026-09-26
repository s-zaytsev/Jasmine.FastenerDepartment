import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import RecipientsApi from "../api/recipientsApi.ts";
import type {ChangeRecipient, RecipientsPageState} from "../models/recipientModels.ts";

export const getRecipients = createAsyncThunkWithErrorHandler(
    "recipients/getRecipients",
    async () => {
        return RecipientsApi.getRecipients();
    }
);

export const createRecipient = createAsyncThunkWithErrorHandler(
    "recipients/createRecipient",
    async (model: ChangeRecipient) => {
        return RecipientsApi.createRecipient(model);
    }
);

export const changeRecipient = createAsyncThunkWithErrorHandler(
    "recipients/changeRecipient",
    async ({id, model}: any) => {
        return RecipientsApi.changeRecipient(id, model);
    }
);

const initialState: RecipientsPageState = {
    recipients: [],
    selected: undefined,
    loading: false,
    success: undefined,
    error: undefined
};

const recipientsSlice = createSlice({
    name: "recipients",
    initialState: initialState,
    reducers: {
        selectRecipient: (state, action: PayloadAction<{ id: string }>) => {
            state.selected = state.recipients.find(x => x.id == action.payload.id);
        },
        clearSelectedRecipient: (state) => {
            state.selected = undefined;
        },
        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
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
            .addCase(createRecipient.pending, (state) => {
                state.loading = true;
            })
            .addCase(createRecipient.fulfilled, (state) => {
                state.success = 'Контакт создан';
                state.loading = false;
            })
            .addCase(createRecipient.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeRecipient.pending, (state) => {
                state.loading = true;
            })
            .addCase(changeRecipient.fulfilled, (state) => {
                state.success = 'Контакт обновлен';
                state.loading = false;
            })
            .addCase(changeRecipient.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    selectRecipient,
    clearSelectedRecipient,
    setSuccess
} = recipientsSlice.actions;

export default recipientsSlice.reducer;