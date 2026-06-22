import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {CompleteOrderForm, CompleteOrderState} from "../../../models/orderModels.ts";
import {useNavigate, useParams} from "react-router-dom";
import {completeOrder, getOrder, getProductTypes} from "../../../slices/CompleteOrderSlice.ts";
import {useEffect} from "react";

const useCompleteOrderPage = () => {
    const state = useAppSelector<CompleteOrderState>(
        (state) => state.completeOrder
    );

    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const params = useParams();

    const handleSubmit = async (formData: CompleteOrderForm) => {
        await dispatch(completeOrder({
            id: params.id,
            model: {
                comment: state.model.comment,
                products: formData.products
            }
        }))

        navigate("/orders");
    }

    useEffect(() => {
        if (!params.id) {
            navigate('/orders');
        }
        dispatch(getProductTypes());
        dispatch(getOrder(params.id!))
    }, [dispatch, navigate, params.id]);

    return {
        model: state.model,
        loading: state.loading,
        productTypes: state.productTypes,
        handleSubmit
    }
}

export default useCompleteOrderPage;