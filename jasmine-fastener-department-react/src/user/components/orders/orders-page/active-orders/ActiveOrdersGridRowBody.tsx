import type {Order} from "../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../../../shared/components/Typography.tsx";

type ActiveOrdersGridRowBodyProps = {
    order: Order;
}

const ActiveOrdersGridRowBody = (props: ActiveOrdersGridRowBodyProps) => {
    return (
        <Box className={'w-full'}>
            <Box className={'w-full flex justify-between my-[1rem]'}>
                <Typography variant={'bodyRegular'} color={'tertiary'}>Поставщик</Typography>
                <Typography variant={'bodyRegularBold'}>{props.order.supplier?.name ?? 'Без поставщика'}</Typography>
            </Box>

            <Box className={'w-full flex justify-between mb-[1rem]'}>
                <Typography variant={'bodyRegular'} color={'tertiary'}>Позиции</Typography>
                <Typography variant={'bodyRegularBold'}>{props.order.products.length} позиций</Typography>
            </Box>
        </Box>
    )
}

export default ActiveOrdersGridRowBody;