import {useForm} from "react-hook-form";
import {Box} from "@mui/material";
import {type ChangeProduct} from "../../../models/productModel.ts";
import type {Supplier} from "../../../models/supplierModels.ts";
import {useEffect} from "react";
import type {ProductType} from "../../../models/productTypeModels.ts";
import DetailsSettingsCard from "./settingsCards/DetailsSettingsCard.tsx";
import PrintSettingsCard from "./settingsCards/PrintSettingsCard";
import OrderSettingsCard from "./settingsCards/OrderSettingsCard";
import SupplierSettingsCard from "./settingsCards/SupplierSettingsCard";
import PriceTagPreview from "./PriceTagPreview.tsx";

interface ChangeProductFormProps {
    changeModel: ChangeProduct;
    suppliers: Supplier[];
    productTypes: ProductType[];
    onSubmit: (data: ChangeProduct) => void;
}

const ProductForm = (props: ChangeProductFormProps) => {
    const {
        formState: {
            isValid,
            errors
        },
        control,
        handleSubmit,
        reset,
    } = useForm<ChangeProduct>({
        values: props.changeModel,
        mode: "onBlur"
    });

    const onSubmit = (data: ChangeProduct) => {
        if (!isValid) return;
        props.onSubmit(data);
    };

    useEffect(() => {
        reset(props.changeModel);
    }, [props.changeModel, reset]);

    return (
        <Box
            component="form"
            id="product-edit-form"
            onSubmit={handleSubmit(onSubmit)}
            className="flex w-full gap-[1rem]"
        >
            <Box className={'w-1/2 flex flex-col gap-[0.5rem]'}>
                <DetailsSettingsCard control={control} errors={errors} productTypes={props.productTypes}/>
                <SupplierSettingsCard control={control} errors={errors} suppliers={props.suppliers}/>
                <PrintSettingsCard control={control}/>
                <OrderSettingsCard control={control}/>
            </Box>

            <Box className="w-1/2">
                <PriceTagPreview control={control}/>
            </Box>

            <button type="submit" style={{display: 'none'}}/>
        </Box>
    );
};

export default ProductForm;