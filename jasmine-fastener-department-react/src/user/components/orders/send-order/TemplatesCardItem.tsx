import {type Template} from "../../../models/templateModels.ts";
import {Box} from "@mui/material";
import {primitives} from "../../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../../assets/variables/semanticColors.ts";
import Typography from "../../../../shared/components/Typography.tsx";
import {memo} from "react";

type TemplatesCardItemProps = {
    template: Template;
    isActive: boolean;
    onChange: (id: string) => void;
}

const TemplatesCardItem = (props: TemplatesCardItemProps) => {
    return (
        <Box
            onClick={() => props.onChange(props.template.id)}
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
                {props.template.name}
            </Typography>
        </Box>
    );
}

export default memo(TemplatesCardItem);