import {type Control, Controller, type FieldArrayWithId} from "react-hook-form";
import type {ChangeOrderForm} from "../../../../../models/orderModels.ts";
import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import {Box, TextField} from "@mui/material";
import TableRow from "../../../../../../shared/components/tables/TableRow.tsx";
import Typography from "../../../../../../shared/components/Typography.tsx";
import {CloseOutlined} from "@mui/icons-material";
import QuantityInput from "../../../../../../shared/components/quantity/QuantityInput.tsx";
import IconButton from "../../../../../../shared/components/buttons/IconButton.tsx";

type OrderProductsAmountGridSectionTableRowProps = {
    columns: TableColumnDefinition[];
    field: FieldArrayWithId<ChangeOrderForm, "products", "id">;
    productIndex: number;
    control: Control<ChangeOrderForm>;
    onRemove: (id?: string, index?: number) => void;
}

const OrderProductsAmountGridSectionTableRow = (props: OrderProductsAmountGridSectionTableRowProps) => {
    function getWidth(columnNumber: number) {
        return props.columns?.at(columnNumber)?.width || '100%'
    }

    return (
        <Box className={'w-full flex'}>
            <TableRow>
                <Box width={getWidth(0)}>
                    <Typography variant={'bodyRegular'}>{props.field.productName}</Typography>
                </Box>

                <Box width={getWidth(1)} className={'pr-[1rem]'}>
                    <Controller
                        name={`products.${props.productIndex}.supplierProductNumber`}
                        control={props.control}
                        render={({field}) => (
                            <TextField
                                {...field}
                                value={field.value || ''}
                                fullWidth
                                autoComplete={'off'}
                                placeholder={'Не указан'}
                            />
                        )}
                    />
                </Box>

                <Box width={getWidth(2)} className={'pr-[1rem]'}>
                    <Controller
                        name={`products.${props.productIndex}.ordered`}
                        control={props.control}
                        rules={{required: 'Количество обязательно'}}
                        render={({field, fieldState}) => (
                            <QuantityInput
                                field={field}
                                fieldState={fieldState}
                            />
                        )}
                    />
                </Box>

                <Box width={getWidth(3)} onClick={() => {
                }}>
                    <IconButton
                        description={'Убрать из заказа'}
                        hasBackground={true}
                        onClick={() => props.onRemove(props.field.productId, props.productIndex)}
                    >
                        <CloseOutlined/>
                    </IconButton>
                </Box>

            </TableRow>
        </Box>
    )
}

export default OrderProductsAmountGridSectionTableRow;