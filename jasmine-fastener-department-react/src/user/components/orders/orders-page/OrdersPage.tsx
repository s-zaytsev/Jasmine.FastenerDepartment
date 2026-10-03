import {Box} from "@mui/material";
import Loader from "../../../../shared/components/Loader.tsx";
import Page from "../../../../shared/components/layout/Page.tsx";
import CompletedOrdersGrid from "./complete-orders/CompletedOrdersGrid.tsx";
import CancelOrderDialog from "../dialogs/CancelOrderDialog.tsx";
import ActiveOrdersGrid from "./active-orders/ActiveOrdersGrid.tsx";
import Section from "../../../../shared/components/section/Section.tsx";
import useOrderPage from "./useOrderPage.ts";

const OrdersPage = () => {
    const {
        activeOrders,
        completedOrders,
        selectedOrder,
        loading,
        isCancelDialogOpen,
        handleCancelDialogClose,
        handleNavigateToCreate,
        handleOpenDialogToCancel,
        handleChangeOrder,
        handleCompleteOrder,
        handleNavigateToDetails,
        handleCancelOrder,
        handleNavigateToSend
    } = useOrderPage();

    if (loading) {
        return <Loader text={'Загрузка списка заказа'}/>;
    }
    return (
        <Page
            title={'Заказы'}
            description={'Список заказов'}
            button={{
                label: 'Создать',
                onClick: handleNavigateToCreate
            }}>

            <Box className={'flex flex-col'}>

                <Box className={'w-full'}>
                    <Section title={'Активные заказы'}>
                        <ActiveOrdersGrid
                            page={activeOrders}
                            onChange={handleChangeOrder}
                            onCancel={handleOpenDialogToCancel}
                            onComplete={handleCompleteOrder}
                            onSend={handleNavigateToSend}
                            onNavigateToDetails={handleNavigateToDetails}
                        />
                    </Section>
                </Box>

                <hr className={'w-[1px] h-full mx-[2rem]'}/>

                <Box className={'w-full mt-[2rem]'}>
                    <Section title={'Завершенные заказы'}>
                        <CompletedOrdersGrid
                            page={completedOrders}
                            onNavigateToDetails={handleNavigateToDetails}
                        />
                    </Section>
                </Box>

            </Box>

            <CancelOrderDialog
                order={selectedOrder!}
                open={isCancelDialogOpen}
                onClose={handleCancelDialogClose}
                onSubmit={handleCancelOrder}
            />
        </Page>
    )
}

export default OrdersPage;