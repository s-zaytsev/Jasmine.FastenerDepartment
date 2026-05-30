import {
    type ChangeProduct,
    longMeasurementUnitName,
    ProductMeasurementUnitCode
} from "../../../../models/productModel.ts";
import SettingsCard from "./SettingsCard.tsx";
import {InfoOutlined} from "@mui/icons-material";
import {type Control, Controller, type FieldErrors} from "react-hook-form";
import {Box, FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import {NumericFormat} from "react-number-format";
import {numberEnumToArray} from "../../../../../shared/models/models.ts";
import type {ProductType} from "../../../../models/productTypeModels.ts";

type DetailsCardSettingsProps = {
    control: Control<ChangeProduct, unknown, ChangeProduct>;
    errors: FieldErrors<ChangeProduct>;
    productTypes: ProductType[];
}

const DetailsSettingsCard = (props: DetailsCardSettingsProps) => {
    return(
        <SettingsCard title={'Детали'} icon={<InfoOutlined />}>
            <Box className={'w-full'}>
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

                <Box className={'w-full flex justify-between gap-[1rem]'}>
                    <Controller
                        name="number"
                        control={props.control}
                        rules={{required: 'Обязательное поле'}}
                        render={({field}) => (
                            <TextField
                                {...field}
                                label="Артикул"
                                variant="outlined"
                                fullWidth
                                margin="normal"
                                autoComplete={'off'}
                                error={!!props.errors.number}
                                helperText={props.errors.number?.message}
                                InputProps={{readOnly: true}}
                            />
                        )}
                    />

                    <Controller
                        name="typeId"
                        control={props.control}
                        defaultValue={''}
                        render={({field}) => (
                            <FormControl
                                fullWidth
                                margin="normal"
                                error={!!props.errors.typeId}
                            >
                                <InputLabel>Тип товара</InputLabel>
                                <Select
                                    {...field}
                                    label="Тип товара"
                                    value={field.value || ''}
                                    onChange={(e) => {
                                        field.onChange(e.target.value);
                                        field.onBlur();
                                    }}
                                >
                                    <MenuItem key={'empty-type'} value=""></MenuItem>
                                    {props.productTypes.map((x) => (
                                        <MenuItem key={x.id} value={x.id}>
                                            {x.name}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        )}
                    />

                </Box>

                <Box className={'w-full flex justify-between gap-[1rem]'}>

                    <Controller
                        name="price"
                        control={props.control}
                        rules={{required: 'Обязательное поле'}}
                        render={({field}) => (
                            <NumericFormat
                                fullWidth
                                margin={'normal'}
                                customInput={TextField}
                                label="Цена"
                                defaultValue={0}
                                variant="outlined"
                                value={field.value}
                                onValueChange={(e) => field.onChange(e.floatValue)}
                                decimalScale={2}
                                fixedDecimalScale={true}
                                error={!!props.errors.price}
                                helperText={props.errors.price?.message}
                            />
                        )}
                    />

                    <Controller
                        name="measurementUnitCode"
                        control={props.control}
                        render={({field}) => (
                            <FormControl
                                fullWidth
                                margin="normal"
                                error={!!props.errors.measurementUnitCode}>
                                <InputLabel>Единица измерения</InputLabel>
                                <Select
                                    {...field}
                                    label="Единица измерения"
                                    onChange={(e) => {
                                        field.onChange(e.target.value);
                                        field.onBlur();
                                    }}
                                >
                                    {numberEnumToArray(ProductMeasurementUnitCode).sort(x => x).map((unit) => (
                                        <MenuItem key={unit} value={unit}>
                                            {longMeasurementUnitName(unit)}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        )}
                    />
                </Box>

            </Box>
        </SettingsCard>
    )
}

export default DetailsSettingsCard;