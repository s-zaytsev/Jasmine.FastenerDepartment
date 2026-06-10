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

interface ChangeProductFormProps {
    changeModel: ChangeProduct;
    suppliers: Supplier[];
    productTypes: ProductType[];
    onChanged: (model: ChangeProduct) => void;
}

const ProductForm = (props: ChangeProductFormProps) => {
    const {
        formState: {
            errors
        },
        control,
        reset,
        watch
    } = useForm<ChangeProduct>({
        defaultValues: props.changeModel,
        mode: "onBlur"
    });

    useEffect(() => {
        reset(props.changeModel);
    }, [props.changeModel, reset]);

    useEffect(() => {
        const subscription = watch((data) => {
            props.onChanged(data as ChangeProduct);
        });

        return () => subscription.unsubscribe();
    }, [watch, props.onChanged]);

    return (
        <Box component="form" className={'w-full flex flex-col gap-[0.5rem]'}>

            <DetailsSettingsCard control={control} errors={errors} productTypes={props.productTypes}/>
            <SupplierSettingsCard control={control} errors={errors} suppliers={props.suppliers}/>
            <PrintSettingsCard control={control}/>
            <OrderSettingsCard control={control}/>
        </Box>
    );
};

export default ProductForm;