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
import {useCallback, useEffect} from "react";
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

    const handleMoveToOrder = useCallback((product: ProductToOrder) => {
        dispatch(addExistedProduct(product));
    }, [dispatch]);

    const handleDeleteProduct = useCallback((id?: string) => {
        dispatch(deleteProduct({id: id}));
    }, [dispatch]);

    const handleUpdateProducts = useCallback((products: ChangeOrderProduct[]) => {
        dispatch(updateProducts(products));
    }, [dispatch]);

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