import type {ReactNode} from "react";
import {Box} from "@mui/material";
import {primitives} from "../../assets/variables/primitives.ts";
import {semanticColors} from "../../assets/variables/semanticColors.ts";
import Typography from "./Typography.tsx";

type ChipProps = {
    title: string;
    icon?: ReactNode;
    color?: ChipColor;
}

export type ChipColor = "active" | "inactive" | "error";

const colors = {
    "active": {
        textColor: primitives.colors.primary,
        backgroundColor: primitives.colors.tonal
    },
    "inactive": {
        textColor: semanticColors.text.secondary,
        backgroundColor: semanticColors.surface.light
    },
    "error": {
        textColor: semanticColors.error.primary,
        backgroundColor: semanticColors.error.tonal
    }
}

const Chip = (props: ChipProps) => {
    const backgroundColor = props.color ? colors[props.color ?? "active"].backgroundColor : colors["active"].backgroundColor;
    const textColor = props.color ? colors[props.color ?? "active"].textColor : colors["active"].textColor;

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                backgroundColor: backgroundColor,
                color: textColor,
                borderRadius: primitives.border.radius,
                padding: '0.5rem 1rem',
            }}>
            {props.icon &&
                <Box sx={{
                    color: textColor,
                    height: '100%',
                    display: 'flex',
                    marginRight: '0.5rem'
                }}>
                    {props.icon}
                </Box>

            }
            <Typography variant={'bodySmall'} color={textColor} sx={{whiteSpace: 'nowrap'}}>{props.title}</Typography>
        </Box>
    )
}

export default Chip;