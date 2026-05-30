import {numberEnumToArray} from "../../models/models.ts";
import type {ControllerRenderProps} from "react-hook-form";
import {Box, MenuItem, Select, TextField} from "@mui/material";
import {longMeasurementUnitName, ProductMeasurementUnitCode} from "../../../user/models/productModel.ts";

type QuantityInputProps = {
    field: ControllerRenderProps<any, string>;
    fieldState?: { error?: { message?: string } };
    isReadOnly?: boolean;
};

const QuantityInput = (props: QuantityInputProps) => {
    const handleValueChange = (value: string) => {
        const newValue = parseFloat(value) || 0;
        props.field.onChange({
            ...props.field.value,
            value: newValue
        });
    };

    const handleUnitChange = (unitCode: ProductMeasurementUnitCode) => {
        props.field.onChange({
            ...props.field.value,
            measurementUnitCode: unitCode
        });
    };
    
    return (
        <Box className="w-full flex items-center">
            <TextField
                style={{ marginRight: "-1px" }}
                sx={{
                    "& fieldset": { borderRadius: '4px 0 0 4px' },
                }}
                value={props.field.value?.value || ''}
                onChange={(e) => handleValueChange(e.target.value)}
                onBlur={props.field.onBlur}
                error={!!props.fieldState?.error}
                helperText={props.fieldState?.error?.message}
                InputProps={{
                    readOnly: props.isReadOnly
                }}
            />

            <Select
                sx={{
                    "& fieldset": { borderRadius: '0 4px 4px 0' },
                }}
                displayEmpty={true}
                value={props.field.value?.measurementUnitCode || ProductMeasurementUnitCode.pieces}
                onChange={(e) => {
                    handleUnitChange(e.target.value as ProductMeasurementUnitCode);
                    props.field.onBlur();
                }}
            >
                <MenuItem key={'none'} value={''}></MenuItem>
                {numberEnumToArray(ProductMeasurementUnitCode).sort(x => x).map((unit) => (
                        <MenuItem key={unit} value={unit}>
                            {longMeasurementUnitName(unit)}
                        </MenuItem>
                    )
                )}
            </Select>
        </Box>
    );
};

export default QuantityInput;