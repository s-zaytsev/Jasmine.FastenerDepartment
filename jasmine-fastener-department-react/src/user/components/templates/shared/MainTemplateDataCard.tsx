import {type Control, Controller, type FieldErrors} from "react-hook-form";
import {type ChangeTemplate, type TemplateType, TemplateTypeCode} from "../../../models/templateModels.ts";
import {Box, FormControl, MenuItem, Select, TextField} from "@mui/material";
import {memo} from "react";

type MainTemplateDataCardProps = {
    control: Control<ChangeTemplate, unknown, ChangeTemplate>;
    errors: FieldErrors<ChangeTemplate>;
    templateTypes: TemplateType[];
    onTypeChanged: (code: TemplateTypeCode) => void;
}

const MainTemplateDataCard = (props: MainTemplateDataCardProps) => {
    return (
        <Box className={'w-full'}>

            {props.templateTypes.length && <Controller
                name="typeCode"
                control={props.control}
                render={({field}) => (
                    <FormControl
                        fullWidth
                        margin="normal"
                        error={!!props.errors.typeCode}>
                        <Select
                            {...field}
                            onChange={(e) => {
                                const newTypeCode = e.target.value as TemplateTypeCode;
                                if (field.value === newTypeCode) return;
                                field.onChange(newTypeCode);
                                props.onTypeChanged(newTypeCode);
                            }}
                        >
                            {props.templateTypes.map(x => (
                                <MenuItem key={x.id} value={x.id}>
                                    {x.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                )}
            />}

            <Controller
                name="name"
                control={props.control}
                rules={{required: 'Обязательное поле'}}
                render={({field}) => (
                    <TextField
                        {...field}
                        label="Название"
                        variant="outlined"
                        fullWidth
                        margin="normal"
                        autoComplete={'off'}
                        error={!!props.errors.name}
                        helperText={props.errors.name?.message}
                    />
                )}
            />
        </Box>);
}

export default memo(MainTemplateDataCard);