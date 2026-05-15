import {Box} from "@mui/material";
import TableRow from "../../../../shared/components/tables/TableRow.tsx";
import Typography from "../../../../shared/components/Typography.tsx";
import {type Control, Controller, type FieldArrayWithId, useWatch} from "react-hook-form";
import QuantityInput from "../../../../shared/components/quantity/QuantityInput.tsx";
import type {TableColumnDefinition} from "../../../../shared/models/models.ts";
import type {CompleteOrderForm} from "../../../models/orderModels.ts";
import TextToggle from "../../../../shared/components/toggles/TextToggle.tsx";
import QuantityChip from "../../../../shared/components/quantity/QuantityChip.tsx";
import {PlaylistRemoveOutlined, PrintOutlined} from "@mui/icons-material";
import IconButton from "../../../../shared/components/buttons/IconButton.tsx";

type CompleteOrderProductFormSectionTableRowProps = {
    columns: TableColumnDefinition[];
    field: FieldArrayWithId<CompleteOrderForm, "products", "id">;
    productIndex: number;
    control: Control<CompleteOrderForm>;
}

const CompleteOrderProductFormSectionTableRow = (props: CompleteOrderProductFormSectionTableRowProps) => {

    const isFulfilled = useWatch({
        control: props.control,
        name: `products.${props.productIndex}.isFulfilled`,
    });

    const opacity = isFulfilled ? 1 : 0.3;

    function getWidth(columnNumber: number) {
        return props.columns?.at(columnNumber)?.width || '100%'
    }

    return (
        <Box className={'w-full flex'}>
            <TableRow>
                <Box width={getWidth(0)}>
                    <Controller
                        name={`products.${props.productIndex}.isFulfilled`}
                        control={props.control}
                        render={({field}) => (
                            <TextToggle
                                checked={field.value}
                                inactiveText={'Не доставлено'}
                                activeText={'Доставлено'}
                                onChange={() => field.onChange(!field.value)}
                            />
                        )}
                    />
                </Box>

                <Box width={getWidth(1)}>
                    <Typography variant={'bodyRegular'}>{props.field.productName}</Typography>
                    <Typography variant={'bodySmall'}>{props.field.supplierProductNumber}</Typography>
                </Box>

                <Box width={getWidth(2)} className={'flex justify-between gap-[1rem]'} sx={{opacity: opacity}}>
                    <QuantityChip type={'inactive'} title={'Заказано'} quantity={props.field.ordered}/>

                    <Controller
                        name={`products.${props.productIndex}.fulfilled`}
                        control={props.control}
                        rules={{required: 'Количество обязательно'}}
                        render={({field, fieldState}) => (
                            <Box>
                                <Typography
                                    variant={'labelRegular'}
                                    sx={{marginBottom: '0.5rem'}}>
                                    Доставлено
                                </Typography>

                                <QuantityInput
                                    field={field}
                                    fieldState={fieldState}
                                />
                            </Box>
                        )}
                    />
                </Box>

                <Box width={getWidth(3)} sx={{opacity: opacity}}>
                    <Box className={'flex justify-center items-center gap-[1rem]'}>
                        <Controller
                            name={`products.${props.productIndex}.addToPrint`}
                            control={props.control}
                            render={({field}) => (
                                <IconButton
                                    isActive={field.value}
                                    description={'Добавить в список печати'}
                                    onClick={() => {
                                        field.onChange(!field.value)
                                    }}
                                >
                                    <PrintOutlined/>
                                </IconButton>
                            )}
                        />

                        <Controller
                            name={`products.${props.productIndex}.removeOrderStatus`}
                            control={props.control}
                            render={({field}) => (
                                <IconButton
                                    isActive={field.value}
                                    description={'Убрать из списка заказа'}
                                    onClick={() => {
                                        field.onChange(!field.value)
                                    }}
                                >
                                    <PlaylistRemoveOutlined/>
                                </IconButton>
                            )}
                        />
                    </Box>
                </Box>

            </TableRow>
        </Box>
    )
}

export default CompleteOrderProductFormSectionTableRow;