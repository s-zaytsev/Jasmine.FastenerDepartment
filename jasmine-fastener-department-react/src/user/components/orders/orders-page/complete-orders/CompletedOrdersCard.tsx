import {type Order, OrderStatusCode} from "../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import {CancelOutlined, DoneOutlined} from "@mui/icons-material";
import Card from "../../../../../shared/components/Card.tsx";
import IconBox from "../../../../../shared/components/IconBox.tsx";
import Typography from "../../../../../shared/components/Typography.tsx";
import {memo} from "react";

type OrdersGridRowProps = {
    order: Order;
    onNavigateToDetails: (id: string) => void;
}

const CompletedOrdersCard = (props: OrdersGridRowProps) => {
    const icon = props.order.statusCode === OrderStatusCode.fulfilled ?
        <DoneOutlined/> :
        <CancelOutlined/>

    const text = (props.order.statusCode === OrderStatusCode.fulfilled ?
        'Доставлен' :
        'Отменен') + ` ${new Date(props.order.historyEntries.at(-1)!.createdDate).toLocaleDateString()}`

    return (
        <Card onClick={() => props.onNavigateToDetails(props.order.id)}>
            <Box className={'w-full flex gap-[1rem]'}>
                <IconBox>
                    {icon}
                </IconBox>

                <Box className={'w-full'}>
                    <Box className={'flex justify-between'}>
                        <Typography variant={'bodyRegularBold'}>Заказ #{props.order.number}</Typography>
                        <Typography variant={'bodyRegularBold'}>
                            {props.order.supplier?.name ?? 'Без поставщика'}
                        </Typography>
                    </Box>

                    <Box className={'flex justify-between'}>
                        <Typography variant={'bodySmall'} color={'tertiary'}>
                            {text}
                        </Typography>

                        <Typography variant={'bodySmall'} color={'tertiary'}>
                            {props.order.products.length} позиций
                        </Typography>
                    </Box>
                </Box>
            </Box>
        </Card>
    )
}

export default memo(CompletedOrdersCard);