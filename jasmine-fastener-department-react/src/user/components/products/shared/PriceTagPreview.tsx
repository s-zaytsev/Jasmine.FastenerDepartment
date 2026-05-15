import {Box} from "@mui/material";
import type {Product} from "../../../models/productModel.ts";
import PriceTag from "../../../../shared/components/priceTags/PriceTag.tsx";

type PriceTagPreviewProps = {
    product?: Product;
}

const PriceTagPreview = (props: PriceTagPreviewProps) => {
    return (
        <Box className={"w-full h-full flex column justify-center items-center"} sx={{borderRadius: '4px'}}>
            <Box sx={{backgroundColor: "white", padding: "1rem", transform: 'scale(1.5)'}}>
                {props.product && <PriceTag product={props.product}/>}
            </Box>
        </Box>
    )
}

export default PriceTagPreview;