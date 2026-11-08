import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useCallback, useEffect, useState} from "react";
import {
    changeRecipient,
    clearSelectedRecipient,
    createRecipient,
    getRecipients,
    selectRecipient
} from "../../slices/RecipientsSlice.ts";
import type {ChangeRecipient, RecipientsPageState} from "../../models/recipientModels.ts";

const useRecipientsPage = () => {
    const state = useAppSelector<RecipientsPageState>(
        (state) => state.recipients
    );

    const dispatch = useAppDispatch();

    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleOpenDialogToCreate = useCallback(() => {
        dispatch(clearSelectedRecipient());
        handleOpen();
    }, [dispatch]);

    const handleOpenDialogToChange = useCallback((id: string) => {
        dispatch(selectRecipient({id}));
        handleOpen();
    }, [dispatch]);

    const handleCreate = useCallback(async (model: ChangeRecipient) => {
        await dispatch(createRecipient(model));
        handleClose();
        await dispatch(getRecipients());
    }, [dispatch]);

    const handleChange = useCallback(async (model: ChangeRecipient) => {
        const id = state.selected?.id;
        if (!id) return;

        await dispatch(changeRecipient({id, model}));
        handleClose();
        await dispatch(getRecipients());
    }, [dispatch, state.selected?.id]);

    useEffect(() => {
        dispatch(getRecipients());
    }, [dispatch]);

    return {
        open,
        handleOpenDialogToCreate,
        handleOpenDialogToChange,
        handleCreate,
        handleChange,
        handleClose,
        loading: state.loading,
        isSubmitting: state.isSubmitting,
        recipients: state.recipients,
        selected: state.selected
    };
}

export default useRecipientsPage;