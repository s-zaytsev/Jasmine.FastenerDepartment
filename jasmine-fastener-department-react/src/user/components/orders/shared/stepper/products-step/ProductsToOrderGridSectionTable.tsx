import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import {Box} from "@mui/material";
import SortTableHead from "../../../../../../shared/components/tables/SortTableHead.tsx";
import type {ProductToOrder} from "../../../../../models/productsToOrderModels.ts";
import ProductsToOrderGridSectionTableRow from "./ProductsToOrderGridSectionTableRow.tsx";
import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";
import {memo, useMemo} from "react";

type ProductsToOrderGridSectionTableProps = {
    products: ProductToOrder[];
    productsInOrder?: ChangeOrderProduct[];
    onMoveToOrder: (product: ProductToOrder) => void;
}

const columns: TableColumnDefinition[] = [
    {title: "Наименование"},
    {title: "", width: '10%'}
];

const ProductsToOrderGridSectionTable = (props: ProductsToOrderGridSectionTableProps) => {
    const productsInOrderSet = useMemo(
        () => new Set(
            props.productsInOrder?.map(x => x.productId)
        ),
        [props.productsInOrder]
    );

    return (
        <Box className={'w-full'}>
            <SortTableHead columns={columns}/>
            {
                props?.products?.map(product => (
                    <ProductsToOrderGridSectionTableRow
                        key={product.product.id}
                        product={product}
                        columns={columns}
                        inOrder={productsInOrderSet.has(product.product.id)}
                        onMoveToOrder={props.onMoveToOrder}
                    />
                ))}
        </Box>
    )
}

export default memo(ProductsToOrderGridSectionTable);