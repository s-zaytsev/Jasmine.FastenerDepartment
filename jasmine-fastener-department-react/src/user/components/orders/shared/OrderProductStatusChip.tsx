import {type OrderProduct, OrderStatusCode} from "../../../models/orderModels.ts";
import Chip, {type ChipColor} from "../../../../shared/components/Chip.tsx";
import type {ReactNode} from "react";
import {AlarmOutlined, CheckOutlined, DoNotDisturbOutlined} from "@mui/icons-material";

type OrderProductStatusChipProps = {
    orderStatusCode?: OrderStatusCode;
    product: OrderProduct;
}

const OrderProductStatusChip = (props: OrderProductStatusChipProps) => {

    let color: ChipColor;
    let title: string;
    let icon: ReactNode;

    if (props.orderStatusCode === OrderStatusCode.cancelled) {
        color = "error";
        title = "Отменено";
        icon = <DoNotDisturbOutlined/>;
    } else if (props.orderStatusCode === OrderStatusCode.fulfilled) {
        if (props.product.isFulfilled) {
            color = "active";
            title = "Доставлено";
            icon = <CheckOutlined/>;
        } else {
            color = "inactive";
            title = "Не доставлено";
            icon = <DoNotDisturbOutlined/>;
        }
    }
    else {
        color = "inactive";
        title = "В ожидании";
        icon = <AlarmOutlined/>;
    }

    return (
        <Chip title={title} color={color} icon={icon}/>
    )
}

export default OrderProductStatusChip;