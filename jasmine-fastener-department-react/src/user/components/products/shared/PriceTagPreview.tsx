import {Box} from "@mui/material";
import {type Product} from "../../../models/productModel.ts";
import PriceTag from "../../../../shared/components/priceTags/PriceTag.tsx";
import {useMemo} from "react";
import {type Control, useWatch} from "react-hook-form";

type PriceTagPreviewProps = {
    control: Control<any>;
}

const PriceTagPreview = (props: PriceTagPreviewProps) => {

    const formValues = useWatch({
        control: props.control,
        name: ["name", "number", "price", "priceTagCode", "isHardwareSizeEnabled", "measurementUnitCode"]
    });

    const [name, number, price, priceTagCode, isHardwareSizeEnabled, measurementUnitCode] = formValues;

    const product = useMemo(() => {
        const p: Product = {
            id: '',
            name: name || '',
            number: Number(number) || 0,
            measurementUnitCode: measurementUnitCode,
            isNeededToOrder: true,
            isNeededToPrint: true,
            isHardwareSizeEnabled: !!isHardwareSizeEnabled,
            price: Number(price) || 0,
            priceTagCode: priceTagCode,
            historyEntries: [],
            suppliers: []
        };
        return p;
    }, [name, number, price, priceTagCode, isHardwareSizeEnabled, measurementUnitCode]);

    return (
        <Box className={"w-full h-full flex column justify-center items-center"} sx={{borderRadius: '4px'}}>
            <Box sx={{backgroundColor: "white", padding: "1rem", transform: 'scale(1.5)'}}>
                <PriceTag product={product}/>
            </Box>
        </Box>
    )
}

export default PriceTagPreview;