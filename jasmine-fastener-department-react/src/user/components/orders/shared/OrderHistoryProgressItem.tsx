import {type OrderHistoryEntry} from "../../../models/orderModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import type {ReactNode} from "react";

type OrderHistoryProgressItemProps = {
    id: string;
    icon: ReactNode;
    history?: OrderHistoryEntry;
    onClick?: (id: string) => void;
}

const OrderHistoryProgressItem = (props: OrderHistoryProgressItemProps) => {
    return (
        <Box className={'order-history-progress-item'}
             onClick={(e) => {
                 if (props.onClick) props.onClick(props.id);
                 e.stopPropagation();
             }}>
            {props.icon}
            {props.history &&
                <Typography
                    variant={'labelRegular'}>{new Date(props.history.createdDate).toLocaleDateString('ru-Ru')}
                </Typography>
            }
        </Box>
    );
}

export default OrderHistoryProgressItem;