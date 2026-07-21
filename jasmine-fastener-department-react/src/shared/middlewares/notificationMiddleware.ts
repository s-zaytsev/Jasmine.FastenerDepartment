import {type Middleware} from "@reduxjs/toolkit";
import {apiNotify} from "../providers/NotificationProvider.tsx";
import type {StateBase} from "../models/models.ts";
import {clearPageStatuses} from "../actions/globalActions.ts";

export const notificationMiddleware: Middleware = (store) => (next) => (action: any) => {
    const actionType = action.type as string;

    if (actionType.endsWith("/fulfilled") || actionType.endsWith("/rejected")) {
        const actionTypeStr = actionType.split("/");
        const sliceName = actionTypeStr[0];
        const status = actionTypeStr[2];

        const state = store.getState()[sliceName] as StateBase;

        if (status === "rejected") {
            const error = action.payload.detail;
            if (error) apiNotify('error', error ?? '')
        } else if (status === "fulfilled") {
            if (state.success) apiNotify("success", state.success ?? '');
        }

        store.dispatch(clearPageStatuses({sliceName}));
    }

    return next(action);
};