import type {ProductToOrder} from "../../../../../models/productsToOrderModels.ts";
import {Box} from "@mui/material";
import TableRow from "../../../../../../shared/components/tables/TableRow.tsx";
import Typography from "../../../../../../shared/components/Typography.tsx";
import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import {AddOutlined, DoneOutlined} from "@mui/icons-material";
import IconButton from "../../../../../../shared/components/buttons/IconButton.tsx";
import IconBox from "../../../../../../shared/components/IconBox.tsx";
import {memo, useCallback} from "react";

type ProductsToOrderGridSectionTableRowProps = {
    product: ProductToOrder;
    columns: TableColumnDefinition[];
    inOrder: boolean;
    onMoveToOrder: (product: ProductToOrder) => void;
}

const ProductsToOrderGridSectionTableRow = (props: ProductsToOrderGridSectionTableRowProps) => {

    const getWidth = useCallback((columnNumber: number) => {
        return props.columns?.at(columnNumber)?.width || '100%'
    }, [props.columns]);

    return (
        <Box className={'w-full flex'}>
            <TableRow>
                <Box width={getWidth(0)}>
                    <Typography variant={'bodyRegular'}>{props.product.product.name}</Typography>
                </Box>

                <Box className={'flex items-center'} width={getWidth(1)}>
                    {!props.inOrder &&
                        <IconButton
                            description={'Добавить в заказ'}
                            hasBackground={true}
                            onClick={() => props.onMoveToOrder(props.product)}>
                            <AddOutlined/>
                        </IconButton>}

                    {props.inOrder &&
                        <IconBox>
                            <DoneOutlined/>
                        </IconBox>}
                </Box>

            </TableRow>
        </Box>
    )
}

export default memo(ProductsToOrderGridSectionTableRow);