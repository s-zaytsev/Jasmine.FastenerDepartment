import {useNavigate} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {ChangeProduct, CreateProductPageState} from "../../../models/productModel.ts";
import {getLastId, getProductTypes, getSuppliers, saveProduct} from "../../../slices/CreateProductSlice.ts";
import {useCallback, useEffect} from "react";

const useCreateProductPage = () => {
    const navigate = useNavigate();
    const dispatch = useAppDispatch();

    const state = useAppSelector<CreateProductPageState>(
        (state) => state.createProduct
    );

    const handleSubmit = useCallback(async (model: ChangeProduct) => {
        await dispatch(saveProduct(model)).unwrap();
        navigate('/');
    }, [dispatch, navigate]);

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
        handleSubmit
    };
}

export default useCreateProductPage;