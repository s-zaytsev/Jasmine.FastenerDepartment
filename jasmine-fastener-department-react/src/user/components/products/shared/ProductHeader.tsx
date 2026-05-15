import type {Product} from "../../../models/productModel.ts";
import {Box} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";

type ProductHeaderProps = {
    model?: Product;
}

const ProductHeader = (props: ProductHeaderProps) => {
    return (
        <Box className={'flex flex-col'}>
            <Typography variant={'headlineH2'}>{props.model?.name}</Typography>
            <Typography variant={'bodySmall'}>Артикул: {props.model?.number}</Typography>
        </Box>
    )
}

export default ProductHeader;