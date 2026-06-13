import type {Order} from "../../../../models/orderModels.ts";
import {Box} from "@mui/material";
import Card from "../../../../../shared/components/Card.tsx";
import ActiveOrdersCardHeader from "./ActiveOrdersCardHeader.tsx";
import ActiveOrdersCardBody from "./ActiveOrdersCardBody.tsx";
import ActiveOrdersCardFooter from "./ActiveOrdersCardFooter.tsx";
import {memo} from "react";

type ActiveOrdersGridRowProps = {
    order: Order;
    onChange: (id: string) => void;
    onComplete: (id: string) => void;
    onSend: (id: string) => void;
    onCancel: (id: string) => void;
    onNavigateToDetails: (id: string) => void;
}

const ActiveOrdersCard = (props: ActiveOrdersGridRowProps) => {
    return (
        <Card onClick={() => props.onNavigateToDetails(props.order.id)}>
            <Box className={'w-full'}>
                <ActiveOrdersCardHeader
                    order={props.order}
                    onSend={props.onSend}
                    onComplete={props.onComplete}
                />

                <ActiveOrdersCardBody order={props.order}/>

                <ActiveOrdersCardFooter
                    order={props.order}
                    onChange={props.onChange}
                    onCancel={props.onCancel}
                />
            </Box>
        </Card>
    )
}

export default memo(ActiveOrdersCard);