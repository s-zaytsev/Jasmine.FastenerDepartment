import {useNavigate, useParams} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import {type SyntheticEvent, useCallback, useEffect, useState} from "react";
import {type ChangeProduct, type ChangeProductPageState} from "../../../models/productModel.ts";
import {getProduct, getProductTypes, getSuppliers, save} from "../../../slices/ChangeProductSlice.ts";


const useChangeProductPage = () => {
    const navigate = useNavigate();
    const params = useParams();
    const dispatch = useAppDispatch();
    const [tabIndex, setTabIndex] = useState(0);

    const state = useAppSelector<ChangeProductPageState>(
        (state) => state.changeProduct
    );

    const handleChangeTabIndex = useCallback((_event: SyntheticEvent, value: number) => {
        setTabIndex(value);
    }, []);

    const handleSubmit = useCallback(async (model: ChangeProduct) => {
        await dispatch(save({id: params.id, model: model})).unwrap();
        navigate('/');
    }, [dispatch, navigate, params.id]);

    useEffect(() => {
        if (!params.id) {
            navigate('/products')
            return;
        }

        dispatch(getSuppliers());
        dispatch(getProductTypes());
        dispatch(getProduct(params.id!));
    }, [params.id, dispatch, navigate]);

    return {
        model: state.model,
        historyEntries: state.historyEntries,
        suppliers: state.suppliers,
        productTypes: state.productTypes,
        loading: state.loading,
        tabIndex: tabIndex,
        handleChangeTabIndex,
        handleSubmit,
    };
}

export default useChangeProductPage;