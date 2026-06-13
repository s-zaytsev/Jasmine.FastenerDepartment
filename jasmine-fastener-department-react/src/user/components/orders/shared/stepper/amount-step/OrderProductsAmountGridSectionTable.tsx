import type {ChangeOrderForm} from "../../../../../models/orderModels.ts";
import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import SortTableHead from "../../../../../../shared/components/tables/SortTableHead.tsx";
import {Box} from "@mui/material";
import type {Control, FieldArrayWithId} from "react-hook-form";
import OrderProductsAmountGridSectionTableRow from "./OrderProductsAmountGridSectionTableRow.tsx";
import {memo} from "react";

type OrderProductsAmountGridSectionTableProps = {
    rows: Array<{
        field: FieldArrayWithId<ChangeOrderForm, "products", "id">;
        productIndex: number;
    }>;
    control: Control<ChangeOrderForm>;
    onRemove: (id?: string, index?: number) => void;
}

const columns: TableColumnDefinition[] = [
    {title: "Наименование"},
    {title: "Артикул", width: '35%'},
    {title: "Количество", width: '35%'},
    {title: "", width: '5%'}
];

const OrderProductsAmountGridSectionTable = (props: OrderProductsAmountGridSectionTableProps) => {

    return (
        <Box className={'w-full mt-[0.5rem]'}>
            <SortTableHead columns={columns}/>

            {props?.rows?.map(({field, productIndex}) => (
                <OrderProductsAmountGridSectionTableRow
                    key={field.id}
                    field={field}
                    productIndex={productIndex}
                    columns={columns}
                    control={props.control}
                    onRemove={props.onRemove}
                />
            ))}
        </Box>
    )
}

export default memo(OrderProductsAmountGridSectionTable);