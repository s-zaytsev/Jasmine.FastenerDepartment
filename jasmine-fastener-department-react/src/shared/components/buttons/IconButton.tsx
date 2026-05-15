import {type MouseEventHandler, type ReactNode} from "react";
import {Button, Tooltip} from "@mui/material";
import {primitives} from "../../../assets/variables/primitives.ts";
import {semanticColors} from "../../../assets/variables/semanticColors.ts";

type IconButtonProps = {
    isActive?: boolean;
    description: string;
    isDisabled?: boolean;
    hasBackground?: boolean;
    onClick: MouseEventHandler<HTMLButtonElement> | undefined;
    children: ReactNode;
}

const IconButton = (props: IconButtonProps) => {
    const isActive = props.isActive ?? true;
    const isDisabled = props.isDisabled;

    return (
        <Tooltip title={!isDisabled ? props.description : ''}>
            <Button
                onClick={!isDisabled ? props.onClick : undefined}
                sx={{
                    color: isActive && !isDisabled ? primitives.colors.primary : semanticColors.text.secondary,
                    cursor: !isDisabled ? 'pointer' : 'default',
                    minWidth: 0,
                    backgroundColor: props.hasBackground ? primitives.colors.tonal : undefined,
                    borderRadius: primitives.border.radius,
                }}>
                {props.children}
            </Button>
        </Tooltip>
    );
}

export default IconButton;