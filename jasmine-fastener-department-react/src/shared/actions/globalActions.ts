import {type ActionReducerMapBuilder, createAction} from "@reduxjs/toolkit";
import type {StateBase} from "../models/models.ts";

export const clearPageStatuses = createAction<{ sliceName: string }>("global/clearPageStatuses");

export const addBaseCases = (builder: ActionReducerMapBuilder<any>, currentSliceName: string) => {
    builder.addCase(clearPageStatuses, (state: StateBase, action) => {
        if (action.payload.sliceName === currentSliceName) {
            state.success = undefined;
            state.error = undefined;
        }
    });
};