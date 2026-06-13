import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useNotify} from "../../../shared/providers/NotificationProvider.tsx";
import {useCallback, useEffect, useState} from "react";
import {
    changeProductType,
    clearSelectedProductType,
    createProductType,
    getExtendedProductTypes,
    selectProductType
} from "../../slices/ProductTypesSlice.ts";
import type {ChangeProductType, ProductTypesState} from "../../models/productTypeModels.ts";

const useProductTypesPage = () => {
    const state = useAppSelector<ProductTypesState>(
        (state) => state.productTypes
    );

    const dispatch = useAppDispatch();
    const notification = useNotify();

    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleOpenDialogToCreate = useCallback(() => {
        dispatch(clearSelectedProductType());
        handleOpen();
    }, [dispatch]);

    const handleOpenDialogToChange = useCallback((id: string) => {
        dispatch(selectProductType({id}));
        handleOpen();
    }, [dispatch]);

    const handleCreate = useCallback(async (model: ChangeProductType) => {
        handleClose();
        await dispatch(createProductType(model));
        await dispatch(getExtendedProductTypes());
    }, [dispatch]);

    const handleChange = useCallback(async (model: ChangeProductType) => {
        const id = state.selectedProductType?.id;

        if (!id) {
            return;
        }

        handleClose();
        await dispatch(changeProductType({id, model}));
        await dispatch(getExtendedProductTypes());
    }, [dispatch, state.selectedProductType?.id]);

    useEffect(() => {
        if (state.error) {
            notification.notifyError(state.error.toString());
        }
    }, [notification, state.error]);

    useEffect(() => {
        dispatch(getExtendedProductTypes());
    }, [dispatch]);

    return {
        open,
        handleOpenDialogToCreate,
        handleOpenDialogToChange,
        handleChange,
        handleCreate,
        handleClose,
        loading: state.loading,
        productTypes: state.productTypes,
        selectedProductType: state.selectedProductType
    }
}

export default useProductTypesPage;