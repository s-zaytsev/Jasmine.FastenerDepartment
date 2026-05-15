import Page from "../../../../shared/components/layout/Page.tsx";
import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {CompleteOrderForm, CompleteOrderState} from "../../../models/orderModels.ts";
import {useNavigate, useParams} from "react-router-dom";
import {useEffect} from "react";
import {completeOrder, getOrder, getProductTypes} from "../../../slices/CompleteOrderSlice.ts";
import CompleteOrderProductForm from "./CompleteOrderProductForm.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {NotificationMessage} from "../../../../shared/models/notificationModel.ts";

const CompleteOrderPage = () => {
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
                    comment: state.completeOrderModel.comment,
                    products: formData.products
                }
            }))

            navigate("/orders");
        } catch(ex) {
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

    if (state.loading) {
        return <Loader text={'Загружаем данные заказа'}/>
    }

    return (
        <Page>
            <CompleteOrderProductForm
                completeOrderModel={state.completeOrderModel}
                productTypes={state.productTypes}
                onSubmit={handleSubmit}
            />
        </Page>
    )
}

export default CompleteOrderPage;