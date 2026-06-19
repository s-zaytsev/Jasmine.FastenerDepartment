import {type ProductHistoryEntry} from "../../../models/productModel.ts";
import type {ProductType} from "../../../models/productTypeModels.ts";
import ProductHistoryCard from "../../../../shared/components/history/ProductHistoryCard.tsx";
import {Box, Grow} from "@mui/material";

type ProductHistoryProps = {
    history?: ProductHistoryEntry[];
    productTypes: ProductType[]
}

const ProductHistory = (props: ProductHistoryProps) => {
    return (
        <Box className={'flex w-full justify-center'}>
            <Box className={'flex flex-col gap-[0.5rem] w-[50%]'}>
                {props.history?.map((x, index) =>
                    <Grow key={x.id} in={true} timeout={index * 150}>
                        <Box>
                            <ProductHistoryCard
                                historyEntry={x}
                                productTypes={props.productTypes}/>
                        </Box>
                    </Grow>
                )}
            </Box>
        </Box>
    );
}

export default ProductHistory;