import type {ReactNode} from "react";
import {Box, type SxProps} from "@mui/material";
import {primitives} from "../../assets/variables/primitives.ts";
import {neutralColors} from "../../assets/variables/neutralColors.ts";

type CardProps = {
    hasHighlight?: boolean;
    backgroundColor?: string;
    onClick?: () => void;
    children: ReactNode;
    sx?: SxProps;
}

const Card = (props: CardProps) => {
    return (
        <Box
            onClick={props.onClick}
            sx={{
                backgroundColor: props.backgroundColor || neutralColors.white,
                borderRadius: primitives.border.radius,
                margin: '5px 0',
                padding: '1rem',
                width: '100%',
                height: '100%',
                alignItems: 'center',
                display: 'flex',
                cursor: props.onClick ? 'pointer' : 'default',
                '&:hover': !props.hasHighlight ? {} : {
                    backgroundColor: primitives.colors.tonal
                },
                ...props.sx
            }}>
            {props.children}
        </Box>
    )
}

export default Card;