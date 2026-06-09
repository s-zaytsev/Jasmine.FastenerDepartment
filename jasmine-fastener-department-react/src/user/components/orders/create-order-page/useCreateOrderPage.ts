import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import {type ChangeOrderProduct, type CreateOrderState, OrderStepperStep} from "../../../models/orderModels.ts";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";
import {
    addExistedProduct,
    changeSupplier,
    createOrder,
    deleteProduct,
    getProductsToOrder,
    getProductTypes,
    getSuppliers,
    updateProducts
} from "../../../slices/CreateOrderSlice.ts";
import type {ProductToOrder} from "../../../models/productsToOrderModels.ts";
import type {Supplier} from "../../../models/supplierModels.ts";
import type {StepperItem} from "../../../../shared/models/models.ts";

const steps: StepperItem<OrderStepperStep>[] = [
    {id: OrderStepperStep.suppliers, label: 'Выбор поставщика'},
    {id: OrderStepperStep.products, label: 'Выбор товаров'},
    {id: OrderStepperStep.amount, label: 'Настройка количества'},
    {id: OrderStepperStep.confirm, label: 'Подтверждение'}
];

const useCreateOrderPage = () => {
    const state = useAppSelector<CreateOrderState>(
        (state) => state.createOrder
    );

    const dispatch = useAppDispatch();
    const notification = useNotify();
    const navigate = useNavigate();

    useEffect(() => {
        if (state.error) {
            notification.notifyError(state.error.toString());
        }
    }, [notification, state.error]);

    useEffect(() => {
        dispatch(getSuppliers());
        dispatch(getProductTypes());
        dispatch(getProductsToOrder({}))
    }, [dispatch]);


    const handleSubmit = async () => {
        try {
            await dispatch(createOrder({
                supplierId: state.model.supplier?.id,
                products: state.model.products
            }));
            navigate("/orders");
        } catch {
            notification.notifyError('Ошибка создания заказа')
        }
    }

    const handleMoveToOrder = (product: ProductToOrder) => {
        dispatch(addExistedProduct(product));
    }

    const handleUpdateProducts = (products: ChangeOrderProduct[]) => {
        dispatch(updateProducts(products));
    }

    const handleDeleteProduct = (id?: string) => {
        dispatch(deleteProduct({id: id}));
    }

    const handleChangeSupplier = (supplier?: Supplier) => {
        dispatch(changeSupplier({supplier}));
        dispatch(getProductsToOrder({supplierId: supplier?.id}));
    };

    return {
        steps: steps,
        model: state.model,
        products: state.products,
        suppliers: state.suppliers,
        handleSubmit,
        handleMoveToOrder,
        handleDeleteProduct,
        handleUpdateProducts,
        handleChangeSupplier,
        loading: state.loading,
    };
}

export default useCreateOrderPage;