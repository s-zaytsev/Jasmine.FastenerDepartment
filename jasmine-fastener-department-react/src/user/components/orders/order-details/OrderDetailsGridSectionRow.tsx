import {type OrderProduct, OrderStatusCode} from "../../../models/orderModels";
import {Box} from "@mui/material";
import type {TableColumnDefinition} from "../../../../shared/models/models.ts";
import Typography from "../../../../shared/components/Typography.tsx";
import TableRow from "../../../../shared/components/tables/TableRow.tsx";
import OrderProductStatusChip from "../shared/OrderProductStatusChip.tsx";
import QuantityChip from "../../../../shared/components/quantity/QuantityChip.tsx";

type OrderDetailsGridSectionRowProps = {
    product: OrderProduct;
    columns: TableColumnDefinition[];
    orderStatusCode?: OrderStatusCode;
}

const OrderDetailsGridSectionRow = (props: OrderDetailsGridSectionRowProps) => {

    function getWidth(columnNumber: number) {
        return props.columns?.at(columnNumber)?.width || '100%'
    }

    return (
        <Box className={'w-full flex'}>
            <TableRow>
                <Box width={getWidth(0)}>
                    <Box className={'px-[1rem]'}>
                        <OrderProductStatusChip
                            product={props.product}
                            orderStatusCode={props.orderStatusCode}/>
                    </Box>
                </Box>

                <Box width={getWidth(1)}>
                    <Typography variant={'bodyRegular'}>{props.product.productName}</Typography>
                </Box>

                <Box width={getWidth(2)}>
                    <Box className={'flex justify-center gap-[0.5rem]'}>
                        <QuantityChip title={'Заказано'} type={'inactive'} quantity={props.product.ordered} />
                        <QuantityChip title={'Доставлено'} type={'active'} quantity={props.product.fulfilled} />
                    </Box>
                </Box>

            </TableRow>
        </Box>
    )
}

export default OrderDetailsGridSectionRow;