import {type Order, OrderStatusCode} from "../../../models/orderModels.ts";
import {Box} from "@mui/material";
import {primitives} from "../../../../assets/variables/primitives.ts";

type OrderProgressBarProps = {
    order: Order;
}

const OrderProgressBar = (props: OrderProgressBarProps) => {
    let percent: number;
    let label = '';

    switch (props.order.statusCode) {
        case OrderStatusCode.cancelled:
            percent = 0;
            label = 'Отменен'
            break;
        case OrderStatusCode.created:
            percent = 33;
            label = 'Создан'
            break;
        case OrderStatusCode.sent:
            percent = 66;
            label = 'Отправлен';
            break;
        case OrderStatusCode.fulfilled:
            percent = 100;
            label = 'Доставлен';
            break;
        default:
            percent = 0;
    }

    return (
        <Box
            sx={{
                position: 'relative',
                width: '100%',
                pt: 4,

                '--target-width': `${percent}%`,
                '@keyframes fillLeftToRight': {
                    '0%': { width: '0%' },
                    '100%': { width: 'var(--target-width)' },
                },

                '&::before': {
                    content: `"${label}"`,
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    fontSize: '14px',
                    color: primitives.colors.primary,
                    transform: 'translateX(80%)',
                    animation: 'fillLeftToRight 1.5s cubic-bezier(0.4, 0, 0.2, 1) forwards',
                },

                '& .main-line': {
                    width: '100%',
                    height: '8px',
                    backgroundColor: primitives.colors.tonal,
                    borderRadius: primitives.border.radius,
                    position: 'relative',
                },

                '& .active-line': {
                    position: 'absolute',
                    top: 0,
                    left: 0,
                    height: '100%',
                    backgroundColor: primitives.colors.primary,
                    borderRadius: primitives.border.radius,

                    animation: 'fillLeftToRight 1.5s cubic-bezier(0.4, 0, 0.2, 1) forwards',
                },
            }}
        >
            <Box className="main-line">
                <Box className="active-line" />
            </Box>
        </Box>
    )
}

export default OrderProgressBar;