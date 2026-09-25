import type {TemplateFormatCode} from "../../models/templateModels.ts";
import {Box} from "@mui/material";
import {primitives} from "../../../assets/variables/primitives.ts";
import Typography from "../../../shared/components/Typography.tsx";

type ExportFormatGridItemProps = {
    type: TemplateFormatCode;
    title: string;
    subtitle: string;
    isActive: boolean;
    onChange: (code: TemplateFormatCode) => void;
}

const ExportFormatGridItem = (props: ExportFormatGridItemProps) => {
    return (
        <Box
            onClick={() => props.onChange(props.type)}
            sx={{
                width: '100%',
                padding: '0.5rem',
                display: 'flex',
                gap: '0.2rem',
                flexDirection: 'column',
                alignItems: 'center',
                border: `1.5px solid ${props.isActive ? primitives.colors.primary : 'transparent'}`,
                borderRadius: primitives.border.radius,
                cursor: 'pointer'
            }}>
            <Typography variant={'bodySmallBold'} color={props.isActive ? 'primary' : 'tertiary'}>
                {props.title}
            </Typography>

            <Typography variant={'labelSmallBold'} color={props.isActive ? 'primary' : 'tertiary'}>
                {props.subtitle}
            </Typography>
        </Box>
    )
}

export default ExportFormatGridItem;