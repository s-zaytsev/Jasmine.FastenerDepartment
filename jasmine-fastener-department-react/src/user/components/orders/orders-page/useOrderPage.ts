import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {CancelOrder, OrderPageState} from "../../../models/orderModels.ts";
import {useNavigate} from "react-router-dom";
import {useCallback, useEffect, useState} from "react";
import {
    cancelOrder,
    clearSelectedOrder,
    getActiveOrdersPage,
    getCompletedOrdersPage,
    selectOrder
} from "../../../slices/OrdersSlice.ts";

const useOrderPage = () => {
    const state = useAppSelector<OrderPageState>(
        (state) => state.orders
    );
    const dispatch = useAppDispatch();
    const navigate = useNavigate();

    const [isCancelDialogOpen, setIsCancelDialogOpen] = useState(false);
    const handleCancelDialogOpen = useCallback(() => setIsCancelDialogOpen(true), []);
    const handleCancelDialogClose = useCallback(() => setIsCancelDialogOpen(false), []);

    const handleNavigateToCreate = useCallback(() => {
        navigate("create");
    }, [navigate]);

    const handleOpenDialogToCancel = useCallback((id: string) => {
        dispatch(selectOrder({id}))
        handleCancelDialogOpen();
    }, [dispatch, handleCancelDialogOpen]);

    const handleNavigateToSend = useCallback((id: string) => {
        navigate(`${id}/send`);
    }, [navigate])

    const handleChangeOrder = useCallback((id: string) => {
        navigate(`${id}/change`);
    }, [navigate]);

    const handleCompleteOrder = useCallback((id: string) => {
        navigate(`${id}/complete`);
    }, [navigate]);

    const handleNavigateToDetails = useCallback((id: string) => {
        navigate(id);
    }, [navigate]);

    const handleCancelOrder = useCallback(async (model: CancelOrder) => {
        handleCancelDialogClose();
        await dispatch(cancelOrder({id: state.selectedOrder?.id, model: model}));
        dispatch(clearSelectedOrder());
        await dispatch(getCompletedOrdersPage(state.completedOrdersQuery));
        await dispatch(getActiveOrdersPage(state.activeOrdersQuery));
    }, [dispatch, handleCancelDialogClose, state.activeOrdersQuery, state.completedOrdersQuery, state.selectedOrder?.id]);

    useEffect(() => {
        dispatch(getCompletedOrdersPage(state.completedOrdersQuery));
        dispatch(getActiveOrdersPage(state.activeOrdersQuery));
    }, [dispatch]);

    return {
        activeOrders: state.activeOrders,
        completedOrders: state.completedOrders,
        selectedOrder: state.selectedOrder,
        loading: state.loading,
        isCancelDialogOpen,
        handleCancelDialogClose,
        handleNavigateToCreate,
        handleOpenDialogToCancel,
        handleChangeOrder,
        handleCompleteOrder,
        handleNavigateToDetails,
        handleCancelOrder,
        handleNavigateToSend
    };
}

export default useOrderPage;