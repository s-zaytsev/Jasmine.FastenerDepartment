import {Box} from "@mui/material";
import {primitives} from "../../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../../assets/variables/semanticColors.ts";
import Typography from "../../../../shared/components/Typography.tsx";
import type {Company} from "../../../models/companyModels.ts";
import {memo} from "react";

type CompaniesCardItemProps = {
    company: Company;
    isActive: boolean;
    onChange: (id: string) => void;
}

const CompaniesCardItem = (props: CompaniesCardItemProps) => {
    return (
        <Box
            onClick={() => props.onChange(props.company.id)}
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
                {props.company.title}
            </Typography>
        </Box>
    );
}

export default memo(CompaniesCardItem);