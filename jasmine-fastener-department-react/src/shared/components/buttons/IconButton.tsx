import {type MouseEventHandler, type ReactNode} from "react";
import {Button, CircularProgress, Tooltip} from "@mui/material";
import {primitives} from "../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";

type IconButtonProps = {
    isActive?: boolean;
    description: string;
    isDisabled?: boolean;
    hasBackground?: boolean;
    isLoading?: boolean;
    onClick: MouseEventHandler<HTMLButtonElement> | undefined;
    children: ReactNode;
}

const IconButton = (props: IconButtonProps) => {
    const isActive = props.isActive ?? true;
    const isDisabled = props.isDisabled || props.isLoading;
    const backgroundColor = !isDisabled ? primitives.colors.tonal : semanticColors.surface.medium;

    return (
        <Tooltip title={!isDisabled ? props.description : ''}>
            <Button
                onClick={!isDisabled ? props.onClick : undefined}
                sx={{
                    color: isActive && !isDisabled ? primitives.colors.primary : semanticColors.text.secondary,
                    cursor: !isDisabled ? 'pointer' : 'default',
                    minWidth: 0,
                    backgroundColor: props.hasBackground ? backgroundColor : undefined,
                    borderRadius: primitives.border.radius,
                }}>
                {
                    props.isLoading ?
                        <CircularProgress size={24} thickness={5.6} sx={{color: primitives.colors.primary}}/> :
                        props.children
                }
            </Button>
        </Tooltip>
    );
}

export default IconButton;