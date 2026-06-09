import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import {type ChangeOrderProduct, type ChangeOrderState, OrderStepperStep} from "../../../models/orderModels.ts";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {useNavigate, useParams} from "react-router-dom";
import {
    addExistedProduct,
    changeOrder,
    deleteProduct,
    getOrder,
    getProductsToOrder,
    getProductTypes,
    updateProducts
} from "../../../slices/ChangeOrderSlice.ts";
import type {ProductToOrder} from "../../../models/productsToOrderModels.ts";
import {useEffect} from "react";
import type {StepperItem} from "../../../../shared/models/models.ts";

const steps: StepperItem<OrderStepperStep>[] = [
    {id: OrderStepperStep.products, label: 'Выбор товаров'},
    {id: OrderStepperStep.amount, label: 'Настройка количества'},
    {id: OrderStepperStep.confirm, label: 'Подтверждение'}
];

const useChangeOrderPage = () => {
    const state = useAppSelector<ChangeOrderState>(
        (state) => state.changeOrder
    );

    const dispatch = useAppDispatch();
    const notification = useNotify();
    const navigate = useNavigate();
    const params = useParams();

    const handleSubmit = async () => {
        await dispatch(changeOrder({id: params.id, model: {products: state.model.products}}));
        navigate("/orders");
    }

    const handleMoveToOrder = (product: ProductToOrder) => {
        dispatch(addExistedProduct(product));
    }

    const handleDeleteProduct = (id?: string) => {
        dispatch(deleteProduct({id: id}));
    }

    const handleUpdateProducts = (products: ChangeOrderProduct[]) => {
        dispatch(updateProducts(products));
    }

    useEffect(() => {
        if (state.error) {
            notification.notifyError(state.error.toString());
        }
    }, [notification, state.error]);

    useEffect(() => {
        if (!params.id) {
            navigate('/orders');
        }
        dispatch(getOrder(params.id!));
        dispatch(getProductsToOrder({supplierId: state.order?.supplier?.id}));
        dispatch(getProductTypes());
    }, [dispatch, navigate]);

    return {
        steps: steps,
        model: state.model,
        products: state.products,
        handleSubmit,
        handleMoveToOrder,
        handleDeleteProduct,
        handleUpdateProducts,
        loading: state.loading,
    };
}

export default useChangeOrderPage;