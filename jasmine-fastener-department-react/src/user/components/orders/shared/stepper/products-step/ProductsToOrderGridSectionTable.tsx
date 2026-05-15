import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import {Box} from "@mui/material";
import SortTableHead from "../../../../../../shared/components/tables/SortTableHead.tsx";
import type {ProductToOrder} from "../../../../../models/productsToOrderModels.ts";
import ProductsToOrderGridSectionTableRow from "./ProductsToOrderGridSectionTableRow.tsx";
import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";

type ProductsToOrderGridSectionTableProps = {
    products: ProductToOrder[];
    productsInOrder?: ChangeOrderProduct[];
    onMoveToOrder: (product: ProductToOrder) => void;
}

const ProductsToOrderGridSectionTable = (props: ProductsToOrderGridSectionTableProps) => {
    const columns: TableColumnDefinition[] = [
        {title: "Наименование"},
        {title: "", width: '10%'}
    ];

    return (
        <Box className={'w-full'}>
            <SortTableHead columns={columns}/>
            {
                props?.products?.map(product => (
                    <ProductsToOrderGridSectionTableRow
                        key={product.product.id}
                        product={product}
                        columns={columns}
                        inOrder={!!props.productsInOrder?.find(x => x.productId === product.product.id)}
                        onMoveToOrder={props.onMoveToOrder}
                    />
                ))}
        </Box>
    )
}

export default ProductsToOrderGridSectionTable;