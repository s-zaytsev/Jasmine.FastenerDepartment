import type {Order} from "../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import IconButton from "../../../../../shared/components/buttons/IconButton.tsx";
import {DoneOutlined, EmailOutlined} from "@mui/icons-material";
import Typography from "../../../../../shared/components/Typography.tsx";

type ActiveOrdersGridRowHeaderProps = {
    order: Order;
    onSend: (id: string) => void;
    onComplete: (id: string) => void;
}

const ActiveOrdersGridRowHeader = (props: ActiveOrdersGridRowHeaderProps) => {
    const orderNumber = "0".repeat(8 - props.order.number.toString().length) + props.order.number.toString();

    return (
        <Box className={'w-full flex items-center justify-between'}>
            <Box>
                <Typography variant={'headlineH3'}>{`Заказ #${orderNumber}`}</Typography>
                <Typography variant={'bodySmall'} color={'tertiary'}>
                    {`Создан ${new Date(props.order.createdDate).toLocaleDateString()}`}
                </Typography>
            </Box>

            <Box className={'flex items-center justify-end gap-[1rem]'}>
                <IconButton
                    description={'Отправить заказ'}
                    hasBackground={true}
                    onClick={(e) => {
                        e.stopPropagation();
                        props.onSend(props.order.id);
                    }}>
                    <EmailOutlined/>
                </IconButton>

                <IconButton
                    description={'Завершить заказ'}
                    hasBackground={true}
                    onClick={(e) => {
                        e.stopPropagation();
                        props.onComplete(props.order.id);
                    }}>
                    <DoneOutlined/>
                </IconButton>
            </Box>
        </Box>
    )
}

export default ActiveOrdersGridRowHeader;