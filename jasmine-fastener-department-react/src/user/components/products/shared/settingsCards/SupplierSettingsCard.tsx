import {type Control, Controller, type FieldErrors, useController} from "react-hook-form";
import type {ChangeProduct} from "../../../../models/productModel";
import type {Supplier} from "../../../../models/supplierModels";
import {Box} from "@mui/material";
import {Person} from "@mui/icons-material";
import SettingsCard from "./SettingsCard.tsx";
import SuppliersGroup from "../suppliersGroup/SuppliersGroup.tsx";
import {memo, useCallback, useMemo} from "react";

type SupplierSettingsCardProps = {
    control: Control<ChangeProduct, unknown, ChangeProduct>;
    errors: FieldErrors<ChangeProduct>;
    suppliers: Supplier[];
}

const SupplierSettingsCard = (props: SupplierSettingsCardProps) => {
    const {field} = useController({
        name: "supplierIds",
        control: props.control,
    });

    const supplierIds = useMemo(() => {
        return field.value;
    }, [field.value]);

    const handleChange = useCallback((id: string) => {
        const nextIds = supplierIds.includes(id)
            ? supplierIds.filter(x => x !== id)
            : [...supplierIds, id];
        field.onChange(nextIds);
    }, [field, supplierIds]);

    return (
        <SettingsCard title={'Поставщики'} icon={<Person/>}>
            <Box>
                <Controller
                    name="supplierIds"
                    control={props.control}
                    render={({field}) => (
                        <SuppliersGroup
                            suppliers={props.suppliers}
                            productSupplierIds={field.value}
                            onChange={handleChange}/>
                    )}
                />
            </Box>
        </SettingsCard>
    )
}

export default memo(SupplierSettingsCard);