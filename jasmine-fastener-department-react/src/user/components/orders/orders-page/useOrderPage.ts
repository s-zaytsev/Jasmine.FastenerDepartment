import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {CancelOrder, OrderPageState, SendOrder} from "../../../models/orderModels.ts";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {useNavigate} from "react-router-dom";
import {useCallback, useEffect, useState} from "react";
import {
    cancelOrder,
    clearSelectedOrder,
    getActiveOrdersPage,
    getCompletedOrdersPage,
    selectOrder,
    sendOrder
} from "../../../slices/OrdersSlice.ts";

const useOrderPage = () => {
    const state = useAppSelector<OrderPageState>(
        (state) => state.orders
    );
    const dispatch = useAppDispatch();
    const notification = useNotify();
    const navigate = useNavigate();

    const [isCancelDialogOpen, setIsCancelDialogOpen] = useState(false);
    const handleCancelDialogOpen = useCallback(() => setIsCancelDialogOpen(true), []);
    const handleCancelDialogClose = useCallback(() => setIsCancelDialogOpen(false), []);

    const [isSendDialogOpen, setIsSendDialogOpen] = useState(false);
    const handleSendDialogOpen = useCallback(() => setIsSendDialogOpen(true), []);
    const handleSendDialogClose = useCallback(() => setIsSendDialogOpen(false), []);

    const handleNavigateToCreate = useCallback(() => {
        navigate("create");
    }, [navigate]);

    const handleOpenDialogToCancel = useCallback((id: string) => {
        dispatch(selectOrder({id}))
        handleCancelDialogOpen();
    }, [dispatch, handleCancelDialogOpen]);

    const handleOpenDialogToSend = useCallback((id: string) => {
        dispatch(selectOrder({id}))
        handleSendDialogOpen();
    }, [dispatch, handleSendDialogOpen]);

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

    const handleSendOrder = useCallback(async (model: SendOrder) => {
        handleSendDialogClose();
        await dispatch(sendOrder({id: state.selectedOrder?.id, model: model}));
        dispatch(clearSelectedOrder());
        await dispatch(getActiveOrdersPage(state.activeOrdersQuery));
    }, [dispatch, handleSendDialogClose, state.activeOrdersQuery, state.selectedOrder?.id]);

    useEffect(() => {
        if (state.error) {
            notification.notifyError(state.error.toString());
        }
    }, [notification, state.error]);

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
        isSendDialogOpen,
        handleCancelDialogClose,
        handleSendDialogClose,
        handleNavigateToCreate,
        handleOpenDialogToCancel,
        handleOpenDialogToSend,
        handleChangeOrder,
        handleCompleteOrder,
        handleNavigateToDetails,
        handleCancelOrder,
        handleSendOrder
    };
}

export default useOrderPage;