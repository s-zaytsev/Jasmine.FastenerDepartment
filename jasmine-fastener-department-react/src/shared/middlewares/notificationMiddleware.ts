import {type Middleware} from "@reduxjs/toolkit";
import {apiNotify} from "../providers/NotificationProvider.tsx";
import type {StateBase} from "../models/models.ts";

export const notificationMiddleware: Middleware = (store) => (next) => (action: any) => {
    const actionType = action.type as string;

    if (actionType.endsWith("/fulfilled") || actionType.endsWith("/rejected")) {
        const [sliceName, thunkName, status] = actionType.split("/");

        const state = store.getState()[sliceName] as StateBase;

        if (status === "rejected") {
            const error = action.payload.detail;
            if (error) apiNotify('error', error ?? '')
        }

        if (status === "fulfilled") {
            if (state.success) apiNotify("success", state.success ?? '');
        }
    }

    return next(action);
};