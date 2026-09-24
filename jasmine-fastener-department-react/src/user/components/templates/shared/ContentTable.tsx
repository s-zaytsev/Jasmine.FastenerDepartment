import type {ContentTableColumn, TemplateContentTableColumn} from "../../../models/templateModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import ContentTableRow from "./ContentTableRow.tsx";
import {memo} from "react";

type ContentTableProps = {
    columns: TemplateContentTableColumn[];
    usedColumns: ContentTableColumn[];
    onChange: (code: ContentTableColumn) => void;
}

const ContentTable = (props: ContentTableProps) => {
    return (
        <Box className={'w-full flex flex-col gap-[0.5rem]'}>
            <Typography variant={'labelRegular'}>Отображаемые колонки</Typography>
            {props.columns.map(column =>
                <ContentTableRow
                    key={column.code}
                    column={column}
                    index={props.usedColumns.indexOf(column.code) + 1}
                    isChecked={props.usedColumns.includes(column.code)}
                    onChange={props.onChange}
                />
            )}
        </Box>
    )
}

export default memo(ContentTable);