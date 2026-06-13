import type {Order} from "../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../../../shared/components/Typography.tsx";
import type {Page} from "../../../../../shared/models/models.ts";
import ActiveOrdersCard from "./ActiveOrdersCard.tsx";

type ActiveOrdersGridProps = {
    page?: Page<Order>;
    onChange: (id: string) => void;
    onComplete: (id: string) => void;
    onCancel: (id: string) => void;
    onSend: (id: string) => void;
    onNavigateToDetails: (id: string) => void;
}

const ActiveOrdersGrid = (props: ActiveOrdersGridProps) => {

    if (props.page?.items?.length === 0) {
        return (
            <Box className={'h-full w-full items-center flex justify-center'}>
                <Typography variant={'headlineH3'}>Список активных заказов пуст</Typography>
            </Box>
        )
    }

    return (
        <Box className={'grid grid-cols-3 gap-[1rem]'}>
            {props.page?.items.map(x =>
                    <ActiveOrdersCard
                        key={x.id}
                        order={x}
                        onChange={props.onChange}
                        onComplete={props.onComplete}
                        onCancel={props.onCancel}
                        onSend={props.onSend}
                        onNavigateToDetails={props.onNavigateToDetails}
                    />)
            }
        </Box>
    );
}

export default ActiveOrdersGrid;