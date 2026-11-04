import {Box} from "@mui/material";
import FilledButton from "../../../../../shared/components/buttons/FilledButton.tsx";
import type {Order} from "../../../../models/orderModels.ts";

type ActiveOrdersGridRowFooterProps = {
    order: Order;
    onComplete: (id: string) => void;
    onCancel: (id: string) => void;
}

const ActiveOrdersCardFooter = (props: ActiveOrdersGridRowFooterProps) => {
    return (
        <Box className={'w-full flex justify-between'}>

            <FilledButton
                variant={'text'}
                sx={{width: 'content-fit'}}
                onClick={(e) => {
                    e.stopPropagation();
                    props.onCancel(props.order.id);
                }}
            >
                Отменить
            </FilledButton>

            <FilledButton
                variant={'outlined'}
                onClick={(e) => {
                    e.stopPropagation();
                    props.onComplete(props.order.id);
                }}
            >
                Завершить
            </FilledButton>
        </Box>
    )
}

export default ActiveOrdersCardFooter;