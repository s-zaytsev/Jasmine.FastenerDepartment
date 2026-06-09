import {Box} from "@mui/material";
import TableRow from "../../../../../../shared/components/tables/TableRow.tsx";
import Typography from "../../../../../../shared/components/Typography.tsx";
import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";
import IconButton from "../../../../../../shared/components/buttons/IconButton.tsx";
import {CloseOutlined} from "@mui/icons-material";

type ProductsInOrderGridSectionTableRowProps = {
    product: ChangeOrderProduct;
    columns: TableColumnDefinition[];
    onDeleteFromOrder: (id?: string) => void;
}

const ProductsInOrderGridSectionTableRow = (props: ProductsInOrderGridSectionTableRowProps) => {
    function getWidth(columnNumber: number) {
        return props.columns?.at(columnNumber)?.width || '100%'
    }

    return (
        <Box className={'w-full flex'}>
            <TableRow>
                <Box width={getWidth(0)}>
                    <Typography variant={'bodyRegular'}>{props.product.productName}</Typography>
                </Box>

                <Box width={getWidth(1)}>
                    <IconButton
                        description={'Убрать из заказа'}
                        hasBackground={true}
                        onClick={() => props.onDeleteFromOrder(props.product.productId)}
                    >
                        <CloseOutlined/>
                    </IconButton>
                </Box>

            </TableRow>
        </Box>
    )
}

export default ProductsInOrderGridSectionTableRow;