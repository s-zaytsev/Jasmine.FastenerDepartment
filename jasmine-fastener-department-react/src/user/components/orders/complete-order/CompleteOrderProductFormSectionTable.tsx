import type {Control, FieldArrayWithId} from "react-hook-form";
import type {CompleteOrderForm} from "../../../models/orderModels.ts";
import type {TableColumnDefinition} from "../../../../shared/models/models.ts";
import {Box} from "@mui/material";
import SortTableHead from "../../../../shared/components/tables/SortTableHead.tsx";
import CompleteOrderProductFormSectionTableRow from "./CompleteOrderProductFormSectionTableRow.tsx";

type CompleteOrderProductFormSectionTableProps = {
    rows: Array<{
        field: FieldArrayWithId<CompleteOrderForm, "products", "id">;
        productIndex: number;
    }>;
    control: Control<CompleteOrderForm>;
}

const CompleteOrderProductFormSectionTable = (props: CompleteOrderProductFormSectionTableProps) => {
    const columns: TableColumnDefinition[] = [
        {title: 'Статус', width: '30%'},
        {title: "Наименование"},
        {title: "Количество", width: '35%'},
        {title: "", width: '20%'}
    ];

    return (
        <Box className={'w-full mt-[0.5rem]'}>
            <SortTableHead columns={columns}/>

            {props?.rows?.map(({field, productIndex}) => (
                <CompleteOrderProductFormSectionTableRow
                    key={field.id}
                    field={field}
                    productIndex={productIndex}
                    columns={columns}
                    control={props.control}
                />
            ))}
        </Box>
    )
}

export default CompleteOrderProductFormSectionTable;