import Loader from "../../../../shared/components/Loader.tsx";
import Page from "../../../../shared/components/layout/Page.tsx";
import OrderDetailsGrid from "./OrderDetailsGrid.tsx";
import OrderDetailsHeader from "./OrderDetailsHeader.tsx";
import {Box} from "@mui/material";
import useOrderDetailsPage from "./useOrderDetailsPage.ts";

const OrderDetailsPage = () => {

    const {
        order,
        productTypes,
        loading,
        handleDownload
    } = useOrderDetailsPage();

    if (loading) {
        return <Loader text={'Загрузка данных заказа'}/>;
    }

    return (
        <Page
            title={'Детали заказа'}
            description={'Информация о товарах в заказе'}
        >
            <Box className={'w-full flex justify-center'}>
                <Box className={'w-[70%]'}>
                    <OrderDetailsHeader
                        order={order}
                        onDownload={handleDownload}
                    />

                    <OrderDetailsGrid
                        orderStatusCode={order?.statusCode}
                        products={order?.products}
                        productTypes={productTypes}
                    />
                </Box>
            </Box>
        </Page>
    )
}

export default OrderDetailsPage;