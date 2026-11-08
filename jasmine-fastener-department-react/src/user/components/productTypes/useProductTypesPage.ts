import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
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
        await dispatch(createProductType(model));
        handleClose();
        await dispatch(getExtendedProductTypes());
    }, [dispatch]);

    const handleChange = useCallback(async (model: ChangeProductType) => {
        const id = state.selectedProductType?.id;
        if (!id) return;

        await dispatch(changeProductType({id, model}));
        handleClose();
        await dispatch(getExtendedProductTypes());
    }, [dispatch, state.selectedProductType?.id]);

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
        isSubmitting: state.isSubmitting,
        loading: state.loading,
        productTypes: state.productTypes,
        selectedProductType: state.selectedProductType
    }
}

export default useProductTypesPage;