import {Box} from "@mui/material";
import FilledButton from "../../../../../shared/components/buttons/FilledButton.tsx";
import type {Order} from "../../../../models/orderModels.ts";

type ActiveOrdersGridRowFooterProps = {
    order: Order;
    onChange: (id: string) => void;
    onCancel: (id: string) => void;
}

const ActiveOrdersGridRowFooter = (props: ActiveOrdersGridRowFooterProps) => {
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
                    props.onChange(props.order.id);
                }}
            >
                Редактировать
            </FilledButton>
        </Box>
    )
}

export default ActiveOrdersGridRowFooter;