import {Box} from "@mui/material";
import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import {useEffect, useState} from "react";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import Loader from "../../../../shared/components/Loader.tsx";
import type {CancelOrder, OrderPageState} from "../../../models/orderModels.ts";
import Page from "../../../../shared/components/layout/Page.tsx";
import FilledButton from "../../../../shared/components/buttons/FilledButton.tsx";
import {useNavigate} from "react-router-dom";
import {
    cancelOrder,
    clearSelectedOrder,
    getActiveOrdersPage,
    getCompletedOrdersPage,
    selectOrder
} from "../../../slices/OrdersSlice.ts";
import CompletedOrdersGrid from "./complete-orders/CompletedOrdersGrid.tsx";
import CancelOrderDialog from "../dialogs/CancelOrderDialog.tsx";
import ActiveOrdersGrid from "./active-orders/ActiveOrdersGrid.tsx";
import Section from "../../../../shared/components/section/Section.tsx";

const OrdersPage = () => {
    const state = useAppSelector<OrderPageState>(
        (state) => state.orders
    );
    const dispatch = useAppDispatch();
    const notification = useNotify();
    const navigate = useNavigate();

    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    function handleNavigateToCreate() {
        navigate("create");
    }

    function handleOpenDialogToCancel(id: string) {
        dispatch(selectOrder({id}))
        handleOpen();
    }

    function handleChangeOrder(id: string) {
        navigate(`${id}/change`);
    }

    function handleCompleteOrder(id: string) {
        navigate(`${id}/complete`);
    }

    function handleSendOrder(id: string) {
        console.log(id);
        alert('Функция не имплементированна')
    }

    function handleNavigateToDetails(id: string) {
        navigate(id);
    }

    async function handleCancelOrder(model: CancelOrder) {
        handleClose();
        await dispatch(cancelOrder({id: state.selectedOrder?.id, model: model}));
        dispatch(clearSelectedOrder());
        await dispatch(getCompletedOrdersPage(state.completedOrdersQuery));
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

    if (state.loading) {
        return <Loader text={'Загрузка списка заказа'}/>;
    }
    return (
        <Page>
            <Box className={"flex justify-end w-full mb-[1rem]"}>
                <FilledButton onClick={handleNavigateToCreate} variant="contained">
                    Создать
                </FilledButton>
            </Box>

            <Box className={'flex flex-col'}>

                <Box className={'w-full'}>
                    <Section title={'Активные заказы'}>
                        <ActiveOrdersGrid
                            page={state.activeOrders}
                            onChange={handleChangeOrder}
                            onCancel={handleOpenDialogToCancel}
                            onComplete={handleCompleteOrder}
                            onSend={handleSendOrder}
                            onNavigateToDetails={handleNavigateToDetails}
                        />
                    </Section>
                </Box>

                <hr className={'w-[1px] h-full mx-[2rem]'}/>

                <Box className={'w-full mt-[2rem]'}>
                    <Section title={'Завершенные заказы'}>
                        <CompletedOrdersGrid
                            page={state.completedOrders}
                            onNavigateToDetails={handleNavigateToDetails}
                        />
                    </Section>
                </Box>

            </Box>

            <CancelOrderDialog
                order={state.selectedOrder!}
                open={open}
                onClose={handleClose}
                onSubmit={handleCancelOrder}
            />
        </Page>
    )
}

export default OrdersPage;