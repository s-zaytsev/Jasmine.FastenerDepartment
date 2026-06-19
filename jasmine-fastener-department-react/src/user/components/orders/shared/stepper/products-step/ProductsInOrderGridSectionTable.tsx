import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import {Box, Grow} from "@mui/material";
import SortTableHead from "../../../../../../shared/components/tables/SortTableHead.tsx";
import ProductsInOrderGridSectionTableRow from "./ProductsInOrderGridSectionTableRow.tsx";
import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";
import {memo} from "react";

type ProductsInOrderGridSectionTableProps = {
    products: ChangeOrderProduct[];
    onDeleteFromOrder: (id?: string) => void;
}

const columns: TableColumnDefinition[] = [
    {title: "Наименование"},
    {title: "", width: '10%'}
];

const ProductsInOrderGridSectionTable = (props: ProductsInOrderGridSectionTableProps) => {
    return (
        <Box className={'w-full'}>
            <SortTableHead columns={columns}/>
            {
                props?.products?.map((product, index) => (
                    <Grow key={product.productId} appear={true} in={true} timeout={{enter: index * 150, exit: 500}}>
                        <Box>
                            <ProductsInOrderGridSectionTableRow
                                product={product}
                                columns={columns}
                                onDeleteFromOrder={props.onDeleteFromOrder}
                            />
                        </Box>
                    </Grow>
                ))}
        </Box>
    )
}

export default memo(ProductsInOrderGridSectionTable);