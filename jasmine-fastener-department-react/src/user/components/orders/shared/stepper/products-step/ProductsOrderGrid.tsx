import {Box} from "@mui/material";
import ProductsToOrderGrid from "./ProductsToOrderGrid.tsx";
import ProductsInOrderGrid from "./ProductsInOrderGrid.tsx";
import type {ProductToOrder} from "../../../../../models/productsToOrderModels.ts";
import type {OrderStepperModel} from "../../../../../models/orderModels.ts";
import {memo} from "react";

type ProductsOrderGridProps = {
    model: OrderStepperModel;
    productsToOrder: ProductToOrder[];
    onMoveToOrder: (product: ProductToOrder) => void;
    onDeleteFromOrder: (id?: string) => void;
}

const ProductsOrderGrid = (props: ProductsOrderGridProps) => {
    return (
        <Box className={"h-full w-full flex justify-between"}>
            <Box className={"w-[50%] h-[85vh] overflow-y-auto"}>
                <ProductsToOrderGrid
                    products={props.productsToOrder}
                    productsInOrder={props.model.products}
                    onMoveToOrder={props.onMoveToOrder}
                />
            </Box>
            <Box className={"w-[50%] h-[85vh] overflow-y-auto"}>
                <ProductsInOrderGrid
                    onDeleteFromOrder={props.onDeleteFromOrder}
                    products={props.model.products}
                />
            </Box>
        </Box>
    )
}

export default memo(ProductsOrderGrid);