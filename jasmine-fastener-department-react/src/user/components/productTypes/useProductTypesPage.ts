import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useNotify} from "../../../shared/providers/NotificationProvider.tsx";
import {useEffect, useState} from "react";
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

    const handleOpenDialogToCreate = () => {
        dispatch(clearSelectedProductType());
        handleOpen();
    }

    const handleOpenDialogToChange = (id: string) => {
        dispatch(selectProductType({id}));
        handleOpen();
    }

    const handleCreate = async (model: ChangeProductType) => {
        handleClose();
        await dispatch(createProductType(model));
        await dispatch(getExtendedProductTypes());
    }

    const handleChange = async (model: ChangeProductType) => {
        const id = state.selectedProductType?.id;

        if (!id) {
            return;
        }

        handleClose();
        await dispatch(changeProductType({id, model}));
        await dispatch(getExtendedProductTypes());
    }

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