import type {Order} from "../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import Card from "../../../../../shared/components/Card.tsx";
import ActiveOrdersGridRowHeader from "./ActiveOrdersGridRowHeader.tsx";
import ActiveOrdersGridRowBody from "./ActiveOrdersGridRowBody.tsx";
import ActiveOrdersGridRowFooter from "./ActiveOrdersGridRowFooter.tsx";

type ActiveOrdersGridRowProps = {
    order: Order;
    onChange: (id: string) => void;
    onComplete: (id: string) => void;
    onSend: (id: string) => void;
    onCancel: (id: string) => void;
    onNavigateToDetails: (id: string) => void;
}

const ActiveOrdersGridRow = (props: ActiveOrdersGridRowProps) => {

    return (
        <Card onClick={() => props.onNavigateToDetails(props.order.id)}>
            <Box className={'w-full'}>
                <ActiveOrdersGridRowHeader
                    order={props.order}
                    onSend={props.onSend}
                    onComplete={props.onComplete}
                />

                <ActiveOrdersGridRowBody order={props.order}/>

                <ActiveOrdersGridRowFooter
                    order={props.order}
                    onChange={props.onChange}
                    onCancel={props.onCancel}
                />
            </Box>
        </Card>
    )
}

export default ActiveOrdersGridRow;