import {type Control, Controller, type FieldErrors} from "react-hook-form";
import type {ChangeProduct} from "../../../../models/productModel";
import type {Supplier} from "../../../../models/supplierModels";
import {Box} from "@mui/material";
import {Person} from "@mui/icons-material";
import SettingsCard from "./SettingsCard.tsx";
import SuppliersGroup from "../suppliersGroup/SuppliersGroup.tsx";

type SupplierSettingsCardProps = {
    control: Control<ChangeProduct, unknown, ChangeProduct>;
    errors: FieldErrors<ChangeProduct>;
    suppliers: Supplier[];
}

const SupplierSettingsCard = (props: SupplierSettingsCardProps) => {
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
                            onChange={id => {
                                const currentIds = field.value;
                                const nextIds = currentIds.includes(id)
                                    ? currentIds.filter(x => x !== id)
                                    : [...currentIds, id];
                                field.onChange(nextIds);
                                field.onBlur();
                            }}/>
                    )}
                />
            </Box>
        </SettingsCard>
    )
}

export default SupplierSettingsCard;