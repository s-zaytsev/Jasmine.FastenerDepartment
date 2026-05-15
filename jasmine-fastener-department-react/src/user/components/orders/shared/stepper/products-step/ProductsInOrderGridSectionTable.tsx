import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import {Box} from "@mui/material";
import SortTableHead from "../../../../../../shared/components/tables/SortTableHead.tsx";
import ProductsInOrderGridSectionTableRow from "./ProductsInOrderGridSectionTableRow.tsx";
import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";

type ProductsInOrderGridSectionTableProps = {
    products: ChangeOrderProduct[];
    onRemoveFromOrder: (product: ChangeOrderProduct) => void;
}

const ProductsInOrderGridSectionTable = (props: ProductsInOrderGridSectionTableProps) => {
    const columns: TableColumnDefinition[] = [
        {title: "Наименование"},
        {title: "", width: '10%'}
    ];

    return (
        <Box className={'w-full'}>
            <SortTableHead columns={columns}/>
            {
                props?.products?.map(product => (
                    <ProductsInOrderGridSectionTableRow
                        key={product.productId}
                        product={product}
                        columns={columns}
                        onRemoveFromOrder={props.onRemoveFromOrder}
                    />
                ))}
        </Box>
    )
}

export default ProductsInOrderGridSectionTable;