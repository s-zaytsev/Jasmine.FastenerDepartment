import type {ChangeOrderProduct} from "../../../../../models/orderModels.ts";
import type {TableColumnDefinition} from "../../../../../../shared/models/models.ts";
import {Box} from "@mui/material";
import SortTableHead from "../../../../../../shared/components/tables/SortTableHead.tsx";
import ConfirmOrderSectionTableRow from "./ConfirmOrderSectionTableRow.tsx";

type ConfirmOrderSectionTableProps = {
    products: ChangeOrderProduct[];
}

const ConfirmOrderSectionTable = (props: ConfirmOrderSectionTableProps) => {
    const columns: TableColumnDefinition[] = [
        {title: "Артикул", width: '35%', columnAlign: 'center'},
        {title: "Наименование"},
        {title: "Количество", width: '35%', columnAlign: 'center'}
    ];

    return (
        <Box className={'w-full'}>
            <SortTableHead columns={columns}/>
            {
                props?.products?.map(product => (
                    <ConfirmOrderSectionTableRow
                        key={product.id}
                        product={product}
                        columns={columns}
                    />
                ))}
        </Box>
    )
}

export default ConfirmOrderSectionTable;

