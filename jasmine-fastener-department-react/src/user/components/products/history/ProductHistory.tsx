import {type ProductHistoryEntry} from "../../../models/productModel.ts";
import type {ProductType} from "../../../models/productTypeModels.ts";
import ProductHistoryCard from "../../../../shared/components/history/ProductHistoryCard.tsx";
import {Box} from "@mui/material";

type ProductHistoryProps = {
    history?: ProductHistoryEntry[];
    productTypes: ProductType[]
}

const ProductHistory = (props: ProductHistoryProps) => {
    return (
        <Box className={'flex w-full justify-center'}>
            <Box className={'flex flex-col gap-[0.5rem] w-[50%]'}>
                {props.history?.map((x) =>
                    <ProductHistoryCard
                        key={x.id}
                        historyEntry={x}
                        productTypes={props.productTypes}/>
                )}
            </Box>
        </Box>
    );
}

export default ProductHistory;