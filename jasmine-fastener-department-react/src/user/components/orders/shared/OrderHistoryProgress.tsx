import {Box} from "@mui/material";
import {type Order, OrderStatusCode} from "../../../models/orderModels.ts";
import OrderHistoryProgressItem from "./OrderHistoryProgressItem.tsx";
import {AddBoxOutlined, DoneOutlined, MarkEmailRead} from "@mui/icons-material";

type OrderHistoryProgressProps = {
    order: Order;
    onSend: (id: string) => void;
    onComplete: (id: string) => void;
}

const OrderHistoryProgress = (props: OrderHistoryProgressProps) => {

    const history = (code: OrderStatusCode) => {
        return props.order.historyEntries.find(x => x.statusCode === code);
    }

    const iconColor = (orderStatusCode: OrderStatusCode, orderHistoryStatusCode: OrderStatusCode) => {
        return orderStatusCode >= orderHistoryStatusCode ? 'primary' : undefined;
    }

    return (
        <Box className={'order-history-progress-container'}>
            <OrderHistoryProgressItem
                id={props.order.id}
                icon={<AddBoxOutlined color={iconColor(props.order.statusCode, OrderStatusCode.created)}/>}
                history={history(OrderStatusCode.created)}
            />
            <OrderHistoryProgressItem
                id={props.order.id}
                icon={<MarkEmailRead color={iconColor(props.order.statusCode, OrderStatusCode.sent)}/>}
                history={history(OrderStatusCode.sent)}
                onClick={props.onSend}
            />
            <OrderHistoryProgressItem
                id={props.order.id}
                icon={<DoneOutlined color={iconColor(props.order.statusCode, OrderStatusCode.fulfilled)}/>}
                onClick={props.onComplete}
            />
        </Box>
    );
}

export default OrderHistoryProgress;