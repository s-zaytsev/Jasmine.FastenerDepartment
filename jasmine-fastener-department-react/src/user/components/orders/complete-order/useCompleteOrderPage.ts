import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {CompleteOrderForm, CompleteOrderState} from "../../../models/orderModels.ts";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {useNavigate, useParams} from "react-router-dom";
import {completeOrder, getOrder, getProductTypes} from "../../../slices/CompleteOrderSlice.ts";
import {useEffect} from "react";
import {NotificationMessage} from "../../../../shared/models/notificationModel.ts";

const useCompleteOrderPage = () => {
    const state = useAppSelector<CompleteOrderState>(
        (state) => state.completeOrder
    );

    const dispatch = useAppDispatch();
    const notification = useNotify();
    const navigate = useNavigate();
    const params = useParams();

    const handleSubmit = async (formData: CompleteOrderForm) => {
        try {
            await dispatch(completeOrder({
                id: params.id,
                model: {
                    comment: state.model.comment,
                    products: formData.products
                }
            }))

            navigate("/orders");
        } catch (ex) {
            notification.notifyError('Ошибка завершения заказа')
        }
    }

    useEffect(() => {
        if (state.error) {
            const message = NotificationMessage.error(state.error!).message;
            notification.notifyError(message!);
        }
    }, [notification, state.error]);

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