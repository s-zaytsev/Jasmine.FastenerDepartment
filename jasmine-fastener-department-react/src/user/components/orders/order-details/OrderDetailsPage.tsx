import {useAppDispatch, useAppSelector} from "../../../../shared/hooks/sharedHooks.ts";
import type {OrderDetailsPageState} from "../../../models/orderModels.ts";
import {useNotify} from "../../../../shared/providers/NotificationProvider.tsx";
import {useNavigate, useParams} from "react-router-dom";
import {useEffect} from "react";
import Loader from "../../../../shared/components/Loader.tsx";
import Page from "../../../../shared/components/layout/Page.tsx";
import {downloadDocument, getOrder, getProductTypes} from "../../../slices/OrderDetailsSlice.ts";
import OrderDetailsGrid from "./OrderDetailsGrid.tsx";
import OrderDetailsHeader from "./OrderDetailsHeader.tsx";
import {Box} from "@mui/material";

const OrderDetailsPage = () => {
    const state = useAppSelector<OrderDetailsPageState>(
        (state) => state.orderDetails
    );
    const dispatch = useAppDispatch();
    const notification = useNotify();
    const params = useParams();
    const navigate = useNavigate();

    function handleDownload() {
        dispatch(downloadDocument(params.id!));
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
        dispatch(getProductTypes());
    }, [dispatch, params.id]);

    if (state.loading) {
        return <Loader text={'Загрузка данных заказа'}/>;
    }

    return (
        <Page>
            <Box className={'w-full flex justify-center'}>
                <Box className={'w-[70%]'}>
                    <OrderDetailsHeader
                        order={state.order}
                        onDownload={handleDownload}
                    />

                    <OrderDetailsGrid
                        orderStatusCode={state.order?.statusCode}
                        products={state.order?.products}
                        productTypes={state.productTypes}
                    />
                </Box>
            </Box>
        </Page>
    )
}

export default OrderDetailsPage;