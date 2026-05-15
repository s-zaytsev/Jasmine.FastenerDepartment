import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";
import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import {Box} from "@mui/material";
import TableRow from "../../../../../../shared/components/tables/TableRow.tsx";
import Typography from "../../../../../../shared/components/Typography.tsx";
import QuantityChip from "../../../../../../shared/components/quantity/QuantityChip.tsx";

type ConfirmOrderSectionTableRowProps = {
    product: ChangeOrderProduct;
    columns: TableColumnDefinition[];
}

const ConfirmOrderSectionTableRow = (props: ConfirmOrderSectionTableRowProps) => {

    function getWidth(columnNumber: number) {
        return props.columns?.at(columnNumber)?.width || '100%'
    }

    return (
        <Box className={'w-full flex'}>
            <TableRow>
                <Box width={getWidth(0)}>
                    <Box>
                        <Typography variant={'bodyRegular'} color={'tertiary'}>
                            {props.product.supplierProductNumber ?? 'Не указан'}
                        </Typography>
                    </Box>
                </Box>

                <Box width={getWidth(1)}>
                    <Typography variant={'bodyRegular'}>{props.product.productName}</Typography>
                </Box>

                <Box width={getWidth(2)}>
                    <Box className={'flex justify-center gap-[0.5rem]'}>
                        <QuantityChip type={'inactive'} quantity={props.product.ordered}/>
                    </Box>
                </Box>

            </TableRow>
        </Box>
    )
}

export default ConfirmOrderSectionTableRow;