import {useAppDispatch, useAppSelector} from "../../../shared/hooks/sharedHooks.ts";
import {useNavigate} from "react-router-dom";
import {useCallback, useEffect, useState} from "react";
import {
    changeSupplier,
    clearSelectedSupplier,
    createSupplier,
    getExtendedSuppliers,
    selectSupplier
} from "../../slices/SuppliersSlice.ts";
import type {ChangeSupplierModel, SuppliersPageState} from "../../models/supplierModels.ts";

const useSuppliersPage = () => {
    const state = useAppSelector<SuppliersPageState>(
        (state) => state.suppliers
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleOpenDialogToCreate = useCallback(() => {
        dispatch(clearSelectedSupplier());
        handleOpen();
    }, [dispatch]);

    const handleOpenDialogToChange = useCallback((id: string) => {
        dispatch(selectSupplier({id}));
        handleOpen();
    }, [dispatch]);

    const handleCreate = useCallback(async (model: ChangeSupplierModel) => {
        handleClose();
        await dispatch(createSupplier(model));
        await dispatch(getExtendedSuppliers());
    }, [dispatch]);

    const handleChange = useCallback(async (model: ChangeSupplierModel) => {
        const id = state.selectedSupplier?.id;

        if (!id) {
            return;
        }

        handleClose();
        await dispatch(changeSupplier({id, model}));
        await dispatch(getExtendedSuppliers());
    }, [dispatch]);

    const handleNavigateToSupplierProducts = useCallback((id: string) => {
        navigate(id);
    }, [navigate]);

    useEffect(() => {
        dispatch(getExtendedSuppliers());
    }, [dispatch]);

    return {
        open,
        handleOpenDialogToCreate,
        handleOpenDialogToChange,
        handleCreate,
        handleChange,
        handleClose,
        handleNavigateToSupplierProducts,
        loading: state.loading,
        suppliers: state.suppliers,
        selectedSupplier: state.selectedSupplier
    };
}

export default useSuppliersPage;