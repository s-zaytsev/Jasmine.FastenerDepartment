import {useNavigate} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import type {ChangeProduct, CreateProductPageState} from "../../../models/productModel.ts";
import {
    changeProduct,
    getLastId,
    getProductTypes,
    getSuppliers,
    saveProduct,
    setError,
    setSuccess
} from "../../../slices/CreateProductSlice.ts";
import {useEffect} from "react";
import type {AxiosError} from "axios";

const useCreateProductPage = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const notification = useNotify();

    const state = useAppSelector<CreateProductPageState>(
        (state) => state.createProduct
    );

    function handleFormChanged(model: ChangeProduct) {
        dispatch(changeProduct(model));
    }

    function handleCreate() {
        dispatch(saveProduct(state.model));
    }

    useEffect(() => {
        if (state.success) {
            notification.notifySuccess(state.success);
            dispatch(setSuccess(undefined));
            navigate('/');
        }
    }, [state.success, navigate, notification, dispatch]);

    useEffect(() => {
        if (state.error) {
            notification.notifyError((state.error as AxiosError).message);
            dispatch(setError(undefined));
        }
    }, [state.error, navigate, notification, dispatch]);

    useEffect(() => {
        dispatch(getLastId());
        dispatch(getSuppliers());
        dispatch(getProductTypes());
    }, [dispatch])

    return {
        model: state.model,
        suppliers: state.suppliers,
        productTypes: state.productTypes,
        loading: state.loading,
        handleFormChanged,
        handleCreate
    };
}

export default useCreateProductPage;