import type {TableColumnDefinition} from "../../../../shared/models/models.ts";
import {Box} from "@mui/material";
import SortTableHead from "../../../../shared/components/tables/SortTableHead.tsx";
import OrderDetailsGridSectionRow from "./OrderDetailsGridSectionRow.tsx";
import {type OrderProduct, OrderStatusCode} from "../../../models/orderModels.ts";

type OrderDetailsGridSectionTableProps = {
    products: OrderProduct[];
    orderStatusCode?: OrderStatusCode;
}

const OrderDetailsGridSectionTable = (props: OrderDetailsGridSectionTableProps) => {
    const columns: TableColumnDefinition[] = [
        {title: "Статус", width: '35%', columnAlign: 'center'},
        {title: "Товар"},
        {title: "Количество", width: '35%', columnAlign: 'center'}
    ];

    return (
        <Box className={'w-full'}>
            <SortTableHead columns={columns}/>
            {
                props?.products?.map(product => (
                    <OrderDetailsGridSectionRow
                        key={product.id}
                        product={product}
                        columns={columns}
                        orderStatusCode={props.orderStatusCode}
                    />
                ))}
        </Box>
    )
}

export default OrderDetailsGridSectionTable;