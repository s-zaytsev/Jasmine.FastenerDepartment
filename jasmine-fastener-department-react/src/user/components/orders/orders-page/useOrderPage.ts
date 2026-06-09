import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {CancelOrder, OrderPageState, SendOrder} from "../../../models/orderModels.ts";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";
import {
    cancelOrder,
    clearSelectedOrder,
    getActiveOrdersPage,
    getCompletedOrdersPage,
    selectOrder, sendOrder
} from "../../../slices/OrdersSlice.ts";

const useOrderPage = () => {
    const state = useAppSelector<OrderPageState>(
        (state) => state.orders
    );
    const dispatch = useAppDispatch();
    const notification = useNotify();
    const navigate = useNavigate();

    const [isCancelDialogOpen, setIsCancelDialogOpen] = useState(false);
    const handleCancelDialogOpen = () => setIsCancelDialogOpen(true);
    const handleCancelDialogClose = () => setIsCancelDialogOpen(false);

    const [isSendDialogOpen, setIsSendDialogOpen] = useState(false);
    const handleSendDialogOpen = () => setIsSendDialogOpen(true);
    const handleSendDialogClose = () => setIsSendDialogOpen(false);

    const handleNavigateToCreate = () => {
        navigate("create");
    }

    const handleOpenDialogToCancel = (id: string) => {
        dispatch(selectOrder({id}))
        handleCancelDialogOpen();
    }

    const handleOpenDialogToSend = (id: string) => {
        dispatch(selectOrder({id}))
        handleSendDialogOpen();
    }

    const handleChangeOrder = (id: string) => {
        navigate(`${id}/change`);
    }

    const handleCompleteOrder = (id: string) => {
        navigate(`${id}/complete`);
    }

    const handleNavigateToDetails = (id: string) => {
        navigate(id);
    }

    async function handleCancelOrder(model: CancelOrder) {
        handleCancelDialogClose();
        await dispatch(cancelOrder({id: state.selectedOrder?.id, model: model}));
        dispatch(clearSelectedOrder());
        await dispatch(getCompletedOrdersPage(state.completedOrdersQuery));
        await dispatch(getActiveOrdersPage(state.activeOrdersQuery));
    }

    async function handleSendOrder(model: SendOrder) {
        handleSendDialogClose();
        await dispatch(sendOrder({id: state.selectedOrder?.id, model: model}));
        dispatch(clearSelectedOrder());
        await dispatch(getActiveOrdersPage(state.activeOrdersQuery));
    }

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