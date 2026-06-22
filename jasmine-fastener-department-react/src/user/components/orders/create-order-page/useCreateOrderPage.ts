import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import {type ChangeOrderProduct, type CreateOrderState, OrderStepperStep} from "../../../models/orderModels.ts";
import {useNavigate} from "react-router-dom";
import {useCallback, useEffect} from "react";
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
    const navigate = useNavigate();

    useEffect(() => {
        dispatch(getSuppliers());
        dispatch(getProductTypes());
        dispatch(getProductsToOrder({supplierId: state.model.supplier?.id}))
    }, [dispatch]);


    const handleSubmit = useCallback(async () => {
        await dispatch(createOrder({
            supplierId: state.model.supplier?.id,
            products: state.model.products
        }));
        navigate("/orders");
    }, [dispatch]);

    const handleMoveToOrder = useCallback((product: ProductToOrder) => {
        return dispatch(addExistedProduct(product));
    }, [dispatch]);

    const handleUpdateProducts = useCallback((products: ChangeOrderProduct[]) => {
        return dispatch(updateProducts(products));
    }, [dispatch]);

    const handleDeleteProduct = useCallback((id?: string) => {
        dispatch(deleteProduct({id: id}));
    }, [dispatch]);

    const handleChangeSupplier = useCallback((supplier?: Supplier) => {
        dispatch(changeSupplier({supplier}));
        dispatch(getProductsToOrder({supplierId: supplier?.id}));
    }, [dispatch]);

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