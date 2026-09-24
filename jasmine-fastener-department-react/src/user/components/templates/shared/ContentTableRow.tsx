import type {ContentTableColumn, TemplateContentTableColumn} from "../../../models/templateModels.ts";
import {Box} from "@mui/material";
import Typography from "../../../../shared/components/Typography.tsx";
import {primitives} from "../../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../../assets/variables/semanticColors.ts";
import {memo} from "react";

type ContentTableRowProps = {
    column: TemplateContentTableColumn;
    isChecked: boolean;
    index?: number;
    onChange: (code: ContentTableColumn) => void;
}

const ContentTableRow = (props: ContentTableRowProps) => {
    return (
        <Box
            onClick={() => props.onChange(props.column.code)}
            sx={{
                display: 'flex',
                justifyContent: 'space-between',
                backgroundColor: props.isChecked ? primitives.colors.tonal : '',
                border: `1px solid ${props.isChecked ? primitives.colors.primary : semanticColors.text.secondary}`,
                borderRadius: primitives.border.radius,
                padding: '0.5rem 1rem',
                cursor: 'pointer',
                whiteSpace: 'nowrap'
            }}>
            <Typography
                variant={props.isChecked ? 'bodyRegularBold' : 'bodyRegular'}
                color={props.isChecked ? 'primary' : ''}>
                {props.column.name}
            </Typography>

            <Typography
                variant={props.isChecked ? 'bodyRegularBold' : 'bodyRegular'}
                color={props.isChecked ? 'primary' : 'tertiary'}>
                {props.index ?? 0}
            </Typography>
        </Box>
    )
}

export default memo(ContentTableRow);