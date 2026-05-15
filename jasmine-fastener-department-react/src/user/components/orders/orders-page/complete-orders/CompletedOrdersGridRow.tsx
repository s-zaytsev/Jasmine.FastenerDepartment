import {type Order, OrderStatusCode} from "../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import {CancelOutlined, DoneOutlined} from "@mui/icons-material";
import Card from "../../../../../shared/components/Card.tsx";
import IconBox from "../../../../../shared/components/IconBox.tsx";
import Typography from "../../../../../shared/components/Typography.tsx";

type OrdersGridRowProps = {
    order: Order;
    onNavigateToDetails: (id: string) => void;
}

const CompletedOrdersGridRow = (props: OrdersGridRowProps) => {

    const orderNumber = "0".repeat(8 - props.order.number.toString().length) + props.order.number.toString();
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
                        <Typography variant={'bodyRegularBold'}>Заказ #{orderNumber}</Typography>
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

            {/*            <Box>
                <Typography variant={'bodyRegularBold'} color={'primary'}>
                    Заказ №{orderNumber}
                </Typography>
                <Typography variant={'bodyRegular'}>
                    от {new Date(props.order.createdDate).toLocaleDateString('ru-Ru')}
                </Typography>
            </Box>
            <Box>

            </Box>*/}
        </Card>
    )
}

export default CompletedOrdersGridRow;