import {createAsyncThunkWithErrorHandler} from "../../shared/thunks/createAsyncThunkWithErrorHandler.ts";
import {createSlice, type PayloadAction} from "@reduxjs/toolkit";
import type {ChangeCompanySettings, ChangeEmailSettings, SettingsPageState} from "../models/settingsModels.ts";
import SettingsApi from "../api/settingsApi.ts";

export const getCompanySettings = createAsyncThunkWithErrorHandler(
    "settings/getCompanySettings",
    async () => {
        return SettingsApi.getCompanySettings();
    }
);

export const changeCompanySettings = createAsyncThunkWithErrorHandler(
    "settings/changeCompanySettings",
    async (model: ChangeCompanySettings) => {
        return SettingsApi.changeCompanySettings(model);
    }
);

export const getEmailSettings = createAsyncThunkWithErrorHandler(
    "settings/getEmailSettings",
    async () => {
        return SettingsApi.getEmailSettings();
    }
);

export const changeEmailSettings = createAsyncThunkWithErrorHandler(
    "settings/changeEmailSettings",
    async (model: ChangeEmailSettings) => {
        return SettingsApi.changeEmailSettings(model);
    }
);


const initialState: SettingsPageState = {
    emailSettings: {
        smtpPort: 0,
        smtpUrl: '',
        userName: '',
        password: '',
        displayName: ''
    },
    companySettings: {
        title: '',
        subTitle: ''
    },
    loading: false,
    success: undefined,
    error: undefined
};

const settingsSlice = createSlice({
    name: "settings",
    initialState: initialState,
    reducers: {
        updateCompanySettings: (state, action: PayloadAction<ChangeCompanySettings>) => {
            state.companySettings = {...state.companySettings, ...action.payload};
        },
        updateEmailSettings: (state, action: PayloadAction<ChangeEmailSettings>) => {
            state.emailSettings = {...state.emailSettings, ...action.payload};
        },

        setSuccess: (state, action) => {
            state.success = action.payload;
        }
    },
    extraReducers: builder => {
        builder
            .addCase(getCompanySettings.pending, (state) => {
                state.loading = true;
            })
            .addCase(getCompanySettings.fulfilled, (state, {payload}) => {
                state.companySettings = payload;
                state.loading = false;
            })
            .addCase(getCompanySettings.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(getEmailSettings.pending, (state) => {
                state.loading = true;
            })
            .addCase(getEmailSettings.fulfilled, (state, {payload}) => {
                state.emailSettings = payload;
                state.loading = false;
            })
            .addCase(getEmailSettings.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeCompanySettings.pending, (state) => {
                state.loading = true;
            })
            .addCase(changeCompanySettings.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(changeCompanySettings.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });

        builder
            .addCase(changeEmailSettings.pending, (state) => {
                state.loading = true;
            })
            .addCase(changeEmailSettings.fulfilled, (state) => {
                state.loading = false;
            })
            .addCase(changeEmailSettings.rejected, (state, action) => {
                state.error = action.payload ?? action.error;
                state.loading = false;
            });
    }
});

export const {
    updateCompanySettings,
    updateEmailSettings,
    setSuccess
} = settingsSlice.actions;

export default settingsSlice.reducer;