import {useNavigate, useParams} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import {type SyntheticEvent, useEffect, useState} from "react";
import type {ChangeProduct, ChangeProductPageState, Product} from "../../../models/productModel.ts";
import {changeProduct, getProduct, getProductTypes, getSuppliers, save} from "../../../slices/ChangeProductSlice.ts";

const useChangeProductPage = () => {
    const navigate = useNavigate();
    const params = useParams();
    const dispatch = useAppDispatch();
    const [tabIndex, setTabIndex] = useState(0);

    const state = useAppSelector<ChangeProductPageState>(
        (state) => state.changeProduct
    );

    function handleFormChanged(model: ChangeProduct) {
        dispatch(changeProduct(model));
    }

    const handleChangeTabIndex = (_event: SyntheticEvent, value: number) => {
        setTabIndex(value);
    };

    async function handleSubmit() {
        await dispatch(save({id: params.id, model: state.model})).unwrap();
        navigate('/');
    }

    useEffect(() => {
        if (!params.id) {
            navigate('/products')
            return;
        }

        dispatch(getSuppliers());
        dispatch(getProductTypes());
        dispatch(getProduct(params.id!));
    }, [params.id, dispatch, navigate])

    const product: Product = {
        number: state.model.number,
        price: state.model.price,
        isHardwareSizeEnabled: state.model.isHardwareSizeEnabled,
        measurementUnitCode: state.model.measurementUnitCode,
        name: state.model.name,
        priceTagCode: state.model.priceTagCode,
        id: state.product?.id || '',
        isNeededToOrder: state.model.isNeededToOrder,
        isNeededToPrint: state.model.isNeededToPrint,
        historyEntries: state.product?.historyEntries || [],
        suppliers: state.suppliers || []
    }

    return {
        product: product,
        model: state.model,
        suppliers: state.suppliers,
        productTypes: state.productTypes,
        loading: state.loading,
        tabIndex: tabIndex,
        handleFormChanged,
        handleChangeTabIndex,
        handleSubmit,
    };
}

export default useChangeProductPage;