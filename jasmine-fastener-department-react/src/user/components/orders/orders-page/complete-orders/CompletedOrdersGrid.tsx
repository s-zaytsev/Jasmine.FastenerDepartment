import type {Page} from "../../../../../shared/models/models.ts";
import type {Order} from "../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../../../shared/components/Typography.tsx";
import CompletedOrdersGridRow from "./CompletedOrdersGridRow.tsx";

type CompletedOrdersGridProps = {
    page?: Page<Order>;
    onNavigateToDetails: (id: string) => void;
}

const CompletedOrdersGrid = (props: CompletedOrdersGridProps) => {

    if (props.page?.items.length === 0) {
        return (
            <Box className={'h-full w-full items-center flex justify-center'}>
                <Typography variant={'headlineH3'}>Список заказов пуст</Typography>
            </Box>
        )
    }

    return (
        <Box className={'grid grid-cols-4 gap-[1rem]'}>
            {props.page?.items.map(x =>
                <CompletedOrdersGridRow
                    key={x.id}
                    order={x}
                    onNavigateToDetails={props.onNavigateToDetails}
                />)}
        </Box>
    )
}

export default CompletedOrdersGrid;