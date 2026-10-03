import type {TemplateFormatCode} from "../../../models/templateModels.ts";
import {memo} from "react";
import {Box} from "@mui/material";
import {primitives} from "../../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../../assets/variables/semanticColors.ts";
import Typography from "../../../../shared/components/Typography.tsx";

type FormatsCardItemProps = {
    code: TemplateFormatCode;
    title: string;
    extension: string;
    isActive: boolean;
    onChange: (code: TemplateFormatCode) => void;
}

const FormatsCardItem = (props: FormatsCardItemProps) => {
    return (
        <Box
            onClick={() => props.onChange(props.code)}
            sx={{
                display: 'flex',
                justifyContent: 'space-between',
                backgroundColor: props.isActive ? primitives.colors.tonal : '',
                border: `1px solid ${props.isActive ? primitives.colors.primary : semanticColors.text.secondary}`,
                borderRadius: primitives.border.radius,
                padding: '0.5rem 1rem',
                cursor: 'pointer',
                whiteSpace: 'nowrap'
            }}>
            <Typography
                variant={props.isActive ? 'bodyRegularBold' : 'bodyRegular'}
                color={props.isActive ? 'primary' : ''}>
                {props.title}
            </Typography>

            <Typography
                variant={props.isActive ? 'bodyRegularBold' : 'bodyRegular'}
                color={props.isActive ? 'primary' : ''}>
                {props.extension}
            </Typography>
        </Box>
    );
}

export default memo(FormatsCardItem);