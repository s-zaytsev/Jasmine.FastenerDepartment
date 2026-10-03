import {Box} from "@mui/material";
import {primitives} from "../../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../../assets/variables/semanticColors.ts";
import Typography from "../../../../shared/components/Typography.tsx";
import type {Recipient} from "../../../models/recipientModels.ts";
import {memo} from "react";

type RecipientsCardItemProps = {
    recipient: Recipient;
    isActive: boolean;
    onChange: (id: string) => void;
}

const RecipientsCardItem = (props: RecipientsCardItemProps) => {
    return (
        <Box
            onClick={() => props.onChange(props.recipient.id)}
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
                {props.recipient.name}
            </Typography>

            <Typography
                variant={props.isActive ? 'bodyRegularBold' : 'bodyRegular'}
                color={props.isActive ? 'primary' : ''}>
                {props.recipient.email}
            </Typography>
        </Box>
    );
}

export default memo(RecipientsCardItem);