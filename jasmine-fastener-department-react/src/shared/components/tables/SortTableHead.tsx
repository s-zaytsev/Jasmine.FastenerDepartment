import {Box} from "@mui/material";
import type {TableColumnDefinition} from "../../models/models.ts";
import SortTableHeadLabel from "./SortTableHeadLabel.tsx";

type SortTableHeadProps = {
    columns: TableColumnDefinition[];
    sortBy?: number;
    sortDesc?: boolean;
    onSort?: (parameter: number) => void;
}

const SortTableHead = (props: SortTableHeadProps) => {
    const getDirection = (parameter?: number): "asc" | "desc" | undefined => {
        return parameter !== props.sortBy ? "asc" :
            props.sortDesc ? "desc" :
                "asc";
    }

    return (
        <Box className={'flex product-table-head-row'}>
            {props.columns.map((column, index) => (
                <Box key={`${index}`} style={{
                    textAlign: column?.columnAlign as CanvasTextAlign,
                    width: column?.width || '100%'
                }}>
                    {column.parameter !== undefined ? (
                        <SortTableHeadLabel
                            isActive={column.parameter === props.sortBy}
                            direction={getDirection(column.parameter!)}
                            onClick={() => props.onSort && props.onSort(column.parameter!)}
                            title={column.title}/>
                    ) : (
                        <Box sx={{cursor: 'default'}}>
                            {column.title}
                        </Box>
                    )}
                </Box>
            ))}
        </Box>
    );
};

export default SortTableHead;
